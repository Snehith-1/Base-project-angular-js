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
    /// This Datacess provide access for various Single fields and Mutliple events (Add, Edit, View, Delete, Upload, Download and Approvals) in Warehouse Master
    /// </summary>
    /// <remarks>Written by Premchander.K </remarks>
    public class DaAgrMstWarehouseAdd
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader, objODBCDatareader1;
        HttpPostedFile httpPostedFile;
        DataTable dt_datatable;
        string msSQL, msGetGid, msGetGid1, msGetDocumentGid, msgetgidSA, msGetWFGid;
        int mnResult;
        string lsaddress_typegid, lswarehouse_gid, lsaddress_type, lspath, spoc_phoneno, lswarehouse2mobileno_gid, lswhatsapp_no, lsmobile_no, lsemail_address, lswarehouse2email_gid, lsaddressline1, lswarehouse2address_gid, lsaddressline2, lsprimary_status, lslandmark, lspostal_code, lscity, lstaluka, lsdistrict, lsstate, lscountry, lslatitude, lslongitude;
        string lsgst_state, lsgst_no, lswarehouse2branch_gid, lsgst_registered, lswarehouseagreement_gid;

        // Warehouse Address Details

        public bool DaPostWarehouseAddressDetail(string employee_gid, string user_gid, MdlagrmstAddressDetails values)
        {
            msSQL = "select primary_status from agr_mst_twarehouse2address where primary_status='Yes' and (warehouse_gid='" + employee_gid + "' or warehouse_gid='" + values.warehouse_gid + "')";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {
                values.status = false;
                values.message = "Already Primary Address Added";
                return false;
            }
            msSQL = "select warehouse2address_gid from agr_mst_twarehouse2address where addresstype_name='" + values.address_type + "' and (warehouse_gid='" + employee_gid + "' or warehouse_gid='" + values.warehouse_gid + "')";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already Address Type Added";
                return false;
            }
            objODBCDatareader.Close();
            msGetGid = objcmnfunctions.GetMasterGID("WH2A");
            msSQL = " insert into agr_mst_twarehouse2address(" +
                    " warehouse2address_gid," +
                    " warehouse_gid," +
                    " addresstype_gid," +
                    " addresstype_name," +
                    " addressline1," +
                    " addressline2," +
                    " primary_status," +
                    " landmark," +
                    " postal_code," +
                    " city," +
                    " taluka," +
                    " district," +
                    " state," +
                    " country," +
                    " latitude," +
                    " longitude," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.address_typegid + "'," +
                    "'" + values.address_type + "'," +
                    "'" + values.addressline1 + "'," +
                    "'" + values.addressline2 + "'," +
                    "'" + values.primary_status + "'," +
                    "'" + values.landmark + "'," +
                    "'" + values.postal_code + "'," +
                    "'" + values.city + "'," +
                    "'" + values.taluka + "'," +
                    "'" + values.district + "'," +
                    "'" + values.state + "'," +
                    "'" + values.country + "'," +
                    "'" + values.latitude + "'," +
                    "'" + values.longitude + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Address Details Added Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }

        }

        public void DaGetWarehouseAddressList(string employee_gid, MdlagrmstAddressDetails values)
        {
            msSQL = "  select warehouse2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country, landmark, latitude, longitude," +
                    " postal_code from agr_mst_twarehouse2address where warehouse_gid='" + employee_gid + "'";
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

        public void DaEditWarehouseAddressDetail(string warehouse2address_gid, MdlagrmstAddressDetails values)
        {
            try
            {
                msSQL = "select addresstype_gid, addresstype_name, addressline1, addressline2, landmark, taluka, primary_status, postal_code, city," +
                    " district, state, country, latitude, longitude, warehouse_gid, warehouse2address_gid " +
                    " from agr_mst_twarehouse2address where warehouse2address_gid='" + warehouse2address_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.address_typegid = objODBCDatareader["addresstype_gid"].ToString();
                    values.address_type = objODBCDatareader["addresstype_name"].ToString();
                    values.addressline1 = objODBCDatareader["addressline1"].ToString();
                    values.addressline2 = objODBCDatareader["addressline2"].ToString();
                    values.landmark = objODBCDatareader["landmark"].ToString();
                    values.taluka = objODBCDatareader["taluka"].ToString();
                    values.primary_status = objODBCDatareader["primary_status"].ToString();
                    values.postal_code = objODBCDatareader["postal_code"].ToString();
                    values.city = objODBCDatareader["city"].ToString();
                    values.district = objODBCDatareader["district"].ToString();
                    values.state = objODBCDatareader["state"].ToString();
                    values.country = objODBCDatareader["country"].ToString();
                    values.latitude = objODBCDatareader["latitude"].ToString();
                    values.longitude = objODBCDatareader["longitude"].ToString();
                    values.warehouse_gid = objODBCDatareader["warehouse_gid"].ToString();
                    values.warehouse2address_gid = objODBCDatareader["warehouse2address_gid"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;
                values.message = "success";
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaUpdateWarehouseAddressDetail(string employee_gid, MdlagrmstAddressDetails values)
        {
            msSQL = "select addresstype_gid, addresstype_name, addressline1, addressline2, landmark, taluka, primary_status, postal_code, city," +
                    " district, state, country, latitude, longitude, warehouse_gid, warehouse2address_gid " +
                    " from agr_mst_twarehouse2address where warehouse2address_gid='" + values.warehouse2address_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsaddress_typegid = objODBCDatareader["addresstype_gid"].ToString();
                lsaddress_type = objODBCDatareader["addresstype_name"].ToString();
                lsaddressline1 = objODBCDatareader["addressline1"].ToString();
                lsaddressline2 = objODBCDatareader["addressline2"].ToString();
                lslandmark = objODBCDatareader["landmark"].ToString();
                lstaluka = objODBCDatareader["taluka"].ToString();
                lsprimary_status = objODBCDatareader["primary_status"].ToString();
                lspostal_code = objODBCDatareader["postal_code"].ToString();
                lscity = objODBCDatareader["city"].ToString();
                lsdistrict = objODBCDatareader["district"].ToString();
                lsstate = objODBCDatareader["state"].ToString();
                lscountry = objODBCDatareader["country"].ToString();
                lslatitude = objODBCDatareader["latitude"].ToString();
                lslongitude = objODBCDatareader["longitude"].ToString();
                lswarehouse_gid = objODBCDatareader["warehouse_gid"].ToString();
                lswarehouse2address_gid = objODBCDatareader["warehouse2address_gid"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_twarehouse2address set " +
                         " addresstype_gid='" + values.address_typegid + "'," +
                         " addresstype_name='" + values.address_type + "'," +
                         " addressline1='" + values.addressline1 + "'," +
                         " addressline2='" + values.addressline2 + "'," +
                         " landmark='" + values.landmark + "'," +
                         " taluka='" + values.taluka + "'," +
                         " primary_status='" + values.primary_status + "'," +
                         " postal_code='" + values.postal_code + "'," +
                         " city='" + values.city + "'," +
                         " district='" + values.district + "'," +
                         " state='" + values.state + "'," +
                         " country='" + values.country + "'," +
                         " latitude='" + values.latitude + "'," +
                         " longitude='" + values.longitude + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where warehouse2address_gid='" + values.warehouse2address_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("WHAU");

                    msSQL = " insert into agr_mst_twarehouse2addressupdatelog(" +
                  " warehouse2addressupdatelog_gid," +
                  " warehouse2address_gid," +
                  " warehouse_gid," +
                  " addresstype_gid," +
                  " addresstype_name," +
                  " addressline1," +
                  " addressline2," +
                  " primary_status," +
                  " landmark," +
                  " postal_code," +
                  " city," +
                  " taluka," +
                  " district," +
                  " state," +
                  " country," +
                  " latitude," +
                  " longitude," +
                  " created_by," +
                  " created_date)" +
                  " values(" +
                  "'" + msGetGid + "'," +
                  "'" + values.warehouse2address_gid + "'," +
                  "'" + lswarehouse_gid + "'," +
                  "'" + lsaddress_typegid + "'," +
                  "'" + lsaddress_type + "'," +
                  "'" + lsaddressline1 + "'," +
                  "'" + lsaddressline2 + "'," +
                  "'" + lsprimary_status + "'," +
                  "'" + lslandmark + "'," +
                  "'" + lspostal_code + "'," +
                  "'" + lscity + "'," +
                  "'" + lstaluka + "'," +
                  "'" + lsdistrict + "'," +
                  "'" + lsstate + "'," +
                  "'" + lscountry + "'," +
                  "'" + lslatitude + "'," +
                  "'" + lslongitude + "'," +
                  "'" + employee_gid + "'," +
                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Address Details Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        public void DaDeleteWarehouseAddressDetail(string warehouse2address_gid, string employee_gid, MdlagrmstAddressDetails values)
        {
            msSQL = "delete from agr_mst_twarehouse2address where warehouse2address_gid='" + warehouse2address_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_twarehouse2addressupdatelog where warehouse2address_gid='" + warehouse2address_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Address Deatils Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }


        public bool DaPostWarehouseEmailAddress(string employee_gid, MdlagrmstEmailAddress values)
        {
            msSQL = "select primary_status from agr_mst_twarehouse2email where primary_status='Yes' and (warehouse_gid='" + employee_gid + "' or warehouse_gid='" + values.warehouse_gid + "')";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {

                values.status = false;
                values.message = "Already Primary Email Address Added";
                return false;
            }
            msSQL = "select warehouse2email_gid from agr_mst_twarehouse2email where email_address='" + values.email_address + "' and (warehouse_gid='" + employee_gid + "' or warehouse_gid='" + values.warehouse_gid + "')";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already This Email Address Added";
                return false;
            }
            objODBCDatareader.Close();
            msGetGid = objcmnfunctions.GetMasterGID("WHEM");
            msSQL = " insert into agr_mst_twarehouse2email(" +
                    " warehouse2email_gid," +
                    " warehouse_gid," +
                    " email_address," +
                    " primary_status," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.email_address + "'," +
                    "'" + values.primary_status + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Email Address Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }

        public void DaGetWarehouseEmailAddressList(string employee_gid, MdlagrmstEmailAddress values)
        {
            msSQL = " select email_address,warehouse2email_gid,primary_status from agr_mst_twarehouse2email where warehouse_gid='" + employee_gid + "'";
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

        public void DaEditWarehouseEmailAddress(string warehouse2email_gid, MdlagrmstEmailAddress values)
        {
            try
            {
                msSQL = " select email_address,warehouse2email_gid,primary_status from agr_mst_twarehouse2email where " +
                        " warehouse2email_gid='" + warehouse2email_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.email_address = objODBCDatareader["email_address"].ToString();
                    values.primary_status = objODBCDatareader["primary_status"].ToString();
                    values.warehouse2email_gid = objODBCDatareader["warehouse2email_gid"].ToString();
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

        public void DaUpdateWarehouseEmailAddress(string employee_gid, MdlagrmstEmailAddress values)
        {
            msSQL = " select email_address,warehouse2email_gid,primary_status from agr_mst_twarehouse2email where " +
                        " warehouse2email_gid='" + values.warehouse2email_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsemail_address = objODBCDatareader["email_address"].ToString();
                lsprimary_status = objODBCDatareader["primary_status"].ToString();
                lswarehouse2email_gid = objODBCDatareader["warehouse2email_gid"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_twarehouse2email set " +
                         " email_address='" + values.email_address + "'," +
                         " primary_status='" + values.primary_status + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where warehouse2email_gid='" + values.warehouse2email_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("WHEU");

                    msSQL = "Insert into agr_mst_twarehouse2emailupdatelog(" +
                   " warehouse2emailaddressupdatelog_gid, " +
                   " warehouse2email_gid, " +
                   " warehouse_gid, " +
                   " email_address," +
                   " primary_status," +
                   " created_by," +
                   " created_date)" +
                   " values (" +
                   "'" + msGetGid + "'," +
                   "'" + values.warehouse2email_gid + "'," +
                   "'" + values.warehouse_gid + "'," +
                   "'" + lsemail_address + "'," +
                   "'" + lsprimary_status + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "Email Address Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        public void DaDeleteWarehouseEmailAddress(string warehouse2email_gid, MdlagrmstEmailAddress values)
        {
            msSQL = "delete from agr_mst_twarehouse2email where warehouse2email_gid='" + warehouse2email_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_twarehouse2emailupdatelog where warehouse2email_gid='" + warehouse2email_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Email Address Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public bool DaPostWarehouseMobileNo(string employee_gid, MdlagrmstMobileNo values)
        {
            msSQL = "select primary_status from agr_mst_twarehouse2mobileno where primary_status='Yes' and (warehouse_gid='" + employee_gid + "' or warehouse_gid='" + values.warehouse_gid + "')";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {
                values.status = false;
                values.message = "Already Primary Mobile Number Added";
                return false;
            }

            msSQL = "select warehouse2mobileno_gid from agr_mst_twarehouse2mobileno where mobile_no='" + values.mobile_no + "' and (warehouse_gid='" + employee_gid + "' or warehouse_gid='" + values.warehouse_gid + "')";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already This Mobile Number Added";
                return false;
            }
            objODBCDatareader.Close();
            msGetGid = objcmnfunctions.GetMasterGID("WHMN");
            msSQL = " insert into agr_mst_twarehouse2mobileno(" +
                    " warehouse2mobileno_gid," +
                    " warehouse_gid," +
                    " mobile_no," +
                    " primary_status," +
                    " whatsapp_no," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.mobile_no + "'," +
                    "'" + values.primary_status + "'," +
                    "'" + values.whatsapp_no + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Mobile Number Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }

        public void DaGetWarehouseMobileNoList(string employee_gid, MdlagrmstMobileNo values)
        {
            msSQL = "select mobile_no,warehouse2mobileno_gid,primary_status,whatsapp_no from agr_mst_twarehouse2mobileno where " +
              " warehouse_gid='" + employee_gid + "'";
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

        public void DaEditWarehouseMobileNo(string warehouse2mobileno_gid, MdlagrmstMobileNo values)
        {
            try
            {
                msSQL = " select mobile_no,warehouse2mobileno_gid,primary_status,whatsapp_no from agr_mst_twarehouse2mobileno where " +
                        " warehouse2mobileno_gid='" + warehouse2mobileno_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.mobile_no = objODBCDatareader["mobile_no"].ToString();
                    values.primary_status = objODBCDatareader["primary_status"].ToString();
                    values.whatsapp_no = objODBCDatareader["whatsapp_no"].ToString();
                    values.warehouse2mobileno_gid = objODBCDatareader["warehouse2mobileno_gid"].ToString();
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

        public void DaUpdateWarehouseMobileNo(string employee_gid, MdlagrmstMobileNo values)
        {
            msSQL = " select mobile_no,warehouse2mobileno_gid,primary_status,whatsapp_no from agr_mst_twarehouse2mobileno where " +
                    " warehouse2mobileno_gid='" + values.warehouse2mobileno_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsmobile_no = objODBCDatareader["mobile_no"].ToString();
                lsprimary_status = objODBCDatareader["primary_status"].ToString();
                lswhatsapp_no = objODBCDatareader["whatsapp_no"].ToString();
                lswarehouse2mobileno_gid = objODBCDatareader["warehouse2mobileno_gid"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_twarehouse2mobileno set " +
                         " mobile_no='" + values.mobile_no + "'," +
                         " primary_status='" + values.primary_status + "'," +
                         " whatsapp_no='" + values.whatsapp_no + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where warehouse2mobileno_gid='" + values.warehouse2mobileno_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("WHMU");

                    msSQL = "Insert into agr_mst_twarehouse2mobilenoupdatelog(" +
                   " warehouse2mobilenoupdatelog_gid, " +
                   " warehouse2mobileno_gid, " +
                   " warehouse_gid, " +
                   " mobile_no," +
                   " primary_status," +
                   " whatsapp_no," +
                   " created_by," +
                   " created_date)" +
                   " values (" +
                   "'" + msGetGid + "'," +
                   "'" + values.warehouse2mobileno_gid + "'," +
                   "'" + values.warehouse_gid + "'," +
                   "'" + lsmobile_no + "'," +
                   "'" + lsprimary_status + "'," +
                   "'" + lswhatsapp_no + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "warehouse Mobile Number Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        public void DaDeleteWarehouseMobileNo(string warehouse2mobileno_gid, MdlagrmstMobileNo values)
        {
            msSQL = "delete from agr_mst_twarehouse2mobileno where warehouse2mobileno_gid='" + warehouse2mobileno_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_twarehouse2mobilenoupdatelog where warehouse2mobileno_gid='" + warehouse2mobileno_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Mobile Number Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;
            }
        }


        public bool Dawarehousedocumentupload(HttpRequest httpRequest, MdlAgrMstWarhouseAdd values, string employee_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string lsdocument_title = httpRequest.Form["document_title"].ToString();
            string lswarehouse_gid = httpRequest.Form["warehouse_gid"].ToString();
            string lswarehouseagreement_gid = httpRequest.Form["warehouseagreement_gid"].ToString();
            string path, lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/WarehouseDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/WarehouseDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/WarehouseDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("WHDU");
                        msSQL = " insert into agr_mst_twarehouse2docupload( " +
                                    " warehouse2docupload_gid," +
                                    " warehouse_gid," +
                                    " warehouseagreement_gid," +
                                    " document_title  ," +
                                    " document_name  ," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + lswarehouse_gid + "'," +
                                    "'" + lswarehouseagreement_gid + "'," +
                                    "'" + lsdocument_title + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        //if (mnResult == 1)
                        //{
                        //    msSQL = " update agr_mst_twarehouse set " +
                        //            " warehouseupload_flag='Y'," +
                        //            " updated_by='" + employee_gid + "'," +
                        //              " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        //              " where warehouse_gid='" + lswarehouse_gid + "' ";
                        //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        //    values.status = true;
                        //    values.message = "Document Uploaded Successfully..!";
                        //}
                        //else
                        //{
                        //    values.status = false;
                        //    values.message = "Error Occured..!";
                        //}

                        msSQL = " select warehouse2docupload_gid,warehouse_gid,document_name,document_path,document_title, warehouseagreement_gid from " +
                            " agr_mst_twarehouse2docupload where warehouseagreement_gid='" + lswarehouseagreement_gid + "'";
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
                }
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
            }
            return true;
        }
        public void Dagetwarehousedoc_delete(string warehouse2docupload_gid, string warehouse2agreement_gid, MdlAgrMstWarhouseAdd values)
        {
            msSQL = "delete from agr_mst_twarehouse2docupload where warehouse2docupload_gid='" + warehouse2docupload_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = " select warehouse2docupload_gid,warehouse_gid,document_name,document_path,document_title, warehouseagreement_gid from " +
                           " agr_mst_twarehouse2docupload where warehouseagreement_gid='" + warehouse2agreement_gid + "'";
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
                values.message = "Warhouse Document deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occrued while deleting document";
                values.status = false;
            }
        }

        public void Dagetwarehousedocument(string warehouse_gid, MdlAgrMstWarhouseAdd values)
        {
            msSQL = " select warehouse2docupload_gid,warehouse_gid,document_name,document_path,document_title, warehouseagreement_gid " +
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
                        warehouseagreement_gid = dt["warehouseagreement_gid"].ToString(),


                    });
                    values.agrmstwarhouse_upload = getcamdocument_list;
                }
            }
            dt_datatable.Dispose();
        }


        // warehouse GST Details

        public bool DaPostWarehouseGST(string employee_gid, MdlagrmstGST values)
        {
            msSQL = "select warehouse_gid from tmp_warehouse where employee_gid='" + employee_gid + "'";
            lswarehouse_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select warehouse_gid from agr_mst_twarehouse2branch where gst_no='" + values.gst_no + "' and warehouse_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already Added";
                return false;
            }

            msGetGid = objcmnfunctions.GetMasterGID("WH2B");
            msSQL = " insert into agr_mst_twarehouse2branch(" +
                    " warehouse2branch_gid," +
                    " warehouse_gid," +
                    " gst_state," +
                    " gst_no," +
                    " gst_registered," +
                    " headoffice_status," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.gst_state + "'," +
                    "'" + values.gst_no + "'," +
                    "'" + values.gst_registered + "'," +
                    "'No'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "GST Details Added Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }
        }

        public bool DaPostWarehouseGSTList(string employee_gid, MdlagrmstGST values)
        {

            warehouseGSTDetails[] GstArray = values.GSTArray;
            string GSTValue, GSTStateCode, GSTState;

            for (int i = 0; i < GstArray.Length; i++)
            {
                GSTValue = GstArray[i].gstinId;
                GSTStateCode = GSTValue.Substring(0, 2);

                msSQL = "select gst_state from agr_mst_tgstcode2state where " +
                       " gst_code='" + GSTStateCode + "'";
                GSTState = objdbconn.GetExecuteScalar(msSQL);

                msGetGid = objcmnfunctions.GetMasterGID("WH2B");
                msSQL = " insert into agr_mst_twarehouse2branch(" +
                    " warehouse2branch_gid," +
                    " warehouse_gid," +
                    " gst_state," +
                    " gst_no," +
                    " gst_registered," +
                    " headoffice_status," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + GSTState + "'," +
                    "'" + GSTValue + "'," +
                    "'" + "Yes" + "'," +
                    "'No'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "GST Details Added Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }
        }

        public void DaGetWarehouseGSTList(string employee_gid, MdlagrmstGST values)
        {
            msSQL = " select warehouse2branch_gid,gst_state,gst_no, gst_registered,headoffice_status from agr_mst_twarehouse2branch where warehouse_gid='" + employee_gid + "' ";
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

        public void DaEditWarehouseGST(string warehouse2branch_gid, MdlagrmstGST values)
        {
            try
            {
                msSQL = "select gst_state, gst_no, warehouse_gid, warehouse2branch_gid, gst_registered " +
                    " from agr_mst_twarehouse2branch where warehouse2branch_gid='" + warehouse2branch_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.gst_state = objODBCDatareader["gst_state"].ToString();
                    values.gst_no = objODBCDatareader["gst_no"].ToString();
                    values.warehouse2branch_gid = objODBCDatareader["warehouse2branch_gid"].ToString();
                    values.warehouse_gid = objODBCDatareader["warehouse_gid"].ToString();
                    values.gst_registered = objODBCDatareader["gst_registered"].ToString();
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

        public void DaUpdateWarehouseGST(string employee_gid, MdlagrmstGST values)
        {
            msSQL = "select gst_state, gst_no, gst_registered, warehouse_gid, warehouse2branch_gid" +
                " from agr_mst_twarehouse2branch where warehouse2branch_gid='" + values.warehouse2branch_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsgst_state = objODBCDatareader["gst_state"].ToString();
                lsgst_no = objODBCDatareader["gst_no"].ToString();
                lswarehouse2branch_gid = objODBCDatareader["warehouse2branch_gid"].ToString();
                lswarehouse_gid = objODBCDatareader["warehouse_gid"].ToString();
                lsgst_registered = objODBCDatareader["gst_registered"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_twarehouse2branch set " +
                         " gst_state='" + values.gst_state + "'," +
                         " gst_no='" + values.gst_no + "'," +
                         " gst_registered='" + values.gst_registered + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where warehouse2branch_gid='" + values.warehouse2branch_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("WHGU");

                    msSQL = "Insert into agr_mst_twarehouse2branchupdatelog(" +
                   " warehouse2gstupdatelog_gid, " +
                   " warehouse2branch_gid, " +
                   " warehouse_gid, " +
                   " gst_state," +
                   " gst_no," +
                   " gst_registered," +
                   " created_by," +
                   " created_date)" +
                   " values (" +
                   "'" + msGetGid + "'," +
                   "'" + values.warehouse2branch_gid + "'," +
                   "'" + values.warehouse_gid + "'," +
                   "'" + lsgst_state + "'," +
                   "'" + lsgst_no + "'," +
                   "'" + lsgst_registered + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "GST Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        public void DaDeleteWarehouseGST(string warehouse2branch_gid, MdlagrmstGST values)
        {
            msSQL = "delete from agr_mst_twarehouse2branch where warehouse2branch_gid='" + warehouse2branch_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_twarehouse2branchupdatelog where warehouse2branch_gid='" + warehouse2branch_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "GST Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }


        public void DaDeleteGSTWarehouse(string employee_gid, string warehouse_gid, MdlagrmstGST values)
        {
            msSQL = "select warehouse2branch_gid from agr_mst_twarehouse2branch where warehouse_gid='" + employee_gid + "' or warehouse_gid='" + warehouse_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            string warehouse2branch_gid;
            foreach (DataRow dr_datarow in dt_datatable.Rows)
            {
                warehouse2branch_gid = (dr_datarow["warehouse2branch_gid"].ToString());
                msSQL = "delete from agr_mst_twarehouse2branch where warehouse2branch_gid='" + warehouse2branch_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            dt_datatable.Dispose();

            if (mnResult != 0)
            {
                values.message = "GST Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Deleting The Gst Details";
                values.status = false;

            }
        }


        public bool DaPostWarehouseCommodity(string employee_gid, Warehousevarietyname_list values)
        {

            msGetGid = objcmnfunctions.GetMasterGID("WCVL");
            msSQL = " insert into agr_mst_twarehouse2commodity(" +
                    " warehouse2commodity_gid," +
                    " warehouse_gid," +
                    " product_gid," +
                    " product_name," +
                    " sector_name," +
                    " category_name," +
                    " variety_gid," +
                    " variety_name," +
                    " botanical_name," +
                    " alternative_name," +
                    " hsn_code," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                     "'" + values.product_gid + "'," +
                    "'" + values.product_name + "'," +
                    "'" + values.sector_name + "'," +
                    "'" + values.category_name + "'," +
                    "'" + values.variety_gid + "'," +
                    "'" + values.variety_name + "'," +
                    "'" + values.botanical_name + "'," +
                    "'" + values.alternative_name + "'," +
                    "'" + values.hsn_code + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Commodity Details Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured While Adding Commodity Details";
                return false;
            }

        }

        public void DaGetWarehouseCommodity(string employee_gid, MdlWarehouseSectorcategory values)
        {
            msSQL = "select warehouse2commodity_gid,product_gid,product_name,sector_name,category_name,variety_gid,variety_name, botanical_name, alternative_name, hsn_code from agr_mst_twarehouse2commodity where " +
              " warehouse_gid='" + employee_gid + "'";
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

        public void DaEditWarehouseCommodity(string warehouse_gid, Warehousevarietyname_list values)
        {
            try
            {
                msSQL = "select warehouse2commodity_gid, warehouse_gid, product_gid, product_name, sector_name, category_name, variety_gid, variety_name, botanical_name, alternative_name, hsn_code" +
                    " from agr_mst_twarehouse2commodity where warehouse_gid='" + warehouse_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.warehouse2commodity_gid = objODBCDatareader["warehouse2commodity_gid"].ToString();
                    values.warehouse_gid = objODBCDatareader["warehouse_gid"].ToString();
                    values.product_gid = objODBCDatareader["product_gid"].ToString();
                    values.product_name = objODBCDatareader["product_name"].ToString();
                    values.sector_name = objODBCDatareader["sector_name"].ToString();
                    values.category_name = objODBCDatareader["category_name"].ToString();
                    values.variety_gid = objODBCDatareader["variety_gid"].ToString();
                    values.variety_name = objODBCDatareader["variety_name"].ToString();
                    values.botanical_name = objODBCDatareader["botanical_name"].ToString();
                    values.alternative_name = objODBCDatareader["alternative_name"].ToString();
                    values.hsn_code = objODBCDatareader["hsn_code"].ToString();
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

        public void DaUpdateWarehouseCommodity(string employee_gid, Warehousevarietyname_list values)
        {
            try
            {
                msSQL = " update agr_mst_twarehouse2commodity set " +
                          " warehouse2commodity_gid," +
                    " warehouse_gid," +
                    " product_gid," +
                    " product_name," +
                    " sector_name," +
                    " category_name," +
                    " variety_gid," +
                    " variety_name," +
                    " botanical_name," +
                    " alternative_name," +
                    " hsn_code," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.warehouse_gid + "'," +
                     "'" + values.product_gid + "'," +
                    "'" + values.product_name + "'," +
                    "'" + values.sector_name + "'," +
                    "'" + values.category_name + "'," +
                    "'" + values.variety_gid + "'," +
                    "'" + values.variety_name + "'," +
                    "'" + values.botanical_name + "'," +
                    "'" + values.alternative_name + "'," +
                    "'" + values.hsn_code + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')" +
                " where warehouse2commodity_gid='" + values.warehouse2commodity_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID(" ");

                    msSQL = "Insert into agr_mst_twarehouse2commodityupdatelog(" +
                    " warehouse2commodityupdatelog_gid " +
                    " warehouse2commodity_gid," +
                    " warehouse_gid," +
                    " product_gid," +
                    " product_name," +
                    " sector_name," +
                    " category_name," +
                    " variety_gid," +
                    " variety_name," +
                    " botanical_name," +
                    " alternative_name," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.warehouse2commodity_gid + "'," +
                    "'" + values.warehouse_gid + "'," +
                     "'" + values.product_gid + "'," +
                    "'" + values.product_name + "'," +
                    "'" + values.sector_name + "'," +
                    "'" + values.category_name + "'," +
                    "'" + values.variety_gid + "'," +
                    "'" + values.variety_name + "'," +
                    "'" + values.botanical_name + "'," +
                    "'" + values.alternative_name + "'," +
                    "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "Commodity Updated Successfully";
                }

            }

            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        public void DaDeleteWarehouseCommodity(string warehouse2commodity_gid, MdlWarehouseSectorcategory values)
        {
            msSQL = "delete from agr_mst_twarehouse2commodity where warehouse2commodity_gid='" + warehouse2commodity_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Commodity Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Deleting The Commodity Details";
                values.status = false;

            }
        }

        //public void DaGetspocno(MdlAgrMstWarhouseAdd values)
        //{

        //    try
        //    {
        //        string spoc_id = string.Empty;
        //        string spoc_name = string.Empty;

        //        if (values.spocid_list != null)
        //        {
        //            for (var i = 0; i < values.spocid_list.Count; i++)
        //            {
        //                spoc_id += "'" + values.spocid_list[i].employee_gid + "'" + ",";
        //            }

        //            spoc_id = spoc_id.TrimEnd(',');
        //        }
        //        msSQL = "select employee_mobileno from hrm_mst_temployee where employee_gid in (" + spoc_id + ")";

        //        //objODBCDatareader = objdbconn.GetDataReader(msSQL);
        //         foreach (DataRow dr_datarow in dt_datatable.Rows)
        //            {
        //            values.spoc_phonenolist = values.spoc_phonenolist + dr_datarow["employee_mobileno"].ToString() + ",";
        //            }

        //        values.spoc_phonenolist = values.spoc_phonenolist.TrimEnd(',');

        //        //values.spoc_phoneno = objODBCDatareader["employee_mobileno"].ToString();


        //        values.status = true;
        //    values.message = "success";
        //    objODBCDatareader.Close();

        //    }

        //catch (Exception ex)
        //{
        //    values.status = false;
        //    values.message = ex.Message.ToString();
        //}

        //}



        public void DaGetspocno(spocid_list values)
        {

            msSQL = "select employee_mobileno from hrm_mst_temployee where employee_gid ='" + values.lsemployee_gid + "'";

            values.spoc_phoneno = objdbconn.GetExecuteScalar(msSQL);


        }

        public void DaPostWarehouseSpocDetails(string employee_gid, Mdlagrmstspoc values)
        {

            msGetGid = objcmnfunctions.GetMasterGID("WSPC");
            msSQL = " insert into agr_mst_twarehouse2spoc(" +
                " warehouse2spoc_gid," +
                " warehouse_gid," +
                " spoc_id," +
                " spoc_name," +
                " spocmobile_no," +
                " created_by," +
                " created_date)" +
                " values(" +
                "'" + msGetGid + "'," +
                "'" + employee_gid + "'," +
                "'" + values.spoc_id + "'," +
                "'" + values.spoc_name + "'," +
                "'" + values.spocmobile_no + "'," +
                "'" + employee_gid + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Spoc Details Added Successfully";

            }
            else
            {
                values.status = false;
                values.message = "Error Occured";

            }
        }

        public void DaEditWarehouseSpocDetails(string warehouse2spoc_gid, Mdlagrmstspoc values)
        {
            try
            {
                msSQL = "select warehouse2spoc_gid, warehouse_gid, spoc_id, spocmobile_no, spoc_name" +
                    " from agr_mst_twarehouse2spoc where warehouse2spoc_gid='" + warehouse2spoc_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.spoc_name = objODBCDatareader["spoc_name"].ToString();
                    values.spocmobile_no = objODBCDatareader["spocmobile_no"].ToString();

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

        public void DaGetWarehouseSpocDetails(string employee_gid, Mdlagrmstspoc values)
        {
            msSQL = "select warehouse2spoc_gid, warehouse_gid, spoc_id, spocmobile_no, spoc_name" +
                    " from agr_mst_twarehouse2spoc where warehouse_gid='" + employee_gid + "'";
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

        public void DaDeleteWarehousespoc(string warehouse2spoc_gid, Mdlagrmstspoc values)
        {
            msSQL = "delete from agr_mst_twarehouse2spoc where warehouse2spoc_gid='" + warehouse2spoc_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Spoc Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Deleting The Spoc Details";
                values.status = false;

            }
        }


        public void DaPostWarehouseSubmit(string employee_gid, MdlAgrMstWarehouseCreation values)
        {


            msSQL = "select warehouse_gid from agr_mst_twarehouse2mobileno where warehouse_gid='" + employee_gid + "' and primary_status='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Primary Mobile Number ";
                return;
            }
            objODBCDatareader.Close();

            msSQL = "select warehouse_gid from agr_mst_twarehouse2email where warehouse_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add primary Email Address";
                return;
            }

            objODBCDatareader.Close();
            msSQL = "select warehouse_gid from agr_mst_twarehouse2address where warehouse_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add primary Address";
                return;
            }
            objODBCDatareader.Close();
            if (values.Gstflag == "Yes")
            {
                msSQL = "select warehouse_gid from agr_mst_twarehouse2branch where warehouse_gid='" + employee_gid + "' and headoffice_status ='Yes' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == false)
                {
                    values.status = false;
                    values.message = "Atleast Select One GST Number as Head Office";
                    return;
                }
               
            }
            objODBCDatareader.Close();
            msgetgidSA = objcmnfunctions.GetMasterGID("WARG");
            msgetgidSA = msgetgidSA.Replace("WARG", "");
            values.warehouse_ref_no = $"{msgetgidSA:00000}";
            values.warehouse_ref_no = "3" + values.warehouse_ref_no; 

            msGetGid = objcmnfunctions.GetMasterGID("WRHS");

            msSQL = " insert into agr_mst_twarehouse(" +
                   " warehouse_gid," +
                   " warehouse_ref_no," +
                   " warehouse_name," +
                   //" owned_by," +
                   " warehouse_pan," +
                   " first_name," +
                   " middle_name," +
                   " last_name," +
                   " subsidiarywarshouse_name," +
                   " warehouse_area," +
                   " warehousearea_uomgid, " +
                   " warehousearea_uom," +
                   " totalcapacity_area," +
                   " totalcapacityarea_uomgid, " +
                   " area_uom, " +
                   " totalcapacity_volume," +
                   " volume_uomgid, " +
                   " volume_uom," +
                   " typeofwarehouse_gid," +
                   " typeofwarehouse_name," +
                   " creditor_gid,"+
                   " Applicant_name,"+
                   " charges," +
                   " capacity," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.warehouse_ref_no + "'," +

                   "'" + values.warehouse_name + "'," +
                   //"'" + values.owned_by + "'," +
                   "'" + values.warehouse_pan + "',";
            if (values.first_name == "" || values.first_name == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.first_name.Replace("'", "") + "',";
            }
            if (values.middle_name == "" || values.middle_name == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.middle_name.Replace("'", "") + "',";
            }
            if (values.last_name == "" || values.last_name == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.last_name.Replace("'", "") + "',";
            }
            msSQL += "'" + values.subsidiarywarshouse_name + "'," +
                     "'" + values.warehouse_area + "'," +
                     "'" + values.warehousearea_uomgid + "'," +
                     "'" + values.warehousearea_uom + "'," +
                     "'" + values.totalcapacity_area + "'," +
                     "'" + values.totalcapacityarea_uomgid + "'," +
                     "'" + values.area_uom + "'," +
                     "'" + values.totalcapacity_volume + "'," +
                     "'" + values.volume_uomgid + "'," +
                     "'" + values.volume_uom + "'," +
                     "'" + values.typeofwarehouse_gid + "'," +
                     "'" + values.typeofwarehouse_name + "'," +
                     "'" + values.creditor_gid + "'," +
                     "'" + values.Applicant_name + "'," +
                     "'" + values.charges + "'," +
                     "'" + values.capacity + "'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {

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
                           "'" + msGetGid + "'," +
                           "'" + values.facility_list[i].warehousefacility_gid + "'," +
                           "'" + values.facility_list[i].warehousefacility_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }


                //Updates

                msSQL = "update agr_mst_twarehouse2mobileno set warehouse_gid ='" + msGetGid + "' where warehouse_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_twarehouse2email set warehouse_gid ='" + msGetGid + "' where warehouse_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_twarehouse2address set warehouse_gid ='" + msGetGid + "' where warehouse_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_twarehouse2branch set warehouse_gid ='" + msGetGid + "' where warehouse_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_twarehouse2commodity set warehouse_gid ='" + msGetGid + "' where warehouse_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_twarehouse2spoc set warehouse_gid ='" + msGetGid + "' where warehouse_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_twarehouse2docupload set warehouse_gid ='" + msGetGid + "' where warehouse_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_twarehouse2agreement set warehouse_gid ='" + msGetGid + "' where warehouse_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_twarehouse set warehousesubmit_flag ='Y' where warehouse_gid='" + msGetGid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //msSQL = " select mstpmgapproval_gid from agr_mst_pmgapproval ";
                //string lsmstpmgapproval_gid = objdbconn.GetExecuteScalar(msSQL);

                //msSQL = " select mstproductapproval_gid from agr_mst_productapproval ";
                //string lsmstproductapproval_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "  select mstpmgapproval_gid, pmgapproval_gid, pmgapproval_name from agr_mst_pmgapproval";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                foreach (DataRow dr_datarow in dt_datatable.Rows)
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
                            "'" + msGetGid + "'," +
                            "'" + (dr_datarow["pmgapproval_gid"].ToString()) + "'," +
                            "'" + (dr_datarow["pmgapproval_name"].ToString()) + "'," +
                            "'" + (dr_datarow["mstpmgapproval_gid"].ToString()) + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msSQL = "  select mstproductapproval_gid, productapproval_gid, productapproval_name from agr_mst_productapproval";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    msGetGid1 = objcmnfunctions.GetMasterGID("W2AG");

                    msSQL = " insert into agr_trn_twarehouse2approval(" +
                            " warehouse2approval_gid," +
                            " warehouse_gid," +
                            " approval_gid," +
                            " approval_name," +
                            " mstproductapproval_gid , " +
                            " created_by , " +
                            " created_date )" +
                            " values(" +
                            "'" + msGetGid1 + "'," +
                            "'" + msGetGid + "'," +
                            "'" + (dr_datarow["productapproval_gid"].ToString()) + "'," +
                            "'" + (dr_datarow["productapproval_name"].ToString()) + "'," +
                            "'" + (dr_datarow["mstproductapproval_gid"].ToString()) + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                values.status = true;
                values.message = "Warehouse Details Submitted Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }



        public void DaGetNewWarehouseSummary(string employee_gid, MdlAgrMstWarhouseAdd values)
        {
            msSQL = " select a.warehouse_gid,a.warehouse_ref_no, a.warehouse_name, d.product_gid, d.product_name," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                    "  from agr_mst_twarehouse a" +
                    " left join agr_mst_twarehouse2commodity d on a.warehouse_gid = d.warehouse_gid" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid where a.created_by='" + employee_gid + "'" +
                    " and warehousesubmit_flag='" + getWarehouseStatusClass.New + "' group by warehouse_gid order by warehouse_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbuyerbank_list = new List<MdlAgrMstWarehouseCreation>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbuyerbank_list.Add(new MdlAgrMstWarehouseCreation
                    {
                        warehouse_gid = (dr_datarow["warehouse_gid"].ToString()),
                        warehouse_ref_no = (dr_datarow["warehouse_ref_no"].ToString()),
                        warehouse_name = (dr_datarow["warehouse_name"].ToString()),
                        product_name = (dr_datarow["product_name"].ToString()),
                        product_gid = (dr_datarow["product_gid"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                    });
                }
                values.MdlAgrMstWarehouseCreation = getbuyerbank_list;
            }
            dt_datatable.Dispose();
        }


        public void DaEditWarehouseDetails(string warehouse_gid, MdlAgrMstWarehouseCreation values)
        {
            try
            {
                msSQL = " select warehouse_gid, warehouse_ref_no, warehouse_name, owned_by, productapproval_flag, pmgapproval_flag, warehousesubmit_flag, warehouse_pan, first_name, middle_name, last_name, " +
                        " subsidiarywarshouse_name, (select group_concat(warehousefacility_name) from agr_mst_twarehouse2facility where warehouse_gid = '" + warehouse_gid + "') " +
                        " as warehousefacility_name,warehousearea_uomgid,totalcapacityarea_uomgid,volume_uomgid, typeofwarehouse_gid, typeofwarehouse_name, " +
                        " warehouse_area, warehousearea_uom, totalcapacity_area, area_uom, totalcapacity_volume, volume_uom,  charges, capacity, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,creditor_gid, Applicant_name," +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date " +
                        " from agr_mst_twarehouse a " +
                        " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                        " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                        " where a.warehouse_gid='" + warehouse_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.warehouse_gid = objODBCDatareader["warehouse_gid"].ToString();
                    values.warehouse_ref_no = objODBCDatareader["warehouse_ref_no"].ToString();
                    values.warehouse_name = objODBCDatareader["warehouse_name"].ToString();
                    values.owned_by = objODBCDatareader["owned_by"].ToString();
                    values.warehouse_pan = objODBCDatareader["warehouse_pan"].ToString();
                    values.first_name = objODBCDatareader["first_name"].ToString();
                    values.middle_name = objODBCDatareader["middle_name"].ToString();
                    values.last_name = objODBCDatareader["last_name"].ToString();
                    values.subsidiarywarshouse_name = objODBCDatareader["subsidiarywarshouse_name"].ToString();
                    values.warehouse_area = objODBCDatareader["warehouse_area"].ToString();
                    values.warehousearea_uom = objODBCDatareader["warehousearea_uom"].ToString();
                    values.totalcapacity_area = objODBCDatareader["totalcapacity_area"].ToString();
                    values.area_uom = objODBCDatareader["area_uom"].ToString();
                    values.totalcapacity_volume = objODBCDatareader["totalcapacity_volume"].ToString();
                    values.volume_uom = objODBCDatareader["volume_uom"].ToString();
                    values.charges = objODBCDatareader["charges"].ToString();
                    values.capacity = objODBCDatareader["capacity"].ToString();
                    values.created_by = objODBCDatareader["created_by"].ToString();
                    values.created_date = objODBCDatareader["created_date"].ToString();
                    values.warehousefacility_name = objODBCDatareader["warehousefacility_name"].ToString();
                    values.productapproval_flag = objODBCDatareader["productapproval_flag"].ToString();
                    values.pmgapproval_flag = objODBCDatareader["pmgapproval_flag"].ToString();
                    values.warehousesubmit_flag = objODBCDatareader["warehousesubmit_flag"].ToString();
                    values.volume_uomgid = objODBCDatareader["volume_uomgid"].ToString();
                    values.totalcapacityarea_uomgid = objODBCDatareader["totalcapacityarea_uomgid"].ToString();
                    values.warehousearea_uomgid = objODBCDatareader["warehousearea_uomgid"].ToString();
                    values.typeofwarehouse_name = objODBCDatareader["typeofwarehouse_name"].ToString();
                    values.typeofwarehouse_gid = objODBCDatareader["typeofwarehouse_gid"].ToString();
                    values.creditor_gid = objODBCDatareader["creditor_gid"].ToString();
                    values.Applicant_name = objODBCDatareader["Applicant_name"].ToString();

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

        public void DaPostWarehouseAgreementDetails(string employee_gid, Mdlagrmstagreementdtllist values)
        {

            msGetGid = objcmnfunctions.GetMasterGID("WAAG");
            msSQL = " insert into agr_mst_twarehouse2agreement(" +
                " warehouse2agreement_gid," +
                " warehouse_gid," +
                " warehouse2address_gid," +
                " warehouseagreement_address," +
                " execution_date," +
                " expiry_date," +
                " created_by," +
                " created_date)" +
                " values(" +
                "'" + msGetGid + "'," +
                "'" + employee_gid + "'," +
                "'" + values.warehouse2address_gid + "'," +
                "'" + values.warehouse_address + "'," +
                //"'" + values.execution_date + "'," +
                //"'" + values.expiry_date + "'," +
                "'" + Convert.ToDateTime(values.execution_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                "'" + Convert.ToDateTime(values.expiry_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                "'" + employee_gid + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Agreement Details Added Successfully";

            }
            else
            {
                values.status = false;
                values.message = "Error Occured";

            }
        }


        public void DaGetWarehouseAgreementDetails(string employee_gid, MdlAgrMstWarhouseAdd values)
        {
            msSQL = " select warehouse2agreement_gid, warehouse_gid, warehouse2address_gid, warehouseagreement_address, " +
                   " date_format(execution_date,'%d-%m-%Y') as execution_date, date_format(expiry_date,'%d-%m-%Y') as expiry_date " +
                   " from agr_mst_twarehouse2agreement where warehouse_gid='" + employee_gid + "'";
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
                        execution_date = (dr_datarow["execution_date"].ToString()),
                        expiry_date = (dr_datarow["expiry_date"].ToString()),

                    });
                }
                values.Mdlagrmstagreementdtllist = getbuyerbank_list;
            }
            dt_datatable.Dispose();
        }


        public void DaGetWarehouseProductcommodity(string warehouse_gid, string employee_gid, MdlWarehouseSectorcategory values)
        {
            msSQL = " select warehouse2commodity_gid,warehouse_gid,product_gid, product_name from agr_mst_twarehouse2commodity " +
                  " where warehouse_gid='" + warehouse_gid + "'or warehouse_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getvarietyname_list = new List<Warehousevarietyname_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getvarietyname_list.Add(new Warehousevarietyname_list
                    {
                        warehouse2commodity_gid = dt["warehouse2commodity_gid"].ToString(),
                        warehouse_gid = dt["warehouse_gid"].ToString(),
                        product_gid = dt["product_gid"].ToString(),
                        product_name = dt["product_name"].ToString(),
                    });
                    values.Warehousevarietyname_list = getvarietyname_list;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetApprovalPendingWarehouseSummary(string employee_gid, MdlWarehouseSummary values)
        {
            msSQL = " select a.warehouse_gid, a.warehouse_ref_no, a.warehouse_name, d.product_gid, d.product_name," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                    " CASE WHEN(productapproval_flag = 'N' and pmgapproval_flag = 'N')  THEN 'Product Approval Pending' " +
                    " WHEN(productapproval_flag = 'Y' and pmgapproval_flag = 'N') THEN 'PMG Approval Pending' " +
                    " WHEN(productapproval_flag = 'R' and pmgapproval_flag = 'N') THEN 'Product Approval - Rejected' " +
                    " WHEN(productapproval_flag = 'Y' and pmgapproval_flag = 'R') THEN 'PMG Approval - Rejected' " +
                    " ELSE '' END as approval_status , " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                    "  from agr_mst_twarehouse a" +
                    " left join agr_mst_twarehouse2commodity d on a.warehouse_gid = d.warehouse_gid" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid where a.created_by='" + employee_gid + "'" +
                    " and warehousesubmit_flag='" + getWarehouseStatusClass.Pending + "' group by warehouse_gid order by warehouse_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbuyerbank_list = new List<MdlAgrMstWarehouseCreation>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbuyerbank_list.Add(new MdlAgrMstWarehouseCreation
                    {
                        warehouse_gid = (dr_datarow["warehouse_gid"].ToString()),
                        warehouse_ref_no = (dr_datarow["warehouse_ref_no"].ToString()),
                        warehouse_name = (dr_datarow["warehouse_name"].ToString()),
                        product_name = (dr_datarow["product_name"].ToString()),
                        product_gid = (dr_datarow["product_gid"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        approval_status = (dr_datarow["approval_status"].ToString()),
                    });
                }
                values.MdlAgrMstWarehouseCreation = getbuyerbank_list;
            }
            dt_datatable.Dispose();
        }

        public void DaGetApprovedWarehouseSummary(string employee_gid, MdlWarehouseSummary values)
        {
            msSQL = " select a.warehouse_gid, a.warehouse_ref_no, a.warehouse_name, d.product_gid, d.product_name," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                     " CASE WHEN(productapproval_flag = 'N' and pmgapproval_flag = 'N')  THEN 'Product Approval Pending' " +
                    " WHEN(productapproval_flag = 'Y' and pmgapproval_flag = 'N') THEN 'PMG Approval Pending' " +
                    " WHEN(productapproval_flag = 'R' and pmgapproval_flag = 'N') THEN 'Product Approval - Rejected' " +
                    " WHEN(productapproval_flag = 'Y' and pmgapproval_flag = 'R') THEN 'PMG Approval - Rejected' " +
                    " ELSE 'Approved' END as approval_status , " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                    "  from agr_mst_twarehouse a" +
                    " left join agr_mst_twarehouse2commodity d on a.warehouse_gid = d.warehouse_gid" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " where warehousesubmit_flag='" + getWarehouseStatusClass.Approved + "' group by warehouse_gid order by warehouse_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbuyerbank_list = new List<MdlAgrMstWarehouseCreation>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbuyerbank_list.Add(new MdlAgrMstWarehouseCreation
                    {
                        warehouse_gid = (dr_datarow["warehouse_gid"].ToString()),
                        warehouse_ref_no = (dr_datarow["warehouse_ref_no"].ToString()),
                        warehouse_name = (dr_datarow["warehouse_name"].ToString()),
                        product_name = (dr_datarow["product_name"].ToString()),
                        product_gid = (dr_datarow["product_gid"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        approval_status = (dr_datarow["approval_status"].ToString()),
                    });
                }
                values.MdlAgrMstWarehouseCreation = getbuyerbank_list;
            }
            dt_datatable.Dispose();
        }

        public void DaGetRejectedWarehouseSummary(string employee_gid, MdlWarehouseSummary values)
        {
            msSQL = " select a.warehouse_gid, a.warehouse_ref_no, a.warehouse_name, d.product_gid, d.product_name," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                    " CASE WHEN(productapproval_flag = 'N' and pmgapproval_flag = 'N')  THEN 'Product Approval Pending' " +
                    " WHEN(productapproval_flag = 'Y' and pmgapproval_flag = 'N') THEN 'PMG Approval Pending' " +
                    " WHEN(productapproval_flag = 'R' and pmgapproval_flag = 'N') THEN 'Product Approval - Rejected' " +
                    " WHEN(productapproval_flag = 'Y' and pmgapproval_flag = 'R') THEN 'PMG Approval - Rejected' " +
                    " ELSE '' END as approval_status , " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                    "  from agr_mst_twarehouse a" +
                    " left join agr_mst_twarehouse2commodity d on a.warehouse_gid = d.warehouse_gid" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid where a.created_by='" + employee_gid + "'" +
                    " and warehousesubmit_flag='" + getWarehouseStatusClass.Rejected + "' group by warehouse_gid order by warehouse_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbuyerbank_list = new List<MdlAgrMstWarehouseCreation>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbuyerbank_list.Add(new MdlAgrMstWarehouseCreation
                    {
                        warehouse_gid = (dr_datarow["warehouse_gid"].ToString()),
                        warehouse_ref_no = (dr_datarow["warehouse_ref_no"].ToString()),
                        warehouse_name = (dr_datarow["warehouse_name"].ToString()),
                        product_name = (dr_datarow["product_name"].ToString()),
                        product_gid = (dr_datarow["product_gid"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        approval_status = (dr_datarow["approval_status"].ToString()),
                    });
                }
                values.MdlAgrMstWarehouseCreation = getbuyerbank_list;
            }
            dt_datatable.Dispose();
        }

        public void DaGetWarehouseCount(string employee_gid, WarehouseCountdtl values)
        {
            msSQL = " select (select count(*) from agr_mst_twarehouse a  where warehousesubmit_flag = '" + getWarehouseStatusClass.New + "' and created_by='" + employee_gid + "') as My_Warehouse , " +
                    " (select count(*) from agr_mst_twarehouse a  where warehousesubmit_flag = '" + getWarehouseStatusClass.Pending + "' and created_by ='" + employee_gid + "') as ApprovalPending_Warehouse, " +
                    " (select count(*) from agr_mst_twarehouse a  where warehousesubmit_flag = '" + getWarehouseStatusClass.Approved + "' ) as Approved_Warehouse," +
                    " (select count(*) from agr_mst_twarehouse a  where warehousesubmit_flag = '" + getWarehouseStatusClass.Rejected + "' and created_by ='" + employee_gid + "') as Rejected_Warehouse";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.My_Warehouse = objODBCDatareader["My_Warehouse"].ToString();
                values.ApprovalPending_Warehouse = objODBCDatareader["ApprovalPending_Warehouse"].ToString();
                values.Approved_Warehouse = objODBCDatareader["Approved_Warehouse"].ToString();
                values.Rejected_Warehouse = objODBCDatareader["Rejected_Warehouse"].ToString();
            }
            objODBCDatareader.Close();
        }

        public void DaDeleteAgreementDetail(string warehouse2agreement_gid, result values)
        {
            msSQL = "delete from agr_mst_twarehouse2agreement where warehouse2agreement_gid='" + warehouse2agreement_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_twarehouse2docupload where warehouseagreement_gid='" + warehouse2agreement_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Agreement Details are Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public void DaWarehouseFacilityEdit(string warehouse_gid, MdlAgrMstWarehouseCreation values)
        {

            msSQL = " select warehouse2facility_gid,warehousefacility_gid,warehousefacility_name from agr_mst_twarehouse2facility " +
                 " where warehouse_gid='" + warehouse_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getwarehousefacility = new List<facility_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getwarehousefacility.Add(new facility_list
                    {
                        warehouse2facility_gid = dt["warehouse2facility_gid"].ToString(),
                        warehousefacility_gid = dt["warehousefacility_gid"].ToString(),
                        warehousefacility_name = dt["warehousefacility_name"].ToString(),
                    });
                    values.facility_list = getwarehousefacility;
                }
            }
            dt_datatable.Dispose();

        }

        public void DaGetSpocEmployee(MdlEmployeeList values,string warehouse_gid)
        {
            msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name," +
                       " b.employee_gid" +
                       " FROM adm_mst_tuser a " +
                       " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                       " WHERE user_status<>'N' AND b.employee_gid NOT IN (select spoc_id from agr_mst_twarehouse2spoc where warehouse_gid='" + warehouse_gid + "')" +
                       " ORDER BY a.user_firstname ASC";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_employee = new List<employeedtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                values.employeedtl = dt_datatable.AsEnumerable().Select(row =>
                  new employeedtl
                  {
                      employee_gid = row["employee_gid"].ToString(),
                      employee_name = row["employee_name"].ToString()
                  }
                ).ToList();
            }
            dt_datatable.Dispose();
        }

        public void DaGetTmpSpocEmployee(MdlEmployeeList values, string employee_gid, string warehouse_gid)
        {
            msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name," +
                       " b.employee_gid" +
                       " FROM adm_mst_tuser a " +
                       " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                       " WHERE user_status<>'N' AND b.employee_gid NOT IN " +
                       " (select spoc_id from agr_mst_twarehouse2spoc where (warehouse_gid='" + warehouse_gid + "' or warehouse_gid='" + employee_gid + "'))" +
                       " ORDER BY a.user_firstname ASC";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_employee = new List<employeedtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                values.employeedtl = dt_datatable.AsEnumerable().Select(row =>
                  new employeedtl
                  {
                      employee_gid = row["employee_gid"].ToString(),
                      employee_name = row["employee_name"].ToString()
                  }
                ).ToList();
            }
            dt_datatable.Dispose();
        }

        public void DaGettypeofwarehouse(MdlAgrMstWarhouseAdd values)
        {
            try
            {
                msSQL = " SELECT a.typeofwarehouse_gid,a.typeofwarehouse_name" +
                        " FROM agr_mst_ttypeofwarehouse a" +
                        " where a.status = 'Y' order by a.typeofwarehouse_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getapplicationmst_list = new List<warehousetype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getapplicationmst_list.Add(new warehousetype_list
                        {
                            typeofwarehouse_gid = (dr_datarow["typeofwarehouse_gid"].ToString()),
                            typeofwarehouse_name = (dr_datarow["typeofwarehouse_name"].ToString()),
                            
                        });
                    }
                    values.warehousetype_list = getapplicationmst_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaUpdateGSTHeadOffice(string employee_gid, MdlGSTwarehouseHeadOffice values)
        {
            msSQL = " update agr_mst_twarehouse2branch set headoffice_status = 'Yes' " +
                    " where warehouse2branch_gid = '" + values.warehouse2branch_gid + "' " +
                    " and warehouse_gid = '" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = " update agr_mst_twarehouse2branch set headoffice_status='No' " +
                        " where warehouse2branch_gid<>'" + values.warehouse2branch_gid + "' " +
                        " and warehouse_gid='" + employee_gid + "'";
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

    }
}