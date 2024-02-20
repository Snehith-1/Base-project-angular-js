using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using embedpbi.Models;
using embedpbi.Services;
using Microsoft.Rest;
using System.Configuration;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;

namespace embedpbi.Controllers
{
    [RoutePrefix("api/ReportPowerBI")]
    public class ReportPowerBIController : ApiController
    {
        private string m_errorMessage;
        [AllowAnonymous]
        [ActionName("EmbedToken")]
        [HttpGet]
        public async Task<HttpResponseMessage> EmbedToken(Guid report_id, Guid workspace_Id, string dataset, string roles, string employeemail)
        {
            try
            {
                m_errorMessage = ConfigValidatorService.GetWebConfigErrors();
                var result = new IndexConfig();
                var assembly = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Where(n => n.Name.Equals("Microsoft.PowerBI.Api")).FirstOrDefault();
                if (assembly != null)
                {
                    result.DotNetSDK = assembly.Version.ToString(3);
                }
                if (dataset == "Empty")
                {
                    dataset = null;
                    
                }
                
                    var embedResult1 = await EmbedService.GetEmbedParams(workspace_Id, report_id, dataset, roles, employeemail);
                    return Request.CreateResponse(HttpStatusCode.OK, embedResult1);
                

            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ex.ToString());
            }
            

        }

        }
    }
