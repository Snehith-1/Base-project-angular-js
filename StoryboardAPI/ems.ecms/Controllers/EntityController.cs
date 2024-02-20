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
    /// Entity Controller Class containing API methods for accessing the  DataAccess class DaEntity - to get entity name and entity gid from entity table
    /// </summary>
    /// <remarks>Written by Sundar Rajan </remarks>
    [RoutePrefix("api/entity")]
    [Authorize]
    public class EntityController : ApiController
    {
       
        [ActionName("Entity")]
        [HttpGet]
        public HttpResponseMessage getEntity()
        {
            DaEntity objDaEntity = new DaEntity();
            MdlEntity objEntity = new MdlEntity();
            objDaEntity.DaGetEntity(objEntity);
            return Request.CreateResponse(HttpStatusCode.OK, objEntity);
        }
    }
}
