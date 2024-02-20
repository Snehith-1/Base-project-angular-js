using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ems.utilities.Functions;
using ems.idas.Models;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Configuration;

namespace ems.idas.DataAccess
{
    public class DaIdasMstTemplate
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        HttpPostedFile httpPostedFile;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msGetGID;
        string msSQL;
        int mnResult;
        string lspath;
        public void DaGetTemplateContent(MdlIdasMstTemplate values)
        {
            try
            {
                msSQL = " select a.template_gid, a.template_name, b.templatetype_name, concat(b.user_code,' ',b.user_firstname,' ',b.user_lastname) as created_by " +
                    " left join adm_mst_ttemplatetype b on b.templatetype_gid = a.templatetype_gid " +
                   " where a.templatetype_gid='3'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.template_content = objODBCDatareader["template_content"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetTemplateSummary(MdlTemplateDtlsList values)
        {
            try
            {
                msSQL = " select a.template_gid as template_gid, a.template_name as template_name, c.templatetype_name  as templatetype_name, concat(b.user_code,' / ',b.user_firstname,' ',b.user_lastname) as created_by " +
                        " from adm_mst_ttemplate a " +
                        " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                        " left join adm_mst_ttemplatetype c on a.templatetype_gid = c.templatetype_gid order by a.template_gid desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getTemplateDtlsList = new List<MdlTemplateDtls>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getTemplateDtlsList.Add(new MdlTemplateDtls
                        {
                            template_gid = dt["template_gid"].ToString(), 
                            template_name = dt["template_name"].ToString(),
                            templatetype_name = dt["templatetype_name"].ToString(),
                            created_by = dt["created_by"].ToString(),
                        });
                    }
                    values.TemplateDtlsList = getTemplateDtlsList;
                    values.status = true;
                    values.message = "Data Fetched";
                }
                else
                {
                    values.status = false;
                    values.message = "No Record Found";
                }
            }
            catch
            {
                values.status = false;
            }
        }

        public bool DapostTemplateDtl(string user_gid, MdlTemplateDtls values)
        {
            try
            {
                msGetGID = objcmnfunctions.GetMasterGID("TMPL");

                msSQL = "select templatetype_gid from adm_mst_ttemplatetype where templatetype_name = '" + values.templatetype_name + "'";
                string templatetype_gid = objdbconn.GetExecuteScalar(msSQL);
                
                msSQL = "INSERT INTO adm_mst_ttemplate(" +
              " template_gid," +
              " template_name," +
              " templatetype_gid," +
              " template_content," +
              " created_by," +
              " created_on)" +
              " VALUES(" +
              "'" + msGetGID + "'," + 
              "'" + values.template_name + "'," +
              "'" + templatetype_gid + "'," +
              "'" + values.template_content.Replace("'","") + "'," +
              "'" + user_gid + "'," +
              "current_timestamp)";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if(mnResult == 1)
                {
                    values.message = "Template Details Added Successfully..!";
                    values.status = true;
                    return true;
                }
                else
                {
                    values.message = "Error Occured";
                    values.status = false;
                    return false;
                }
            }
            catch
            {
                values.status = false;
                return false;
            }
           
        }

        public void DaGetTemplateDtl(string template_gid, MdlTemplateDtls values)
        {
            try
            {
                msSQL = " select a.template_name as template_name, a.templatetype_gid as templatetype_gid, b.templatetype_name  as templatetype_name, a.template_content as template_content " +
                      " from adm_mst_ttemplate a " +
                      " left join adm_mst_ttemplatetype b on a.templatetype_gid = b.templatetype_gid " +
                      " where a.template_gid = '" + template_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.template_name = objODBCDatareader["template_name"].ToString();
                    values.templatetype_gid = objODBCDatareader["templatetype_gid"].ToString();
                    values.templatetype_name = objODBCDatareader["templatetype_name"].ToString();
                    values.template_content = objODBCDatareader["template_content"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetTemplateType(MdlTemplateDtls objtemplatetype)
        {
            try
            {
                msSQL = " SELECT templatetype_gid,templatetype_name FROM adm_mst_ttemplatetype ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gettemplatetype_list = new List<templatetype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        gettemplatetype_list.Add(new templatetype_list
                        {
                            templatetype_gid = (dr_datarow["templatetype_gid"].ToString()),
                            templatetype_name = (dr_datarow["templatetype_name"].ToString()),
                        });
                    }
                    objtemplatetype.templatetype_list = gettemplatetype_list;
                }
                dt_datatable.Dispose();

                objtemplatetype.status = true;
            }
            catch
            {
                objtemplatetype.status = false;
            }

        }

        public bool DaUpdateTemplateDtl(string user_gid, MdlTemplateDtls values)
        {
            try
            {
                msGetGID = objcmnfunctions.GetMasterGID("TMPL");

                msSQL = "select templatetype_gid from adm_mst_ttemplatetype where templatetype_name = '" + values.templatetype_name + "'";
                string templatetype_gid = objdbconn.GetExecuteScalar(msSQL);


                msSQL = " update adm_mst_ttemplate set " +
                        " template_name='" + values.template_name + "'," +
                        " templatetype_gid='" + templatetype_gid + "'," +
                        " template_content='" + values.template_content.Replace("'", "") + "'," +
                        " updated_by='" + user_gid + "'," +
                        " updated_date=current_timestamp" +
                        " where template_gid='" + values.template_gid + "' ";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                if (mnResult == 1)
                {
                    values.message = "Template Details Updated Successfully..!";
                    values.status = true;
                }
                else
                {
                    values.message = "Error Occured";
                    values.status = false;
                }

            }
            catch
            {
                values.status = false;
            }
            return true;
        }

     


    }
}