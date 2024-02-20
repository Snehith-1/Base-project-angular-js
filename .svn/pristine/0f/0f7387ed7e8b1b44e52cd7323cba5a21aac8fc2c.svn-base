using ems.mastersamagro.Models;
using ems.utilities.Functions;
using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.Odbc;
using System.Configuration;
using ems.storage.Functions;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System.Globalization;
using OfficeOpenXml;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;
using System.Linq;

namespace ems.mastersamagro.DataAccess
{
    /// <summary>
    /// This DataAccess provide access for various Single fields and Mutliple events ( View, Download and Approvals) in Warehouse Master Add Page.
    /// </summary>
    /// <remarks>Written by Sherin Augusta , PremChandar.K</remarks>
    public class DaAgrMstWarehouseView
    {

        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader, objODBCDatareader1;
        HttpPostedFile httpPostedFile;
        DataTable dt_datatable;
        string msSQL, msGetGid, msGetGid1, msGetDocumentGid, lsapproval_flag, lsproduct_flag, lsmstpmgapproval, lsmstproductapproval, lscreatedby_name;
        int mnResult;

        public void DawarehouseGSTView(string employee_gid, string warehouse_gid, MdlagrmstGST values)
        {
            msSQL = "select warehouse2branch_gid,gst_state,gst_no, gst_registered,headoffice_status from agr_mst_twarehouse2branch " +
                " where warehouse_gid='" + warehouse_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstgst_list = new List<agrmstgst_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstgst_list.Add(new agrmstgst_list
                    {
                        warehouse2branch_gid = (dr_datarow["warehouse2branch_gid"].ToString()),
                        gst_state = (dr_datarow["gst_state"].ToString()),
                        gst_no = (dr_datarow["gst_no"].ToString()),
                        gst_registered = (dr_datarow["gst_registered"].ToString()),
                        headoffice_status = (dr_datarow["headoffice_status"].ToString())

                    });
                }
                values.agrmstgst_list = getmstgst_list;
            }
            dt_datatable.Dispose();
        }

        public void DawarehouseMobileNoView(string employee_gid, string warehouse_gid, MdlagrmstMobileNo values)
        {
            msSQL = "select mobile_no,warehouse2mobileno_gid,primary_status,whatsapp_no from agr_mst_twarehouse2mobileno where " +
              " warehouse_gid='" + warehouse_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstmobileno_list = new List<agrmstmobileno_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstmobileno_list.Add(new agrmstmobileno_list
                    {
                        warehouse2mobileno_gid = (dr_datarow["warehouse2mobileno_gid"].ToString()),
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        whatsapp_no = (dr_datarow["whatsapp_no"].ToString()),
                    });
                }
                values.agrmstmobileno_list = getmstmobileno_list;
            }
            dt_datatable.Dispose();
        }

        public void DawarehouseEmailAddressView(string employee_gid, string warehouse_gid, MdlagrmstEmailAddress values)
        {
            msSQL = "select email_address,warehouse2email_gid,primary_status from agr_mst_twarehouse2email " +
                " where warehouse_gid='" + warehouse_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstemailaddress_list = new List<agrmstemailaddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstemailaddress_list.Add(new agrmstemailaddress_list
                    {
                        warehouse2email_gid = (dr_datarow["warehouse2email_gid"].ToString()),
                        email_address = (dr_datarow["email_address"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString())
                    });
                }
                values.agrmstemailaddress_list = getmstemailaddress_list;
            }
            dt_datatable.Dispose();
        }

        public void DawarehouseAddressView(string employee_gid, string warehouse_gid, MdlagrmstAddressDetails values)
        {
            msSQL = "  select warehouse2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka,city, district, state, country, latitude, longitude, landmark," +
                    " postal_code from agr_mst_twarehouse2address where warehouse_gid='" + warehouse_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstaddress_list = new List<agrmstaddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstaddress_list.Add(new agrmstaddress_list
                    {
                        warehouse2address_gid = (dr_datarow["warehouse2address_gid"].ToString()),
                        address_type = (dr_datarow["addresstype_name"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        addressline1 = (dr_datarow["addressline1"].ToString()),
                        addressline2 = (dr_datarow["addressline2"].ToString()),
                        taluka = (dr_datarow["taluka"].ToString()),
                        district = (dr_datarow["district"].ToString()),
                        state = (dr_datarow["state"].ToString()),
                        country = (dr_datarow["country"].ToString()),
                        latitude = (dr_datarow["latitude"].ToString()),
                        longitude = (dr_datarow["longitude"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString()),
                        city = (dr_datarow["city"].ToString()),
                        landmark = (dr_datarow["landmark"].ToString())
                    });
                }
                values.agrmstaddress_list = getmstaddress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaGetWarehouseSpocView(string employee_gid, string warehouse_gid, Mdlagrmstspoc values)
        {
            msSQL = "select warehouse2spoc_gid, warehouse_gid, spoc_id, spocmobile_no, spoc_name" +
                    " from agr_mst_twarehouse2spoc  where warehouse_gid='" + warehouse_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbuyerbank_list = new List<Warehousespoc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbuyerbank_list.Add(new Warehousespoc_list
                    {
                        spoc_name = (dr_datarow["spoc_name"].ToString()),
                        spocmobile_no = (dr_datarow["spocmobile_no"].ToString()),

                    });
                }
                values.Warehousespoc_list = getbuyerbank_list;
            }
            dt_datatable.Dispose();
        }


        public void DaGetWarehouseAgreementDetailsView(string employee_gid, string warehouse_gid, MdlAgrMstWarhouseAdd values)
        {
            msSQL = " select warehouse2agreement_gid, warehouse_gid, warehouse2address_gid, warehouseagreement_address," +
                    " date_format(execution_date,'%d-%m-%Y') as execution_date, date_format(expiry_date,'%d-%m-%Y') as expiry_date " +
                    " from agr_mst_twarehouse2agreement  where warehouse_gid='" + warehouse_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbuyerbank_list = new List<Mdlagrmstagreementdtllist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbuyerbank_list.Add(new Mdlagrmstagreementdtllist
                    {
                        warehouse2agreement_gid = (dr_datarow["warehouse2agreement_gid"].ToString()),
                        warehouse_gid = (dr_datarow["warehouse_gid"].ToString()),
                        warehouse2address_gid = (dr_datarow["warehouse2address_gid"].ToString()),
                        warehouseagreement_address = (dr_datarow["warehouseagreement_address"].ToString()),
                        expiry_date = (dr_datarow["expiry_date"].ToString()),
                        execution_date = (dr_datarow["execution_date"].ToString()),

                    });
                }
                values.Mdlagrmstagreementdtllist = getbuyerbank_list;
            }
            dt_datatable.Dispose();
        }


        public void DaGetWarehouseCommodityView(string employee_gid, string warehouse_gid, MdlWarehouseSectorcategory values)
        {
            msSQL = "select warehouse2commodity_gid,product_gid,product_name,sector_name,category_name,variety_gid,variety_name, botanical_name, alternative_name from agr_mst_twarehouse2commodity where " +
              " warehouse_gid='" + warehouse_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbuyerbank_list = new List<Warehousevarietyname_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbuyerbank_list.Add(new Warehousevarietyname_list
                    {
                        warehouse2commodity_gid = (dr_datarow["warehouse2commodity_gid"].ToString()),
                        product_gid = (dr_datarow["product_gid"].ToString()),
                        product_name = (dr_datarow["product_name"].ToString()),
                        sector_name = (dr_datarow["sector_name"].ToString()),
                        category_name = (dr_datarow["category_name"].ToString()),
                        variety_gid = (dr_datarow["variety_gid"].ToString()),
                        variety_name = (dr_datarow["variety_name"].ToString()),
                        botanical_name = (dr_datarow["botanical_name"].ToString()),
                        alternative_name = (dr_datarow["alternative_name"].ToString()),
                    });
                }
                values.Warehousevarietyname_list = getbuyerbank_list;
            }
            dt_datatable.Dispose();
        }


        public void DaWarehouseDocumentUploadView(string employee_gid, string warehouse_gid, MdlAgrMstWarhouseAdd values)
        {
            msSQL = " select warehouse2docupload_gid,warehouse_gid,document_name,document_path,document_title,warehouseagreement_gid from " +
                       " agr_mst_twarehouse2docupload where warehouse_gid='" + warehouse_gid + "'";
            //" agr_mst_twarehouse2docupload where (warehouse_gid='" + warehouse_gid + "') and warehouseagreement_gid ='" + warehouseagreement_gid"'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcamdocument_list = new List<agrmstwarhouse_upload>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcamdocument_list.Add(new agrmstwarhouse_upload
                    {
                        document_name = dt["document_name"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        warehouse_gid = dt["warehouse_gid"].ToString(),
                        warehouse2docupload_gid = dt["warehouse2docupload_gid"].ToString(),
                        warehouseagreement_gid = dt["warehouseagreement_gid"].ToString(),


                    });
                    values.agrmstwarhouse_upload = getcamdocument_list;
                }
            }
            dt_datatable.Dispose();


        }


        public void DaGetapprovalflag(string warehouse_gid)
        {
            msSQL = " select warehousesubmit_flag from " +
                      " agr_mst_twarehouse where warehouse_gid='" + warehouse_gid + "'";

            lsapproval_flag = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select productapproval_flag from " +
                      " agr_trn_twarehouse2approval where warehouse_gid='" + warehouse_gid + "'";

            lsproduct_flag = objdbconn.GetExecuteScalar(msSQL);

        }


        public void DaUpdateProductApproval(string employee_gid, Mdlapprovalremark values)
        {
            // Product Approval - updation
            if (values.product_approvalflag == "Y")
            {
                if (values.approvalstatus == "Y")
                {
                    msSQL = " update agr_trn_twarehouse2approval set " +
                            " productapproval_flag='" + values.approvalstatus + "'," +
                            " approval_remarks='" + values.remarks.Replace("'", "") + "'," +
                            " approved_by='" + employee_gid + "'," +
                            " approved_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                            " where warehouse2approval_gid='" + values.warehouse2approval_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    msSQL = " update agr_trn_twarehouse2approval set " +
                           " productapproval_flag='" + values.approvalstatus + "'," +
                           " approval_remarks='" + values.remarks.Replace("'", "") + "'," +
                           " rejected_by='" + employee_gid + "'," +
                           " rejected_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                           " where warehouse2approval_gid='" + values.warehouse2approval_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                if (mnResult != 0)
                {
                    msSQL = " update agr_mst_twarehouse set " +
                          " productapproval_flag='" + values.approvalstatus + "'," +
                          " product_approvedby='" + employee_gid + "'," +
                          " product_approveddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                          " where warehouse_gid='" + values.warehouse_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    if (values.approvalstatus == "Y")
                        values.message = "Warehouse Product Approved Successfully";
                    else
                        values.message = "Warehouse Product Rejected Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred While Approving";
                }

            }
            else
            {
                // PMG Approval - updation

                if (values.approvalstatus == "Y")
                {
                    msSQL = " update agr_trn_twarehouse2approval set " +
                            " pmgapproval_flag='" + values.approvalstatus + "'," +
                            " approval_remarks='" + values.remarks.Replace("'", "") + "'," +
                            " approved_by='" + employee_gid + "'," +
                            " approved_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                            " where warehouse2approval_gid='" + values.warehouse2approval_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    msSQL = " update agr_trn_twarehouse2approval set " +
                           " pmgapproval_flag='" + values.approvalstatus + "'," +
                           " approval_remarks='" + values.remarks.Replace("'", "") + "'," +
                           " rejected_by='" + employee_gid + "'," +
                           " rejected_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                           " where warehouse2approval_gid='" + values.warehouse2approval_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                if (mnResult != 0)
                {
                    msSQL = " update agr_mst_twarehouse set " +
                            " pmgapproval_flag='" + values.approvalstatus + "'," +
                            " pmg_approvedby='" + employee_gid + "'," +
                            " pmg_approveddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                            " where warehouse_gid='" + values.warehouse_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    if (values.approvalstatus == "Y")
                        values.message = "Warehouse PMG Approved Successfully";
                    else
                        values.message = "Warehouse PMG Rejected Successfully"; 
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred While Approving";
                }

            }
            // Overall warehouse Approval - updation
            string lsproductapproval_flag = "", lspmgapproval_flag = "";
            msSQL = "select productapproval_flag, pmgapproval_flag from agr_mst_twarehouse where warehouse_gid='" + values.warehouse_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsproductapproval_flag = objODBCDatareader["productapproval_flag"].ToString();
                lspmgapproval_flag = objODBCDatareader["pmgapproval_flag"].ToString();
            }
            objODBCDatareader.Close();

            if (lsproductapproval_flag == "Y" && lspmgapproval_flag == "Y")
            {
                msSQL = " update agr_mst_twarehouse set " +
                        " warehousesubmit_flag='" + getWarehouseStatusClass.Approved + "'," +
                        " warehouse_approvedflag='Y'," +
                        " warehouse_approveddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where warehouse_gid='" + values.warehouse_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else if (lsproductapproval_flag == "R" || lspmgapproval_flag == "R")
            {
                msSQL = " update agr_mst_twarehouse set " +
                        " warehousesubmit_flag='" + getWarehouseStatusClass.Rejected + "'," +
                        " warehouse_approveddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where warehouse_gid='" + values.warehouse_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }


        }


        public void DaGetProductApprovaldtl(string warehouse_gid, Warehouseapprovaldtl values)
        {
            msSQL = " select approval_name,approval_remarks, case when productapproval_flag='Y' then date_format(approved_date,'%d-%m-%Y %H:%m %p') " +
                 " when productapproval_flag = 'R' then date_format(rejected_date,'%d-%m-%Y %H:%m %p') end as approved_date, " +
                 "  case when productapproval_flag='Y' then 'Approved' when productapproval_flag = 'R' then 'Rejected' else '' end as productapproval_flag " +
                 " from agr_trn_twarehouse2approval where warehouse_gid='" + warehouse_gid + "' and mstproductapproval_gid is not null";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getWarehouseapproval_list = new List<Warehouseapproval_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getWarehouseapproval_list.Add(new Warehouseapproval_list
                    {
                        approval_name = dt["approval_name"].ToString(),
                        approval_remarks = dt["approval_remarks"].ToString(),
                        approved_date = (dt["approved_date"].ToString()),
                        approval_flag = dt["productapproval_flag"].ToString(),
                    });
                    values.Warehouseapproval_list = getWarehouseapproval_list;
                }
            }
            dt_datatable.Dispose();


        }
        public void DaGetPmgApprovaldtl(string warehouse_gid, Warehouseapprovaldtl values)
        {
            msSQL = " select approval_name,approval_remarks, case when pmgapproval_flag='Y' then date_format(approved_date,'%d-%m-%Y %H:%m %p') " +
                   " when pmgapproval_flag = 'R' then date_format(rejected_date,'%d-%m-%Y %H:%m %p') end as approved_date, " +
                   "  case when pmgapproval_flag='Y' then 'Approved' when pmgapproval_flag = 'R' then 'Rejected' else '' end as pmgapproval_flag " +
                   " from agr_trn_twarehouse2approval where warehouse_gid='" + warehouse_gid + "' and mstpmgapproval_gid is not null";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getWarehouseapproval_list = new List<Warehouseapproval_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getWarehouseapproval_list.Add(new Warehouseapproval_list
                    {
                        approval_name = dt["approval_name"].ToString(),
                        approval_remarks = dt["approval_remarks"].ToString(),
                        approved_date = (dt["approved_date"].ToString()),
                        approval_flag = dt["pmgapproval_flag"].ToString(),
                    });
                    values.Warehouseapproval_list = getWarehouseapproval_list;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaPostWarehouseRaiseQuery(mdlwarehouseraisequery values, string user_gid, string employee_gid)
        {

            msSQL = " select concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as createdby_name from adm_mst_tuser a " +
                  " where a.user_gid = '" + user_gid + "'";
            string lscreatedby_name = objdbconn.GetExecuteScalar(msSQL);

            msGetGid = objcmnfunctions.GetMasterGID("WARQ");
            msSQL = "Insert into agr_trn_twarehouse2query( " +
                   " warehouse2query_gid, " +
                   " warehouse_gid," +
                   " query_title," +
                   " query_description," +
                   " productapproval_flag," +
                   " pmgapproval_flag," +
                   " query_from," +
                   " createdby_name," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.warehouse_gid + "', " +
                   "'" + values.query_title.Replace("'", "") + "'," +
                   "'" + values.description.Replace("'", "") + "'," +
                   "'" + values.productapproval_flag + "', " +
                   "'" + values.pmgapproval_flag + "', " +
                   "'" + values.query_from + "', " +
                   "'" + lscreatedby_name + "', " +
                   "'" + user_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {

                msSQL = "update agr_mst_twarehouse set  query_status ='Query Raised' " +
                           " where warehouse_gid='" + values.warehouse_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                values.status = true;
                values.message = "Query Raised Successfully";
            }
            else
            {
                values.message = "Error Occured While Raising Query";
                values.status = false;
            }
        }

        public void DaGetWarehouseRaiseQuerySummary(mdlwarehouseraisequery values, string warehouse_gid)
        {
            msSQL = " select warehouse2query_gid, warehouse_gid, query_title,query_status,query_description,close_remarks, query_from," +
                     " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as created_by, createdby_name, " +
                     " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date from agr_trn_twarehouse2query a" +
                     " left join adm_mst_tuser b on b.user_gid = a.created_by " +
                     " where a.warehouse_gid ='" + warehouse_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getwarehouseraisequerylist = new List<warehouseraisequerylist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getwarehouseraisequerylist.Add(new warehouseraisequerylist
                    {
                        warehouse2query_gid = dt["warehouse2query_gid"].ToString(),
                        query_title = dt["query_title"].ToString(),
                        query_status = dt["query_status"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        query_description = dt["query_description"].ToString(),
                        close_remarks = dt["close_remarks"].ToString(),
                        warehouse_gid = dt["warehouse_gid"].ToString(),
                        createdby_name = dt["createdby_name"].ToString(),
                        query_from = dt["query_from"].ToString(),
                    });
                    values.warehouseraisequerylist = getwarehouseraisequerylist;
                }
            }
            dt_datatable.Dispose();


        }

        public void DaGetOpenQueryStatus(mdlwarehouseraisequery values, string warehouse_gid)
        {

            msSQL = " select warehouse2query_gid from agr_trn_twarehouse2query where warehouse_gid ='" + warehouse_gid + "'" +
                    " and query_status ='Open'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.openquery_flag = "Y";
            }
            else
            {
                values.openquery_flag = "N";
            }
            objODBCDatareader.Close();
        }


        public void DaGetRaiseQuerydesc(mdlwarehouseraisequery values, string warehouse2query_gid)
        {
            msSQL = "select query_title, query_description, close_remarks from agr_trn_twarehouse2query where warehouse2query_gid='" + warehouse2query_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.description = objODBCDatareader["query_description"].ToString();
                values.query_title = objODBCDatareader["query_title"].ToString();
                values.close_remarks = objODBCDatareader["close_remarks"].ToString();
            }
            objODBCDatareader.Close();
        }


        public void DaPostUpdateQueryStatus(mdlwarehouseraisequery values, string user_gid)
        {
            msSQL = " update agr_trn_twarehouse2query set  query_status='Closed', close_remarks='" + values.close_remarks.Replace("'", "") + "'," +
                    " closed_by='" + user_gid + "', closed_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where warehouse2query_gid ='" + values.warehouse2query_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = " select warehouse2query_gid from agr_trn_twarehouse2query where warehouse_gid ='" + values.warehouse_gid + "'" +
                        " and query_status ='Open'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {

                }
                else
                {
                    msSQL = "update agr_mst_twarehouse set  query_status ='Closed' " +
                           " where creditor_gid='" + values.warehouse_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                objODBCDatareader.Close();


                values.status = true;
                values.message = "Query Closed Successfully..!";

            }

            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }

        }


    }
}