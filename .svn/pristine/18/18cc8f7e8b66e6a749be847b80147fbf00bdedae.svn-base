using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ems.utilities.Functions;
using ems.ecms.Models;
using System.Data;
using System.Data.Odbc;

namespace ems.ecms.DataAccess
{
    /// <summary>
    /// template Controller Class containing API methods for accessing the  DataAccess class DaTemplate 
    ///  Teplate - Get template from DB, Get content from DB
    /// </summary>
    /// <remarks>Written by Sundar Rajan </remarks>
    public class DaTemplate
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
      
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL;

        public void DaGetTemplate(Mdltemplate objTemplate)
        {
            try
            {
                msSQL = " select a.template_gid, a.template_name, a.template_content from adm_mst_ttemplate a " +
                   " left join adm_mst_ttemplatetype b on b.templatetype_gid = a.templatetype_gid " +
                   " where a.templatetype_gid='2' ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getTemplate = new List<template_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getTemplate.Add(new template_list
                        {
                            template_gid = (dr_datarow["template_gid"].ToString()),
                            template_name = (dr_datarow["template_name"].ToString()),
                        });
                    }
                    objTemplate.template_list = getTemplate;
                }
                dt_datatable.Dispose();
                objTemplate.status = true;
            }
            catch (Exception ex)
            {
                objTemplate.status = false;
            }
           
        }
        public void DaGetTemplateContent(template_list values)
        {
            try
            {
                msSQL = " select a.template_gid, a.template_name, a.template_content from adm_mst_ttemplate a " +
                   " left join adm_mst_ttemplatetype b on b.templatetype_gid = a.templatetype_gid " +
                   " where a.templatetype_gid='2'";

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
    }
}