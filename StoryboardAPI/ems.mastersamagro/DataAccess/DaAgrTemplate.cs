using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ems.utilities.Functions;
using ems.mastersamagro.Models;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Configuration;

namespace ems.mastersamagro.DataAccess
{
    /// <summary>
    /// This DataAccess provide access to template and editor doc for contract flow in samagro
    /// </summary>
    /// <remarks>Written by Venkatesh </remarks>
    public class DaAgrTemplate
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

        public void DaGetTemplateSummary(MdlTemplateDtlsList values, string vertical_gid, string template_type)
        {
            try
            {
                msSQL = " select a.template_gid as template_gid, a.template_name as template_name,program_name, " +
                        " concat(b.user_firstname,' ',b.user_lastname,' / ', b.user_code) as created_by " +
                        " from ocs_mst_ttemplate a " +
                        " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                        " where vertical_gid='" + vertical_gid + "' and template_type='" + template_type + "'" +
                        " order by a.template_gid desc";

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
                            program_name = dt["program_name"].ToString(),
                            created_by = dt["created_by"].ToString(),
                        });
                    }
                    values.TemplateDtlsList = getTemplateDtlsList;
                    values.status = true;
                }
                else
                {
                    values.status = false;
                }
                dt_datatable.Dispose();


                msSQL = " select concat(vertical_name,' / ',vertical_code) as vertical_name from ocs_mst_tvertical " +
                        " where vertical_gid = '" + vertical_gid + "'";
                values.vertical_name = objdbconn.GetExecuteScalar(msSQL);
            }
            catch
            {
                values.status = false;
                dt_datatable.Dispose();
            }
        }

        public void DaPostTemplateDtl(string user_gid, MdlTemplateDtls values)
        {
            try
            {
                foreach (var z in values.template_programlist)
                {
                    msGetGID = objcmnfunctions.GetMasterGID("OCTE");

                    msSQL = "INSERT INTO ocs_mst_ttemplate(" +
                            " template_gid," +
                            " template_name," +
                            " template_content, " +
                            " vertical_gid, " +
                            " vertical_name, " +
                            " program_gid, " +
                            " program_name, " +
                            " template_type, " +
                            " created_by," +
                            " created_date)" +
                            " VALUES(" +
                            "'" + msGetGID + "'," +
                            "'" + values.template_name.Replace("'", "\\'") + "'," +
                            "'" + values.template_content.Replace("'", "\\'") + "'," +
                            "'" + values.vertical_gid + "'," +
                            "'" + values.vertical_name + "'," +
                            "'" + z.program_gid + "'," +
                            "'" + z.program_name + "'," +
                            "'" + values.template_type + "'," +
                            "'" + user_gid + "'," +
                            "current_timestamp)";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    foreach (var i in values.templateinputtype_list)
                    {
                        string msGetInputGID = objcmnfunctions.GetMasterGID("TING");
                        if (i.input_placeholder == null)
                            i.input_placeholder = "";
                        string lsdbfield = "N";
                        if (i.DBfield_type != "" && i.DBfield_type != null)
                            lsdbfield = "Y";
                        msSQL = "INSERT INTO ocs_mst_ttemplateinputdtl(" +
                                " templateinputdtl_gid," +
                                " template_gid," +
                                " input_fieldid, " +
                                " input_fieldname, " +
                                " inputgroup_gid, " +
                                " inputgroup_name, " +
                                " input_type, " +
                                " input_placeholder, " +
                                " inputmax_length, " +
                                " input_mandatory, " +
                                " input_previewtext, " +
                                " vertical_gid, " +
                                " program_gid, " +
                                " dbfield_type," +
                                " created_by," +
                                " created_date)" +
                                " VALUES(" +
                                "'" + msGetInputGID + "'," +
                                "'" + msGetGID + "'," +
                                "'" + i.input_fieldid + "'," +
                                "'" + i.input_fieldname.Replace("'", "\\'") + "'," +
                                "'" + i.inputgroup_gid + "'," +
                                "'" + i.inputgroup_name.Replace("'", "\\'") + "'," +
                                "'" + i.input_type + "'," +
                                "'" + i.input_placeholder.Replace("'", "\\'") + "'," +
                                "'" + i.inputmax_length + "'," +
                                "'" + i.input_mandatory + "'," +
                                "'" + i.input_previewtext.Replace("'", "\\'") + "'," +
                                "'" + values.vertical_gid + "'," +
                                "'" + z.program_gid + "'," +
                                "'" + lsdbfield + "'," +
                                "'" + user_gid + "'," +
                                "current_timestamp)";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (lsdbfield == "Y")
                        {
                            string msdbinputgid = objcmnfunctions.GetMasterGID("TDID");

                            msSQL = " insert into ocs_mst_ttemplatedbinputdtl (templatedbinputdtl_gid,fieldmapping_gid, template_gid, " +
                                    " templateinputdtl_gid, " +
                                    " input_fieldid, input_type, stage_fieldid, stage_fieldname, stage_fieldvalue, menu_fieldname, menu_fieldvalue," +
                                    " menu_tablevalue, fieldname, field_value,field_tablevalue, field_columnvalue, subfield_flag, subfield_fieldname,subfield_fieldvalue," +
                                    " subfield_tablevalue, subfield_columnvalue,subfield_joincolumn,subfield_joincolumn1,subfield_joincolumn2, created_by , created_date) " +
                                    " (select '" + msdbinputgid + "',fieldmapping_gid,'" + msGetGID + "','" + msGetInputGID + "', " +
                                    " '" + i.input_fieldid + "',input_type, stage_fieldid,stage_fieldname,stage_fieldvalue, " +
                                    " menu_fieldname,menu_fieldvalue, " +
                                    " menu_tablevalue, fieldname,field_value,field_tablevalue,field_columnvalue,subfield_flag,subfield_fieldname, " +
                                    " subfield_fieldvalue, subfield_tablevalue,subfield_columnvalue,subfield_joincolumn,subfield_joincolumn1,subfield_joincolumn2,'" + user_gid + "', current_timestamp " +
                                    " from agr_mst_tfieldmapping where fieldmapping_gid = '" + i.DBfield_type + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        if (i.inputlistarray != null)
                        {
                            foreach (var j in i.inputlistarray)
                            {
                                msSQL = "INSERT INTO ocs_mst_ttemplateinputlistdtl(" +
                                    " templateinputdtl_gid," +
                                    " template_gid," +
                                    " input_fieldid, " +
                                    " input_type, " +
                                    " input_previewtext, " +
                                    " input_value, " +
                                    " vertical_gid, " +
                                    " program_gid, " +
                                    " created_by," +
                                    " created_date)" +
                                    " VALUES(" +
                                    "'" + msGetInputGID + "'," +
                                    "'" + values.template_gid + "'," +
                                    "'" + i.input_fieldid + "'," +
                                    "'" + i.input_type + "'," +
                                    "'" + j.preview_text.Replace("'", "\\'") + "'," +
                                    "'" + j.Value.Replace("'", "\\'") + "'," +
                                    "'" + values.vertical_gid + "'," +
                                    "'" + z.program_gid + "'," +
                                    "'" + user_gid + "'," +
                                    "current_timestamp)";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                        }
                    }
                }

                if (mnResult == 1)
                {
                    values.message = "Template Details Added Successfully..!";
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

        }

        public void DaGetTemplateDtl(string template_gid, MdlTemplateDtls values)
        {
            try
            {
                msSQL = " select a.template_name as template_name,program_name,  " +
                      " a.template_content as template_content, " +
                      " concat(a.vertical_name, ' / ', b.vertical_code) as vertical_name " +
                      " from ocs_mst_ttemplate a " +
                      " left join ocs_mst_tvertical b on a.vertical_gid = b.vertical_gid " +
                      " where a.template_gid = '" + template_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.template_name = objODBCDatareader["template_name"].ToString();
                    values.template_content = objODBCDatareader["template_content"].ToString();
                    values.vertical_name = objODBCDatareader["vertical_name"].ToString();
                    values.program_name = objODBCDatareader["program_name"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select a.templateinputdtl_gid, inputgroup_name, a.input_type, input_placeholder, " +
                       " inputmax_length,input_mandatory,fieldmapping_gid, " +
                       " a.input_fieldid, input_fieldname,inputgroup_gid,input_previewtext " +
                       " from ocs_mst_ttemplateinputdtl a " +
                       " left join ocs_mst_ttemplatedbinputdtl b on a.templateinputdtl_gid=b.templateinputdtl_gid " +
                       " where a.template_gid = '" + template_gid + "'" +
                       " order by a.inputgroup_name asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getTemplateDtlsList = new List<templateinputtype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getTemplateDtlsList.Add(new templateinputtype_list
                        {
                            templateinputdtl_gid = dt["templateinputdtl_gid"].ToString(),
                            inputgroup_name = dt["inputgroup_name"].ToString(),
                            input_type = dt["input_type"].ToString(),
                            input_placeholder = dt["input_placeholder"].ToString(),
                            inputmax_length = dt["inputmax_length"].ToString(),
                            input_mandatory = dt["input_mandatory"].ToString(),
                            input_fieldid = dt["input_fieldid"].ToString(),
                            input_fieldname = dt["input_fieldname"].ToString(),
                            inputgroup_gid = dt["inputgroup_gid"].ToString(),
                            input_previewtext = dt["input_previewtext"].ToString(),
                            fieldmapping_gid = dt["fieldmapping_gid"].ToString(),
                        });
                    }
                    values.templateinputtype_list = getTemplateDtlsList;
                }
                dt_datatable.Dispose();
                msSQL = " select templateinputdtl_gid, input_previewtext,input_value from ocs_mst_ttemplateinputlistdtl " +
                        " where template_gid = '" + template_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var objinputarraylist = new List<inputlistarray>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        objinputarraylist.Add(new inputlistarray
                        {
                            templateinputdtl_gid = dt["templateinputdtl_gid"].ToString(),
                            preview_text = dt["input_previewtext"].ToString(),
                            Value = dt["input_value"].ToString(),
                        });
                    }
                }
                dt_datatable.Dispose();
                if (values.templateinputtype_list != null)
                {
                    int j = 0;
                    foreach (var i in values.templateinputtype_list)
                    {
                        var getinputarraylist = objinputarraylist.Where(a => a.templateinputdtl_gid == i.templateinputdtl_gid).ToList();
                        if (getinputarraylist.Count != 0)
                        {
                            values.templateinputtype_list[j].inputlistarray = getinputarraylist;
                        }
                        j++;
                    }
                }
                values.status = true;
            }
            catch
            {
                dt_datatable.Dispose();
                values.status = false;
            }
        }

        public void DaUpdateTemplateDtl(string user_gid, MdlTemplateDtls values)
        {
            try
            {
                msSQL = " select vertical_gid, program_gid from ocs_mst_ttemplate " +
                        " where template_gid='" + values.template_gid + "' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.vertical_gid = objODBCDatareader["vertical_gid"].ToString();
                    values.program_gid = objODBCDatareader["program_gid"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " update ocs_mst_ttemplate set " +
                        " template_name='" + values.template_name.Replace("'", "\\'") + "'," +
                        " template_content='" + values.template_content.Replace("'", "\\'") + "'," +
                        " updated_by='" + user_gid + "'," +
                        " updated_date=current_timestamp" +
                        " where template_gid='" + values.template_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    msSQL = "delete from ocs_mst_ttemplateinputdtl where template_gid='" + values.template_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "delete from ocs_mst_ttemplateinputlistdtl where template_gid='" + values.template_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "delete from ocs_mst_ttemplatedbinputdtl where template_gid='" + values.template_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                foreach (var i in values.templateinputtype_list)
                {
                    string msGetInputGID = objcmnfunctions.GetMasterGID("TING");
                    if (i.input_placeholder == null)
                        i.input_placeholder = "";
                    string lsdbfield = "N";
                    if (i.DBfield_type != null && i.DBfield_type != "")
                        lsdbfield = "Y";
                    msSQL = "INSERT INTO ocs_mst_ttemplateinputdtl(" +
                          " templateinputdtl_gid," +
                          " template_gid," +
                          " input_fieldid, " +
                          " input_fieldname, " +
                          " inputgroup_gid, " +
                          " inputgroup_name, " +
                          " input_type, " +
                          " input_placeholder, " +
                          " inputmax_length, " +
                          " input_mandatory, " +
                          " input_previewtext, " +
                          " vertical_gid, " +
                          " program_gid, " +
                          " dbfield_type," +
                          " created_by," +
                          " created_date)" +
                          " VALUES(" +
                          "'" + msGetInputGID + "'," +
                          "'" + values.template_gid + "'," +
                          "'" + i.input_fieldid + "'," +
                          "'" + i.input_fieldname.Replace("'", "\\'") + "'," +
                          "'" + i.inputgroup_gid + "'," +
                          "'" + i.inputgroup_name.Replace("'", "\\'") + "'," +
                          "'" + i.input_type + "'," +
                          "'" + i.input_placeholder.Replace("'", "\\'") + "'," +
                          "'" + i.inputmax_length + "'," +
                          "'" + i.input_mandatory + "'," +
                          "'" + i.input_previewtext.Replace("'", "\\'") + "'," +
                          "'" + values.vertical_gid + "'," +
                          "'" + values.program_gid + "'," +
                          "'" + lsdbfield + "'," +
                          "'" + user_gid + "'," +
                          "current_timestamp)";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (lsdbfield == "Y")
                    {
                        string msdbinputgid = objcmnfunctions.GetMasterGID("TDID");

                        msSQL = " insert into ocs_mst_ttemplatedbinputdtl (templatedbinputdtl_gid,fieldmapping_gid, template_gid, " +
                                " templateinputdtl_gid, " +
                                " input_fieldid, input_type, stage_fieldid, stage_fieldname, stage_fieldvalue, menu_fieldname, menu_fieldvalue," +
                                " menu_tablevalue, fieldname, field_value,field_tablevalue, field_columnvalue, subfield_flag, subfield_fieldname,subfield_fieldvalue," +
                                " subfield_tablevalue, subfield_columnvalue,subfield_joincolumn,subfield_joincolumn1,subfield_joincolumn2, created_by , created_date) " +
                                " (select '" + msdbinputgid + "',fieldmapping_gid,'" + values.template_gid + "','" + msGetInputGID + "', " +
                                " '" + i.input_fieldid + "',input_type, stage_fieldid,stage_fieldname,stage_fieldvalue, " +
                                " menu_fieldname,menu_fieldvalue, " +
                                " menu_tablevalue, fieldname,field_value,field_tablevalue,field_columnvalue,subfield_flag,subfield_fieldname, " +
                                " subfield_fieldvalue, subfield_tablevalue,subfield_columnvalue,subfield_joincolumn,subfield_joincolumn1,subfield_joincolumn2,'" + user_gid + "', current_timestamp " +
                                " from agr_mst_tfieldmapping where fieldmapping_gid = '" + i.DBfield_type + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                    if (i.inputlistarray != null)
                    {
                        foreach (var j in i.inputlistarray)
                        {
                            msSQL = "INSERT INTO ocs_mst_ttemplateinputlistdtl(" +
                                " templateinputdtl_gid," +
                                " template_gid," +
                                " input_fieldid, " +
                                " input_type, " +
                                " input_previewtext, " +
                                " input_value, " +
                                " vertical_gid, " +
                                " program_gid, " +
                                " created_by," +
                                " created_date)" +
                                " VALUES(" +
                                "'" + msGetInputGID + "'," +
                                "'" + values.template_gid + "'," +
                                "'" + i.input_fieldid + "'," +
                                "'" + i.input_type + "'," +
                                "'" + j.preview_text.Replace("'", "\\'") + "'," +
                                "'" + j.Value.Replace("'", "\\'") + "'," +
                                "'" + values.vertical_gid + "'," +
                                "'" + values.program_gid + "'," +
                                "'" + user_gid + "'," +
                                "current_timestamp)";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                }

                if (mnResult == 1)
                {
                    values.message = "Template Details are Updated Successfully..!";
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
        }

        public void DaGetInputDropdown(string template_gid, string input_type, MdlInputTypeList values)
        {
            try
            {
                msSQL = " select templateinputdtl_gid, inputgroup_name, input_type, input_placeholder, inputmax_length,input_mandatory, " +
                        " input_fieldid, input_fieldname,inputgroup_gid,input_previewtext " +
                        " from ocs_mst_ttemplateinputdtl where template_gid = '" + template_gid + "'" +
                        " and input_type = '" + input_type + "' and dbfield_type='N' order by inputgroup_name asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getTemplateDtlsList = new List<MdlInputDtls>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getTemplateDtlsList.Add(new MdlInputDtls
                        {
                            templateinputdtl_gid = dt["templateinputdtl_gid"].ToString(),
                            inputgroup_name = dt["inputgroup_name"].ToString(),
                            input_type = dt["input_type"].ToString(),
                            input_placeholder = dt["input_placeholder"].ToString(),
                            inputmax_length = dt["inputmax_length"].ToString(),
                            input_mandatory = dt["input_mandatory"].ToString(),
                            input_fieldid = dt["input_fieldid"].ToString(),
                            input_fieldname = dt["input_fieldname"].ToString(),
                            inputgroup_gid = dt["inputgroup_gid"].ToString(),
                            input_previewtext = dt["input_previewtext"].ToString(),
                        });
                    }
                    values.MdlInputDtls = getTemplateDtlsList;

                    msSQL = " select templateinputdtl_gid, input_previewtext,input_value from ocs_mst_ttemplateinputlistdtl " +
                            " where template_gid = '" + template_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var objinputarraylist = new List<inputlistarray>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            objinputarraylist.Add(new inputlistarray
                            {
                                templateinputdtl_gid = dt["templateinputdtl_gid"].ToString(),
                                preview_text = dt["input_previewtext"].ToString(),
                                Value = dt["input_value"].ToString(),
                            });
                        }
                    }
                    dt_datatable.Dispose();
                    if (values.MdlInputDtls != null)
                    {
                        int j = 0;
                        foreach (var i in values.MdlInputDtls)
                        {
                            var getinputarraylist = objinputarraylist.Where(a => a.templateinputdtl_gid == i.templateinputdtl_gid).ToList();
                            if (getinputarraylist.Count != 0)
                            {
                                values.MdlInputDtls[j].inputlistarray = getinputarraylist;
                            }
                            j++;
                        }
                    }

                    values.status = true;
                }
                else
                {
                    values.status = false;
                }
                dt_datatable.Dispose();
            }
            catch
            {
                values.status = false;
                dt_datatable.Dispose();
            }
        }

        public void DaGetTrnInputList(string template_gid, string templatetype_gid, MdlInputTypeList values)
        {
            try
            {
                msSQL = " select trntemplateinputdtl_gid, templateinputdtl_gid, inputgroup_name,  " +
                        " input_type, input_placeholder, inputmax_length,input_mandatory, " +
                        " case when input_givendatagid is not null then input_givendatagid " +
                        " else input_givendata end as input_givendata,input_givendata as input_givenvalue, " +
                        " input_fieldid, input_fieldname,inputgroup_gid,input_previewtext " +
                        " from ocs_trn_ttemplateinputdtl where template_gid = '" + template_gid + "'" +
                        " and templatetype_gid = '" + templatetype_gid + "' and inputgroup_gid='0'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getTemplateDtlsList = new List<MdlInputDtls>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getTemplateDtlsList.Add(new MdlInputDtls
                        {
                            templateinputdtl_gid = dt["trntemplateinputdtl_gid"].ToString(),
                            inputgroup_name = dt["inputgroup_name"].ToString(),
                            input_type = dt["input_type"].ToString(),
                            input_placeholder = dt["input_placeholder"].ToString(),
                            inputmax_length = dt["inputmax_length"].ToString(),
                            input_mandatory = dt["input_mandatory"].ToString(),
                            input_fieldid = dt["input_fieldid"].ToString(),
                            input_fieldname = dt["input_fieldname"].ToString(),
                            inputgroup_gid = dt["inputgroup_gid"].ToString(),
                            input_givendata = dt["input_givendata"].ToString(),
                            input_givenvalue = dt["input_givenvalue"].ToString(),
                            input_previewtext = dt["input_previewtext"].ToString(),
                        });
                    }
                    dt_datatable.Dispose();
                    values.MdlInputDtls = getTemplateDtlsList;

                    msSQL = " select trntemplateinputlistdtl_gid, trntemplateinputdtl_gid, input_previewtext,input_value from ocs_trn_ttemplateinputlistdtl " +
                            " where templatetype_gid = '" + templatetype_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var objinputarraylist = new List<MdlInputlistDtls>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            objinputarraylist.Add(new MdlInputlistDtls
                            {
                                templateinputdtl_gid = dt["trntemplateinputdtl_gid"].ToString(),
                                trntemplateinputlistdtl_gid = dt["trntemplateinputlistdtl_gid"].ToString(),
                                preview_text = dt["input_previewtext"].ToString(),
                                Value = dt["input_value"].ToString(),
                            });
                        }
                    }
                    dt_datatable.Dispose();
                    int j = 0;
                    foreach (var i in values.MdlInputDtls)
                    {
                        var getinputarraylist = objinputarraylist.Where(a => a.templateinputdtl_gid == i.templateinputdtl_gid).ToList();
                        if (getinputarraylist.Count != 0)
                        {
                            values.MdlInputDtls[j].MdlInputlistDtls = getinputarraylist;
                        }
                        j++;
                    }

                    values.status = true;
                }
                else
                {
                    values.status = false;
                }
            }
            catch
            {
                values.status = false;
                dt_datatable.Dispose();
            }
        }

        public void DaPostTrnInputList(MdlInputTypeList values, string employee_gid)
        {
            try
            {
                foreach (var i in values.MdlInputDtls)
                {
                    string input_givendatavalue = "";
                    msSQL = " select GROUP_CONCAT('\\\'',trntemplateinputdtl_gid,'\\\'')  from  ocs_trn_ttemplateinputdtl " +
                            " where  inputgroup_gid in (select input_fieldid from ocs_trn_ttemplateinputdtl " +
                            " where trntemplateinputdtl_gid='" + i.templateinputdtl_gid + "')";
                    string lstrntemplateinputdtl_gid = objdbconn.GetExecuteScalar(msSQL);
                    if (lstrntemplateinputdtl_gid == "")
                        lstrntemplateinputdtl_gid = "''";
                    if (i.input_type == "select-one" || i.input_type == "radio")
                    {
                        if (i.DBInput == false)
                        {
                            msSQL = " select input_value from ocs_trn_ttemplateinputlistdtl " +
                                " where trntemplateinputlistdtl_gid = '" + i.input_givendata + "' and input_type = '" + i.input_type + "'";
                            input_givendatavalue = objdbconn.GetExecuteScalar(msSQL);
                        }
                        else
                        {
                            input_givendatavalue = i.input_givendata;
                            i.input_givendata = "";
                        }

                        msSQL = " update ocs_trn_ttemplateinputdtl set input_givendata = '" + input_givendatavalue + "', " +
                          " input_givendatagid ='" + i.input_givendata + "'," +
                          " updated_by='" + employee_gid + "'," +
                          " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                          " where trntemplateinputdtl_gid in ('" + i.templateinputdtl_gid + "'," + lstrntemplateinputdtl_gid + ")";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else
                    {
                        msSQL = " update ocs_trn_ttemplateinputdtl set input_givendata = '" + i.input_givendata + "', " +
                           " updated_by='" + employee_gid + "'," +
                           " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                           " where trntemplateinputdtl_gid in ('" + i.templateinputdtl_gid + "'," + lstrntemplateinputdtl_gid + ")";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                msSQL = " select trntemplateinputdtl_gid, templateinputdtl_gid, inputgroup_name,  " +
                        " input_type, input_placeholder, inputmax_length,input_mandatory, " +
                        " case when input_givendatagid is not null then input_givendatagid " +
                        " else input_givendata end as input_givendata,input_givendata as input_givenvalue, " +
                        " input_fieldid, input_fieldname,inputgroup_gid " +
                        " from ocs_trn_ttemplateinputdtl where template_gid = '" + values.template_gid + "'" +
                        " and templatetype_gid = '" + values.templatetype_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getTemplateDtlsList = new List<MdlInputDtls>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getTemplateDtlsList.Add(new MdlInputDtls
                        {
                            templateinputdtl_gid = dt["trntemplateinputdtl_gid"].ToString(),
                            inputgroup_name = dt["inputgroup_name"].ToString(),
                            input_type = dt["input_type"].ToString(),
                            input_placeholder = dt["input_placeholder"].ToString(),
                            inputmax_length = dt["inputmax_length"].ToString(),
                            input_mandatory = dt["input_mandatory"].ToString(),
                            input_fieldid = dt["input_fieldid"].ToString(),
                            input_fieldname = dt["input_fieldname"].ToString(),
                            inputgroup_gid = dt["inputgroup_gid"].ToString(),
                            input_givendata = dt["input_givendata"].ToString(),
                            input_givenvalue = dt["input_givenvalue"].ToString(),
                        });
                    }
                    values.MdlInputDtls = getTemplateDtlsList;
                    dt_datatable.Dispose();
                    msSQL = " select trntemplateinputlistdtl_gid, trntemplateinputdtl_gid, input_previewtext, " +
                           " input_value from ocs_trn_ttemplateinputlistdtl " +
                           " where templatetype_gid = '" + values.templatetype_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var objinputarraylist = new List<MdlInputlistDtls>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            objinputarraylist.Add(new MdlInputlistDtls
                            {
                                trntemplateinputlistdtl_gid = dt["trntemplateinputlistdtl_gid"].ToString(),
                                templateinputdtl_gid = dt["trntemplateinputdtl_gid"].ToString(),
                                preview_text = dt["input_previewtext"].ToString(),
                                Value = dt["input_value"].ToString(),
                            });
                        }
                    }
                    dt_datatable.Dispose();
                    int j = 0;
                    foreach (var i in values.MdlInputDtls)
                    {
                        var getinputarraylist = objinputarraylist.Where(a => a.templateinputdtl_gid == i.templateinputdtl_gid).ToList();
                        if (getinputarraylist.Count != 0)
                        {
                            values.MdlInputDtls[j].MdlInputlistDtls = getinputarraylist;
                        }
                        j++;
                    }
                    values.status = true;
                }
                else
                {
                    values.status = false;
                }


                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Input Details are updated successfully..!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.ToString();
            }
        }

        public void DaGetVerticalProgramTemplate(string application_gid, MdlTemplatelist1 values)
        {
            try
            {
                msSQL = " select template_gid,template_name,template_content from ocs_mst_ttemplate a " +
                        " left join ocs_mst_tapplication b on a.vertical_gid = b.vertical_gid and a.program_gid = b.program_gid " +
                        " where b.application_gid = '" + application_gid + "' and a.template_status='Y'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var objMdlTemplatedtl1 = new List<MdlTemplatedtl1>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        objMdlTemplatedtl1.Add(new MdlTemplatedtl1
                        {
                            template_gid = dt["template_gid"].ToString(),
                            template_name = dt["template_name"].ToString(),
                            template_content = dt["template_content"].ToString(),
                        });
                    }
                }
                values.MdlTemplatedtl1 = objMdlTemplatedtl1;
                dt_datatable.Dispose();
            }
            catch
            {
                dt_datatable.Dispose();
            }
        }

        public void DaPostTrnTemplateConfirm(MdlTemplateGeneratedtl values, string user_gid)
        {
            string lstemplate_gid = "", lstemplate_name = "", lstemplate_content = "", lstemplate_type = "";
            msSQL = " select template_gid,template_name,template_content,template_type from ocs_mst_ttemplate " +
                    " where template_gid ='" + values.template_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lstemplate_gid = objODBCDatareader["template_gid"].ToString();
                lstemplate_name = objODBCDatareader["template_name"].ToString();
                lstemplate_content = objODBCDatareader["template_content"].ToString();
                lstemplate_type = objODBCDatareader["template_type"].ToString();
            }
            objODBCDatareader.Close();

            string msGetTempGid = objcmnfunctions.GetMasterGID("DYTG");
            msSQL = " INSERT INTO ocs_trn_tdynamictemplatedtl(" +
                                " dynamictemplatedtl_gid," +
                                " templatetype_gid," +
                                " templatetype_name, " +
                                " template_gid, " +
                                " template_name, " +
                                " template_content, " +
                                " created_by," +
                                " created_date)" +
                                " VALUES(" +
                                "'" + msGetTempGid + "'," +
                                "'" + values.templatetype_gid + "'," +
                                "'" + lstemplate_type + "'," +
                                "'" + lstemplate_gid + "'," +
                                "'" + lstemplate_name.Replace("'", "\\'") + "'," +
                                "'" + lstemplate_content.Replace("'", "\\'") + "'," +
                                "'" + user_gid + "'," +
                                "current_timestamp)";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select templateinputdtl_gid from ocs_mst_ttemplateinputdtl where template_gid='" + lstemplate_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string msGetTrnGid = objcmnfunctions.GetMasterGID("TTIG");
                    msSQL = " insert into ocs_trn_ttemplateinputdtl (trntemplateinputdtl_gid, templateinputdtl_gid,  " +
                     " templatetype_gid, templatetype_name, template_gid, input_fieldid, input_fieldname, " +
                     " inputgroup_gid, inputgroup_name, input_type, input_placeholder, inputmax_length, " +
                     " input_mandatory, input_previewtext,dbfield_type, created_by, created_date) " +
                     " (select '" + msGetTrnGid + "',templateinputdtl_gid, '" + values.templatetype_gid + "', '" + lstemplate_name + "', " +
                     " template_gid, input_fieldid, input_fieldname, " +
                     " inputgroup_gid, inputgroup_name, input_type, input_placeholder, inputmax_length, " +
                     " input_mandatory,input_previewtext, dbfield_type, '" + user_gid + "', current_timestamp " +
                     " from ocs_mst_ttemplateinputdtl where templateinputdtl_gid='" + dt["templateinputdtl_gid"].ToString() + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    string msdbinputgid = objcmnfunctions.GetMasterGID("TDIG");

                    msSQL = " insert into ocs_trn_ttemplatedbinputdtl (trntemplatedbinputdtl_gid,trntemplateinputdtl_gid, " +
                            " templatedbinputdtl_gid, templatetype_gid, templatetype_name, fieldmapping_gid, template_gid, " +
                            " templateinputdtl_gid, " +
                            " input_fieldid, input_type, stage_fieldid, stage_fieldname, stage_fieldvalue, menu_fieldname, menu_fieldvalue," +
                            " menu_tablevalue, fieldname, field_value,field_tablevalue, field_columnvalue, subfield_flag, subfield_fieldname,subfield_fieldvalue," +
                            " subfield_tablevalue, subfield_columnvalue,subfield_joincolumn,subfield_joincolumn1,subfield_joincolumn2, created_by , created_date) " +
                            " (select '" + msdbinputgid + "','" + msGetTrnGid + "',templatedbinputdtl_gid, '" + values.templatetype_gid + "', " +
                            " '" + lstemplate_name + "', fieldmapping_gid,'" + values.template_gid + "',templateinputdtl_gid, " +
                            " input_fieldid,input_type, stage_fieldid,stage_fieldname,stage_fieldvalue, " +
                            " menu_fieldname,menu_fieldvalue, " +
                            " menu_tablevalue, fieldname,field_value,field_tablevalue,field_columnvalue,subfield_flag,subfield_fieldname, " +
                            " subfield_fieldvalue, subfield_tablevalue,subfield_columnvalue,subfield_joincolumn,subfield_joincolumn1,subfield_joincolumn2,'" + user_gid + "', current_timestamp " +
                            " from ocs_mst_ttemplatedbinputdtl where templateinputdtl_gid = '" + dt["templateinputdtl_gid"].ToString() + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " insert into ocs_trn_ttemplateinputlistdtl (templateinputlistdtl_gid, trntemplateinputdtl_gid,  " +
                    " templatetype_gid, input_fieldid, input_type, input_previewtext, input_value, " +
                    " created_by, created_date) " +
                    " (select  templateinputlistdtl_gid, '" + msGetTrnGid + "', '" + values.templatetype_gid + "', input_fieldid , " +
                    " input_type, input_previewtext, input_value, '" + user_gid + "', current_timestamp " +
                    " from ocs_mst_ttemplateinputlistdtl where templateinputdtl_gid='" + dt["templateinputdtl_gid"].ToString() + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            dt_datatable.Dispose();
            if (values.lstemplatefrom == getTemplateClass1.Contract)
            {
                msSQL = " update agr_trn_tapplication2contract set template_name='" + lstemplate_name + "', " +
                        " template_gid='" + lstemplate_gid + "', " +
                        " defaulttemplate_content='" + lstemplate_content.Replace("'", "\\'") + "' " +
                        " where application2sanction_gid='" + values.templatetype_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Template Details are updated successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }


        public void DaGetFieldMappingSummary(MdlFieldMappinglist values)
        {
            try
            {
                msSQL = " select a.fieldmapping_gid, a.fieldmapping_code,fieldmapping_name, " +
                        " concat(b.user_firstname,' ',b.user_lastname,' / ', b.user_code) as created_by, " +
                        " date_format(a.created_date, '%d-%m-%Y') as created_date " +
                        " from agr_mst_tfieldmapping a " +
                        " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                        " order by a.fieldmapping_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getFieldMappingList = new List<MdlFieldMapping>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getFieldMappingList.Add(new MdlFieldMapping
                        {
                            fieldmapping_gid = dt["fieldmapping_gid"].ToString(),
                            fieldmapping_code = dt["fieldmapping_code"].ToString(),
                            fieldmapping_name = dt["fieldmapping_name"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),
                        });
                    }
                    values.MdlFieldMapping = getFieldMappingList;
                    values.status = true;
                }
                else
                {
                    values.status = false;
                }
                dt_datatable.Dispose();
            }
            catch
            {
                values.status = false;
                dt_datatable.Dispose();
            }
        }


        public void DaPostFieldMappingDtl(string user_gid, MdlFieldMappingdtl values)
        {
            try
            {
                msGetGID = objcmnfunctions.GetMasterGID("FMPS");
                string msGetCode = objcmnfunctions.GetMasterGID("FMS");

                msSQL = "INSERT INTO agr_mst_tfieldmapping(" +
                        " fieldmapping_gid," +
                        " fieldmapping_code," +
                        " fieldmapping_name, " +
                        " input_type, " +
                        " stage_fieldid, " +
                        " stage_fieldname, " +
                        " stage_fieldvalue, " +
                        " menu_fieldname, " +
                        " menu_fieldvalue, " +
                        " menu_tablevalue, " +
                        " fieldname, " +
                        " field_value, " +
                        " field_tablevalue, " +
                        " field_columnvalue, " +
                        " subfield_flag, " +
                        " subfield_fieldname, " +
                        " subfield_fieldvalue, " +
                        " subfield_tablevalue, " +
                        " subfield_columnvalue, ";
                if (values.subfield_joincolumn == "" || values.subfield_joincolumn == null)
                {
                    msSQL += " subfield_joincolumn1, " +
                            " subfield_joincolumn2, ";
                }
                else
                {
                    msSQL += " subfield_joincolumn, ";
                }
                msSQL += " created_by," +
                        " created_date)" +
                        " VALUES(" +
                        "'" + msGetGID + "'," +
                        "'" + msGetCode + "'," +
                        "'" + values.fieldmapping_name.Replace("'", "\\'") + "'," +
                        "'" + values.input_type + "'," +
                        "'" + values.stage_fieldid + "'," +
                        "'" + values.stage_fieldname + "'," +
                        "'" + values.stage_fieldvalue + "'," +
                        "'" + values.menu_fieldname + "'," +
                        "'" + values.menu_fieldvalue + "'," +
                        "'" + values.menu_tablevalue + "'," +
                        "'" + values.fieldname + "'," +
                        "'" + values.field_value + "'," +
                        "'" + values.field_tablevalue + "'," +
                        "'" + values.field_columnvalue + "'," +
                        "'" + values.subfield_flag + "'," +
                        "'" + values.subfield_fieldname + "'," +
                        "'" + values.subfield_fieldvalue + "'," +
                        "'" + values.subfield_tablevalue + "'," +
                        "'" + values.subfield_columnvalue + "',";
                if (values.subfield_joincolumn == "" || values.subfield_joincolumn == null)
                {
                    msSQL += "'" + values.subfield_joincolumn1 + "'," +
                             "'" + values.subfield_joincolumn2 + "',";
                }
                else
                {
                    msSQL += "'" + values.subfield_joincolumn + "',";
                }
                msSQL += "'" + user_gid + "'," +
                        "current_timestamp)";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                if (mnResult == 1)
                {
                    values.message = "Field Mapping Details are Added Successfully..!";
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

        }

        public void DaGetFieldMappingDtl(string fieldmapping_gid, MdlFieldMappingdtl values)
        {
            try
            {
                msSQL = " select a.fieldmapping_code,fieldmapping_name,stage_fieldname,subfield_flag,  " +
                      " menu_fieldname,fieldname, subfield_fieldname " +
                      " from agr_mst_tfieldmapping a " +
                      " where a.fieldmapping_gid = '" + fieldmapping_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.fieldmapping_code = objODBCDatareader["fieldmapping_code"].ToString();
                    values.fieldmapping_name = objODBCDatareader["fieldmapping_name"].ToString();
                    values.stage_fieldname = objODBCDatareader["stage_fieldname"].ToString();
                    values.menu_fieldname = objODBCDatareader["menu_fieldname"].ToString();
                    values.fieldname = objODBCDatareader["fieldname"].ToString();
                    values.subfield_flag = objODBCDatareader["subfield_flag"].ToString();
                    values.subfield_fieldname = objODBCDatareader["subfield_fieldname"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetFieldMappingDropdown(string input_type, MdlFieldMappingDropdownlist values)
        {
            try
            {
                msSQL = " select a.fieldmapping_gid,a.fieldmapping_code,fieldmapping_name,stage_fieldname, " +
                        " subfield_flag, menu_fieldname,fieldname, subfield_fieldname " +
                        " from agr_mst_tfieldmapping a " +
                        " where a.input_type = '" + input_type + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getFieldMappingList = new List<MdlFieldMappingDropdown>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getFieldMappingList.Add(new MdlFieldMappingDropdown
                        {
                            fieldmapping_gid = dt["fieldmapping_gid"].ToString(),
                            fieldmapping_code = dt["fieldmapping_code"].ToString(),
                            fieldmapping_name = dt["fieldmapping_name"].ToString(),
                            stage_fieldname = dt["stage_fieldname"].ToString(),
                            subfield_flag = dt["subfield_flag"].ToString(),
                            menu_fieldname = dt["menu_fieldname"].ToString(),
                            fieldname = dt["fieldname"].ToString(),
                            subfield_fieldname = dt["subfield_fieldname"].ToString(),
                        });
                    }
                    values.status = true;
                    values.MdlFieldMappingDropdown = getFieldMappingList;
                }
                else
                {
                    values.status = false;
                }
                dt_datatable.Dispose();
            }
            catch
            {
                values.status = false;
                dt_datatable.Dispose();
            }
        }



        public void DaGetTrnDBInputList(string template_gid, string templatetype_gid, string application_gid, MdlDBInputList values)
        {
            try
            {
                msSQL = " select a.trntemplateinputdtl_gid, a.templateinputdtl_gid, input_fieldname,input_placeholder, " +
                        " b.input_fieldid,b.input_type, field_value,input_givendata, " +
                        " menu_tablevalue, field_tablevalue, field_columnvalue, " +
                        " subfield_flag, subfield_fieldvalue, subfield_joincolumn,subfield_joincolumn1,subfield_joincolumn2, " +
                        " subfield_tablevalue, subfield_columnvalue " +
                        " from ocs_trn_ttemplatedbinputdtl a " +
                        " left join ocs_trn_ttemplateinputdtl b on a.templateinputdtl_gid = b.templateinputdtl_gid " +
                        " where a.templatetype_gid='" + templatetype_gid + "' and a.template_gid='" + template_gid + "' group by a.templateinputdtl_gid";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getTemplateDtlsList = new List<MdlDBInputDtls>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getTemplateDtlsList.Add(new MdlDBInputDtls
                        {
                            templateinputdtl_gid = dt["trntemplateinputdtl_gid"].ToString(),
                            input_givendata = dt["input_givendata"].ToString(),
                            input_fieldname = dt["input_fieldname"].ToString(),
                            input_placeholder = dt["input_placeholder"].ToString(),
                            input_type = dt["input_type"].ToString(),
                            input_fieldid = dt["input_fieldid"].ToString(),
                            menu_tablevalue = dt["menu_tablevalue"].ToString(),
                            field_tablevalue = dt["field_tablevalue"].ToString(),
                            field_value = dt["field_value"].ToString(),
                            field_columnvalue = dt["field_columnvalue"].ToString(),
                            subfield_flag = dt["subfield_flag"].ToString(),
                            subfield_fieldvalue = dt["subfield_fieldvalue"].ToString(),
                            subfield_tablevalue = dt["subfield_tablevalue"].ToString(),
                            subfield_columnvalue = dt["subfield_columnvalue"].ToString(),
                            subfield_joincolumn = dt["subfield_joincolumn"].ToString(),
                            subfield_joincolumn1 = dt["subfield_joincolumn1"].ToString(),
                            subfield_joincolumn2 = dt["subfield_joincolumn2"].ToString(),
                        });
                    }
                    dt_datatable.Dispose();
                    values.MdlDBInputDtls = getTemplateDtlsList;
                    if (values.MdlDBInputDtls != null)
                    {
                        int j = 0;
                        foreach (var i in values.MdlDBInputDtls)
                        {
                            if (i.subfield_flag == "N" && i.input_type == "text")
                            {
                                msSQL = " select " + i.field_columnvalue + " from  " + i.menu_tablevalue + " " +
                                        " where application_gid='" + application_gid + "'";
                                values.MdlDBInputDtls[j].input_fieldvalue = objdbconn.GetExecuteScalar(msSQL);
                            }
                            else if (i.subfield_flag == "N" && i.input_type != "text")
                            {
                                List<MdlInputGivenList> InputInfoList = new List<MdlInputGivenList>();
                                msSQL = " select " + i.field_columnvalue + " as input_fieldvalue from  " + i.field_tablevalue + " " +
                                        " where application_gid='" + application_gid + "'";
                                dt_datatable = objdbconn.GetDataTable(msSQL);
                                InputInfoList = cmnfunctions.ConvertDataTable<MdlInputGivenList>(dt_datatable);
                                dt_datatable.Dispose();
                                values.MdlDBInputDtls[j].MdlInputGivenList = InputInfoList;
                            }
                            else if (i.subfield_flag == "Y" && i.subfield_tablevalue == "")
                            {
                                List<MdlInputGivenList> InputInfoList = new List<MdlInputGivenList>();
                                msSQL = " select " + i.subfield_columnvalue + " as input_fieldvalue from  " + i.menu_tablevalue + " " +
                                        " where application_gid='" + application_gid + "' " +
                                        " and " + i.field_columnvalue + " = '" + i.field_value + "'";
                                dt_datatable = objdbconn.GetDataTable(msSQL);
                                InputInfoList = cmnfunctions.ConvertDataTable<MdlInputGivenList>(dt_datatable);
                                dt_datatable.Dispose();
                                values.MdlDBInputDtls[j].MdlInputGivenList = InputInfoList;
                            }
                            else if (i.subfield_flag == "Y" && i.subfield_tablevalue != "" && i.subfield_joincolumn1 == "" && i.subfield_joincolumn2 == "")
                            {
                                List<MdlInputGivenList> InputInfoList = new List<MdlInputGivenList>();
                                msSQL = " select concat(a." + i.subfield_columnvalue + ") as input_fieldvalue from  " + i.subfield_tablevalue + " a " +
                                        " left join " + i.menu_tablevalue + " b on a." + i.subfield_joincolumn + " = b." + i.subfield_joincolumn + " " +
                                        " where application_gid='" + application_gid + "' " +
                                        " and " + i.field_columnvalue + " = '" + i.field_value + "'";
                                dt_datatable = objdbconn.GetDataTable(msSQL);
                                InputInfoList = cmnfunctions.ConvertDataTable<MdlInputGivenList>(dt_datatable);
                                dt_datatable.Dispose();
                                values.MdlDBInputDtls[j].MdlInputGivenList = InputInfoList;
                            }
                            else if (i.subfield_flag == "Y" && i.subfield_tablevalue != "" && i.subfield_joincolumn1 != "" && i.subfield_joincolumn2 != "")
                            {
                                List<MdlInputGivenList> InputInfoList = new List<MdlInputGivenList>();
                                msSQL = " select concat(a." + i.subfield_columnvalue + ") as input_fieldvalue from  " + i.subfield_tablevalue + " a " +
                                        " left join " + i.menu_tablevalue + " b on a." + i.subfield_joincolumn2 + " = b." + i.subfield_joincolumn1 + " " +
                                        " where b.application_gid='" + application_gid + "' " +
                                        " and b." + i.field_columnvalue + " = '" + i.field_value + "'";
                                dt_datatable = objdbconn.GetDataTable(msSQL);
                                InputInfoList = cmnfunctions.ConvertDataTable<MdlInputGivenList>(dt_datatable);
                                dt_datatable.Dispose();
                                values.MdlDBInputDtls[j].MdlInputGivenList = InputInfoList;
                            }
                            j++;
                        }
                    }
                    values.status = true;
                }
                else
                {
                    values.status = false;
                }
            }
            catch
            {
                values.status = false;
                dt_datatable.Dispose();
            }
        }

    }
}