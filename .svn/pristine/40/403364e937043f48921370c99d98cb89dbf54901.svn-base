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
    /// This DataAccess will provide access to Product & PMG Approval master and corresponding approvals in warehouse masters.
    /// </summary>
    /// <remarks>Written by Sherin Augusta, Premchander.K</remarks>
    public class DaAgrMstProductPmgApproval
    {

        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader, objODBCDatareader1, objODBCDatareader2;
        string msSQL, msGetGid, msGetGidREF, lsapproval_flag, msGetGid1, msGetAPICode;
        string productemployee, pmgemployee;
        int mnResult;

        public void DaGetPmgApproval(PmgApprovallist objvalues)
        {
            try
            {
                msSQL = " SELECT mstpmgapproval_gid,pmgapproval_ID,lms_code, bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,api_code," +
                        " pmgapproval_gid,pmgapproval_name " +
                        " FROM agr_mst_pmgapproval a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.mstpmgapproval_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getPmgApprovaldtl = new List<PmgApprovaldtl>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getPmgApprovaldtl.Add(new PmgApprovaldtl
                        {
                            mstpmgapproval_gid = (dr_datarow["mstpmgapproval_gid"].ToString()),
                            pmgapproval_ID = (dr_datarow["pmgapproval_ID"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            pmgapproval_gid = (dr_datarow["pmgapproval_gid"].ToString()),
                            pmgapproval_name = (dr_datarow["pmgapproval_name"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                        });
                    }
                    objvalues.PmgApprovaldtl = getPmgApprovaldtl;
                }
                dt_datatable.Dispose();
            }
            catch
            {
            }
        }


        public void DaCreatePmgApproval(PmgApprovaldtl values, string employee_gid)
        {

            msSQL = "select pmgapproval_gid from agr_mst_pmgapproval where pmgapproval_gid = '" + values.pmgapproval_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "This employee is already added for the pmg approval";
            }
            else
            {

                msGetAPICode = objcmnfunctions.GetApiMasterGID("PMGC");
                msGetGid = objcmnfunctions.GetMasterGID("AMPM");
                if (values.lms_code == null || values.lms_code == "")
                    values.lms_code = "";
                else
                    values.lms_code = values.lms_code.Replace("'", "\\'");
                if (values.bureau_code == null || values.bureau_code == "")
                    values.bureau_code = "";
                else
                    values.bureau_code = values.bureau_code.Replace("'", "\\'");
                values.pmgapproval_ID = objcmnfunctions.GetMasterGID("PMG");
                msSQL = " insert into agr_mst_pmgapproval(" +
                            " mstpmgapproval_gid, " +
                            " api_code, " +
                            " pmgapproval_ID, " +
                            " pmgapproval_gid, " +
                            " pmgapproval_name, " +
                            " lms_code," +
                            " bureau_code," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + msGetAPICode + "'," +
                            "'" + values.pmgapproval_ID + "'," +
                            "'" + values.pmgapproval_gid + "'," +
                            "'" + values.pmgapproval_name + "'," +
                            "'" + values.lms_code + "'," +
                            "'" + values.bureau_code + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {

                    msSQL = "select warehouse_gid from agr_mst_twarehouse where pmgapproval_flag='N'";

                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {

                            msGetGid1 = objcmnfunctions.GetMasterGID("W2AG");

                            msSQL = " insert into agr_trn_twarehouse2approval(" +
                                    "    warehouse2approval_gid," +
                                    " warehouse_gid," +
                                    " approval_gid," +
                                    " approval_name," +
                                    " mstpmgapproval_gid, " +
                                    " created_by , " +
                                    " created_date )" +
                                    " values(" +
                                    "'" + msGetGid1 + "'," +
                                    "'" + (dt["warehouse_gid"].ToString()) + "'," +
                                    "'" + values.pmgapproval_gid + "'," +
                                    "'" + values.pmgapproval_name + "'," +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        }

                    }

                    values.status = true;
                    values.message = "PMG Approval details are Added Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred While Adding";
                }

            }
        }

        public void DaEditPmgApproval(string mstpmgapproval_gid, PmgApprovaldtl values)
        {
            try
            {
                msSQL = " SELECT mstpmgapproval_gid, pmgapproval_ID, lms_code, bureau_code, " +
                        " pmgapproval_gid,pmgapproval_name " +
                        " FROM agr_mst_pmgapproval a  where mstpmgapproval_gid = '" + mstpmgapproval_gid + "' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.mstpmgapproval_gid = objODBCDatareader["mstpmgapproval_gid"].ToString();
                    values.pmgapproval_ID = objODBCDatareader["pmgapproval_ID"].ToString();
                    values.lms_code = objODBCDatareader["lms_code"].ToString();
                    values.bureau_code = objODBCDatareader["bureau_code"].ToString();
                    values.pmgapproval_gid = objODBCDatareader["pmgapproval_gid"].ToString();
                    values.pmgapproval_name = objODBCDatareader["pmgapproval_name"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaUpdatePmgApproval(string employee_gid, PmgApprovaldtl values)
        {
            if (values.lms_code == null || values.lms_code == "")
                values.lms_code = "";
            else
                values.lms_code = values.lms_code.Replace("'", "");
            if (values.bureau_code == null || values.bureau_code == "")
                values.bureau_code = "";
            else
                values.bureau_code = values.bureau_code.Replace("'", "");

            msSQL = " update agr_mst_pmgapproval set " +
                    //" pmgapproval_gid='" + values.pmgapproval_gid + "'," +
                    //" pmgapproval_name='" + values.pmgapproval_name + "'," +
                    " lms_code='" + values.lms_code + "'," +
                    " bureau_code='" + values.bureau_code + "'," +
                    " updated_by='" + employee_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where mstpmgapproval_gid='" + values.mstpmgapproval_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "PMG Approval details are Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }

        public void DaDeletePmgApproval(string mstpmgapproval_gid, string employee_gid, result values)
        {
            msSQL = " delete from agr_mst_pmgapproval where mstpmgapproval_gid='" + mstpmgapproval_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "PMG Approval details are Deleted Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public void DaGetProductApproval(ProductApprovallist objvalues)
        {
            try
            {
                msSQL = " SELECT mstproductapproval_gid,productapproval_ID,lms_code, bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,api_code," +
                        " productapproval_gid,productapproval_name " +
                        " FROM agr_mst_productapproval a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.mstproductapproval_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getProductApprovaldtl = new List<ProductApprovaldtl>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getProductApprovaldtl.Add(new ProductApprovaldtl
                        {
                            mstproductapproval_gid = (dr_datarow["mstproductapproval_gid"].ToString()),
                            productapproval_ID = (dr_datarow["productapproval_ID"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            productapproval_gid = (dr_datarow["productapproval_gid"].ToString()),
                            productapproval_name = (dr_datarow["productapproval_name"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                        });
                    }
                    objvalues.ProductApprovaldtl = getProductApprovaldtl;
                }
                dt_datatable.Dispose();
            }
            catch
            {
            }
        }

        public void DaCreateProductApproval(ProductApprovaldtl values, string employee_gid)
        {


            msSQL = "select productapproval_gid from agr_mst_productapproval where productapproval_gid = '" + values.productapproval_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "This employee is already added for the product approval";
            }
            else
            {

                msGetAPICode = objcmnfunctions.GetApiMasterGID("PAAC");
                msGetGid = objcmnfunctions.GetMasterGID("APAL");
                if (values.lms_code == null || values.lms_code == "")
                    values.lms_code = "";
                else
                    values.lms_code = values.lms_code.Replace("'", "\\'");
                if (values.bureau_code == null || values.bureau_code == "")
                    values.bureau_code = "";
                else
                    values.bureau_code = values.bureau_code.Replace("'", "\\'");
                values.productapproval_ID = objcmnfunctions.GetMasterGID("PA");
                msSQL = " insert into agr_mst_productapproval(" +
                            " mstproductapproval_gid, " +
                            " api_code, " +
                            " productapproval_ID, " +
                            " productapproval_gid, " +
                            " productapproval_name, " +
                            " lms_code," +
                            " bureau_code," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + msGetAPICode + "'," +
                            "'" + values.productapproval_ID + "'," +
                            "'" + values.productapproval_gid + "'," +
                            "'" + values.productapproval_name + "'," +
                            "'" + values.lms_code + "'," +
                            "'" + values.bureau_code + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {

                    //msSQL = " select GROUP_CONCAT('\\'', warehouse_gid,'\\'') as warehouse_gid  from agr_trn_twarehouse2approval " +
                    //        " where creditor_gid='" + creditor_gid + "'";

                    //values.loanproduct_name = objdbconn.GetExecuteScalar(msSQL);

                    //msSQL = "select distinct (warehouse_gid) from agr_trn_twarehouse2approval ";

                    //dt_datatable = objdbconn.GetDataTable(msSQL);
                    //var getproductwarehouselist = new List<productwarehouselist>();
                    //if (dt_datatable.Rows.Count != 0)
                    //{
                    //    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    //    {
                    //        getproductwarehouselist.Add(new productwarehouselist
                    //        {
                    //            warehouse_gid = (dr_datarow["warehouse_gid"].ToString()),

                    //        });
                    //    }
                    //    values.productwarehouselist = getproductwarehouselist;
                    //}
                    //dt_datatable.Dispose();

                    //string warehouse_gid = string.Empty;

                    //if (values.productwarehouselist != null)
                    //{
                    //    for (var j = 0; j < values.productwarehouselist.Count; j++)
                    //    {
                    //        warehouse_gid += "'" + values.productwarehouselist[j].warehouse_gid + "'" + ",";

                    //    }

                    //    warehouse_gid = warehouse_gid.TrimEnd(',');
                    //}

                    msSQL = "select warehouse_gid from agr_mst_twarehouse where productapproval_flag='N'";

                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {

                            msGetGid1 = objcmnfunctions.GetMasterGID("W2AG");

                            msSQL = " insert into agr_trn_twarehouse2approval(" +
                                    "    warehouse2approval_gid," +
                                    " warehouse_gid," +
                                    " approval_gid," +
                                    " approval_name," +
                                    " mstproductapproval_gid, " +
                                    " created_by , " +
                                    " created_date )" +
                                    " values(" +
                                    "'" + msGetGid1 + "'," +
                                    "'" + (dt["warehouse_gid"].ToString()) + "'," +
                                    "'" + values.productapproval_gid + "'," +
                                    "'" + values.productapproval_name + "'," +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        }

                    }

                    values.status = true;
                    values.message = "Product Approval details are Added Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred While Adding";
                }

            }
        }

        public void DaEditProductApproval(string mstproductapproval_gid, ProductApprovaldtl values)
        {
            try
            {
                msSQL = " SELECT mstproductapproval_gid, productapproval_ID, lms_code, bureau_code, " +
                        " productapproval_gid,productapproval_name " +
                        " FROM agr_mst_productapproval a  where mstproductapproval_gid = '" + mstproductapproval_gid + "' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.mstproductapproval_gid = objODBCDatareader["mstproductapproval_gid"].ToString();
                    values.productapproval_ID = objODBCDatareader["productapproval_ID"].ToString();
                    values.lms_code = objODBCDatareader["lms_code"].ToString();
                    values.bureau_code = objODBCDatareader["bureau_code"].ToString();
                    values.productapproval_gid = objODBCDatareader["productapproval_gid"].ToString();
                    values.productapproval_name = objODBCDatareader["productapproval_name"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaUpdateProductApproval(string employee_gid, ProductApprovaldtl values)
        {
            if (values.lms_code == null || values.lms_code == "")
                values.lms_code = "";
            else
                values.lms_code = values.lms_code.Replace("'", "");
            if (values.bureau_code == null || values.bureau_code == "")
                values.bureau_code = "";
            else
                values.bureau_code = values.bureau_code.Replace("'", "");

            msSQL = " update agr_mst_productapproval set " +
                    //" productapproval_gid='" + values.productapproval_gid + "'," +
                    //" productapproval_name='" + values.productapproval_name + "'," +
                    " lms_code='" + values.lms_code + "'," +
                    " bureau_code='" + values.bureau_code + "'," +
                    " updated_by='" + employee_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where mstproductapproval_gid='" + values.mstproductapproval_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Product Approval details are Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }

        public void DaDeleteProductApproval(string mstproductapproval_gid, string employee_gid, result values)
        {
            msSQL = " delete from agr_mst_productapproval where mstproductapproval_gid='" + mstproductapproval_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Product Approval details are Deleted Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public void DaGetPendingProductApprovalSummary(string employee_gid, PendingProductApprovallist values)
        {
            msSQL = "select pmgapproval_gid from agr_mst_pmgapproval   where pmgapproval_gid = '" + employee_gid + "'";
            values.pmgapproval = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select productapproval_gid  from agr_mst_productapproval where productapproval_gid = '" + employee_gid + "'";
            values.productapproval = objdbconn.GetExecuteScalar(msSQL);


            //msSQL = "   select (select pmgapproval_gid from agr_mst_pmgapproval   where pmgapproval_gid='" + employee_gid + "') as pmgapproval ,  " +
            //      " productapproval_gid as productapproval from agr_mst_productapproval where productapproval_gid = '" + employee_gid + "'";
            //objODBCDatareader = objdbconn.GetDataReader(msSQL);

            //if (objODBCDatareader.HasRows == true)
            //{

            //    values.pmgapproval = objODBCDatareader["pmgapproval"].ToString();
            //    values.productapproval = objODBCDatareader["productapproval"].ToString();


            //}
            //objODBCDatareader.Close();


            if ( !string.IsNullOrEmpty(values.productapproval))
            {

                msSQL = " select a.warehouse_gid, a.warehouse_ref_no, a.warehouse_name, d.product_name," +
                  " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                  " CASE WHEN(a.productapproval_flag = 'N' and a.pmgapproval_flag = 'N')  THEN 'Product Approval Pending' " +
                    //" WHEN(a.productapproval_flag = 'Y' and a.pmgapproval_flag = 'N') THEN 'PMG Approval Pending' " +
                    //" WHEN(a.productapproval_flag = 'R' and a.pmgapproval_flag = 'N') THEN 'Product Approval - Rejected' " +
                    //" WHEN(a.productapproval_flag = 'Y' and a.pmgapproval_flag = 'R') THEN 'PMG Approval - Rejected' " +
                    " ELSE '-' END as approval_status , " +
                  " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,e.warehouse2approval_gid  " +
                  "  from agr_mst_twarehouse a" +
                  " left join agr_trn_twarehouse2approval e on a.warehouse_gid = e.warehouse_gid" +
                  " left join agr_mst_twarehouse2commodity d on a.warehouse_gid = d.warehouse_gid" +
                  " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                  " left join adm_mst_tuser c on c.user_gid=b.user_gid where e.approval_gid='" + employee_gid + "' " +
                  " and (a.productapproval_flag='N' ) and a.pmgapproval_flag='N' and mstproductapproval_gid is not null  and warehousesubmit_flag='" + getWarehouseStatusClass.Pending + "' " +
                  " group by warehouse_gid order by warehouse_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getbuyerbank_list = new List<PendingProductApprovaldtl>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getbuyerbank_list.Add(new PendingProductApprovaldtl
                        {
                            warehouse_gid = (dr_datarow["warehouse_gid"].ToString()),
                            warehouse_ref_no = (dr_datarow["warehouse_ref_no"].ToString()),
                            warehouse_name = (dr_datarow["warehouse_name"].ToString()),
                            product_name = (dr_datarow["product_name"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            warehouse2approval_gid = (dr_datarow["warehouse2approval_gid"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),

                        });
                    }
                    values.PendingProductApprovaldtl = getbuyerbank_list;
                }
                dt_datatable.Dispose();

            }

            else { }
        }

        public void DaGetProductApprovalSummary(string employee_gid, PendingProductApprovallist values)
        {
            msSQL = "select pmgapproval_gid from agr_mst_pmgapproval   where pmgapproval_gid = '" + employee_gid + "'";
            values.pmgapproval = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select productapproval_gid  from agr_mst_productapproval where productapproval_gid = '" + employee_gid + "'";
            values.productapproval = objdbconn.GetExecuteScalar(msSQL);

             if (!string.IsNullOrEmpty(values.productapproval))
            {


                msSQL = " select a.warehouse_gid, a.warehouse_ref_no, a.warehouse_name, d.product_name," +
                  " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                  " CASE WHEN(a.productapproval_flag = 'Y' and a.pmgapproval_flag = 'N') THEN 'PMG Approval Pending' " +
                    " WHEN(a.productapproval_flag = 'R' and a.pmgapproval_flag = 'N') THEN 'Product Approval - Rejected' " +
                    " WHEN(a.productapproval_flag = 'Y' and a.pmgapproval_flag = 'R') THEN 'PMG Approval - Rejected' " +
                    " ELSE '-' END as approval_status , " +
                  " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,e.warehouse2approval_gid  " +
                  "  from agr_mst_twarehouse a" +
                  " left join agr_trn_twarehouse2approval e on a.warehouse_gid = e.warehouse_gid" +
                  " left join agr_mst_twarehouse2commodity d on a.warehouse_gid = d.warehouse_gid" +
                  " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                  " left join adm_mst_tuser c on c.user_gid=b.user_gid where e.approval_gid='" + employee_gid + "' " +
                  " and  a.productapproval_flag='Y' and a.pmgapproval_flag='N' and mstproductapproval_gid is not null " +
                  //"(warehousesubmit_flag='" + getWarehouseStatusClass.Pending + "' or warehousesubmit_flag='" + getWarehouseStatusClass.Rejected + "' or warehousesubmit_flag='" + getWarehouseStatusClass.Approved + "' )" +
                  " group by warehouse_gid order by warehouse_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getbuyerbank_list = new List<PendingProductApprovaldtl>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getbuyerbank_list.Add(new PendingProductApprovaldtl
                        {
                            warehouse_gid = (dr_datarow["warehouse_gid"].ToString()),
                            warehouse_ref_no = (dr_datarow["warehouse_ref_no"].ToString()),
                            warehouse_name = (dr_datarow["warehouse_name"].ToString()),
                            product_name = (dr_datarow["product_name"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            warehouse2approval_gid = (dr_datarow["warehouse2approval_gid"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),

                        });
                    }
                    values.PendingProductApprovaldtl = getbuyerbank_list;
                }
                dt_datatable.Dispose();

            }

            else { }
        }

        public void DaGetPendingPMGApprovalSummary(string employee_gid, PendingProductApprovallist values)
        {

            msSQL = "select pmgapproval_gid from agr_mst_pmgapproval   where pmgapproval_gid = '" + employee_gid + "'";
            values.pmgapproval = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select productapproval_gid  from agr_mst_productapproval where productapproval_gid = '" + employee_gid + "'";
            values.productapproval = objdbconn.GetExecuteScalar(msSQL);

            if (!string.IsNullOrEmpty(values.pmgapproval))
            {

                msSQL = " select a.warehouse_gid,e.warehouse2approval_gid, a.warehouse_ref_no, a.warehouse_name, d.product_name," +
                  " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                  " CASE WHEN(a.productapproval_flag = 'N' and a.pmgapproval_flag = 'N')  THEN 'Product Approval Pending' " +
                  " WHEN(a.productapproval_flag = 'Y' and a.pmgapproval_flag = 'N') THEN 'PMG Approval Pending' " +
                  " WHEN(a.productapproval_flag = 'R' and a.pmgapproval_flag = 'N') THEN 'Product Approval - Rejected' " +
                  " WHEN(a.productapproval_flag = 'Y' and a.pmgapproval_flag = 'R') THEN 'PMG Approval - Rejected' " +
                  " ELSE '-' END as approval_status , " +
                  " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                  "  from agr_mst_twarehouse a" +
                  " left join agr_trn_twarehouse2approval e on a.warehouse_gid = e.warehouse_gid" +
                  " left join agr_mst_twarehouse2commodity d on a.warehouse_gid = d.warehouse_gid" +
                  " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                  " left join adm_mst_tuser c on c.user_gid=b.user_gid where e.approval_gid='" + employee_gid + "'" +
                  " and a.productapproval_flag ='Y' and a.pmgapproval_flag='N'  and mstpmgapproval_gid is not null " +
                  " group by warehouse_gid order by warehouse_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbuyerbank_list = new List<PendingProductApprovaldtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbuyerbank_list.Add(new PendingProductApprovaldtl
                    {
                        warehouse_gid = (dr_datarow["warehouse_gid"].ToString()),
                        warehouse_ref_no = (dr_datarow["warehouse_ref_no"].ToString()),
                        warehouse_name = (dr_datarow["warehouse_name"].ToString()),
                        product_name = (dr_datarow["product_name"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        warehouse2approval_gid = (dr_datarow["warehouse2approval_gid"].ToString()),
                        approval_status = (dr_datarow["approval_status"].ToString()),

                    });
                }
                values.PendingProductApprovaldtl = getbuyerbank_list;
            }
            dt_datatable.Dispose();

            }


            else { }
        }

        public void DaGetRejectedApprovalSummary(string employee_gid, PendingProductApprovallist values)
        {
            msSQL = "select pmgapproval_gid from agr_mst_pmgapproval   where pmgapproval_gid = '" + employee_gid + "'";
            values.pmgapproval = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select productapproval_gid  from agr_mst_productapproval where productapproval_gid = '" + employee_gid + "'";
            values.productapproval = objdbconn.GetExecuteScalar(msSQL);

            if (!string.IsNullOrEmpty(values.pmgapproval) )
            {

                msSQL = " select a.warehouse_gid,e.warehouse2approval_gid, a.warehouse_ref_no, a.warehouse_name, d.product_name," +
                  " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                  " CASE WHEN(a.productapproval_flag = 'N' and a.pmgapproval_flag = 'N')  THEN 'Product Approval Pending' " +
                    " WHEN(a.productapproval_flag = 'Y' and a.pmgapproval_flag = 'N') THEN 'PMG Approval Pending' " +
                    " WHEN(a.productapproval_flag = 'R' and a.pmgapproval_flag = 'N') THEN 'Product Approval - Rejected' " +
                    " WHEN(a.productapproval_flag = 'Y' and a.pmgapproval_flag = 'R') THEN 'PMG Approval - Rejected' " +
                    " ELSE '-' END as approval_status , " +
                  " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                  "  from agr_mst_twarehouse a" +
                  " left join agr_trn_twarehouse2approval e on a.warehouse_gid = e.warehouse_gid" +
                  " left join agr_mst_twarehouse2commodity d on a.warehouse_gid = d.warehouse_gid" +
                  " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                  " left join adm_mst_tuser c on c.user_gid=b.user_gid where " +
                  "  a.warehousesubmit_flag='" + getWarehouseStatusClass.Rejected + "' group by warehouse_gid order by warehouse_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getbuyerbank_list = new List<PendingProductApprovaldtl>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getbuyerbank_list.Add(new PendingProductApprovaldtl
                        {
                            warehouse_gid = (dr_datarow["warehouse_gid"].ToString()),
                            warehouse_ref_no = (dr_datarow["warehouse_ref_no"].ToString()),
                            warehouse_name = (dr_datarow["warehouse_name"].ToString()),
                            product_name = (dr_datarow["product_name"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            warehouse2approval_gid = (dr_datarow["warehouse2approval_gid"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString())

                        });
                    }
                    values.PendingProductApprovaldtl = getbuyerbank_list;
                }
                dt_datatable.Dispose();

            }

            else if (!string.IsNullOrEmpty(values.productapproval))
            {


                msSQL = " select a.warehouse_gid,e.warehouse2approval_gid, a.warehouse_ref_no, a.warehouse_name, d.product_name," +
                   " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                   " CASE WHEN(a.productapproval_flag = 'N' and a.pmgapproval_flag = 'N')  THEN 'Product Approval Pending' " +
                     " WHEN(a.productapproval_flag = 'Y' and a.pmgapproval_flag = 'N') THEN 'PMG Approval Pending' " +
                     " WHEN(a.productapproval_flag = 'R' and a.pmgapproval_flag = 'N') THEN 'Product Approval - Rejected' " +
                     " WHEN(a.productapproval_flag = 'Y' and a.pmgapproval_flag = 'R') THEN 'PMG Approval - Rejected' " +
                     " ELSE '-' END as approval_status , " +
                   " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                   "  from agr_mst_twarehouse a" +
                   " left join agr_trn_twarehouse2approval e on a.warehouse_gid = e.warehouse_gid" +
                   " left join agr_mst_twarehouse2commodity d on a.warehouse_gid = d.warehouse_gid" +
                   " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                   " left join adm_mst_tuser c on c.user_gid=b.user_gid where " +
                   "  a.warehousesubmit_flag='" + getWarehouseStatusClass.Rejected + "' group by warehouse_gid order by warehouse_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getbuyerbank_list = new List<PendingProductApprovaldtl>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getbuyerbank_list.Add(new PendingProductApprovaldtl
                        {
                            warehouse_gid = (dr_datarow["warehouse_gid"].ToString()),
                            warehouse_ref_no = (dr_datarow["warehouse_ref_no"].ToString()),
                            warehouse_name = (dr_datarow["warehouse_name"].ToString()),
                            product_name = (dr_datarow["product_name"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            warehouse2approval_gid = (dr_datarow["warehouse2approval_gid"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString())

                        });
                    }
                    values.PendingProductApprovaldtl = getbuyerbank_list;
                }
                dt_datatable.Dispose();


            }


            else { }
        }


        public void DaGetApprovedwarehouseSummary(string employee_gid, PendingProductApprovallist values)
        {

            msSQL = "select pmgapproval_gid from agr_mst_pmgapproval   where pmgapproval_gid = '" + employee_gid + "'";
            values.pmgapproval = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select productapproval_gid  from agr_mst_productapproval where productapproval_gid = '" + employee_gid + "'";
            values.productapproval = objdbconn.GetExecuteScalar(msSQL);

            if (!string.IsNullOrEmpty(values.pmgapproval))
            {

                msSQL = " select a.warehouse_gid,e.warehouse2approval_gid, a.warehouse_ref_no, a.warehouse_name, d.product_name," +
                      " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                       " concat(g.user_firstname,' ',g.user_lastname,' / ',g.user_code) as updated_by," +
                      " CASE WHEN(a.productapproval_flag = 'N' and a.pmgapproval_flag = 'N')  THEN 'Product Approval Pending' " +
                        " WHEN(a.productapproval_flag = 'Y' and a.pmgapproval_flag = 'N') THEN 'PMG Approval Pending' " +
                        " WHEN(a.productapproval_flag = 'R' and a.pmgapproval_flag = 'N') THEN 'Product Approval - Rejected' " +
                        " WHEN(a.productapproval_flag = 'Y' and a.pmgapproval_flag = 'R') THEN 'PMG Approval - Rejected' " +
                        " ELSE '-' END as approval_status , " +
                      " date_format(a.warehouse_approveddate,'%d-%m-%Y %h:%i %p') as warehouse_approveddate, " +
                       " date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                      " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date " +
                      "  from agr_mst_twarehouse a" +
                      " left join agr_trn_twarehouse2approval e on a.warehouse_gid = e.warehouse_gid" +
                      " left join agr_mst_twarehouse2commodity d on a.warehouse_gid = d.warehouse_gid" +
                      " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                      " left join adm_mst_tuser c on c.user_gid=b.user_gid  " +
                      " left join hrm_mst_temployee f on f.employee_gid=a.updated_by " +
                      " left join adm_mst_tuser g on g.user_gid=f.user_gid where " +
                      "  (a.pmgapproval_flag='Y' and a.productapproval_flag='Y') group by warehouse_gid order by warehouse_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getbuyerbank_list = new List<PendingProductApprovaldtl>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getbuyerbank_list.Add(new PendingProductApprovaldtl
                        {
                            warehouse_gid = (dr_datarow["warehouse_gid"].ToString()),
                            warehouse_ref_no = (dr_datarow["warehouse_ref_no"].ToString()),
                            warehouse_name = (dr_datarow["warehouse_name"].ToString()),
                            product_name = (dr_datarow["product_name"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            warehouse2approval_gid = (dr_datarow["warehouse2approval_gid"].ToString()),
                            warehouse_approveddate = (dr_datarow["warehouse_approveddate"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),

                        });
                    }
                    values.PendingProductApprovaldtl = getbuyerbank_list;
                }
                dt_datatable.Dispose();

            }

            else if (!string.IsNullOrEmpty(values.productapproval))
            {


                msSQL = " select a.warehouse_gid,e.warehouse2approval_gid, a.warehouse_ref_no, a.warehouse_name, d.product_name," +
                     " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                      " concat(g.user_firstname,' ',g.user_lastname,' / ',g.user_code) as updated_by," +
                     " CASE WHEN(a.productapproval_flag = 'N' and a.pmgapproval_flag = 'N')  THEN 'Product Approval Pending' " +
                       " WHEN(a.productapproval_flag = 'Y' and a.pmgapproval_flag = 'N') THEN 'PMG Approval Pending' " +
                       " WHEN(a.productapproval_flag = 'R' and a.pmgapproval_flag = 'N') THEN 'Product Approval - Rejected' " +
                       " WHEN(a.productapproval_flag = 'Y' and a.pmgapproval_flag = 'R') THEN 'PMG Approval - Rejected' " +
                       " ELSE '-' END as approval_status , " +
                     " date_format(a.warehouse_approveddate,'%d-%m-%Y %h:%i %p') as warehouse_approveddate, " +
                      " date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                     " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date " +
                     "  from agr_mst_twarehouse a" +
                     " left join agr_trn_twarehouse2approval e on a.warehouse_gid = e.warehouse_gid" +
                     " left join agr_mst_twarehouse2commodity d on a.warehouse_gid = d.warehouse_gid" +
                     " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                     " left join adm_mst_tuser c on c.user_gid=b.user_gid  " +
                     " left join hrm_mst_temployee f on f.employee_gid=a.updated_by " +
                     " left join adm_mst_tuser g on g.user_gid=f.user_gid where " +
                     "  (a.pmgapproval_flag='Y' and a.productapproval_flag='Y') group by warehouse_gid order by warehouse_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getbuyerbank_list = new List<PendingProductApprovaldtl>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getbuyerbank_list.Add(new PendingProductApprovaldtl
                        {
                            warehouse_gid = (dr_datarow["warehouse_gid"].ToString()),
                            warehouse_ref_no = (dr_datarow["warehouse_ref_no"].ToString()),
                            warehouse_name = (dr_datarow["warehouse_name"].ToString()),
                            product_name = (dr_datarow["product_name"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            warehouse2approval_gid = (dr_datarow["warehouse2approval_gid"].ToString()),
                            warehouse_approveddate = (dr_datarow["warehouse_approveddate"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),

                        });
                    }
                    values.PendingProductApprovaldtl = getbuyerbank_list;
                }
                dt_datatable.Dispose();


            }

            else { }

        }

        public void DaGetWarehouseApprovalCount(string employee_gid, WarehouseCountdtl values)
        {

            msSQL = "select pmgapproval_gid from agr_mst_pmgapproval   where pmgapproval_gid = '" + employee_gid + "'";
            values.pmgapproval = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select productapproval_gid  from agr_mst_productapproval where productapproval_gid = '" + employee_gid + "'";
            values.productapproval = objdbconn.GetExecuteScalar(msSQL);


            if (!string.IsNullOrEmpty(values.productapproval) && !string.IsNullOrEmpty(values.pmgapproval))
            {

                msSQL = " select(select count(*) from agr_mst_twarehouse a " +
                    " left join agr_trn_twarehouse2approval b on a.warehouse_gid = b.warehouse_gid " +
                    " where (a.productapproval_flag='N' or a.productapproval_flag='Y')  and a.pmgapproval_flag='N' and mstproductapproval_gid is not null " +
                    " and warehousesubmit_flag = '" + getWarehouseStatusClass.Pending + "' and b.approval_gid = '" + employee_gid + "') as PendingProduct_Warehouse, " +
                    " (select count(*) from agr_mst_twarehouse a " +
                    " left join agr_trn_twarehouse2approval b on a.warehouse_gid = b.warehouse_gid " +
                     " where (a.productapproval_flag='N' or a.productapproval_flag='Y' )  and a.pmgapproval_flag='N' and mstproductapproval_gid is not null  " +
                    " and warehousesubmit_flag = '" + getWarehouseStatusClass.Pending + "' and b.approval_gid = '" + employee_gid + "') as PendingPMG_Warehouse, " +
                    " (select count(DISTINCT a.warehouse_gid) from agr_mst_twarehouse a" +
                    " left join agr_trn_twarehouse2approval b on a.warehouse_gid = b.warehouse_gid" +
                    " where warehousesubmit_flag = '" + getWarehouseStatusClass.Rejected + "') as Rejected_Warehouse," +
                    " (select count(DISTINCT a.warehouse_gid) from agr_mst_twarehouse a" +
                    " left  join agr_trn_twarehouse2approval b on a.warehouse_gid = b.warehouse_gid" +
                    " where a.pmgapproval_flag = 'Y' and a.productapproval_flag = 'Y' ) as Approved_Warehouse";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.PendingProduct_Warehouse = objODBCDatareader["PendingProduct_Warehouse"].ToString();
                    values.PendingPMG_Warehouse = objODBCDatareader["PendingPMG_Warehouse"].ToString();
                    values.Approved_Warehouse = objODBCDatareader["Approved_Warehouse"].ToString();
                    values.Rejected_Warehouse = objODBCDatareader["Rejected_Warehouse"].ToString();
                }
                objODBCDatareader.Close();
            }

           else  if (!string.IsNullOrEmpty(values.pmgapproval) )
            {

                msSQL = " select(select count(*) from agr_mst_twarehouse a " +
                    " left join agr_trn_twarehouse2approval b on a.warehouse_gid = b.warehouse_gid " +
                    " where (a.productapproval_flag='N' )  and a.pmgapproval_flag='N' and mstproductapproval_gid is not null " +
                    " and warehousesubmit_flag = '" + getWarehouseStatusClass.Pending + "' and b.approval_gid = '" + employee_gid + "') as PendingProduct_Warehouse, " +
                    " (select count(*) from agr_mst_twarehouse a " +
                    " left join agr_trn_twarehouse2approval b on a.warehouse_gid = b.warehouse_gid " +
                     " where (a.productapproval_flag='N' or a.productapproval_flag='Y' )  and a.pmgapproval_flag='N' and mstproductapproval_gid is not null  " +
                    " and warehousesubmit_flag = '" + getWarehouseStatusClass.Pending + "' and b.approval_gid = '" + employee_gid + "') as PendingPMG_Warehouse, " +
                    " (select count(DISTINCT a.warehouse_gid) from agr_mst_twarehouse a" +
                    " left join agr_trn_twarehouse2approval b on a.warehouse_gid = b.warehouse_gid" +
                    " where warehousesubmit_flag = '" + getWarehouseStatusClass.Rejected + "') as Rejected_Warehouse," +
                    " (select count(DISTINCT a.warehouse_gid) from agr_mst_twarehouse a" +
                    " left  join agr_trn_twarehouse2approval b on a.warehouse_gid = b.warehouse_gid" +
                    " where a.pmgapproval_flag = 'Y' and a.productapproval_flag = 'Y' ) as Approved_Warehouse";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.PendingProduct_Warehouse = "0";
                    values.PendingPMG_Warehouse = objODBCDatareader["PendingPMG_Warehouse"].ToString();
                    values.Approved_Warehouse = objODBCDatareader["Approved_Warehouse"].ToString();
                    values.Rejected_Warehouse = objODBCDatareader["Rejected_Warehouse"].ToString();
                }
                objODBCDatareader.Close();
            }

           else if ( !string.IsNullOrEmpty(values.productapproval))
            {

                msSQL = " select(select count(*) from agr_mst_twarehouse a " +
                    " left join agr_trn_twarehouse2approval b on a.warehouse_gid = b.warehouse_gid " +
                    " where (a.productapproval_flag='N' or a.productapproval_flag='Y' )  and a.pmgapproval_flag='N' and mstproductapproval_gid is not null " +
                    " and warehousesubmit_flag = '" + getWarehouseStatusClass.Pending + "' and b.approval_gid = '" + employee_gid + "') as PendingProduct_Warehouse, " +
                    " (select count(*) from agr_mst_twarehouse a " +
                    " left join agr_trn_twarehouse2approval b on a.warehouse_gid = b.warehouse_gid " +
                     " where (a.productapproval_flag='N' or a.productapproval_flag='Y' )  and a.pmgapproval_flag='N' and mstproductapproval_gid is not null  " +
                    " and warehousesubmit_flag = '" + getWarehouseStatusClass.Pending + "' and b.approval_gid = '" + employee_gid + "') as PendingPMG_Warehouse, " +
                    " (select count(DISTINCT a.warehouse_gid) from agr_mst_twarehouse a" +
                    " left join agr_trn_twarehouse2approval b on a.warehouse_gid = b.warehouse_gid" +
                    " where warehousesubmit_flag = '" + getWarehouseStatusClass.Rejected + "') as Rejected_Warehouse," +
                    " (select count(DISTINCT a.warehouse_gid) from agr_mst_twarehouse a" +
                    " left  join agr_trn_twarehouse2approval b on a.warehouse_gid = b.warehouse_gid" +
                    " where a.pmgapproval_flag = 'Y' and a.productapproval_flag = 'Y' ) as Approved_Warehouse";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.PendingProduct_Warehouse = objODBCDatareader["PendingProduct_Warehouse"].ToString();
                    values.PendingPMG_Warehouse = "0";
                    values.Approved_Warehouse = objODBCDatareader["Approved_Warehouse"].ToString();
                    values.Rejected_Warehouse = objODBCDatareader["Rejected_Warehouse"].ToString();
                }
                objODBCDatareader.Close();
            }

            else
            {

                values.PendingProduct_Warehouse = "0";
                values.PendingPMG_Warehouse = "0";
                values.Approved_Warehouse = "0";
                values.Rejected_Warehouse = "0";

            }

        }

        public void DaGetUpcomingApprovalSummary(string employee_gid, PendingProductApprovallist values)
        {
            msSQL = "select pmgapproval_gid from agr_mst_pmgapproval   where pmgapproval_gid = '" + employee_gid + "'";
            values.pmgapproval = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select productapproval_gid  from agr_mst_productapproval where productapproval_gid = '" + employee_gid + "'";
            values.productapproval = objdbconn.GetExecuteScalar(msSQL);


            if (!string.IsNullOrEmpty(values.pmgapproval))
            {

                msSQL = " select a.warehouse_gid, a.warehouse_ref_no, a.warehouse_name, d.product_name," +
                  " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                  " CASE WHEN(a.productapproval_flag = 'N' and a.pmgapproval_flag = 'N')  THEN 'Product Approval Pending' " +
                    //" WHEN(a.productapproval_flag = 'Y' and a.pmgapproval_flag = 'N') THEN 'PMG Approval Pending' " +
                    //" WHEN(a.productapproval_flag = 'R' and a.pmgapproval_flag = 'N') THEN 'Product Approval - Rejected' " +
                    //" WHEN(a.productapproval_flag = 'Y' and a.pmgapproval_flag = 'R') THEN 'PMG Approval - Rejected' " +
                    " ELSE '-' END as approval_status , " +
                  " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,e.warehouse2approval_gid  " +
                  "  from agr_mst_twarehouse a" +
                  " left join agr_trn_twarehouse2approval e on a.warehouse_gid = e.warehouse_gid" +
                  " left join agr_mst_twarehouse2commodity d on a.warehouse_gid = d.warehouse_gid" +
                  " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                  " left join adm_mst_tuser c on c.user_gid=b.user_gid where e.approval_gid='" + employee_gid + "' " +
                  " and (a.productapproval_flag='N' ) and a.pmgapproval_flag='N' and mstproductapproval_gid is not null  and warehousesubmit_flag='" + getWarehouseStatusClass.Pending + "' " +
                  " group by warehouse_gid order by warehouse_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getbuyerbank_list = new List<PendingProductApprovaldtl>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getbuyerbank_list.Add(new PendingProductApprovaldtl
                        {
                            warehouse_gid = (dr_datarow["warehouse_gid"].ToString()),
                            warehouse_ref_no = (dr_datarow["warehouse_ref_no"].ToString()),
                            warehouse_name = (dr_datarow["warehouse_name"].ToString()),
                            product_name = (dr_datarow["product_name"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            warehouse2approval_gid = (dr_datarow["warehouse2approval_gid"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),

                        });
                    }
                    values.PendingProductApprovaldtl = getbuyerbank_list;
                }
                dt_datatable.Dispose();

            }

            else { }
        }


        public void DaGetProductbasedWarehouseApprovalCount(string employee_gid, WarehouseCountdtl values)
        {

            msSQL = "select pmgapproval_gid from agr_mst_pmgapproval   where pmgapproval_gid = '" + employee_gid + "'";
            values.pmgapproval = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select productapproval_gid  from agr_mst_productapproval where productapproval_gid = '" + employee_gid + "'";
            values.productapproval = objdbconn.GetExecuteScalar(msSQL);


            if (!string.IsNullOrEmpty(values.productapproval) && !string.IsNullOrEmpty(values.pmgapproval))
            {

                msSQL = " select(select count(*) from agr_mst_twarehouse a " +
                    " left join agr_trn_twarehouse2approval b on a.warehouse_gid = b.warehouse_gid " +
                    " where (a.productapproval_flag='N' or a.productapproval_flag='Y')  and a.pmgapproval_flag='N' and mstproductapproval_gid is not null " +
                    " and warehousesubmit_flag = '" + getWarehouseStatusClass.Pending + "' and b.approval_gid = '" + employee_gid + "') as PendingProduct_Warehouse, " +
                    " (select count(*) from agr_mst_twarehouse a " +
                    " left join agr_trn_twarehouse2approval b on a.warehouse_gid = b.warehouse_gid " +
                     " where (a.productapproval_flag='N' or a.productapproval_flag='Y' )  and a.pmgapproval_flag='N' and mstproductapproval_gid is not null  " +
                    " and warehousesubmit_flag = '" + getWarehouseStatusClass.Pending + "' and b.approval_gid = '" + employee_gid + "') as PendingPMG_Warehouse, " +
                    " (select count(DISTINCT a.warehouse_gid) from agr_mst_twarehouse a" +
                    " left join agr_trn_twarehouse2approval b on a.warehouse_gid = b.warehouse_gid" +
                    " where warehousesubmit_flag = '" + getWarehouseStatusClass.Rejected + "') as Rejected_Warehouse," +
                    " (select count(DISTINCT a.warehouse_gid) from agr_mst_twarehouse a" +
                    " left  join agr_trn_twarehouse2approval b on a.warehouse_gid = b.warehouse_gid" +
                    " where a.pmgapproval_flag = 'Y' and a.productapproval_flag = 'Y' ) as Approved_Warehouse";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.PendingProduct_Warehouse = objODBCDatareader["PendingProduct_Warehouse"].ToString();
                    values.PendingPMG_Warehouse = objODBCDatareader["PendingPMG_Warehouse"].ToString();
                    values.Approved_Warehouse = objODBCDatareader["Approved_Warehouse"].ToString();
                    values.Rejected_Warehouse = objODBCDatareader["Rejected_Warehouse"].ToString();
                }
                objODBCDatareader.Close();
            }


           else if (!string.IsNullOrEmpty(values.productapproval))
            {

                msSQL = " select(select count(*) from agr_mst_twarehouse a " +
                    " left join agr_trn_twarehouse2approval b on a.warehouse_gid = b.warehouse_gid " +
                    " where (a.productapproval_flag='N' or a.productapproval_flag='Y')  and a.pmgapproval_flag='N' and mstproductapproval_gid is not null " +
                    " and warehousesubmit_flag = '" + getWarehouseStatusClass.Pending + "' and b.approval_gid = '" + employee_gid + "') as PendingProduct_Warehouse, " +
                    " (select count(*) from agr_mst_twarehouse a " +
                    " left join agr_trn_twarehouse2approval b on a.warehouse_gid = b.warehouse_gid " +
                     " where (a.productapproval_flag='N' or a.productapproval_flag='Y' )  and a.pmgapproval_flag='N' and mstproductapproval_gid is not null  " +
                    " and warehousesubmit_flag = '" + getWarehouseStatusClass.Pending + "' and b.approval_gid = '" + employee_gid + "') as PendingPMG_Warehouse, " +
                    " (select count(DISTINCT a.warehouse_gid) from agr_mst_twarehouse a" +
                    " left join agr_trn_twarehouse2approval b on a.warehouse_gid = b.warehouse_gid" +
                    " where warehousesubmit_flag = '" + getWarehouseStatusClass.Rejected + "') as Rejected_Warehouse," +
                    " (select count(DISTINCT a.warehouse_gid) from agr_mst_twarehouse a" +
                    " left  join agr_trn_twarehouse2approval b on a.warehouse_gid = b.warehouse_gid" +
                    " where a.pmgapproval_flag = 'Y' and a.productapproval_flag = 'Y' ) as Approved_Warehouse";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.PendingProduct_Warehouse = objODBCDatareader["PendingProduct_Warehouse"].ToString();
                    values.PendingPMG_Warehouse = "0";
                    values.Approved_Warehouse = objODBCDatareader["Approved_Warehouse"].ToString();
                    values.Rejected_Warehouse = objODBCDatareader["Rejected_Warehouse"].ToString();
                }
                objODBCDatareader.Close();
            }

            else
            {

                msSQL = " select(select count(*) from agr_mst_twarehouse a " +
                    " left join agr_trn_twarehouse2approval b on a.warehouse_gid = b.warehouse_gid " +
                    " where (a.productapproval_flag='N' or a.productapproval_flag='Y')  and a.pmgapproval_flag='N' and mstproductapproval_gid is not null " +
                    " and warehousesubmit_flag = '" + getWarehouseStatusClass.Pending + "' and b.approval_gid = '" + employee_gid + "') as PendingProduct_Warehouse, " +
                    " (select count(*) from agr_mst_twarehouse a " +
                    " left join agr_trn_twarehouse2approval b on a.warehouse_gid = b.warehouse_gid " +
                    " where (a.productapproval_flag='N' or a.productapproval_flag='Y' )  and a.pmgapproval_flag='N' and mstproductapproval_gid is not null  " +
                    " and warehousesubmit_flag = '" + getWarehouseStatusClass.Pending + "' and b.approval_gid = '" + employee_gid + "') as PendingPMG_Warehouse, " +
                    " (select count(DISTINCT a.warehouse_gid) from agr_mst_twarehouse a" +
                    " left join agr_trn_twarehouse2approval b on a.warehouse_gid = b.warehouse_gid" +
                    " where warehousesubmit_flag = '" + getWarehouseStatusClass.Rejected + "') as Rejected_Warehouse," +
                    " (select count(DISTINCT a.warehouse_gid) from agr_mst_twarehouse a" +
                    " left  join agr_trn_twarehouse2approval b on a.warehouse_gid = b.warehouse_gid" +
                    " where a.pmgapproval_flag = 'Y' and a.productapproval_flag = 'Y' ) as Approved_Warehouse";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {

                    values.PendingProduct_Warehouse = "0";
                    values.PendingPMG_Warehouse = objODBCDatareader["PendingPMG_Warehouse"].ToString();
                    values.Approved_Warehouse = objODBCDatareader["Approved_Warehouse"].ToString();
                    values.Rejected_Warehouse = objODBCDatareader["Rejected_Warehouse"].ToString();

                }
                objODBCDatareader.Close();
            }

            }

        public void DaGetPmgbasedWarehouseApprovalCount(string employee_gid, WarehouseCountdtl values)
        {

            msSQL = "select pmgapproval_gid from agr_mst_pmgapproval   where pmgapproval_gid = '" + employee_gid + "'";
            values.pmgapproval = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select productapproval_gid  from agr_mst_productapproval where productapproval_gid = '" + employee_gid + "'";
            values.productapproval = objdbconn.GetExecuteScalar(msSQL);

            if (!string.IsNullOrEmpty(values.productapproval) && !string.IsNullOrEmpty(values.pmgapproval))
            {

                msSQL = " select(select count(*) from agr_mst_twarehouse a " +
                    " left join agr_trn_twarehouse2approval b on a.warehouse_gid = b.warehouse_gid " +
                    " where (a.productapproval_flag='N' or a.productapproval_flag='Y')  and a.pmgapproval_flag='N' and mstproductapproval_gid is not null " +
                    " and warehousesubmit_flag = '" + getWarehouseStatusClass.Pending + "' and b.approval_gid = '" + employee_gid + "') as PendingProduct_Warehouse, " +
                    " (select count(*) from agr_mst_twarehouse a " +
                    " left join agr_trn_twarehouse2approval b on a.warehouse_gid = b.warehouse_gid " +
                     " where (a.productapproval_flag='N' or a.productapproval_flag='Y' )  and a.pmgapproval_flag='N' and mstproductapproval_gid is not null  " +
                    " and warehousesubmit_flag = '" + getWarehouseStatusClass.Pending + "' and b.approval_gid = '" + employee_gid + "') as PendingPMG_Warehouse, " +
                    " (select count(DISTINCT a.warehouse_gid) from agr_mst_twarehouse a" +
                    " left join agr_trn_twarehouse2approval b on a.warehouse_gid = b.warehouse_gid" +
                    " where warehousesubmit_flag = '" + getWarehouseStatusClass.Rejected + "') as Rejected_Warehouse," +
                    " (select count(DISTINCT a.warehouse_gid) from agr_mst_twarehouse a" +
                    " left  join agr_trn_twarehouse2approval b on a.warehouse_gid = b.warehouse_gid" +
                    " where a.pmgapproval_flag = 'Y' and a.productapproval_flag = 'Y' ) as Approved_Warehouse";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.PendingProduct_Warehouse = objODBCDatareader["PendingProduct_Warehouse"].ToString();
                    values.PendingPMG_Warehouse = objODBCDatareader["PendingPMG_Warehouse"].ToString();
                    values.Approved_Warehouse = objODBCDatareader["Approved_Warehouse"].ToString();
                    values.Rejected_Warehouse = objODBCDatareader["Rejected_Warehouse"].ToString();
                }
                objODBCDatareader.Close();
            }


           else if (!string.IsNullOrEmpty(values.pmgapproval))
            {

                msSQL = " select(select count(*) from agr_mst_twarehouse a " +
                    " left join agr_trn_twarehouse2approval b on a.warehouse_gid = b.warehouse_gid " +
                    " where (a.productapproval_flag='N' or a.productapproval_flag='Y' )  and a.pmgapproval_flag='N' and mstproductapproval_gid is not null " +
                    " and warehousesubmit_flag = '" + getWarehouseStatusClass.Pending + "' and b.approval_gid = '" + employee_gid + "') as PendingProduct_Warehouse, " +
                    " (select count(*) from agr_mst_twarehouse a " +
                    " left join agr_trn_twarehouse2approval b on a.warehouse_gid = b.warehouse_gid " +
                    " where (a.productapproval_flag='N' or a.productapproval_flag='Y' )  and a.pmgapproval_flag='N' and mstproductapproval_gid is not null  " +
                    " and warehousesubmit_flag = '" + getWarehouseStatusClass.Pending + "' and b.approval_gid = '" + employee_gid + "') as PendingPMG_Warehouse, " +
                    " (select count(DISTINCT a.warehouse_gid) from agr_mst_twarehouse a" +
                    " left join agr_trn_twarehouse2approval b on a.warehouse_gid = b.warehouse_gid" +
                    " where warehousesubmit_flag = '" + getWarehouseStatusClass.Rejected + "') as Rejected_Warehouse," +
                    " (select count(DISTINCT a.warehouse_gid) from agr_mst_twarehouse a" +
                    " left  join agr_trn_twarehouse2approval b on a.warehouse_gid = b.warehouse_gid" +
                    " where a.pmgapproval_flag = 'Y' and a.productapproval_flag = 'Y' ) as Approved_Warehouse";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.PendingProduct_Warehouse = objODBCDatareader["PendingProduct_Warehouse"].ToString();
                    values.PendingPMG_Warehouse = "0";
                    values.Approved_Warehouse = objODBCDatareader["Approved_Warehouse"].ToString();
                    values.Rejected_Warehouse = objODBCDatareader["Rejected_Warehouse"].ToString();
                }
                objODBCDatareader.Close();
            }

            else
            {

                msSQL = " select(select count(*) from agr_mst_twarehouse a " +
                    " left join agr_trn_twarehouse2approval b on a.warehouse_gid = b.warehouse_gid " +
                    " where (a.productapproval_flag='N' or a.productapproval_flag='Y' )  and a.pmgapproval_flag='N' and mstproductapproval_gid is not null " +
                    " and warehousesubmit_flag = '" + getWarehouseStatusClass.Pending + "' and b.approval_gid = '" + employee_gid + "') as PendingProduct_Warehouse, " +
                    " (select count(*) from agr_mst_twarehouse a " +
                    " left join agr_trn_twarehouse2approval b on a.warehouse_gid = b.warehouse_gid " +
                     " where (a.productapproval_flag='N' or a.productapproval_flag='Y' )  and a.pmgapproval_flag='N' and mstproductapproval_gid is not null  " +
                    " and warehousesubmit_flag = '" + getWarehouseStatusClass.Pending + "' and b.approval_gid = '" + employee_gid + "') as PendingPMG_Warehouse, " +
                    " (select count(DISTINCT a.warehouse_gid) from agr_mst_twarehouse a" +
                    " left join agr_trn_twarehouse2approval b on a.warehouse_gid = b.warehouse_gid" +
                    " where warehousesubmit_flag = '" + getWarehouseStatusClass.Rejected + "') as Rejected_Warehouse," +
                    " (select count(DISTINCT a.warehouse_gid) from agr_mst_twarehouse a" +
                    " left  join agr_trn_twarehouse2approval b on a.warehouse_gid = b.warehouse_gid" +
                    " where a.pmgapproval_flag = 'Y' and a.productapproval_flag = 'Y' ) as Approved_Warehouse";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {

                    values.PendingProduct_Warehouse = objODBCDatareader["PendingProduct_Warehouse"].ToString();
                    values.PendingPMG_Warehouse = "0";
                    values.Approved_Warehouse = objODBCDatareader["Approved_Warehouse"].ToString();
                    values.Rejected_Warehouse = objODBCDatareader["Rejected_Warehouse"].ToString();

                }

            }

        }

    }
}