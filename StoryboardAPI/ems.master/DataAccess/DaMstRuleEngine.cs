using ems.master.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Web;

namespace ems.master.DataAccess
{
    public class DaMstRuleEngine
    {

        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        string msSQL, msGetGid, msGetGid1, msGetAPICode;
        string lsrule_id, lsrule_function, lsparam_value,lsruletemplatemastergid;
        int mnResult;
        int count_Pass, total_rules;
        double Percentage, doublecount_Pass, doubletotal_rules;

        public void DaPostAddRule(addrule values, string user_gid)
        {
            msGetAPICode = objcmnfunctions.GetApiMasterGID("REAC");
            msGetGid = objcmnfunctions.GetMasterGID("RLMS");

            msSQL = " insert into ocs_mst_truleenginemaster(" +
                    " ruleenginemaster_gid," +
                    " api_code," +
                    " rule_id ," +
                    " rule_title," +
                    " rule_function," +
                    " rule_category," +
                    " rule_datatype," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + msGetAPICode + "'," +
                    "'" + values.rule_id + "'," +
                    "'" + values.rule_title + "'," +
                     "'" + values.rule_function + "'," +
                     "'" + values.rule_category + "'," +           
                     "'" + values.rule_datatype + "'," +
                    "'" + user_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Rule Added Successfully";
            }
            else
            {
                values.message = "Error Occured While Adding";
                values.status = false;
            }
        }

        public void DaGetRuleSummary(MdlMstRuleEngine values)
        {
            try
            {
                msSQL = " SELECT ruleenginemaster_gid,rule_id, rule_title, rule_function,rule_category,rule_datatype,param_name,param_value,param_remarks,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by, api_code " +
                        " FROM ocs_mst_truleenginemaster a" +
                        " left join adm_mst_tuser b on a.created_by = b.user_gid order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getrule_list = new List<rule_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getrule_list.Add(new rule_list
                        {
                            ruleenginemaster_gid = (dr_datarow["ruleenginemaster_gid"].ToString()),
                            rule_id = (dr_datarow["rule_id"].ToString()),
                            rule_title = (dr_datarow["rule_title"].ToString()),
                            rule_function = (dr_datarow["rule_function"].ToString()),
                            rule_category = (dr_datarow["rule_category"].ToString()),
                            rule_datatype = (dr_datarow["rule_datatype"].ToString()),
                            param_name = (dr_datarow["param_name"].ToString()),
                            param_value = (dr_datarow["param_value"].ToString()),
                            param_remarks = (dr_datarow["param_remarks"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),

                        });
                    }
                    values.rule_list = getrule_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }



        public void DaPostAddTemplate(addtemplate_assignrules values, string user_gid)
        {

            msGetGid = objcmnfunctions.GetMasterGID("TEMS");

            msSQL = " insert into ocs_mst_truletemplatemaster(" +
                         " ruletemplatemaster_gid," +
                         " template_code ," +
                         " template_name," +
                         " created_by," +
                         " created_date)" +
                         " values(" +
                         "'" + msGetGid + "'," +
                         //"'" + values.ruletemplatemaster_gid + "'," +
                         "'" + values.template_code + "'," +
                         "'" + values.template_name + "'," +
                         "'" + user_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                

                var count = 0;
                foreach (string i in values.ruleenginemaster_gid)
                {

                    msSQL = " select rule_id,rule_function,param_value from ocs_mst_truleenginemaster where ruleenginemaster_gid='" + i + "'";

                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsrule_id = objODBCDatareader["rule_id"].ToString();
                        lsrule_function = objODBCDatareader["rule_function"].ToString();
                        lsparam_value = objODBCDatareader["param_value"].ToString();
                    }
                    objODBCDatareader.Close();
                    msGetGid1 = objcmnfunctions.GetMasterGID("TEPR");

                    msSQL = " insert into ocs_trn_truletemplateparameter(" +
                             " ruletemplateparameter_gid," +
                             " ruleenginemaster_gid ," +
                             " rule_id," +
                             " ruletemplatemaster_gid," +
                             " template_code," +
                             " rule_parameter," +
                             " created_by," +
                             " created_date)" +
                             " values(" +
                             "'" + msGetGid1 + "'," +
                              "'"+ i + "',"+
                             "'" + lsrule_id + "'," +
                              "'" + msGetGid + "'," +
                             "'" + values.template_code + "'," +
                             "'" + lsparam_value + "'," +
                             "'" + user_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    count = count + 1;
                }

                if (count == 0)
                {
                    values.status = false;
                    values.message = "Rules Assigned to Template Sucessfully";

                }
                else
                {
                    values.status = true;
                    values.message = "Error Occured While Assigned to Template Sucessfully";

                }

            }
            else
            {
                values.message = "Error Occured While Adding Template";
                values.status = false;
            }
        }

        
        public void DaGetTemplateSummary(MdlMstRuleEngine values)
        {
            try
            {
                msSQL = " SELECT ruletemplatemaster_gid,api_code,template_code, template_name, concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by ,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by " +
                        " FROM ocs_mst_truletemplatemaster a" +
                        " left join adm_mst_tuser b on a.created_by = b.user_gid order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getruletemplate_list = new List<ruletemplate_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getruletemplate_list.Add(new ruletemplate_list
                        {
                            ruletemplatemaster_gid = (dr_datarow["ruletemplatemaster_gid"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                            template_code = (dr_datarow["template_code"].ToString()),
                            template_name = (dr_datarow["template_name"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()), 
                            //updated_by = (dr_datarow["updated_by"].ToString()),
                            //updated_date = (dr_datarow["updated_date"].ToString()),
                        });
                    }
                    values.ruletemplate_list = getruletemplate_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaTemplateSummaryForConfiguration(MdlMstRuleEngine values, string ruletemplatemaster_gid)
        {
            try
            {    
                msSQL = " SELECT a.ruletemplateparameter_gid,a.ruleenginemaster_gid,a.rule_id,b.rule_title,b.rule_function,b.rule_datatype,b.param_name,b.param_value,b.param_remarks,a.ruletemplatemaster_gid,a.template_code,c.template_name, a.rule_parameter" +
                        " FROM ocs_trn_truletemplateparameter a " +
                        " left join ocs_mst_truleenginemaster b on a.ruleenginemaster_gid=b.ruleenginemaster_gid " +
                         " left join ocs_mst_truletemplatemaster c on a.ruletemplatemaster_gid=c.ruletemplatemaster_gid " +
                        " where a.ruletemplatemaster_gid='" + ruletemplatemaster_gid + "' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gettemplateforconfigure_list = new List<templateforconfigure_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        gettemplateforconfigure_list.Add(new templateforconfigure_list
                        {
                            ruletemplateparameter_gid = (dr_datarow["ruletemplateparameter_gid"].ToString()),
                            template_code = (dr_datarow["template_code"].ToString()),
                            template_name = (dr_datarow["template_name"].ToString()),
                            rule_id = (dr_datarow["rule_id"].ToString()),
                            rule_title = (dr_datarow["rule_title"].ToString()),
                            rule_function = (dr_datarow["rule_function"].ToString()),
                            rule_parameter = (dr_datarow["rule_parameter"].ToString()),
                            rule_datatype = (dr_datarow["rule_datatype"].ToString()),
                            param_name = (dr_datarow["param_name"].ToString()),
                            param_value = (dr_datarow["param_value"].ToString()),
                            param_remarks = (dr_datarow["param_remarks"].ToString()),

                        });
                    }
                    values.templateforconfigure_list = gettemplateforconfigure_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }


        //datatype wise input field change
        public void DaconfigRule(string ruleenginemaster_gid, addrule values)
        {
            msSQL = " SELECT ruleenginemaster_gid,rule_id, rule_title, rule_function,rule_datatype,param_name,param_value,param_remarks" +
                      " FROM ocs_mst_truleenginemaster " +
                       " where ruleenginemaster_gid='" + ruleenginemaster_gid + "'";

           
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.rule_datatype = objODBCDatareader["rule_datatype"].ToString();
                values.ruleenginemaster_gid = objODBCDatareader["ruleenginemaster_gid"].ToString();
                values.param_name = objODBCDatareader["param_name"].ToString();
                values.param_value = objODBCDatareader["param_value"].ToString();
                values.param_remarks = objODBCDatareader["param_remarks"].ToString();
            }
            objODBCDatareader.Close();
            
           


        }

        //submitting param values in rule master
        public void DaPostconfigureRule(addrule values, string user_gid)
        {
          

            msSQL = " update ocs_mst_truleenginemaster set " +
               " param_name='" + values.param_name.Replace("'", "") + "'," +
               " param_value='" + values.param_value + "'," +
               " param_remarks='" + values.param_remarks + "'" +
               " where ruleenginemaster_gid='" + values.ruleenginemaster_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Rule configured Successfully";
            }
            else
            {
                values.message = "Error Occured While Configure";
                values.status = false;
            }
        }

        //edit configuration
        public void DaEditconfigureRule(string ruleenginemaster_gid, addrule values)
        {
            msSQL = " select param_name,param_value,param_remarks from ocs_mst_truleenginemaster " +
                    " where ruleenginemaster_gid='" + ruleenginemaster_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.param_name = objODBCDatareader["param_name"].ToString();
                values.param_value = objODBCDatareader["param_value"].ToString();
                values.param_remarks = objODBCDatareader["param_remarks"].ToString();
               
            }
            objODBCDatareader.Close();
            values.status = true;
            
        }

        //update configuration
        public void DaUpdateconfigureRule(string employee_gid, addrule values)
        {

     
            msSQL = " update ocs_mst_truleenginemaster set " +
                 " param_value='" + values.param_value + "'," +
                 " param_remarks='" + values.param_remarks + "'" +
                 " where ruleenginemaster_gid='" + values.ruleenginemaster_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

         
           
            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Rule configuration Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }

  

        //TemplateDropdown
        public void DaGetTemplateDropDown(MdlMstRuleEngine values)
        {
            msSQL = "select ruletemplatemaster_gid,template_code,template_name from ocs_mst_truletemplatemaster";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getTemplateList = new List<Template_DropDown>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getTemplateList.Add(new Template_DropDown
                    {
                        ruletemplatemaster_gid = dt["ruletemplatemaster_gid"].ToString(),
                        template_code = dt["template_code"].ToString(),
                        template_name = dt["template_name"].ToString()

                    });
                    values.Template_DropDown = getTemplateList;
                }
            }
            dt_datatable.Dispose();

        }

        //ApplicationDropdown
        public void DaGetApplicationDropDown(MdlMstRuleEngine values)
        {
            msSQL = "select application_gid,application_no from ocs_mst_tapplication";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getApplicationList = new List<Application_DropDown>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getApplicationList.Add(new Application_DropDown
                    {
                        application_gid = dt["application_gid"].ToString(),
                        application_no = dt["application_no"].ToString()
                       

                    });
                    values.Application_DropDown = getApplicationList;
                }
            }
            dt_datatable.Dispose();

        }

        //execute summmary
        public void DaPostExecuteSummary(MdlMstRuleEngine values)
        {
            try
            {
               
                msSQL = "select a.application_gid,b.application_no,a.postruleenginerun_gid,concat(d.user_firstname,' ',d.user_lastname,' / ',d.user_code) as created_by,a.created_date,c.template_name " +
                        "From ocs_trn_tpostruleenginerun a left join ocs_mst_tapplication b on a.application_gid = b.application_gid left join ocs_mst_truletemplatemaster c on a.ruletemplatemaster_gid = c.ruletemplatemaster_gid left join adm_mst_tuser d on a.created_by = d.user_gid order by created_date desc";

               

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getpostexecute_list = new List<postexecute_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getpostexecute_list.Add(new postexecute_list
                        {
                            postruleenginerun_gid = (dr_datarow["postruleenginerun_gid"].ToString()),                          
                            application_gid = (dr_datarow["application_gid"].ToString()),
                            application_no = (dr_datarow["application_no"].ToString()),
                            template_name = (dr_datarow["template_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            
                            

                        });
                    }
                    values.postexecute_list = getpostexecute_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }

          
        }

        //execute view summmary
        public void DaPostExecuteSummaryView(string postruleenginerun_gid, MdlMstRuleEngine values)
        {
            try
            {
                msSQL = "select a.postruleenginerun_gid,a.created_date,b.postruleenginerundetails_gid,b.application_gid,b.template_code,b.rule_id,b.ruleengine_result,b.ruleengineresult_details,c.application_no,d.template_name, e.rule_title,e.param_remarks " +
                        "From ocs_trn_tpostruleenginerun a left join ocs_trn_tpostruleenginerundetails b on a.postruleenginerun_gid = b.postruleenginerun_gid left join ocs_mst_tapplication c on b.application_gid=c.application_gid " +
                        "left join ocs_mst_truletemplatemaster d on b.template_code = d.template_code left join ocs_mst_truleenginemaster e on b.rule_id = e.rule_id where a.postruleenginerun_gid='" + postruleenginerun_gid + "'";
                //msSQL = "select a.postruleenginerun_gid,b.postruleenginerundetails_gid,b.application_gid,b.template_code,b.rule_id,b.ruleengine_result,b.ruleengineresult_details,c.application_no " +
                //        "From ocs_trn_tpostruleenginerun a left join ocs_trn_tpostruleenginerundetails b on a.postruleenginerun_gid = b.postruleenginerun_gid left join ocs_mst_tapplication c on b.application_gid=c.application_gid where a.postruleenginerun_gid='" + postruleenginerun_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getpostexecute_list = new List<postexecute_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getpostexecute_list.Add(new postexecute_list
                        {
                            postruleenginerun_gid = (dr_datarow["postruleenginerun_gid"].ToString()),
                            postruleenginerundetails_gid = (dr_datarow["postruleenginerundetails_gid"].ToString()),
                            application_gid = (dr_datarow["application_gid"].ToString()),
                            application_no = (dr_datarow["application_no"].ToString()),
                            template_code = (dr_datarow["template_code"].ToString()),
                            template_name = (dr_datarow["template_name"].ToString()),                       
                            rule_id = (dr_datarow["rule_id"].ToString()),
                            rule_title = (dr_datarow["rule_title"].ToString()),
                            ruleengine_result = (dr_datarow["ruleengine_result"].ToString()),
                            ruleengineresult_details = (dr_datarow["ruleengineresult_details"].ToString()),
                            param_remarks = (dr_datarow["param_remarks"].ToString()),

                        });
                    }
                    values.postexecute_list = getpostexecute_list;
                }
                dt_datatable.Dispose();
                values.status = true;
                msSQL = " select distinct b.template_code,d.template_name,c.application_no From ocs_trn_tpostruleenginerun a left join ocs_trn_tpostruleenginerundetails b on a.postruleenginerun_gid = b.postruleenginerun_gid left join ocs_mst_tapplication c on b.application_gid=c.application_gid left join ocs_mst_truletemplatemaster d on b.template_code = d.template_code left join ocs_mst_truleenginemaster e on b.rule_id = e.rule_id where a.postruleenginerun_gid='" + postruleenginerun_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.template_code = objODBCDatareader["template_code"].ToString();
                    values.application_no = objODBCDatareader["application_no"].ToString();
                    values.template_name = objODBCDatareader["template_name"].ToString();

                }
                objODBCDatareader.Close();

                

                msSQL = " SELECT ruletemplatemaster_gid FROM ocs_trn_tpostruleenginerun where postruleenginerun_gid='" + postruleenginerun_gid + "'";

                lsruletemplatemastergid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " SELECT ruleenginemaster_gid,rule_id FROM ocs_trn_truletemplateparameter " +
                      " where ruletemplatemaster_gid='" + lsruletemplatemastergid + "' ";

                dt_datatable = objdbconn.GetDataTable(msSQL);

                List<string> ruletemplatemastergid_list = new List<string>();

                ruletemplatemastergid_list = dt_datatable.AsEnumerable().Select(p => p.Field<string>("ruleenginemaster_gid")).ToList();


                doubletotal_rules = ruletemplatemastergid_list.Count();



                msSQL = " SELECT COUNT(*) FROM ocs_trn_tpostruleenginerundetails where ruleengine_result='Pass' and postruleenginerun_gid='" + postruleenginerun_gid + "'";

                string lscount_Pass = objdbconn.GetExecuteScalar(msSQL);

                int.TryParse(lscount_Pass, out count_Pass);

                doublecount_Pass=Convert.ToDouble(count_Pass);

                double total = 100;

                Percentage = (doublecount_Pass / doubletotal_rules) *(total);

                Percentage = Math.Round(Percentage, 2);

                values.percentage = Percentage.ToString();


            }
            catch
            {
                values.status = false;
            }
        }

        // Group Title - Start

        public void DaGetGroupTitle(MdlMstRuleEngine objMdlgrouptitle)
        {
            try
            {
                msSQL = " SELECT grouptitle_gid,grouptitle_name,lms_code,bureau_code,status, " +
                    " date_format(a.created_date,'%d-%m-%Y || %h:%i %p') as created_date,concat(c.user_firstname,' ' ,c.user_lastname,' || ',c.user_code) as created_by " +
                    " from ocs_mst_tgrouptitle a" +
                    " left join hrm_mst_temployee b on a.created_by=b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid order by grouptitle_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getgrouptitle_list = new List<grouptitle_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getgrouptitle_list.Add(new grouptitle_list
                        {
                            grouptitle_gid = (dr_datarow["grouptitle_gid"].ToString()),
                            grouptitle_name = (dr_datarow["grouptitle_name"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                        });
                    }
                    objMdlgrouptitle.grouptitle_list = getgrouptitle_list;
                }
                dt_datatable.Dispose();
                objMdlgrouptitle.status = true;
            }
            catch
            {
                objMdlgrouptitle.status = false;
            }
        }

        public void DaCreateGroupTitle(GroupTitle values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("GTCL");
            msSQL = " insert into ocs_mst_tgrouptitle(" +
                    " grouptitle_gid," +
                    " lms_code," +
                    " bureau_code," +
                    " grouptitle_name," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "',";
            if (values.lms_code == "" || values.lms_code == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.lms_code.Replace("'", "") + "',";
            }
            if (values.bureau_code == "" || values.bureau_code == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.bureau_code.Replace("'", "") + "',";
            }

            msSQL += "'" + values.grouptitle_name.Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Group Title Added Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while adding";
                values.status = false;
            }
        }

        public void DaEditGroupTitle(string grouptitle_gid, GroupTitle values)
        {
            try
            {
                msSQL = " select grouptitle_gid,lms_code,bureau_code,status ,grouptitle_name from ocs_mst_tgrouptitle where grouptitle_gid='" + grouptitle_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.lms_code = objODBCDatareader["lms_code"].ToString();
                    values.bureau_code = objODBCDatareader["bureau_code"].ToString();
                    values.Status = objODBCDatareader["status"].ToString();
                    values.grouptitle_name = objODBCDatareader["grouptitle_name"].ToString();
                    values.grouptitle_gid = objODBCDatareader["grouptitle_gid"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaUpdateGroupTitle(string employee_gid, GroupTitle values)
        {
            msSQL = "select updated_by, updated_date,grouptitle_name from ocs_mst_tgrouptitle where grouptitle_gid = '" + values.grouptitle_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("GTUL");
                    msSQL = " insert into ocs_trn_tgrouptitlelog(" +
                              " grouptitlelog_gid," +
                              " grouptitle_gid," +
                              " grouptitle_name, " +
                              " created_by, " +
                              " created_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.grouptitle_gid + "'," +
                              "'" + objODBCDatareader["grouptitle_name"].ToString() + "'," +
                               "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();
            msSQL = " update ocs_mst_tgrouptitle set ";
            if (values.lms_code == "" || values.lms_code == null)
            {
                msSQL += " lms_code='',";
            }
            else
            {
                msSQL += " lms_code='" + values.lms_code + "',";
            }
            if (values.bureau_code == "" || values.bureau_code == null)
            {
                msSQL += " bureau_code='',";
            }
            else
            {
                msSQL += " bureau_code='" + values.bureau_code + "',";
            }

            msSQL += " grouptitle_name='" + values.grouptitle_name + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where grouptitle_gid='" + values.grouptitle_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Group Title Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }

        /* public void DaGroupTitleDelete(string answertype_gid, string employee_gid, answertype values)
        {
            msSQL = " select answertype_gid from ocs_mst_tapplication2loan where answertype_gid='" + answertype_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                values.message = "Can't able to Delete Group Title, Because it is tagged to Application Creation";
                values.status = false;
                return;
            }
            else
            {
                objODBCDatareader.Close();
                msSQL = " select grouptitle_name from ocs_mst_tanswertype where answertype_gid='" + answertype_gid + "'";
                lsmaster_value = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " delete from ocs_mst_tanswertype where answertype_gid='" + answertype_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Group Title Deleted Successfully..!";
                    msGetGid = objcmnfunctions.GetMasterGID("MSTD");
                    msSQL = " insert into ocs_mst_tmasterdeletelog(" +
                             "master_gid, " +
                             "master_name, " +
                             "master_value, " +
                             "deleted_by, " +
                             "deleted_date) " +
                             " values(" +
                             "'" + msGetGid + "'," +
                             "'Group Title'," +
                             "'" + lsmaster_value + "'," +
                             "'" + employee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    values.status = false;
                }
            }
        }*/

        public void DaGroupTitleStatusUpdate(string employee_gid, GroupTitle values)
        {

            msSQL = " update ocs_mst_tgrouptitle set status='" + values.Status + "'," +
                " remarks='" + values.remarks.Replace("'", "\\'") + "'," +
                " updated_by='" + employee_gid + "'," +
                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                " where grouptitle_gid='" + values.grouptitle_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("GTSU");
                msSQL = " insert into ocs_mst_tgrouptitleinactivelog(" +
                          " grouptitleinactivelog_gid," +
                          " grouptitle_gid," +
                          " status, " +
                          " remarks, " +
                          " updated_by, " +
                          " updated_date) " +
                          " values(" +
                          "'" + msGetGid + "'," +
                          "'" + values.grouptitle_gid + "'," +
                          "'" + values.Status + "'," +
                          "'" + values.remarks.Replace("'", "\\'") + "'," +
                          "'" + employee_gid + "'," +
                          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.message = "Status Updated Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while updating Status";
                values.status = false;
            }
        }
        public void DaGetGroupTitleInactiveLog(string grouptitle_gid, MdlMstRuleEngine objMdlgrouptitle)
        {
            try
            {
                msSQL = " SELECT d.grouptitle_name,a.status,a.remarks, " +
                    " date_format(a.updated_date,'%d-%m-%Y || %h:%i %p') as updated_date,concat(c.user_firstname,' ' ,c.user_lastname,' || ',c.user_code) as updated_by" +
                    " FROM ocs_mst_tgrouptitleinactivelog a" +
                    " left join hrm_mst_temployee b on a.updated_by=b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    "  left join ocs_mst_tgrouptitle d on a.grouptitle_gid=d.grouptitle_gid where a.grouptitle_gid='" + grouptitle_gid + "'" +
                    " order by a.grouptitleinactivelog_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getSegment = new List<grouptitle_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getSegment.Add(new grouptitle_list
                        {
                            grouptitle_name = (dr_datarow["grouptitle_name"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                        });
                    }
                    objMdlgrouptitle.grouptitle_list = getSegment;
                }
                dt_datatable.Dispose();
                objMdlgrouptitle.status = true;

            }
            catch
            {
                objMdlgrouptitle.status = false;
            }
        }


        // Answer Type - Start
        public void DaGetAnswerType(MdlMstRuleEngine objMdlAnswertype)
        {
            try
            {
                msSQL = " SELECT answertype_gid,answertype_name,lms_code,bureau_code,status, " +
                    " date_format(a.created_date,'%d-%m-%Y || %h:%i %p') as created_date,concat(c.user_firstname,' ' ,c.user_lastname,' || ',c.user_code) as created_by " +
                    " from ocs_mst_tanswertype a" +
                    " left join hrm_mst_temployee b on a.created_by=b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid order by answertype_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getanswertype_list = new List<answertype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getanswertype_list.Add(new answertype_list
                        {
                            answertype_gid = (dr_datarow["answertype_gid"].ToString()),
                            answertype_name = (dr_datarow["answertype_name"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                        });
                    }
                    objMdlAnswertype.answertype_list = getanswertype_list;
                }
                dt_datatable.Dispose();
                objMdlAnswertype.status = true;
            }
            catch
            {
                objMdlAnswertype.status = false;
            }
        }

        public void DaCreateAnswerType(string employee_gid, AnswerType values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("ATCL");
            msSQL = " insert into ocs_mst_tanswertype(" +
                    " answertype_gid," +
                    " lms_code," +
                    " bureau_code," +
                    " answertype_name," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "',";
            if (values.lms_code == "" || values.lms_code == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.lms_code.Replace("'", "") + "',";
            }
            if (values.bureau_code == "" || values.bureau_code == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.bureau_code.Replace("'", "") + "',";
            }

            msSQL += "'" + values.answertype_name.Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Answer Type Added Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while adding";
                values.status = false;
            }
        }

        public void DaEditAnswerType(string answertype_gid, AnswerType values)
        {
            try
            {
                msSQL = " select answertype_gid,lms_code,bureau_code,status ,answertype_name from ocs_mst_tanswertype where answertype_gid='" + answertype_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.lms_code = objODBCDatareader["lms_code"].ToString();
                    values.bureau_code = objODBCDatareader["bureau_code"].ToString();
                    values.Status = objODBCDatareader["status"].ToString();
                    values.answertype_name = objODBCDatareader["answertype_name"].ToString();
                    values.answertype_gid = objODBCDatareader["answertype_gid"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaUpdateAnswerType(string employee_gid, AnswerType values)
        {
            msSQL = "select updated_by, updated_date,answertype_name from ocs_mst_tanswertype where answertype_gid = '" + values.answertype_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("ATUL");
                    msSQL = " insert into ocs_trn_tanswertypelog(" +
                              " answertypelog_gid," +
                              " answertype_gid," +
                              " answertype_name, " +
                              " created_by, " +
                              " created_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.answertype_gid + "'," +
                              "'" + objODBCDatareader["answertype_name"].ToString() + "'," +
                               "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();
            msSQL = " update ocs_mst_tanswertype set ";
            if (values.lms_code == "" || values.lms_code == null)
            {
                msSQL += " lms_code='',";
            }
            else
            {
                msSQL += " lms_code='" + values.lms_code + "',";
            }
            if (values.bureau_code == "" || values.bureau_code == null)
            {
                msSQL += " bureau_code='',";
            }
            else
            {
                msSQL += " bureau_code='" + values.bureau_code + "',";
            }

            msSQL += " answertype_name='" + values.answertype_name + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where answertype_gid='" + values.answertype_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Answer Type Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }

        /* public void DaAnswerTypeDelete(string answertype_gid, string employee_gid, AnswerType values)
         {
             msSQL = " select answertype_gid from ocs_mst_tapplication2loan where answertype_gid='" + answertype_gid + "'";
             objODBCDatareader = objdbconn.GetDataReader(msSQL);
             if (objODBCDatareader.HasRows == true)
             {
                 objODBCDatareader.Close();
                 values.message = "Can't able to Delete Answer Type, Because it is tagged to Application Creation";
                 values.status = false;
                 return;
             }
             else
             {
                 objODBCDatareader.Close();
                 msSQL = " select answertype_name from ocs_mst_tanswertype where answertype_gid='" + answertype_gid + "'";
                 lsmaster_value = objdbconn.GetExecuteScalar(msSQL);
                 msSQL = " delete from ocs_mst_tanswertype where answertype_gid='" + answertype_gid + "'";
                 mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                 if (mnResult != 0)
                 {
                     values.status = true;
                     values.message = "Answer Type Deleted Successfully..!";
                     msGetGid = objcmnfunctions.GetMasterGID("MSTD");
                     msSQL = " insert into ocs_mst_tmasterdeletelog(" +
                              "master_gid, " +
                              "master_name, " +
                              "master_value, " +
                              "deleted_by, " +
                              "deleted_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'Answer Type'," +
                              "'" + lsmaster_value + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                     mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                 }
                 else
                 {
                     values.status = false;
                 }
             }
         } */

        public void DaAnswerTypeStatusUpdate(string employee_gid, AnswerType values)
        {

            msSQL = " update ocs_mst_tanswertype set status='" + values.Status + "'," +
                " remarks='" + values.remarks.Replace("'", "\\'") + "'," +
                " updated_by='" + employee_gid + "'," +
                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                " where answertype_gid='" + values.answertype_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("ATSU");
                msSQL = " insert into ocs_mst_tanswertypeinactivelog(" +
                          " answertypeinactivelog_gid," +
                          " answertype_gid," +
                          " status, " +
                          " remarks, " +
                          " updated_by, " +
                          " updated_date) " +
                          " values(" +
                          "'" + msGetGid + "'," +
                          "'" + values.answertype_gid + "'," +
                          "'" + values.Status + "'," +
                          "'" + values.remarks.Replace("'", "\\'") + "'," +
                          "'" + employee_gid + "'," +
                          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.message = "Status Updated Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while updating Status";
                values.status = false;
            }
        }
        public void DaGetAnswerTypeInactiveLog(string answertype_gid, MdlMstRuleEngine values)
        {
            try
            {
                msSQL = " SELECT d.answertype_name,a.status,a.remarks, " +
                    " date_format(a.updated_date,'%d-%m-%Y || %h:%i %p') as updated_date,concat(c.user_firstname,' ' ,c.user_lastname,' || ',c.user_code) as updated_by" +
                    " FROM ocs_mst_tanswertypeinactivelog a" +
                    " left join hrm_mst_temployee b on a.updated_by=b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    "  left join ocs_mst_tanswertype d on a.answertype_gid=d.answertype_gid where a.answertype_gid='" + answertype_gid + "'" +
                    " order by a.answertypeinactivelog_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getSegment = new List<answertype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getSegment.Add(new answertype_list
                        {
                            answertype_name = (dr_datarow["answertype_name"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                        });
                    }
                    values.answertype_list = getSegment;
                }
                dt_datatable.Dispose();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }


    }
}