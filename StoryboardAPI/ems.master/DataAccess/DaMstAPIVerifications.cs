using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using ems.master.Models;
using ems.utilities.Functions;
using System.Data;
using System.Data.Odbc;

/// <summary>
/// (It's used for APIVerifications ) APIVerifications DataAccess Class accessed by API methods from related Controller class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Praveen Raj</remarks>

namespace ems.master.DataAccess
{
    public class DaMstAPIVerifications
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL, msGetGid;
        int mnResult;
        public void DaPostTAN (MdlTAN values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("KTAV");

            msSQL = " update ocs_trn_ttandtl set " +
                    " tandtl_gid='" + msGetGid + "',";
            if(values.remarks==null ||values.remarks== "" || values.remarks == "undefined")
            {
                msSQL += " remarks='',";
            }
            else
            {
                msSQL += " remarks='" + values.remarks.Replace("'", "") + "',";
            }
            msSQL += " updated_by='" + employee_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where tandtl_gid='" + employee_gid + "' ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
         if(mnResult!=0)
            {
                values.message = "TAN Details Added Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while adding";
                values.status = true;
            }
           
        }
        public void DaGetTANsummary(string function_gid,string employee_gid, MdlTAN values)
        {
            msSQL = " select a.tandtl_gid,a.remarks,a.tan ," +
                    " case when a.validation_status = 'Verified' then a.name else '-' end as name," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, a.validation_status from ocs_trn_ttandtl a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                     " left join adm_mst_tuser c on c.user_gid=b.user_gid  where  tandtl_gid not in ('" + employee_gid + "') and function_gid='" + function_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gettan_list = new List<tan_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    gettan_list.Add(new tan_list
                    {
                        tandtl_gid = dt["tandtl_gid"].ToString(),
                        remarks = dt["remarks"].ToString(),
                        tan_no = dt["tan"].ToString(),
                        name = dt["name"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        validation_status = dt["validation_status"].ToString(),
                       });

                }
            }
            values.tan_list = gettan_list;
            dt_datatable.Dispose();
        }

        public void DaPostCompanyLLP(MdlCIN values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("KPNA");

            msSQL = " update ocs_trn_tcompanyllpno set " +
                    " companyllpno_gid='" + msGetGid + "',";
            if (values.remarks == null || values.remarks == "" || values.remarks == "undefined")
            {
                msSQL += " remarks='',";
            }
            else
            {
                msSQL += " remarks='" + values.remarks.Replace("'", "") + "',";
            }
            msSQL += " updated_by='" + employee_gid + "'," +
                       " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                       " where companyllpno_gid='" + employee_gid + "' ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Company and LLP Number Added Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while adding";
                values.status = true;
            }

        }
        public void DaGetCompanyLLP(string function_gid, string employee_gid, MdlCIN values)
        {
            msSQL = " select a.companyllpno_gid,a.remarks,a.cin_no,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, a.validation_status from ocs_trn_tcompanyllpno a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                     " left join adm_mst_tuser c on c.user_gid=b.user_gid  where  companyllpno_gid not in ('" + employee_gid + "') and function_gid='" + function_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcin_list = new List<cin_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcin_list.Add(new cin_list
                    {
                        companyllpno_gid = dt["companyllpno_gid"].ToString(),
                        remarks = dt["remarks"].ToString(),
                        cin_no = dt["cin_no"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        validation_status = dt["validation_status"].ToString(),
                    });

                }
            }
            values.cin_list = getcin_list;
            dt_datatable.Dispose();
        }

        public void DaPostMCASignature(MdlCIN values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("KPNA");

            msSQL = " update ocs_trn_mcasignatories set " +
                " mcasignatories_gid='"+ msGetGid + "',";
            if (values.remarks == null || values.remarks == "" || values.remarks == "undefined")
            {
                msSQL += " remarks='',";
            }
            else
            {
                msSQL += " remarks='" + values.remarks.Replace("'", "") + "',";
            }
            msSQL += " updated_by='" + employee_gid + "'," +
                          " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                          " where mcasignatories_gid='" + employee_gid + "' ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = " update ocs_trn_mcasignatorydetails set " +
                " mcasignatories_gid='" + msGetGid + "'," +        
                 " updated_by='" + employee_gid + "'," +
                              " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                              " where mcasignatories_gid='" + employee_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "MCA Signatories Added Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while adding";
                values.status = true;
            }

        }
        public void DaGetMCASignature(string function_gid, string employee_gid, MdlCIN values)
        {
            msSQL = " select a.mcasignatories_gid,a.remarks,a.cin,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, a.validation_status from ocs_trn_mcasignatories a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                     " left join adm_mst_tuser c on c.user_gid=b.user_gid  where  mcasignatories_gid not in ('" + employee_gid + "') and function_gid='" + function_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcin_list = new List<cin_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcin_list.Add(new cin_list
                    {
                        mcasignatories_gid = dt["mcasignatories_gid"].ToString(),
                        remarks = dt["remarks"].ToString(),
                        cin_no = dt["cin"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        validation_status = dt["validation_status"].ToString(),
                    });

                }
            }
            values.cin_list = getcin_list;
            dt_datatable.Dispose();
        }

        public void DaPostIECDetailed(MdlIECDetailed values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("KPNA");

            msSQL = " update ocs_trn_tiecdtl set " +
                " iecdtl_gid='" + msGetGid + "',";
            if (values.remarks == null || values.remarks == "" || values.remarks == "undefined")
            {
                msSQL += " remarks='',";
            }
            else
            {
                msSQL += " remarks='" + values.remarks.Replace("'","") + "',";
            }
            msSQL += " updated_by='" + employee_gid + "'," +
                          " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                          " where iecdtl_gid='" + employee_gid + "' ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "IEC Detailed Added Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while adding";
                values.status = true;
            }

        }
        public void DaGetIECDetailed(string function_gid, string employee_gid, MdlIECDetailed values)
        {
            msSQL = " select a.iecdtl_gid,a.remarks,a.iec_no,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, a.validation_status from ocs_trn_tiecdtl a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                     " left join adm_mst_tuser c on c.user_gid=b.user_gid  where  iecdtl_gid not in ('" + employee_gid + "') and function_gid='" + function_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getIECDetailed_list = new List<IECDetailed_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getIECDetailed_list.Add(new IECDetailed_list
                    {
                        iecdtl_gid = dt["iecdtl_gid"].ToString(),
                        remarks = dt["remarks"].ToString(),
                        iec_no = dt["iec_no"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        validation_status = dt["validation_status"].ToString(),
                    });

                }
            }
            values.IECDetailed_list = getIECDetailed_list;
            dt_datatable.Dispose();
        }
        //FSSAI
        public void DaPostFSSAI(MdlFSSAI values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("KPNA");

            msSQL = " update ocs_trn_tfssailicenseauthentication set " +
                " fssailicenseauthentication_gid='" + msGetGid + "',";
            if (values.remarks == null || values.remarks == "" || values.remarks == "undefined")
            {
                msSQL += " remarks='',";
            }
            else
            {
                msSQL += " remarks='" + values.remarks.Replace("'", "") + "',";
            }
            msSQL += " updated_by='" + employee_gid + "'," +
                          " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                          " where fssailicenseauthentication_gid='" + employee_gid + "' ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "FSSAI License  Added Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while adding";
                values.status = true;
            }

        }
        public void DaGetFSSAI(string function_gid, string employee_gid, MdlFSSAI values)
        {
            msSQL = " select a.fssailicenseauthentication_gid,a.remarks,a.reg_no,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, a.validation_status from ocs_trn_tfssailicenseauthentication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                     " left join adm_mst_tuser c on c.user_gid=b.user_gid  where  fssailicenseauthentication_gid not in ('" + employee_gid + "') and function_gid='" + function_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getfssai_list = new List<fssai_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getfssai_list.Add(new fssai_list
                    {
                        fssailicenseauthentication_gid = dt["fssailicenseauthentication_gid"].ToString(),
                        remarks = dt["remarks"].ToString(),
                        reg_no = dt["reg_no"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        validation_status = dt["validation_status"].ToString(),
                    });

                }
            }
            values.fssai_list = getfssai_list;
            dt_datatable.Dispose();
        }
        //FDA
        public void DaPostFDA(MdlFDA values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("KPNA");

            msSQL = " update ocs_trn_tfdalicenseauthentication set " +
                " fdalicenseauthentication_gid='" + msGetGid + "',";
            if (values.remarks == null || values.remarks == "" || values.remarks == "undefined")
            {
                msSQL += " remarks='',";
            }
            else
            {
                msSQL += " remarks='" + values.remarks.Replace("'", "") + "',";
            }
            msSQL += " updated_by='" + employee_gid + "'," +
                          " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                          " where fdalicenseauthentication_gid='" + employee_gid + "' ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "FDA License  Added Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while adding";
                values.status = true;
            }

        }
        public void DaGetFDA(string function_gid, string employee_gid, MdlFDA values)
        {
            msSQL = " select a.fdalicenseauthentication_gid,a.remarks,a.license_no,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, a.validation_status from ocs_trn_tfdalicenseauthentication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                     " left join adm_mst_tuser c on c.user_gid=b.user_gid  where  fdalicenseauthentication_gid not in ('" + employee_gid + "') and function_gid='" + function_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getfda_list = new List<fda_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getfda_list.Add(new fda_list
                    {
                        fdalicenseauthentication_gid = dt["fdalicenseauthentication_gid"].ToString(),
                        remarks = dt["remarks"].ToString(),
                        license_no = dt["license_no"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        validation_status = dt["validation_status"].ToString(),
                    });

                }
            }
            values.fda_list = getfda_list;
            dt_datatable.Dispose();
        }

        public void DaGetTANdelete(string tandtl_gid, MdlTAN values)
        {
            msSQL = "delete from ocs_trn_ttandtl where tandtl_gid='" + tandtl_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "TAN Number Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while deleting";
                values.status = false;

            }
        }
        public void DaCompanyLLPNodelete(string companyllpno_gid, MdlTAN values)
        {
            msSQL = "delete from ocs_trn_tcompanyllpno where companyllpno_gid='" + companyllpno_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

               values.message = "Company & LLP Identification Number Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while deleting";
                values.status = false;

            }
        }
        public void DaMCASigndelete(string mcasignatories_gid, MdlTAN values)
        {
            msSQL = "delete from ocs_trn_mcasignatories where mcasignatories_gid='" + mcasignatories_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "MCA Signatories Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while deleting";
                values.status = false;

            }
        }

        public void DaIECdelete(string iecdtl_gid, MdlTAN values)
        {
            msSQL = "delete from ocs_trn_tiecdtl where iecdtl_gid='" + iecdtl_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "IEC Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while deleting";
                values.status = false;

            }
        }
        public void DaFSSAIdelete(string fssailicenseauthentication_gid, MdlTAN values)
        {
            msSQL = "delete from ocs_trn_tfssailicenseauthentication where fssailicenseauthentication_gid='" + fssailicenseauthentication_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "FSSAI License Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while deleting";
                values.status = false;

            }
        }
        public void DaFDAdelete(string fdalicenseauthentication_gid, MdlTAN values)
        {
            msSQL = "delete from ocs_trn_tfdalicenseauthentication where fdalicenseauthentication_gid='" + fdalicenseauthentication_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "FDA License Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while deleting";
                values.status = false;

            }
        }
        public void DaGetStateList(MdlMstGST values)
        {
            try
            {
                msSQL = " SELECT gstcode_gid,gst_state,state_code from ocs_mst_tgstcode2state a";
                   
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmstgst_list = new List<mstgst_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmstgst_list.Add(new mstgst_list
                        {
                            gststate_gid = (dr_datarow["gstcode_gid"].ToString()),
                            gst_state = (dr_datarow["gst_state"].ToString()),
                            state_code = (dr_datarow["state_code"].ToString()),
                           });
                    }
                    values.mstgst_list = getmstgst_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaGetGSTList(MdlMstGST values,string institution_gid)
        {
            try
            {
                msSQL = "select institution2branch_gid,gst_state,gst_no, gst_registered,authentication_status,returnfilling_status,verification_status " +
                    " from ocs_mst_tinstitution2branch where institution_gid='" + institution_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmstgst_list = new List<mstgst_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmstgst_list.Add(new mstgst_list
                        {
                            institution2branch_gid = (dr_datarow["institution2branch_gid"].ToString()),
                            gst_state = (dr_datarow["gst_state"].ToString()),
                            gst_no = (dr_datarow["gst_no"].ToString()),
                            gst_registered = (dr_datarow["gst_registered"].ToString()),
                            authentication_status = (dr_datarow["authentication_status"].ToString()),
                            returnfilling_status = (dr_datarow["returnfilling_status"].ToString()),
                            verification_status = (dr_datarow["verification_status"].ToString())
                        });
                    }
                    values.mstgst_list = getmstgst_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaCompanyLLPViewDetails(string companyllpno_gid, string employee_gid, MdlCompanyLLPDetails values)
        {
            try { 
            msSQL = " select company_name,roc_code,registration_no,company_category,company_subcategory,class_of_company,number_of_members,date_of_incorporation," +
                    " company_status,registered_address,alternative_address,email_address,listed_status,suspended_at_stock_exchange,date_of_last_AGM,date_of_balance_sheet,paid_up_capital,authorised_capital" +
                    " from ocs_trn_tcompanyllpno a" +
                    " where  companyllpno_gid not in ('" + employee_gid + "') and companyllpno_gid='" + companyllpno_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                    values.company_name = objODBCDatareader["company_name"].ToString();
                    values.roc_code = objODBCDatareader["roc_code"].ToString();
                    values.registration_no = objODBCDatareader["registration_no"].ToString();
                    values.company_category = objODBCDatareader["company_category"].ToString();
                    values.company_subcategory = objODBCDatareader["company_subcategory"].ToString();
                    values.class_of_company = objODBCDatareader["class_of_company"].ToString();
                    values.number_of_members = objODBCDatareader["number_of_members"].ToString();
                    values.date_of_incorporation = objODBCDatareader["date_of_incorporation"].ToString();
                    values.company_status = objODBCDatareader["company_status"].ToString();
                    values.registered_address = objODBCDatareader["registered_address"].ToString();
                    values.alternative_address = objODBCDatareader["alternative_address"].ToString();
                    values.email_address = objODBCDatareader["email_address"].ToString();
                    values.listed_status = objODBCDatareader["listed_status"].ToString();
                    values.suspended_at_stock_exchange = objODBCDatareader["suspended_at_stock_exchange"].ToString();
                    values.date_of_last_AGM = objODBCDatareader["date_of_last_AGM"].ToString();
                    values.date_of_balance_sheet = objODBCDatareader["date_of_balance_sheet"].ToString();
                    values.paid_up_capital = objODBCDatareader["paid_up_capital"].ToString();
                    values.authorised_capital = objODBCDatareader["authorised_capital"].ToString();
                }
            values.status = true;
            values.message = "success";
            objODBCDatareader.Close();

            }

            catch
            {
                values.status = false;
                values.message = "failure";
            }


        }

        public void DaFDAViewDetails(string fdalicenseauthentication_gid, string employee_gid, MdlFDADetails values)
        {
            try
            {
                msSQL = " select store_name,contact_no,license_detail,name,address" +
                        " from ocs_trn_tfdalicenseauthentication a" +
                        " where  fdalicenseauthentication_gid not in ('" + employee_gid + "') and fdalicenseauthentication_gid='" + fdalicenseauthentication_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    values.store_name = objODBCDatareader["store_name"].ToString();
                    values.contact_no = objODBCDatareader["contact_no"].ToString();
                    values.license_detail = objODBCDatareader["license_detail"].ToString();
                    values.name = objODBCDatareader["name"].ToString();
                    values.address = objODBCDatareader["address"].ToString();
                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();

            }

            catch
            {
                values.status = false;
                values.message = "failure";
            }


        }

        public void DaFSSAIViewDetails(string fssailicenseauthentication_gid, string employee_gid, MdlFSSAIDetails values)
        {
            try
            {
                msSQL = " select fssai_status,license_type,license_no,firm_name,address" +
                        " from ocs_trn_tfssailicenseauthentication a" +
                        " where  fssailicenseauthentication_gid not in ('" + employee_gid + "') and fssailicenseauthentication_gid='" + fssailicenseauthentication_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    values.fssai_status = objODBCDatareader["fssai_status"].ToString();
                    values.license_type = objODBCDatareader["license_type"].ToString();
                    values.license_no = objODBCDatareader["license_no"].ToString();
                    values.firm_name = objODBCDatareader["firm_name"].ToString();
                    values.address = objODBCDatareader["address"].ToString();
                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();

            }

            catch
            {
                values.status = false;
                values.message = "failure";
            }


        }


        public void DaMCASignatoriesViewDetails(string mcasignatories_gid, string employee_gid, MdlMCASignatoryDetails values)
        {
            try
            {
                msSQL = "select date_of_appointment,designation,dsc_expiry_date, wheather_dsc_registered,DINDPINPAN,full_name,address " +
                    " from ocs_trn_mcasignatorydetails where mcasignatories_gid='" + mcasignatories_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmcasignatorydetails_list = new List<mcasignatorydetails_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmcasignatorydetails_list.Add(new mcasignatorydetails_list
                        {
                            date_of_appointment = (dr_datarow["date_of_appointment"].ToString()),
                            designation = (dr_datarow["designation"].ToString()),
                            dsc_expiry_date = (dr_datarow["dsc_expiry_date"].ToString()),
                            wheather_dsc_registered = (dr_datarow["wheather_dsc_registered"].ToString()),
                            DINDPINPAN = (dr_datarow["DINDPINPAN"].ToString()),
                            full_name = (dr_datarow["full_name"].ToString()),
                            address = (dr_datarow["address"].ToString()),
                        });
                    }
                    values.mcasignatorydetails_list = getmcasignatorydetails_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGSTAuthenticationViewDetails(string institution2branch_gid, string employee_gid, MdlGSTAuthenticationDetails values)
        {
            try
            {

                msSQL = " select CAST(response as char) as response" +
                        " from ocs_trn_tgspinauthentication where function_gid='" + institution2branch_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    values = JsonConvert.DeserializeObject<MdlGSTAuthenticationDetails>(objODBCDatareader["response"].ToString());

                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();

            }

            catch (Exception ex)
            {
                values.status = false;
                values.message = "failure";
            }


        }



        public void DaGSTVerificationViewDetails(string institution2branch_gid, string employee_gid, MdlGSTVerificationDetails values)
        {
            try
            {

                msSQL = " select CAST(response as char) as response" +
                        " from ocs_trn_tgspinverification where function_gid='" + institution2branch_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    values = JsonConvert.DeserializeObject<MdlGSTVerificationDetails>(objODBCDatareader["response"].ToString());

                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();

            }

            catch (Exception ex)
            {
                values.status = false;
                values.message = "failure";
            }


        }
        public void DaGSTReturnFillingViewDetails(string institution2branch_gid, string employee_gid, MdlGSPGSTReturnFilingDetails values)
        {
            try
            {

                msSQL = " select CAST(response as char) as response" +
                        " from ocs_trn_tgstreturnfilling where function_gid='" + institution2branch_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    values = JsonConvert.DeserializeObject<MdlGSPGSTReturnFilingDetails>(objODBCDatareader["response"].ToString());

                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();

            }

            catch (Exception ex)
            {
                values.status = false;
                values.message = "failure";
            }


        }


        public void DaIECProfileViewDetails(string iecdtl_gid, string employee_gid, MdlIECProfileDetails values)
        {
            try
            {

                msSQL = " select CAST(response as char) as response" +
                        " from ocs_trn_tiecdtl where iecdtl_gid='" + iecdtl_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    values = JsonConvert.DeserializeObject<MdlIECProfileDetails>(objODBCDatareader["response"].ToString());

                }
                values.status = true;
                values.message = "success"; 
                objODBCDatareader.Close();

            }

            catch (Exception ex)
            {
                values.status = false;
                values.message = "failure";
            }


        }

        public void DaPostLPGID(MdlLPGID values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("LPIA");

            msSQL = " update ocs_trn_tlpgiddtl set " +
                " lpgiddtl_gid='" + msGetGid + "',";
            if (values.remarks == null || values.remarks == "" || values.remarks == "undefined")
            {
                msSQL += " remarks='',";
            }
            else
            {
                msSQL += " remarks='" + values.remarks.Replace("'", "") + "',";
            }
            msSQL += " updated_by='" + employee_gid + "'," +
                          " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                          " where lpgiddtl_gid='" + employee_gid + "' ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "LPG ID Added Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while adding";
                values.status = true;
            }

        }

        public void DaGetLPGIDList(string function_gid, string employee_gid, MdlLPGID values)
        {
            msSQL = " select lpgiddtl_gid,remarks,lpg_id,validation_status" +
                    " from ocs_trn_tlpgiddtl" +
                    " where lpgiddtl_gid not in ('" + employee_gid + "') and function_gid='" + function_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getLPGID_list = new List<LPGID_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getLPGID_list.Add(new LPGID_list
                    {
                        lpgiddtl_gid = dt["lpgiddtl_gid"].ToString(),
                        remarks = dt["remarks"].ToString(),
                        lpg_id = dt["lpg_id"].ToString(),                      
                        validation_status = dt["validation_status"].ToString(),
                    });

                }
            }
            values.LPGID_list = getLPGID_list;
            dt_datatable.Dispose();
        }

        public void DaLPGIDdelete(string lpgiddtl_gid, MdlLPGID values)
        {
            msSQL = "delete from ocs_trn_tlpgiddtl where lpgiddtl_gid='" + lpgiddtl_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "LPG ID Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while deleting";
                values.status = false;

            }
        }

        public void DaLPGIDViewDetails(string lpgiddtl_gid, string employee_gid, MdlLPGIDAuthenticationDetails values)
        {
            try
            {

                msSQL = " select CAST(response as char) as response" +
                        " from ocs_trn_tlpgiddtl where lpgiddtl_gid='" + lpgiddtl_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    values = JsonConvert.DeserializeObject<MdlLPGIDAuthenticationDetails>(objODBCDatareader["response"].ToString());

                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();

            }

            catch (Exception ex)
            {
                values.status = false;
                values.message = "failure";
            }


        }

        //SHOP AND ESTABLISHMENT
        public void DaPostShop(MdlShop values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("SNES");

            msSQL = " update ocs_trn_tshopandestablishment set " +
                " shopandestablishment_gid='" + msGetGid + "',";
            if (values.remarks == null || values.remarks == "" || values.remarks == "undefined")
            {
                msSQL += " remarks='',";
            }
            else
            {
                msSQL += " remarks='" + values.remarks.Replace("'", "") + "',";
            }
            msSQL += " updated_by='" + employee_gid + "'," +
                          " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                          " where shopandestablishment_gid='" + employee_gid + "' ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Shop Added Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while adding";
                values.status = true;
            }

        }
        public void DaGetShopList(string function_gid, string employee_gid, MdlShop values)
        {
            msSQL = " select shopandestablishment_gid,remarks,regNo,validation_status" +
                    " from ocs_trn_tshopandestablishment a" +
                    " where  shopandestablishment_gid not in ('" + employee_gid + "') and function_gid='" + function_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getshop_list = new List<shop_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getshop_list.Add(new shop_list
                    {
                        shopandestablishment_gid = dt["shopandestablishment_gid"].ToString(),
                        remarks = dt["remarks"].ToString(),
                        regNo = dt["regNo"].ToString(),                      
                        validation_status = dt["validation_status"].ToString(),
                    });

                }
            }
            values.shop_list = getshop_list;
            dt_datatable.Dispose();
        }

        public void DaShopdelete(string shopandestablishment_gid, MdlShop values)
        {
            msSQL = "delete from ocs_trn_tshopandestablishment where shopandestablishment_gid='" + shopandestablishment_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Shop Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while deleting";
                values.status = false;

            }
        }

        public void DaShopandEstablishmentViewDetails(string shopandestablishment_gid, string employee_gid, MdlShopAndEstablishmentDetails values)
        {
            try
            {

                msSQL = " select CAST(response as char) as response" +
                        " from ocs_trn_tshopandestablishment where shopandestablishment_gid='" + shopandestablishment_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    values = JsonConvert.DeserializeObject<MdlShopAndEstablishmentDetails>(objODBCDatareader["response"].ToString());
                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();

            }

            catch (Exception ex)
            {
                values.status = false;
                values.message = "failure";
            }

        }

        public void DaPostRCAuthAdvanced(MdlRCAuthAdvanced values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("VRAA");

            msSQL = " update ocs_trn_tvehiclercauthadvanced set " +
                " vehiclercauthadvanced_gid='" + msGetGid + "',";
            if (values.remarks == null || values.remarks == "" || values.remarks == "undefined")
            {
                msSQL += " remarks='',";
            }
            else
            {
                msSQL += " remarks='" + values.remarks.Replace("'", "") + "',";
            }
            msSQL += " updated_by='" + employee_gid + "'," +
                          " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                          " where vehiclercauthadvanced_gid='" + employee_gid + "' ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "RC Authentication Advanced Detail Added Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while adding";
                values.status = true;
            }

        }

        public void DaGetRCAuthAdvancedList(string function_gid, string employee_gid, MdlRCAuthAdvanced values)
        {
            msSQL = " select vehiclercauthadvanced_gid,remarks,registrationNumber,validation_status" +
                    " from ocs_trn_tvehiclercauthadvanced" +
                    " where vehiclercauthadvanced_gid not in ('" + employee_gid + "') and function_gid='" + function_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getRCAuthAdvanced_list = new List<RCAuthAdvanced_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getRCAuthAdvanced_list.Add(new RCAuthAdvanced_list
                    {
                        vehiclercauthadvanced_gid = dt["vehiclercauthadvanced_gid"].ToString(),
                        remarks = dt["remarks"].ToString(),
                        registrationNumber = dt["registrationNumber"].ToString(),
                        validation_status = dt["validation_status"].ToString(),
                    });

                }
            }
            values.RCAuthAdvanced_list = getRCAuthAdvanced_list;
            dt_datatable.Dispose();
        }

        public void DaRCAuthAdvanceddelete(string vehiclercauthadvanced_gid, MdlRCAuthAdvanced values)
        {
            msSQL = "delete from ocs_trn_tvehiclercauthadvanced where vehiclercauthadvanced_gid='" + vehiclercauthadvanced_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "RC Authentication Advanced Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while deleting";
                values.status = false;

            }
        }

        public void DaRCAuthAdvancedViewDetails(string vehiclercauthadvanced_gid, string employee_gid, MdlVehicleRCAuthAdvancedDetails values)
        {
            try
            {

                msSQL = " select CAST(response as char) as response" +
                        " from ocs_trn_tvehiclercauthadvanced where vehiclercauthadvanced_gid='" + vehiclercauthadvanced_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    values = JsonConvert.DeserializeObject<MdlVehicleRCAuthAdvancedDetails>(objODBCDatareader["response"].ToString());

                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();

            }

            catch (Exception ex)
            {
                values.status = false;
                values.message = "failure";
            }


        }

        //Vehicle RC Search

        public void DaPostRCSearch(MdlRCSearch values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("VRCS");

            msSQL = " update ocs_trn_tvehiclercsearch set " +
                " vehiclercsearch_gid='" + msGetGid + "',";
            if (values.remarks == null || values.remarks == "" || values.remarks == "undefined")
            {
                msSQL += " remarks='',";
            }
            else
            {
                msSQL += " remarks='" + values.remarks.Replace("'", "") + "',";
            }
            msSQL += " updated_by='" + employee_gid + "'," +
                          " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                          " where vehiclercsearch_gid='" + employee_gid + "' ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "RC Search Detail Added Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while adding";
                values.status = true;
            }

        }

        public void DaGetRCSearchList(string function_gid, string employee_gid, MdlRCSearch values)
        {
            msSQL = " select vehiclercsearch_gid,remarks,engine_no,validation_status" +
                    " from ocs_trn_tvehiclercsearch" +
                    " where vehiclercsearch_gid not in ('" + employee_gid + "') and function_gid='" + function_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getRCSearch_list = new List<RCSearch_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getRCSearch_list.Add(new RCSearch_list
                    {
                        vehiclercsearch_gid = dt["vehiclercsearch_gid"].ToString(),
                        remarks = dt["remarks"].ToString(),
                        engine_no = dt["engine_no"].ToString(),
                        validation_status = dt["validation_status"].ToString(),
                    });

                }
            }
            values.RCSearch_list = getRCSearch_list;
            dt_datatable.Dispose();
        }

        public void DaRCSearchdelete(string vehiclercsearch_gid, MdlRCSearch values)
        {
            msSQL = "delete from ocs_trn_tvehiclercsearch where vehiclercsearch_gid='" + vehiclercsearch_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "RC Search Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while deleting";
                values.status = false;

            }
        }

        public void DaRCSearchViewDetails(string vehiclercsearch_gid, string employee_gid, MdlVehicleRCSearchDetails values)
        {
            try
            {

                msSQL = " select CAST(response as char) as response" +
                        " from ocs_trn_tvehiclercsearch where vehiclercsearch_gid='" + vehiclercsearch_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    values = JsonConvert.DeserializeObject<MdlVehicleRCSearchDetails>(objODBCDatareader["response"].ToString());

                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();

            }

            catch (Exception ex)
            {
                values.status = false;
                values.message = "failure";
            }


        }


        //Property Tax

        public void DaPostPropertyTax(MdlPropertyTax values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("PRTX");

            msSQL = " update ocs_trn_tpropertytax set " +
                " propertytax_gid='" + msGetGid + "',";
            if (values.remarks == null || values.remarks == "" || values.remarks == "undefined")
            {
                msSQL += " remarks='',";
            }
            else
            {
                msSQL += " remarks='" + values.remarks.Replace("'", "") + "',";
            }
            msSQL += " updated_by='" + employee_gid + "'," +
                          " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                          " where propertytax_gid='" + employee_gid + "' ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Property Tax Added Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while adding";
                values.status = true;
            }

        }

        public void DaGetPropertyTaxList(string function_gid, string employee_gid, MdlPropertyTax values)
        {
            msSQL = " select propertytax_gid,remarks,propertyNo,validation_status" +
                    " from ocs_trn_tpropertytax" +
                    " where propertytax_gid not in ('" + employee_gid + "') and function_gid='" + function_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getPropertyTax_list = new List<PropertyTax_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getPropertyTax_list.Add(new PropertyTax_list
                    {
                        propertytax_gid = dt["propertytax_gid"].ToString(),
                        remarks = dt["remarks"].ToString(),
                        propertyNo = dt["propertyNo"].ToString(),
                        validation_status = dt["validation_status"].ToString(),
                    });

                }
            }
            values.PropertyTax_list = getPropertyTax_list;
            dt_datatable.Dispose();
        }

        public void DaPropertyTaxdelete(string propertytax_gid, MdlPropertyTax values)
        {
            msSQL = "delete from ocs_trn_tpropertytax where propertytax_gid='" + propertytax_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Property Tax Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while deleting";
                values.status = false;

            }
        }

        public void DaPropertyTaxViewDetails(string propertytax_gid, string employee_gid, MdlPropertyTaxDetails values)
        {
            try
            {

                msSQL = " select CAST(response as char) as response" +
                        " from ocs_trn_tpropertytax where propertytax_gid='" + propertytax_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    values = JsonConvert.DeserializeObject<MdlPropertyTaxDetails>(objODBCDatareader["response"].ToString());

                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();

            }

            catch (Exception ex)
            {
                values.status = false;
                values.message = "failure";
            }

        }

        //Application KYC API Lists

        public void DaAppnTANList(string application_gid, string employee_gid, MdlTAN values)
        {
            msSQL = " select a.tan, a.name, a.validation_status, a.remarks," +        
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date  from ocs_trn_ttandtl a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid  where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gettan_list = new List<tan_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    gettan_list.Add(new tan_list
                    {                                              
                        tan_no = dt["tan"].ToString(),
                        name = dt["name"].ToString(),
                        validation_status = dt["validation_status"].ToString(),
                        remarks = dt["remarks"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString()                                     
                    });

                }
            }
            values.tan_list = gettan_list;
            dt_datatable.Dispose();
        }

        public void DaAppnCompanyLLPList(string application_gid, string employee_gid, MdlCIN values)
        {
            msSQL = " select a.companyllpno_gid,a.cin_no,a.remarks,a.validation_status,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date from ocs_trn_tcompanyllpno a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                     " left join adm_mst_tuser c on c.user_gid=b.user_gid  where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcin_list = new List<cin_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcin_list.Add(new cin_list
                    {
                        companyllpno_gid = dt["companyllpno_gid"].ToString(),
                        cin_no = dt["cin_no"].ToString(),
                        validation_status = dt["validation_status"].ToString(),
                        remarks = dt["remarks"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),                       
                    });

                }
            }
            values.cin_list = getcin_list;
            dt_datatable.Dispose();
        }

        public void DaAppnMCASignatureList(string application_gid, string employee_gid, MdlCIN values)
        {
            msSQL = " select a.mcasignatories_gid,a.remarks,a.cin,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, a.validation_status from ocs_trn_mcasignatories a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid  where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcin_list = new List<cin_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcin_list.Add(new cin_list
                    {
                        mcasignatories_gid = dt["mcasignatories_gid"].ToString(),
                        remarks = dt["remarks"].ToString(),
                        cin_no = dt["cin"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        validation_status = dt["validation_status"].ToString(),
                    });

                }
            }
            values.cin_list = getcin_list;
            dt_datatable.Dispose();
        }

        public void DaAppnIECDetailedList(string application_gid, string employee_gid, MdlIECDetailed values)
        {
            msSQL = " select a.iecdtl_gid,a.remarks,a.iec_no,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, a.validation_status from ocs_trn_tiecdtl a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                     " left join adm_mst_tuser c on c.user_gid=b.user_gid where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getIECDetailed_list = new List<IECDetailed_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getIECDetailed_list.Add(new IECDetailed_list
                    {
                        iecdtl_gid = dt["iecdtl_gid"].ToString(),
                        remarks = dt["remarks"].ToString(),
                        iec_no = dt["iec_no"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        validation_status = dt["validation_status"].ToString(),
                    });

                }
            }
            values.IECDetailed_list = getIECDetailed_list;
            dt_datatable.Dispose();
        }

        public void DaAppnFSSAIList(string application_gid, string employee_gid, MdlFSSAI values)
        {
            msSQL = " select a.fssailicenseauthentication_gid,a.remarks,a.reg_no,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, a.validation_status from ocs_trn_tfssailicenseauthentication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid  where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getfssai_list = new List<fssai_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getfssai_list.Add(new fssai_list
                    {
                        fssailicenseauthentication_gid = dt["fssailicenseauthentication_gid"].ToString(),
                        remarks = dt["remarks"].ToString(),
                        reg_no = dt["reg_no"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        validation_status = dt["validation_status"].ToString(),
                    });

                }
            }
            values.fssai_list = getfssai_list;
            dt_datatable.Dispose();
        }

        public void DaAppnFDAList(string application_gid, string employee_gid, MdlFDA values)
        {
            msSQL = " select a.fdalicenseauthentication_gid,a.remarks,a.license_no,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, a.validation_status from ocs_trn_tfdalicenseauthentication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid  where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getfda_list = new List<fda_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getfda_list.Add(new fda_list
                    {
                        fdalicenseauthentication_gid = dt["fdalicenseauthentication_gid"].ToString(),
                        remarks = dt["remarks"].ToString(),
                        license_no = dt["license_no"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        validation_status = dt["validation_status"].ToString(),
                    });

                }
            }
            values.fda_list = getfda_list;
            dt_datatable.Dispose();
        }

        public void DaAppnGSTVerificationList(string application_gid, string employee_gid, MdlGST values)
        {
            msSQL = " select a.function_gid,a.remarks,a.gst_no,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,a.validation_status from ocs_trn_tgspinverification a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid  where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getgst_list = new List<gst_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getgst_list.Add(new gst_list
                    {
                        function_gid = dt["function_gid"].ToString(),
                        remarks = dt["remarks"].ToString(),
                        gst_no = dt["gst_no"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        validation_status = dt["validation_status"].ToString(),
                    });

                }
            }
            values.gst_list = getgst_list;
            dt_datatable.Dispose();
        }

        public void DaAppnGSTReturnFilingList(string application_gid, string employee_gid, MdlGST values)
        {
            msSQL = " select a.function_gid,a.remarks,a.gst_no,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,a.validation_status from ocs_trn_tgstreturnfilling a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid  where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getgst_list = new List<gst_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getgst_list.Add(new gst_list
                    {
                        function_gid = dt["function_gid"].ToString(),
                        remarks = dt["remarks"].ToString(),
                        gst_no = dt["gst_no"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        validation_status = dt["validation_status"].ToString(),
                    });

                }
            }
            values.gst_list = getgst_list;
            dt_datatable.Dispose();
        }

        public void DaAppnGSTAuthenticationList(string application_gid, string employee_gid, MdlGST values)
        {
            msSQL = " select a.function_gid,a.remarks,a.gst_no,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,a.validation_status from ocs_trn_tgspinauthentication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid  where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getgst_list = new List<gst_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getgst_list.Add(new gst_list
                    {
                        function_gid = dt["function_gid"].ToString(),
                        remarks = dt["remarks"].ToString(),
                        gst_no = dt["gst_no"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        validation_status = dt["validation_status"].ToString(),
                    });

                }
            }
            values.gst_list = getgst_list;
            dt_datatable.Dispose();
        }

        public void DaAppnLPGIDAuthenticationList(string application_gid, string employee_gid, MdlLPGID values)
        {
            msSQL = " select a.lpgiddtl_gid,a.remarks,a.lpg_id,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,a.validation_status from ocs_trn_tlpgiddtl a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid  where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getLPGID_list = new List<LPGID_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getLPGID_list.Add(new LPGID_list
                    {
                        lpgiddtl_gid = dt["lpgiddtl_gid"].ToString(),
                        remarks = dt["remarks"].ToString(),
                        lpg_id = dt["lpg_id"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        validation_status = dt["validation_status"].ToString(),
                    });

                }
            }
            values.LPGID_list = getLPGID_list;
            dt_datatable.Dispose();
        }

        public void DaAppnShopList(string application_gid, string employee_gid, MdlShop values)
        {
            msSQL = " select a.shopandestablishment_gid,a.remarks,a.regNo,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                   " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,a.validation_status from ocs_trn_tshopandestablishment a" +
                   " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                   " left join adm_mst_tuser c on c.user_gid=b.user_gid  where application_gid='" + application_gid + "'";

           
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getshop_list = new List<shop_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getshop_list.Add(new shop_list
                    {
                        shopandestablishment_gid = dt["shopandestablishment_gid"].ToString(),
                        remarks = dt["remarks"].ToString(),
                        regNo = dt["regNo"].ToString(),
                        validation_status = dt["validation_status"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                    });

                }
            }
            values.shop_list = getshop_list;
            dt_datatable.Dispose();
        }

        public void DaAppnRCAuthAdvancedList(string application_gid, string employee_gid, MdlRCAuthAdvanced values)
        {


            msSQL = " select a.vehiclercauthadvanced_gid,a.remarks,a.registrationNumber,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                  " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,a.validation_status from ocs_trn_tvehiclercauthadvanced a" +
                  " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                  " left join adm_mst_tuser c on c.user_gid=b.user_gid  where application_gid='" + application_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getRCAuthAdvanced_list = new List<RCAuthAdvanced_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getRCAuthAdvanced_list.Add(new RCAuthAdvanced_list
                    {
                        vehiclercauthadvanced_gid = dt["vehiclercauthadvanced_gid"].ToString(),
                        remarks = dt["remarks"].ToString(),
                        registrationNumber = dt["registrationNumber"].ToString(),
                        validation_status = dt["validation_status"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                    });

                }
            }
            values.RCAuthAdvanced_list = getRCAuthAdvanced_list;
            dt_datatable.Dispose();
        }

        public void DaAppnRCSearchList(string application_gid, string employee_gid, MdlRCSearch values)
        {        
            msSQL = " select a.vehiclercsearch_gid,a.remarks,a.engine_no,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                  " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,a.validation_status from ocs_trn_tvehiclercsearch a" +
                  " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                  " left join adm_mst_tuser c on c.user_gid=b.user_gid  where application_gid='" + application_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getRCSearch_list = new List<RCSearch_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getRCSearch_list.Add(new RCSearch_list
                    {
                        vehiclercsearch_gid = dt["vehiclercsearch_gid"].ToString(),
                        remarks = dt["remarks"].ToString(),
                        engine_no = dt["engine_no"].ToString(),
                        validation_status = dt["validation_status"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                    });

                }
            }
            values.RCSearch_list = getRCSearch_list;
            dt_datatable.Dispose();
        }

        public void DaAppnPropertyTaxList(string application_gid, string employee_gid, MdlPropertyTax values)
        {
            msSQL = " select a.propertytax_gid,a.remarks,a.propertyNo,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,a.validation_status from ocs_trn_tpropertytax a" +
                " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                " left join adm_mst_tuser c on c.user_gid=b.user_gid  where application_gid='" + application_gid + "'";
        
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getPropertyTax_list = new List<PropertyTax_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getPropertyTax_list.Add(new PropertyTax_list
                    {
                        propertytax_gid = dt["propertytax_gid"].ToString(),
                        remarks = dt["remarks"].ToString(),
                        propertyNo = dt["propertyNo"].ToString(),
                        validation_status = dt["validation_status"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                    });

                }
            }
            values.PropertyTax_list = getPropertyTax_list;
            dt_datatable.Dispose();
        }



    }
}