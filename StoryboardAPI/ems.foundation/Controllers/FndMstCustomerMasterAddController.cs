using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using ems.foundation.Models;
using ems.foundation.DataAccess;
using System.Web;

namespace ems.foundation.Controllers
{


    [RoutePrefix("api/FndMstCustomerMasterAdd")]
    [Authorize]
    public class FndMstCustomerMasterAddController : ApiController
    {
        session_values ObjGetGID = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaFndMstCustomerMasterAdd objDaFndMstCustomerMasterAdd = new DaFndMstCustomerMasterAdd();

        [ActionName("PostMobileNo")]
        [HttpPost]
        public HttpResponseMessage PostMobileNo(MdlcustomerMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaFndMstCustomerMasterAdd.DaPostMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMobileNoList")]
        [HttpGet]
        public HttpResponseMessage GetMobileNoList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlcustomerMobileNo values = new MdlcustomerMobileNo();
            objDaFndMstCustomerMasterAdd.DaGetMobileNoList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //----------- Delete Mobile No----------//
        [ActionName("DeleteMobileNo")]
        [HttpGet]
        public HttpResponseMessage deleteMmobileno(string customer2mobileno_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlcustomerMobileNo values = new MdlcustomerMobileNo();
            objDaFndMstCustomerMasterAdd.DaDeleteMobileNo(customer2mobileno_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MobileNoTempList")]
        [HttpGet]
        public HttpResponseMessage MobileNoTempList(string customer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlcustomerMobileNo values = new MdlcustomerMobileNo();
            objDaFndMstCustomerMasterAdd.DaMobileNoTempList(customer_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostEmailAddress")]
        [HttpPost]
        public HttpResponseMessage PostEmailAddress(MdlcustomerEmailAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaFndMstCustomerMasterAdd.DaPostEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEmailAddressList")]
        [HttpGet]
        public HttpResponseMessage GetEmailAddressList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlcustomerEmailAddress values = new MdlcustomerEmailAddress();
            objDaFndMstCustomerMasterAdd.DaGetEmailAddressList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EmailTempList")]
        [HttpGet]
        public HttpResponseMessage EmailTempList(string customer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlcustomerEmailAddress values = new MdlcustomerEmailAddress();
            objDaFndMstCustomerMasterAdd.DaEmailTempList(customer_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteEmailAddress")]
        [HttpGet]
        public HttpResponseMessage DeleteEmailAddress(string customer2emailaddress_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlcustomerEmailAddress values = new MdlcustomerEmailAddress();
            objDaFndMstCustomerMasterAdd.DaDeleteEmailAddress(customer2emailaddress_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPendingCustomer")]
        [HttpGet]
        public HttpResponseMessage GetPendingCustomer()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlFndMstCustomerMasterAdd values = new MdlFndMstCustomerMasterAdd();
            objDaFndMstCustomerMasterAdd.DaGetPendingCustomer(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostAddress")]
        [HttpPost]
        public HttpResponseMessage PostAaddress(MdlcustomerAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaFndMstCustomerMasterAdd.DaPostAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAddressList")]
        [HttpGet]
        public HttpResponseMessage GetAddressList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlcustomerAddress values = new MdlcustomerAddress();
            objDaFndMstCustomerMasterAdd.DaGetAddressList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteAddress")]
        [HttpGet]
        public HttpResponseMessage DeleteAddress(string customer2address_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlcustomerAddress values = new MdlcustomerAddress();
            objDaFndMstCustomerMasterAdd.DaDeleteAddress(customer2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Updatecustomer")]
        [HttpPost]
        public HttpResponseMessage Updatecustomer(MdlFndMstCustomerMasterAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaFndMstCustomerMasterAdd.DaUpdatecustomer(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCustomerCounts")]
        [HttpGet]
        public HttpResponseMessage GetCustomerCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlTrnCampaign values = new MdlTrnCampaign();
            objDaFndMstCustomerMasterAdd.DaGetCustomerCounts(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCustomerViewCounts")]
        [HttpGet]
        public HttpResponseMessage GetCustomerViewCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlTrnCampaign values = new MdlTrnCampaign();
            objDaFndMstCustomerMasterAdd.DaGetCustomerViewCounts(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //[ActionName("PostBank")]
        //[HttpPost]
        //public HttpResponseMessage PostBank(MdlcustomerBank values)
        //{
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = ObjGetGID.gettokenvalues(token);
        //    objDaFndMstCustomerMasterAdd.DaPostBank(getsessionvalues.employee_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        //[ActionName("GetBankList")]
        //[HttpGet]
        //public HttpResponseMessage GetBankList()
        //{
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = ObjGetGID.gettokenvalues(token);
        //    MdlcustomerBank values = new MdlcustomerBank();
        //    objDaFndMstCustomerMasterAdd.DaGetBankList(getsessionvalues.employee_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        //[ActionName("DeleteBank")]
        //[HttpGet]
        //public HttpResponseMessage DeleteBank(string customer2bank_gid)
        //{
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = ObjGetGID.gettokenvalues(token);
        //    MdlcustomerBank values = new MdlcustomerBank();
        //    objDaFndMstCustomerMasterAdd.DaDeleteBank(customer2bank_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}


        [ActionName("GetYearsAndMonthsInBusiness")]
        [HttpGet]
        public HttpResponseMessage getage(string businessstart_date)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlFndMstCustomerMasterAdd values = new MdlFndMstCustomerMasterAdd();
            objDaFndMstCustomerMasterAdd.DaGetYearsAndMonthsInBusiness(businessstart_date, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("customerSave")]
        [HttpPost]
        public HttpResponseMessage customerSave(MdlFndMstCustomerMasterAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaFndMstCustomerMasterAdd.DacustomerSave(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("customerSubmit")]
        [HttpPost]
        public HttpResponseMessage customerSubmit(MdlFndMstCustomerMasterAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaFndMstCustomerMasterAdd.DacustomerSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetcustomerSummary")]
        [HttpGet]
        public HttpResponseMessage GetcustomerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlFndMstCustomerMasterAdd values = new MdlFndMstCustomerMasterAdd();
            objDaFndMstCustomerMasterAdd.DaGetcustomerSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Editcustomer")]
        [HttpGet]
        public HttpResponseMessage Editcustomer(string customer_gid)
        {
            MdlFndMstCustomerMasterAdd values = new MdlFndMstCustomerMasterAdd();
            objDaFndMstCustomerMasterAdd.DaEditcustomer(customer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Inactivecustomer")]
        [HttpPost]
        public HttpResponseMessage Inactivecustomer(MdlFndMstCustomerMasterAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaFndMstCustomerMasterAdd.DaInactivecustomer(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("customerInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage customerInactiveLogview(string customer_gid)
        {
            MdlFndMstCustomerMasterAdd values = new MdlFndMstCustomerMasterAdd();
            objDaFndMstCustomerMasterAdd.DacustomerInactiveLogview(customer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



        [ActionName("GetcustomerApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetcustomerApprovalSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlFndMstCustomerMasterAdd values = new MdlFndMstCustomerMasterAdd();
            objDaFndMstCustomerMasterAdd.DaGetcustomerApprovalSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("Getcustomerapprover")]
        [HttpGet]
        public HttpResponseMessage Getcustomerapprover()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlFndMstCustomerMasterAdd values = new MdlFndMstCustomerMasterAdd();
            objDaFndMstCustomerMasterAdd.DaGetcustomerapprover(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getcustomerapproverview")]
        [HttpGet]
        public HttpResponseMessage Getcustomerapproverview()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlFndMstCustomerMasterAdd values = new MdlFndMstCustomerMasterAdd();
            objDaFndMstCustomerMasterAdd.DaGetcustomerapproverview(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getcustomerreject")]
        [HttpGet]
        public HttpResponseMessage Getcustomerreject()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlFndMstCustomerMasterAdd values = new MdlFndMstCustomerMasterAdd();
            objDaFndMstCustomerMasterAdd.DaGetcustomerreject(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getcustomerapprovalreject")]
        [HttpGet]
        public HttpResponseMessage Getcustomerapprovalreject()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlFndMstCustomerMasterAdd values = new MdlFndMstCustomerMasterAdd();
            objDaFndMstCustomerMasterAdd.DaGetcustomerapprovalreject(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Deletecustomer")]
        [HttpGet]
        public HttpResponseMessage Deletecustomer(string customer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlFndMstCustomerMasterAdd values = new MdlFndMstCustomerMasterAdd();
            objDaFndMstCustomerMasterAdd.DaDeletecustomer(customer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostGST")]
        [HttpPost]
        public HttpResponseMessage PostGSTDetail(MdlcustomerGST values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaFndMstCustomerMasterAdd.DaPostGST(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostGSTList")]
        [HttpPost]
        public HttpResponseMessage PostGSTList(MdlcustomerGST values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaFndMstCustomerMasterAdd.DaPostGSTList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetGSTList")]
        [HttpGet]
        public HttpResponseMessage GetGSTDetailsList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlcustomerGST values = new MdlcustomerGST();
            objDaFndMstCustomerMasterAdd.DaGetGSTList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("customerGSTList")]
        [HttpGet]
        public HttpResponseMessage customerGSTList(string customer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlFndMstCustomerMasterAdd values = new MdlFndMstCustomerMasterAdd();
            objDaFndMstCustomerMasterAdd.DacustomerGSTList(customer_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("customerMobileNoList")]
        [HttpGet]
        public HttpResponseMessage customerMobileNoList(string customer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            Mdlmobile_no values = new Mdlmobile_no();
            objDaFndMstCustomerMasterAdd.DacustomerMobileNoList(customer_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



        [ActionName("customerEmailAddressList")]
        [HttpGet]
        public HttpResponseMessage customerEmailAddressList(string customer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlEmail_address values = new MdlEmail_address();
            objDaFndMstCustomerMasterAdd.DacustomerEmailAddressList(customer_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ChequeDocumentList")]
        [HttpGet]
        public HttpResponseMessage ChequeDocumentList(string fndmanagement2cheque_gid)
        {
            MdlChequeDocument values = new MdlChequeDocument();
            objDaFndMstCustomerMasterAdd.DaChequeDocumentList(fndmanagement2cheque_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetDropDownUdc")]
        [HttpGet]
        public HttpResponseMessage GetDropDownUdc()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlDropDownUdc values = new MdlDropDownUdc();
            objDaFndMstCustomerMasterAdd.DaGetDropDownUdc(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("customerAddressList")]
        [HttpGet]
        public HttpResponseMessage customerAddressList(string customer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlcustomerAddress values = new MdlcustomerAddress();
            objDaFndMstCustomerMasterAdd.DacustomerAddressList(customer_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("DeleteGST")]
        [HttpGet]
        public HttpResponseMessage DeleteGST(string customer2gst_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlcustomerGST values = new MdlcustomerGST();
            objDaFndMstCustomerMasterAdd.DaDeleteGST(customer2gst_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteGSTCustomer")]
        [HttpGet]
        public HttpResponseMessage DeleteGSTcustomer(string customer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlcustomerGST values = new MdlcustomerGST();
            objDaFndMstCustomerMasterAdd.DaDeleteGSTcustomer(getsessionvalues.employee_gid, customer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPostalCodeDetails")]
        [HttpGet]
        public HttpResponseMessage GetPostalCodeDetails(string postal_code)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlcustomerAddress values = new MdlcustomerAddress();
            objDaFndMstCustomerMasterAdd.DaGetPostalCodeDetails(postal_code, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Temp Clear 
        [ActionName("GetcustomerTempClear")]
        [HttpGet]
        public HttpResponseMessage GetcustomerTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            result values = new result();
            objDaFndMstCustomerMasterAdd.DaGetcustomerTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getconstitution")]
        [HttpGet]
        public HttpResponseMessage Getconstitution()
        {
            MdlFndMstCustomerMasterAdd objconstitution = new MdlFndMstCustomerMasterAdd();
            objDaFndMstCustomerMasterAdd.DaGetconstitution(objconstitution);
            return Request.CreateResponse(HttpStatusCode.OK, objconstitution);
        }
        [ActionName("Getassessmentagency")]
        [HttpGet]
        public HttpResponseMessage Getassessmentagency()
        {
            MdlFndMstCustomerMasterAdd objassessmentagency = new MdlFndMstCustomerMasterAdd();
            objDaFndMstCustomerMasterAdd.DaGetassessmentagency(objassessmentagency);
            return Request.CreateResponse(HttpStatusCode.OK, objassessmentagency);
        }

        [ActionName("Getassessmentagencyrating")]
        [HttpGet]
        public HttpResponseMessage Getassessmentagencyrating()
        {
            MdlFndMstCustomerMasterAdd objassessmentagencyrating = new MdlFndMstCustomerMasterAdd();
            objDaFndMstCustomerMasterAdd.DaGetassessmentagencyrating(objassessmentagencyrating);
            return Request.CreateResponse(HttpStatusCode.OK, objassessmentagencyrating);
        }


        [ActionName("Getamlcategory")]
        [HttpGet]
        public HttpResponseMessage Getamlcategory()
        {
            MdlFndMstCustomerMasterAdd objamlcategory = new MdlFndMstCustomerMasterAdd();
            objDaFndMstCustomerMasterAdd.DaGetamlcategory(objamlcategory);
            return Request.CreateResponse(HttpStatusCode.OK, objamlcategory);
        }

        [ActionName("Getbusinesscategory")]
        [HttpGet]
        public HttpResponseMessage Getbusinesscategory()
        {
            MdlFndMstCustomerMasterAdd objbusinesscategory = new MdlFndMstCustomerMasterAdd();
            objDaFndMstCustomerMasterAdd.DaGetbusinesscategory(objbusinesscategory);
            return Request.CreateResponse(HttpStatusCode.OK, objbusinesscategory);
        }

        [ActionName("Getdesignation")]
        [HttpGet]
        public HttpResponseMessage Getdesignation()
        {
            MdlFndMstCustomerMasterAdd objdesignation = new MdlFndMstCustomerMasterAdd();
            objDaFndMstCustomerMasterAdd.DaGetdesignation(objdesignation);
            return Request.CreateResponse(HttpStatusCode.OK, objdesignation);
        }

        [ActionName("Getindividualproof")]
        [HttpGet]
        public HttpResponseMessage Getindividualproof()
        {
            MdlFndMstCustomerMasterAdd objindividualproof = new MdlFndMstCustomerMasterAdd();
            objDaFndMstCustomerMasterAdd.DaGetindividualproof(objindividualproof);
            return Request.CreateResponse(HttpStatusCode.OK, objindividualproof);
        }



        [ActionName("state")]
        [HttpGet]
        public HttpResponseMessage State()
        {
            MdlFndMstCustomerMasterAdd objState = new MdlFndMstCustomerMasterAdd();
            objDaFndMstCustomerMasterAdd.DaGetState(objState);
            return Request.CreateResponse(HttpStatusCode.OK, objState);
        }
        // customer Basic Details Update
        [ActionName("customerEditSave")]
        [HttpPost]
        public HttpResponseMessage customerEditSave(MdlFndMstCustomerMasterAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaFndMstCustomerMasterAdd.DacustomerEditSave(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("customerEditSubmit")]
        [HttpPost]
        public HttpResponseMessage customerEditSubmit(MdlFndMstCustomerMasterAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaFndMstCustomerMasterAdd.DacustomerEditSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("customerEditupdated")]
        [HttpPost]
        public HttpResponseMessage customerEditupdated(MdlFndMstCustomerMasterAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaFndMstCustomerMasterAdd.DacustomerEditupdated(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("customerEditUpdate")]
        [HttpPost]
        public HttpResponseMessage customerEditUpdate(MdlFndMstCustomerMasterAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaFndMstCustomerMasterAdd.DacustomerEditUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCustomerDetailscount")]
        [HttpGet]
        public HttpResponseMessage GetCustomerDetailscount()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlFndMstCustomerMasterAdd values = new MdlFndMstCustomerMasterAdd();
            objDaFndMstCustomerMasterAdd.DaGetCustomerDetailscount(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("customersubmitapproval")]
        [HttpPost]
        public HttpResponseMessage customersubmitapproval(MdlFndMstCustomerMasterAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaFndMstCustomerMasterAdd.Dacustomersubmitapproval(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("approverEditUpdate")]
        [HttpPost]
        public HttpResponseMessage approverEditUpdate(MdlFndMstCustomerMasterAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaFndMstCustomerMasterAdd.DaapproverEditUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("rejectedEditUpdate")]
        [HttpPost]
        public HttpResponseMessage rejectedEditUpdate(MdlFndMstCustomerMasterAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaFndMstCustomerMasterAdd.DarejectedEditUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        //[ActionName("customerDetailsEdit")]
        //[HttpGet]
        //public HttpResponseMessage customerDetailsEdit(string customer_gid)
        //{
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = ObjGetGID.gettokenvalues(token);
        //    objDaFndMstCustomerMasterAdd.DacustomerDetailsEdit(getsessionvalues.customer_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        //[ActionName("DeleteGSTCustomer")]
        //[HttpGet]
        //public HttpResponseMessage DeleteGSTcustomer(string customer_gid)
        //{
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = ObjGetGID.gettokenvalues(token);
        //    MdlcustomerGST values = new MdlcustomerGST();
        //    objDaFndMstCustomerMasterAdd.DaDeleteGSTcustomer(getsessionvalues.employee_gid, customer_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        [ActionName("customerDetailsEdit")]
        [HttpGet]
        public HttpResponseMessage customerDetailsEdit(string customer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlFndMstCustomerMasterAdd values = new MdlFndMstCustomerMasterAdd();
            objDaFndMstCustomerMasterAdd.DacustomerDetailsEdit(customer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("PostChequeDetail")]
        [HttpPost]
        public HttpResponseMessage PostChequeDetail(MdlFndMstCustomerMasterAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaFndMstCustomerMasterAdd.DaPostChequeDetail(getsessionvalues.employee_gid,  values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetChequeSummary")]
        [HttpGet]
        public HttpResponseMessage GetChequeSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlCheque values = new MdlCheque();
            objDaFndMstCustomerMasterAdd.DaGetChequeSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        



        [ActionName("GetChequeSummaryView")]
        [HttpGet]
        public HttpResponseMessage GetChequeSummaryView(string customer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlFndMstCustomerMasterAdd values = new MdlFndMstCustomerMasterAdd();
            objDaFndMstCustomerMasterAdd.DaGetChequeSummaryView(customer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetChequeView")]
        [HttpGet]
        public HttpResponseMessage GetChequeView(string fndmanagement2cheque_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlFndMstCustomerMasterAdd values = new MdlFndMstCustomerMasterAdd();
            objDaFndMstCustomerMasterAdd.DaGetChequeView(fndmanagement2cheque_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



        [ActionName("ChequeDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage ChequeDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objDaFndMstCustomerMasterAdd.DaChequeDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetChequeDocumentList")]
        [HttpGet]
        public HttpResponseMessage GetChequeDocumentList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlChequeDocument values = new MdlChequeDocument();
            objDaFndMstCustomerMasterAdd.DaGetChequeDocumentList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ChequeDocumentDelete")]
        [HttpGet]
        public HttpResponseMessage ChequeDocumentDelete(string cheque2document_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlChequeDocument values = new MdlChequeDocument();
            objDaFndMstCustomerMasterAdd.DaChequeDocumentDelete(cheque2document_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //[ActionName("UpdateChequeDetail")]
        //[HttpPost]
        //public HttpResponseMessage UpdateChequeDetail(MdlCheque values)
        //{
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    objDaFndMstCustomerMasterAdd.DaUpdateChequeDetail(getsessionvalues.employee_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}


        //[ActionName("ChequeDetailsEdit")]
        //[HttpGet]
        //public HttpResponseMessage ChequeDetailsEdit(string udcmanagement2cheque_gid)
        //{
        //    MdlCheque values = new MdlCheque();
        //    objDaUdcManagement.DaChequeDetailsEdit(udcmanagement2cheque_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        //[ActionName("ChequeList")]
        //[HttpGet]
        //public HttpResponseMessage ChequeList(string udcmanagement_gid)
        //{
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    MdlCheque values = new MdlCheque();
        //    objDaFndMstCustomerMasterAdd.DaChequeList(getsessionvalues.employee_gid, udcmanagement_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        //[ActionName("ChequeDocumentList")]
        //[HttpGet]
        //public HttpResponseMessage ChequeDocumentList(string udcmanagement2cheque_gid)
        //{
        //    MdlChequeDocument values = new MdlChequeDocument();
        //    objDaFndMstCustomerMasterAdd.DaChequeDocumentList(udcmanagement2cheque_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        [ActionName("DeleteChequeDetail")]
        [HttpGet]
        public HttpResponseMessage DeleteChequeDetail(string fndmanagement2cheque_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlCheque values = new MdlCheque();
            objDaFndMstCustomerMasterAdd.DaDeleteChequeDetail(fndmanagement2cheque_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ChequeDetailsEdit")]
        [HttpGet]
        public HttpResponseMessage ChequeDetailsEdit(string customer_gid)
        {
            MdlFndMstCustomerMasterAdd values = new MdlFndMstCustomerMasterAdd();
            objDaFndMstCustomerMasterAdd.DaChequeDetailsEdit(customer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GSTEdit")]
        [HttpGet]
        public HttpResponseMessage GSTEdit(string customer2gst_gid)
        {
            MdlcustomerGST values = new MdlcustomerGST();
            objDaFndMstCustomerMasterAdd.DaGSTEdit(customer2gst_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Email Address Edit
        [ActionName("EmailAddressEdit")]
        [HttpGet]
        public HttpResponseMessage EmailAddressEdit(string customer2emailaddress_gid)
        {
            MdlEmail_address values = new MdlEmail_address();
            objDaFndMstCustomerMasterAdd.DaEmailAddressEdit(customer2emailaddress_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Email Address Update
        [ActionName("EmailAddressUpdate")]
        [HttpPost]
        public HttpResponseMessage EmailAddressUpdate(MdlEmail_address values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaFndMstCustomerMasterAdd.DaEmailAddressUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Mobile Number Edit
        [ActionName("MobileNoEdit")]
        [HttpGet]
        public HttpResponseMessage MobileNoEdit(string customer2mobileno_gid)
        {
            Mdlmobile_no values = new Mdlmobile_no();
            objDaFndMstCustomerMasterAdd.DaMobileNoEdit(customer2mobileno_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Address Details Edit
        [ActionName("AddressDetailEdit")]
        [HttpGet]
        public HttpResponseMessage AddressDetailEdit(string customer2address_gid)
        {
            MdlMstaddresstype values = new MdlMstaddresstype();
            objDaFndMstCustomerMasterAdd.DaAddressDetailEdit(customer2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Address Details Update
        [ActionName("AddressDetailUpdate")]
        [HttpPost]
        public HttpResponseMessage AddressDetailUpdate(MdlMstaddresstype values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaFndMstCustomerMasterAdd.DaAddressDetailUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        // Mobile Number Update
        [ActionName("MobileNoUpdate")]
        [HttpPost]
        public HttpResponseMessage MobileNoUpdate(Mdlmobile_no values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaFndMstCustomerMasterAdd.DaMobileNoUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GSTUpdate")]
        [HttpPost]
        public HttpResponseMessage GSTUpdate(MdlbuyerGST values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaFndMstCustomerMasterAdd.DaGSTUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetGSTState")]
        [HttpGet]
        public HttpResponseMessage GetGSTState(string gst_code)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlMstGST values = new MdlMstGST();
            objDaFndMstCustomerMasterAdd.DaGetGSTState(gst_code, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostCustomerRaiseQuery")]
        [HttpPost]
        public HttpResponseMessage PostCustomerRaiseQuery(MdlFndMstCustomerMasterAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaFndMstCustomerMasterAdd.DaPostCustomerRaiseQuery(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetCustomerRaiseQuery")]
        [HttpGet]
        public HttpResponseMessage GetCustomerRaiseQuery(string customer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlFndMstCustomerMasterAdd values = new MdlFndMstCustomerMasterAdd();
            objDaFndMstCustomerMasterAdd.DaGetCustomerRaiseQuery(customer_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostCustomerresponsequery")]
        [HttpPost]
        public HttpResponseMessage PostCustomerresponsequery(MdlFndMstCustomerMasterAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaFndMstCustomerMasterAdd.DaPostCustomerresponsequery(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}