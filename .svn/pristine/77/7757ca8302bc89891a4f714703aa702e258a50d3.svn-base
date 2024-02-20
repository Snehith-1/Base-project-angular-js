using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestSharp;
using Newtonsoft.Json;
using System.Configuration;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Data.Odbc;

namespace ems.reports.Controllers
{
    /// <summary>
    /// SamRptController Class containing API methods for accessing the related DataAccess class and returning relevant response to client. 
    /// </summary>
    /// <remarks>Written by Mani & sundar  </remarks>

    [RoutePrefix("api/SamRpt")]
    [AllowAnonymous]

    public class SamRptController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        string msSQL;
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        [ActionName("Report1")]
        [HttpGet]
        public HttpResponseMessage Report1(Guid report_id, Guid workspace_Id, String dataset, String roles)
        {
            string path = string.Empty;
            string employeemail = string.Empty;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid ='" + getsessionvalues.employee_gid + "'";
            employeemail = objdbconn.GetExecuteScalar(msSQL);
            var restClient = new RestClient(ConfigurationManager.AppSettings["powerbiurl"]);
            var restRequest = new RestRequest(Method.GET);
            restRequest.AddQueryParameter("report_id", report_id.ToString());
            restRequest.AddQueryParameter("workspace_Id", workspace_Id.ToString());
            restRequest.AddQueryParameter("dataset", dataset.ToString());
            restRequest.AddQueryParameter("roles", roles.ToString());
            restRequest.AddQueryParameter("employeemail", employeemail.ToString());
            IRestResponse restResponse = restClient.Execute(restRequest);
            path = restResponse.Content;
            return Request.CreateResponse(HttpStatusCode.OK, path);
        }

        [ActionName("PostAuditView")]
        [HttpGet]
        public HttpResponseMessage PostAuditView(string page_name, string page_head)
        {
            
            string user_gid = string.Empty;
            int mnResult;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            msSQL = "select user_gid from hrm_mst_temployee where employee_gid ='" + getsessionvalues.employee_gid + "'";
            user_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " INSERT INTO rpt_mst_tauditlog(" +
                       " page_name," +
                       " page_head," +
                       " action_taken," +
                       " created_by)" +
                       " VALUES(" +
                       "'" + page_name + "'," +
                       "'" + page_head + "'," +
                       "'View'," +
                       "'" + user_gid + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
