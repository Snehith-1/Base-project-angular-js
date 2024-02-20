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
    /// This DataAccess will provide access for various Single fields and Mutliple events (Add, Edit, View, Delete, Upload, Download and Approvals) in Warehouse Master Edit Page
    /// </summary>
    /// <remarks> Written by Premchander.K </remarks>

    public class DaAgrMstWarehouseEdit
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader, objODBCDatareader1;
        HttpPostedFile httpPostedFile;
        DataTable dt_datatable;
        string msSQL, msGetGid, msGetGid1, msGetDocumentGid, msGetWFGid;
        int mnResult;
        string lsaddress_typegid, lswarehouse_gid, lsaddress_type, lspath, spoc_phoneno, lswarehouse2mobileno_gid, lswhatsapp_no, lsmobile_no, lsemail_address, lswarehouse2email_gid, lsaddressline1, lswarehouse2address_gid, lsaddressline2, lsprimary_status, lslandmark, lspostal_code, lscity, lstaluka, lsdistrict, lsstate, lscountry, lslatitude, lslongitude;
        string lsgst_state, lsgst_no, lswarehouse2branch_gid, lsgst_registered, lstypeofwarehouse_gid, lstypeofwarehouse_name, lscreditor_gid, lsApplicant_name;

        string lswarehouse_ref_no, lswarehouse_name, lsowned_by, lswarehouse_pan, lsfirst_name, lsmiddle_name, lslast_name, lssubsidiarywarshouse_name, lswarehouse_area, lswarehousearea_uom, lstotalcapacity_area, lstotalcapacity_volume, lsvolume_uom, lsarea_uom, lscharges, lscapacity;


        public void DaGetWarehouseMobileNoList(string employee_gid, string warehouse_gid, MdlagrmstMobileNo values)
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

        public void DaGetWarehouseEmailAddressList(string employee_gid, string warehouse_gid, MdlagrmstEmailAddress values)
        {
            msSQL = " select email_address,warehouse2email_gid,primary_status from agr_mst_twarehouse2email where warehouse_gid='" + warehouse_gid + "'";
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


        public void DaGetWarehouseAddressList(string employee_gid, string warehouse_gid, MdlagrmstAddressDetails values)
        {
            msSQL = "  select warehouse2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country, landmark, latitude, longitude," +
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
                        postal_code = (dr_datarow["postal_code"].ToString()),
                        landmark = (dr_datarow["landmark"].ToString()),
                        latitude = (dr_datarow["latitude"].ToString()),
                        longitude = (dr_datarow["longitude"].ToString()),
                    });
                }
                values.agrmstaddress_list = getmstaddress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaGetWarehouseCommodity(string employee_gid, string warehouse_gid, MdlWarehouseSectorcategory values)
        {
            msSQL = "select warehouse2commodity_gid,product_gid,product_name,sector_name,category_name,variety_gid,variety_name, botanical_name, alternative_name, hsn_code from agr_mst_twarehouse2commodity where " +
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
                        hsn_code = (dr_datarow["hsn_code"].ToString()),
                    });
                }
                values.Warehousevarietyname_list = getbuyerbank_list;
            }
            dt_datatable.Dispose();
        }
        public void DaUpdateGSTHeadOffice(string employee_gid, MdlGSTwarehouseHeadOffice values)
        {
            msSQL = " update agr_mst_twarehouse2branch set headoffice_status = 'Yes' " +
                    " where warehouse2branch_gid = '" + values.warehouse2branch_gid + "' " +
                    " and (warehouse_gid = '" + employee_gid + "' or warehouse_gid = '" + values.warehouse_gid + "') ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = " update agr_mst_twarehouse2branch set headoffice_status='No' " +
                        " where warehouse2branch_gid<>'" + values.warehouse2branch_gid + "' " +
                        " and (warehouse_gid = '" + employee_gid + "' or warehouse_gid = '" + values.warehouse_gid + "') ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Head Office Confirmed Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }

        }

        public void Dagetwarehousedocument(string warehouse_gid, MdlAgrMstWarhouseAdd values)
        {
            msSQL = " select warehouse2docupload_gid,warehouse_gid,document_name,document_path,document_title from " +
                       " agr_mst_twarehouse2docupload where warehouse_gid='" + warehouse_gid + "'";
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

                    });
                    values.agrmstwarhouse_upload = getcamdocument_list;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaGetWarehouseGSTList(string employee_gid, string warehouse_gid, MdlagrmstGST values)
        {
            msSQL = " select warehouse2branch_gid,gst_state,gst_no, gst_registered from agr_mst_twarehouse2branch where warehouse_gid='" + warehouse_gid + "' ";
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
                        gst_registered = (dr_datarow["gst_registered"].ToString())
                    });
                }
                values.agrmstgst_list = getmstgst_list;
            }
            dt_datatable.Dispose();
        }


        public void DawarehouseGSTTmpList(string employee_gid, string warehouse_gid, MdlagrmstGST values)
        {
            msSQL = "select warehouse2branch_gid,gst_state,gst_no, gst_registered,headoffice_status,warehouse_gid from agr_mst_twarehouse2branch " +
                " where warehouse_gid='" + warehouse_gid + "' or warehouse_gid='" + employee_gid + "'";
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
                        headoffice_status = (dr_datarow["headoffice_status"].ToString()),
                        warehouse_gid = (dr_datarow["warehouse_gid"].ToString())

                    });
                }
                values.agrmstgst_list = getmstgst_list;
            }
            dt_datatable.Dispose();
        }

        public void DawarehouseMobileNoTmpList(string employee_gid, string warehouse_gid, MdlagrmstMobileNo values)
        {
            msSQL = "select mobile_no,warehouse2mobileno_gid,primary_status,whatsapp_no from agr_mst_twarehouse2mobileno where " +
              " warehouse_gid='" + warehouse_gid + "' or warehouse_gid='" + employee_gid + "'";
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

        public void DawarehouseEmailAddressTmpList(string warehouse_gid, string employee_gid, MdlagrmstEmailAddress values)
        {
            msSQL = "select email_address,warehouse2email_gid,primary_status from agr_mst_twarehouse2email " +
                " where warehouse_gid='" + warehouse_gid + "' or warehouse_gid='" + employee_gid + "'";
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

        public void DawarehouseAddressTmpList(string warehouse_gid, string employee_gid, MdlagrmstAddressDetails values)
        {
            msSQL = "  select warehouse2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country, latitude, longitude, landmark," +
                    " postal_code from agr_mst_twarehouse2address where warehouse_gid='" + warehouse_gid + "' or warehouse_gid='" + employee_gid + "'";
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
                        landmark = (dr_datarow["landmark"].ToString())
                    });
                }
                values.agrmstaddress_list = getmstaddress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaGetWarehouseSpocTmpList(string employee_gid, string warehouse_gid, Mdlagrmstspoc values)
        {
            msSQL = "select warehouse2spoc_gid, warehouse_gid, spoc_id, spocmobile_no, spoc_name" +
                    " from agr_mst_twarehouse2spoc  where warehouse_gid='" + warehouse_gid + "' or warehouse_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbuyerbank_list = new List<Warehousespoc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbuyerbank_list.Add(new Warehousespoc_list
                    {
                        warehouse2spoc_gid = (dr_datarow["warehouse2spoc_gid"].ToString()),
                        spoc_name = (dr_datarow["spoc_name"].ToString()),
                        spocmobile_no = (dr_datarow["spocmobile_no"].ToString()),

                    });
                }
                values.Warehousespoc_list = getbuyerbank_list;
            }
            dt_datatable.Dispose();
        }


        public void DaGetWarehouseAgreementDetailsTmpList(string employee_gid, string warehouse_gid, MdlAgrMstWarhouseAdd values)
        {
            msSQL = "select warehouse2agreement_gid, warehouse_gid, warehouse2address_gid, warehouseagreement_address," +
                    " date_format(execution_date,'%d-%m-%Y') as execution_date, date_format(expiry_date,'%d-%m-%Y') as expiry_date " +
                    " from agr_mst_twarehouse2agreement  where warehouse_gid='" + warehouse_gid + "' or warehouse_gid='" + employee_gid + "'";
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

        public void DaGetWarehouseTmpClear(string employee_gid)
        {
            msSQL = " delete from  agr_mst_twarehouse2address where warehouse_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from  agr_mst_twarehouse2address where warehouse_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from  agr_mst_twarehouse2branch where warehouse_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from  agr_mst_twarehouse2mobileno where warehouse_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from  agr_mst_twarehouse2address where warehouse_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from  agr_mst_twarehouse2spoc where warehouse_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from  agr_mst_twarehouse2agreement where warehouse_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from  agr_mst_twarehouse2docupload where warehouse_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from  agr_mst_twarehouse2email where warehouse_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from  agr_mst_twarehouse2commodity where warehouse_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        }


        public void DaGetWarehouseCommodityTmpList(string employee_gid, string warehouse_gid, MdlWarehouseSectorcategory values)
        {
            msSQL = "select warehouse2commodity_gid,product_gid,product_name,sector_name,category_name,variety_gid,variety_name, botanical_name, alternative_name, hsn_code from agr_mst_twarehouse2commodity where " +
              " warehouse_gid='" + warehouse_gid + "' or warehouse_gid='" + employee_gid + "'";
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
                        hsn_code = (dr_datarow["hsn_code"].ToString()),
                    });
                }
                values.Warehousevarietyname_list = getbuyerbank_list;
            }
            dt_datatable.Dispose();
        }


        public void DaWarehouseDocumentUploadTmpList(string warehouse2agreement_gid, string employee_gid, MdlAgrMstWarhouseAdd values)
        {
            msSQL = " select warehouse2docupload_gid,warehouse_gid,document_name,document_path,document_title,warehouseagreement_gid from " +
                       " agr_mst_twarehouse2docupload where warehouseagreement_gid='" + warehouse2agreement_gid + "'";
            //" agr_mst_twarehouse2docupload where (warehouse_gid='" + warehouse_gid + "' or warehouse_gid='" + employee_gid + "') and warehouseagreement_gid ='" + warehouseagreement_gid"'";

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



        public bool DaUpdatewarehouseDtl(MdlAgrMstWarehouseCreation values, string employee_gid)
        {
            string lswarehousearea_uomgid = "", lstotalcapacityarea_uomgid = "", lsvolume_uomgid = "";
            msSQL = "select warehouse_gid from agr_mst_twarehouse2mobileno where (warehouse_gid='" + employee_gid + "' or warehouse_gid='" + values.warehouse_gid + "') and primary_status='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Primary Mobile Number";
                return false;
            }

            msSQL = "select warehouse_gid from agr_mst_twarehouse2mobileno where warehouse_gid='" + employee_gid + "' or warehouse_gid='" + values.warehouse_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Atleast One Mobile Number";
                return false;
            }
            if (values.Gstflag == "Yes")
            {
                //msSQL = "select creditor_gid from agr_mst_tcreditor2branch where creditor_gid='" + employee_gid + "' and headoffice_status ='Yes' ";
                msSQL = "select warehouse2branch_gid from agr_mst_twarehouse2branch where (warehouse_gid='" + employee_gid + "' or warehouse_gid='" + values.warehouse_gid + "') and headoffice_status ='Yes' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == false)
                {
                    values.status = false;
                    values.message = "Atleast Select One GST Number as Head Office";
                    return false;
                }
                objODBCDatareader.Close();
            }

            msSQL = "select warehouse_gid from agr_mst_twarehouse2email where warehouse_gid='" + employee_gid + "' or warehouse_gid='" + values.warehouse_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Atleast One Email Address";
                return false;
            }

            msSQL = "select warehouse_gid from agr_mst_twarehouse2address where warehouse_gid='" + employee_gid + "' or warehouse_gid='" + values.warehouse_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Atleast One Address Detail";
                return false;
            }


            msSQL = " select warehouse_gid, warehouse_ref_no, warehouse_name, owned_by, warehouse_pan, first_name, middle_name, last_name, subsidiarywarshouse_name, " +
                     " warehouse_area, warehousearea_uom, totalcapacity_area, totalcapacity_volume, volume_uom, area_uom, totalcapacity_volume, volume_uom,  " +
                     " charges, capacity, created_by, created_date, warehousearea_uomgid,totalcapacityarea_uomgid,volume_uomgid, typeofwarehouse_name, typeofwarehouse_gid, creditor_gid, Applicant_name " +
                     " from agr_mst_twarehouse where warehouse_gid='" + values.warehouse_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lswarehouse_gid = objODBCDatareader["warehouse_gid"].ToString();
                lswarehouse_ref_no = objODBCDatareader["warehouse_ref_no"].ToString();
                lswarehouse_name = objODBCDatareader["warehouse_name"].ToString();
                lsowned_by = objODBCDatareader["owned_by"].ToString();
                lswarehouse_pan = objODBCDatareader["warehouse_pan"].ToString();
                lsfirst_name = objODBCDatareader["first_name"].ToString();
                lsmiddle_name = objODBCDatareader["middle_name"].ToString();
                lslast_name = objODBCDatareader["last_name"].ToString();
                lssubsidiarywarshouse_name = objODBCDatareader["subsidiarywarshouse_name"].ToString();
                lswarehouse_area = objODBCDatareader["warehouse_area"].ToString();
                lswarehousearea_uom = objODBCDatareader["warehousearea_uom"].ToString();
                lstotalcapacity_area = objODBCDatareader["totalcapacity_area"].ToString();
                lstotalcapacity_volume = objODBCDatareader["totalcapacity_volume"].ToString();
                lsvolume_uom = objODBCDatareader["volume_uom"].ToString();
                lsarea_uom = objODBCDatareader["area_uom"].ToString();

                lscharges = objODBCDatareader["charges"].ToString();
                lscapacity = objODBCDatareader["capacity"].ToString();
                lswarehousearea_uomgid = objODBCDatareader["warehousearea_uomgid"].ToString();
                lstotalcapacityarea_uomgid = objODBCDatareader["totalcapacityarea_uomgid"].ToString();
                lsvolume_uomgid = objODBCDatareader["volume_uomgid"].ToString();
                lstypeofwarehouse_gid = objODBCDatareader["typeofwarehouse_gid"].ToString();
                lstypeofwarehouse_name = objODBCDatareader["typeofwarehouse_name"].ToString();
                lscreditor_gid = objODBCDatareader["creditor_gid"].ToString();
                lsApplicant_name = objODBCDatareader["Applicant_name"].ToString();

            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_twarehouse set " +
                        " warehouse_ref_no='" + values.warehouse_ref_no + "'," +

                         " warehouse_name='" + values.warehouse_name + "'," +
                         //" owned_by='" + values.owned_by + "'," +
                         " warehouse_pan='" + values.warehouse_pan + "'," +
                         " first_name='" + values.first_name + "'," +
                         " middle_name='" + values.middle_name + "'," +
                         " last_name='" + values.last_name + "'," +
                         " subsidiarywarshouse_name='" + values.subsidiarywarshouse_name + "'," +
                         " warehousearea_uomgid='" + values.warehousearea_uomgid + "'," +
                         " warehouse_area='" + values.warehouse_area + "'," +
                         " warehousearea_uom='" + values.warehousearea_uom + "'," +
                         " totalcapacity_area='" + values.totalcapacity_area + "'," +
                         " totalcapacityarea_uomgid='" + values.totalcapacityarea_uomgid + "'," +
                         " totalcapacity_volume='" + values.totalcapacity_volume + "'," +
                         " volume_uomgid='" + values.volume_uomgid + "'," +
                         " volume_uom='" + values.volume_uom + "'," +
                         " area_uom='" + values.area_uom + "'," +
                         " charges='" + values.charges + "'," +
                         " capacity='" + values.capacity + "'," +
                         " typeofwarehouse_gid='" + values.typeofwarehouse_gid + "'," +
                         " typeofwarehouse_name='" + values.typeofwarehouse_name + "'," +
                         " creditor_gid='" + values.creditor_gid +"',"+
                         " Applicant_name='" + values.Applicant_name +"',"+
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where warehouse_gid='" + values.warehouse_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msSQL = "delete from agr_mst_twarehouse2facility where warehouse_gid='" + values.warehouse_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    for (var i = 0; i < values.facility_list.Count; i++)
                    {
                        msGetWFGid = objcmnfunctions.GetMasterGID("W2FG");
                        msSQL = "Insert into agr_mst_twarehouse2facility( " +
                               " warehouse2facility_gid, " +
                               " warehouse_gid," +
                               " warehousefacility_gid," +
                               " warehousefacility_name," +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + msGetWFGid + "'," +
                               "'" + values.warehouse_gid + "'," +
                               "'" + values.facility_list[i].warehousefacility_gid + "'," +
                               "'" + values.facility_list[i].warehousefacility_name + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                    msGetGid = objcmnfunctions.GetMasterGID("AGWU");

                    msSQL = " insert into agr_mst_twarehouseupdatelog(" +
                    " warehouseupdatelog_gid," +
                    " warehouse_gid," +
                    " warehouse_ref_no, " +
                    " warehouse_name," +
                   " owned_by," +
                   " warehouse_pan," +
                   " first_name," +
                   " middle_name," +
                   " last_name," +
                   " subsidiarywarshouse_name," +
                   " warehouse_area," +
                   " warehousearea_uom," +
                   " totalcapacity_area," +
                   " area_uom, " +
                   " totalcapacity_volume," +
                   " volume_uom," +
                   " warehousearea_uomgid, " +
                   " totalcapacityarea_uomgid, " +
                   " volume_uomgid, " +
                   " typeofwarehouse_gid," +
                   " typeofwarehouse_name," +
                   " creditor_gid," +
                   " Applicant_name," +
                   " charges," +
                   " capacity," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                     "'" + msGetGid + "'," +
                      "'" + lswarehouse_gid + "'," +
                      "'" + lswarehouse_ref_no + "'," +
                        "'" + lswarehouse_name + "'," +
                        "'" + lsowned_by + "'," +
                        "'" + lswarehouse_pan + "'," +
                               "'" + lsfirst_name + "'," +
                               "'" + lsmiddle_name + "'," +
                               "'" + lslast_name + "'," +
                               "'" + lssubsidiarywarshouse_name + "'," +
                               "'" + lswarehouse_area + "'," +
                               "'" + lswarehousearea_uom + "'," +
                               "'" + lstotalcapacity_area + "'," +
                               "'" + lsarea_uom + "'," +
                               "'" + lstotalcapacity_volume + "'," +
                               "'" + lsvolume_uom + "'," + 
                               "'" + lswarehousearea_uomgid + "'," +
                               "'" + lstotalcapacityarea_uomgid + "'," +
                               "'" + lsvolume_uomgid + "'," +
                               "'" + lstypeofwarehouse_gid + "'," +
                               "'" + lstypeofwarehouse_name + "'," +
                               "'" + lscreditor_gid + "'," +
                               "'" + lsApplicant_name + "'," +
                               "'" + lscharges + "'," +
                               "'" + lscapacity + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    // Updates for Multiple Add
                    msSQL = "update agr_mst_twarehouse2branch set warehouse_gid='" + values.warehouse_gid + "' where warehouse_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_twarehouse2mobileno set warehouse_gid='" + values.warehouse_gid + "' where warehouse_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_twarehouse2email set warehouse_gid='" + values.warehouse_gid + "' where warehouse_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_twarehouse2address set warehouse_gid='" + values.warehouse_gid + "' where warehouse_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_twarehouse2spoc set warehouse_gid ='" + values.warehouse_gid + "' where warehouse_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_twarehouse2docupload set warehouse_gid='" + values.warehouse_gid + "' where warehouse_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_twarehouse2agreement set warehouse_gid ='" + values.warehouse_gid + "' where warehouse_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_twarehouse2commodity set warehouse_gid ='" + values.warehouse_gid + "' where warehouse_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "select mobile_no from agr_mst_twarehouse2mobileno where warehouse_gid='" + values.warehouse_gid + "' and primary_status='yes'";
                    string lsmobileno = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "select email_address from agr_mst_twarehouse2email where warehouse_gid='" + values.warehouse_gid + "' and primary_status='yes'";
                    lsemail_address = objdbconn.GetExecuteScalar(msSQL);

                    //msSQL = "update agr_mst_twarehouse set mobile_no='" + lsmobileno + "'," +
                    // " email_address='" + lsemail_address + "' where warehouse_gid='" + values.warehouse_gid + "' ";
                    //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Warehouse Details Updated Successfully";
                    return true;
                }
                return true;
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
                return false;
            }
        }



    }
}