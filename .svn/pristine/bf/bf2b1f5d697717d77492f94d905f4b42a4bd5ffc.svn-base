using ems.ep.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Web;

namespace ems.ep.DataAccess
{
    public class DaEpWelcome
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objodbcdatareader;
        DataTable dt_datatable;
        string msSQL;

        public bool DaGetExternalDetails(externaldetails values, string externaluser_gid)
        {
             
            msSQL = " select a.external_vendorcode,a.external_vendorname,a.photo_path from rsk_mst_texternalregistration a " +
                    " left join rsk_mst_texternaluser b on a.externalregister_gid = b.external_registerGid " +
                    " where b.external_usergid = '" + externaluser_gid + "'";
            objodbcdatareader = objdbconn.GetDataReader(msSQL);
            if (objodbcdatareader.HasRows == true)
            {
                values.externaluser_code = objodbcdatareader["external_vendorcode"].ToString();
                values.externaluser_name = objodbcdatareader["external_vendorname"].ToString();
                if(objodbcdatareader["photo_path"].ToString()!="")
                {
                    values.external_photo = HttpContext.Current.Server.MapPath("../../" + objodbcdatareader["photo_path"].ToString()) ;
                }
                else
                {
                    values.external_photo = "N";
                }
               
            }
            objodbcdatareader.Close();

            msSQL = " select a.allocation_assignedcount from " +
               " (select count(*) as allocation_assignedcount from rsk_trn_tallocationdtl where allocate_externalGid = '" + externaluser_gid + "') as a ";
            objodbcdatareader = objdbconn.GetDataReader(msSQL);
            if (objodbcdatareader.HasRows == true)
            {
                values.count_allocation = objodbcdatareader["allocation_assignedcount"].ToString();
            }

            objodbcdatareader.Close();
            return true;
        }


    }
}