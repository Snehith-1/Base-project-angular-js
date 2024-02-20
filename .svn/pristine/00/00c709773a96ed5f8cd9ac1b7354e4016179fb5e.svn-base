using ems.hbapiconn.Functions;
using ems.mastersamagro.Models;
using ems.storage.Functions;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
 

namespace ems.mastersamagro.DataAccess
{
    /// <summary>
    /// This DataAccess provide access for various Single and Mutliple events (Add, Edit, View, Delete, Upload, Download and Approvals) in Other Creditor Master 
    /// </summary>
    /// <remarks>Written by Premchander.K </remarks>

    public class DaAgrMstCreditorMaster
    {

        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader, objODBCDatareader1;
        HttpPostedFile httpPostedFile;
        DataTable dt_datatable, dt_datatable2;
        string msSQL, msGetGid, msGetGid1, msGetDocumentGid, msgetgidSA, msGetPDGid, msGetPGGid;
        int mnResult;

        string lsaddress_typegid, lscreditor_gid, lsaddress_type, lspath, spoc_phoneno, lscreditor2mobileno_gid, lswhatsapp_no, lsmobile_no, lsemail_address, lscreditor2email_gid, lsaddressline1, lscreditor2address_gid, lsaddressline2, lsprimary_status, lslandmark, lspostal_code, lscity, lstaluka, lsdistrict, lsstate, lscountry, lslatitude, lslongitude;
        string lsgst_state, lsgst_no, lscreditor2branch_gid, lsgst_registered, lscreditoragreement_gid, lsaadhar_no;

        string lscreditorref_no, lsApplicant_name, lsApplicant_category, lspan_no, lsApplicanttype_gid, lsApplicant_type, lsloanproduct_gid, lsdesignation_type, lsloanproduct_name, lsloansubproduct_gid, lsloansubproduct_name, lsdesignation_gid, lscontact_no, lscontactperson_name, lsemail_id, lscreated_by, lscreated_date;

        string lscreditor2cheque_gid, lsdesignation_name,
       lsaccountholder_name, lsaccount_number, lsbank_name, lscheque_no, lsifsc_code, lsbranch_address, lsbranch_name,
       lsmergedbankingentity_gid, lsmergedbankingentity_name, lsspecial_condition, lsgeneral_remarks, lscts_enabled, lscheque_type, lsdate_chequetype,
       lsdate_chequepresentation, lsstatus_chequepresentation, lsdate_chequeclearance, lsstatus_chequeclearance;
        FnSamAgroHBAPIConn objFnSamAgroHBAPIConn = new FnSamAgroHBAPIConn();
        FnSamAgroHBAPIConnEdit objFnSamAgroHBAPIConnEdit = new FnSamAgroHBAPIConnEdit();
        FnSamAgroHAPIOtherCreditor objFnSamAgroHAPIOtherCreditor = new FnSamAgroHAPIOtherCreditor();

        public bool DaPostCreditorMasterGST(string employee_gid, MdlcreditorGST values)
        {
            msSQL = "select creditor_gid from tmp_creditor where employee_gid='" + employee_gid + "'";
            lscreditor_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select creditor_gid from agr_mst_tcreditor2branch where gst_no='" + values.gst_no + "' and creditor_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already Added";
                return false;
            }

            msGetGid = objcmnfunctions.GetMasterGID("CR2B");
            msSQL = " insert into agr_mst_tcreditor2branch(" +
                    " creditor2branch_gid," +
                    " creditor_gid," +
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

        public bool DaPostCreditorMasterGSTList(string employee_gid, MdlcreditorGST values)
        {

            creditorGSTDetails[] GstArray = values.GSTArray;
            string GSTValue, GSTStateCode, GSTState;

            for (int i = 0; i < GstArray.Length; i++)
            {
                GSTValue = GstArray[i].gstinId;
                GSTStateCode = GSTValue.Substring(0, 2);

                msSQL = "select gst_state from agr_mst_tgstcode2state where " +
                       " gst_code='" + GSTStateCode + "'";
                GSTState = objdbconn.GetExecuteScalar(msSQL);

                msGetGid = objcmnfunctions.GetMasterGID("CR2B");
                msSQL = " insert into agr_mst_tcreditor2branch(" +
                    " creditor2branch_gid," +
                    " creditor_gid," +
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

        public void DaGetCreditorMasterGSTList(string employee_gid, MdlcreditorGST values)
        {
            msSQL = " select creditor2branch_gid,gst_state,gst_no, gst_registered,headoffice_status,creditor_gid from agr_mst_tcreditor2branch where creditor_gid='" + employee_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstgst_list = new List<creditorgst_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstgst_list.Add(new creditorgst_list
                    {
                        creditor2branch_gid = (dr_datarow["creditor2branch_gid"].ToString()),
                        gst_state = (dr_datarow["gst_state"].ToString()),
                        gst_no = (dr_datarow["gst_no"].ToString()),
                        gst_registered = (dr_datarow["gst_registered"].ToString()),
                        headoffice_status = (dr_datarow["headoffice_status"].ToString()),
                        creditor_gid = (dr_datarow["creditor_gid"].ToString())
                    });
                }
                values.creditorgst_list = getmstgst_list;
            }
            dt_datatable.Dispose();
        }

        public void DaEditCreditorMasterGST(string creditor2branch_gid, MdlcreditorGST values)
        {
            try
            {
                msSQL = "select gst_state, gst_no, creditor_gid, creditor2branch_gid, gst_registered " +
                    " from agr_mst_tcreditor2branch where creditor2branch_gid='" + creditor2branch_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.gst_state = objODBCDatareader["gst_state"].ToString();
                    values.gst_no = objODBCDatareader["gst_no"].ToString();
                    values.creditor2branch_gid = objODBCDatareader["creditor2branch_gid"].ToString();
                    values.creditor_gid = objODBCDatareader["creditor_gid"].ToString();
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

        public void DaUpdateCreditorMasterGST(string employee_gid, MdlcreditorGST values)
        {
            msSQL = "select gst_state, gst_no, gst_registered, creditor_gid, creditor2branch_gid" +
                " from agr_mst_tcreditor2branch where creditor2branch_gid='" + values.creditor2branch_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsgst_state = objODBCDatareader["gst_state"].ToString();
                lsgst_no = objODBCDatareader["gst_no"].ToString();
                lscreditor2branch_gid = objODBCDatareader["creditor2branch_gid"].ToString();
                lscreditor_gid = objODBCDatareader["creditor_gid"].ToString();
                lsgst_registered = objODBCDatareader["gst_registered"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_tcreditor2branch set " +
                         " gst_state='" + values.gst_state + "'," +
                         " gst_no='" + values.gst_no + "'," +
                         " gst_registered='" + values.gst_registered + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where creditor2branch_gid='" + values.creditor2branch_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("C2BU");

                    msSQL = "Insert into agr_mst_tcreditor2branchupdatelog(" +
                   " creditor2gstupdatelog_gid, " +
                   " creditor2branch_gid, " +
                   " creditor_gid, " +
                   " gst_state," +
                   " gst_no," +
                   " gst_registered," +
                   " created_by," +
                   " created_date)" +
                   " values (" +
                   "'" + msGetGid + "'," +
                   "'" + values.creditor2branch_gid + "'," +
                   "'" + values.creditor_gid + "'," +
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

        public void DaDeleteCreditorMasterGST(string creditor2branch_gid, MdlcreditorGST values)
        {
            msSQL = "delete from agr_mst_tcreditor2branch where creditor2branch_gid='" + creditor2branch_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_tcreditor2branchupdatelog where creditor2branch_gid='" + creditor2branch_gid + "'";
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


        public void DaDeleteGSTCreditor(string employee_gid, string creditor_gid, MdlcreditorGST values)
        {
            msSQL = "select creditor2branch_gid from agr_mst_tcreditor2branch where creditor_gid='" + employee_gid + "' or creditor_gid='" + creditor_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            string creditor2branch_gid;
            foreach (DataRow dr_datarow in dt_datatable.Rows)
            {
                creditor2branch_gid = (dr_datarow["creditor2branch_gid"].ToString());
                msSQL = "delete from agr_mst_tcreditor2branch where creditor2branch_gid='" + creditor2branch_gid + "'";
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

        public bool DaPostcreditorAddressDetail(string employee_gid, string user_gid, MdlcreditorAddressDetails values)
        {
            msSQL = "select primary_status from agr_mst_tcreditor2address where primary_status='Yes' and (creditor_gid='" + employee_gid + "' or creditor_gid='" + values.creditor_gid + "')";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {
                values.status = false;
                values.message = "Already Primary Address Added";
                return false;
            }
            msSQL = "select creditor2address_gid from agr_mst_tcreditor2address where addresstype_name='" + values.address_type + "' and (creditor_gid='" + employee_gid + "' or creditor_gid='" + values.creditor_gid + "')";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already Address Type Added";
                return false;
            }
            objODBCDatareader.Close();
            msGetGid = objcmnfunctions.GetMasterGID("CR2A");
            msSQL = " insert into agr_mst_tcreditor2address(" +
                    " creditor2address_gid," +
                    " creditor_gid," +
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
                    "'" + values.addressline1.Replace("'", "") + "'," +
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

        public void DaGetcreditorAddressList(string employee_gid, MdlcreditorAddressDetails values)
        {
            msSQL = "  select creditor2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, city, district, state, country, landmark, latitude, longitude," +
                    " postal_code from agr_mst_tcreditor2address where creditor_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstaddress_list = new List<creditoraddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstaddress_list.Add(new creditoraddress_list
                    {
                        creditor2address_gid = (dr_datarow["creditor2address_gid"].ToString()),
                        address_type = (dr_datarow["addresstype_name"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        addressline1 = (dr_datarow["addressline1"].ToString()),
                        addressline2 = (dr_datarow["addressline2"].ToString()),
                        taluka = (dr_datarow["taluka"].ToString()),
                        district = (dr_datarow["district"].ToString()),
                        state = (dr_datarow["state"].ToString()),
                        city = (dr_datarow["city"].ToString()),
                        country = (dr_datarow["country"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString()),
                        landmark = (dr_datarow["landmark"].ToString()),
                        latitude = (dr_datarow["latitude"].ToString()),
                        longitude = (dr_datarow["longitude"].ToString()),
                    });
                }
                values.creditoraddress_list = getmstaddress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaEditcreditorAddressDetail(string creditor2address_gid, MdlcreditorAddressDetails values)
        {
            try
            {
                msSQL = "select addresstype_gid, addresstype_name, addressline1, addressline2, landmark, taluka, primary_status, postal_code, city," +
                    " district, state, country, latitude, longitude, creditor_gid, creditor2address_gid " +
                    " from agr_mst_tcreditor2address where creditor2address_gid='" + creditor2address_gid + "'";
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
                    values.creditor_gid = objODBCDatareader["creditor_gid"].ToString();
                    values.creditor2address_gid = objODBCDatareader["creditor2address_gid"].ToString();
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

        public void DaUpdatecreditorAddressDetail(string employee_gid, MdlcreditorAddressDetails values)
        {
            msSQL = "select addresstype_gid, addresstype_name, addressline1, addressline2, landmark, taluka, primary_status, postal_code, city," +
                    " district, state, country, latitude, longitude, creditor_gid, creditor2address_gid " +
                    " from agr_mst_tcreditor2address where creditor2address_gid='" + values.creditor2address_gid + "'";
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
                lscreditor_gid = objODBCDatareader["creditor_gid"].ToString();
                lscreditor2address_gid = objODBCDatareader["creditor2address_gid"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_tcreditor2address set " +
                         " addresstype_gid='" + values.address_typegid + "'," +
                         " addresstype_name='" + values.address_type + "'," +
                         " addressline1='" + values.addressline1.Replace("'", "") + "'," +
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
                         " where creditor2address_gid='" + values.creditor2address_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("C2AU");

                    msSQL = " insert into agr_mst_tcreditor2addressupdatelog(" +
                  " creditor2addressupdatelog_gid," +
                  " creditor2address_gid," +
                  " creditor_gid," +
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
                  "'" + values.creditor2address_gid + "'," +
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

                if (ConfigurationManager.AppSettings["sysSamagroHyperbrdigeAPIEnable"].ToString() == "Yes")
                {
                    HttpContext ctx = HttpContext.Current;

                    Thread t = new Thread(new ThreadStart(() =>
                    {
                        HttpContext.Current = ctx;

                        List<string> existingStringList = new List<string> { lsaddressline1, lsaddressline2, lscity, lsstate, lspostal_code, lslatitude, lslongitude };
                        List<string> updatedStringList = new List<string> { values.addressline1, values.addressline2, values.city, values.state, values.postal_code, values.latitude, values.longitude };

                        if (!(existingStringList.SequenceEqual(updatedStringList)))
                        {
                            objFnSamAgroHAPIOtherCreditor.UpdateOtherCreditorAddressHBAPI(values.creditor2address_gid);
                        }

                    }));

                    t.Start();
                }

                //return true;
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        public void DaDeletecreditorAddressDetail(string creditor2address_gid, string employee_gid, MdlcreditorAddressDetails values)
        {
            msSQL = "delete from agr_mst_tcreditor2address where creditor2address_gid='" + creditor2address_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_tcreditor2addressupdatelog where creditor2address_gid='" + creditor2address_gid + "'";
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


        public bool DaPostChequeDetail(string employee_gid, Mdlcreditorcheque values)
        {

            msSQL = "select primary_status from agr_mst_tcreditor2cheque where primary_status='Yes' and (creditor_gid ='" + employee_gid + "' or creditor_gid ='" + values.creditor_gid + "')";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {
                values.status = false;
                values.message = "Already Added the Primary Cheque Details";
                return false;
            }


            msGetGid = objcmnfunctions.GetMasterGID("CR2C");
            //msGetUdcGid = objcmnfunctions.GetMasterGID("UCMG");

            msSQL = " insert into agr_mst_tcreditor2cheque(" +
                   " creditor2cheque_gid ," +
                   " creditor_gid ," +
                   " accountholder_name," +
                   " account_number," +
                   " bank_name," +
                   " cheque_no," +
                   " ifsc_code," +
                   " micr," +
                   " branch_address," +
                   " branch_name," +
                   " city," +
                   " district," +
                   " state," +
                   " mergedbankingentity_gid," +
                   " mergedbankingentity_name," +
                   " special_condition," +
                   " general_remarks," +
                   " cts_enabled," +
                   " cheque_type," +
                   " date_chequetype," +
                   " date_chequepresentation," +
                   " status_chequepresentation," +
                   " date_chequeclearance," +
                   " status_chequeclearance," +
                   " primary_status," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + employee_gid + "',";
            if ((values.accountholder_name == null) || (values.accountholder_name == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.accountholder_name.Replace("'", "") + "',";

            }
            if ((values.account_number == null) || (values.account_number == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.account_number.Replace("'", "") + "',";
            }

            if ((values.bank_name == null) || (values.bank_name == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.bank_name.Replace("'", "") + "',";
            }


            msSQL +=
              "'" + values.cheque_no + "'," +
              "'" + values.ifsc_code + "'," +
               "'" + values.micr + "',";
            if ((values.branch_address == null) || (values.branch_address == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.branch_address.Replace("'", "") + "',";

            }
            if ((values.branch_name == null) || (values.branch_name == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.branch_name.Replace("'", "") + "',";
            }

            msSQL +=

                   "'" + values.city + "'," +
                   "'" + values.district + "'," +
                   "'" + values.state + "'," +
                   "'" + values.mergedbankingentity_gid + "'," +
                   "'" + values.mergedbankingentity_name + "',";
            if ((values.special_condition == null) || (values.special_condition == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.special_condition.Replace("'", "") + "',";
            }

            if ((values.general_remarks == null) || (values.general_remarks == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.general_remarks.Replace("'", "") + "',";
            }

            msSQL +=
                              "'" + values.cts_enabled + "'," +
                   "'" + values.cheque_type + "',";
            if ((values.date_chequetype == null) || (values.date_chequetype == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.date_chequetype).ToString("yyyy-MM-dd") + "',";
            }
            if ((values.date_chequepresentation == null) || (values.date_chequepresentation == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.date_chequepresentation).ToString("yyyy-MM-dd") + "',";
            }
            msSQL += "'" + values.status_chequepresentation + "',";
            if ((values.date_chequeclearance == null) || (values.date_chequeclearance == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.date_chequeclearance).ToString("yyyy-MM-dd") + "',";
            }

            if ((values.status_chequeclearance == null) || (values.status_chequeclearance == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.status_chequeclearance.Replace("'", "") + "',";
            }
            msSQL +=
                "'" + values.primary_status + "'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                //Updates

                msSQL = "update agr_mst_tcreditorcheque2document set creditor2cheque_gid ='" + msGetGid + "' where creditor2cheque_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //msSQL = " insert into ocs_mst_tcreditor(" +
                //        " creditor_gid," +
                //        " customer_gid," +
                //        " customer_name," +
                //        " application_gid," +
                //        " udc_status," +
                //        " created_by," +
                //        " created_date)" +
                //        " values(" +
                //        "'" + msGetUdcGid + "'," +
                //        "'" + values.customer_gid + "'," +
                //        "'" + values.customer_name + "'," +
                //        "'" + values.application_gid + "'," +
                //        "'Completed'," +
                //        "'" + employee_gid + "'," +
                //        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Cheque details Added successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while Adding Cheque";
                return false;
            }
        }


        public void DaGetChequeSummary(string employee_gid, Mdlcreditorcheque values)
        {

            msSQL = " select a.creditor2cheque_gid, a.primary_status, a.cheque_no, a.account_number, a.mergedbankingentity_name ,a.cheque_type ,a.cts_enabled, b.document_path, b.document_name " +
                    " from agr_mst_tcreditor2cheque a" +
                    " left join agr_mst_tcreditorcheque2document b on a.creditor2cheque_gid = b.creditor2cheque_gid " +
                    " where a.creditor_gid='" + employee_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getchequeList = new List<creditorcheque_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getchequeList.Add(new creditorcheque_list
                    {
                        creditor2cheque_gid = dt["creditor2cheque_gid"].ToString(),
                        cheque_no = dt["cheque_no"].ToString(),
                        account_number = dt["account_number"].ToString(),
                        mergedbankingentity_name = dt["mergedbankingentity_name"].ToString(),
                        cheque_type = dt["cheque_type"].ToString(),
                        cts_enabled = dt["cts_enabled"].ToString(),
                        primary_status = dt["primary_status"].ToString(),
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),

                    });

                }
            }
            values.creditorcheque_list = getchequeList;
            dt_datatable.Dispose();
        }

        public void DaGetChequeSummaryTmplist(string employee_gid, string creditor_gid, Mdlcreditorcheque values)
        {

            msSQL = " select a.creditor2cheque_gid, a.cheque_no, a.primary_status , a.account_number, a.mergedbankingentity_name ,a.cheque_type ,a.cts_enabled, b.document_path, b.document_name " +
                    " from agr_mst_tcreditor2cheque a" +
                    " left join agr_mst_tcreditorcheque2document b on a.creditor2cheque_gid = b.creditor2cheque_gid " +
                    " where a.creditor_gid='" + employee_gid + "' or a.creditor_gid = '" + creditor_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getchequeList = new List<creditorcheque_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getchequeList.Add(new creditorcheque_list
                    {
                        creditor2cheque_gid = dt["creditor2cheque_gid"].ToString(),
                        cheque_no = dt["cheque_no"].ToString(),
                        account_number = dt["account_number"].ToString(),
                        mergedbankingentity_name = dt["mergedbankingentity_name"].ToString(),
                        primary_status = dt["primary_status"].ToString(),
                        cheque_type = dt["cheque_type"].ToString(),
                        cts_enabled = dt["cts_enabled"].ToString(),
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),

                    });

                }
            }
            values.creditorcheque_list = getchequeList;
            dt_datatable.Dispose();
        }

        public bool DaChequeDocumentUpload(HttpRequest httpRequest, creditorchequeuploaddocument objfilename, string employee_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            //string lsdocument_title = httpRequest.Form["document_title"].ToString();
            String path = lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/ChequeDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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
                            objfilename.message = "File format is not supported";
                            return false;
                        }
                        //lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "Master/ChequeDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        //FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                        //ms.WriteTo(file);
                        //file.Close();
                        //ms.Close();
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/ChequeDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/ChequeDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        //lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "Master/ChequeDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("CQ2D");
                        msGetDocumentGid = objcmnfunctions.GetMasterGID("CQDA");

                        msSQL = " delete from agr_mst_tcreditorcheque2document where creditor2cheque_gid = '" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " insert into agr_mst_tcreditorcheque2document( " +
                                    " creditorcheque2document_gid," +
                                    " creditor2cheque_gid," +
                                    " document_gid ," +
                                    " document_name ," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + msGetDocumentGid + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            objfilename.status = true;
                            objfilename.message = "Cheque Document Uploaded Successfully..!";
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error Occured in uploading cheque document..!";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objfilename.message = ex.ToString();
            }
            return true;
        }

        public void DaGetChequeDocumentList(string employee_gid, MdlcreditorchequeDocument values)
        {
            msSQL = " select creditorcheque2document_gid ,document_name,document_path from agr_mst_tcreditorcheque2document " +
                                 " where creditor2cheque_gid ='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<creditorchequedocument_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new creditorchequedocument_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        creditorcheque2document_gid = dt["creditorcheque2document_gid"].ToString(),
                    });
                    values.creditorchequedocument_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaChequeDocumentDelete(string creditorcheque2document_gid, MdlcreditorchequeDocument values)
        {
            msSQL = "delete from agr_mst_tcreditorcheque2document where creditorcheque2document_gid='" + creditorcheque2document_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Document Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public void DaUpdateChequeDetail(string employee_gid, Mdlcreditorcheque values)
        {
            msSQL = " select mergedbankingentity_gid,mergedbankingentity_name,special_condition," +
                    " general_remarks, cts_enabled, cheque_type, date_chequetype, date_chequepresentation," +
                    " status_chequepresentation, date_chequeclearance,status_chequeclearance" +
                    " from agr_mst_tcreditor2cheque where creditor2cheque_gid='" + values.creditor2cheque_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsmergedbankingentity_gid = objODBCDatareader["mergedbankingentity_gid"].ToString();
                lsmergedbankingentity_name = objODBCDatareader["mergedbankingentity_name"].ToString();

                lsspecial_condition = objODBCDatareader["special_condition"].ToString();
                lsgeneral_remarks = objODBCDatareader["general_remarks"].ToString();
                lscts_enabled = objODBCDatareader["cts_enabled"].ToString();
                lscheque_type = objODBCDatareader["cheque_type"].ToString();
                lsdate_chequetype = objODBCDatareader["date_chequetype"].ToString();
                lsdate_chequepresentation = objODBCDatareader["date_chequepresentation"].ToString();
                lsstatus_chequepresentation = objODBCDatareader["status_chequepresentation"].ToString();
                lsdate_chequeclearance = objODBCDatareader["date_chequeclearance"].ToString();
                lsstatus_chequeclearance = objODBCDatareader["status_chequeclearance"].ToString();

            }
            try
            {
                msSQL = " update agr_mst_tcreditor2cheque set " +
                         " mergedbankingentity_gid='" + values.mergedbankingentity_gid + "'," +
                         " mergedbankingentity_name='" + values.mergedbankingentity_name + "'," +

                " special_condition='" + values.special_condition + "'," +
                " general_remarks='" + values.general_remarks + "'," +
                " cts_enabled='" + values.cts_enabled + "'," +
                " cheque_type='" + values.cheque_type + "'," +
                " status_chequepresentation='" + values.status_chequepresentation + "'," +
                " primary_status='" + values.primary_status + "'," +
                " status_chequeclearance='" + values.status_chequeclearance + "',";

                try
                {
                    if (values.date_chequetype == "" || values.date_chequetype == null)
                    {

                    }
                    else
                    {
                        msSQL += " date_chequetype='" + Convert.ToDateTime(values.date_chequetype).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    }

                    if (values.date_chequepresentation == "" || values.date_chequepresentation == null)
                    {

                    }
                    else
                    {
                        msSQL += " date_chequepresentation='" + Convert.ToDateTime(values.date_chequepresentation).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    }
                    if (values.date_chequeclearance == "" || values.date_chequeclearance == null)
                    {

                    }
                    else
                    {
                        msSQL += " date_chequeclearance='" + Convert.ToDateTime(values.date_chequeclearance).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    }

                    msSQL += " updated_by='" + employee_gid + "'," +
                             " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                }
                catch (Exception ex)
                {
                    msSQL += " updated_by='" + employee_gid + "'," +
                             " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                }


                msSQL += " where creditor2cheque_gid='" + values.creditor2cheque_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("U2CL");
                    msSQL = " insert into agr_mst_tcreditor2chequeupdatelog(" +
                   " creditor2chequeupdatelog_gid ," +
                   " creditor2cheque_gid ," +
                   " creditor_gid ," +
                   " mergedbankingentity_gid," +
                   " mergedbankingentity_name," +
                   " special_condition," +
                   " general_remarks," +
                   " cts_enabled," +
                   " cheque_type," +
                   " date_chequetype," +
                   " date_chequepresentation," +
                   " status_chequepresentation," +
                   " date_chequeclearance," +
                   " status_chequeclearance," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.creditor2cheque_gid + "'," +
                   "'" + values.creditor_gid + "'," +
                   "'" + lsmergedbankingentity_gid + "'," +
                   "'" + lsmergedbankingentity_name + "'," +
                   "'" + lsspecial_condition + "'," +
                   "'" + lsgeneral_remarks + "'," +
                   "'" + lscts_enabled + "'," +
                   "'" + lscheque_type + "',";
                    if ((lsdate_chequetype == null) || (lsdate_chequetype == ""))
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += "'" + Convert.ToDateTime(lsdate_chequetype).ToString("yyyy-MM-dd") + "',";
                    }
                    if ((lsdate_chequepresentation == null) || (lsdate_chequepresentation == ""))
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += "'" + Convert.ToDateTime(lsdate_chequepresentation).ToString("yyyy-MM-dd") + "',";
                    }
                    msSQL +=
                   "'" + lsstatus_chequepresentation + "',";
                    if ((lsdate_chequeclearance == null) || (lsdate_chequeclearance == ""))
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += "'" + Convert.ToDateTime(lsdate_chequeclearance).ToString("yyyy-MM-dd") + "',";
                    }
                    msSQL +=
                   "'" + lsstatus_chequeclearance + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";


                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Cheque Details Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        public void DaChequeDetailsEdit(string creditor2cheque_gid, Mdlcreditorcheque values)
        {
            try
            {
                msSQL = " select accountholder_name,account_number,bank_name,cheque_no,primary_status, " +
                    " ifsc_code, micr, branch_address,branch_name,city,district,state,mergedbankingentity_gid,mergedbankingentity_name,special_condition," +
                    " general_remarks, cts_enabled, cheque_type,date_chequetype,date_chequepresentation,status_chequepresentation,date_chequeclearance,status_chequeclearance" +
                    " from agr_mst_tcreditor2cheque where creditor2cheque_gid='" + creditor2cheque_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {

                    values.accountholder_name = objODBCDatareader["accountholder_name"].ToString();
                    values.account_number = objODBCDatareader["account_number"].ToString();
                    values.bank_name = objODBCDatareader["bank_name"].ToString();
                    values.cheque_no = objODBCDatareader["cheque_no"].ToString();
                    values.ifsc_code = objODBCDatareader["ifsc_code"].ToString();
                    values.primary_status = objODBCDatareader["primary_status"].ToString();
                    values.ifsc_code = objODBCDatareader["ifsc_code"].ToString();
                    values.micr = objODBCDatareader["micr"].ToString();
                    values.branch_address = objODBCDatareader["branch_address"].ToString();
                    values.branch_name = objODBCDatareader["branch_name"].ToString();
                    values.city = objODBCDatareader["city"].ToString();
                    values.district = objODBCDatareader["district"].ToString();
                    values.state = objODBCDatareader["state"].ToString();
                    values.mergedbankingentity_gid = objODBCDatareader["mergedbankingentity_gid"].ToString();
                    values.mergedbankingentity_name = objODBCDatareader["mergedbankingentity_name"].ToString();

                    values.special_condition = objODBCDatareader["special_condition"].ToString();
                    values.general_remarks = objODBCDatareader["general_remarks"].ToString();
                    values.cts_enabled = objODBCDatareader["cts_enabled"].ToString();
                    values.cheque_type = objODBCDatareader["cheque_type"].ToString();
                    if (objODBCDatareader["date_chequetype"].ToString() != "" && objODBCDatareader["date_chequetype"].ToString() != null)
                    {
                        values.datechequetype = Convert.ToDateTime(objODBCDatareader["date_chequetype"].ToString());
                    }
                    //values.date_chequetype = Convert.ToDateTime(objODBCDatareader["date_chequetype"]).ToString("MM-dd-yyyy");
                    if (objODBCDatareader["date_chequepresentation"].ToString() != "" && objODBCDatareader["date_chequepresentation"].ToString() != null)
                    {
                        values.datechequepresentation = Convert.ToDateTime(objODBCDatareader["date_chequepresentation"].ToString());
                    }
                    //values.date_chequepresentation = Convert.ToDateTime(objODBCDatareader["date_chequepresentation"]).ToString("MM-dd-yyyy");
                    values.status_chequepresentation = objODBCDatareader["status_chequepresentation"].ToString();
                    if (objODBCDatareader["date_chequeclearance"].ToString() != "" && objODBCDatareader["date_chequeclearance"].ToString() != null)
                    {
                        values.datechequeclearance = Convert.ToDateTime(objODBCDatareader["date_chequeclearance"].ToString());
                    }
                    //values.date_chequeclearance = Convert.ToDateTime(objODBCDatareader["date_chequeclearance"]).ToString("MM-dd-yyyy");
                    values.status_chequeclearance = objODBCDatareader["status_chequeclearance"].ToString();


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

        public void DaChequeList(string employee_gid, string creditor_gid, Mdlcreditorcheque values)
        {

            msSQL = " select creditor2cheque_gid, cheque_no, stakeholder_type,cheque_type" +
                    " from agr_mst_tcreditor2cheque" +
                    " where creditor_gid='" + employee_gid + "' or creditor_gid = '" + creditor_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getchequeList = new List<creditorcheque_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getchequeList.Add(new creditorcheque_list
                    {
                        creditor2cheque_gid = dt["creditor2cheque_gid"].ToString(),
                        cheque_no = dt["cheque_no"].ToString(),
                        cheque_type = dt["cheque_type"].ToString(),
                    });

                }
            }
            values.creditorcheque_list = getchequeList;
            dt_datatable.Dispose();
        }

        public void DaChequeDocumentList(string creditor2cheque_gid, MdlcreditorchequeDocument values)
        {
            msSQL = " select creditorcheque2document_gid,document_name,document_path from agr_mst_tcreditorcheque2document " +
                                 " where creditor2cheque_gid='" + creditor2cheque_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<creditorchequedocument_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new creditorchequedocument_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        creditorcheque2document_gid = dt["creditorcheque2document_gid"].ToString(),
                    });
                    values.creditorchequedocument_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaDeleteChequeDetail(string creditor2cheque_gid, Mdlcreditorcheque values)
        {
            msSQL = "delete from agr_mst_tcreditor2cheque where creditor2cheque_gid='" + creditor2cheque_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Cheque Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }


        public void DaPostcreditorAgreementDetails(string employee_gid, Mdlcreditoragreementdtllist values)
        {

            msGetGid = objcmnfunctions.GetMasterGID("CR2A");
            msSQL = " insert into agr_mst_tcreditor2agreement(" +
                " creditor2agreement_gid," +
                " creditor_gid," +
                " samcontactperson_gid," +
                " samcontactperson_name," +
                " agreementinvolvement_type," +
                " creditor2agreement_no," +

                " execution_date," +
                " expiry_date," +
                " created_by," +
                " created_date)" +
                " values(" +
                "'" + msGetGid + "'," +
                "'" + employee_gid + "'," +
                "'" + values.samcontact_perssonid + "'," +
                "'" + values.samcontact_perssonname + "'," +
                "'" + values.agreementinvolvement_type + "',";
            if (values.creditor2agreement_no == null || values.creditor2agreement_no == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += " '" + values.creditor2agreement_no.Replace("'", "") + "',";

            }
            //"'" + values.execution_date + "'," +
            //"'" + values.expiry_date + "'," +
            if (values.execution_date == null || values.execution_date == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.execution_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if (values.expiry_date == null || values.expiry_date == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.expiry_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            //"'" + Convert.ToDateTime(values.execution_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
            //    "'" + Convert.ToDateTime(values.expiry_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
            msSQL += "'" + employee_gid + "'," +
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


        public void DaGetcreditorAgreementDetails(string employee_gid, MdlcreditorMaster values)
        {
            msSQL = " select creditor2agreement_gid, creditor_gid, samcontactperson_gid, samcontactperson_name, agreementinvolvement_type, creditor2agreement_no, " +
                   " date_format(execution_date,'%d-%m-%Y') as execution_date, date_format(expiry_date,'%d-%m-%Y') as expiry_date " +
                   " from agr_mst_tcreditor2agreement where creditor_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbuyerbank_list = new List<Mdlcreditoragreementdtllist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbuyerbank_list.Add(new Mdlcreditoragreementdtllist
                    {
                        creditor2agreement_gid = (dr_datarow["creditor2agreement_gid"].ToString()),
                        creditor_gid = (dr_datarow["creditor_gid"].ToString()),
                        samcontactperson_gid = (dr_datarow["samcontactperson_gid"].ToString()),
                        samcontactperson_name = (dr_datarow["samcontactperson_name"].ToString()),
                        agreementinvolvement_type = (dr_datarow["agreementinvolvement_type"].ToString()),
                        creditor2agreement_no = (dr_datarow["creditor2agreement_no"].ToString()),
                        execution_date = (dr_datarow["execution_date"].ToString()),
                        expiry_date = (dr_datarow["expiry_date"].ToString()),

                    });
                }
                values.Mdlcreditoragreementdtllist = getbuyerbank_list;
            }
            dt_datatable.Dispose();
        }


        public bool Dacreditordocumentupload(HttpRequest httpRequest, MdlcreditorMaster values, string employee_gid)
        {
            creditoragreement_upload objdocumentmodel = new creditoragreement_upload();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string lsdocument_title = httpRequest.Form["document_title"].ToString();
            string lswarehouse_gid = httpRequest.Form["creditor_gid"].ToString();
            string lswarehouseagreement_gid = httpRequest.Form["creditor2agreement_gid"].ToString();
            string path, lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/CreditorAgreementDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/CreditorAgreementDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/CreditorAgreementDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("CA2D");
                        msSQL = " insert into agr_mst_tcreditoragreement2docupload( " +
                                    " creditoragreement2docupload_gid," +
                                    " creditor_gid," +
                                    " creditor2agreement_gid," +
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

                        msSQL = " select creditoragreement2docupload_gid,creditor_gid,document_name,document_path,document_title, creditor2agreement_gid from " +
                            " agr_mst_tcreditoragreement2docupload where creditor2agreement_gid='" + lswarehouseagreement_gid + "'";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        var getcamdocument_list = new List<creditoragreement_upload>();
                        if (dt_datatable.Rows.Count != 0)
                        {
                            foreach (DataRow dt in dt_datatable.Rows)
                            {
                                getcamdocument_list.Add(new creditoragreement_upload
                                {
                                    document_name = dt["document_name"].ToString(),
                                    document_title = dt["document_title"].ToString(),
                                    document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                                    creditor_gid = dt["creditor_gid"].ToString(),
                                    creditoragreement2docupload_gid = dt["creditoragreement2docupload_gid"].ToString(),
                                    creditor2agreement_gid = dt["creditor2agreement_gid"].ToString(),


                                });
                                values.creditoragreement_upload = getcamdocument_list;
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
        public void Dagetcreditordoc_delete(string creditoragreement2docupload_gid, string creditor2agreement_gid, MdlcreditorMaster values)
        {
            msSQL = "delete from agr_mst_tcreditoragreement2docupload where creditoragreement2docupload_gid='" + creditoragreement2docupload_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = " select creditoragreement2docupload_gid,creditor_gid,document_name,document_path,document_title, creditor2agreement_gid from " +
                           " agr_mst_tcreditoragreement2docupload where creditor2agreement_gid='" + creditor2agreement_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcamdocument_list = new List<creditoragreement_upload>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getcamdocument_list.Add(new creditoragreement_upload
                        {
                            document_name = dt["document_name"].ToString(),
                            document_title = dt["document_title"].ToString(),
                            document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                            creditor_gid = dt["creditor_gid"].ToString(),
                            creditoragreement2docupload_gid = dt["creditoragreement2docupload_gid"].ToString(),
                            creditor2agreement_gid = dt["creditor2agreement_gid"].ToString(),


                        });
                        values.creditoragreement_upload = getcamdocument_list;
                    }
                }
                dt_datatable.Dispose();
                values.message = "Creditor Agreement Document deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occrued while deleting document";
                values.status = false;
            }
        }

        public void Dagetcreditordocument(string employee_gid, string creditor2agreement_gid, MdlcreditorMaster values)
        {
            msSQL = " select creditoragreement2docupload_gid,creditor_gid,document_name,document_path,document_title, creditor2agreement_gid from " +
                       " agr_mst_tcreditoragreement2docupload where creditor2agreement_gid='" + creditor2agreement_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcamdocument_list = new List<creditoragreement_upload>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcamdocument_list.Add(new creditoragreement_upload
                    {
                        document_name = dt["document_name"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        creditor_gid = dt["creditor_gid"].ToString(),
                        creditoragreement2docupload_gid = dt["creditoragreement2docupload_gid"].ToString(),
                        creditor2agreement_gid = dt["creditor2agreement_gid"].ToString(),


                    });
                    values.creditoragreement_upload = getcamdocument_list;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaDeleteAgreementDetail(string creditor2agreement_gid, result values)
        {
            msSQL = "delete from agr_mst_tcreditor2agreement where creditor2agreement_gid='" + creditor2agreement_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_tcreditor2agreement where creditor2agreement_gid='" + creditor2agreement_gid + "'";
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

        public void DaGetCreditorTmpClear(string employee_gid)
        {
            msSQL = " delete from  agr_mst_tcreditor2address where creditor_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tcreditor2address where creditor_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tcreditor2branch where creditor_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tcreditoragreement2docupload where creditor_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tcreditor2agreement where creditor_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from  agr_mst_tcreditor2cheque where creditor_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from  agr_mst_tcreditorcheque2document where creditor_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        }


        public void DaPostCreditorSubmit(string employee_gid, MdlcreditorCreation values)
        {

            msSQL = "select creditor_gid from agr_mst_tcreditor2address where creditor_gid='" + employee_gid + "' and primary_status='Yes' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Primary Address";
                return;
            }
            objODBCDatareader.Close();


            msSQL = "select creditor_gid from agr_mst_tcreditor2cheque where creditor_gid='" + employee_gid + "' and primary_status='Yes' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Primary Cheque details";
                return;
            }
            objODBCDatareader.Close();
            if (values.Gstflag == "Yes")
            {
                msSQL = "select creditor_gid from agr_mst_tcreditor2branch where creditor_gid='" + employee_gid + "' and headoffice_status ='Yes' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == false)
                {
                    values.status = false;
                    values.message = "Atleast Select One GST Number as Head Office";
                    return;
                }
                objODBCDatareader.Close();
            }
            msgetgidSA = objcmnfunctions.GetMasterGID("SAWH");
            msgetgidSA = msgetgidSA.Replace("SAWH", "");
            values.creditorref_no = $"{msgetgidSA:00000}";
            values.creditorref_no = "5" + values.creditorref_no;

            msGetGid = objcmnfunctions.GetMasterGID("CRDT");

            msSQL = " insert into agr_mst_tcreditor(" +
                   " creditor_gid," +
                   " creditorref_no," +
                   " Applicant_name," +
                  " Applicant_category," +
                   " Applicanttype_gid," +
                   " Applicant_type," +
                   " loanproduct_gid," +
                   " loanproduct_name," +
                   " loansubproduct_gid," +
                   " loansubproduct_name," +
                   " designation_gid, " +
                   " designation_type," +
                   " contactperson_name," +
                   " contact_no," +
                   " email_id, " +
                   " pan_no, " +
                   " aadhar_no, " +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.creditorref_no + "'," +
                   "'" + values.Applicant_name.Replace("'", "") + "'," +
                    "'" + values.Applicant_category + "'," +
                       "'" + values.Applicanttype_gid + "'," +
                       "'" + values.Applicant_type + "'," +
                     "'" + values.loanproduct_gid + "'," +
                     "'" + values.loanproduct_name + "'," +
                     "'" + values.loansubproduct_gid + "'," +
                     "'" + values.loansubproduct_name + "'," +
                     "'" + values.designation_gid + "'," +
                     "'" + values.designation_type + "',";
            if (values.contactperson_name == null || values.contactperson_name == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.contactperson_name.Replace("'", "") + "',";
            }
            msSQL +=
                     "'" + values.contact_no + "'," +
                     "'" + values.email_id + "'," +
                     "'" + values.pan_no + "'," +
                      "'" + values.aadhar_no + "'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {


                for (var i = 0; i < values.multiloanproduct_list.Count; i++)
                {
                    msGetPDGid = objcmnfunctions.GetMasterGID("C2PU");
                    msSQL = "Insert into agr_mst_tcreditor2product( " +
                           " creditor2product_gid, " +
                           " creditor_gid," +
                           " loanproduct_gid," +
                           " loanproduct_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetPDGid + "'," +
                           "'" + msGetGid + "'," +
                           "'" + values.multiloanproduct_list[i].loanproduct_gid + "'," +
                           "'" + values.multiloanproduct_list[i].loanproduct_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                }
                string loansubproduct_gid = string.Empty;
                string loansubproduct_name = string.Empty;

                if (values.multiloansubproduct_list != null)
                {
                    for (var j = 0; j < values.multiloansubproduct_list.Count; j++)
                    {
                        loansubproduct_gid += "'" + values.multiloansubproduct_list[j].loansubproduct_gid + "'" + ",";

                    }

                    loansubproduct_gid = loansubproduct_gid.TrimEnd(',');
                }

                msSQL = "select a.loansubproduct_name,a.loansubproduct_gid,a.loanproduct_gid,a.loanproduct_name, b.creditor2product_gid from agr_mst_tloansubproduct a " +
                "left join agr_mst_tcreditor2product b on b.loanproduct_gid = a.loanproduct_gid " +
                     " where loansubproduct_gid in (" + loansubproduct_gid + ") and b.creditor_gid ='" + msGetGid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {

                        msGetPGGid = objcmnfunctions.GetMasterGID("CP2P");
                        msSQL = "Insert into agr_mst_tcreditorproduct2program( " +
                                " creditorproduct2program_gid, " +
                               " creditor2product_gid, " +
                               " creditor_gid," +
                               " loanproduct_gid," +
                               " loanproduct_name," +
                               " loansubproduct_gid," +
                               " loansubproduct_name," +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                                "'" + msGetPGGid + "'," +
                                "'" + dt["creditor2product_gid"].ToString() + "'," +
                                "'" + msGetGid + "'," +
                                "'" + dt["loanproduct_gid"].ToString() + "'," +
                                "'" + dt["loanproduct_name"].ToString() + "'," +
                                "'" + dt["loansubproduct_gid"].ToString() + "'," +
                                "'" + dt["loansubproduct_name"].ToString() + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();



                //Updates

                msSQL = "update agr_mst_tcreditor2address set creditor_gid ='" + msGetGid + "' where creditor_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tcreditor2branch set creditor_gid ='" + msGetGid + "' where creditor_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tcreditoragreement2docupload set creditor_gid ='" + msGetGid + "' where creditor_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tcreditor2agreement set creditor_gid ='" + msGetGid + "' where creditor_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tcreditor2cheque set creditor_gid='" + msGetGid + "' where creditor_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tcreditorcheque2document set creditor_gid='" + msGetGid + "' where creditor_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Creditor Details Saved Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }

        public void DacreditorGSTTmpList(string employee_gid, string creditor_gid, MdlcreditorGST values)
        {
            msSQL = "select creditor2branch_gid,gst_state,gst_no, gst_registered,headoffice_status,creditor_gid from agr_mst_tcreditor2branch " +
                " where creditor_gid='" + creditor_gid + "' or creditor_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstgst_list = new List<creditorgst_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstgst_list.Add(new creditorgst_list
                    {
                        creditor2branch_gid = (dr_datarow["creditor2branch_gid"].ToString()),
                        gst_state = (dr_datarow["gst_state"].ToString()),
                        gst_no = (dr_datarow["gst_no"].ToString()),
                        gst_registered = (dr_datarow["gst_registered"].ToString()),
                        headoffice_status = (dr_datarow["headoffice_status"].ToString()),
                        creditor_gid = (dr_datarow["creditor_gid"].ToString())

                    });
                }
                values.creditorgst_list = getmstgst_list;
            }
            dt_datatable.Dispose();
        }
        public void DaUpdateGSTHeadOffice(string employee_gid, MdlCreditorGSTHeadOffice values)
        {
            msSQL = " update agr_mst_tcreditor2branch set headoffice_status = 'Yes' " +
                    " where creditor2branch_gid = '" + values.creditor2branch_gid + "' " +
                    " and creditor_gid = '" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = " update agr_mst_tcreditor2branch set headoffice_status='No' " +
                        " where creditor2branch_gid<>'" + values.creditor2branch_gid + "' " +
                        " and creditor_gid='" + employee_gid + "'";
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


        public void DaUpdateGSTHeadOfficeEdit(string employee_gid, MdlCreditorGSTHeadOffice values)
        {
            msSQL = " update agr_mst_tcreditor2branch set headoffice_status = 'Yes' " +
                    " where creditor2branch_gid = '" + values.creditor2branch_gid + "' " +
                    " and (creditor_gid = '" + employee_gid + "' or creditor_gid = '" + values.creditor_gid + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = " update agr_mst_tcreditor2branch set headoffice_status='No' " +
                        " where creditor2branch_gid<>'" + values.creditor2branch_gid + "' " +
                        " and (creditor_gid = '" + employee_gid + "' or creditor_gid = '" + values.creditor_gid + "')";
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

        public void DacreditorAddressTmpList(string creditor_gid, string employee_gid, MdlcreditorAddressDetails values)
        {
            msSQL = "  select creditor2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, city, state, country, latitude, longitude, landmark," +
                    " postal_code from agr_mst_tcreditor2address where creditor_gid='" + creditor_gid + "' or creditor_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstaddress_list = new List<creditoraddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstaddress_list.Add(new creditoraddress_list
                    {
                        creditor2address_gid = (dr_datarow["creditor2address_gid"].ToString()),
                        address_type = (dr_datarow["addresstype_name"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        addressline1 = (dr_datarow["addressline1"].ToString()),
                        addressline2 = (dr_datarow["addressline2"].ToString()),
                        taluka = (dr_datarow["taluka"].ToString()),
                        district = (dr_datarow["district"].ToString()),
                        state = (dr_datarow["state"].ToString()),
                        city = (dr_datarow["city"].ToString()),
                        country = (dr_datarow["country"].ToString()),
                        latitude = (dr_datarow["latitude"].ToString()),
                        longitude = (dr_datarow["longitude"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString()),
                        landmark = (dr_datarow["landmark"].ToString())
                    });
                }
                values.creditoraddress_list = getmstaddress_list;
            }
            dt_datatable.Dispose();
        }
       
        public void DaGetcreditorAgreementDetailsTmpList(string employee_gid, string creditor_gid, MdlcreditorMaster values)
        {
            msSQL = "select creditor2agreement_gid, creditor_gid, creditor2agreement_no, agreementinvolvement_type, samcontactperson_gid, samcontactperson_name, " +
                    " date_format(execution_date,'%d-%m-%Y') as execution_date, date_format(expiry_date,'%d-%m-%Y') as expiry_date " +
                    " from agr_mst_tcreditor2agreement  where creditor_gid='" + creditor_gid + "' or creditor_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbuyerbank_list = new List<Mdlcreditoragreementdtllist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbuyerbank_list.Add(new Mdlcreditoragreementdtllist
                    {
                        creditor2agreement_gid = (dr_datarow["creditor2agreement_gid"].ToString()),
                        creditor_gid = (dr_datarow["creditor_gid"].ToString()),
                        creditor2agreement_no = (dr_datarow["creditor2agreement_no"].ToString()),
                        agreementinvolvement_type = (dr_datarow["agreementinvolvement_type"].ToString()),
                        samcontactperson_gid = (dr_datarow["samcontactperson_gid"].ToString()),
                        samcontactperson_name = (dr_datarow["samcontactperson_name"].ToString()),
                        expiry_date = (dr_datarow["expiry_date"].ToString()),
                        execution_date = (dr_datarow["execution_date"].ToString()),

                    });


                }
                values.Mdlcreditoragreementdtllist = getbuyerbank_list;
            }
            dt_datatable.Dispose();
        }

        public void DacreditorDocumentUploadTmpList(string creditor2agreement_gid, string employee_gid, MdlcreditorMaster values)
        {
            msSQL = " select creditoragreement2docupload_gid,creditor_gid,document_name,document_path,document_title,creditor2agreement_gid from " +
                       " agr_mst_tcreditoragreement2docupload where creditor2agreement_gid='" + creditor2agreement_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcamdocument_list = new List<creditoragreement_upload>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcamdocument_list.Add(new creditoragreement_upload
                    {
                        document_name = dt["document_name"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        creditor_gid = dt["creditor_gid"].ToString(),
                        creditoragreement2docupload_gid = dt["creditoragreement2docupload_gid"].ToString(),
                        creditor2agreement_gid = dt["creditor2agreement_gid"].ToString(),


                    });
                    values.creditoragreement_upload = getcamdocument_list;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaEditcreditorDetails(string creditor_gid, MdlcreditorCreation values)
        {
            try
            {
                msSQL = " select a.creditor_gid, a.creditorref_no, a.Applicant_name, a.Applicant_category, a.Applicanttype_gid, a.Applicant_type,   " +
                        " a.loanproduct_gid, a.loanproduct_name, a.loansubproduct_gid, a.loansubproduct_name, a.designation_gid, a.designation_type," +
                        " a.contact_no, a.contactperson_name, a.email_id, a.pan_no, a.aadhar_no, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date " +
                        " from agr_mst_tcreditor a " +
                        " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                        " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                        " where a.creditor_gid='" + creditor_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.creditor_gid = objODBCDatareader["creditor_gid"].ToString();
                    values.creditorref_no = objODBCDatareader["creditorref_no"].ToString();
                    values.Applicant_category = objODBCDatareader["Applicant_category"].ToString();
                    values.Applicant_name = objODBCDatareader["Applicant_name"].ToString();
                    values.Applicant_type = objODBCDatareader["Applicant_type"].ToString();
                    values.Applicanttype_gid = objODBCDatareader["Applicanttype_gid"].ToString();
                    values.loanproduct_gid = objODBCDatareader["loanproduct_gid"].ToString();
                    values.loanproduct_name = objODBCDatareader["loanproduct_name"].ToString();
                    values.loansubproduct_gid = objODBCDatareader["loansubproduct_gid"].ToString();
                    values.loansubproduct_name = objODBCDatareader["loansubproduct_name"].ToString();
                    values.designation_gid = objODBCDatareader["designation_gid"].ToString();
                    values.designation_type = objODBCDatareader["designation_type"].ToString();
                    values.contactperson_name = objODBCDatareader["contactperson_name"].ToString();
                    values.contact_no = objODBCDatareader["contact_no"].ToString();
                    values.email_id = objODBCDatareader["email_id"].ToString();
                    values.pan_no = objODBCDatareader["pan_no"].ToString();
                    values.created_by = objODBCDatareader["created_by"].ToString();
                    values.created_date = objODBCDatareader["created_date"].ToString();
                    values.aadhar_no = objODBCDatareader["aadhar_no"].ToString();

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


        public void DaGetApprovedcreditorSummary(string employee_gid, MdlcreditorSummary values)
        {
            msSQL = " select a.creditor_gid,a.creditorref_no, a.Applicant_name, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                    "  from agr_mst_tcreditor a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " where a.approval_status='Y'" +
                    "  group by creditor_gid order by creditor_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbuyerbank_list = new List<MdlcreditorCreation>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbuyerbank_list.Add(new MdlcreditorCreation
                    {
                        creditor_gid = (dr_datarow["creditor_gid"].ToString()),
                        creditorref_no = (dr_datarow["creditorref_no"].ToString()),
                        Applicant_name = (dr_datarow["Applicant_name"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                    });
                }
                values.MdlcreditorCreation = getbuyerbank_list;
            }
            dt_datatable.Dispose();
        }


        public void DaGetNewcreditorSummary(string employee_gid, MdlcreditorSummary values)
        {
            msSQL = " select a.creditor_gid,a.creditorref_no, a.Applicant_name, a.Applicant_category,a.pan_no, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.designation_type, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, a.contactperson_name, a.contact_no, email_id " +
                    "  from agr_mst_tcreditor a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " where a.approval_status='Y' and a.Applicant_type not Like '%Warehouse Service Provider (WSP)%' " +
                    "  group by creditor_gid order by creditor_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbuyerbank_list = new List<MdlcreditorCreation>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbuyerbank_list.Add(new MdlcreditorCreation
                    {
                        creditor_gid = (dr_datarow["creditor_gid"].ToString()),
                        creditorref_no = (dr_datarow["creditorref_no"].ToString()),
                        Applicant_name = (dr_datarow["Applicant_name"].ToString()),
                        Applicant_category = (dr_datarow["Applicant_category"].ToString()),
                        pan_no = (dr_datarow["pan_no"].ToString()),
                        designation_type = (dr_datarow["designation_type"].ToString()),
                        contactperson_name = (dr_datarow["contactperson_name"].ToString()),
                        contact_no = (dr_datarow["contact_no"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                    });
                }
                values.MdlcreditorCreation = getbuyerbank_list;
            }
            dt_datatable.Dispose();
        }


        public void DaGetcreditorapplicanttype(string employee_gid, MdlcreditorSummary values)
        {
            msSQL = " select a.creditor_gid,a.creditorref_no, a.Applicant_name, Applicant_type, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                    "  from agr_mst_tcreditor a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " where a.approval_status='Y' and a.Applicant_type Like '%Warehouse Service Provider (WSP)%' " +
                    "  group by creditor_gid order by creditor_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbuyerbank_list = new List<MdlcreditorCreation>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbuyerbank_list.Add(new MdlcreditorCreation
                    {
                        creditor_gid = (dr_datarow["creditor_gid"].ToString()),
                        creditorref_no = (dr_datarow["creditorref_no"].ToString()),
                        Applicant_name = (dr_datarow["Applicant_name"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        Applicant_type = (dr_datarow["Applicant_type"].ToString()),
                    });
                }
                values.MdlcreditorCreation = getbuyerbank_list;
            }
            dt_datatable.Dispose();
        }

        public void DawarehouseGSTView(string employee_gid, string creditor_gid, MdlcreditorGST values)
        {
            msSQL = "select creditor2branch_gid,gst_state,gst_no, gst_registered,headoffice_status from agr_mst_tcreditor2branch " +
                " where creditor_gid='" + creditor_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstgst_list = new List<creditorgst_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstgst_list.Add(new creditorgst_list
                    {
                        creditor2branch_gid = (dr_datarow["creditor2branch_gid"].ToString()),
                        gst_state = (dr_datarow["gst_state"].ToString()),
                        gst_no = (dr_datarow["gst_no"].ToString()),
                        gst_registered = (dr_datarow["gst_registered"].ToString()),
                        headoffice_status = (dr_datarow["headoffice_status"].ToString())
                    });
                }
                values.creditorgst_list = getmstgst_list;
            }
            dt_datatable.Dispose();
        }


        public void DawarehouseAddressView(string employee_gid, string creditor_gid, MdlcreditorAddressDetails values)
        {
            msSQL = "  select creditor2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka,city, district, state, country, latitude, longitude, landmark," +
                    " postal_code from agr_mst_tcreditor2address where creditor_gid='" + creditor_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstaddress_list = new List<creditoraddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstaddress_list.Add(new creditoraddress_list
                    {
                        creditor2address_gid = (dr_datarow["creditor2address_gid"].ToString()),
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
                values.creditoraddress_list = getmstaddress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaGetcreditorAgreementDetailsView(string employee_gid, string creditor_gid, MdlcreditorMaster values)
        {
            msSQL = "select creditor2agreement_gid, creditor_gid, creditor2agreement_no, agreementinvolvement_type, samcontactperson_gid, samcontactperson_name, " +
                    " date_format(execution_date,'%d-%m-%Y') as execution_date, date_format(expiry_date,'%d-%m-%Y') as expiry_date " +
                    " from agr_mst_tcreditor2agreement  where creditor_gid='" + creditor_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbuyerbank_list = new List<Mdlcreditoragreementdtllist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbuyerbank_list.Add(new Mdlcreditoragreementdtllist
                    {
                        creditor2agreement_gid = (dr_datarow["creditor2agreement_gid"].ToString()),
                        creditor_gid = (dr_datarow["creditor_gid"].ToString()),
                        creditor2agreement_no = (dr_datarow["creditor2agreement_no"].ToString()),
                        agreementinvolvement_type = (dr_datarow["agreementinvolvement_type"].ToString()),
                        samcontactperson_gid = (dr_datarow["samcontactperson_gid"].ToString()),
                        samcontactperson_name = (dr_datarow["samcontactperson_name"].ToString()),
                        expiry_date = (dr_datarow["expiry_date"].ToString()),
                        execution_date = (dr_datarow["execution_date"].ToString()),

                    });


                }
                values.Mdlcreditoragreementdtllist = getbuyerbank_list;
            }
            dt_datatable.Dispose();
        }

        public bool DaUpdatecreditorDtl(MdlcreditorCreation values, string employee_gid)
        {

            msSQL = "select creditor_gid from agr_mst_tcreditor2address where creditor_gid='" + employee_gid + "' or creditor_gid='" + values.creditor_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Add Atleast One Address Detail";
                return false;
            }

            msSQL = "select creditor_gid from agr_mst_tcreditor2cheque where creditor_gid='" + employee_gid + "' or creditor_gid='" + values.creditor_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One cheque details";
                return false;
            }
            objODBCDatareader.Close();
            if (values.Gstflag == "Yes")
            {
                //msSQL = "select creditor_gid from agr_mst_tcreditor2branch where creditor_gid='" + employee_gid + "' and headoffice_status ='Yes' ";
                msSQL = "select creditor2branch_gid from agr_mst_tcreditor2branch where (creditor_gid='" + employee_gid + "' or creditor_gid='" + values.creditor_gid + "') and headoffice_status ='Yes' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == false)
                {
                    values.status = false;
                    values.message = "Atleast Select One GST Number as Head Office";
                    return false;
                }
                objODBCDatareader.Close();
            }

            msSQL = " select a.creditor_gid, a.creditorref_no, a.Applicant_name, a.Applicant_category, a.Applicanttype_gid, a.Applicant_type,   " +
                        " a.loanproduct_gid, a.loanproduct_name, a.loansubproduct_gid, a.loansubproduct_name, a.designation_gid, a.designation_type," +
                        " a.contact_no, a.contactperson_name, a.email_id, a.pan_no, a.aadhar_no, a.created_by, a.created_date " +
                     " from agr_mst_tcreditor a where a.creditor_gid='" + values.creditor_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lscreditor_gid = objODBCDatareader["creditor_gid"].ToString();
                lscreditorref_no = objODBCDatareader["creditorref_no"].ToString();
                lsApplicant_name = objODBCDatareader["Applicant_name"].ToString();
                lsApplicant_category = objODBCDatareader["Applicant_category"].ToString();
                lsApplicanttype_gid = objODBCDatareader["Applicanttype_gid"].ToString();
                lsApplicant_type = objODBCDatareader["Applicant_type"].ToString();
                lsloanproduct_gid = objODBCDatareader["loanproduct_gid"].ToString();
                lsloanproduct_name = objODBCDatareader["loanproduct_name"].ToString();
                lsloansubproduct_gid = objODBCDatareader["loansubproduct_gid"].ToString();
                lsloansubproduct_name = objODBCDatareader["loansubproduct_name"].ToString();
                lsdesignation_gid = objODBCDatareader["designation_gid"].ToString();
                lsdesignation_type = objODBCDatareader["designation_type"].ToString();
                lscontact_no = objODBCDatareader["contact_no"].ToString();
                lscontactperson_name = objODBCDatareader["contactperson_name"].ToString();
                lsemail_id = objODBCDatareader["email_id"].ToString();
                lspan_no = objODBCDatareader["pan_no"].ToString();
                lscreated_by = objODBCDatareader["created_by"].ToString();
                lscreated_date = objODBCDatareader["created_date"].ToString();
                lsaadhar_no = objODBCDatareader["aadhar_no"].ToString();




            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_tcreditor set " +
                        " creditorref_no='" + values.creditorref_no + "'," +

                         " Applicant_name='" + values.Applicant_name.Replace("'", "") + "'," +
                         " Applicant_category='" + values.Applicant_category + "'," +
                         " Applicanttype_gid='" + values.Applicanttype_gid + "'," +
                         " Applicant_type='" + values.Applicant_type + "'," +
                         " loanproduct_gid='" + values.loanproduct_gid + "'," +
                         " loanproduct_name='" + values.loanproduct_name + "'," +
                         " loansubproduct_gid='" + values.loansubproduct_gid + "'," +
                         " loansubproduct_name='" + values.loansubproduct_name + "'," +
                         " designation_gid='" + values.designation_gid + "'," +
                         " designation_type='" + values.designation_type + "'," +
                         " contact_no='" + values.contact_no + "',";
                if ((values.contactperson_name == null) || (values.contactperson_name == ""))
                {
                    msSQL += "contactperson_name=null,";
                }

                else
                {
                    msSQL += " contactperson_name='" + values.contactperson_name.Replace("'", "") + "',";
                }

                msSQL +=
                         " email_id='" + values.email_id + "'," +
                         " pan_no='" + values.pan_no + "'," +
                         " aadhar_no='" + values.aadhar_no + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where creditor_gid='" + values.creditor_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {

                    msGetGid = objcmnfunctions.GetMasterGID("CRUL");

                    msSQL = " insert into agr_mst_tcreditorupdatelog(" +
                    " creditorupdatelog_gid," +
                     " creditor_gid," +
                   " creditorref_no," +
                   " Applicant_name," +
                   " Applicant_category," +
                   " Applicanttype_gid," +
                   " Applicant_type," +
                   " loanproduct_gid," +
                   " loanproduct_name," +
                   " loansubproduct_gid," +
                   " loansubproduct_name," +
                   " designation_gid, " +
                   " designation_type," +
                   " contactperson_name," +
                   " contact_no," +
                   " email_id, " +
                   " pan_no, " +
                   " aadhar_no, " +
                  " created_by," +
                   " created_date)" +
                   " values(" +
                     "'" + msGetGid + "'," +
                      "'" + lscreditor_gid + "'," +
                      "'" + lscreditorref_no + "'," +
                        "'" + lsApplicant_name + "'," +
                        "'" + lsApplicant_category + "'," +
                        "'" + lsApplicanttype_gid + "'," +
                               "'" + lsApplicant_type + "'," +
                               "'" + lsloanproduct_gid + "'," +
                               "'" + lsloanproduct_name + "'," +
                               "'" + lsloansubproduct_gid + "'," +
                               "'" + lsloansubproduct_name + "'," +
                               "'" + lsdesignation_gid + "'," +
                               "'" + lsdesignation_type + "'," +
                               "'" + lscontactperson_name + "'," +
                               "'" + lscontact_no + "'," +
                               "'" + lsemail_id + "'," +
                               "'" + lspan_no + "'," +
                               "'" + lsaadhar_no + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "delete from agr_mst_tcreditorproduct2program where creditor_gid='" + values.creditor_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "delete from agr_mst_tcreditor2product where creditor_gid='" + values.creditor_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    for (var i = 0; i < values.multiloanproduct_list.Count; i++)
                    {
                        msGetPDGid = objcmnfunctions.GetMasterGID("C2PU");
                        msSQL = "Insert into agr_mst_tcreditor2product( " +
                               " creditor2product_gid, " +
                               " creditor_gid," +
                               " loanproduct_gid," +
                               " loanproduct_name," +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + msGetPDGid + "'," +
                               "'" + values.creditor_gid + "'," +
                               "'" + values.multiloanproduct_list[i].loanproduct_gid + "'," +
                               "'" + values.multiloanproduct_list[i].loanproduct_name + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    }
                    string loansubproduct_gid = string.Empty;
                    string loansubproduct_name = string.Empty;

                    if (values.multiloansubproduct_list != null)
                    {
                        for (var j = 0; j < values.multiloansubproduct_list.Count; j++)
                        {
                            loansubproduct_gid += "'" + values.multiloansubproduct_list[j].loansubproduct_gid + "'" + ",";

                        }

                        loansubproduct_gid = loansubproduct_gid.TrimEnd(',');
                    }

                    msSQL = "select a.loansubproduct_name,a.loansubproduct_gid,a.loanproduct_gid,a.loanproduct_name, b.creditor2product_gid from agr_mst_tloansubproduct a " +
                    "left join agr_mst_tcreditor2product b on b.loanproduct_gid = a.loanproduct_gid " +
                         " where loansubproduct_gid in (" + loansubproduct_gid + ") and b.creditor_gid ='" + values.creditor_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {

                            msGetPGGid = objcmnfunctions.GetMasterGID("CP2P");
                            msSQL = "Insert into agr_mst_tcreditorproduct2program( " +
                                    " creditorproduct2program_gid, " +
                                   " creditor2product_gid, " +
                                   " creditor_gid," +
                                   " loanproduct_gid," +
                                   " loanproduct_name," +
                                   " loansubproduct_gid," +
                                   " loansubproduct_name," +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                    "'" + msGetPGGid + "'," +
                                    "'" + dt["creditor2product_gid"].ToString() + "'," +
                                    "'" + values.creditor_gid + "'," +
                                    "'" + dt["loanproduct_gid"].ToString() + "'," +
                                    "'" + dt["loanproduct_name"].ToString() + "'," +
                                    "'" + dt["loansubproduct_gid"].ToString() + "'," +
                                    "'" + dt["loansubproduct_name"].ToString() + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    dt_datatable.Dispose();


                    // Updates for Multiple Add
                    msSQL = "update agr_mst_tcreditor2branch set creditor_gid='" + values.creditor_gid + "' where creditor_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tcreditor2address set creditor_gid='" + values.creditor_gid + "' where creditor_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tcreditoragreement2docupload set creditor_gid='" + values.creditor_gid + "' where creditor_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tcreditor2agreement set creditor_gid ='" + values.creditor_gid + "' where creditor_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tcreditor2cheque set creditor_gid ='" + values.creditor_gid + "' where creditor_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tcreditorcheque2document set creditor_gid ='" + values.creditor_gid + "' where creditor_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                   // other creditor Update
                    if (ConfigurationManager.AppSettings["sysSamagroHyperbrdigeAPIEnable"].ToString() == "Yes")
                    {
                        HttpContext ctx = HttpContext.Current;

                        Thread t = new Thread(new ThreadStart(() =>
                        {
                            HttpContext.Current = ctx;

                            objFnSamAgroHAPIOtherCreditor.UpdateOtherCreditorHBAPI(values.creditor_gid, employee_gid);

                            //Calling to update addresses not updated in ERP
                            objFnSamAgroHAPIOtherCreditor.UpdateOtherCreditorAddressAddHBAPI(values.creditor_gid);

                        }));

                        t.Start();
                    }

                    values.status = true;
                    values.message = "Creditor Details Updated Successfully";
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

        public void DaPostCreditorSubmitApproval(string employee_gid, Mdlcreditorapproval values)
        {

            msSQL = " update agr_mst_tcreditor set " +
                    //"initiated_remarks='" + values.initiated_remarks.Replace("'", "") + "'," +
                    " approval_submittedflag='Y'," +
                    " approval_submitteddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where creditor_gid='" + values.creditor_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Submitted to Approval Successfully!";

            }
            else
            {
                values.status = false;
                values.message = "Error Occured!";
            }

        }


        public void DaPostCreditorApproval(string employee_gid, Mdlcreditorapproval values)
        {
            msSQL = " select concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as approvalperson_name from adm_mst_tuser a " +
                    " left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                    " where b.employee_gid = '" + employee_gid + "'";
            string lsapprovalperson_name = objdbconn.GetExecuteScalar(msSQL);

            if (values.approval_status == "Approved")
            {
                msSQL = " update agr_mst_tcreditor set approval_status='" + CreditorStatus.Approved + "', " +
                        " approval_remarks='" + values.approval_remarks.Replace("'", "") + "'," +
                        " approved_by='" + employee_gid + "'," +
                        " approvedby_name='" + lsapprovalperson_name + "'," +
                        " approved_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where creditor_gid='" + values.creditor_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {

                    values.status = true;
                    values.message = "Creditor Approved Successfully!";

                    if (ConfigurationManager.AppSettings["sysSamagroHyperbrdigeAPIEnable"].ToString() == "Yes")
                    {
                        HttpContext ctx = HttpContext.Current;

                        Thread t = new Thread(new ThreadStart(() =>
                        {
                            HttpContext.Current = ctx;

                            objFnSamAgroHAPIOtherCreditor.DaPostOtherCreditorAddHAPI(values.creditor_gid, employee_gid);

                        }));

                        t.Start();
                    }

                }

                else
                {
                    values.status = true;
                    values.message = "Error Occured!";


                }
            }
            else
            {
                msSQL = " update agr_mst_tcreditor set approval_status='" + CreditorStatus.Rejected + "', " +
                        " approval_remarks='" + values.approval_remarks.Replace("'", "") + "'," +
                        " approved_by='" + employee_gid + "'," +
                        " approvedby_name='" + lsapprovalperson_name + "'," +
                        " approved_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where creditor_gid='" + values.creditor_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    values.status = true;
                    values.message = "Creditor Rejected Successfully!";
                }
                else
                {
                    values.status = true;
                    values.message = "Error Occured!";
                }
            }
        }

        public void DaGetRejectedCreditorSummary(string employee_gid, MdlcreditorSummary values)
        {
            msSQL = " select a.creditor_gid,a.creditorref_no, a.Applicant_name," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as approved_by," +
                    " CASE WHEN( approval_status = 'R') THEN 'Rejected' " +
                    " ELSE '' END as approval_status , " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, date_format(a.approved_date,'%d-%m-%Y %H:%i %p') as approved_date" +
                    "  from agr_mst_tcreditor a" +
                    " left join hrm_mst_temployee d on d.employee_gid=a.approved_by " +
                    " left join adm_mst_tuser e on e.user_gid=d.user_gid " +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid where " +
                    " approval_status='" + CreditorStatus.Rejected + "' group by creditor_gid order by creditor_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbuyerbank_list = new List<MdlcreditorCreation>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbuyerbank_list.Add(new MdlcreditorCreation
                    {
                        creditor_gid = (dr_datarow["creditor_gid"].ToString()),
                        creditorref_no = (dr_datarow["creditorref_no"].ToString()),
                        Applicant_name = (dr_datarow["Applicant_name"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        rejected_date = (dr_datarow["approved_date"].ToString()),
                        rejected_by = (dr_datarow["approved_by"].ToString()),

                    });
                }
                values.MdlcreditorCreation = getbuyerbank_list;
            }
            dt_datatable.Dispose();
        }


        public void DaGetCreditorApprovedSummary(string employee_gid, MdlcreditorSummary values)
        {
            msSQL = " select a.creditor_gid,a.creditorref_no, a.Applicant_name," +
                 " concat(g.user_firstname,' ',g.user_lastname,' / ',g.user_code) as updated_by," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as approved_by," +
                    " CASE WHEN( approval_status = 'Y') THEN 'Approved' " +
                    " ELSE '' END as approval_status , " +
                      " date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, date_format(a.approved_date,'%d-%m-%Y %H:%i %p') as approved_date,a.erp_id" +
                    "  from agr_mst_tcreditor a" +
                     " left join hrm_mst_temployee d on d.employee_gid=a.approved_by " +
                    " left join adm_mst_tuser e on e.user_gid=d.user_gid " +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid  " +
                       " left join hrm_mst_temployee f on f.employee_gid=a.updated_by " +
                      " left join adm_mst_tuser g on g.user_gid=f.user_gid where " +
                    " approval_status='" + CreditorStatus.Approved + "' order by creditor_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<MdlcreditorCreation>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new MdlcreditorCreation
                    {
                        creditor_gid = (dr_datarow["creditor_gid"].ToString()),
                        creditorref_no = (dr_datarow["creditorref_no"].ToString()),
                        Applicant_name = (dr_datarow["Applicant_name"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        approved_date = (dr_datarow["approved_date"].ToString()),
                        approved_by = (dr_datarow["approved_by"].ToString()),
                        updated_by = (dr_datarow["updated_by"].ToString()),
                        updated_date = (dr_datarow["updated_date"].ToString()),
                        erp_id = (dr_datarow["erp_id"].ToString()),

                    });

                }
            }
            values.MdlcreditorCreation = getapplicationadd_list;
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetCreditorApprovalPendingSummary(string employee_gid, MdlcreditorSummary values)
        {
            msSQL = " select a.creditor_gid,a.creditorref_no, a.Applicant_name," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                    " CASE WHEN( approval_status = 'N') THEN 'Pending' " +
                    " ELSE '' END as approval_status , " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, date_format(a.approval_submitteddate,'%d-%m-%Y %h:%i %p') as approval_submitteddate" +
                    "  from agr_mst_tcreditor a" +
                     " left join hrm_mst_temployee d on d.employee_gid=a.approved_by " +
                    " left join adm_mst_tuser e on e.user_gid=d.user_gid " +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid where " +
                    " approval_status='" + CreditorStatus.Pending + "' and approval_submittedflag = 'Y' order by creditor_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<MdlcreditorCreation>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new MdlcreditorCreation
                    {
                        creditor_gid = (dr_datarow["creditor_gid"].ToString()),
                        creditorref_no = (dr_datarow["creditorref_no"].ToString()),
                        Applicant_name = (dr_datarow["Applicant_name"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        approval_submitteddate = (dr_datarow["approval_submitteddate"].ToString()),
                        //api_code = (dr_datarow["api_code"].ToString()),
                    });

                }
            }
            values.MdlcreditorCreation = getapplicationadd_list;
            dt_datatable.Dispose();
            values.status = true;
        }


        public void DaGetRMRejectedCreditorSummary(string employee_gid, MdlcreditorSummary values)
        {
            msSQL = " select a.creditor_gid,a.creditorref_no, a.Applicant_name, a.approval_status as app_status," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as approved_by," +
                    " CASE WHEN( approval_status = 'R') THEN 'Rejected' " +
                    " ELSE '' END as approval_status , " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, date_format(a.approved_date,'%d-%m-%Y %H:%i %p') as approved_date" +
                    "  from agr_mst_tcreditor a" +
                     " left join hrm_mst_temployee d on d.employee_gid=a.approved_by " +
                    " left join adm_mst_tuser e on e.user_gid=d.user_gid " +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid where a.created_by='" + employee_gid + "'and" +
                    " approval_status='" + CreditorStatus.Rejected + "' group by creditor_gid order by creditor_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbuyerbank_list = new List<MdlcreditorCreation>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbuyerbank_list.Add(new MdlcreditorCreation
                    {
                        creditor_gid = (dr_datarow["creditor_gid"].ToString()),
                        creditorref_no = (dr_datarow["creditorref_no"].ToString()),
                        Applicant_name = (dr_datarow["Applicant_name"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        rejected_date = (dr_datarow["approved_date"].ToString()),
                        rejected_by = (dr_datarow["approved_by"].ToString()),
                        approval_status = (dr_datarow["app_status"].ToString()),

                    });
                }
                values.MdlcreditorCreation = getbuyerbank_list;
            }
            dt_datatable.Dispose();
        }


        public void DaGetRMCreditorApprovedSummary(string employee_gid, MdlcreditorSummary values)
        {
            msSQL = " select a.creditor_gid,a.creditorref_no, a.Applicant_name, a.approval_status as app_status," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,  concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as approved_by," +
                    " CASE WHEN( approval_status = 'Y') THEN 'Approved' " +
                    " ELSE '' END as approval_status , " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, date_format(a.approved_date,'%d-%m-%Y %H:%i %p') as approved_date" +
                    "  from agr_mst_tcreditor a" +
                     " left join hrm_mst_temployee d on d.employee_gid=a.approved_by " +
                    " left join adm_mst_tuser e on e.user_gid=d.user_gid " +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid where a.created_by='" + employee_gid + "'and" +
                    " approval_status='" + CreditorStatus.Approved + "' order by creditor_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<MdlcreditorCreation>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new MdlcreditorCreation
                    {
                        creditor_gid = (dr_datarow["creditor_gid"].ToString()),
                        creditorref_no = (dr_datarow["creditorref_no"].ToString()),
                        Applicant_name = (dr_datarow["Applicant_name"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        approved_date = (dr_datarow["approved_date"].ToString()),
                        approved_by = (dr_datarow["approved_by"].ToString()),
                        approval_status = (dr_datarow["app_status"].ToString()),
                    });

                }
            }
            values.MdlcreditorCreation = getapplicationadd_list;
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetRMCreditorApprovalPendingSummary(string employee_gid, MdlcreditorSummary values)
        {
            msSQL = " select a.creditor_gid,a.creditorref_no, a.Applicant_name,  a.approval_status as app_status, query_status, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                    " CASE WHEN( approval_status = 'N') THEN 'Pending' " +
                    " ELSE '' END as approval_status , " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, date_format(a.approval_submitteddate,'%d-%m-%Y %h:%i %p') as approval_submitteddate" +
                    "  from agr_mst_tcreditor a" +
                     " left join hrm_mst_temployee d on d.employee_gid=a.approved_by " +
                    " left join adm_mst_tuser e on e.user_gid=d.user_gid " +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid where a.created_by='" + employee_gid + "'and" +
                    " approval_status='" + CreditorStatus.Pending + "' and approval_submittedflag = 'Y' order by creditor_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<MdlcreditorCreation>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new MdlcreditorCreation
                    {
                        creditor_gid = (dr_datarow["creditor_gid"].ToString()),
                        creditorref_no = (dr_datarow["creditorref_no"].ToString()),
                        Applicant_name = (dr_datarow["Applicant_name"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        approval_submitteddate = (dr_datarow["approval_submitteddate"].ToString()),
                        approval_status = (dr_datarow["app_status"].ToString()),
                        query_status = (dr_datarow["query_status"].ToString()),
                    });

                }
            }
            values.MdlcreditorCreation = getapplicationadd_list;
            dt_datatable.Dispose();
            values.status = true;
        }



        public void DaGetRMCreditorOpenSummary(string employee_gid, MdlcreditorSummary values)
        {
            msSQL = " select a.creditor_gid,a.creditorref_no, a.Applicant_name,  a.approval_status as app_status," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                    " CASE WHEN( approval_status = 'N' and approval_submittedflag = 'N') THEN 'Pending' " +
                    " ELSE '' END as approval_status , " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                    "  from agr_mst_tcreditor a" +
                     " left join hrm_mst_temployee d on d.employee_gid=a.approved_by " +
                    " left join adm_mst_tuser e on e.user_gid=d.user_gid " +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid where a.created_by='" + employee_gid + "' and" +
                    " approval_status='N' and approval_submittedflag = 'N' order by creditor_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<MdlcreditorCreation>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new MdlcreditorCreation
                    {
                        creditor_gid = (dr_datarow["creditor_gid"].ToString()),
                        creditorref_no = (dr_datarow["creditorref_no"].ToString()),
                        Applicant_name = (dr_datarow["Applicant_name"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        approval_status = (dr_datarow["app_status"].ToString()),
                    });

                }
            }
            values.MdlcreditorCreation = getapplicationadd_list;
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetRMCreditorCountDetail(string employee_gid, RMCreditorCountdtl values)
        {
            msSQL = " select (select count(*) from agr_mst_tcreditor where created_by = '" + employee_gid + "' " +
                    " and approval_status = 'N' and approval_submittedflag = 'N') as Open_Creditor, " +
                    " (select count(*) from agr_mst_tcreditor where created_by = '" + employee_gid + "' and approval_status = 'N' and approval_submittedflag = 'Y') as ApprovalPending_Creditor, " +
                    " (select count(*) from agr_mst_tcreditor where created_by = '" + employee_gid + "' and approval_status = 'Y' ) as Approved_Creditor," +
                    " (select count(*) from agr_mst_tcreditor where created_by = '" + employee_gid + "' and approval_status = 'R' ) as Rejected_Creditor, " +
                    "(select count(*) from agr_mst_tcreditor where created_by = '" + employee_gid + "' ) as Total_Creditor; ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.Open_Creditor = objODBCDatareader["Open_Creditor"].ToString();
                values.ApprovalPending_Creditor = objODBCDatareader["ApprovalPending_Creditor"].ToString();
                values.Approved_Creditor = objODBCDatareader["Approved_Creditor"].ToString();
                values.Rejected_Creditor = objODBCDatareader["Rejected_Creditor"].ToString();
                values.Total_Creditor = objODBCDatareader["Total_Creditor"].ToString();
            }
            objODBCDatareader.Close();
            values.status = true;
        }

        public void DaGetApprovalCreditorCountDetail(string employee_gid, RMCreditorCountdtl values)
        {
            msSQL = " select (select count(*) from agr_mst_tcreditor where created_by = '" + employee_gid + "' " +
                    " and approval_status = 'N' and approval_submittedflag = 'N') as Open_Creditor, " +
                    " (select count(*) from agr_mst_tcreditor where approval_status = 'N' and approval_submittedflag = 'Y') as ApprovalPending_Creditor, " +
                    " (select count(*) from agr_mst_tcreditor where approval_status = 'Y' ) as Approved_Creditor," +
                    " (select count(*) from agr_mst_tcreditor where approval_status = 'R' ) as Rejected_Creditor, " +
                    "(select count(*) from agr_mst_tcreditor where  approval_submittedflag = 'Y' ) as Total_Creditor; ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.Open_Creditor = objODBCDatareader["Open_Creditor"].ToString();
                values.ApprovalPending_Creditor = objODBCDatareader["ApprovalPending_Creditor"].ToString();
                values.Approved_Creditor = objODBCDatareader["Approved_Creditor"].ToString();
                values.Rejected_Creditor = objODBCDatareader["Rejected_Creditor"].ToString();
                values.Total_Creditor = objODBCDatareader["Total_Creditor"].ToString();
            }
            objODBCDatareader.Close();
            values.status = true;
        }


        public void DaGetCreditorApproveRejectSummary(string creditor_gid, MdlcreditorCreation values)
        {
            try
            {

                msSQL = " select a.creditor_gid,a.creditorref_no, a.Applicant_name, a.approval_status, a.approval_remarks," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,  concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as approved_by," +
                        " CASE WHEN( approval_status = 'Y') THEN 'Approved' " +
                         " WHEN( approval_status = 'R') THEN 'Rejected' " +
                        " ELSE '-' END as creditorapproval_status , " +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, date_format(a.approved_date,'%d-%m-%Y %H:%i %p') as approved_date" +
                        "  from agr_mst_tcreditor a" +
                         " left join hrm_mst_temployee d on d.employee_gid=a.approved_by " +
                        " left join adm_mst_tuser e on e.user_gid=d.user_gid " +
                        " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                        " left join adm_mst_tuser c on c.user_gid=b.user_gid where a.creditor_gid='" + creditor_gid + "'" +
                        " order by creditor_gid desc ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.approved_date = objODBCDatareader["approved_date"].ToString();
                    values.approved_by = objODBCDatareader["approved_by"].ToString();
                    values.rejected_date = objODBCDatareader["approved_date"].ToString();
                    values.rejected_by = objODBCDatareader["approved_by"].ToString();
                    values.creditorapproval_status = objODBCDatareader["creditorapproval_status"].ToString();
                    values.approval_status = objODBCDatareader["approval_status"].ToString();
                    values.approval_remarks = objODBCDatareader["approval_remarks"].ToString();

                }
                objODBCDatareader.Close();
                values.status = true;
                values.message = "success";
            }

            catch
            {
                values.status = false;
            }

        }

        public void DaCreditorProductEdit(string creditor_gid, MdlcreditorCreation values)
        {

            msSQL = " select distinct (loanproduct_gid),(loanproduct_name) from agr_mst_tcreditor2product " +
                 " where creditor_gid='" + creditor_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcreditorproduct_list = new List<creditorproduct_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcreditorproduct_list.Add(new creditorproduct_list
                    {
                        loanproduct_gid = dt["loanproduct_gid"].ToString(),
                        loanproduct_name = dt["loanproduct_name"].ToString(),

                    });
                    values.creditorproduct_list = getcreditorproduct_list;
                }
            }
            dt_datatable.Dispose();

        }

        public void DaCreditorProgramEdit(string creditor_gid, MdlcreditorCreation values)
        {

            msSQL = " select distinct (loansubproduct_gid),(loansubproduct_name) from agr_mst_tcreditorproduct2program " +
                 " where creditor_gid='" + creditor_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcreditorprogram_list = new List<creditorprogram_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcreditorprogram_list.Add(new creditorprogram_list
                    {
                        loansubproduct_gid = dt["loansubproduct_gid"].ToString(),
                        loansubproduct_name = dt["loansubproduct_name"].ToString(),

                    });
                    values.creditorprogram_list = getcreditorprogram_list;
                }
            }
            dt_datatable.Dispose();

        }


        public void DaCreditorSubProductEdit(string creditor_gid, MdlcreditorCreation values)
        {

            //msSQL = "call agr_mst_spcreditorproducteditlist(" + "'" + creditor_gid + "'" + ")";

            //msSQL = "select distinct( loanproduct_gid) as loanproduct_gid from agr_mst_tcreditor2product where creditor_gid= '" + creditor_gid + "'";
            //= objdbconn.GetExecuteScalar(msSQL);lsloanproduct_gid

            //dt_datatable2 = objdbconn.GetDataTable(msSQL);

            //if (dt_datatable2.Rows.Count != 0)
            //{
            //    foreach (DataRow dt2 in dt_datatable2.Rows)
            //    {

            //msSQL = "select distinct (loansubproduct_gid), (loansubproduct_name) from agr_mst_tcreditorproduct2program where loanproduct_gid ='" + dt2["loanproduct_gid"] + "' and creditor_gid= '" + creditor_gid + "' ";
            msSQL = "select distinct (loansubproduct_gid), (loansubproduct_name) from agr_mst_tcreditorproduct2program where  creditor_gid= '" + creditor_gid + "' ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getcreditorprogram_list = new List<creditorprogram_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getcreditorprogram_list.Add(new creditorprogram_list
                            {
                                loansubproduct_gid = dt["loansubproduct_gid"].ToString(),
                                loansubproduct_name = dt["loansubproduct_name"].ToString(),

                            });
                           
                        }
                    }
            values.creditorprogram_list = getcreditorprogram_list;
            dt_datatable.Dispose();


            //    }
            //}

            //else
            //{

            //}

        }


        public void DaCreditorProductView(string creditor_gid, MdlcreditorCreation values)
        {

            try
            {

                msSQL = " select GROUP_CONCAT(distinct(loanproduct_name)) as loanproduct_name  from agr_mst_tcreditor2product " +
                     " where creditor_gid='" + creditor_gid + "' ";

                values.loanproduct_name = objdbconn.GetExecuteScalar(msSQL);

            }

            catch (Exception ex)
            {

            }


        }


        public void DaCreditorProgramView(string creditor_gid, MdlcreditorCreation values)
        {

            try
            {
                //msSQL = "select GROUP_CONCAT('\\\'', loanproduct_gid,'\\\'') as loanproduct_gid from agr_mst_tcreditor2product where creditor_gid= '" + creditor_gid + "'";
                //lsloanproduct_gid = objdbconn.GetExecuteScalar(msSQL);

                //if (!string.IsNullOrEmpty(lsloanproduct_gid))
                //{


                msSQL = "select GROUP_CONCAT(distinct(loansubproduct_name)) as c from agr_mst_tcreditorproduct2program where creditor_gid= '" + creditor_gid + "'";

                values.loansubproduct_name = objdbconn.GetExecuteScalar(msSQL);


                //}

            }

            catch (Exception ex)
            {

            }


        }


        public void DaPostCreditorRaiseQuery(mdlcreditorraisequery values, string user_gid, string employee_gid)
        {

            msGetGid = objcmnfunctions.GetMasterGID("OCRQ");
            msSQL = "Insert into agr_mst_tcreditorquery( " +
                   " creditorquery_gid, " +
                   " creditor_gid," +
                   " query_title," +
                   " query_description," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.creditor_gid + "', " +
                   "'" + values.query_title.Replace("'", "") + "'," +
                   "'" + values.description.Replace("'", "") + "'," +
                   "'" + user_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {

                msSQL = "update agr_mst_tcreditor set  query_status ='Query Raised' " +
                           " where creditor_gid='" + values.creditor_gid + "'";
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

        public void DaGetCreditorRaiseQuerySummary(mdlcreditorraisequery values, string creditor_gid)
        {
            msSQL = " select creditorquery_gid, creditor_gid, query_title,query_status,query_description,close_remarks, " +
                     " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as created_by," +
                     " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date from agr_mst_tcreditorquery a" +
                     " left join adm_mst_tuser b on b.user_gid = a.created_by " +
                     " where a.creditor_gid ='" + creditor_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbyrraisequerylist = new List<creditorraisequerylist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getbyrraisequerylist.Add(new creditorraisequerylist
                    {
                        creditorquery_gid = dt["creditorquery_gid"].ToString(),
                        query_title = dt["query_title"].ToString(),
                        query_status = dt["query_status"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        query_description = dt["query_description"].ToString(),
                        close_remarks = dt["close_remarks"].ToString(),
                        creditor_gid = dt["creditor_gid"].ToString()
                    });
                    values.creditorraisequerylist = getbyrraisequerylist;
                }
            }
            dt_datatable.Dispose();


        }

        public void DaGetOpenQueryStatus(mdlcreditorraisequery values, string creditor_gid)
        {

            msSQL = " select creditorquery_gid from agr_mst_tcreditorquery where creditor_gid ='" + creditor_gid + "'" +
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


        public void DaGetRaiseQuerydesc(mdlcreditorraisequery values, string creditorquery_gid)
        {
            msSQL = "select query_title, query_description, close_remarks from agr_mst_tcreditorquery where creditorquery_gid='" + creditorquery_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.description = objODBCDatareader["query_description"].ToString();
                values.query_title = objODBCDatareader["query_title"].ToString();
                values.close_remarks = objODBCDatareader["close_remarks"].ToString();
            }
            objODBCDatareader.Close();
        }


        public void DaPostUpdateQueryStatus(mdlcreditorraisequery values, string user_gid)
        {
            msSQL = " update agr_mst_tcreditorquery set  query_status='Closed', close_remarks='" + values.close_remarks.Replace("'", "") + "'," +
                    " closed_by='" + user_gid + "', closed_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where creditorquery_gid ='" + values.creditorquery_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = " select creditorquery_gid from agr_mst_tcreditorquery where creditor_gid ='" + values.creditor_gid + "'" +
                        " and query_status ='Open'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {

                }
                else
                {
                    msSQL = "update agr_mst_tcreditor set  query_status ='Closed' " +
                           " where creditor_gid='" + values.creditor_gid + "'";
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


        public bool DaPostTrade2CreditorDtl(string employee_gid, string user_gid, Mdlcreditor2warehouse values)
        {

            msGetGid = objcmnfunctions.GetMasterGID("AT2C");
            msSQL = " insert into agr_mst_tapplicationtrade2creditor(" +
                    " applicationtrade2creditor_gid," +
                    " application2trade_gid," +
                    //" application2loan_gid," +
                    " application_gid," +
                    " creditor_gid," +
                    " Applicant_name," +
                    " Applicant_category," +
                    " designation_type," +
                    " contactperson_name," +
                    " contact_no," +
                    " email_id," +
                    " pan_no," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    //"'" + values.application2loan_gid + "'," +
                    "'" + values.application_gid + "'," +
                    "'" + values.creditor_gid + "'," +
                    "'" + values.Applicant_name + "'," +
                    "'" + values.Applicant_category + "'," +
                    "'" + values.designation_type + "'," +
                    "'" + values.contactperson_name + "'," +
                    "'" + values.email_id + "'," +
                    "'" + values.contact_no + "'," +
                    "'" + values.pan_no + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Trade Creditor Details Added Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }

        }


        public void DaGetTrade2CreditorDtl(string employee_gid, Mdlcreditor2warehouse values)
        {
            msSQL = "  select applicationtrade2creditor_gid, application2trade_gid,application2loan_gid,application_gid,creditor_gid,Applicant_name ,Applicant_category ," +
                    " designation_type,contactperson_name,contact_no,email_id,pan_no from agr_mst_tapplicationtrade2creditor where application2trade_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstaddress_list = new List<trade2creditor_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstaddress_list.Add(new trade2creditor_list
                    {
                        applicationtrade2creditor_gid = (dr_datarow["applicationtrade2creditor_gid"].ToString()),
                        application2trade_gid = (dr_datarow["application2trade_gid"].ToString()),
                        application_gid = (dr_datarow["application_gid"].ToString()),
                        creditor_gid = (dr_datarow["creditor_gid"].ToString()),
                        Applicant_name = (dr_datarow["Applicant_name"].ToString()),
                        Applicant_category = (dr_datarow["Applicant_category"].ToString()),
                        designation_type = (dr_datarow["designation_type"].ToString()),
                        contactperson_name = (dr_datarow["contactperson_name"].ToString()),
                        contact_no = (dr_datarow["contact_no"].ToString()),
                        email_id = (dr_datarow["email_id"].ToString()),
                        pan_no = (dr_datarow["pan_no"].ToString()),

                    });
                }
                values.trade2creditor_list = getmstaddress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaDeleteTrade2CreditorDtl(string applicationtrade2creditor_gid, string employee_gid, Mdlcreditor2warehouse values)
        {
            msSQL = "delete from agr_mst_tapplicationtrade2creditor where applicationtrade2creditor_gid='" + applicationtrade2creditor_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Trade Creditor Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }



         public void DaGetTrade2CreditorTmpDtl(string employee_gid, string application_gid, string application2trade_gid,  string tmp_status, string application2loan_gid, Mdlcreditor2warehouse values)
        {
            msSQL = "  select applicationtrade2creditor_gid, application2trade_gid,application2loan_gid,application_gid,creditor_gid,Applicant_name ,Applicant_category ," +
                    " designation_type,contactperson_name,contact_no,email_id,pan_no from agr_mst_tapplicationtrade2creditor ";


            if (tmp_status == "true")

                msSQL += " where application2trade_gid = '" + application2trade_gid + "'and application_gid = '" + application_gid + "' order by applicationtrade2creditor_gid asc";


            else
                msSQL += " where application2trade_gid = '" + employee_gid + "' and application_gid = '" + application_gid + "' order by applicationtrade2creditor_gid asc";


            

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstaddress_list = new List<trade2creditor_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstaddress_list.Add(new trade2creditor_list
                    {
                        applicationtrade2creditor_gid = (dr_datarow["applicationtrade2creditor_gid"].ToString()),
                        application2trade_gid = (dr_datarow["application2trade_gid"].ToString()),
                        application_gid = (dr_datarow["application_gid"].ToString()),
                        creditor_gid = (dr_datarow["creditor_gid"].ToString()),
                        Applicant_name = (dr_datarow["Applicant_name"].ToString()),
                        Applicant_category = (dr_datarow["Applicant_category"].ToString()),
                        designation_type = (dr_datarow["designation_type"].ToString()),
                        contactperson_name = (dr_datarow["contactperson_name"].ToString()),
                        contact_no = (dr_datarow["contact_no"].ToString()),
                        email_id = (dr_datarow["email_id"].ToString()),
                        pan_no = (dr_datarow["pan_no"].ToString()),
                    });
                }
                values.trade2creditor_list = getmstaddress_list;
            }
            dt_datatable.Dispose();
        }



        public bool DaPostEditTrade2CreditorDtl(string employee_gid, string user_gid, Mdlcreditor2warehouse values)
        {

            msGetGid = objcmnfunctions.GetMasterGID("AT2C");
            msSQL = " insert into agr_mst_tapplicationtrade2creditor(" +
                    " applicationtrade2creditor_gid," +
                    " application2trade_gid," +
                    " application2loan_gid," +
                    " application_gid," +
                    " creditor_gid," +
                    " Applicant_name," +
                    " Applicant_category," +
                    " designation_type," +
                    " contactperson_name," +
                    " contact_no," +
                    " email_id," +
                    " pan_no," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.application2trade_gid + "'," +
                    "'" + values.application2loan_gid + "'," +
                    "'" + values.application_gid + "'," +
                    "'" + values.creditor_gid + "'," +
                    "'" + values.Applicant_name + "'," +
                    "'" + values.Applicant_category + "'," +
                    "'" + values.designation_type + "'," +
                    "'" + values.contactperson_name + "'," +
                    "'" + values.email_id + "'," +
                    "'" + values.contact_no + "'," +
                    "'" + values.pan_no + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Trade Creditor Details Added Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }

        }


        public bool DaPostEditTrade2WarehouseDetail(string employee_gid, string user_gid, Mdlcreditor2warehouse values)
        {

            msGetGid = objcmnfunctions.GetMasterGID("AT2W");
            msSQL = " insert into agr_mst_tapplicationtrade2warehouse(" +
                    " applicationtrade2warehouse_gid," +
                    " application2trade_gid," +
                    " application2loan_gid," +
                    " application_gid," +
                    " creditor_gid," +
                    " warehouse_gid," +
                    " warehouse_agency," +
                    " warehouse_name," +
                    " typeofwarehouse_name," +
                    " volume_uom," +
                    " totalcapacity_volume," +
                    " totalcapacity_area," +
                    " area_uom," +
                    " warehouse2address_gid," +
                    " warehouse_address," +
                    " capacity_commodity," +
                    " capacity_panina," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.application2trade_gid + "'," +
                    "'" + values.application2loan_gid + "'," +
                    "'" + values.application_gid + "'," +
                    "'" + values.creditor_gid + "'," +
                    "'" + values.warehouse_gid + "'," +
                    "'" + values.warehouse_agency + "'," +
                    "'" + values.warehouse_name + "'," +
                    "'" + values.typeofwarehouse_name + "'," +
                    "'" + values.volume_uom + "'," +
                    "'" + values.totalcapacity_volume + "'," +
                    "'" + values.totalcapacity_area + "'," +
                    "'" + values.area_uom + "'," +
                    "'" + values.warehouse2address_gid + "'," +
                    "'" + values.warehouse_address + "'," +
                    "'" + values.capacity_commodity.Replace("'", "") + "'," +
                    "'" + values.capacity_panina.Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Trade Warehouse Details Added Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }

        }

        public void DaGetEditTrade2CreditorTmpDtl(string employee_gid, string application_gid, string application2trade_gid, string tmp_status, string application2loan_gid, Mdlcreditor2warehouse values)
        {
            msSQL = "  select applicationtrade2creditor_gid, application2trade_gid,application2loan_gid,application_gid,creditor_gid,Applicant_name ,Applicant_category ," +
                    " designation_type,contactperson_name,contact_no,email_id,pan_no from agr_mst_tapplicationtrade2creditor " +

                    " where application2trade_gid = '" + application2trade_gid + "'and application_gid = '" + application_gid + "' order by applicationtrade2creditor_gid asc";


            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstaddress_list = new List<trade2creditor_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstaddress_list.Add(new trade2creditor_list
                    {
                        applicationtrade2creditor_gid = (dr_datarow["applicationtrade2creditor_gid"].ToString()),
                        application2trade_gid = (dr_datarow["application2trade_gid"].ToString()),
                        application_gid = (dr_datarow["application_gid"].ToString()),
                        creditor_gid = (dr_datarow["creditor_gid"].ToString()),
                        Applicant_name = (dr_datarow["Applicant_name"].ToString()),
                        Applicant_category = (dr_datarow["Applicant_category"].ToString()),
                        designation_type = (dr_datarow["designation_type"].ToString()),
                        contactperson_name = (dr_datarow["contactperson_name"].ToString()),
                        contact_no = (dr_datarow["contact_no"].ToString()),
                        email_id = (dr_datarow["email_id"].ToString()),
                        pan_no = (dr_datarow["pan_no"].ToString()),
                    });
                }
                values.trade2creditor_list = getmstaddress_list;
            }
            dt_datatable.Dispose();
        }


        public void DaGetEditTrade2WarehouseTmpDetail(string employee_gid, string application_gid, string application2trade_gid, string tmp_status, string application2loan_gid, Mdlcreditor2warehouse values)
        {
            msSQL = "  select applicationtrade2warehouse_gid, application2trade_gid,application2loan_gid,application_gid,creditor_gid,warehouse_gid,warehouse_agency ,warehouse_name,typeofwarehouse_name ," +
                    " volume_uomgid,volume_uom,totalcapacity_volume,totalcapacity_area,totalcapacityarea_uomgid,area_uom,warehouse2address_gid,warehouse_address,capacity_commodity,capacity_panina " +
                    "from agr_mst_tapplicationtrade2warehouse " +
                " where application2trade_gid = '" + application2trade_gid + "'and application_gid = '" + application_gid + "' order by applicationtrade2warehouse_gid asc";


            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstaddress_list = new List<creditor2warehouse_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstaddress_list.Add(new creditor2warehouse_list
                    {
                        applicationtrade2warehouse_gid = (dr_datarow["applicationtrade2warehouse_gid"].ToString()),
                        application2trade_gid = (dr_datarow["application2trade_gid"].ToString()),
                        application_gid = (dr_datarow["application_gid"].ToString()),
                        creditor_gid = (dr_datarow["creditor_gid"].ToString()),
                        warehouse_gid = (dr_datarow["warehouse_gid"].ToString()),
                        warehouse_agency = (dr_datarow["warehouse_agency"].ToString()),
                        warehouse_name = (dr_datarow["warehouse_name"].ToString()),
                        typeofwarehouse_name = (dr_datarow["typeofwarehouse_name"].ToString()),
                        volume_uom = (dr_datarow["volume_uom"].ToString()),
                        totalcapacity_volume = (dr_datarow["totalcapacity_volume"].ToString()),
                        totalcapacity_area = (dr_datarow["totalcapacity_area"].ToString()),
                        area_uom = (dr_datarow["area_uom"].ToString()),
                        warehouse2address_gid = (dr_datarow["warehouse2address_gid"].ToString()),
                        warehouse_address = (dr_datarow["warehouse_address"].ToString()),
                        capacity_commodity = (dr_datarow["capacity_commodity"].ToString()),
                        capacity_panina = (dr_datarow["capacity_panina"].ToString()),
                    });
                }
                values.creditor2warehouse_list = getmstaddress_list;
            }
            dt_datatable.Dispose();
        }


        public void DaGetEditTrade2CreditorDtl(string application_gid, Mdlcreditor2warehouse values)
        {
            msSQL = "  select applicationtrade2creditor_gid, application2trade_gid,application2loan_gid,application_gid,creditor_gid,Applicant_name ,Applicant_category ," +
                    " designation_type,contactperson_name,contact_no,email_id,pan_no from agr_mst_tapplicationtrade2creditor where application_gid ='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstaddress_list = new List<trade2creditor_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstaddress_list.Add(new trade2creditor_list
                    {
                        applicationtrade2creditor_gid = (dr_datarow["applicationtrade2creditor_gid"].ToString()),
                        application2trade_gid = (dr_datarow["application2trade_gid"].ToString()),
                        application_gid = (dr_datarow["application_gid"].ToString()),
                        creditor_gid = (dr_datarow["creditor_gid"].ToString()),
                        Applicant_name = (dr_datarow["Applicant_name"].ToString()),
                        Applicant_category = (dr_datarow["Applicant_category"].ToString()),
                        designation_type = (dr_datarow["designation_type"].ToString()),
                        contactperson_name = (dr_datarow["contactperson_name"].ToString()),
                        contact_no = (dr_datarow["contact_no"].ToString()),
                        email_id = (dr_datarow["email_id"].ToString()),
                        pan_no = (dr_datarow["pan_no"].ToString()),

                    });
                }
                values.trade2creditor_list = getmstaddress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaGetEditTrade2WarehouseDetail(string application_gid, Mdlcreditor2warehouse values)
        {
            msSQL = "  select applicationtrade2warehouse_gid, application2trade_gid,application2loan_gid,application_gid,creditor_gid,warehouse_gid,warehouse_agency ,warehouse_name,typeofwarehouse_name ," +
                    " volume_uomgid,volume_uom,totalcapacity_volume,totalcapacity_area,totalcapacityarea_uomgid,area_uom,warehouse2address_gid,warehouse_address,capacity_commodity,capacity_panina from agr_mst_tapplicationtrade2warehouse where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstaddress_list = new List<creditor2warehouse_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstaddress_list.Add(new creditor2warehouse_list
                    {
                        applicationtrade2warehouse_gid = (dr_datarow["applicationtrade2warehouse_gid"].ToString()),
                        application2trade_gid = (dr_datarow["application2trade_gid"].ToString()),
                        application_gid = (dr_datarow["application_gid"].ToString()),
                        creditor_gid = (dr_datarow["creditor_gid"].ToString()),
                        warehouse_gid = (dr_datarow["warehouse_gid"].ToString()),
                        warehouse_agency = (dr_datarow["warehouse_agency"].ToString()),
                        warehouse_name = (dr_datarow["warehouse_name"].ToString()),
                        typeofwarehouse_name = (dr_datarow["typeofwarehouse_name"].ToString()),
                        volume_uom = (dr_datarow["volume_uom"].ToString()),
                        totalcapacity_volume = (dr_datarow["totalcapacity_volume"].ToString()),
                        totalcapacity_area = (dr_datarow["totalcapacity_area"].ToString()),
                        area_uom = (dr_datarow["area_uom"].ToString()),
                        warehouse2address_gid = (dr_datarow["warehouse2address_gid"].ToString()),
                        warehouse_address = (dr_datarow["warehouse_address"].ToString()),
                        capacity_commodity = (dr_datarow["capacity_commodity"].ToString()),
                        capacity_panina = (dr_datarow["capacity_panina"].ToString()),
                    });
                }
                values.creditor2warehouse_list = getmstaddress_list;
            }
            dt_datatable.Dispose();
        }


    }

}