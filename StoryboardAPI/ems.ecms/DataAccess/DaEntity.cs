using System.Web;
using System.Data;
using System.Data.Odbc;
using System.Net;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.VisualBasic;
using System.Security.Cryptography;
using ems.utilities.Functions;
using ems.ecms.Models;
using System.Collections.Generic;
using System;

namespace ems.ecms.DataAccess
{
    /// <summary>
    /// Entity Controller Class containing API methods for accessing the  DataAccess class DaEntity - to get entity name and entity gid from entity table
    /// </summary>
    /// <remarks>Written by Sundar Rajan </remarks>
    public class DaEntity
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
       
        DataTable dt_datatable;
        string msSQL;
      
        public void DaGetEntity(MdlEntity objEntity)
        {
            try
            {
                msSQL = " SELECT entity_gid,entity_name FROM adm_mst_tentity ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getEntity = new List<entity_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getEntity.Add(new entity_list
                        {
                            entity_gid = (dr_datarow["entity_gid"].ToString()),
                            entity_name = (dr_datarow["entity_name"].ToString()),
                        });
                    }
                    objEntity.entity_list = getEntity;
                }
                dt_datatable.Dispose();
                objEntity.status = true;

            }
            catch(Exception ex)
            {
                objEntity.status = false;
                objEntity.message = ex.Message.ToString();
            }
           
        }
    }
}