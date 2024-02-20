using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.ecms.Models;
using OfficeOpenXml;
using System.Configuration;
using System.IO;
using OfficeOpenXml.Style;
using System.Drawing;
using ems.storage.Functions;



namespace ems.ecms.DataAccess
{
    /// <summary>
    /// customer Controller Class containing API methods for accessing the  DataAccess class DaCustomer 
    /// To get customer details, state name, ccmail details, customer details submit, UnTag NPA, Tag NPA, Tagged NPA Customer List,
    /// NPA Tagged History List, Export Customer details to excel, Common Customer detail 
    /// </summary>
    /// <remarks>Written by Sundar Rajan </remarks>
    public class DaCustomer
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader, objODBCDatareader1;
        DataTable dt_datatable;
        string msSQL, msGetGid, msGetGidREF;
        string deferral_gid = string.Empty;
        int mnResult;
        string lsallocationdtl_gid;
        string lsextend_date;
        string customerGID = string.Empty;
        string lsRelationshipManager;
        string lsUpdatedBy;
        string lsUpdatedDate;

        public void DaGetState(MdlCustomer objState)
        {
            try
            {
                msSQL = " SELECT state_gid,state_name FROM ocs_mst_tstate ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getState = new List<state_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getState.Add(new state_list
                        {
                            state_gid = (dr_datarow["state_gid"].ToString()),
                            state_name = (dr_datarow["state_name"].ToString()),
                        });
                    }
                    objState.state_list = getState;
                }
                dt_datatable.Dispose();

                objState.status = true;
            }
            catch
            {
                objState.status = false;
            }

        }

        public void DaGetccmail(MdlCustomer objState)
        {
            try
            {
                msSQL = " SELECT ccmail_text FROM ccmail ";
                objState.ccmail = objdbconn.GetExecuteScalar(msSQL);
                objState.status = true;
            }
            catch
            {
                objState.status = false;
            }

        }

        public void DaGetCustomer(MdlCustomer objCustomer)
        {
            try
            {
                msSQL = " select a.customer_gid,a.vertical_code,a.customer_code,a.customer_urn,a.customername,a.contactperson, " +
                  " case when a.zonal_head = '' then 'NA' else a.zonal_name end as zonal_head," +
                  " case when a.business_head = '' then 'NA' else a.businesshead_name end as business_head, " +
                  " case when a.cluster_manager_gid = '' then 'NA' else a.cluster_manager_name end as cluster_manager, " +
                  " case when a.relationship_manager = '' then 'NA' else a.relationshipmgmt_name end as relationship_manager " +
                  " from ocs_mst_tcustomer a order by a.customername  ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcustomer = new List<customer_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcustomer.Add(new customer_list
                        {
                            customer_gid = (dr_datarow["customer_gid"].ToString()),
                            customercode = (dr_datarow["customer_code"].ToString()),
                            customername = (dr_datarow["customername"].ToString()),
                            contactperson = (dr_datarow["contactperson"].ToString()),
                            vertical_code = (dr_datarow["vertical_code"].ToString()),
                            zonalGid = (dr_datarow["zonal_head"].ToString()),
                            businessHeadGid = (dr_datarow["business_head"].ToString()),
                            relationshipMgmtGid = (dr_datarow["relationship_manager"].ToString()),
                            clustermanagerGid = (dr_datarow["cluster_manager"].ToString()),
                            customer_urn = (dr_datarow["customer_urn"].ToString())
                        });
                    }
                    objCustomer.customer_list = getcustomer;
                }
                dt_datatable.Dispose();
                objCustomer.status = true;


            }
            catch
            {
                objCustomer.status = false;
            }


        }

        public void DaGetCustomerdetail(MdlCustomer objCustomer)
        {
            try
            {
                msSQL = " select a.customer_gid,a.vertical_code,a.customer_code,a.customer_urn,a.customername,a.contactperson, " +
                  " case when a.zonal_head = '' then 'NA' else a.zonal_name end as zonal_head," +
                  " case when a.business_head = '' then 'NA' else a.businesshead_name end as business_head, " +
                  " case when a.cluster_manager_gid = '' then 'NA' else a.cluster_manager_name end as cluster_manager, " +
                  " case when a.relationship_manager = '' then 'NA' else a.relationshipmgmt_name end as relationship_manager, " +
                  " case when a.creditmanager_gid = '' then 'NA' else a.creditmgmt_name end as creditmgmt_name, " +
                  " concat(c.user_firstname,' ',c.user_lastname,'/',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y') as created_date," +
                  " case when a.legaltag_flag is null then 'N' else a.legaltag_flag end as legaltag_flag, " +
                   " case when a.legaltag_flag is null then 'NA' when a.legaltag_flag='N' then 'UnTagged' else 'Tagged' end as legal_tagging, " +
                   " case when a.npatag_flag is null then 'N' else a.npatag_flag end as npatag_flag, " +
                   " case when a.npatag_flag is null then 'NA' when a.npatag_flag='N' then 'UnTagged' else 'Tagged' end as npa_tagging " +
                  " from ocs_mst_tcustomer a " +
                  " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                  " left join adm_mst_tuser c on b.user_gid = c.user_gid order by a.customer_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    objCustomer.customer_list = dt_datatable.AsEnumerable().Select(row =>
                    new customer_list
                    {
                        customer_gid = (row["customer_gid"].ToString()),
                        customercode = (row["customer_code"].ToString()),
                        customername = (row["customername"].ToString()),
                        contactperson = (row["contactperson"].ToString()),
                        vertical_code = (row["vertical_code"].ToString()),
                        zonalGid = (row["zonal_head"].ToString()),
                        legaltag_flag = (row["legaltag_flag"].ToString()),
                        legal_tagging = (row["legal_tagging"].ToString()),
                        npatag_flag = (row["npatag_flag"].ToString()),
                        npa_tagging = (row["npa_tagging"].ToString()),
                        businessHeadGid = (row["business_head"].ToString()),
                        relationshipMgmtGid = (row["relationship_manager"].ToString()),
                        clustermanagerGid = (row["cluster_manager"].ToString()),
                        customer_urn = (row["customer_urn"].ToString()),
                        creditmanagerName = (row["creditmgmt_name"].ToString()),
                        created_by = (row["created_by"].ToString()),
                        created_date = (row["created_date"].ToString())
                    }).ToList();


                }
                dt_datatable.Dispose();
                objCustomer.status = true;
            }
            catch
            {
                objCustomer.status = false;
            }



        }

        public void DaGetCustomerDetails(MdlCustomer objCustomer, string employee_gid)
        {
            try
            {
                msSQL = " select a.customer_gid,a.customer_urn,a.customer_code,concat(a.customer_code,'/',a.customername) as customername,a.contactperson, " +
                 " case when a.zonal_head = '' then 'NA' else a.zonal_name end as zonal_head," +
                 " case when a.business_head = '' then 'NA' else a.businesshead_name end as business_head, " +
                 " case when a.cluster_manager_gid = '' then 'NA' else a.cluster_manager_name end as cluster_manager, " +
                 " case when a.relationship_manager = '' then 'NA' else a.relationshipmgmt_name end as relationship_manager " +
                 " from ocs_mst_tcustomer a where a.created_by='" + employee_gid + "' order by a.customer_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcustomer = new List<customer_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcustomer.Add(new customer_list
                        {
                            customer_gid = (dr_datarow["customer_gid"].ToString()),
                            customercode = (dr_datarow["customer_code"].ToString()),
                            customername = (dr_datarow["customername"].ToString()),
                            customer_urn = (dr_datarow["customer_urn"].ToString()),
                            contactperson = (dr_datarow["contactperson"].ToString()),
                            zonalGid = (dr_datarow["zonal_head"].ToString()),
                            businessHeadGid = (dr_datarow["business_head"].ToString()),
                            relationshipMgmtGid = (dr_datarow["relationship_manager"].ToString()),
                            clustermanagerGid = (dr_datarow["cluster_manager"].ToString()),
                        });
                    }
                    objCustomer.customer_list = getcustomer;
                }
                dt_datatable.Dispose();
                objCustomer.status = true;
            }
            catch
            {
                objCustomer.status = false;
            }

        }


        public void DaPostCreateCustomer(mdlcreatecustomer values, string employee_gid)
        {

            msSQL = "select customer_gid from ocs_mst_tcustomer where customername='" + values.customername + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.status = false;
                values.message = "Customer Name Already Exists";
                return;
            }
            objODBCDatareader.Close();

            if (values.customer_urn != null)
            {
                msSQL = "select customer_gid from ocs_mst_tcustomer where customer_urn='" + values.customer_urn + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.status = false;
                    values.message = "Customer URN Already Exists";
                    return;
                }
                objODBCDatareader.Close();
            }


            msGetGid = objcmnfunctions.GetMasterGID("CRMS");
            msGetGidREF = objcmnfunctions.GetMasterGID("CC");
            msSQL = " insert into ocs_mst_tcustomer(" +
                    " customer_gid," +
                    " customer_code," +
                    " customername," +
                    " contactperson," +
                    " customer_urn," +
                    " gst_number," +
                    " pan_number," +
                    " mobileno," +
                    " contact_no," +
                    " email," +
                    " address," +
                    " address2," +
                    " region," +
                    " vertical_gid," +
                    " vertical_code," +
                    " state," +
                    " state_gid," +
                    " postalcode," +
                    " country," +
                    " tomail_text," +
                    " ccmail_text," +
                    " zonal_head," +
                    " business_head," +
                    " regional_head," +
                    " relationship_manager," +
                    " cluster_manager_gid," +
                    " creditmanager_gid," +
                    " zonal_name," +
                    " businesshead_name," +
                    " regionalhead_name," +
                    " relationshipmgmt_name," +
                    " creditmgmt_name," +
                    " cluster_manager_name," +
                    " constitution_name," +
                    " constitution_gid," +
                    " major_corporate," +
                    " zonal_riskmanager," +
                    " zonal_riskmanagerName," +
                    " assigned_RM," +
                    " assigned_RMName," +
                    " riskMonitoring_GID," +
                    " riskMonitoring_Name," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + msGetGidREF + "'," +
                    "'" + values.customername.Replace("'", "") + "',";
            if (values.contactperson == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.contactperson.Replace("'", "") + "',";
            }

            msSQL += "'" + values.customer_urn + "',";
            if (values.gst_number == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.gst_number.Replace("'", "") + "',";
            }
            if (values.pan_number == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.pan_number.Replace("'", "") + "',";
            }
            msSQL += "'" + values.mobileno + "'," +
                "'" + values.contactnumber + "'," +
            "'" + values.email + "',";
            if (values.address1 == null)
            {
                msSQL += "'',";

            }
            else
            {
                msSQL += "'" + values.address1.Replace("'", "") + "',";
            }
            if (values.address2 == null)
            {
                msSQL += "'',";

            }
            else
            {
                msSQL += "'" + values.address2.Replace("'", "") + "',";
            }

            msSQL += "'" + values.region + "'," +
                     "'" + values.vertical_gid + "'," +
                     "'" + values.vertical_code.Replace("'", "").Replace("\n", "") + "'," +
                     "'" + values.state.Replace("    ", "").Replace("\n", "") + "'," +
                     "'" + values.state_gid + "'," +
                     "'" + values.postalcode + "'," +
                     "'" + values.country + "'," +
                     "'" + values.tomail + "'," +
                     "'" + values.ccmail + "'," +
                     "'" + values.zonalGid + "'," +
                     "'" + values.businessHeadGid + "'," +
                     "'" + values.regionalHeadGid + "'," +
                     "'" + values.relationshipMgmtGid + "'," +
                     "'" + values.clustermanagerGid + "'," +
                     "'" + values.creditmanagerGid + "'," +
                     "'" + values.zonal_name.Replace("    ", "").Replace("\n", "") + "'," +
                     "'" + values.businesshead_name.Replace("    ", "").Replace("\n", "") + "'," +
                     "'" + values.regionalhead_name.Replace("    ", "").Replace("\n", "") + "'," +
                     "'" + values.relationshipmgmt_name.Replace("    ", "").Replace("\n", "") + "'," +
                     "'" + values.creditmanager_name.Replace("    ", "").Replace("\n", "") + "'," +
                     "'" + values.cluster_manager_name.Replace("    ", "").Replace("\n", "") + "'," +
                     "'" + values.constitution_name.Replace("   ", "").Replace("\n", "") + "'," +
                     "'" + values.constitution_gid + "',";
            if (values.major_corporate == null)
            {
                msSQL += "'',";

            }
            else
            {
                msSQL += "'" + values.major_corporate.Replace("'", "") + "',";
            }
            msSQL += "'" + values.zonal_riskmanagerGID + "'," +
                     "'" + values.zonal_riskmanagerName.Replace("    ", "").Replace("\n", "") + "'," +
                     "'" + values.risk_managerGID + "'," +
                     "'" + values.riskmanager_name.Replace("    ", "").Replace("\n", "") + "'," +
                     "'" + values.riskMonitoring_GID + "'," +
                     "'" + values.riskMonitoring_Name.Replace("    ", "").Replace("\n", "") + "',";
            msSQL += "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select zonalmapping_gid,zonal_RM from rsk_mst_trmmapping where state_gid='" + values.state_gid + "' group by state_gid";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                msSQL = " update ocs_mst_tcustomer set zonal_gid='" + objODBCDatareader["zonalmapping_gid"].ToString() + "'" +
                        " where customer_gid='" + msGetGid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            objODBCDatareader.Close();

            if (mnResult != 0)
            {
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Creating the Customer";
                values.status = false;

            }
        }
        public void DaGetEditCustomer(string customer_gid, customeredit values)
        {
            try
            {
                msSQL = " select customer_code,customername,contactperson,mobileno,gst_number,pan_number," +
        " contact_no,email,address,address2,region,vertical_gid,vertical_code,ccmail_text," +
        " country,state_gid,postalcode,business_head,regional_head,zonal_head,zonal_name,businesshead_name,regionalhead_name," +
        " cluster_manager_gid,cluster_manager_name,relationshipmgmt_name,relationship_manager,creditmanager_gid,creditmgmt_name,customer_urn," +
        " major_corporate,constitution_name,constitution_gid,zonal_riskmanager,zonal_riskmanagerName,assigned_RM,assigned_RMName, " +
        " riskMonitoring_GID,riskMonitoring_Name from ocs_mst_tcustomer where customer_gid='" + customer_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.customerCodeedit = objODBCDatareader["customer_code"].ToString();
                    values.customerNameedit = objODBCDatareader["customername"].ToString();
                    values.contactPersonedit = objODBCDatareader["contactperson"].ToString();
                    if (objODBCDatareader["mobileno"].ToString() != "")
                    {
                        values.mobileNo_edit = Convert.ToDouble(objODBCDatareader["mobileno"].ToString());
                    }
                    values.mobileNoedit = objODBCDatareader["mobileno"].ToString();
                    if (objODBCDatareader["contact_no"].ToString() != "")
                    {
                        values.contactno_edit = Convert.ToDouble(objODBCDatareader["contact_no"].ToString());
                    }
                    values.contactnoedit = objODBCDatareader["contact_no"].ToString();
                    values.emailedit = objODBCDatareader["email"].ToString();
                    values.addressline1edit = objODBCDatareader["address"].ToString();
                    values.addressline2edit = objODBCDatareader["address2"].ToString();
                    values.regionedit = objODBCDatareader["region"].ToString();
                    values.vertical_gid = objODBCDatareader["vertical_gid"].ToString();
                    values.vertical_code = objODBCDatareader["vertical_code"].ToString();
                    values.ccmailedit = objODBCDatareader["ccmail_text"].ToString();
                    values.countryedit = objODBCDatareader["country"].ToString();
                    values.state_gid = objODBCDatareader["state_gid"].ToString();
                    if (objODBCDatareader["postalcode"].ToString() != "")
                    {
                        values.postalcode_edit = Convert.ToDouble(objODBCDatareader["postalcode"].ToString());
                    }
                    values.postalcodeedit = objODBCDatareader["postalcode"].ToString();
                    values.businessHeadGid = objODBCDatareader["business_head"].ToString();
                    values.regionalHeadGid = objODBCDatareader["regional_head"].ToString();
                    values.zonalGid = objODBCDatareader["zonal_head"].ToString();
                    values.employee_gid = objODBCDatareader["zonal_head"].ToString();
                    values.employee_name = objODBCDatareader["zonal_name"].ToString();
                    values.zonal_name = objODBCDatareader["zonal_name"].ToString();
                    values.businesshead_name = objODBCDatareader["businesshead_name"].ToString();
                    values.regionalhead_name = objODBCDatareader["regionalhead_name"].ToString();
                    values.clustermanagerGid = objODBCDatareader["cluster_manager_gid"].ToString();
                    values.cluster_manager_name = objODBCDatareader["cluster_manager_name"].ToString();
                    values.relationshipmgmt_name = objODBCDatareader["relationshipmgmt_name"].ToString();
                    values.relationshipMgmtGid = objODBCDatareader["relationship_manager"].ToString();
                    values.creditmanagerGid = objODBCDatareader["creditmanager_gid"].ToString();
                    values.creditmanager_name = objODBCDatareader["creditmgmt_name"].ToString();
                    values.customer_urnedit = objODBCDatareader["customer_urn"].ToString();
                    values.gst_number = objODBCDatareader["gst_number"].ToString();
                    values.pan_number = objODBCDatareader["pan_number"].ToString();
                    values.customer_gid = customer_gid;
                    values.constitution_nameedit = objODBCDatareader["constitution_name"].ToString();
                    values.constitution_gidedit = objODBCDatareader["constitution_gid"].ToString();
                    values.major_corporateedit = objODBCDatareader["major_corporate"].ToString();
                    values.zonal_riskmanagerGID = objODBCDatareader["zonal_riskmanager"].ToString();
                    values.zonal_riskmanagerName = objODBCDatareader["zonal_riskmanagerName"].ToString();
                    values.risk_managerGID = objODBCDatareader["assigned_RM"].ToString();
                    values.risk_managerName = objODBCDatareader["assigned_RMName"].ToString();
                    values.riskMonitoring_GID = objODBCDatareader["riskMonitoring_GID"].ToString();
                    values.riskMonitoring_Name = objODBCDatareader["riskMonitoring_Name"].ToString();
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

        public void DaPostUpdateCustomer(string employee_gid, customeredit values)
        {
            try
            {
                msSQL = " select customer_gid from ocs_mst_tcustomer" +
                 " where customername='" + values.customerNameedit + "'";
                customerGID = objdbconn.GetExecuteScalar(msSQL);
                if (customerGID != "")
                {
                    if (customerGID != values.customer_gid)
                    {
                        values.message = "Customer Name Already Exists";
                        values.status = false;
                        return;
                    }

                }
                if (values.customer_urnedit != null && values.customer_urnedit != "")
                {
                    msSQL = " select customer_gid from ocs_mst_tcustomer" +
                           " where customer_urn='" + values.customer_urnedit + "'";
                    customerGID = objdbconn.GetExecuteScalar(msSQL);
                    if (customerGID != "")
                    {
                        if (customerGID != values.customer_gid)
                        {
                            values.message = "Customer URN Already Exists";
                            values.status = false;
                            return;
                        }

                    }
                }
                msSQL = "select relationship_manager from ocs_mst_tcustomer where customer_gid = '" + values.customer_gid + "'";
                string lsrelationship_manager = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select updated_by, date_format(updated_date, '%Y-%m-%d') as updated_date, customername, customer_urn from ocs_mst_tcustomer where customer_gid = '" + values.customer_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                    lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                    if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                    {
                        msGetGid = objcmnfunctions.GetMasterGID("CUPL");
                        msSQL = " insert into ocs_trn_tcustomerupdatedlog(" +
                                  " customerupdatedlog_gid," +
                                  " customer_gid," +
                                  " customer_name, " +
                                  " customer_urn, " +
                                  " lastupdated_by, " +
                                  " lastupdated_date, " +
                                  " created_by, " +
                                  " created_date) " +
                                  " values(" +
                                  "'" + msGetGid + "'," +
                                  "'" + values.customer_gid + "'," +
                                  "'" + objODBCDatareader["customername"].ToString() + "'," +
                                  "'" + objODBCDatareader["customer_urn"].ToString() + "'," +
                                  "'" + objODBCDatareader["updated_by"].ToString() + "'," +
                                  "'" + objODBCDatareader["updated_date"].ToString() + "'," +
                                  "'" + employee_gid + "'," +
                                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                objODBCDatareader.Close();


                msSQL = " update ocs_mst_tcustomer set " +
                        " customer_code='" + values.customerCodeedit + "'," +
                        " customername='" + values.customerNameedit.Replace("'", "") + "'," +
                        " contactperson='" + values.contactPersonedit + "'," +
                        " customer_urn='" + values.customer_urnedit + "'," +
                        " gst_number='" + values.gst_number + "'," +
                        " pan_number='" + values.pan_number + "'," +
                        " mobileno='" + values.mobileNoedit + "'," +
                        " contact_no='" + values.contactnoedit + "'," +
                        " vertical_code='" + values.vertical_code.Replace("    ", "").Replace("\n", "") + "'," +
                        " vertical_gid='" + values.vertical_gid + "'," +
                        " email='" + values.emailedit + "'," +
                        " address='" + values.addressline1edit + "'," +
                        " address2='" + values.addressline2edit + "'," +
                        " region='" + values.regionedit + "'," +
                        " tomail_text='" + values.tomailedit + "'," +
                        " ccmail_text='" + values.ccmailedit + "'," +
                        " country='" + values.countryedit + "'," +
                        " state_gid='" + values.state_gid + "'," +
                        " state='" + values.state.Replace("    ", "").Replace("\n", "") + "'," +
                        " postalcode='" + values.postalcodeedit + "'," +
                        " zonal_head='" + values.zonalGid + "'," +
                        " business_head='" + values.businessHeadGid + "'," +
                        " regional_head='" + values.regionalHeadGid + "'," +
                        " relationship_manager='" + values.relationshipMgmtGid + "'," +
                        "creditmanager_gid='" + values.creditmanagerGid + "'," +
                        " cluster_manager_gid='" + values.clustermanagerGid + "'," +
                        " zonal_name='" + values.zonal_name + "'," +
                        " businesshead_name='" + values.businesshead_name + "'," +
                        " regionalhead_name='" + values.regionalhead_name + "'," +
                        " relationshipmgmt_name='" + values.relationshipmgmt_name + "'," +
                        " creditmgmt_name='" + values.creditmanager_name + "'," +
                        " cluster_manager_name='" + values.cluster_manager_name + "'," +
                        " constitution_name='" + values.constitution_nameedit + "'," +
                         " constitution_gid='" + values.constitution_gidedit + "'," +
                        " major_corporate='" + values.major_corporateedit.Replace("'", "") + "'," +
                        " zonal_riskmanager='" + values.zonal_riskmanagerGID + "'," +
                        " zonal_riskmanagerName='" + values.zonal_riskmanagerName + "'," +
                        " assigned_RM='" + values.risk_managerGID + "'," +
                        " assigned_RMName='" + values.risk_managerName + "'," +
                        " riskMonitoring_GID='" + values.riskMonitoring_GID + "'," +
                        " riskMonitoring_Name='" + values.riskMonitoring_Name + "'," +
                        " updated_by='" + employee_gid + "'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where customer_gid='" + values.customer_gid + "' ";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_trn_tloan set " +
                        " customer_name='" + values.customerNameedit + "'," +
                           " zonal_gid='" + values.zonalGid + "'," +
                         " businesshead_gid='" + values.businessHeadGid + "'," +
                         " relationshipmgmt_gid='" + values.relationshipMgmtGid + "'," +
                         " cluster_manager_gid='" + values.clustermanagerGid + "'," +
                         " creditmanager_gid='" + values.creditmanagerGid + "'," +
                         " zonal_name='" + values.zonal_name + "'," +
                         " businesshead_name='" + values.businesshead_name + "'," +
                         " relationshipmgmt_name='" + values.relationshipmgmt_name + "'," +
                         " cluster_manager_name='" + values.cluster_manager_name + "'," +
                         " creditmgmt_name='" + values.creditmanager_name + "'" +
                        " where customer_gid='" + values.customer_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_trn_tdeferral set " +
                         " customer_name='" + values.customerNameedit + "'," +
                         " vertical_code='" + values.vertical_code.Replace("    ", "").Replace("\n", "") + "'," +
                         " vertical_gid='" + values.vertical_gid + "'," +
                         " zonal_gid='" + values.zonalGid + "'," +
                         " businesshead_gid='" + values.businessHeadGid + "'," +
                         " relationshipmgmt_gid='" + values.relationshipMgmtGid + "'," +
                         " cluster_manager_gid='" + values.clustermanagerGid + "'," +
                         " creditmanager_gid='" + values.creditmanagerGid + "'," +
                         " regionalhead_gid='" + values.regionalHeadGid + "'," +
                         " zonal_name='" + values.zonal_name + "'," +
                         " businesshead_name='" + values.businesshead_name + "'," +
                         " relationshipmgmt_name='" + values.relationshipmgmt_name + "'," +
                         " cluster_manager_name='" + values.cluster_manager_name + "'," +
                         " creditmgmt_name='" + values.creditmanager_name + "'," +
                         " regionalhead_name='" + values.regionalhead_name + "'" +
                         " where customer_gid='" + values.customer_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_trn_tdeferral2loan set " +
                        " vertical_code='" + values.vertical_code.Replace("    ", "").Replace("\n", "") + "'," +
                        " vertical_gid='" + values.vertical_gid + "'" +
                        " where customer_gid='" + values.customer_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " select concat(a.user_firstname,' ',a.user_lastname,'/',a.user_code) as RelationshipManager " +
                        " from adm_mst_tuser a " +
                        " left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                        " where b.employee_gid = '" + values.relationshipMgmtGid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsRelationshipManager = objODBCDatareader["RelationshipManager"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select a.allocationdtl_gid from rsk_trn_tallocationdtl a " +
                       " left join rsk_trn_tvisitreportgenerate b on a.allocationdtl_gid = b.allocationdtl_gid " +
                       " left join rsk_trn_tobservationreport c on b.allocationdtl_gid = c.allocationdtl_gid " +
                       " where((a.allocation_status = 'Allocated') or " +
                       " (a.allocation_status = 'Completed' and b.report_status = 'Completed' and " +
                       " (observation_flag is null or observation_flag = 'N'))) and a.customer_gid = '" + values.customer_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsallocationdtl_gid = objODBCDatareader["allocationdtl_gid"].ToString();

                    msSQL = " update rsk_trn_tallocatecustomerdtl set relationship_managergid='" + values.relationshipMgmtGid + "' , " +
                            " relationship_managername='" + lsRelationshipManager + "'" +
                            " where allocationdtl_gid = '" + lsallocationdtl_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update rsk_trn_tvisitreportgenerate set RM_name='" + lsRelationshipManager + "' " +
                           " where allocationdtl_gid = '" + lsallocationdtl_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update rsk_trn_tobservationreport set relationship_manager_name='" + lsRelationshipManager + "', " +
                           " relationship_manager_gid='" + values.relationshipMgmtGid + "' " +
                           " where allocationdtl_gid = '" + lsallocationdtl_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                objODBCDatareader.Close();

                msSQL = " select a.allocationdtl_gid from rsk_trn_tallocationdtl a " +
                        " where a.customer_gid = '" + values.customer_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        msSQL = " update rsk_trn_tobservationreport set " +
                                " vertical='" + values.vertical_code.Replace("    ", "").Replace("\n", "") + "'" +
                                " where allocationdtl_gid = '" + dr_datarow["allocationdtl_gid"].ToString() + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();

                msSQL = " update rsk_trn_tallocatecustomerdtl set  " +
                        " vertical_code='" + values.vertical_code.Replace("    ", "").Replace("\n", "") + "'," +
                        " vertical_gid='" + values.vertical_gid + "'" +
                        " where customer_gid = '" + values.customer_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL); 
                values.status = true;
            }
            catch (Exception ex)
            {

                values.status = false;
                values.message = "Error Occured..";
            }



        }

        public void DaPostDeleteCustomer(string customer_gid, customeredit values)
        {

            msSQL = " delete from ocs_mst_tcustomer where customer_gid='" + customer_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "success";
            }
            else
            {
                values.status = false;
                values.message = "failure";
            }
        }


        public void DaGetCustomerforAlert(MdlCustomer objCustomer)
        {
            try
            {
                msSQL = " select distinct a.customer_gid,a.vertical_code,a.customer_urn,a.customer_code,a.customername,a.contactperson, " +
                   " (select count(p.customer_gid) from ocs_trn_tcustomermail p where p.customer_gid=a.customer_gid group by p.customer_gid) as mail_count," +
                   " case when a.creditmanager_gid='' then 'NA' else a.creditmgmt_name end as creditmgmt_name," +
                   " case when a.zonal_head = '' then 'NA' else a.zonal_name end as zonal_head," +
                   " case when a.business_head = '' then 'NA' else a.businesshead_name end as business_head, " +
                   " case when a.cluster_manager_gid = '' then 'NA' else a.cluster_manager_name end as cluster_manager, " +
                   " case when a.relationship_manager = '' then 'NA' else a.relationshipmgmt_name end as relationship_manager " +
                   " from ocs_mst_tcustomer a where a.customer_gid in ( " +
                   " select x.customer_gid from ocs_trn_tdeferral x  " +
                   " left join ocs_trn_tdeferralapproval y on y.deferral_gid=x.deferral_gid " +
                   " where a.customer_gid = x.customer_gid and (x.deferral_status='Expired' or x.deferral_status='Live') and x.entity_name='SAMFIN' " +
                   " and (x.deferral_catagory='Sanction Item' or x.deferral_catagory='Credit Deferral') " +
                   " AND CASE WHEN date_format(x.created_date,'%Y-%m')>='2020-03' THEN x.aging>=60 ELSE x.aging>=90 END " +
                   " and (y.approval_status='Extend' or y.approval_status='Pending' or y.approval_status='ReOpen') " +
                   " and x.covenanttype_name<>'Unit Visit Report')  " +
                   " order by a.customername  ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcustomer = new List<customer_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcustomer.Add(new customer_list
                        {
                            customer_gid = (dr_datarow["customer_gid"].ToString()),
                            customercode = (dr_datarow["customer_code"].ToString()),
                            customername = (dr_datarow["customername"].ToString()),
                            contactperson = (dr_datarow["contactperson"].ToString()),
                            vertical_code = (dr_datarow["vertical_code"].ToString()),
                            zonalGid = (dr_datarow["zonal_head"].ToString()),
                            businessHeadGid = (dr_datarow["business_head"].ToString()),
                            relationshipMgmtGid = (dr_datarow["relationship_manager"].ToString()),
                            clustermanagerGid = (dr_datarow["cluster_manager"].ToString()),
                            creditmanagerName = (dr_datarow["creditmgmt_name"].ToString()),
                            mail_count = (dr_datarow["mail_count"].ToString()),
                        });


                        objCustomer.customer_list = getcustomer;

                    }


                }
                dt_datatable.Dispose();
                objCustomer.status = true;

            }
            catch
            {
                objCustomer.status = false;
            }


        }
        public void DaGetCustomerforalertSearch(MdlCustomer objCustomer, string customer_gid)
        {
            try
            {

                msSQL = " select distinct a.customer_gid,a.vertical_code,a.customer_code,a.customername,a.contactperson, " +
                        " (select count(p.customer_gid) from ocs_trn_tcustomermail p where p.customer_gid=a.customer_gid group by p.customer_gid) as mail_count," +
                        " case when a.creditmanager_gid='' then 'NA' else a.creditmgmt_name end as creditmgmt_name," +
                        " case when a.zonal_head = '' then 'NA' else a.zonal_name end as zonal_head," +
                        " case when a.business_head = '' then 'NA' else a.businesshead_name end as business_head, " +
                        " case when a.cluster_manager_gid = '' then 'NA' else a.cluster_manager_name end as cluster_manager, " +
                        " case when a.relationship_manager = '' then 'NA' else a.relationshipmgmt_name end as relationship_manager " +
                        " from ocs_mst_tcustomer a" +
                        " where a.customer_gid in ( " +
                        " select x.customer_gid from ocs_trn_tdeferral x  " +
                        " left join ocs_trn_tdeferralapproval y on y.deferral_gid=x.deferral_gid " +
                        " where a.customer_gid = x.customer_gid and (x.deferral_status='Expired' or x.deferral_status='Live') and x.entity_name='SAMFIN' " +
                         " and (x.deferral_catagory='Sanction Item' or x.deferral_catagory='Credit Deferral') " +
                             " and (y.approval_status='Extend' or y.approval_status='Pending' or y.approval_status='ReOpen') " +
                             " AND CASE WHEN date_format(x.created_date, '%Y-%m') >= '2020-03' THEN x.aging >= 60 ELSE x.aging >= 90 END " +
                             " and x.covenanttype_name<>'Unit Visit Report') and customer_gid='" + customer_gid + "' " +
                        " order by a.customer_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcustomer = new List<customer_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcustomer.Add(new customer_list
                        {
                            customer_gid = (dr_datarow["customer_gid"].ToString()),
                            customercode = (dr_datarow["customer_code"].ToString()),
                            customername = (dr_datarow["customername"].ToString()),
                            contactperson = (dr_datarow["contactperson"].ToString()),
                            vertical_code = (dr_datarow["vertical_code"].ToString()),
                            zonalGid = (dr_datarow["zonal_head"].ToString()),
                            businessHeadGid = (dr_datarow["business_head"].ToString()),
                            relationshipMgmtGid = (dr_datarow["relationship_manager"].ToString()),
                            clustermanagerGid = (dr_datarow["cluster_manager"].ToString()),
                            creditmanagerName = (dr_datarow["creditmgmt_name"].ToString()),
                            mail_count = (dr_datarow["mail_count"].ToString()),
                        });


                        objCustomer.customer_list = getcustomer;

                    }
                    dt_datatable.Dispose();
                    objCustomer.status = true;

                }
                else
                {
                    dt_datatable.Dispose();
                    objCustomer.status = false;

                }



            }
            catch
            {
                objCustomer.status = true;
            }

        }

        public void DaDeferralDetails(string customer_gid, MdlCustomer values)
        {
            try
            {
                msSQL = " SELECT a.deferral_gid,a.entity_name as 'Entity',a.customer_name as 'Customer',if(a.customeralertmail_status is null,'Pending',a.customeralertmail_status) as mail_status, " +
                        " a.vertical_code as 'Vertical',a.loanref_no as loan_title,a.sanction_refno as 'Sanction Refno',a.sanction_date as 'Sanction Date', " +
                        " a.record_id, a.tracking_type," +
                        " a.deferral_catagory,a.customer_remarks," +
                        " case when a.tracking_type='Deferral' then a.deferral_name else a.covenanttype_name end as deferral_name,a.criticallity as 'Criticallity',  " +
                        " date_format(a.due_date, '%d-%m-%Y') as due_date," +
                        " case when m.approval_status='ReOpen' then 'Pending' else  m.approval_status end as approval_status, " +
                        " case when m.approval_status='Closed' then 'Closed' else a.deferral_status end as deferral_status," +
                          " case when m.approval_status='Closed' then '-' else a.aging end as aging," +
                        " case when b.customer_remarks<>'' then b.customer_remarks " +
                        "  when m.approval_remarks <> '' then m.approval_remarks else a.remarks end as remarks " +
                        " from ocs_trn_tdeferral2loan b " +
                        " left join ocs_trn_tdeferral a on a.deferral_gid = b.deferral_gid " +
                        " left join ocs_trn_tdeferralapproval m on m.deferral_gid = a.deferral_gid" +
                        " where  " +
                        " (a.deferral_catagory='Sanction Item' or a.deferral_catagory='Credit Deferral') " +
                        " and (m.approval_status='Extend' or m.approval_status='Pending' or m.approval_status='ReOpen') " +
                        " and a.covenanttype_name<>'Unit Visit Report' and (a.deferral_status='Expired' or a.deferral_status='Live') and a.entity_name='SAMFIN' " +
                        " AND CASE WHEN date_format(a.created_date,'%Y-%m')>='2020-03' THEN a.aging>=60 ELSE a.aging>=90 END " +
                        " and a.customer_gid in ('" + customer_gid + "')" +
                        " order by a.created_date desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var uatlog = new List<customerdeferral_list>();
                foreach (DataRow dtdatarow in dt_datatable.Rows)
                {
                    msSQL = " select date_format(extended_date,'%d-%m-%Y') as extended_date from ocs_trn_tdeferralapproval " +
                               " where deferral_gid='" + dtdatarow["deferral_gid"].ToString() + "' and approval_status='Extend'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows)
                    {
                        lsextend_date = objODBCDatareader["extended_date"].ToString();
                        objODBCDatareader.Close();
                    }
                    else
                    {
                        lsextend_date = "-";
                        objODBCDatareader.Close();
                    }

                    uatlog.Add(new customerdeferral_list
                    {
                        deferral_gid = (dtdatarow["deferral_gid"].ToString()),
                        record_id = (dtdatarow["record_id"].ToString()),
                        tracking_type = (dtdatarow["tracking_type"].ToString()),
                        deferral_name = (dtdatarow["deferral_name"].ToString()),
                        deferral_category = (dtdatarow["deferral_catagory"].ToString()),
                        due_date = (dtdatarow["due_date"].ToString()),
                        customer_remarks = (dtdatarow["remarks"].ToString()),
                        remarks = (dtdatarow["remarks"].ToString()),
                        aging = (dtdatarow["aging"].ToString()),
                        loanTitle = (dtdatarow["loan_title"].ToString()),
                        deferral_status = (dtdatarow["deferral_status"].ToString()),
                        status = (dtdatarow["approval_status"].ToString()),
                        mail_status = (dtdatarow["mail_status"].ToString()),
                        extended_date = lsextend_date,
                        sanction_refno = (dtdatarow["Sanction Refno"].ToString()),
                        sanction_date = (dtdatarow["Sanction Date"].ToString()),
                    });
                }
                values.customerdeferral_list = uatlog;
                dt_datatable.Dispose();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }

        }

        public void DaGetCustomerDetails(string customer_gid, customeredit values)
        {
            try
            {
                msSQL = " select a.customer_gid,a.vertical_code,a.customer_urn,a.customer_code,a.customername, " +
                 " case when a.address<>'' then a.address else '-' end as address,address2, " +
                 " case when a.mobileno<>'' then a.mobileno else '-' end as mobileno, " +
                 " case when a.contactperson<>'' then a.contactperson else '-' end as contactperson, " +
                 " case when a.creditmanager_gid='' then 'NA' else a.creditmgmt_name end as creditmgmt_name," +
                 " case when a.zonal_head = '' then 'NA' else a.zonal_name end as zonal_head," +
                 " case when a.business_head = '' then 'NA' else a.businesshead_name end as business_head, " +
                 " case when a.cluster_manager_gid = '' then 'NA' else a.cluster_manager_name end as cluster_manager, " +
                 " case when a.relationship_manager = '' then 'NA' else a.relationshipmgmt_name end as relationship_manager,a.customer_urn " +
                 " from ocs_mst_tcustomer a where customer_gid='" + customer_gid + "' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.customerCodeedit = objODBCDatareader["customer_code"].ToString();
                    values.customerNameedit = objODBCDatareader["customername"].ToString();
                    values.vertical_code = objODBCDatareader["vertical_code"].ToString();
                    values.businesshead_name = objODBCDatareader["business_head"].ToString();
                    values.zonal_name = objODBCDatareader["zonal_head"].ToString();
                    values.cluster_manager_name = objODBCDatareader["cluster_manager"].ToString();
                    values.relationshipmgmt_name = objODBCDatareader["relationship_manager"].ToString();
                    values.creditmanager_name = objODBCDatareader["creditmgmt_name"].ToString();
                    values.customer_urnedit = objODBCDatareader["customer_urn"].ToString();
                    values.contactPersonedit = objODBCDatareader["contactperson"].ToString();
                    values.addressline1edit = objODBCDatareader["address"].ToString();
                    values.addressline2edit = objODBCDatareader["address2"].ToString();
                    values.mobileNoedit = objODBCDatareader["mobileno"].ToString();
                    values.customer_urnedit = objODBCDatareader["customer_urn"].ToString();
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

        public void DaGetconstitution(MdlCustomer objconstitution)
        {
            try
            {
                msSQL = " SELECT constitution_gid,constitution_name FROM ocs_mst_tconstitution ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getconstitution_list = new List<constitution_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getconstitution_list.Add(new constitution_list
                        {
                            constitution_gid = (dr_datarow["constitution_gid"].ToString()),
                            constitution_name = (dr_datarow["constitution_name"].ToString()),
                        });
                    }
                    objconstitution.constitution_list = getconstitution_list;
                }
                dt_datatable.Dispose();

                objconstitution.status = true;
            }
            catch
            {
                objconstitution.status = false;
            }

        }


        public bool DaGetNewCustomerURN(customerurndetails values, string employee_gid)
        {
            if (values.newcustomer_urn != null && values.newcustomer_urn != "")
            {
                msSQL = " select customer_gid from ocs_mst_tcustomer" +
                       " where customer_urn='" + values.newcustomer_urn + "'";
                customerGID = objdbconn.GetExecuteScalar(msSQL);
                if (customerGID != "")
                {
                    if (customerGID != values.customer_gid)
                    {
                        values.message = "Customer URN Already Exists";
                        values.status = false;
                        return false;
                    }

                }
            }

            msGetGid = objcmnfunctions.GetMasterGID("HICU");

            msSQL = " insert into ocs_trn_thistorycustomerurn(" +
                    " history_urngid," +
                    " customer_gid," +
                    " oldcustomer_urn," +
                    " newcustomer_urn," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.customer_gid + "'," +
                    "'" + values.currentcustomer_urn + "'," +
                    "'" + values.newcustomer_urn + "'," +
                    "'" + employee_gid + "'," +
                    "current_timestamp)";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                msSQL = " update ocs_mst_tcustomer set " +
                       " customer_urn='" + values.newcustomer_urn + "'" +
                       " where customer_gid='" + values.customer_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " select bankalert2notallocated_gid,email_to,email_from,from_mailaddress,date_format(email_date,'%Y-%m-%d %h:%i:%s') as email_date,cc,bcc," +
                   " email_subject,email_content,mailheader,document_name,document_path from osd_trn_tbankalert2notallocated where " +
                   " customer_urn='" + values.newcustomer_urn + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        msSQL = "select customername,relationship_manager,relationshipmgmt_name from ocs_mst_tcustomer where customer_gid = '" + values.customer_gid + "' ";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            if ((objODBCDatareader["relationship_manager"].ToString() == "") || (objODBCDatareader["relationship_manager"].ToString() == null))
                            {
                                msSQL = "update osd_trn_tbankalert2allocated set reason='RM Empty' '" + dr_datarow["bankalert2notallocated_gid"].ToString() + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                            else
                            {
                                string msGETGIDRef = objcmnfunctions.GetMasterGID("BREF");
                                string MSGETGID = objcmnfunctions.GetMasterGID("ALDB");
                                msSQL = "insert into osd_trn_tbankalert2allocated (" +
                                          " bankalert2allocated_gid," +
                                          " customer_name," +
                                          " customer_urn," +
                                          " customer_gid," +
                                          " relationshipmanager_name," +
                                          " relationshipmanager_gid," +
                                          " ticketref_no," +
                                          " email_to," +
                                          " email_from," +
                                          " from_mailaddress," +
                                          " email_date," +
                                          " cc," +
                                          " bcc," +
                                          " email_subject," +
                                          " email_content," +
                                          " mailheader," +
                                          " document_name," +
                                          " document_path," +
                                          " created_by," +
                                          " created_date) values(" +
                                          " '" + MSGETGID + "'," +
                                          " '" + objODBCDatareader["customername"].ToString() + "'," +
                                          " '" + values.newcustomer_urn + "'," +
                                          " '" + values.customer_gid + "'," +
                                          " '" + objODBCDatareader["relationshipmgmt_name"].ToString() + "'," +
                                          " '" + objODBCDatareader["relationship_manager"].ToString() + "'," +
                                          " '" + msGETGIDRef + "'," +
                                          " '" + dr_datarow["email_to"].ToString() + "'," +
                                          " '" + dr_datarow["email_from"].ToString() + "'," +
                                          " '" + dr_datarow["from_mailaddress"].ToString() + "'," +
                                          " '" + dr_datarow["email_date"].ToString() + "'," +
                                          " '" + dr_datarow["cc"].ToString() + "'," +
                                          " '" + dr_datarow["bcc"].ToString() + "'," +
                                          " '" + dr_datarow["email_subject"].ToString() + "'," +
                                          " '" + dr_datarow["email_content"].ToString() + "'," +
                                          " '" + dr_datarow["mailheader"].ToString() + "'," +
                                          " '" + dr_datarow["document_name"].ToString() + "'," +
                                          " '" + dr_datarow["document_path"].ToString() + "'," +
                                          " 'Auto Allocated'," +
                                          " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                if (mnResult == 1)
                                {
                                    msSQL = "delete from osd_trn_tbankalert2notallocated where bankalert2notallocated_gid = '" + dr_datarow["bankalert2notallocated_gid"].ToString() + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }

                            }
                        }
                        objODBCDatareader.Close();


                    }
                }
                dt_datatable.Dispose();
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Customer URN Updated Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }

        public void DaGetCustomers(CustomersList values, string lscustomername)
        {
            msSQL = " SELECT customer_gid,customername" +
                " FROM ocs_mst_tcustomer " +
                " WHERE customername like '%" + lscustomername + "%'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.Customers = dt_datatable.AsEnumerable().Select(row => new Customers
                {
                    customer_gid = row["customer_gid"].ToString(),
                    customername = row["customername"].ToString()
                }).ToList();

                dt_datatable.Dispose();
                values.status = true;
                values.message = "Record Found";
            }
            else
            {
                dt_datatable.Dispose();
                values.status = false;
                values.message = "No Record";
            }

        }

        public bool DaPostTagtoLegal(mdltagtolegal values, string employee_gid)
        {

            msGetGid = objcmnfunctions.GetMasterGID("CTLH");

            msSQL = " insert into ocs_trn_tcustomer2legalhistory(" +
                    " customer2legalhistory_gid," +
                    " customer_gid," +
                    " customer_name," +
                    " customer_urn," +
                    " customer_status," +
                    " remarks," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.customer_gid + "'," +
                    "'" + values.customer_name.Replace("'", "") + "'," +
                    "'" + values.currentcustomer_urn + "'," +
                    "'Tagged'," +
                    "'" + values.tag_remarks.Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "current_timestamp)";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                msSQL = " update ocs_mst_tcustomer set " +
                        " customer2legalhistory_gid='" + msGetGid + "'," +
                        " legaltag_flag='Y'," +
                        " tag_remarks='" + values.tag_remarks.Replace("'", "") + "'," +
                        " tagged_by='" + employee_gid + "'," +
                        " tagged_date=current_timestamp" +
                        " where customer_gid='" + values.customer_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Customer Tagged to Legal Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }

        public void DaGetTaggedCustomerList(mdltagtolegal objCustomer)
        {
            try
            {
                msSQL = " select a.customer_gid,a.vertical_code,a.customer_urn,a.customername, " +
                        " concat(c.user_firstname,' ',c.user_lastname,'/',c.user_code) as tagged_by, " +
                        " date_format(a.tagged_date,'%d-%m-%Y') as tagged_date,a.tag_remarks " +
                        " from ocs_mst_tcustomer a " +
                        " left join hrm_mst_temployee b on a.tagged_by = b.employee_gid" +
                        " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                        " where a.legaltag_flag='Y' order by a.customer2legalhistory_gid desc  ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcustomer = new List<customertag_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcustomer.Add(new customertag_list
                        {
                            customer_gid = (dr_datarow["customer_gid"].ToString()),
                            customer_urn = (dr_datarow["customer_urn"].ToString()),
                            customer_name = (dr_datarow["customername"].ToString()),
                            vertical = (dr_datarow["vertical_code"].ToString()),
                            tagged_by = (dr_datarow["tagged_by"].ToString()),
                            tagged_date = (dr_datarow["tagged_date"].ToString()),
                            remarks = (dr_datarow["tag_remarks"].ToString())
                        });
                    }
                    objCustomer.customertag_list = getcustomer;
                }
                dt_datatable.Dispose();
                objCustomer.status = true;
            }
            catch
            {
                objCustomer.status = false;
            }
        }

        public void DaGetTaggedHistoeyList(mdltagtolegal objCustomer, string customer_gid)
        {
            try
            {
                msSQL = " select a.customer_gid,a.customer2legalhistory_gid,a.remarks, " +
                        " concat(a.customer_status,' ','by',' ',concat(c.user_code,'/',c.user_firstname,' ',c.user_lastname),' ','on',' ',date_format(a.created_date,'%d-%m-%Y %h:%i:%s %p')) as done_by " +
                        " from ocs_trn_tcustomer2legalhistory a " +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                        " where a.customer_gid='" + customer_gid + "' order by a.customer2legalhistory_gid desc  ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcustomer = new List<customertag_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcustomer.Add(new customertag_list
                        {
                            customer_gid = (dr_datarow["customer_gid"].ToString()),
                            customer2legalhistory_gid = (dr_datarow["customer2legalhistory_gid"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            tagged_by = (dr_datarow["done_by"].ToString())
                        });
                    }
                    objCustomer.customertag_list = getcustomer;
                }
                dt_datatable.Dispose();
                objCustomer.status = true;
            }
            catch
            {
                objCustomer.status = false;
            }
        }

        public bool DaPostUnTagtoLegal(mdluntagtolegal values, string employee_gid)
        {

            msGetGid = objcmnfunctions.GetMasterGID("CTLH");

            msSQL = " insert into ocs_trn_tcustomer2legalhistory(" +
                    " customer2legalhistory_gid," +
                    " customer_gid," +
                    " customer_name," +
                    " customer_urn," +
                    " customer_status," +
                    " remarks," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.customer_gid + "'," +
                    "'" + values.customer_name.Replace("'", "") + "'," +
                    "'" + values.currentcustomer_urn + "'," +
                    "'UnTagged'," +
                    "'" + values.untag_remarks.Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "current_timestamp)";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                msSQL = " update ocs_mst_tcustomer set " +
                        " customer2legalhistory_gid='" + msGetGid + "'," +
                        " legaltag_flag='N'," +
                        " untag_remarks='" + values.untag_remarks.Replace("'", "") + "'," +
                        " untagged_by='" + employee_gid + "'," +
                        " untagged_date=current_timestamp" +
                        " where customer_gid='" + values.customer_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Customer UnTagged From Legal Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }
        public void DaGetCommonCustomers(CustomersList values, string lscustomername)
        {
            msSQL = " (SELECT customer_gid" +
                    " FROM ocs_mst_tcustomer WHERE customername like '%" + lscustomername + "%') union (SELECT tmpcustomer_gid as customer_gid" +
                    " FROM ocs_tmp_tcustomer WHERE customername like '%" + lscustomername + "%')";
            string lscustomer_gid = objdbconn.GetExecuteScalar(msSQL);

            if (lscustomer_gid == "" || lscustomer_gid == null)
            {
                values.status = false;
                return;
            }
            else
            {
                msSQL = " SELECT customer_gid,customername" +
                                " FROM ocs_mst_tcustomer " +
                                " WHERE customername like '%" + lscustomername + "%'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    values.Customers = dt_datatable.AsEnumerable().Select(row => new Customers
                    {
                        customer_gid = row["customer_gid"].ToString(),
                        customername = row["customername"].ToString()
                    }).ToList();
                    values.message = "You can't add this Customer. Already Added in CAD master.";
                    dt_datatable.Dispose();
                    values.status = true;
                    return;
                }
                else
                {
                    msSQL = " SELECT tmpcustomer_gid,customername" +
                          " FROM ocs_tmp_tcustomer " +
                          " WHERE customername like '%" + lscustomername + "%'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        values.Customers = dt_datatable.AsEnumerable().Select(row => new Customers
                        {
                            customer_gid = row["tmpcustomer_gid"].ToString(),
                            customername = row["customername"].ToString()
                        }).ToList();

                    }
                    dt_datatable.Dispose();
                    values.status = true;
                    values.message = "You can't add this Customer. Tag the customer from master.";
                    return;

                }
            }
        }

        public void DaGetCustomerData(customerSummary objcustomersummary)
        {
            msSQL = " select a.customer_urn as 'Customer URN', a.customername as 'Customer Name', a.contactperson as 'Contact Person', a.email as 'Email Address', " +
                 " a.contact_no as 'Contact No', a.mobileno as 'Mobile No', a.vertical_code as 'Vertical', a.gst_number as 'GST Number', a.pan_number as 'PAN Number', " +
                 " a.address as 'Address Line 1', a.address2 as 'Address Line 2', a.region as 'Region', a.state as 'State', a.postalcode as 'Postal Code', " +
                 " a.country as 'Country', a.constitution_name as 'Constitution', a.zonal_name as 'Zonal Head', a.businesshead_name as 'Business Head', a.regionalhead_name as 'Regional Head', " +
                 " a.cluster_manager_name as 'Cluster Manager', a.relationshipmgmt_name as 'Relationship Manager', a.creditmgmt_name as 'Credit Manager', " +
                 " a.zonal_riskmanagerName as 'Zonal Risk Manager', a.assigned_RMName as 'Risk Manager', a.riskMonitoring_Name as 'Head Risk Monitoring', " +
                 " a.ccmail_text as 'CC Mail', a.major_corporate as 'Major Corporate/Group Company Name', " +
                 " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as 'Created By', " +
                 " date_format(a.created_date, '%d-%m-%Y') as 'Created On'," +
                 " case when a.legaltag_flag is null then 'NA' else " +
                        " (select concat(e.customer_status, ' ', 'by', ' ', concat(c.user_code, '/', c.user_firstname, ' ', c.user_lastname), ' ', 'on', ' '," +
                        " date_format(e.created_date, '%d-%m-%Y %h:%i:%s %p')) as done_by " +
                        " from ocs_trn_tcustomer2legalhistory e " +
                        " left join hrm_mst_temployee b on e.created_by = b.employee_gid " +
                        " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                        " where e.customer2legalhistory_gid = a.customer2legalhistory_gid )" +
                 " end as 'Legal Status', " +
                 " case when legaltag_flag = 'Y' then a.tag_remarks when legaltag_flag = 'N' then a.untag_remarks else 'NA' end as 'Legal Remarks', " +
                 " case when a.npatag_flag is null then 'NA' else " +
                        " (select concat(f.customer_status, ' ', 'by', ' ', concat(c.user_code, '/', c.user_firstname, ' ', c.user_lastname), ' ', 'on', ' '," +
                        " date_format(f.created_date, '%d-%m-%Y %h:%i:%s %p')) as done_by " +
                        " from ocs_trn_tcustomer2npahistory f " +
                        " left join hrm_mst_temployee b on f.created_by = b.employee_gid " +
                        " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                        " where f.customer2npahistory_gid = a.customer2npahistory_gid )" +
                 " end as 'NPA Status', " +
                 " case when npatag_flag = 'Y' then a.npatag_remarks when npatag_flag = 'N' then a.npauntag_remarks else 'NA' end as 'NPA Remarks' " +
                 " from ocs_mst_tcustomer a " +
                 " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                 " left join adm_mst_tuser c on b.user_gid = c.user_gid ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;


            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("CAD Customer List");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objcustomersummary.lsname = "Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "ECMS/Customer/CustomerReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objcustomersummary.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "ECMS/Customer/CustomerReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objcustomersummary.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objcustomersummary.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 33])  //Address "A1:A33"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                dt_datatable.Dispose();
                objcustomersummary.lspath = lscompany_code + "/" + "ECMS/Customer/CustomerReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objcustomersummary.lsname;
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", objcustomersummary.lspath, ms);
                ms.Close();

            }
            catch (Exception ex)
            {
                objcustomersummary.status = false;
                objcustomersummary.message = "Failure";
            }
            objcustomersummary.lspath = objcmnstorage.EncryptData(objcustomersummary.lspath);
            objcustomersummary.status = true;
            objcustomersummary.message = "Success";
        }

        public bool DaPostTagtoNPA(mdltagtonpa values, string employee_gid)
        {

            msGetGid = objcmnfunctions.GetMasterGID("CTNH");

            msSQL = " insert into ocs_trn_tcustomer2npahistory(" +
                    " customer2npahistory_gid," +
                    " customer_gid," +
                    " customer_name," +
                    " customer_urn," +
                    " customer_status," +
                    " remarks," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.customer_gid + "'," +
                    "'" + values.customer_name.Replace("'", "") + "'," +
                    "'" + values.currentcustomer_urn + "'," +
                    "'Tagged'," +
                    "'" + values.tag_remarks.Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "current_timestamp)";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                msSQL = " update ocs_mst_tcustomer set " +
                        " customer2npahistory_gid='" + msGetGid + "'," +
                        " npatag_flag='Y'," +
                        " npatag_remarks='" + values.tag_remarks.Replace("'", "") + "'," +
                        " npatagged_by='" + employee_gid + "'," +
                        " npatagged_date=current_timestamp" +
                        " where customer_gid='" + values.customer_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Customer Tagged to NPA Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }

        public void DaGetTaggedNPACustomerList(mdltagtonpa objCustomer)
        {
            try
            {
                msSQL = " select a.customer_gid,a.vertical_code,a.customer_urn,a.customername, " +
                        " concat(c.user_firstname,' ',c.user_lastname,'/',c.user_code) as tagged_by, " +
                        " date_format(a.npatagged_date,'%d-%m-%Y') as tagged_date,a.npatag_remarks " +
                        " from ocs_mst_tcustomer a " +
                        " left join hrm_mst_temployee b on a.npatagged_by = b.employee_gid" +
                        " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                        " where a.npatag_flag='Y' order by a.customer2npahistory_gid desc  ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcustomer = new List<customertagnpa_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcustomer.Add(new customertagnpa_list
                        {
                            customer_gid = (dr_datarow["customer_gid"].ToString()),
                            customer_urn = (dr_datarow["customer_urn"].ToString()),
                            customer_name = (dr_datarow["customername"].ToString()),
                            vertical = (dr_datarow["vertical_code"].ToString()),
                            tagged_by = (dr_datarow["tagged_by"].ToString()),
                            tagged_date = (dr_datarow["tagged_date"].ToString()),
                            remarks = (dr_datarow["npatag_remarks"].ToString())
                        });
                    }
                    objCustomer.customertagnpa_list = getcustomer;
                }
                dt_datatable.Dispose();
                objCustomer.status = true;
            }
            catch
            {
                objCustomer.status = false;
            }
        }

        public void DaGetTaggedNPAHistoryList(mdltagtonpa objCustomer, string customer_gid)
        {
            try
            {
                msSQL = " select a.customer_gid,a.customer2npahistory_gid,a.remarks, " +
                        " concat(a.customer_status,' ','by',' ',concat(c.user_code,'/',c.user_firstname,' ',c.user_lastname),' ','on',' ',date_format(a.created_date,'%d-%m-%Y %h:%i:%s %p')) as done_by " +
                        " from ocs_trn_tcustomer2npahistory a " +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                        " where a.customer_gid='" + customer_gid + "' order by a.customer2npahistory_gid desc  ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcustomer = new List<customertagnpa_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcustomer.Add(new customertagnpa_list
                        {
                            customer_gid = (dr_datarow["customer_gid"].ToString()),
                            customer2npahistory_gid = (dr_datarow["customer2npahistory_gid"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            tagged_by = (dr_datarow["done_by"].ToString())
                        });
                    }
                    objCustomer.customertagnpa_list = getcustomer;
                }
                dt_datatable.Dispose();
                objCustomer.status = true;
            }
            catch
            {
                objCustomer.status = false;
            }
        }

        public bool DaPostUnTagtoNPA(mdluntagtonpa values, string employee_gid)
        {

            msGetGid = objcmnfunctions.GetMasterGID("CTNH");

            msSQL = " insert into ocs_trn_tcustomer2npahistory(" +
                    " customer2npahistory_gid," +
                    " customer_gid," +
                    " customer_name," +
                    " customer_urn," +
                    " customer_status," +
                    " remarks," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.customer_gid + "'," +
                    "'" + values.customer_name.Replace("'", "") + "'," +
                    "'" + values.currentcustomer_urn + "'," +
                    "'UnTagged'," +
                    "'" + values.untag_remarks.Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "current_timestamp)";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                msSQL = " update ocs_mst_tcustomer set " +
                        " customer2npahistory_gid='" + msGetGid + "'," +
                        " npatag_flag='N'," +
                        " npauntag_remarks='" + values.untag_remarks.Replace("'", "") + "'," +
                        " npauntagged_by='" + employee_gid + "'," +
                        " npauntagged_date=current_timestamp" +
                        " where customer_gid='" + values.customer_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Customer UnTagged From NPA Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }

    }
}