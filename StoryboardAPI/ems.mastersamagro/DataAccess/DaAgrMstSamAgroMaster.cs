using ems.mastersamagro.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Web;
using ems.storage.Functions;

namespace ems.mastersamagro.DataAccess
{
    /// <summary>
    /// This DataAccess will provide access to various masters in samagro and their summary, add, edit, view, active, inactive & delete records (Samagro Master includes Scope, Other creditor Applicant type, Milestone Payment, Sector classification, type of warehouse, Buyer/Supplier Type, Product desk mapping & Insurane company).
    /// </summary>
    /// <remarks>Written by Sherin Augusta, Kalaiarsan, Praveen Raj.R</remarks>
    public class DaAgrMstSamAgroMaster
    {
        dbconn objdbconn = new dbconn();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        HttpPostedFile httpPostedFile;
        string msSQL, msGetGid, msGetAPICode;
        int mnResult;
        string lsmaster_value;
        string lslms_code, lsbureau_code;

        public string productdesk_lms, productdesk_bureau;
        public string msGetproductdeskmember_gid, msGetproductdeskmanager_gid, lsmappinggid, lsgroupid;

        string lsinsurancecompany2policylog_gid, lsinsurancecompany2policy_gid, lsinsurancecompany_gid, lspolicy_name, lspolicy_number, lspolicy_amount,
                lspolicyperiod_from, lspolicyperiod_to, lspremium_amount, lspremiumpayment_status, lspaid_date;

        string lsinsurancecompany_name, lsstatus, lsremarks;


        // Product Desk Mapping ---- STARTING

        public void DaPostProductDeskAdd(MdlProductDesk values, string employee_gid)
        {
            msSQL = "select productdesk_name from agr_mst_tproductdeskmapping where productdesk_name = '" + values.productdesk_name.Replace("'", "\\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Product Desk Name Already Exist";
            }
            else
            {
                msGetAPICode = objcmnfunctions.GetApiMasterGID("PDAC");
                msGetGid = objcmnfunctions.GetMasterGID("PDCR");
                lsgroupid = objcmnfunctions.GetMasterGID("PDCI");
                msSQL = " insert into agr_mst_tproductdeskmapping(" +
                        " productdesk_gid ," +
                        " productdesk_id," +
                        " api_code," +
                        " products_gid," +
                        " products_name," +
                        " productdesk_name," +
                        " productdesk_lms," +
                        " productdesk_bureau," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + lsgroupid + "'," +
                        "'" + msGetAPICode + "'," +
                        "'" + values.products_gid + "'," +
                        "'" + values.products_name.Replace("'", "\\'") + "'," +
                        "'" + values.productdesk_name.Replace("'", "\\'") + "'," +
                        "'" + values.productdesk_lms + "'," +
                        "'" + values.productdesk_bureau + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                for (var i = 0; i < values.ProductDeskMember.Count; i++)
                {
                    msGetproductdeskmember_gid = objcmnfunctions.GetMasterGID("PMEI");

                    msSQL = "Insert into agr_mst_tproductdeskmapping_member ( " +
                           " productdeskmember_gid, " +
                           " productdesk_gid," +
                           " employee_gid," +
                           " employee_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetproductdeskmember_gid + "'," +
                           "'" + msGetGid + "'," +
                           "'" + values.ProductDeskMember[i].employee_gid + "'," +
                           "'" + values.ProductDeskMember[i].employee_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                for (var i = 0; i < values.ProductDeskManager.Count; i++)
                {
                    msGetproductdeskmanager_gid = objcmnfunctions.GetMasterGID("PMAI");

                    msSQL = "Insert into agr_mst_tproductdeskmapping_manager( " +
                           " productdeskmanager_gid, " +
                           " productdesk_gid," +
                           " employee_gid," +
                           " employee_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetproductdeskmanager_gid + "'," +
                           "'" + msGetGid + "'," +
                           "'" + values.ProductDeskManager[i].employee_gid + "'," +
                           "'" + values.ProductDeskManager[i].employee_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Product Desk Added successfully";
                }
                else
                {
                    values.message = "Error Occured while Adding";
                    values.status = false;
                }
            }
            objODBCDatareader.Close();
        }

        public void DaGetProductDeskSummary(MdlProductDesk objmaster)
        {
            try
            {
                msSQL = " SELECT a.productdesk_id,a.productdesk_gid , a.products_gid, a.products_name, a.productdesk_name,a.productdesk_lms, a.productdesk_bureau, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,api_code," +
                        " case when a.productdesk_status='N' then 'Inactive' else 'Active' end as productdesk_status" +
                        " FROM agr_mst_tproductdeskmapping a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getproductdesk_list = new List<ProductDesk>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getproductdesk_list.Add(new ProductDesk
                        {
                            productdesk_id = (dr_datarow["productdesk_id"].ToString()),
                            productdesk_gid = (dr_datarow["productdesk_gid"].ToString()),
                            products_gid = (dr_datarow["products_gid"].ToString()),
                            products_name = (dr_datarow["products_name"].ToString()),
                            productdesk_name = (dr_datarow["productdesk_name"].ToString()),
                            productdesk_lms = (dr_datarow["productdesk_lms"].ToString()),
                            productdesk_bureau = (dr_datarow["productdesk_bureau"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            productdesk_status = (dr_datarow["productdesk_status"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                        });
                    }
                    objmaster.ProductDesk = getproductdesk_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch (Exception ex)
            {
                objmaster.status = false;
            }
        }

        public void DaGetProductsNameSummary(MdlProductDesk objmaster)
        {
            try
            {
                msSQL = " SELECT  loanproduct_gid, loanproduct_name FROM agr_mst_tloanproduct a" +
                        " where status = 'Y' order by loanproduct_gid desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getproductdesk_list = new List<Products_Name>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getproductdesk_list.Add(new Products_Name
                        {
                            products_gid = (dr_datarow["loanproduct_gid"].ToString()),
                            products_name = (dr_datarow["loanproduct_name"].ToString())
                        });
                    }
                    objmaster.Products_Name = getproductdesk_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch (Exception ex)
            {
                objmaster.status = false;
            }
        }


        public void DaGetProductDeskEdit(string productdesk_gid, MdlProductDesk objmaster)
        {
            msSQL = " select productdesk_gid,productdesk_name, products_gid, products_name, productdesk_id, productdesk_lms, productdesk_bureau, productdesk_status from agr_mst_tproductdeskmapping " +
                    " where productdesk_gid='" + productdesk_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objmaster.productdesk_gid = objODBCDatareader["productdesk_gid"].ToString();
                objmaster.productdesk_name = objODBCDatareader["productdesk_name"].ToString();
                objmaster.products_gid = objODBCDatareader["products_gid"].ToString();
                objmaster.products_name = objODBCDatareader["products_name"].ToString();
                objmaster.productdesk_id = objODBCDatareader["productdesk_id"].ToString();
                objmaster.productdesk_lms = objODBCDatareader["productdesk_lms"].ToString();
                objmaster.productdesk_bureau = objODBCDatareader["productdesk_bureau"].ToString();
                objmaster.productdesk_status = objODBCDatareader["productdesk_status"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select productdeskmember_gid,employee_gid,employee_name from agr_mst_tproductdeskmapping_member " +
                  " where productdesk_gid='" + productdesk_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getproductdeskmemberList = new List<ProductDeskMember>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getproductdeskmemberList.Add(new ProductDeskMember
                    {
                        msGetproductdeskmember_gid = dt["productdeskmember_gid"].ToString(),
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_name = dt["employee_name"].ToString(),
                    });
                }
                objmaster.ProductDeskMember = getproductdeskmemberList;
            }
            dt_datatable.Dispose();

            msSQL = " select productdeskmanager_gid,employee_gid,employee_name from agr_mst_tproductdeskmapping_manager " +
                " where productdesk_gid='" + productdesk_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getproductdeskmanagerList = new List<ProductDeskManager>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getproductdeskmanagerList.Add(new ProductDeskManager
                    {
                        msGetproductdeskmanager_gid = dt["productdeskmanager_gid"].ToString(),
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_name = dt["employee_name"].ToString(),
                    });
                }
                objmaster.ProductDeskManager = getproductdeskmanagerList;
            }
            dt_datatable.Dispose();


            msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
               " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
               " where user_status<>'N' order by a.user_firstname asc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_productdeskmember = new List<ProductDeskMemberem_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                objmaster.ProductDeskMemberem_list = dt_datatable.AsEnumerable().Select(row =>
                  new ProductDeskMemberem_list
                  {
                      employee_gid = row["employee_gid"].ToString(),
                      employee_name = row["employee_name"].ToString()
                  }
                ).ToList();
            }
            dt_datatable.Dispose();

            msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
               " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
               " where user_status<>'N' order by a.user_firstname asc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_productdeskmanager = new List<ProductDeskManagerem_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                objmaster.ProductDeskManagerem_list = dt_datatable.AsEnumerable().Select(row =>
                  new ProductDeskManagerem_list
                  {
                      employee_gid = row["employee_gid"].ToString(),
                      employee_name = row["employee_name"].ToString()
                  }
                ).ToList();
            }
            dt_datatable.Dispose();

        }

        public void DaGetProductDeskDetails(string productdesk_gid, ProductDeskDetails values)
        {
            msSQL = " select group_concat(employee_name) as employee_name  from agr_mst_tproductdeskmapping_member " +
                  " where productdesk_gid='" + productdesk_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.productdesk_member = objODBCDatareader["employee_name"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select group_concat(employee_name) as employee_name from agr_mst_tproductdeskmapping_manager " +
                " where productdesk_gid='" + productdesk_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.productdesk_manager = objODBCDatareader["employee_name"].ToString();
            }
            objODBCDatareader.Close();

        }

        public bool DaPostProductDeskUpdate(string employee_gid, MdlProductDesk values)
        {
            msSQL = "select productdesk_gid from agr_mst_tproductdeskmapping where productdesk_name = '" + values.productdesk_name.Replace("'", "\\'") + "'";
            lsmappinggid = objdbconn.GetExecuteScalar(msSQL);
            if (lsmappinggid != "")
            {
                if (lsmappinggid != values.productdesk_gid)
                {
                    values.status = false;
                    values.message = "Product Desk Name Already Exist";
                    return false;
                }
            }

            msSQL = "select updated_by, updated_date, productdesk_name from agr_mst_tproductdeskmapping where productdesk_gid ='" + values.productdesk_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("PDUL");
                    msSQL = " insert into agr_mst_tproductdeskmappingupdatelog(" +
                              " productdeskupdatelog_gid," +
                              " productdesk_gid," +
                              " products_gid," +
                              " products_name," +
                              " productdesk_name , " +
                              " productdesk_lms," +
                              " productdesk_bureau , " +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.productdesk_gid + "'," +
                              "'" + values.products_gid + "'," +
                              "'" + values.products_name.Replace("'", "") + "'," +
                              "'" + objODBCDatareader["productdesk_name"].ToString() + "'," +
                               "'" + values.productdesk_lms + "'," +
                                "'" + values.productdesk_bureau + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();

            msSQL = "update agr_mst_tproductdeskmapping set productdesk_name='" + values.productdesk_name.Replace("'", "") + "'," +
                    " products_gid='" + values.products_gid + "'," +
                    " products_name='" + values.products_name.Replace("'", "") + "'," +
                    " productdesk_lms='" + values.productdesk_lms + "'," +
                    " productdesk_bureau='" + values.productdesk_bureau + "'," +
                     " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where productdesk_gid='" + values.productdesk_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

      //  msSQL = "update agr_mst_tapplication set productdesk_name='" + values.productdesk_name.Replace("'", "") + "'" +
       //         " where productdesk_gid='" + values.productdesk_gid + "' ";
      //  mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from agr_mst_tproductdeskmapping_member where productdesk_gid ='" + values.productdesk_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                for (var i = 0; i < values.ProductDeskMember.Count; i++)
                {
                    msGetproductdeskmember_gid = objcmnfunctions.GetMasterGID("PUME");

                    msSQL = "Insert into agr_mst_tproductdeskmapping_member( " +
                           " productdeskmember_gid, " +
                           " productdesk_gid," +
                           " employee_gid," +
                           " employee_name," +
                           " updated_by," +
                           " updated_date)" +
                           " values(" +
                           "'" + msGetproductdeskmember_gid + "'," +
                           "'" + values.productdesk_gid + "'," +
                           "'" + values.ProductDeskMember[i].employee_gid + "'," +
                           "'" + values.ProductDeskMember[i].employee_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }

            msSQL = " delete from agr_mst_tproductdeskmapping_manager where productdesk_gid ='" + values.productdesk_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                for (var i = 0; i < values.ProductDeskManager.Count; i++)
                {
                    msGetproductdeskmanager_gid = objcmnfunctions.GetMasterGID("PUMA");

                    msSQL = "Insert into agr_mst_tproductdeskmapping_manager( " +
                           " productdeskmanager_gid, " +
                           " productdesk_gid," +
                           " employee_gid," +
                           " employee_name," +
                           " updated_by," +
                           " updated_date)" +
                           " values(" +
                           "'" + msGetproductdeskmanager_gid + "'," +
                           "'" + values.productdesk_gid + "'," +
                           "'" + values.ProductDeskManager[i].employee_gid + "'," +
                           "'" + values.ProductDeskManager[i].employee_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Product Desk updated successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating Product Desk";
                return false;
            }
        }

        public void DaPostProductDeskInactive(ProductDesk values, string employee_gid)
        {
            msSQL = " update agr_mst_tproductdeskmapping set productdesk_status ='" + values.productdesk_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "\\'") + "'" +
                    " where productdesk_gid='" + values.productdesk_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("PDIA");

                msSQL = " insert into agr_mst_tproductdeskmappinginactivelog (" +
                      " productdeskinactivelog_gid, " +
                      " productdesk_gid," +
                      " productdesk_name ," +
                      " productdesk_status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.productdesk_gid + "'," +
                      " '" + values.productdesk_name + "'," +
                      " '" + values.productdesk_status + "'," +
                      " '" + values.remarks.Replace("'", "\\'") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.productdesk_status == "N")
                {
                    values.status = true;
                    values.message = "Product Desk Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Product Desk Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Changing Status";
            }
        }

        public void DaGetProductDeskInactiveLogview(string productdesk_gid, MdlProductDesk values)
        {
            try
            {
                msSQL = " SELECT productdesk_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.productdesk_status='N' then 'Inactive' else 'Active' end as productdesk_status, a.remarks" +
                        " FROM agr_mst_tproductdeskmappinginactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where productdesk_gid ='" + productdesk_gid + "' order by a.productdeskinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getProductDesklog = new List<ProductDesklog>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getProductDesklog.Add(new ProductDesklog
                        {
                            productdesk_gid = (dr_datarow["productdesk_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            productdesk_status = (dr_datarow["productdesk_status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.ProductDesklog = getProductDesklog;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        // Product Desk Mapping ---- ENDING
        //typeofsupplynature
        public void DaGettypeofsupplynature(MdlAgrMstSamAgroMaster objagroapplication360)
        {
            try
            {
                msSQL = " SELECT a.typeofsupplynature_gid,a.typeofsupplynature_name,a.lms_code, a.bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,api_code," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM agr_mst_ttypeofsupplynature a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.typeofsupplynature_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getapplicationmst_list = new List<applicationmst_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getapplicationmst_list.Add(new applicationmst_list
                        {
                            typeofsupplynature_gid = (dr_datarow["typeofsupplynature_gid"].ToString()),
                            typeofsupplynature_name = (dr_datarow["typeofsupplynature_name"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            Status = (dr_datarow["status"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                        });
                    }
                    objagroapplication360.applicationmst_list = getapplicationmst_list;
                }
                dt_datatable.Dispose();
                objagroapplication360.status = true;
            }
            catch
            {
                objagroapplication360.status = false;
            }
        }

        public void DaCreatetypeofsupplynature(applicationmst values, string employee_gid)
        {
            if (values.lms_code == null || values.lms_code == "")
            {
                lslms_code = "";
            }
            else
            {
                lslms_code = values.lms_code.Replace("'", "");
            }
            if (values.bureau_code == null || values.bureau_code == "")
            {
                lsbureau_code = "";
            }
            else
            {
                lsbureau_code = values.bureau_code.Replace("'", "");
            }
            msGetAPICode = objcmnfunctions.GetApiMasterGID("TSAC");
            msGetGid = objcmnfunctions.GetMasterGID("TYSN");
            msSQL = " insert into agr_mst_ttypeofsupplynature(" +
                    " typeofsupplynature_gid," +
                    " typeofsupplynature_name," +
                    " api_code," +
                    " lms_code," +
                    " bureau_code," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.typeofsupplynature_name.Replace("'", "") + "'," +
                    "'" + msGetAPICode + "'," +
                    "'" + lslms_code + "'," +
                    "'" + lsbureau_code + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Type of Supply/Nature Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Adding";
            }
        }

        public void DaEdittypeofsupplynature(string typeofsupplynature_gid, applicationmst values)
        {
            try
            {
                msSQL = " SELECT typeofsupplynature_gid,typeofsupplynature_name,lms_code, bureau_code, status as Status FROM agr_mst_ttypeofsupplynature where typeofsupplynature_gid='" + typeofsupplynature_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.typeofsupplynature_gid = objODBCDatareader["typeofsupplynature_gid"].ToString();
                    values.typeofsupplynature_name = objODBCDatareader["typeofsupplynature_name"].ToString();
                    values.lms_code = objODBCDatareader["lms_code"].ToString();
                    values.bureau_code = objODBCDatareader["bureau_code"].ToString();
                    values.Status = objODBCDatareader["Status"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaUpdatetypeofsupplynature(string employee_gid, applicationmst values)
        {

            if (values.lms_code == null || values.lms_code == "")
            {
                lslms_code = "";
            }
            else
            {
                lslms_code = values.lms_code.Replace("'", "");
            }
            if (values.bureau_code == null || values.bureau_code == "")
            {
                lsbureau_code = "";
            }
            else
            {
                lsbureau_code = values.bureau_code.Replace("'", "");
            }

            msSQL = " update agr_mst_ttypeofsupplynature set " +
                 " typeofsupplynature_name='" + values.typeofsupplynature_name.Replace("'", "") + "'," +
                 " lms_code='" + lslms_code + "'," +
                 " bureau_code='" + lsbureau_code + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where typeofsupplynature_gid='" + values.typeofsupplynature_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("TUSN");

                msSQL = " insert into agr_mst_ttypeofsupplynaturelog (" +
                       " typeofsupplynature_LOGgid, " +
                       " typeofsupplynature_gid, " +
                       " typeofsupplynature_name," +
                       " updated_by," +
                       " updated_date) " +
                       " values (" +
                       " '" + msGetGid + "'," +
                       " '" + values.typeofsupplynature_gid + "'," +
                       " '" + values.typeofsupplynature_name.Replace("'", "") + "'," +
                       " '" + employee_gid + "'," +
                       " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Type of Supply/Nature Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }

        public void DaInactivetypeofsupplynature(applicationmst values, string employee_gid)
        {
            msSQL = " update agr_mst_ttypeofsupplynature set status='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where typeofsupplynature_gid='" + values.typeofsupplynature_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("TISU");

                msSQL = " insert into agr_mst_ttypeofsupplynatureinactivelog (" +
                      " typeofsupplynatureinactivelog_gid, " +
                      " typeofsupplynature_gid," +
                      " typeofsupplynature_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.typeofsupplynature_gid + "'," +
                      " '" + values.typeofsupplynature_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Type of Supply/Nature Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Type of Supply/Nature Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaDeletetypeofsupplynature(string typeofsupplynature_gid, string employee_gid, result values)
        {
            msSQL = " select typeofsupplynature_name from agr_mst_ttypeofsupplynature where typeofsupplynature_gid='" + typeofsupplynature_gid + "'";
            lsmaster_value = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " delete from agr_mst_ttypeofsupplynature where typeofsupplynature_gid='" + typeofsupplynature_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "typeofsupplynature Deleted Successfully..!";
                msGetGid = objcmnfunctions.GetMasterGID("MSTD");
                msSQL = " insert into agr_mst_tmasterdeletelog(" +
                         "master_gid, " +
                         "master_name, " +
                         "master_value, " +
                         "deleted_by, " +
                         "deleted_date) " +
                         " values(" +
                         "'" + msGetGid + "'," +
                         "'Type of Supply Nature'," +
                         "'" + lsmaster_value + "'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while Delete..!";
            }
        }

        public void DatypeofsupplynatureInactiveLogview(string typeofsupplynature_gid, MdlAgrMstSamAgroMaster values)
        {
            try
            {
                msSQL = " SELECT a.typeofsupplynature_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM agr_mst_ttypeofsupplynatureinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.typeofsupplynature_gid ='" + typeofsupplynature_gid + "' order by a.typeofsupplynatureinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getapplicationmst_list = new List<applicationmst_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getapplicationmst_list.Add(new applicationmst_list
                        {
                            typeofsupplynature_gid = (dr_datarow["typeofsupplynature_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            Status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.applicationmst_list = getapplicationmst_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void InactivetypeofsupplynatureHistory(ApplicationInactiveHistory objapplicationhistory, string typeofsupplynature_gid)
        {
            try
            {
                msSQL = " select a.remarks, date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                " from agr_mst_ttypeofsupplynatureinactivelog a " +
                " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                " where a.typeofsupplynature_gid='" + typeofsupplynature_gid + "' order by a.typeofsupplynatureinactivelog_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getinactivehistory_list = new List<inactivehistory_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getinactivehistory_list.Add(new inactivehistory_list
                        {
                            status = (dr_datarow["status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString())
                        });
                    }
                    objapplicationhistory.inactivehistory_list = getinactivehistory_list;
                }
                dt_datatable.Dispose();
                objapplicationhistory.status = true;
            }
            catch
            {
                objapplicationhistory.status = false;
            }
        }

        public void DatypeofsupplynatureList(MdlAgrMstSamAgroMaster values)
        {


            msSQL = " select typeofsupplynature_gid, typeofsupplynature_name from agr_mst_ttypeofsupplynature " +
                    " order by typeofsupplynature_gid_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_typeofsupplynature = new List<applicationmst_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_typeofsupplynature.Add(new applicationmst_list
                    {

                        typeofsupplynature_gid = dt[" typeofsupplynature_gid"].ToString(),
                        typeofsupplynature_name = dt["typeofsupplynature_name"].ToString(),
                    });
                    values.applicationmst_list = get_typeofsupplynature;
                }
            }
            dt_datatable.Dispose();
        }

        //sectorclassification
        public void DaGetsectorclassification(MdlAgrMstSamAgroMaster objagroapplication360)
        {
            try
            {
                msSQL = " SELECT a.sectorclassification_gid,a.sectorclassification_name,a.lms_code, a.bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,api_code," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM agr_mst_tsectorclassification a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.sectorclassification_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getapplicationmst_list = new List<applicationmst_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getapplicationmst_list.Add(new applicationmst_list
                        {
                            sectorclassification_gid = (dr_datarow["sectorclassification_gid"].ToString()),
                            sectorclassification_name = (dr_datarow["sectorclassification_name"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            Status = (dr_datarow["status"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                        });
                    }
                    objagroapplication360.applicationmst_list = getapplicationmst_list;
                }
                dt_datatable.Dispose();
                objagroapplication360.status = true;
            }
            catch
            {
                objagroapplication360.status = false;
            }
        }

        public void DaCreatesectorclassification(applicationmst values, string employee_gid)
        {
            if (values.lms_code == null || values.lms_code == "")
            {
                lslms_code = "";
            }
            else
            {
                lslms_code = values.lms_code.Replace("'", "");
            }
            if (values.bureau_code == null || values.bureau_code == "")
            {
                lsbureau_code = "";
            }
            else
            {
                lsbureau_code = values.bureau_code.Replace("'", "");
            }
            msGetAPICode = objcmnfunctions.GetApiMasterGID("SCAC");
            msGetGid = objcmnfunctions.GetMasterGID("SCCS");
            msSQL = " insert into agr_mst_tsectorclassification(" +
                    " sectorclassification_gid," +
                    " sectorclassification_name," +
                    " api_code," +
                    " lms_code," +
                    " bureau_code," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetAPICode + "'," +
                    "'" + values.sectorclassification_name.Replace("'", "") + "'," +
                    "'" + msGetAPICode + "'," +
                    "'" + lslms_code + "'," +
                    "'" + lsbureau_code + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Sector Classification Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Adding";
            }
        }

        public void DaEditsectorclassification(string sectorclassification_gid, applicationmst values)
        {
            try
            {
                msSQL = " SELECT sectorclassification_gid,sectorclassification_name,lms_code, bureau_code, status as Status FROM agr_mst_tsectorclassification where sectorclassification_gid='" + sectorclassification_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.sectorclassification_gid = objODBCDatareader["sectorclassification_gid"].ToString();
                    values.sectorclassification_name = objODBCDatareader["sectorclassification_name"].ToString();
                    values.lms_code = objODBCDatareader["lms_code"].ToString();
                    values.bureau_code = objODBCDatareader["bureau_code"].ToString();
                    values.Status = objODBCDatareader["Status"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaUpdatesectorclassification(string employee_gid, applicationmst values)
        {

            if (values.lms_code == null || values.lms_code == "")
            {
                lslms_code = "";
            }
            else
            {
                lslms_code = values.lms_code.Replace("'", "");
            }
            if (values.bureau_code == null || values.bureau_code == "")
            {
                lsbureau_code = "";
            }
            else
            {
                lsbureau_code = values.bureau_code.Replace("'", "");
            }

            msSQL = " update agr_mst_tsectorclassification set " +
                 " sectorclassification_name='" + values.sectorclassification_name.Replace("'", "") + "'," +
                 " lms_code='" + lslms_code + "'," +
                 " bureau_code='" + lsbureau_code + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where sectorclassification_gid='" + values.sectorclassification_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("SCUL");

                msSQL = " insert into agr_mst_tsectorclassificationlog (" +
                       " sectorclassificationlog_gid, " +
                       " sectorclassification_gid, " +
                       " sectorclassification_name," +
                       " updated_by," +
                       " updated_date) " +
                       " values (" +
                       " '" + msGetGid + "'," +
                       " '" + values.sectorclassification_gid + "'," +
                       " '" + values.sectorclassification_name.Replace("'", "") + "'," +
                       " '" + employee_gid + "'," +
                       " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Sector Classification Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }

        public void DaInactivesectorclassification(applicationmst values, string employee_gid)
        {
            msSQL = " update agr_mst_tsectorclassification set status='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where sectorclassification_gid='" + values.sectorclassification_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("SCIL");

                msSQL = " insert into agr_mst_tsectorclassificationinactivelog (" +
                      " sectorclassificationinactivelog_gid, " +
                      " sectorclassification_gid," +
                      " sectorclassification_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.sectorclassification_gid + "'," +
                      " '" + values.sectorclassification_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Sector Classification Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Sector Classification Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaDeletesectorclassification(string sectorclassification_gid, string employee_gid, result values)
        {
            msSQL = " select sectorclassification_name from agr_mst_tsectorclassification where sectorclassification_gid='" + sectorclassification_gid + "'";
            lsmaster_value = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " delete from agr_mst_tsectorclassification where sectorclassification_gid='" + sectorclassification_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "sectorclassification Deleted Successfully..!";
                msGetGid = objcmnfunctions.GetMasterGID("MSTD");
                msSQL = " insert into agr_mst_tmasterdeletelog(" +
                         "master_gid, " +
                         "master_name, " +
                         "master_value, " +
                         "deleted_by, " +
                         "deleted_date) " +
                         " values(" +
                         "'" + msGetGid + "'," +
                         "'sectorclassification'," +
                         "'" + lsmaster_value + "'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while Delete..!";
            }
        }

        public void DasectorclassificationInactiveLogview(string sectorclassification_gid, MdlAgrMstSamAgroMaster values)
        {
            try
            {
                msSQL = " SELECT a.sectorclassification_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM agr_mst_tsectorclassificationinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.sectorclassification_gid ='" + sectorclassification_gid + "' order by a.sectorclassificationinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getapplicationmst_list = new List<applicationmst_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getapplicationmst_list.Add(new applicationmst_list
                        {
                            sectorclassification_gid = (dr_datarow["sectorclassification_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            Status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.applicationmst_list = getapplicationmst_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void InactivesectorclassificationHistory(ApplicationInactiveHistory objapplicationhistory, string sectorclassification_gid)
        {
            try
            {
                msSQL = " select a.remarks, date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                " from agr_mst_tsectorclassificationinactivelog a " +
                " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                " where a.sectorclassification_gid='" + sectorclassification_gid + "' order by a.sectorclassificationinactivelog_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getinactivehistory_list = new List<inactivehistory_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getinactivehistory_list.Add(new inactivehistory_list
                        {
                            status = (dr_datarow["status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString())
                        });
                    }
                    objapplicationhistory.inactivehistory_list = getinactivehistory_list;
                }
                dt_datatable.Dispose();
                objapplicationhistory.status = true;
            }
            catch
            {
                objapplicationhistory.status = false;
            }
        }

        public void DasectorclassificationList(MdlAgrMstSamAgroMaster values)
        {


            msSQL = " select sectorclassification_gid, sectorclassification_name from agr_mst_tsectorclassification " +
                    " order by sectorclassification_gid_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_sectorclassification = new List<applicationmst_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_sectorclassification.Add(new applicationmst_list
                    {

                        sectorclassification_gid = dt[" sectorclassification_gid"].ToString(),
                        sectorclassification_name = dt["sectorclassification_name"].ToString(),
                    });
                    values.applicationmst_list = get_sectorclassification;
                }
            }
            dt_datatable.Dispose();
        }

        //typeofwarehouse
        public void DaGettypeofwarehouse(MdlAgrMstSamAgroMaster objagroapplication360)
        {
            try
            {
                msSQL = " SELECT a.typeofwarehouse_gid,a.typeofwarehouse_name,a.lms_code, a.bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,api_code," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM agr_mst_ttypeofwarehouse a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.typeofwarehouse_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getapplicationmst_list = new List<applicationmst_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getapplicationmst_list.Add(new applicationmst_list
                        {
                            typeofwarehouse_gid = (dr_datarow["typeofwarehouse_gid"].ToString()),
                            typeofwarehouse_name = (dr_datarow["typeofwarehouse_name"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            Status = (dr_datarow["status"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                        });
                    }
                    objagroapplication360.applicationmst_list = getapplicationmst_list;
                }
                dt_datatable.Dispose();
                objagroapplication360.status = true;
            }
            catch
            {
                objagroapplication360.status = false;
            }
        }

        public void DaCreatetypeofwarehouse(applicationmst values, string employee_gid)
        {
            if (values.lms_code == null || values.lms_code == "")
            {
                lslms_code = "";
            }
            else
            {
                lslms_code = values.lms_code.Replace("'", "");
            }
            if (values.bureau_code == null || values.bureau_code == "")
            {
                lsbureau_code = "";
            }
            else
            {
                lsbureau_code = values.bureau_code.Replace("'", "");
            }
            msGetAPICode = objcmnfunctions.GetApiMasterGID("TWAC");
            msGetGid = objcmnfunctions.GetMasterGID("TYWH");
            msSQL = " insert into agr_mst_ttypeofwarehouse(" +
                    " typeofwarehouse_gid," +
                    " typeofwarehouse_name," +
                    " api_code," +
                    " lms_code," +
                    " bureau_code," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.typeofwarehouse_name.Replace("'", "") + "'," +
                    "'" + msGetAPICode + "'," +
                    "'" + lslms_code + "'," +
                    "'" + lsbureau_code + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Type of Warehouse Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Adding";
            }
        }

        public void DaEdittypeofwarehouse(string typeofwarehouse_gid, applicationmst values)
        {
            try
            {
                msSQL = " SELECT typeofwarehouse_gid,typeofwarehouse_name,lms_code, bureau_code, status as Status FROM agr_mst_ttypeofwarehouse where typeofwarehouse_gid='" + typeofwarehouse_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.typeofwarehouse_gid = objODBCDatareader["typeofwarehouse_gid"].ToString();
                    values.typeofwarehouse_name = objODBCDatareader["typeofwarehouse_name"].ToString();
                    values.lms_code = objODBCDatareader["lms_code"].ToString();
                    values.bureau_code = objODBCDatareader["bureau_code"].ToString();
                    values.Status = objODBCDatareader["Status"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaUpdatetypeofwarehouse(string employee_gid, applicationmst values)
        {

            if (values.lms_code == null || values.lms_code == "")
            {
                lslms_code = "";
            }
            else
            {
                lslms_code = values.lms_code.Replace("'", "");
            }
            if (values.bureau_code == null || values.bureau_code == "")
            {
                lsbureau_code = "";
            }
            else
            {
                lsbureau_code = values.bureau_code.Replace("'", "");
            }

            msSQL = " update agr_mst_ttypeofwarehouse set " +
                 " typeofwarehouse_name='" + values.typeofwarehouse_name.Replace("'", "") + "'," +
                 " lms_code='" + lslms_code + "'," +
                 " bureau_code='" + lsbureau_code + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where typeofwarehouse_gid='" + values.typeofwarehouse_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("TUWH");

                msSQL = " insert into agr_mst_ttypeofwarehouselog (" +
                       " typeofwarehouse_LOGgid, " +
                       " typeofwarehouse_gid, " +
                       " typeofwarehouse_name," +
                       " updated_by," +
                       " updated_date) " +
                       " values (" +
                       " '" + msGetGid + "'," +
                       " '" + values.typeofwarehouse_gid + "'," +
                       " '" + values.typeofwarehouse_name.Replace("'", "") + "'," +
                       " '" + employee_gid + "'," +
                       " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Type of Warehouse Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }

        public void DaInactivetypeofwarehouse(applicationmst values, string employee_gid)
        {
            msSQL = " select typeofwarehouse_gid from agr_mst_twarehouse where typeofwarehouse_gid='" + values.typeofwarehouse_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                values.message = "Can't able to inactive Warehouse type, Because it is tagged to Warehouse";
                values.status = false;
            }
            else
            {

                msSQL = " update agr_mst_ttypeofwarehouse set status='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where typeofwarehouse_gid='" + values.typeofwarehouse_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("TIWH");

                    msSQL = " insert into agr_mst_ttypeofwarehouseinactivelog (" +
                          " typeofwarehouseinactivelog_gid, " +
                          " typeofwarehouse_gid," +
                          " typeofwarehouse_name," +
                          " status," +
                          " remarks," +
                          " updated_by," +
                          " updated_date) " +
                          " values (" +
                          " '" + msGetGid + "'," +
                          " '" + values.typeofwarehouse_gid + "'," +
                          " '" + values.typeofwarehouse_name + "'," +
                          " '" + values.rbo_status + "'," +
                          " '" + values.remarks.Replace("'", "") + "'," +
                          " '" + employee_gid + "'," +
                          " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (values.rbo_status == 'N')
                    {
                        values.status = true;
                        values.message = "Type of Warehouse Inactivated Successfully";
                    }
                    else
                    {
                        values.status = true;
                        values.message = "Type of Warehouse Activated Successfully";
                    }
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred";
                }
            }
        }

        public void DaDeletetypeofwarehouse(string typeofwarehouse_gid, string employee_gid, result values)
        {

            msSQL = " select typeofwarehouse_gid from agr_mst_twarehouse where typeofwarehouse_gid='" + typeofwarehouse_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                values.message = "Can't able to delete Warehouse type, Because it is tagged to Warehouse";
                values.status = false;
            }
            else
            {

                msSQL = " select typeofwarehouse_name from agr_mst_ttypeofwarehouse where typeofwarehouse_gid='" + typeofwarehouse_gid + "'";
                lsmaster_value = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " delete from agr_mst_ttypeofwarehouse where typeofwarehouse_gid='" + typeofwarehouse_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "typeofwarehouse Deleted Successfully..!";
                    msGetGid = objcmnfunctions.GetMasterGID("MSTD");
                    msSQL = " insert into agr_mst_tmasterdeletelog(" +
                             "master_gid, " +
                             "master_name, " +
                             "master_value, " +
                             "deleted_by, " +
                             "deleted_date) " +
                             " values(" +
                             "'" + msGetGid + "'," +
                             "'typeofwarehouse'," +
                             "'" + lsmaster_value + "'," +
                             "'" + employee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured while Delete..!";
                }

            }

        }

        public void DatypeofwarehouseInactiveLogview(string typeofwarehouse_gid, MdlAgrMstSamAgroMaster values)
        {
            try
            {
                msSQL = " SELECT a.typeofwarehouse_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM agr_mst_ttypeofwarehouseinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.typeofwarehouse_gid ='" + typeofwarehouse_gid + "' order by a.typeofwarehouseinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getapplicationmst_list = new List<applicationmst_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getapplicationmst_list.Add(new applicationmst_list
                        {
                            typeofwarehouse_gid = (dr_datarow["typeofwarehouse_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            Status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.applicationmst_list = getapplicationmst_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void InactivetypeofwarehouseHistory(ApplicationInactiveHistory objapplicationhistory, string typeofwarehouse_gid)
        {
            try
            {
                msSQL = " select a.remarks, date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                " from agr_mst_ttypeofwarehouseinactivelog a " +
                " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                " where a.typeofwarehouse_gid='" + typeofwarehouse_gid + "' order by a.typeofwarehouseinactivelog_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getinactivehistory_list = new List<inactivehistory_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getinactivehistory_list.Add(new inactivehistory_list
                        {
                            status = (dr_datarow["status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString())
                        });
                    }
                    objapplicationhistory.inactivehistory_list = getinactivehistory_list;
                }
                dt_datatable.Dispose();
                objapplicationhistory.status = true;
            }
            catch
            {
                objapplicationhistory.status = false;
            }
        }

        public void DatypeofwarehouseList(MdlAgrMstSamAgroMaster values)
        {


            msSQL = " select typeofwarehouse_gid, typeofwarehouse_name from agr_mst_ttypeofwarehouse " +
                    " order by typeofwarehouse_gid_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_typeofwarehouse = new List<applicationmst_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_typeofwarehouse.Add(new applicationmst_list
                    {

                        typeofwarehouse_gid = dt[" typeofwarehouse_gid"].ToString(),
                        typeofwarehouse_name = dt["typeofwarehouse_name"].ToString(),
                    });
                    values.applicationmst_list = get_typeofwarehouse;
                }
            }
            dt_datatable.Dispose();
        }



        public void GettypeofsupplynatureDropdown(Mdltypeofsupplynature objvalues)
        {
            try
            {
                msSQL = " select typeofsupplynature_gid, typeofsupplynature_name from agr_mst_ttypeofsupplynature " +
                        " where status='Y' order by typeofsupplynature_name asc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getapplicationmst_list = new List<typeofsupplynature_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getapplicationmst_list.Add(new typeofsupplynature_list
                        {
                            typeofsupplynature_gid = (dr_datarow["typeofsupplynature_gid"].ToString()),
                            typeofsupplynature_name = (dr_datarow["typeofsupplynature_name"].ToString()),
                        });
                    }
                    objvalues.typeofsupplynature_list = getapplicationmst_list;
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
            }
        }

        public void DaGetsectorclassificationDropdown(Mdlsectorclassification objvalues)
        {
            try
            {
                msSQL = " SELECT a.sectorclassification_gid,a.sectorclassification_name " +
                        " FROM agr_mst_tsectorclassification a " +
                        " where status='Y' order by a.sectorclassification_name asc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getapplicationmst_list = new List<Mdlsectorclassification_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getapplicationmst_list.Add(new Mdlsectorclassification_list
                        {
                            sectorclassification_gid = (dr_datarow["sectorclassification_gid"].ToString()),
                            sectorclassification_name = (dr_datarow["sectorclassification_name"].ToString()),
                        });
                    }
                    objvalues.Mdlsectorclassification_list = getapplicationmst_list;
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
            }
        }


        public void DaGetinsurancecompanyDropdown(Mdlinsurancecompany objvalues)
        {
            try
            {
                msSQL = " select insurancecompany_gid,insurancecompany_name " +
                        " from agr_mst_tinsurancecompany " +
                        " where status='Y' order by insurancecompany_name asc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getapplicationmst_list = new List<Mdlinsurancecompany_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getapplicationmst_list.Add(new Mdlinsurancecompany_list
                        {
                            insurancecompany_gid = (dr_datarow["insurancecompany_gid"].ToString()),
                            insurancecompany_name = (dr_datarow["insurancecompany_name"].ToString()),
                        });
                    }
                    objvalues.Mdlinsurancecompany_list = getapplicationmst_list;
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
            }
        }

        public void DaGetinsurancePolicyDropdown(string insurancecompany_gid, MdlinsurancePolicy objvalues)
        {
            try
            {
                msSQL = " select insurancecompany2policy_gid, concat(policy_name, '/', policy_number) as policy from agr_mst_tinsurancecompany2policy " +
                        " where insurancecompany_gid = '" + insurancecompany_gid + "' order by policy_name asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getapplicationmst_list = new List<MdlinsurancePolicy_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getapplicationmst_list.Add(new MdlinsurancePolicy_list
                        {
                            insurancecompany2policy_gid = (dr_datarow["insurancecompany2policy_gid"].ToString()),
                            policy_name = (dr_datarow["policy"].ToString()),
                        });
                    }
                    objvalues.MdlinsurancePolicy_list = getapplicationmst_list;
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
            }
        }

        public void DaGetMstProductDropdown(MdlMstProductDropDown objvalues)
        {
            try
            {
                //Loan Product
                msSQL = " SELECT loanproduct_gid,loanproduct_name FROM agr_mst_tloanproduct a" +
                           " where status='Y' order by a.loanproduct_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getloanproduct_list = new List<MstProductlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getloanproduct_list.Add(new MstProductlist
                        {
                            loanproduct_gid = (dr_datarow["loanproduct_gid"].ToString()),
                            loanproduct_name = (dr_datarow["loanproduct_name"].ToString()),
                        });
                    }
                    objvalues.MstProductlist = getloanproduct_list;
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
            }
        }

        public void DaGetMstSubProductDropdown(string loanproduct_gid, MdlMstSubProductDropDown objvalues)
        {
            try
            {
                msSQL = "select loansubproduct_gid,loansubproduct_name from agr_mst_tloansubproduct where status='Y' and loanproduct_gid='" + loanproduct_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getapplication_list = new List<MstSubProductlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getapplication_list.Add(new MstSubProductlist
                        {
                            loansubproduct_gid = (dr_datarow["loansubproduct_gid"].ToString()),
                            loansubproduct_name = (dr_datarow["loansubproduct_name"].ToString()),
                        });
                    }
                    objvalues.MstSubProductlist = getapplication_list;
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
            }
        }


        // Commodity GST Add Event

        public void DaCreateCommodityGst(commoditygststatus values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("CGTS");
            msSQL = " insert into agr_mst_tcommoditygststatus(" +
                    " commoditygststatus_gid," +
                    " product_gid," +
                    " variety_gid," +
                    " IGST_percent," +
                    " SGST_percent," +
                    " CGST_percent, " +
                    " CESS_percent, " +
                    " wef_date, " +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.variety_gid + "'," +
                    "'" + values.IGST_percent + "'," +
                    "'" + values.SGST_percent + "'," +
                    "'" + values.CGST_percent + "'," +
                    "'" + values.CESS_percent + "'," +
                    "'" + Convert.ToDateTime(values.wef_date).ToString("yyyy-MM-dd") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "GST Details are Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Adding";
            }
        }

        public void DaGetCommodityGstList(string variety_gid, commoditygststatuslist values)
        {
            msSQL = " select commoditygststatus_gid ,product_gid,IGST_percent,SGST_percent, CGST_percent,CESS_percent, " +
                    " date_format(wef_date,'%d-%m-%Y') as wef_date from agr_mst_tcommoditygststatus " +
                    " where variety_gid ='" + variety_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getvariety_list = new List<commoditygststatus>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getvariety_list.Add(new commoditygststatus
                    {
                        commoditygststatus_gid = dt["commoditygststatus_gid"].ToString(),
                        product_gid = dt["product_gid"].ToString(),
                        IGST_percent = dt["IGST_percent"].ToString(),
                        SGST_percent = dt["SGST_percent"].ToString(),
                        CGST_percent = dt["CGST_percent"].ToString(),
                        CESS_percent = dt["CESS_percent"].ToString(),
                        wef_date = dt["wef_date"].ToString(),
                    });
                    values.commoditygststatus = getvariety_list;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaDeleteCommodityGst(string commoditygststatus_gid, result values)
        {
            msSQL = "delete from agr_mst_tcommoditygststatus where commoditygststatus_gid='" + commoditygststatus_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "GST Details are Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;
            }
        }

        // Commodity - Trade Product Details

        public void DaCreateCommodityTradeProdct(commodityTradeProdct values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("CTPG");
            msSQL = " insert into agr_mst_tcommoditytradeproductdtl(" +
                    " commoditytradeproductdtl_gid," +
                    " mstproduct_gid," +
                    " variety_gid," +
                    " product_gid," +
                    " product_name," +
                    " subproduct_gid, " +
                    " subproduct_name, " +
                    " insurancecompany_gid, " +
                    " insurancecompany_name, " +
                    " insurancepolicy_gid, " +
                    " insurancepolicy_name, " +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.variety_gid + "'," +
                    "'" + values.product_gid + "'," +
                    "'" + values.product_name + "'," +
                    "'" + values.subproduct_gid + "'," +
                    "'" + values.subproduct_name + "'," +
                    "'" + values.insurancecompany_gid + "'," +
                    "'" + values.insurancecompany_name + "'," +
                    "'" + values.insurancepolicy_gid + "'," +
                    "'" + values.insurancepolicy_name + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Trade Prodct Details are Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Adding";
            }
        }

        public void DaGetCommodityTradeProdctList(string variety_gid, commodityTradeProdctlist values)
        {
            msSQL = " select commoditytradeproductdtl_gid,product_gid,product_name,subproduct_gid, subproduct_name, " +
                    " insurancecompany_gid, insurancecompany_name, insurancepolicy_name from agr_mst_tcommoditytradeproductdtl " +
                    " where variety_gid ='" + variety_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getvariety_list = new List<commodityTradeProdct>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getvariety_list.Add(new commodityTradeProdct
                    {
                        commoditytradeproductdtl_gid = dt["commoditytradeproductdtl_gid"].ToString(),
                        product_gid = dt["product_gid"].ToString(),
                        product_name = dt["product_name"].ToString(),
                        subproduct_gid = dt["subproduct_gid"].ToString(),
                        subproduct_name = dt["subproduct_name"].ToString(),
                        insurancecompany_gid = dt["insurancecompany_gid"].ToString(),
                        insurancecompany_name = dt["insurancecompany_name"].ToString(),
                        insurancepolicy_name = dt["insurancepolicy_name"].ToString(),
                    });
                    values.commodityTradeProdct = getvariety_list;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaDeleteCommodityTradeProdct(string commoditytradeproductdtl_gid, result values)
        {
            msSQL = "delete from agr_mst_tcommoditytradeproductdtl where commoditytradeproductdtl_gid='" + commoditytradeproductdtl_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Trade Prodct Details are Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;
            }
        }


        // Commodity - Document Upload

        public void DaCreateCommodityDocumentUpload(HttpRequest httpRequest, commodityDocumentUpload values, string employee_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string lsfirstUpload = httpRequest.Form["firstUpload"].ToString();
            values.variety_gid = httpRequest.Form["variety_gid"].ToString();
            values.ason_date = httpRequest.Form["txtason_date"].ToString();
            string path = "", lspath = "";
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/CommodityReportDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    string lsfirstdocument_filepath = string.Empty;
                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {
                        string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        Stream ls_readStream;
                        ls_readStream = httpPostedFile.InputStream;
                        MemoryStream ms = new MemoryStream();
                        ls_readStream.CopyTo(ms);

                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            values.message = "File format is not supported";
                            values.status = false;
                            return;
                        }
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/CommodityReportDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/CommodityReportDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        if (lsfirstUpload == "CommodityReport")
                        {
                            if (values.commodityreport_filename == null)
                            {
                                values.commodityreport_filename = httpPostedFile.FileName;
                                values.commodityreport_filepath = lspath + msdocument_gid + FileExtension;
                            }
                            else
                            {
                                values.riskanalysisreport_filename = httpPostedFile.FileName;
                                values.riskanalysisreport_filepath = lspath + msdocument_gid + FileExtension;
                            }
                        }
                        if (lsfirstUpload == "RiskAnalysisDoc")
                        {
                            if (values.riskanalysisreport_filename == null)
                            {
                                values.riskanalysisreport_filename = httpPostedFile.FileName;
                                values.riskanalysisreport_filepath = lspath + msdocument_gid + FileExtension;
                            }
                            else
                            {
                                values.commodityreport_filename = httpPostedFile.FileName;
                                values.commodityreport_filepath = lspath + msdocument_gid + FileExtension;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            msGetGid = objcmnfunctions.GetMasterGID("CDOG");
            msSQL = " insert into agr_mst_tcommoditydocument(" +
                    " commoditydocument_gid," +
                    " product_gid," +
                    " variety_gid," +
                    " ason_date," +
                    " commodityreport_filename," +
                    " commodityreport_filepath, " +
                    " riskanalysisreport_filename, " +
                    " riskanalysisreport_filepath, " +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.variety_gid + "'," +
                    "'" + values.ason_date + "'," +
                    "'" + values.commodityreport_filename + "'," +
                    "'" + values.commodityreport_filepath + "'," +
                    "'" + values.riskanalysisreport_filename + "'," +
                    "'" + values.riskanalysisreport_filepath + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Document Details are Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Adding";
            }
        }

        public void DaGetCommodityDocumentUploadList(string variety_gid, commodityDocumentUploadlist values)
        {
            msSQL = " select commoditydocument_gid,product_gid,date_format(ason_date,'%d-%m-%Y') as ason_date, commodityreport_filename,commodityreport_filepath, " +
                    " riskanalysisreport_filename,riskanalysisreport_filepath, concat(c.user_firstname, c.user_lastname, '/',c.user_code) as created_by " +
                    " from agr_mst_tcommoditydocument a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                    " where a.variety_gid ='" + variety_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getvariety_list = new List<commodityDocumentUpload>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getvariety_list.Add(new commodityDocumentUpload
                    {
                        commoditydocument_gid = dt["commoditydocument_gid"].ToString(),
                        product_gid = dt["product_gid"].ToString(),
                        ason_date = dt["ason_date"].ToString(),
                        commodityreport_filepath = objcmnstorage.EncryptData(dt["commodityreport_filepath"].ToString()),
                        commodityreport_filename = dt["commodityreport_filename"].ToString(),
                        riskanalysisreport_filename = dt["riskanalysisreport_filename"].ToString(),
                        riskanalysisreport_filepath = objcmnstorage.EncryptData(dt["riskanalysisreport_filepath"].ToString()),
                        created_by = dt["created_by"].ToString()
                    });
                    values.commodityDocumentUpload = getvariety_list;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaDeleteCommodityDocumentUpload(string commoditydocument_gid, result values)
        {
            msSQL = "delete from agr_mst_tcommoditydocument where commoditydocument_gid='" + commoditydocument_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Document Details are Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;
            }
        }

        public void DaGetInsuranceCompanySummary(MdlInsuranceCompany objMdlInsuranceCompany)
        {
            try
            {
                msSQL = " SELECT a.insurancecompany_gid,a.insurancecompany_name,api_code," +
                    " case when a.status='N' then 'Inactive' else 'Active' end as status," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,concat(c.user_firstname,' ' ,c.user_lastname,'/',c.user_code) as created_by " +
                    " from agr_mst_tinsurancecompany a" +
                    " left join hrm_mst_temployee b on a.created_by=b.employee_gid" +
                    " left join adm_mst_tuser c on b.user_gid=c.user_gid order by a.insurancecompany_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getinsurancecompany_list = new List<insurancecompany_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getinsurancecompany_list.Add(new insurancecompany_list
                        {
                            insurancecompany_gid = (dr_datarow["insurancecompany_gid"].ToString()),
                            insurancecompany_name = (dr_datarow["insurancecompany_name"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                        });
                    }
                    objMdlInsuranceCompany.insurancecompany_list = getinsurancecompany_list;
                }
                dt_datatable.Dispose();
                objMdlInsuranceCompany.status = true;
            }
            catch
            {
                objMdlInsuranceCompany.status = false;
            }
        }

        public void DaPostPolicyAdd(MdlPolicy values, string employee_gid)
        {
            msSQL = "select policy_name from agr_mst_tinsurancecompany2policy where policy_name = '" + values.policy_name + "' and insurancecompany_gid = '" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Policy Already Exists";
            }
            else
            {

                msGetGid = objcmnfunctions.GetMasterGID("INCP");

                msSQL = " insert into agr_mst_tinsurancecompany2policy(" +
                        " insurancecompany2policy_gid ," +
                        " insurancecompany_gid ," +
                        " policy_name," +
                        " policy_number," +
                        " policy_amount," +
                        " policyperiod_from ," +
                        " policyperiod_to ," +
                        " premium_amount ," +
                        " premiumpayment_status," +
                        " paid_date," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + employee_gid + "'," +
                        "'" + values.policy_name.Replace("'", "") + "'," +
                        "'" + values.policy_number + "'," +
                        "'" + values.policy_amount + "',";


                if ((values.policyperiod_from == null) || (values.policyperiod_from == ""))
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.policyperiod_from).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }

                if ((values.policyperiod_to == null) || (values.policyperiod_to == ""))
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.policyperiod_to).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }

                msSQL += "'" + values.premium_amount + "'," +
                         "'" + values.premiumpayment_status + "',";

                if ((values.paid_date == null) || (values.paid_date == ""))
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.paid_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }


                msSQL += "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Policy Added Successfully";
                }
                else
                {
                    values.message = "Error Occured While Adding";
                    values.status = false;
                }
            }
            objODBCDatareader.Close();
        }

        public void DaGetPolicyList(string employee_gid, MdlPolicy values)
        {
            msSQL = " select insurancecompany2policy_gid,insurancecompany_gid,policy_name,policy_number,format((policy_amount), 2, 'en_IN') as policy_amount," +
                    " date_format(policyperiod_from, '%d-%m-%Y') as policyperiod_from,date_format(policyperiod_to, '%d-%m-%Y') as policyperiod_to," +
                    " format((premium_amount), 2, 'en_IN') as premium_amount,premiumpayment_status,  date_format(paid_date, '%d-%m-%Y') as paid_date" +
                    " from agr_mst_tinsurancecompany2policy where " +
                    " insurancecompany_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getpolicy_list = new List<policy_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getpolicy_list.Add(new policy_list
                    {
                        insurancecompany2policy_gid = (dr_datarow["insurancecompany2policy_gid"].ToString()),
                        insurancecompany_gid = (dr_datarow["insurancecompany_gid"].ToString()),
                        policy_name = (dr_datarow["policy_name"].ToString()),
                        policy_number = (dr_datarow["policy_number"].ToString()),
                        policy_amount = (dr_datarow["policy_amount"].ToString()),
                        policyperiod_from = (dr_datarow["policyperiod_from"].ToString()),
                        policyperiod_to = (dr_datarow["policyperiod_to"].ToString()),
                        premium_amount = (dr_datarow["premium_amount"].ToString()),
                        premiumpayment_status = (dr_datarow["premiumpayment_status"].ToString()),
                        paid_date = (dr_datarow["paid_date"].ToString()),
                    });
                }
                values.policy_list = getpolicy_list;
            }
            dt_datatable.Dispose();
        }

        public void DaDeletePolicy(string insurancecompany2policy_gid, MdlPolicy values)
        {
            msSQL = "delete from agr_mst_tinsurancecompany2policy where insurancecompany2policy_gid='" + insurancecompany2policy_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Policy Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Deleting The Policy Details";
                values.status = false;
            }
        }

        public bool DaPolicyDocumentUpload(HttpRequest httpRequest, MdlPolicy values, string employee_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string lsdocument_title = httpRequest.Form["document_title"].ToString();
            string lsinsurancecompany2policy_gid = httpRequest.Form["insurancecompany2policy_gid"].ToString();
            string path, lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/PolicyDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    string lsfirstdocument_filepath = string.Empty;
                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {
                        string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        Stream ls_readStream;
                        ls_readStream = httpPostedFile.InputStream;
                        MemoryStream ms = new MemoryStream();
                        ls_readStream.CopyTo(ms);

                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            values.message = "File format is not supported";
                            return false;
                        }
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/PolicyDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/PolicyDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("INPD");
                        msSQL = " insert into agr_mst_tinsurancecompanypolicy2document( " +
                                    " insurancecompanypolicy2document_gid," +
                                    " insurancecompany2policy_gid," +
                                    " document_title  ," +
                                    " document_name  ," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + lsinsurancecompany2policy_gid + "'," +
                                    "'" + lsdocument_title + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    }
                }
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
            }
            return true;
        }

        public void DaPolicyDocumentUploadTmpList(string insurancecompany2policy_gid, string employee_gid, MdlPolicy values)
        {
            msSQL = " select insurancecompanypolicy2document_gid,insurancecompany2policy_gid,document_title,document_name,document_path from " +
                       " agr_mst_tinsurancecompanypolicy2document where insurancecompany2policy_gid='" + insurancecompany2policy_gid + "'";


            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getpolicydoc_list = new List<policydoc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getpolicydoc_list.Add(new policydoc_list
                    {
                        insurancecompanypolicy2document_gid = dt["insurancecompanypolicy2document_gid"].ToString(),
                        insurancecompany2policy_gid = dt["insurancecompany2policy_gid"].ToString(),
                        document_title = (dt["document_title"].ToString()),
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),


                    });
                    values.policydoc_list = getpolicydoc_list;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaPolicyDocumentDelete(string insurancecompanypolicy2document_gid, MdlPolicy values)
        {
            msSQL = "delete from agr_mst_tinsurancecompanypolicy2document where insurancecompanypolicy2document_gid='" + insurancecompanypolicy2document_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Policy Document deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occrued while deleting document";
                values.status = false;
            }
        }

        public bool DaInsuranceCompanySubmit(MdlInsuranceCompany values, string employee_gid)
        {
            msSQL = "select insurancecompany_name from agr_mst_tinsurancecompany where insurancecompany_name = '" + values.insurancecompany_name + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Insurance Company Already Exists";
                return false;
            }
            msSQL = "select insurancecompany_gid from agr_mst_tinsurancecompany2policy where insurancecompany_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Policy";
                return false;
            }

            objODBCDatareader.Close();

            msGetAPICode = objcmnfunctions.GetApiMasterGID("ICAC");
            msGetGid = objcmnfunctions.GetMasterGID("INCM");

            msSQL = " insert into agr_mst_tinsurancecompany(" +
                    " insurancecompany_gid ," +
                    " insurancecompany_name ," +
                    " api_code ," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.insurancecompany_name.Replace("'", "") + "'," +
                    "'" + msGetAPICode + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                msSQL = "update agr_mst_tinsurancecompany2policy set insurancecompany_gid='" + msGetGid + "'  where insurancecompany_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Insurance Company Submitted Successfully";
                return true;
            }
            else
            {
                values.message = "Error Occured While Submitting";
                values.status = false;
                return false;
            }


        }

        public void DaGetInsuranceCompanyTempClear(string employee_gid, result values)
        {
            msSQL = "delete from agr_mst_tinsurancecompany2policy where insurancecompany_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
        }

        public void DaEditInsuranceCompany(string insurancecompany_gid, MdlInsuranceCompany objmaster)
        {
            msSQL = " SELECT insurancecompany_gid,insurancecompany_name,status as Status FROM agr_mst_tinsurancecompany a" +
                    " where insurancecompany_gid='" + insurancecompany_gid + "' ";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objmaster.insurancecompany_gid = objODBCDatareader["insurancecompany_gid"].ToString();
                objmaster.insurancecompany_name = objODBCDatareader["insurancecompany_name"].ToString();
                objmaster.rbo_status = objODBCDatareader["Status"].ToString();
            }
            objODBCDatareader.Close();

        }

        public void DaInactiveInsuranceCompany(MdlInsuranceCompany values, string employee_gid)
        {
            msSQL = " update agr_mst_tinsurancecompany set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where insurancecompany_gid='" + values.insurancecompany_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("ICIL");

                msSQL = " insert into agr_mst_tinsurancecompanyinactivelog (" +
                      " insurancecompanyinactivelog_gid  , " +
                      " insurancecompany_gid," +
                      " insurancecompany_name," +
                      " status," +
                      " remarks," +
                      " created_by," +
                      " created_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.insurancecompany_gid + "'," +
                      " '" + values.insurancecompany_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == "N")
                {
                    values.status = true;
                    values.message = "Insurance Company Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Insurance Company Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaInsuranceCompanyInactiveLogview(string insurancecompany_gid, MdlInsuranceCompany values)
        {
            try
            {
                msSQL = " SELECT insurancecompany_gid,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM agr_mst_tinsurancecompanyinactivelog a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where insurancecompany_gid ='" + insurancecompany_gid + "' order by a.insurancecompanyinactivelog_gid   desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getinsurancecompany_list = new List<insurancecompany_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getinsurancecompany_list.Add(new insurancecompany_list
                        {
                            insurancecompany_gid = (dr_datarow["insurancecompany_gid"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                        });
                    }
                    values.insurancecompany_list = getinsurancecompany_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaEditPolicy(string insurancecompany2policy_gid, MdlPolicy values)
        {
            msSQL = " SELECT insurancecompany2policy_gid,insurancecompany_gid,policy_name,policy_number,policy_amount,policyperiod_from,policyperiod_to,premium_amount," +
                    " premiumpayment_status,paid_date FROM agr_mst_tinsurancecompany2policy a" +
                    " where insurancecompany2policy_gid='" + insurancecompany2policy_gid + "' ";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.insurancecompany2policy_gid = objODBCDatareader["insurancecompany2policy_gid"].ToString();
                values.insurancecompany_gid = objODBCDatareader["insurancecompany_gid"].ToString();
                values.policy_name = objODBCDatareader["policy_name"].ToString();
                values.policy_number = objODBCDatareader["policy_number"].ToString();
                values.policy_amount = objODBCDatareader["policy_amount"].ToString();
                if (objODBCDatareader["policyperiod_from"].ToString() == "")
                {
                }
                else
                {
                    values.editpolicyperiod_from = Convert.ToDateTime(objODBCDatareader["policyperiod_from"]).ToString("dd-MM-yyyy");
                }
                if (objODBCDatareader["policyperiod_to"].ToString() == "")
                {
                }
                else
                {
                    values.editpolicyperiod_to = Convert.ToDateTime(objODBCDatareader["policyperiod_to"]).ToString("dd-MM-yyyy");
                }
                values.premium_amount = objODBCDatareader["premium_amount"].ToString();
                values.premiumpayment_status = objODBCDatareader["premiumpayment_status"].ToString();
                if (objODBCDatareader["paid_date"].ToString() == "")
                {
                }
                else
                {
                    values.editpaid_date = Convert.ToDateTime(objODBCDatareader["paid_date"]).ToString("dd-MM-yyyy");
                }
            }
            objODBCDatareader.Close();

        }

        public void DaUpdatePolicy(string employee_gid, MdlPolicy values)
        {
            msSQL = " select insurancecompany2policy_gid, insurancecompany_gid, policy_name, policy_number,policy_amount, policyperiod_from, policyperiod_to, premium_amount," +
                     " premiumpayment_status, paid_date" +
                     " from agr_mst_tinsurancecompany2policy where insurancecompany2policy_gid='" + values.insurancecompany2policy_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsinsurancecompany2policy_gid = objODBCDatareader["insurancecompany2policy_gid"].ToString();
                lsinsurancecompany_gid = objODBCDatareader["insurancecompany_gid"].ToString();

                lspolicy_name = objODBCDatareader["policy_name"].ToString();
                lspolicy_number = objODBCDatareader["policy_number"].ToString();
                lspolicy_amount = objODBCDatareader["policy_amount"].ToString();



                if (objODBCDatareader["policyperiod_from"].ToString() == "")
                {
                }
                else
                {
                    lspolicyperiod_from = Convert.ToDateTime(objODBCDatareader["policyperiod_from"]).ToString("dd-MM-yyyy");
                }
                if (objODBCDatareader["policyperiod_to"].ToString() == "")
                {
                }
                else
                {
                    lspolicyperiod_to = Convert.ToDateTime(objODBCDatareader["policyperiod_to"]).ToString("dd-MM-yyyy");
                }
                lspremium_amount = objODBCDatareader["premium_amount"].ToString();
                lspremiumpayment_status = objODBCDatareader["premiumpayment_status"].ToString();
                if (objODBCDatareader["paid_date"].ToString() == "")
                {
                }
                else
                {
                    lspaid_date = Convert.ToDateTime(objODBCDatareader["paid_date"]).ToString("dd-MM-yyyy");
                }



            }
            objODBCDatareader.Close();


            msSQL = " update agr_mst_tinsurancecompany2policy set " +
                 " policy_name='" + values.policy_name.Replace("'", "") + "'," +
                 " policy_number='" + values.policy_number + "',";

            if (values.policy_amount == null)
            {
                msSQL += " policy_amount='0.00',";
            }
            else
            {
                msSQL += " policy_amount='" + values.policy_amount.Replace(",", "") + "',";
            }

            if (Convert.ToDateTime(values.policyperiodfrom).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL += " policyperiod_from='" + Convert.ToDateTime(values.policyperiodfrom).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
            }

            if (Convert.ToDateTime(values.policyperiodto).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL += " policyperiod_to='" + Convert.ToDateTime(values.policyperiodto).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
            }

            if (values.premium_amount == null)
            {
                msSQL += " premium_amount='0.00',";
            }
            else
            {
                msSQL += " premium_amount='" + values.premium_amount.Replace(",", "") + "',";
            }

            msSQL += " premiumpayment_status='" + values.premiumpayment_status + "',";

            if (Convert.ToDateTime(values.paiddate).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL += " paid_date='" + Convert.ToDateTime(values.paiddate).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "',";
            }

            msSQL += " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where insurancecompany2policy_gid='" + values.insurancecompany2policy_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("ICPL");

                msSQL = " insert into agr_mst_tinsurancecompany2policylog (" +
                       " insurancecompany2policylog_gid, " +
                       " insurancecompany2policy_gid, " +
                       " insurancecompany_gid," +
                       " policy_name," +
                       " policy_number," +
                       " policy_amount," +
                       " policyperiod_from," +
                       " policyperiod_to," +
                       " premium_amount," +
                       " premiumpayment_status," +
                       " paid_date," +
                       " updated_by," +
                       " updated_date) " +
                       " values (" +
                       "'" + msGetGid + "'," +
                       "'" + lsinsurancecompany2policy_gid + "'," +
                       "'" + lsinsurancecompany_gid + "'," +
                       "'" + lspolicy_name + "'," +
                       "'" + lspolicy_number + "'," +
                       "'" + lspolicy_amount + "'," +
                       "'" + lspolicyperiod_from + "'," +
                       "'" + lspolicyperiod_to + "'," +
                       "'" + lspremium_amount + "'," +
                       "'" + lspremiumpayment_status + "'," +
                       "'" + lspaid_date + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Policy Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }

        public void DaGetPolicyTempList(string insurancecompany_gid, string employee_gid, MdlPolicy values)
        {
            msSQL = " select insurancecompany2policy_gid,insurancecompany_gid,policy_name,policy_number,format((policy_amount), 2, 'en_IN') as policy_amount," +
                    " date_format(policyperiod_from, '%d-%m-%Y') as policyperiod_from,date_format(policyperiod_to, '%d-%m-%Y') as policyperiod_to," +
                    " format((premium_amount), 2, 'en_IN') as premium_amount,premiumpayment_status,  date_format(paid_date, '%d-%m-%Y') as paid_date" +
                    " from agr_mst_tinsurancecompany2policy where " +
                    " insurancecompany_gid='" + employee_gid + "' or insurancecompany_gid='" + insurancecompany_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getpolicy_list = new List<policy_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getpolicy_list.Add(new policy_list
                    {
                        insurancecompany2policy_gid = (dr_datarow["insurancecompany2policy_gid"].ToString()),
                        insurancecompany_gid = (dr_datarow["insurancecompany_gid"].ToString()),
                        policy_name = (dr_datarow["policy_name"].ToString()),
                        policy_number = (dr_datarow["policy_number"].ToString()),
                        policy_amount = (dr_datarow["policy_amount"].ToString()),
                        policyperiod_from = (dr_datarow["policyperiod_from"].ToString()),
                        policyperiod_to = (dr_datarow["policyperiod_to"].ToString()),
                        premium_amount = (dr_datarow["premium_amount"].ToString()),
                        premiumpayment_status = (dr_datarow["premiumpayment_status"].ToString()),
                        paid_date = (dr_datarow["paid_date"].ToString()),
                    });
                }
                values.policy_list = getpolicy_list;
            }
            dt_datatable.Dispose();
        }

        public void DaPolicyList(string insurancecompany_gid, MdlPolicy values)
        {


            msSQL = " select insurancecompany2policy_gid,insurancecompany_gid,policy_name,policy_number,format((policy_amount), 2, 'en_IN') as policy_amount," +
                    " date_format(policyperiod_from, '%d-%m-%Y') as policyperiod_from,date_format(policyperiod_to, '%d-%m-%Y') as policyperiod_to," +
                    " format((premium_amount), 2, 'en_IN') as premium_amount,premiumpayment_status,  date_format(paid_date, '%d-%m-%Y') as paid_date" +
                    " from agr_mst_tinsurancecompany2policy where " +
                    " insurancecompany_gid='" + insurancecompany_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getpolicy_list = new List<policy_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getpolicy_list.Add(new policy_list
                    {
                        insurancecompany2policy_gid = (dr_datarow["insurancecompany2policy_gid"].ToString()),
                        insurancecompany_gid = (dr_datarow["insurancecompany_gid"].ToString()),
                        policy_name = (dr_datarow["policy_name"].ToString()),
                        policy_number = (dr_datarow["policy_number"].ToString()),
                        policy_amount = (dr_datarow["policy_amount"].ToString()),
                        policyperiod_from = (dr_datarow["policyperiod_from"].ToString()),
                        policyperiod_to = (dr_datarow["policyperiod_to"].ToString()),
                        premium_amount = (dr_datarow["premium_amount"].ToString()),
                        premiumpayment_status = (dr_datarow["premiumpayment_status"].ToString()),
                        paid_date = (dr_datarow["paid_date"].ToString()),
                    });
                }
                values.policy_list = getpolicy_list;
            }
            dt_datatable.Dispose();
        }

        public bool DaUpdateInsuranceCompany(string employee_gid, MdlInsuranceCompany values)
        {
            msSQL = "select insurancecompany_gid from agr_mst_tinsurancecompany2policy where insurancecompany_gid='" + values.insurancecompany_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Policy";
                return false;
            }

            objODBCDatareader.Close();

            msSQL = " select insurancecompany_gid, insurancecompany_name, status, remarks" +
                     " from agr_mst_tinsurancecompany where insurancecompany_gid='" + values.insurancecompany_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsinsurancecompany_gid = objODBCDatareader["insurancecompany_gid"].ToString();
                lsinsurancecompany_name = objODBCDatareader["insurancecompany_name"].ToString();
                lsstatus = objODBCDatareader["status"].ToString();
                lsremarks = objODBCDatareader["remarks"].ToString();

            }
            objODBCDatareader.Close();


            msSQL = " update agr_mst_tinsurancecompany set " +
                 " insurancecompany_name='" + values.insurancecompany_name.Replace("'", "") + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where insurancecompany_gid='" + values.insurancecompany_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("INCL");

                msSQL = " insert into agr_mst_tinsurancecompanylog (" +
                       " insurancecompanylog_gid, " +
                       " insurancecompany_gid," +
                       " insurancecompany_name," +
                       " updated_by," +
                       " updated_date) " +
                       " values (" +
                       "'" + msGetGid + "'," +
                       "'" + lsinsurancecompany_gid + "'," +
                       "'" + lsinsurancecompany_name + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tinsurancecompany2policy set insurancecompany_gid='" + values.insurancecompany_gid + "'  where insurancecompany_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Insurance Company Updated Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
                return false;
            }
        }

        // Buyer/Supplier Type

        public void DaGetBuyerSupplierType(MdlBuyerSupplierType objbuyersuppliertype)
        {
            try
            {
                msSQL = " SELECT buyersuppliertype_gid,buyersuppliertype_name,lms_code, bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,api_code," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM agr_mst_tbuyersuppliertype a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getapplication_list = new List<BuyerSupplierType_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getapplication_list.Add(new BuyerSupplierType_List
                        {
                            buyersuppliertype_gid = (dr_datarow["buyersuppliertype_gid"].ToString()),
                            buyersuppliertype_name = (dr_datarow["buyersuppliertype_name"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            Status = (dr_datarow["status"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                        });
                    }
                    objbuyersuppliertype.BuyerSupplierType_List = getapplication_list;
                }
                dt_datatable.Dispose();
                objbuyersuppliertype.status = true;
            }
            catch
            {
                objbuyersuppliertype.status = false;
            }
        }

        public void DaCreatetBuyerSupplierType(BuyerSupplierType values, string employee_gid)
        {
            if (values.lms_code == null || values.lms_code == "")
            {
                lslms_code = "";
            }
            else
            {
                lslms_code = values.lms_code.Replace("'", "\\'");
            }
            if (values.bureau_code == null || values.bureau_code == "")
            {
                lsbureau_code = "";
            }
            else
            {
                lsbureau_code = values.bureau_code.Replace("'", "\\'");
            }
            msGetAPICode = objcmnfunctions.GetApiMasterGID("SBAC");
            msGetGid = objcmnfunctions.GetMasterGID("BSTC");
            msSQL = " insert into agr_mst_tbuyersuppliertype(" +
                    " buyersuppliertype_gid," +
                    " buyersuppliertype_name," +
                    " api_code," +
                    " lms_code," +
                    " bureau_code," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.buyersuppliertype_name.Replace("'", "\\'") + "'," +
                    "'" + msGetAPICode + "'," +
                    "'" + lslms_code + "'," +
                    "'" + lsbureau_code + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Buyer/Supplier Type Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Adding";
            }
        }

        public void DaEditBuyerSupplierType(string buyersuppliertype_gid, BuyerSupplierType values)
        {
            try
            {
                msSQL = " SELECT buyersuppliertype_gid,buyersuppliertype_name,lms_code, bureau_code, status as Status FROM agr_mst_tbuyersuppliertype " +
                        " where buyersuppliertype_gid='" + buyersuppliertype_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.buyersuppliertype_gid = objODBCDatareader["buyersuppliertype_gid"].ToString();
                    values.buyersuppliertype_name = objODBCDatareader["buyersuppliertype_name"].ToString();
                    values.lms_code = objODBCDatareader["lms_code"].ToString();
                    values.bureau_code = objODBCDatareader["bureau_code"].ToString();
                    values.Status = objODBCDatareader["Status"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaUpdateBuyerSupplierType(string employee_gid, BuyerSupplierType values)
        {
            if (values.lms_code == null || values.lms_code == "")
            {
                lslms_code = "";
            }
            else
            {
                lslms_code = values.lms_code.Replace("'", "\\'");
            }
            if (values.bureau_code == null || values.bureau_code == "")
            {
                lsbureau_code = "";
            }
            else
            {
                lsbureau_code = values.bureau_code.Replace("'", "\\'");
            }

            msSQL = " update agr_mst_tbuyersuppliertype set " +
                 " buyersuppliertype_name='" + values.buyersuppliertype_name.Replace("'", "\\'") + "'," +
                 " lms_code='" + lslms_code + "'," +
                 " bureau_code='" + lsbureau_code + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where buyersuppliertype_gid='" + values.buyersuppliertype_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("BSTU");

                msSQL = " insert into agr_mst_tbuyersuppliertype_log (" +
                       " buyersuppliertypelog_gid, " +
                       " buyersuppliertype_gid, " +
                       " buyersuppliertype_name," +
                       " updated_by," +
                       " updated_date) " +
                       " values (" +
                       " '" + msGetGid + "'," +
                       " '" + values.buyersuppliertype_gid + "'," +
                       " '" + values.buyersuppliertype_name.Replace("'", "\\'") + "'," +
                       " '" + employee_gid + "'," +
                       " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Buyer/Supplier Type Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }

        public void DaInactiveBuyerSupplierType(BuyerSupplierType values, string employee_gid)
        {
            msSQL = " update agr_mst_tbuyersuppliertype set status='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "\\'") + "'" +
                    " where buyersuppliertype_gid='" + values.buyersuppliertype_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("BSTL");

                msSQL = " insert into agr_mst_tbuyersuppliertype_inactivelog (" +
                      " buyersuppliertypeinactivelog_gid, " +
                      " buyersuppliertype_gid," +
                      " buyersuppliertype_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.buyersuppliertype_gid + "'," +
                      " '" + values.buyersuppliertype_name.Replace("'", "\\'") + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "\\'") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Buyer/Supplier Type Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Buyer/Supplier Type Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaDeleteBuyerSupplierType(string buyersuppliertype_gid, string employee_gid, result values)
        {

            msSQL = " select buyersuppliertype_name from agr_mst_tbuyersuppliertype where buyersuppliertype_gid='" + buyersuppliertype_gid + "'";
            lsmaster_value = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " delete from agr_mst_tbuyersuppliertype where buyersuppliertype_gid='" + buyersuppliertype_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Buyer/Supplier Type Deleted Successfully..!";
                msGetGid = objcmnfunctions.GetMasterGID("MSTD");
                msSQL = " insert into agr_mst_tmasterdeletelog(" +
                         "master_gid, " +
                         "master_name, " +
                         "master_value, " +
                         "deleted_by, " +
                         "deleted_date) " +
                         " values(" +
                         "'" + msGetGid + "'," +
                         "'Buyer/Supplier Type'," +
                         "'" + lsmaster_value + "'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }

        }

        public void DaBuyerSupplierTypeInactiveLogview(string buyersuppliertype_gid, MdlBuyerSupplierType values)
        {
            try
            {
                msSQL = " SELECT buyersuppliertype_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM agr_mst_tbuyersuppliertype_inactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where buyersuppliertype_gid ='" + buyersuppliertype_gid + "' order by a.buyersuppliertypeinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getapplication_list = new List<BuyerSupplierType_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getapplication_list.Add(new BuyerSupplierType_List
                        {
                            buyersuppliertype_gid = (dr_datarow["buyersuppliertype_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            Status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.BuyerSupplierType_List = getapplication_list;
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