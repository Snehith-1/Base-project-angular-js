using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.ecms.Models;

using ems.ecms.DataAccess;
using ems.utilities.Models;
using ems.utilities.Functions;

namespace ems.ecms.Controllers
{
    /// <summary>
    /// customer Controller Class containing API methods for accessing the  DataAccess class DaCustomer 
    /// To get customer details, state name, ccmail details, customer details submit, UnTag NPA, Tag NPA, Tagged NPA Customer List,
    /// NPA Tagged History List, Export Customer details to excel, Common Customer detail 
    /// </summary>
    /// <remarks>Written by Sundar Rajan </remarks>
    [RoutePrefix("api/customer")]
    [Authorize]
    public class CustomerController : ApiController
    {
        session_values objsesseion = new session_values();
        logintoken objtoken = new logintoken();

        DaCustomer objDaCustomer = new DaCustomer();
        [ActionName("customer")]
        [HttpGet]
        public HttpResponseMessage Customer()
        {
            MdlCustomer objMdlCustomer = new MdlCustomer();
            objDaCustomer.DaGetCustomer(objMdlCustomer);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlCustomer);
        }

        [ActionName("customerdetail")]
        [HttpGet]
        public HttpResponseMessage Customerdetail()
        {
            MdlCustomer objMdlCustomer = new MdlCustomer();
            objDaCustomer.DaGetCustomerdetail(objMdlCustomer); 
            return Request.CreateResponse(HttpStatusCode.OK, objMdlCustomer);
        }
        [ActionName("state")]
        [HttpGet]
        public HttpResponseMessage State()
        {
            MdlCustomer objMdlState = new MdlCustomer();
            objDaCustomer.DaGetState(objMdlState); 
            return Request.CreateResponse(HttpStatusCode.OK, objMdlState);
        }

        [ActionName("cMmail")]
        [HttpGet]
        public HttpResponseMessage Ccmail()
        {
            MdlCustomer objMdlState = new MdlCustomer();
            objDaCustomer.DaGetccmail(objMdlState);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlState);
        }

        [ActionName("customerSubmit")]
        [HttpPost]
        public HttpResponseMessage CustomerSubmit(mdlcreatecustomer values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            objtoken = objsesseion.gettokenvalues(token);
            objDaCustomer.DaPostCreateCustomer(values, objtoken.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getcustomerupdatedetails")]
        [HttpGet]
        public HttpResponseMessage Getcustomerupdatedetails(string customer_gid)
        {
            customeredit values = new customeredit();
            objDaCustomer.DaGetEditCustomer(customer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("customerUpdate")]
        [HttpPost]
        public HttpResponseMessage customerUpdate(customeredit values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            objtoken = objsesseion.gettokenvalues(token);
            objDaCustomer .DaPostUpdateCustomer(objtoken.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("customerDelete")]
        [HttpGet]
        public HttpResponseMessage CustomerDelete(string customer_gid)
        {
            customeredit values = new customeredit();
            objDaCustomer.DaPostDeleteCustomer(customer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CustomerAlert")]
        [HttpGet]
        public HttpResponseMessage CustomerAlert()
        {
            MdlCustomer objMdlCustomer = new MdlCustomer();
           objDaCustomer.DaGetCustomerforAlert(objMdlCustomer);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlCustomer);
        }

        [ActionName("CustomerAlertSearch")]
        [HttpGet]
        public HttpResponseMessage CustomerAlertSearch(string customer_gid)
        {
            MdlCustomer objMdlCustomer = new MdlCustomer();
            objDaCustomer.DaGetCustomerforalertSearch(objMdlCustomer,customer_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlCustomer);
        }

        [ActionName("deferraldetails")]
        [HttpGet]
        public HttpResponseMessage Deferraldetails(string customer_gid)
        {
            MdlCustomer objMdldetails = new MdlCustomer();
           objDaCustomer.DaDeferralDetails(customer_gid, objMdldetails);
            return Request.CreateResponse(HttpStatusCode.OK, objMdldetails);
        }

        [ActionName("Getcustomerdetails")]
        [HttpGet]
        public HttpResponseMessage Getcustomerdetails(string customer_gid)
        {
            customeredit values = new customeredit();
            objDaCustomer.DaGetCustomerDetails(customer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getconstitution")]
        [HttpGet]
        public HttpResponseMessage Getconstitution()
        {
            MdlCustomer objconstitution = new MdlCustomer();
            objDaCustomer.DaGetconstitution(objconstitution);
            return Request.CreateResponse(HttpStatusCode.OK, objconstitution);
        }

        [ActionName("GetNewCustomerURN")]
        [HttpPost]
        public HttpResponseMessage GetNewCustomerURN(customerurndetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            objtoken = objsesseion.gettokenvalues(token);
            objDaCustomer.DaGetNewCustomerURN(values, objtoken.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ExploreCustomer")]
        [HttpGet]
        public HttpResponseMessage ExploreCustomer(string customername)
        {
            CustomersList values = new CustomersList();
            objDaCustomer.DaGetCustomers(values, customername);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("TagtoLegal")]
        [HttpPost]
        public HttpResponseMessage TagtoLegal(mdltagtolegal values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            objtoken = objsesseion.gettokenvalues(token);
            objDaCustomer.DaPostTagtoLegal(values, objtoken.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("TaggedCustomerList")]
        [HttpGet]
        public HttpResponseMessage TaggedCustomerList()
        {
            mdltagtolegal objMdlCustomer = new mdltagtolegal();
            objDaCustomer.DaGetTaggedCustomerList(objMdlCustomer);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlCustomer);
        }

        [ActionName("TaggedHistoryList")]
        [HttpGet]
        public HttpResponseMessage TaggedHistoeyList(string customer_gid)
        {
            mdltagtolegal objMdlCustomer = new mdltagtolegal();
            objDaCustomer.DaGetTaggedHistoeyList(objMdlCustomer, customer_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlCustomer);
        }

        [ActionName("UnTagtoLegal")]
        [HttpPost]
        public HttpResponseMessage UnTagtoLegal(mdluntagtolegal values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            objtoken = objsesseion.gettokenvalues(token);
            objDaCustomer.DaPostUnTagtoLegal(values, objtoken.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CommonCustomer")]
        [HttpGet]
        public HttpResponseMessage CommonCustomer(string customername)
        {
            CustomersList values = new CustomersList();
            objDaCustomer.DaGetCommonCustomers(values, customername);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ExportCustomer")]
        [HttpGet]
        public HttpResponseMessage GetCustomerData()
        {
            customerSummary objcustomersummary = new customerSummary();
            objDaCustomer.DaGetCustomerData(objcustomersummary);
            return Request.CreateResponse(HttpStatusCode.OK, objcustomersummary);
        }
        [ActionName("TagtoNPA")]
        [HttpPost]
        public HttpResponseMessage TagtoNPA(mdltagtonpa values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            objtoken = objsesseion.gettokenvalues(token);
            objDaCustomer.DaPostTagtoNPA(values, objtoken.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("TaggedNPACustomerList")]
        [HttpGet]
        public HttpResponseMessage TaggedNPACustomerList()
        {
            mdltagtonpa objMdlCustomer = new mdltagtonpa();
            objDaCustomer.DaGetTaggedNPACustomerList(objMdlCustomer);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlCustomer);
        }
        [ActionName("TaggedNPAHistoryList")]
        [HttpGet]
        public HttpResponseMessage TaggedNPAHistoryList(string customer_gid)
        {
            mdltagtonpa objMdlCustomer = new mdltagtonpa();
            objDaCustomer.DaGetTaggedNPAHistoryList(objMdlCustomer, customer_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlCustomer);
        }
        [ActionName("UnTagtoNPA")]
        [HttpPost]
        public HttpResponseMessage UnTagtoNPA(mdluntagtonpa values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            objtoken = objsesseion.gettokenvalues(token);
            objDaCustomer.DaPostUnTagtoNPA(values, objtoken.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
