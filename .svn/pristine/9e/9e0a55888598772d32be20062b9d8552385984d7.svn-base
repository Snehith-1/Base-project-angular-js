using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.rsk.Models;
using System.Configuration;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using ems.storage.Functions;

namespace ems.rsk.DataAccess
{
    public class DaAllocationManagement
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader, objODBCDatareader1, objODBCDatareader2;
        DataTable dt_datatable, dr_datatable;
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        HttpPostedFile httpPostedFile;
        string msSQL, msGetGid, msGetSanctionGid, msGetAllocationGid;
        string msGetCollateralGid, msGetGurantorGid, msGetPromotorGid;
        string lstransferred_from, lstransfrom_stategid, lstransfrom_districtgid;
        string lsstate_name, lsdistrict_name;
        int mnResult;
        string lspath, lscount_status;
        string msGetDocumentGid;
        string lsdisbursement_date, lsdaypassed_visit;
        string msGet_visitGid, lssanctionamount, lscustomer_urn;
        int lsallocation_pendingcount;
        string lscount_fresh;
        string lscount_revisit;
        int lsmonth, lsmonthdesc, lsmontheql, lscurmonth;
        string lsexpiry_date = string.Empty;

        public bool DaGetAllocateRM(string district_gid, mappingdtl values)
        {
            msSQL = " select a.assigned_RM,concat(c.user_firstname,' ',c.user_lastname, ' / ',c.user_code) as allocateRMname, " +
                    " a.zonal_RM,concat(e.user_firstname,' ',e.user_lastname, ' / ',e.user_code) as ZonalRMname" +
                    " from rsk_mst_tRMmapping a" +
                    " left join hrm_mst_temployee b on a.assigned_RM=b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " left join hrm_mst_temployee d on a.zonal_RM=d.employee_gid" +
                    " left join adm_mst_tuser e on e.user_gid=d.user_gid" +
                    " where district_gid='" + district_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.assigned_RM = objODBCDatareader["allocateRMname"].ToString();
                values.assignedRM_gid = objODBCDatareader["assigned_RM"].ToString();
                values.ZonalRM_gid = objODBCDatareader["zonal_RM"].ToString();
                values.ZonalRMname = objODBCDatareader["ZonalRMname"].ToString();
            }
            objODBCDatareader.Close();
            return true;
        }

        public bool DaPostAllocationRMdetails(string employee_gid, mappingdtl values)
        {

            msSQL = "select sum(sanction_amount) from ocs_mst_tcustomer2sanction where customer_gid='" + values.customer_gid + "'";
            string lssanction_amount = objdbconn.GetExecuteScalar(msSQL);
            if (lssanction_amount == "")
            {
                values.status = false;
                values.message = "Sanction Details are empty..!";
                return false;
            }
            else
            {

            }


            msSQL = "select allocation_gid from rsk_trn_tallocation where customer_gid='" + values.customer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {

                msSQL = " update rsk_trn_tallocation set " +
                        " zonal_gid ='" + values.zonal_gid + "'," +
                        " state_gid='" + values.state_gid + "'," +
                        " district_gid ='" + values.district_gid + "'," +
                        " zonal_RM='" + values.ZonalRM_gid + "'," +
                        " assigned_RM='" + values.assignedRM_gid + "',";
                if (lssanction_amount == "")
                {
                    msSQL += "sanction_amount=null,";
                }
                else
                {
                    msSQL += " sanction_amount='" + lssanction_amount + "',";
                }
                msSQL += " status='Allocated'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where allocation_gid='" + objODBCDatareader["allocation_gid"].ToString() + "'";
                objODBCDatareader.Close();
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }
            else
            {
                objODBCDatareader.Close();
                msGetAllocationGid = objcmnfunctions.GetMasterGID("CUAL");
                msSQL = " Insert into rsk_trn_tallocation( " +
                          " allocation_gid," +
                          " customer_gid," +
                          " zonal_gid," +
                          " state_gid," +
                          " district_gid," +
                          " zonal_RM, " +
                          " assigned_RM," +
                          " sanction_amount," +
                          " status," +
                          " created_by," +
                          " created_date)" +
                          " values(" +
                          "'" + msGetAllocationGid + "', " +
                          "'" + values.customer_gid + "'," +
                          "'" + values.zonal_gid + "'," +
                          "'" + values.state_gid + "'," +
                          "'" + values.district_gid + "'," +
                          "'" + values.ZonalRM_gid + "'," +
                          "'" + values.assignedRM_gid + "',";
                if (lssanction_amount == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + lssanction_amount + "',";
                }

                msSQL += "'Allocated'," +
                 "'" + employee_gid + "'," +
                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            msSQL = "select allocation_gid from rsk_trn_tallocation where customer_gid='" + values.customer_gid + "'";
            string lsallocation_gid = objdbconn.GetExecuteScalar(msSQL);
            msGetGid = objcmnfunctions.GetMasterGID("RKAL");

            msSQL = " Insert into rsk_trn_tallocationdtl( " +
                           " allocationdtl_gid," +
                           " allocation_gid," +
                           " customer_gid," +
                           " zonal_gid," +
                           " state_gid," +
                           " state_name," +
                           " district_gid," +
                           " district_name," +
                           " allocation_assignedRM," +
                           " assignedRM_name," +
                           " allocation_zonalRM," +
                           " zonalRM_name, " +
                           " allocation_flag," +
                           " allocation_status," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid + "', " +
                           "'" + lsallocation_gid + "', " +
                           "'" + values.customer_gid + "'," +
                           "'" + values.zonal_gid + "'," +
                           "'" + values.state_gid + "'," +
                           "'" + values.state_name + "'," +
                           "'" + values.district_gid + "'," +
                           "'" + values.district_name + "'," +
                           "'" + values.assignedRM_gid + "'," +
                           "'" + values.assigned_RM + "'," +
                           "'" + values.ZonalRM_gid + "'," +
                           "'" + values.ZonalRMname + "'," +
                           "'Y'," +
                           "'Allocated'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Allocated Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }

        public bool DaGetAllocationCustomerDtl(sanctionloanlist values, string allocationdtl_gid)
        {

            msSQL = " select a.customer_gid,a.vertical_code,a.customer_name, " +
               " case when a.address_line1<>'' then a.address_line1 else '-' end as address,address_line2, " +
               " case when a.contact_no<>'' then a.contact_no else '-' end as contact_no, " +
               " case when a.contact_person<>'' then a.contact_person else '-' end as contactperson, " +
               " case when a.creditmanager_gid='' then 'NA' else a.creditmanager_name end as creditmgmt_name," +
               " case when a.zonalhead_gid = '' then 'NA' else a.zonalhead_name end as zonal_head," +
               " case when a.businesshead_gid = '' then 'NA' else a.businesshead_name end as business_head, " +
               " case when a.cluster_managergid = '' then 'NA' else a.cluster_managername end as cluster_manager, " +
               " case when a.relationship_managergid = '' then 'NA' else a.relationship_managername end as relationship_manager, " +
               " case when a.ppa_gid = '' then 'NA' else a.ppa_name end as ppa_name, " +
               " a.customer_urn " +
               " from rsk_trn_tallocatecustomerdtl a where allocationdtl_gid='" + allocationdtl_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.customerNameedit = objODBCDatareader["customer_name"].ToString();
                values.vertical_code = objODBCDatareader["vertical_code"].ToString();
                values.businesshead_name = objODBCDatareader["business_head"].ToString();
                values.zonal_name = objODBCDatareader["zonal_head"].ToString();
                values.cluster_manager_name = objODBCDatareader["cluster_manager"].ToString();
                values.relationshipmgmt_name = objODBCDatareader["relationship_manager"].ToString();
                values.creditmanager_name = objODBCDatareader["creditmgmt_name"].ToString();
                values.customer_urnedit = objODBCDatareader["customer_urn"].ToString();
                values.contactPersonedit = objODBCDatareader["contactperson"].ToString();
                values.addressline1edit = objODBCDatareader["address"].ToString();
                values.addressline2edit = objODBCDatareader["address_line2"].ToString();
                values.mobileNoedit = objODBCDatareader["contact_no"].ToString();
                values.ppa_name = objODBCDatareader["ppa_name"].ToString();
            }
            objODBCDatareader.Close();
            
            msSQL = " select a.sanction_gid,a.sanction_refno,date_format(a.sanction_date,'%d-%m-%Y') as sanctiondate,a.sanction_type, " +
                    " format(sanction_amount, 2) as sanction_amount,facility_type,a.entity,a.colanding_status,a.colander_name" +
                    " from rsk_trn_tallocatesanctiondtl a " +
                    " where a.allocationdtl_gid = '" + allocationdtl_gid + "' order by a.allocatesanctiondtl_Gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getloanlistdtl = new List<loandtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getloanlistdtl.Add(new loandtl
                    {
                        sanction_amount = (dr_datarow["sanction_amount"].ToString()),
                        sanction_refno = (dr_datarow["sanction_refno"].ToString()),
                        sanction_date = (dr_datarow["sanctiondate"].ToString()),
                        sanction_gid = (dr_datarow["sanction_gid"].ToString()),
                        facility_type = (dr_datarow["facility_type"].ToString()),
                        sanction_type = (dr_datarow["sanction_type"].ToString()),
                        entity = (dr_datarow["entity"].ToString()),
                        colanding_status = (dr_datarow["colanding_status"].ToString()),
                        colander_name = (dr_datarow["colander_name"].ToString()),
                    });
                }
                values.loandtl = getloanlistdtl;
            }
            dt_datatable.Dispose();

            msSQL = " select security_type,security_description,account_status " +
                    " from rsk_trn_tallocatecollateral where allocationdtl_gid='" + allocationdtl_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcollateraldtl = new List<collateraldtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcollateraldtl.Add(new collateraldtl
                    {
                        security_type = (dr_datarow["security_type"].ToString()),
                        security_description = dr_datarow["security_description"].ToString(),
                        account_status = (dr_datarow["account_status"].ToString()),
                    });
                }
                values.collateraldtl = getcollateraldtl;
            }
            dt_datatable.Dispose();

            msSQL = " select promoter_name,designation,promoter_age,mobile " +
                  " from rsk_trn_tallocatepromotors where allocationdtl_gid='" + allocationdtl_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_customerPromotor = new List<Promoterdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_customerPromotor.Add(new Promoterdtl
                    {
                        promoter_name = (dr_datarow["promoter_name"].ToString()),
                        designation = (dr_datarow["designation"].ToString()),
                        promoter_age = (dr_datarow["promoter_age"].ToString()),
                        mobile = (dr_datarow["mobile"].ToString()),
                    });
                }
                values.Promoterdtl = get_customerPromotor;
            }
            dt_datatable.Dispose();

            msSQL = " select guarantors_name,guarantor_age,networth,basisofNW " +
                        " from rsk_trn_tallocateguarantor where allocationdtl_gid='" + allocationdtl_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_customerGurantor = new List<Guarantorsdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_customerGurantor.Add(new Guarantorsdtl
                    {
                        guarantors_name = (dr_datarow["guarantors_name"].ToString()),
                        guarantor_age = (dr_datarow["guarantor_age"].ToString()),
                        networth = (dr_datarow["networth"].ToString()),
                        basisofNW = (dr_datarow["basisofNW"].ToString()),
                    });
                }
                values.Guarantorsdtl = get_customerGurantor;
            }
            dt_datatable.Dispose();

            msSQL = " select date_format(oldallocated_date,'%d-%m-%Y') as previous_allocatedate,allocationhold_reason, " +
                    " date_format(allocated_date,'%d-%m-%Y') as current_allocatedate, " +
                    " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as created_by, " +
                    " date_format(a.created_date, '%d-%m-%Y') as created_date from rsk_trn_tallocation2hold a " +
                    " left join adm_mst_tuser b on b.user_gid = a.created_by " +
                    " where allocationdtl_gid='" + allocationdtl_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_holdAllocation = new List<holdallocation>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_holdAllocation.Add(new holdallocation
                    {
                        previous_allocatedate = (dr_datarow["previous_allocatedate"].ToString()),
                        current_allocatedate = (dr_datarow["current_allocatedate"].ToString()),
                        allocationhold_reason = (dr_datarow["allocationhold_reason"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                    });
                }
                values.holdallocation = get_holdAllocation;
            }
            dt_datatable.Dispose();

            msSQL = " select date_format(oldallocated_date,'%d-%m-%Y') as previous_allocatedate,allocation_reason, " +
                    " date_format(allocated_date,'%d-%m-%Y') as current_allocatedate, " +
                    " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as created_by, " +
                    " date_format(a.created_date, '%d-%m-%Y') as created_date from rsk_trn_tupcoming2currentallocation a " +
                    " left join adm_mst_tuser b on b.user_gid = a.created_by " +
                    " where allocationdtl_gid='" + allocationdtl_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_U2CmovedAllocation = new List<U2CMovedallocation>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_U2CmovedAllocation.Add(new U2CMovedallocation
                    {
                        previous_allocatedate = (dr_datarow["previous_allocatedate"].ToString()),
                        current_allocatedate = (dr_datarow["current_allocatedate"].ToString()),
                        allocationmoved_reason = (dr_datarow["allocation_reason"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                    });
                }
                values.U2CMovedallocation = get_U2CmovedAllocation;
            }
            dt_datatable.Dispose();

            values.status = true;
            return true;
        }


        public void DaGetAllocateloanList(allocationsanction objvalues, loanListdetail values)
        {
            msSQL = " select format(loanfacility_amount,2) as loanfacility_amount,loanfacility_type,format(document_limit,2) as document_limit," +
                    " date_format(expiry_date, '%d-%m-%Y') as expiry_date,tenure,loanfacilityref_no,proposed_roi " +
                    " from rsk_trn_tallocateloan " +
                    " where sanction_gid='" + objvalues.sanction_gid + "' and allocationdtl_gid='" + objvalues.allocationdtl_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getloanlistdtl = new List<loanList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getloanlistdtl.Add(new loanList
                    {
                        loanfacility_amount = (dr_datarow["loanfacility_amount"].ToString()),
                        loanfacility_type = (dr_datarow["loanfacility_type"].ToString()),
                        document_limit = (dr_datarow["document_limit"].ToString()),
                        expiry_date = (dr_datarow["expiry_date"].ToString()),
                        tenure = (dr_datarow["tenure"].ToString()),
                        loanfacilityref_no = (dr_datarow["loanfacilityref_no"].ToString()),
                        proposed_roi = (dr_datarow["proposed_roi"].ToString()),
                    });
                    values.loanList = getloanlistdtl;
                }
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public bool DaPostCreateAllocation(mappingdtl values, string employee_gid)
        {


            msSQL = "select sum(sanction_amount) from ocs_mst_tcustomer2sanction where customer_gid='" + values.customer_gid + "'";
            string lssanction_amount = objdbconn.GetExecuteScalar(msSQL);
            if (lssanction_amount == "")
            {
                values.status = false;
                values.message = "Sanction Details are empty..!";
                return false;
            }
            else
            {
                msSQL = "select customer_urn from ocs_mst_tcustomer where customer_gid='" + values.customer_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.customer_urn = objODBCDatareader["customer_urn"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = "select allocation_gid from rsk_trn_tallocation where customer_gid='" + values.customer_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {

                    msSQL = " update rsk_trn_tallocation set " +
                            " zonal_gid ='" + values.zonal_gid + "'," +
                            " state_gid='" + values.state_gid + "'," +
                            " district_gid ='" + values.district_gid + "'," +
                            " zonal_RM='" + values.ZonalRM_gid + "'," +
                            " assigned_RM='" + values.assignedRM_gid + "',";
                    if (lssanction_amount == "")
                    {
                        msSQL += "sanction_amount=null,";
                    }
                    else
                    {
                        msSQL += " sanction_amount='" + lssanction_amount + "',";
                    }
                    msSQL += " status='Allocated'," +
                     " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where allocation_gid='" + objODBCDatareader["allocation_gid"].ToString() + "'";
                    objODBCDatareader.Close();
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }
                else
                {
                    objODBCDatareader.Close();


                    msGetAllocationGid = objcmnfunctions.GetMasterGID("CUAL");
                    msSQL = " Insert into rsk_trn_tallocation( " +
                              " allocation_gid," +
                              " customer_gid," +
                              " customer_urn," +
                              " zonal_gid," +
                              " state_gid," +
                              " district_gid," +
                              " zonal_RM, " +
                              " assigned_RM," +
                              " sanction_amount," +
                              " status," +
                              " created_by," +
                              " created_date)" +
                              " values(" +
                              "'" + msGetAllocationGid + "', " +
                              "'" + values.customer_gid + "'," +
                              "'" + lscustomer_urn + "'," +
                              "'" + values.zonal_gid + "'," +
                              "'" + values.state_gid + "'," +
                              "'" + values.district_gid + "'," +
                              "'" + values.ZonalRM_gid + "'," +
                              "'" + values.assignedRM_gid + "',";
                    if (lssanction_amount == "")
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += "'" + lssanction_amount + "',";
                    }

                    msSQL += "'Allocated'," +

                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                msSQL = "update rsk_trn_tcustomerdisbursement set allocate_flag='Y' where customer_urn='" + values.customer_urn + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select allocation_gid from rsk_trn_tallocation where customer_gid='" + values.customer_gid + "'";
                string lsallocation_gid = objdbconn.GetExecuteScalar(msSQL);

                msGetGid = objcmnfunctions.GetMasterGID("RKAL");
                values.allocationdtl_gid = msGetGid;
                msSQL = " Insert into rsk_trn_tallocationdtl( " +
                               " allocationdtl_gid," +
                               " allocation_gid," +
                               " customer_gid," +
                               " customer_name," +
                               " zonal_gid," +
                               " state_gid," +
                               " state_name," +
                               " district_gid," +
                               " district_name," +
                               " allocation_assignedRM," +
                               " assignedRM_name," +
                               " allocation_zonalRM," +
                               " zonalRM_name, " +
                               " allocation_flag," +
                               " allocation_status," +
                               " qualified_status," +
                               " Manual_Allocation, " +
                               " allocated_by," +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + msGetGid + "', " +
                               "'" + lsallocation_gid + "', " +
                               "'" + values.customer_gid + "'," +
                               "'" + values.customername + "'," +
                               "'" + values.zonal_gid + "'," +
                               "'" + values.state_gid + "'," +
                               "'" + values.state_name + "'," +
                               "'" + values.district_gid + "'," +
                               "'" + values.district_name + "'," +
                               "'" + values.assignedRM_gid + "'," +
                               "'" + values.assigned_RM + "'," +
                               "'" + values.ZonalRM_gid + "'," +
                               "'" + values.ZonalRMname + "'," +
                               "'Y'," +
                               "'Allocated'," +
                               "'" + values.qualified_status + "'," +
                               "'" + values.Manual_Allocation + "'," +
                               "'" + employee_gid + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update rsk_trn_tallocationdtl" +
                      " set visit_allocated_date = CONCAT(LEFT(NOW() + INTERVAL 1 MONTH, 7), '-01') " +
                      " where allocationdtl_gid = '" + msGetGid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " select customer_gid,customername,customer_urn,contactperson,contact_no,address,address2, " +
                        " vertical_gid,vertical_code,zonal_head,zonal_name,business_head,businesshead_name, " +
                        " relationship_manager,relationshipmgmt_name,cluster_manager_gid,cluster_manager_name, " +
                        " creditmanager_gid,creditmgmt_name,ppa_gid,ppa_name from ocs_mst_tcustomer " +
                        " where customer_gid='" + values.customer_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    string msGetCustomerGid = objcmnfunctions.GetMasterGID("ALCU");
                    msSQL = " Insert into rsk_trn_tallocatecustomerdtl( " +
                          " allocatecustomerdtl_gid," +
                          " allocationdtl_gid," +
                          " customer_gid," +
                          " customer_name," +
                          " customer_urn," +
                          " contact_person," +
                          " contact_no," +
                          " address_line1," +
                          " address_line2," +
                          " vertical_gid," +
                          " vertical_code," +
                          " zonalhead_gid," +
                          " zonalhead_name," +
                          " businesshead_gid," +
                          " businesshead_name," +
                          " relationship_managergid," +
                          " relationship_managername," +
                          " cluster_managergid," +
                          " cluster_managername," +
                          " creditmanager_gid," +
                          " creditmanager_name," +
                          " ppa_gid," +
                          " ppa_name," +
                          " created_by," +
                          " created_date)" +
                          " values ( " +
                          "'" + msGetCustomerGid + "'," +
                          "'" + values.allocationdtl_gid + "'," +
                          "'" + values.customer_gid + "'," +
                          "'" + objODBCDatareader["customername"].ToString() + "'," +
                          "'" + objODBCDatareader["customer_urn"].ToString() + "'," +
                          "'" + objODBCDatareader["contactperson"].ToString() + "'," +
                          "'" + objODBCDatareader["contact_no"].ToString() + "'," +
                          "'" + objODBCDatareader["address"].ToString() + "'," +
                          "'" + objODBCDatareader["address2"].ToString() + "'," +
                          "'" + objODBCDatareader["vertical_gid"].ToString() + "'," +
                          "'" + objODBCDatareader["vertical_code"].ToString() + "'," +
                          "'" + objODBCDatareader["zonal_head"].ToString() + "'," +
                          "'" + objODBCDatareader["zonal_name"].ToString() + "'," +
                          "'" + objODBCDatareader["business_head"].ToString() + "'," +
                          "'" + objODBCDatareader["businesshead_name"].ToString() + "'," +
                          "'" + objODBCDatareader["relationship_manager"].ToString() + "'," +
                          "'" + objODBCDatareader["relationshipmgmt_name"].ToString() + "'," +
                          "'" + objODBCDatareader["cluster_manager_gid"].ToString() + "'," +
                          "'" + objODBCDatareader["cluster_manager_name"].ToString() + "'," +
                          "'" + objODBCDatareader["creditmanager_gid"].ToString() + "'," +
                          "'" + objODBCDatareader["creditmgmt_name"].ToString() + "'," +
                          "'" + objODBCDatareader["ppa_gid"].ToString() + "'," +
                          "'" + objODBCDatareader["ppa_name"].ToString() + "'," +
                          "'" + employee_gid + "'," +
                          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    objODBCDatareader.Close();
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msSQL = "select rsksanction_documentgid,file_name,file_path,document_name from rsk_mst_tsanctiondocumentdtl " +
                        " where customer_gid='" + values.customer_gid + "' and file_path<>''";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msGetDocumentGid = objcmnfunctions.GetMasterGID("DOAL");

                        msSQL = " Insert into rsk_trn_tallocationdocument( " +
                                  " allocation_documentGid," +
                                  " allocationdtl_gid," +
                                  " rsksanction_documentgid," +
                                  " customer_gid," +
                                  " document_name," +
                                  " document_path," +
                                  " document_type," +
                                  " created_by," +
                                  " created_date)" +
                                  " values(" +
                                  "'" + msGetDocumentGid + "', " +
                                  "'" + values.allocationdtl_gid + "'," +
                                  "'" + dt["rsksanction_documentgid"].ToString() + "'," +
                                  "'" + values.customer_gid + "'," +
                                  "'" + dt["file_name"].ToString() + "'," +
                                  "'" + dt["file_path"].ToString() + "'," +
                                  "'" + dt["document_name"].ToString().Replace("'", "\\'") + "'," +
                                  "'" + employee_gid + "'," +
                                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    }
                }
                dt_datatable.Dispose();

                msSQL = " select customer2guarantor_gid,guarantors_name,guarantor_age,networth,basisofNW " +
                         " from ocs_mst_tcustomer2guarantor where customer_gid='" + values.customer_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msGetGurantorGid = objcmnfunctions.GetMasterGID("ALGR");
                        msSQL = " Insert into rsk_trn_tallocateguarantor( " +
                                  " allocateGuarantor_Gid," +
                                  " allocationdtl_gid," +
                                  " guarantor_gid," +
                                  " guarantors_name," +
                                  " guarantor_age," +
                                  " networth," +
                                  " basisofNW," +
                                  " created_by," +
                                  " created_date)" +
                                  " values(" +
                                  "'" + msGetGurantorGid + "', " +
                                  "'" + values.allocationdtl_gid + "'," +
                                  "'" + dt["customer2guarantor_gid"].ToString() + "'," +
                                  "'" + dt["guarantors_name"].ToString() + "'," +
                                  "'" + dt["guarantor_age"].ToString() + "'," +
                                  "'" + dt["networth"].ToString() + "'," +
                                  "'" + dt["basisofNW"].ToString() + "'," +
                                  "'" + employee_gid + "'," +
                                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();

                msSQL = " select customer2promotor_gid,promoter_name,designation,promoter_age,mobile " +
                        " from ocs_mst_tcustomer2promotor where customer_gid='" + values.customer_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msGetPromotorGid = objcmnfunctions.GetMasterGID("ALPR");
                        msSQL = " Insert into rsk_trn_tallocatepromotors( " +
                                  " allocatepromotor_Gid," +
                                  " allocationdtl_gid," +
                                  " promotor_gid," +
                                  " promoter_name," +
                                  " designation," +
                                  " promoter_age," +
                                  " mobile," +
                                  " created_by," +
                                  " created_date)" +
                                  " values(" +
                                  "'" + msGetPromotorGid + "', " +
                                  "'" + values.allocationdtl_gid + "'," +
                                  "'" + dt["customer2promotor_gid"].ToString() + "'," +
                                  "'" + dt["promoter_name"].ToString() + "'," +
                                  "'" + dt["designation"].ToString() + "'," +
                                  "'" + dt["promoter_age"].ToString() + "'," +
                                  "'" + dt["mobile"].ToString() + "'," +
                                  "'" + employee_gid + "'," +
                                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();

                msSQL = " select customer2sanction_gid,a.sanction_refno,sanction_date, a.sanction_type,sanction_amount,facility_type," +
                " a.entity,a.colanding_status,a.colander_name from ocs_mst_tcustomer2sanction a " +
                " where a.customer_gid = '" + values.customer_gid + "' order by a.customer2sanction_gid desc ";
                
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msGetSanctionGid = objcmnfunctions.GetMasterGID("ALSA");
                        msSQL = " Insert into rsk_trn_tallocatesanctiondtl( " +
                                  " allocatesanctiondtl_Gid," +
                                  " allocationdtl_gid," +
                                  " sanction_gid," +
                                  " sanction_refno," +
                                  " facility_type," +
                                  " sanction_date," +
                                  " sanction_amount," +
                                  " sanction_type," +
                                  " entity," +
                                  " colanding_status," +
                                  " colander_name," +
                                  " created_by," +
                                  " created_date)" +
                                  " values(" +
                                  "'" + msGetSanctionGid + "', " +
                                  "'" + values.allocationdtl_gid + "'," +
                                  "'" + dt["customer2sanction_gid"].ToString() + "'," +
                                  "'" + dt["sanction_refno"].ToString() + "'," +
                                  "'" + dt["facility_type"].ToString() + "'," +
                                  "'" + Convert.ToDateTime(dt["sanction_date"]).ToString("yyyy-MM-dd") + "',";
                        if (dt["sanction_amount"].ToString() != "")
                        {
                            msSQL += "'" + dt["sanction_amount"].ToString() + "',";
                        }
                        else
                        {
                            msSQL += "null,";
                        }
                        msSQL += "'" + dt["sanction_type"].ToString() + "'," +
                            "'" + dt["entity"].ToString() + "'," +
                            "'" + dt["colanding_status"].ToString() + "'," +
                            "'" + dt["colander_name"].ToString() + "'," +
                                 "'" + employee_gid + "'," +
                                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " select loanfacility_amount,loanfacility_type,document_limit, expiry_date,tenure,loanfacilityref_no,proposed_roi" +
                                " from ocs_mst_tsanction2loanfacilitytype  where customer2sanction_gid='" + dt["customer2sanction_gid"] + "'";
                        
                        dr_datatable = objdbconn.GetDataTable(msSQL);
                        if (dr_datatable.Rows.Count != 0)
                        {
                            foreach (DataRow dr in dr_datatable.Rows)
                            {
                                if(dr["expiry_date"].ToString() == null || dr["expiry_date"].ToString() == "")
                                {
                                    lsexpiry_date = null;
                                }
                                else
                                {
                                    lsexpiry_date = dr["expiry_date"].ToString();
                                }
                                string msgetGet_LoanGid = objcmnfunctions.GetMasterGID("ALLO");

                                msSQL = " Insert into rsk_trn_tallocateloan( " +
                                          " allocate_loangid," +
                                          " allocationdtl_gid," +
                                          " sanction_gid," +
                                          " loanfacility_amount," +
                                          " loanfacility_type," +
                                          " document_limit," +
                                          " expiry_date," +
                                          " tenure," +
                                          " loanfacilityref_no," +
                                          " proposed_roi," +
                                          " created_by," +
                                          " created_date)" +
                                          " values(" +
                                          "'" + msgetGet_LoanGid + "', " +
                                          "'" + values.allocationdtl_gid + "'," +
                                          "'" + dt["customer2sanction_gid"].ToString() + "'," +
                                          "'" + dr["loanfacility_amount"].ToString() + "'," +
                                          "'" + dr["loanfacility_type"].ToString() + "'," +
                                          "'" + dr["document_limit"].ToString() + "'," +
                                          "'" + Convert.ToDateTime(lsexpiry_date).ToString("yyyy-MM-dd") + "'," +
                                          "'" + dr["tenure"].ToString() + "'," +
                                          "'" + dr["loanfacilityref_no"].ToString() + "'," +
                                          "'" + dr["proposed_roi"].ToString() + "'," +
                                          "'" + employee_gid + "'," +
                                          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                        }
                        dr_datatable.Dispose();
                    }
                }
                dt_datatable.Dispose();


                msSQL = " select collateral_gid,security_type,security_description,account_status " +
                        " from ocs_trn_tcustomercollateral where customer_gid='" + values.customer_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {

                        msGetCollateralGid = objcmnfunctions.GetMasterGID("ALCL");

                        msSQL = " Insert into rsk_trn_tallocatecollateral( " +
                                  " allocatecollateral_gid," +
                                  " allocationdtl_gid," +
                                  " collateral_gid," +
                                  " security_type," +
                                  " security_description," +
                                  " account_status," +
                                  " created_by," +
                                  " created_date)" +
                                  " values(" +
                                  "'" + msGetCollateralGid + "', " +
                                  "'" + values.allocationdtl_gid + "'," +
                                  "'" + dt["collateral_gid"].ToString() + "'," +
                                  "'" + dt["security_type"].ToString() + "'," +
                                  "'" + dt["security_description"].ToString() + "'," +
                                  "'" + dt["account_status"].ToString() + "'," +
                                  "'" + employee_gid + "'," +
                                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();
            }
            dt_datatable.Dispose();


            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Allocated  Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }


        public bool DaGetAllocationSaveDetails(allocation objvalues, string employee_gid)
        {
            msSQL = "select allocation_gid from rsk_trn_tallocationdtl where allocationdtl_gid='" + objvalues.allocationdtl_gid + "'";
            string lsallocation_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select sum(sanction_amount) from ocs_mst_tcustomer2sanction where customer_gid='" + objvalues.customer_gid + "'";
            string lssanction_amount = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " update rsk_trn_tallocation set ";
            if (lssanction_amount == "")
            {
                msSQL += " sanction_amount=null";
            }
            else
            {
                msSQL += " sanction_amount='" + lssanction_amount + "'";
            }
            msSQL += " where allocation_gid='" + lsallocation_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select document_name,document_path,document_type from rsk_tmp_tallocationdocument where created_by='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetDocumentGid = objcmnfunctions.GetMasterGID("DOAL");

                    msSQL = " Insert into rsk_trn_tallocationdocument( " +
                              " allocation_documentGid," +
                              " allocationdtl_gid," +
                              " document_name," +
                              " document_path," +
                              " document_type," +
                              " created_by," +
                              " created_date)" +
                              " values(" +
                              "'" + msGetDocumentGid + "', " +
                              "'" + objvalues.allocationdtl_gid + "'," +
                              "'" + dt["document_name"].ToString() + "'," +
                              "'" + dt["document_path"].ToString() + "'," +
                              "'" + dt["document_type"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult == 1)

                    {
                        msSQL = "delete from rsk_tmp_tallocationdocument where tmp_documentGid ='" + dt["tmp_documentGid"].ToString() + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
            }
            dt_datatable.Dispose();

            if (objvalues.allocationSubmit == "N")
            {

                msSQL = "select allocatecustomerdtl_gid from rsk_trn_tallocatecustomerdtl where allocationdtl_gid='" + objvalues.allocationdtl_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    msSQL = " select customername,customer_urn,contactperson,contact_no,address,address2, " +
                            " vertical_gid,vertical_code,zonal_head,zonal_name,business_head,businesshead_name, " +
                            " relationship_manager,relationshipmgmt_name,cluster_manager_gid,cluster_manager_name " +
                            " ppa_gid,ppa_name,creditmanager_gid,creditmgmt_name " +
                            " from ocs_mst_tcustomer where customer_gid='" + objvalues.customer_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        msSQL = " update rsk_trn_tallocatecustomerdtl set " +
                           " customer_name='" + objODBCDatareader["customername"].ToString() + "'," +
                           " customer_urn='" + objODBCDatareader["customer_urn"].ToString() + "'," +
                           " contact_person='" + objODBCDatareader["contactperson"].ToString() + "'," +
                           " contact_no='" + objODBCDatareader["contact_no"].ToString() + "'," +
                           " address_line1='" + objODBCDatareader["address"].ToString() + "'," +
                           " address_line2='" + objODBCDatareader["address2"].ToString() + "'," +
                           " vertical_gid='" + objODBCDatareader["vertical_gid"].ToString() + "'," +
                           " vertical_code='" + objODBCDatareader["vertical_code"].ToString() + "'," +
                           " zonalhead_gid='" + objODBCDatareader["zonal_head"].ToString() + "'," +
                           " zonalhead_name='" + objODBCDatareader["zonal_name"].ToString() + "'," +
                           " businesshead_gid='" + objODBCDatareader["business_head"].ToString() + "'," +
                           " businesshead_name='" + objODBCDatareader["businesshead_name"].ToString() + "'," +
                           " relationship_managergid='" + objODBCDatareader["relationship_manager"].ToString() + "'," +
                           " relationship_managername='" + objODBCDatareader["relationshipmgmt_name"].ToString() + "'," +
                           " cluster_managergid='" + objODBCDatareader["cluster_manager_gid"].ToString() + "'," +
                           " cluster_managername='" + objODBCDatareader["cluster_manager_name"].ToString() + "'," +
                           " ppa_gid='" + objODBCDatareader["ppa_gid"].ToString() + "'," +
                           " ppa_name='" + objODBCDatareader["ppa_name"].ToString() + "'," +
                           " creditmanager_gid='" + objODBCDatareader["creditmanager_gid"].ToString() + "'," +
                           " creditmanager_name='" + objODBCDatareader["creditmgmt_name"].ToString() + "'," +
                           " updated_by='" + employee_gid + "'," +
                           " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                           " where allocationdtl_gid='" + objvalues.allocationdtl_gid + "'";
                        objODBCDatareader.Close();
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                else
                {
                    objODBCDatareader.Close();
                    msGetGid = objcmnfunctions.GetMasterGID("ALCU");

                    msSQL = " select customername,customer_urn,contactperson,contact_no,address,address2,vertical_gid, " +
                            " vertical_code,zonal_head, zonal_name,business_head,businesshead_name,relationship_manager, " +
                            " relationshipmgmt_name, cluster_manager_gid,cluster_manager_name,creditmanager_gid,creditmgmt_name, " +
                            " ppa_gid,ppa_name from ocs_mst_tcustomer where customer_gid='" + objvalues.customer_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        msSQL = " Insert into rsk_trn_tallocatecustomerdtl( " +
                          " allocatecustomerdtl_gid," +
                          " allocationdtl_gid," +
                          " customer_gid," +
                          " customer_name," +
                          " customer_urn," +
                          " contact_person," +
                          " contact_no," +
                          " address_line1," +
                          " address_line2," +
                          " vertical_gid," +
                          " vertical_code," +
                          " zonalhead_gid," +
                          " zonalhead_name," +
                          " businesshead_gid," +
                          " businesshead_name," +
                          " relationship_managergid," +
                          " relationship_managername," +
                          " cluster_managergid," +
                          " cluster_managername," +
                          " creditmanager_gid," +
                          " creditmanager_name," +
                          " ppa_gid," +
                          " ppa_name," +
                          " created_by," +
                          " created_date)" +
                          " values ( " +
                          "'" + msGetGid + "'," +
                          "'" + objvalues.allocationdtl_gid + "'," +
                          "'" + objvalues.customer_gid + "'," +
                          "'" + objODBCDatareader["customername"].ToString() + "'," +
                          "'" + objODBCDatareader["customer_urn"].ToString() + "'," +
                          "'" + objODBCDatareader["contactperson"].ToString() + "'," +
                          "'" + objODBCDatareader["contact_no"].ToString() + "'," +
                          "'" + objODBCDatareader["address"].ToString() + "'," +
                          "'" + objODBCDatareader["address2"].ToString() + "'," +
                          "'" + objODBCDatareader["vertical_gid"].ToString() + "'," +
                          "'" + objODBCDatareader["vertical_code"].ToString() + "'," +
                          "'" + objODBCDatareader["zonal_head"].ToString() + "'," +
                          "'" + objODBCDatareader["zonal_name"].ToString() + "'," +
                          "'" + objODBCDatareader["business_head"].ToString() + "'," +
                          "'" + objODBCDatareader["businesshead_name"].ToString() + "'," +
                          "'" + objODBCDatareader["relationship_manager"].ToString() + "'," +
                          "'" + objODBCDatareader["relationshipmgmt_name"].ToString() + "'," +
                          "'" + objODBCDatareader["cluster_manager_gid"].ToString() + "'," +
                          "'" + objODBCDatareader["cluster_manager_name"].ToString() + "'," +
                          "'" + objODBCDatareader["creditmanager_gid"].ToString() + "'," +
                          "'" + objODBCDatareader["creditmgmt_name"].ToString() + "'," +
                          "'" + objODBCDatareader["ppa_gid"].ToString() + "'," +
                          "'" + objODBCDatareader["ppa_name"].ToString() + "'," +
                          "'" + employee_gid + "'," +
                          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        objODBCDatareader.Close();
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                }
                msSQL = " select customer2guarantor_gid,guarantors_name,guarantor_age,networth,basisofNW " +
                        " from ocs_mst_tcustomer2guarantor where customer_gid='" + objvalues.customer_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msSQL = " select allocateGuarantor_Gid from rsk_trn_tallocateguarantor " +
                                " where guarantor_gid='" + dt["customer2guarantor_gid"] + "' and allocationdtl_gid='" + objvalues.allocationdtl_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {

                            msSQL = " update rsk_trn_tallocateguarantor set " +
                                    " guarantors_name='" + dt["guarantors_name"].ToString() + "'," +
                                    " guarantor_age='" + dt["guarantor_age"].ToString() + "'," +
                                    " networth='" + dt["networth"].ToString() + "'," +
                                    " basisofNW='" + dt["basisofNW"].ToString() + "'," +
                                    " updated_by='" + employee_gid + "'," +
                                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                                    " where allocateGuarantor_Gid='" + objODBCDatareader["allocateGuarantor_Gid"].ToString() + "'";
                            objODBCDatareader.Close();
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        else
                        {
                            objODBCDatareader.Close();
                            msGetGurantorGid = objcmnfunctions.GetMasterGID("ALGR");
                            msSQL = " Insert into rsk_trn_tallocateguarantor( " +
                                      " allocateGuarantor_Gid," +
                                      " allocationdtl_gid," +
                                      " guarantor_gid," +
                                      " guarantors_name," +
                                      " guarantor_age," +
                                      " networth," +
                                      " basisofNW," +
                                      " created_by," +
                                      " created_date)" +
                                      " values(" +
                                      "'" + msGetGurantorGid + "', " +
                                      "'" + objvalues.allocationdtl_gid + "'," +
                                      "'" + dt["customer2guarantor_gid"].ToString() + "'," +
                                      "'" + dt["guarantors_name"].ToString() + "'," +
                                      "'" + dt["guarantor_age"].ToString() + "'," +
                                      "'" + dt["networth"].ToString() + "'," +
                                      "'" + dt["basisofNW"].ToString() + "'," +
                                      "'" + employee_gid + "'," +
                                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                }
                dt_datatable.Dispose();

                msSQL = " select customer2promotor_gid,promoter_name,designation,promoter_age,mobile " +
                        " from ocs_mst_tcustomer2promotor where customer_gid='" + objvalues.customer_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msSQL = " select allocatepromotor_Gid from rsk_trn_tallocatepromotors " +
                                " where promotor_gid='" + dt["customer2promotor_gid"] + "' and allocationdtl_gid='" + objvalues.allocationdtl_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {

                            msSQL = " update rsk_trn_tallocatepromotors set " +
                                    " promoter_name='" + dt["promoter_name"].ToString() + "'," +
                                    " designation='" + dt["designation"].ToString() + "'," +
                                    " promoter_age='" + dt["promoter_age"].ToString() + "'," +
                                    " mobile='" + dt["mobile"].ToString() + "'," +
                                    " updated_by='" + employee_gid + "'," +
                                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                                    " where allocatepromotor_Gid='" + objODBCDatareader["allocatepromotor_Gid"].ToString() + "'";
                            objODBCDatareader.Close();
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        else
                        {
                            objODBCDatareader.Close();
                            msGetPromotorGid = objcmnfunctions.GetMasterGID("ALPR");
                            msSQL = " Insert into rsk_trn_tallocatepromotors( " +
                                      " allocatepromotor_Gid," +
                                      " allocationdtl_gid," +
                                      " promotor_gid," +
                                      " promoter_name," +
                                      " designation," +
                                      " promoter_age," +
                                      " mobile," +
                                      " created_by," +
                                      " created_date)" +
                                      " values(" +
                                      "'" + msGetPromotorGid + "', " +
                                      "'" + objvalues.allocationdtl_gid + "'," +
                                      "'" + dt["customer2promotor_gid"].ToString() + "'," +
                                      "'" + dt["promoter_name"].ToString() + "'," +
                                      "'" + dt["designation"].ToString() + "'," +
                                      "'" + dt["promoter_age"].ToString() + "'," +
                                      "'" + dt["mobile"].ToString() + "'," +
                                      "'" + employee_gid + "'," +
                                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                }
                dt_datatable.Dispose();

                msSQL = " select customer2sanction_gid,sanction_refno,sanction_date,facility_type,sanction_limit,tenure_months, " +
                        " sanction_amount from ocs_mst_tcustomer2sanction where customer_gid='" + objvalues.customer_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msSQL = " select allocatesanctiondtl_Gid from rsk_trn_tallocatesanctiondtl " +
                                " where sanction_gid='" + dt["customer2sanction_gid"] + "' and allocationdtl_gid='" + objvalues.allocationdtl_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {

                            msSQL = " update rsk_trn_tallocatesanctiondtl set " +
                                    " sanction_refno='" + dt["sanction_refno"].ToString() + "'," +
                                    " facility_type='" + dt["facility_type"].ToString() + "'," +
                                    " sanction_date='" + dt["sanction_date"].ToString() + "',";
                            if (dt["sanction_amount"].ToString() != "")
                            {
                                msSQL += "sanction_amount='" + dt["sanction_amount"].ToString() + "',";
                            }
                            else
                            {
                                msSQL += "sanction_amount=null,";
                            }
                            msSQL += " sanction_limit='" + dt["sanction_limit"].ToString() + "'," +
                                    " tenure_months='" + dt["tenure_months"].ToString() + "'," +
                                    " updated_by='" + employee_gid + "'," +
                                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                                    " where allocatesanctiondtl_Gid='" + objODBCDatareader["allocatesanctiondtl_Gid"].ToString() + "'";
                            objODBCDatareader.Close();
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        else
                        {
                            objODBCDatareader.Close();
                            msGetSanctionGid = objcmnfunctions.GetMasterGID("ALSA");
                            msSQL = " Insert into rsk_trn_tallocatesanctiondtl( " +
                                      " allocatesanctiondtl_Gid," +
                                      " allocationdtl_gid," +
                                      " sanction_gid," +
                                      " sanction_refno," +
                                      " facility_type," +
                                      " sanction_date," +
                                      " sanction_amount," +
                                      " sanction_limit," +
                                      " tenure_months," +
                                      " created_by," +
                                      " created_date)" +
                                      " values(" +
                                      "'" + msGetSanctionGid + "', " +
                                      "'" + objvalues.allocationdtl_gid + "'," +
                                      "'" + dt["customer2sanction_gid"].ToString() + "'," +
                                      "'" + dt["sanction_refno"].ToString() + "'," +
                                      "'" + dt["facility_type"].ToString() + "'," +
                                      "'" + dt["sanction_date"].ToString() + "',";
                            if (dt["sanction_amount"].ToString() != "")
                            {
                                msSQL += "'" + dt["sanction_amount"].ToString() + "',";
                            }
                            else
                            {
                                msSQL += "null,";
                            }
                            msSQL += "'" + dt["sanction_limit"].ToString() + "'," +
                                      "'" + dt["tenure_months"].ToString() + "'," +
                                      "'" + employee_gid + "'," +
                                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }


                        msSQL = "select loanref_no,loan_gid,loan_title from ocs_trn_tloan where sanction_gid='" + dt["customer2sanction_gid"] + "'";
                        dr_datatable = objdbconn.GetDataTable(msSQL);
                        if (dr_datatable.Rows.Count != 0)
                        {
                            foreach (DataRow dr in dr_datatable.Rows)
                            {
                                msSQL = " select allocate_loangid from  rsk_trn_tallocateloan where loan_gid='" + dr["loan_gid"] + "' " +
                               " and allocationdtl_gid='" + objvalues.allocationdtl_gid + "'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    msSQL = " update rsk_trn_tallocateloan set " +
                                            " loanref_no='" + dr["loanref_no"].ToString() + "'," +
                                            " facility_type='" + dr["loan_title"].ToString() + "'," +
                                            " updated_by='" + employee_gid + "'," +
                                            " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                                            " where allocate_loangid='" + objODBCDatareader["allocate_loangid"].ToString() + "'";
                                    objODBCDatareader.Close();
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                                else
                                {
                                    objODBCDatareader.Close();
                                    string msgetGet_LoanGid = objcmnfunctions.GetMasterGID("ALLO");

                                    msSQL = " Insert into rsk_trn_tallocateloan( " +
                                              " allocate_loangid," +
                                              " allocationdtl_gid," +
                                              " sanction_gid," +
                                              " loan_gid," +
                                              " loanref_no," +
                                              " facility_type," +
                                              " created_by," +
                                              " created_date)" +
                                              " values(" +
                                              "'" + msgetGet_LoanGid + "', " +
                                              "'" + objvalues.allocationdtl_gid + "'," +
                                              "'" + dt["customer2sanction_gid"].ToString() + "'," +
                                              "'" + dr["loan_gid"].ToString() + "'," +
                                              "'" + dr["loanref_no"].ToString() + "'," +
                                             "'" + dr["loan_title"].ToString() + "'," +
                                             "'" + employee_gid + "'," +
                                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                            }
                        }
                        dr_datatable.Dispose();

                    }
                }
                dt_datatable.Dispose();

                msSQL = " select collateral_gid,security_type,security_description,account_status " +
                        " from ocs_trn_tcustomercollateral where customer_gid='" + objvalues.customer_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {

                        msSQL = "select allocatecollateral_gid from rsk_trn_tallocatecollateral " +
                                "where collateral_gid='" + dt["collateral_gid"].ToString() + "' and allocationdtl_gid='" + objvalues.allocationdtl_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            msSQL = " update rsk_trn_tallocatecollateral set " +
                                   " security_type='" + dt["security_type"].ToString() + "'," +
                                   " security_description='" + dt["security_description"].ToString() + "'," +
                                   " account_status='" + dt["account_status"].ToString() + "'," +
                                   " updated_by='" + employee_gid + "'," +
                                   " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                                   " where allocatecollateral_gid='" + objODBCDatareader["allocatecollateral_gid"].ToString() + "'";
                            objODBCDatareader.Close();
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        else
                        {
                            objODBCDatareader.Close();
                            msGetCollateralGid = objcmnfunctions.GetMasterGID("ALCL");

                            msSQL = " Insert into rsk_trn_tallocatecollateral( " +
                                      " allocatecollateral_gid," +
                                      " allocationdtl_gid," +
                                      " collateral_gid," +
                                      " security_type," +
                                      " security_description," +
                                      " account_status," +
                                      " created_by," +
                                      " created_date)" +
                                      " values(" +
                                      "'" + msGetCollateralGid + "', " +
                                      "'" + objvalues.allocationdtl_gid + "'," +
                                      "'" + dt["collateral_gid"].ToString() + "'," +
                                      "'" + dt["security_type"].ToString() + "'," +
                                      "'" + dt["security_description"].ToString() + "'," +
                                      "'" + dt["account_status"].ToString() + "'," +
                                      "'" + employee_gid + "'," +
                                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                }
                dt_datatable.Dispose();
            }
            if (mnResult != 0)
            {
                if (objvalues.allocationSubmit == "Y")
                {
                    objvalues.message = "Allocation Details are Submitted Successfully..!";
                }
                else
                {
                    objvalues.message = "Allocation Details are Saved Successfully..!";
                }
                objvalues.status = true;
                return true;
            }
            else
            {
                objvalues.status = false;
                objvalues.message = "Error Occured..!";
                return false;
            }
        }
        // rsk_trn_tallocationdtl //

        public bool DaGetAllocationSummary(mappingdtlList values)
        {
            msSQL = " select a.customer_gid,a.customer_urn,a.vertical_code,a.customername,b.allocationdtl_gid,b.allocation_status,b.allocate_external, " +
                   " concat(b.state_name,' / ',b.district_name) as allocatedLocation,b.allocation_flag, " +
                   " concat(f.user_firstname,' ',f.user_lastname,' / ',f.user_code) as assigned_RM, " +
                   " concat(h.user_firstname, ' ', h.user_lastname, ' / ', h.user_code) as ZonalRMname from ocs_mst_tcustomer a " +
                   " left join rsk_trn_tallocationdtl b on a.customer_gid = b.customer_gid " +
                   " left join hrm_mst_temployee e on e.employee_gid = b.allocation_assignedRM " +
                   " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
                   " left join hrm_mst_temployee g on g.employee_gid = b.allocation_zonalRM " +
                   " left join adm_mst_tuser h on h.user_gid = g.user_gid ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mappingdtl = new List<mappingdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_mappingdtl.Add(new mappingdtl
                    {
                        allocationdtl_gid = dt["allocationdtl_gid"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        customername = dt["customername"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical = dt["vertical_code"].ToString(),
                        location = dt["allocatedLocation"].ToString(),
                        assigned_RM = dt["assigned_RM"].ToString(),
                        ZonalRMname = dt["ZonalRMname"].ToString(),
                        allocation_flag = dt["allocation_flag"].ToString(),
                        allocation_status = dt["allocation_status"].ToString(),
                        allocate_external = dt["allocate_external"].ToString(),
                    });
                }
                values.mappingdtl = get_mappingdtl;
            }
            dt_datatable.Dispose();

            return true;
        }


        public bool DaGetNotAllocateSummary(mappingdtlList values)
        {
            msSQL = " select a.customer_gid,a.customer_urn,format(sum(f.sanction_amount), 2) as sanction_amount,a.vertical_code,a.customername, " +
                   " concat(k.zonal_name, ' / ', a.state, ' / ', a.district_name) as allocatedLocation, " +
                   " concat(h.user_firstname, ' ', h.user_lastname, ' / ', h.user_code) as assignedRM_name, " +
                   " concat(j.user_firstname, ' ', j.user_lastname, ' / ', j.user_code) as zonalRM_name, " +
                   " date_format(d.lastvisit_date, '%d-%m-%Y') as lastvisit_date,DATEDIFF(CURDATE(), d.lastvisit_date) as count_visit, " +
                   " case when(d.lastvisit_date is null) then date_format(c.first_disb_date, '%Y-%m-%d') else " +
                   " date_format(c.last_disb_date, '%Y-%m-%d') end as disbursementDate, " +
                   " date_format(d.lastvisit_date, '%Y-%m-%d') as lastvisitDate, " +
                   " case when(d.lastvisit_date is null) then date_format(c.first_disb_date, '%d-%m-%Y') else " +
                   " date_format(c.last_disb_date, '%d-%m-%Y')  end as disbursement_date, " +
                   " case when(d.lastvisit_date is null) then DATEDIFF(CURDATE(), c.first_disb_date) else " +
                   " DATEDIFF(CURDATE(), c.last_disb_date) end as daypassed_disbursement from ocs_mst_tcustomer a " +
                   " left join rsk_trn_tcustomerdisbursement c on a.customer_urn = c.customer_urn " +
                   " left join rsk_trn_tcustomervisit d on d.customer_gid = a.customer_gid " +
                   " left join ocs_mst_tcustomer2sanction f on f.customer_gid = a.customer_gid " +
                   " left join hrm_mst_temployee g on a.assigned_RM = g.employee_gid " +
                   " left join adm_mst_tuser h on g.user_gid = h.user_gid " +
                   " left join hrm_mst_temployee i on a.zonal_riskmanager = i.employee_gid " +
                   " left join adm_mst_tuser j on i.user_gid = j.user_gid " +
                   " left join rsk_mst_tzonalmapping k on k.zonalmapping_gid = a.zonal_gid " +
                   " where a.customer_gid not in (select customer_gid from rsk_trn_tallocation) group by customer_gid ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mappingdtl = new List<mappingdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    if (dt["disbursement_date"].ToString() != "" && dt["lastvisit_date"].ToString() != "")
                    {
                        if (Convert.ToDateTime(dt["disbursementDate"]) > Convert.ToDateTime(dt["lastvisitDate"]))
                        {
                            lsdisbursement_date = dt["disbursement_date"].ToString();
                            lsdaypassed_visit = dt["daypassed_disbursement"].ToString();
                        }
                        else
                        {
                            lsdisbursement_date = lsdaypassed_visit = "-";
                        }
                    }
                    else
                    {
                        lsdisbursement_date = dt["disbursement_date"].ToString();
                        lsdaypassed_visit = dt["daypassed_disbursement"].ToString();
                    }
                    get_mappingdtl.Add(new mappingdtl
                    {
                        //allocationdtl_gid = dt["allocationdtl_gid"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        customername = dt["customername"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical = dt["vertical_code"].ToString(),
                        location = dt["allocatedLocation"].ToString(),
                        assigned_RM = dt["assignedRM_name"].ToString(),
                        ZonalRMname = dt["zonalRM_name"].ToString(),
                        lastvisit_date = dt["lastvisit_date"].ToString(),
                        count_lastvisit = dt["count_visit"].ToString(),
                        disbursement_date = lsdisbursement_date,
                        daypassed_disbursement = lsdaypassed_visit,
                    });
                }
                values.mappingdtl = get_mappingdtl;
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaGetUpcomingAllocation(allocationlist values)
        {

            msSQL = " select a.customer_gid,f.customer_urn,format(a.sanction_amount, 2) as sanction_amount,f.vertical_code,f.customername, " +
                   " b.allocationdtl_gid,b.allocation_status,b.allocate_external,date_format(b.created_date, '%d-%m-%Y') as allocated_date, " +
                   " concat(e.zonal_name, ' / ', b.state_name, ' / ', b.district_name) as allocatedLocation,b.allocation_flag," +
                   " date_format(d.lastvisit_date, '%d-%m-%Y') as lastvisit_date,DATEDIFF(CURDATE(), d.lastvisit_date) as count_visit," +
                   " c.first_disb_date,c.last_disb_date,cast(monthname(visit_allocated_date) as char) as visitallocatemonth," +
                   " case when(d.lastvisit_date is null) then date_format(c.first_disb_date, '%d-%m-%Y') else" +
                   " date_format(c.last_disb_date, '%d-%m-%Y') end as disbursement_date," +
                   "  case when(d.lastvisit_date is null) then DATEDIFF(CURDATE(), c.first_disb_date) else" +
                   " DATEDIFF(CURDATE(), c.last_disb_date) end as daypassed_disbursement," +
                   "  case when(d.lastvisit_date is null) then date_format(c.first_disb_date, '%Y-%m-%d') else" +
                   "  date_format(c.last_disb_date, '%Y-%m-%d') end as disbursementDate," +
                   "  date_format(d.lastvisit_date, '%Y-%m-%d') as lastvisitDate," +
                   " b.assignedRM_name,b.zonalRM_name from rsk_trn_tallocation a" +
                   " left join rsk_trn_tallocationdtl b on a.allocation_gid = b.allocation_gid " +
                   " left join rsk_trn_tcustomervisit d on d.customer_gid = a.customer_gid" +
                   " left join rsk_mst_tzonalmapping e on b.zonal_gid = e.zonalmapping_gid" +
                   " left join ocs_mst_tcustomer f on f.customer_gid = a.customer_gid" +
                   " left join rsk_trn_tcustomerdisbursement c on f.customer_urn = c.customer_urn" +
                   " where b.allocation_status = 'Allocated' and visit_allocated_date > NOW() group by a.customer_gid ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mappingdtl = new List<allocationdtl>();
            values.count_upcoming = dt_datatable.Rows.Count;
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    if (dt["disbursement_date"].ToString() != "" && dt["lastvisit_date"].ToString() != "")
                    {
                        if (Convert.ToDateTime(dt["disbursementDate"]) > Convert.ToDateTime(dt["lastvisitDate"]))
                        {
                            lsdisbursement_date = dt["disbursement_date"].ToString();
                            lsdaypassed_visit = dt["daypassed_disbursement"].ToString();
                        }
                        else
                        {
                            lsdisbursement_date = lsdaypassed_visit = "-";
                        }
                    }
                    else
                    {
                        lsdisbursement_date = dt["disbursement_date"].ToString();
                        lsdaypassed_visit = dt["daypassed_disbursement"].ToString();
                    }

                    get_mappingdtl.Add(new allocationdtl
                    {
                        allocated_date = dt["allocated_date"].ToString(),
                        allocationdtl_gid = dt["allocationdtl_gid"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        customername = dt["customername"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical = dt["vertical_code"].ToString(),
                        location = dt["allocatedLocation"].ToString(),
                        assigned_RM = dt["assignedRM_name"].ToString(),
                        ZonalRMname = dt["zonalRM_name"].ToString(),
                        allocation_flag = dt["allocation_flag"].ToString(),
                        allocation_status = dt["allocation_status"].ToString(),
                        allocate_external = dt["allocate_external"].ToString(),
                        lastvisit_date = dt["lastvisit_date"].ToString(),
                        count_lastvisit = dt["count_visit"].ToString(),
                        disbursement_date = lsdisbursement_date,
                        daypassed_disbursement = lsdaypassed_visit,
                        visit_allocatemonth = dt["visitallocatemonth"].ToString(),
                        sanction_amount = dt["sanction_amount"].ToString(),

                    });
                }
                values.allocationdtl = get_mappingdtl;
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaGetCurrentAllocateSummary(allocationlist values)
        {

            msSQL = " select a.customer_gid,f.customer_urn,format(a.sanction_amount, 2) as sanction_amount,f.vertical_code,f.customername, " +
                   " b.allocationdtl_gid,b.allocation_status,b.allocate_external,date_format(b.created_date, '%d-%m-%Y') as allocated_date, " +
                   " concat(e.zonal_name, ' / ', b.state_name, ' / ', b.district_name) as allocatedLocation,b.allocation_flag," +
                   " date_format(d.lastvisit_date, '%d-%m-%Y') as lastvisit_date,DATEDIFF(CURDATE(), d.lastvisit_date) as count_visit," +
                   " c.first_disb_date,c.last_disb_date,cast(monthname(visit_allocated_date) as char) as visitallocatemonth," +
                   " case when(d.lastvisit_date is null) then date_format(c.first_disb_date, '%d-%m-%Y') else" +
                   " date_format(c.last_disb_date, '%d-%m-%Y') end as disbursement_date," +
                   "  case when(d.lastvisit_date is null) then DATEDIFF(CURDATE(), c.first_disb_date) else" +
                   " DATEDIFF(CURDATE(), c.last_disb_date) end as daypassed_disbursement," +
                   "  case when(d.lastvisit_date is null) then date_format(c.first_disb_date, '%Y-%m-%d') else" +
                   "  date_format(c.last_disb_date, '%Y-%m-%d') end as disbursementDate," +
                   "  date_format(d.lastvisit_date, '%Y-%m-%d') as lastvisitDate, " +
                   " date_format(cast(date_add(date_format(b.created_date,'%Y-%m-%d'),INTERVAL 60 DAY) as date), '%d-%m-%Y')as cutoff_date ," +
                   " b.assignedRM_name,b.zonalRM_name, b.Manual_Allocation from rsk_trn_tallocation a" +
                   " left join rsk_trn_tallocationdtl b on a.allocation_gid = b.allocation_gid " +
                   " left join rsk_trn_tcustomervisit d on d.customer_gid = a.customer_gid" +
                   " left join rsk_mst_tzonalmapping e on b.zonal_gid = e.zonalmapping_gid" +
                   " left join ocs_mst_tcustomer f on f.customer_gid = a.customer_gid" +
                   " left join rsk_trn_tcustomerdisbursement c on f.customer_urn = c.customer_urn" +
                   " where b.allocation_status = 'Allocated' and visit_allocated_date <= NOW() group by a.customer_gid ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mappingdtl = new List<allocationdtl>();
            values.count_currentallo = dt_datatable.Rows.Count;
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    if (dt["disbursement_date"].ToString() != "" && dt["lastvisit_date"].ToString() != "")
                    {
                        if (Convert.ToDateTime(dt["disbursementDate"]) > Convert.ToDateTime(dt["lastvisitDate"]))
                        {
                            lsdisbursement_date = dt["disbursement_date"].ToString();
                            lsdaypassed_visit = dt["daypassed_disbursement"].ToString();
                        }
                        else
                        {
                            lsdisbursement_date = lsdaypassed_visit = "-";
                        }
                    }
                    else
                    {
                        lsdisbursement_date = dt["disbursement_date"].ToString();
                        lsdaypassed_visit = dt["daypassed_disbursement"].ToString();
                    }

                    get_mappingdtl.Add(new allocationdtl
                    {
                        allocated_date = dt["allocated_date"].ToString(),
                        allocationdtl_gid = dt["allocationdtl_gid"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        customername = dt["customername"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical = dt["vertical_code"].ToString(),
                        location = dt["allocatedLocation"].ToString(),
                        assigned_RM = dt["assignedRM_name"].ToString(),
                        ZonalRMname = dt["zonalRM_name"].ToString(),
                        allocation_flag = dt["allocation_flag"].ToString(),
                        allocation_status = dt["allocation_status"].ToString(),
                        allocate_external = dt["allocate_external"].ToString(),
                        lastvisit_date = dt["lastvisit_date"].ToString(),
                        count_lastvisit = dt["count_visit"].ToString(),
                        disbursement_date = lsdisbursement_date,
                        daypassed_disbursement = lsdaypassed_visit,
                        visit_allocatemonth = dt["visitallocatemonth"].ToString(),
                        sanction_amount = dt["sanction_amount"].ToString(),
                        cutoff_date = dt["cutoff_date"].ToString(),
                        Manual_Allocation = dt["Manual_Allocation"].ToString(),
                    });
                }
                values.allocationdtl = get_mappingdtl;
            }
            dt_datatable.Dispose();


            return true;
        }

        public bool DaGetcompletedAllocationSummary(allocationlist values)
        {

            msSQL = " select a.customer_gid,f.customer_urn,format(a.sanction_amount, 2) as sanction_amount,f.vertical_code,f.customername, " +
                   " b.allocationdtl_gid,b.allocation_status,b.allocate_external, " +
                   " date_format(b.created_date,'%d-%m-%Y') as allocated_date,cast(monthname(visit_allocated_date) as char) as visitallocatemonth," +
                   " concat(e.zonal_name, ' / ', b.state_name, ' / ', b.district_name) as allocatedLocation,b.allocation_flag," +
                   " date_format(d.lastvisit_date, '%d-%m-%Y') as lastvisit_date,DATEDIFF(CURDATE(), d.lastvisit_date) as count_visit," +
                   " c.first_disb_date,c.last_disb_date," +
                   " case when(d.lastvisit_date is null) then date_format(c.first_disb_date, '%d-%m-%Y') else" +
                   " date_format(c.last_disb_date, '%d-%m-%Y') end as disbursement_date," +
                   "  case when(d.lastvisit_date is null) then DATEDIFF(CURDATE(), c.first_disb_date) else" +
                   " DATEDIFF(CURDATE(), c.last_disb_date) end as daypassed_disbursement," +
                   "  case when(d.lastvisit_date is null) then date_format(c.first_disb_date, '%Y-%m-%d') else" +
                   "  date_format(c.last_disb_date, '%Y-%m-%d') end as disbursementDate," +
                   "  date_format(d.lastvisit_date, '%Y-%m-%d') as lastvisitDate," +
                   " b.assignedRM_name,b.zonalRM_name from rsk_trn_tallocation a" +
                   " left join rsk_trn_tallocationdtl b on a.allocation_gid = b.allocation_gid " +
                   " left join rsk_trn_tcustomervisit d on d.customer_gid = a.customer_gid" +
                   " left join rsk_mst_tzonalmapping e on b.zonal_gid = e.zonalmapping_gid" +
                   " left join ocs_mst_tcustomer f on f.customer_gid = a.customer_gid" +
                   " left join rsk_trn_tcustomerdisbursement c on f.customer_urn = c.customer_urn" +
                   " where a.status = 'Completed' group by a.customer_gid ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mappingdtl = new List<allocationdtl>();
            values.count_completedallo = dt_datatable.Rows.Count;
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    if (dt["disbursement_date"].ToString() != "" && dt["lastvisit_date"].ToString() != "")
                    {
                        if (Convert.ToDateTime(dt["disbursementDate"]) > Convert.ToDateTime(dt["lastvisitDate"]))
                        {
                            lsdisbursement_date = dt["disbursement_date"].ToString();
                            lsdaypassed_visit = dt["daypassed_disbursement"].ToString();
                        }
                        else
                        {
                            lsdisbursement_date = lsdaypassed_visit = "-";
                        }
                    }
                    else
                    {
                        lsdisbursement_date = dt["disbursement_date"].ToString();
                        lsdaypassed_visit = dt["daypassed_disbursement"].ToString();
                    }

                    get_mappingdtl.Add(new allocationdtl
                    {
                        allocationdtl_gid = dt["allocationdtl_gid"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        customername = dt["customername"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical = dt["vertical_code"].ToString(),
                        location = dt["allocatedLocation"].ToString(),
                        assigned_RM = dt["assignedRM_name"].ToString(),
                        ZonalRMname = dt["zonalRM_name"].ToString(),
                        allocation_flag = dt["allocation_flag"].ToString(),
                        allocation_status = dt["allocation_status"].ToString(),
                        allocate_external = dt["allocate_external"].ToString(),
                        lastvisit_date = dt["lastvisit_date"].ToString(),
                        count_lastvisit = dt["count_visit"].ToString(),
                        disbursement_date = lsdisbursement_date,
                        daypassed_disbursement = lsdaypassed_visit,
                        sanction_amount = dt["sanction_amount"].ToString(),
                        allocated_date = dt["allocated_date"].ToString(),
                        visit_allocatemonth = dt["visitallocatemonth"].ToString(),
                    });
                }
                values.allocationdtl = get_mappingdtl;
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaGetExternalAllocationSummary(allocationlist values)
        {
            msSQL = " select b.allocate_externalname,a.customer_gid,f.customer_urn,format(a.sanction_amount, 2) as sanction_amount, " +
                   " f.vertical_code,f.customername,date_format(b.target_date, '%d-%m-%Y') as target_date, " +
                   " date_format(b.created_date, '%d-%m-%Y') as allocate_date, cast(monthname(visit_allocated_date) as char) as visitallocatemonth, " +
                   " b.allocationdtl_gid,b.allocation_status,b.allocate_external, " +
                   " concat(e.zonal_name, ' / ', b.state_name, ' / ', b.district_name) as allocatedLocation,b.allocation_flag," +
                   " date_format(d.lastvisit_date, '%d-%m-%Y') as lastvisit_date,DATEDIFF(CURDATE(), d.lastvisit_date) as count_visit," +
                   " case when(d.lastvisit_date is null) then date_format(c.first_disb_date, '%d-%m-%Y') else" +
                   " date_format(c.last_disb_date, '%d-%m-%Y') end as disbursement_date," +
                   "  case when(d.lastvisit_date is null) then DATEDIFF(CURDATE(), c.first_disb_date) else" +
                   " DATEDIFF(CURDATE(), c.last_disb_date) end as daypassed_disbursement," +
                   "  case when(d.lastvisit_date is null) then date_format(c.first_disb_date, '%Y-%m-%d') else" +
                   "  date_format(c.last_disb_date, '%Y-%m-%d') end as disbursementDate," +
                   "  date_format(d.lastvisit_date, '%Y-%m-%d') as lastvisitDate," +
                   " b.assignedRM_name,b.zonalRM_name from rsk_trn_tallocation a" +
                   " left join rsk_trn_tallocationdtl b on a.allocation_gid = b.allocation_gid " +
                   " left join rsk_trn_tcustomervisit d on d.customer_gid = a.customer_gid" +
                   " left join rsk_mst_tzonalmapping e on b.zonal_gid = e.zonalmapping_gid" +
                   " left join ocs_mst_tcustomer f on f.customer_gid = a.customer_gid" +
                   " left join rsk_trn_tcustomerdisbursement c on f.customer_urn = c.customer_urn" +
                   " where b.allocation_status = 'External' group by a.customer_gid ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mappingdtl = new List<allocationdtl>();
            values.count_external = dt_datatable.Rows.Count;
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    if (dt["disbursement_date"].ToString() != "" && dt["lastvisit_date"].ToString() != "")
                    {
                        if (Convert.ToDateTime(dt["disbursementDate"]) > Convert.ToDateTime(dt["lastvisitDate"]))
                        {
                            lsdisbursement_date = dt["disbursement_date"].ToString();
                            lsdaypassed_visit = dt["daypassed_disbursement"].ToString();
                        }
                        else
                        {
                            lsdisbursement_date = lsdaypassed_visit = "-";
                        }
                    }
                    else
                    {
                        lsdisbursement_date = dt["disbursement_date"].ToString();
                        lsdaypassed_visit = dt["daypassed_disbursement"].ToString();
                    }

                    get_mappingdtl.Add(new allocationdtl
                    {
                        allocationdtl_gid = dt["allocationdtl_gid"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        customername = dt["customername"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical = dt["vertical_code"].ToString(),
                        location = dt["allocatedLocation"].ToString(),
                        allocate_externalname = dt["allocate_externalname"].ToString(),
                        allocation_flag = dt["allocation_flag"].ToString(),
                        allocation_status = dt["allocation_status"].ToString(),
                        allocate_external = dt["allocate_external"].ToString(),
                        lastvisit_date = dt["lastvisit_date"].ToString(),
                        count_lastvisit = dt["count_visit"].ToString(),
                        disbursement_date = lsdisbursement_date,
                        daypassed_disbursement = lsdaypassed_visit,
                        sanction_amount = dt["sanction_amount"].ToString(),
                        target_date = dt["target_date"].ToString(),
                        allocated_date = dt["allocate_date"].ToString(),
                        visit_allocatemonth = dt["visitallocatemonth"].ToString(),
                    });
                }
                values.allocationdtl = get_mappingdtl;
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaGetAllocationViewdtl(string allocationdtl_gid, mappingdtl values)
        {

            msSQL = " select f.zonal_name,a.state_name,a.district_name,a.customer_gid,g.relationship_managergid, " +
                   " g.relationship_managername,case when g.ppa_gid = '' then 'NA' else g.ppa_name end as ppa_name, " +
                   " g.customer_name,g.customer_urn,g.creditmanager_gid,g.creditmanager_name, " +
                   " a.allocation_assignedRM,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as assigedRMname, " +
                   " concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as assigedZonalRM,a.allocation_status from rsk_trn_tallocationdtl a " +
                   " left join hrm_mst_temployee b on a.allocation_assignedRM = b.employee_gid " +
                   " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                   " left join hrm_mst_temployee d on d.employee_gid = a.allocation_zonalRM " +
                   " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                   " left join rsk_mst_tzonalmapping f on a.zonal_gid=f.zonalmapping_gid " +
                   " left join rsk_trn_tallocatecustomerdtl g on g.allocationdtl_gid=a.allocationdtl_gid " +
                   " where a.allocationdtl_gid = '" + allocationdtl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.customername = objODBCDatareader["customer_name"].ToString();
                values.customer_urn = objODBCDatareader["customer_urn"].ToString();
                values.credit_managername = objODBCDatareader["creditmanager_name"].ToString();
                values.creditmanager_gid = objODBCDatareader["creditmanager_gid"].ToString();
                values.state_name = objODBCDatareader["state_name"].ToString();
                values.district_name = objODBCDatareader["district_name"].ToString();
                values.assigned_RM = objODBCDatareader["assigedRMname"].ToString();
                values.customer_gid = objODBCDatareader["customer_gid"].ToString();
                values.ZonalRMname = objODBCDatareader["assigedZonalRM"].ToString();
                values.allocation_status = objODBCDatareader["allocation_status"].ToString();
                values.zonal_name = objODBCDatareader["zonal_name"].ToString();
                values.assignedRM_gid = objODBCDatareader["allocation_assignedRM"].ToString();
                values.PPA_name = objODBCDatareader["ppa_name"].ToString();
                values.relationship_managername = objODBCDatareader["relationship_managername"].ToString();
                values.relationship_managerGid = objODBCDatareader["relationship_managergid"].ToString();
            }
            objODBCDatareader.Close();

            msSQL= " select format(sum(sanction_amount),2) as sanction_amount from rsk_trn_tallocatesanctiondtl " +
                    " where allocationdtl_gid = '" + allocationdtl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.sanction_amount = objODBCDatareader["sanction_amount"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select a.transferFrom_statename,a.transferFrom_districtname,transferFrom_name, " +
                  "  transferfrom_zonalRMname from rsk_trn_tallocationtransfer a " +
                  " where a.allocationdtl_gid = '" + allocationdtl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.transferFrom_statename = objODBCDatareader["transferFrom_statename"].ToString();
                values.transferFrom_districtname = objODBCDatareader["transferFrom_districtname"].ToString();
                values.transferfrom_assignedRM = objODBCDatareader["transferFrom_name"].ToString();
                values.transferFrom_ZonalRMname = objODBCDatareader["transferfrom_zonalRMname"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select a.customername,a.customer_urn,constitution_name,a.creditmgmt_name,a.relationshipmgmt_name, " +
                    " format(sum(b.sanction_amount),2) as sanction_amount,format(total_disbamount,2) as totaldisb_amount," +
                    " a.relationship_manager,a.creditmanager_gid,date_format(c.last_disb_date, '%Y-%m-%d') as lastdisb_date," +
                    " date_format(c.first_disb_date, '%Y-%m-%d') as firstdisb_date from ocs_mst_tcustomer a " +
                    " left join ocs_mst_tcustomer2sanction b on a.customer_gid=b.customer_gid " +
                    " left join rsk_trn_tcustomerdisbursement c on a.customer_urn=c.customer_urn " +
                    " where a.customer_gid='" + values.customer_gid + "' group by b.customer_gid";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.constitution = objODBCDatareader["constitution_name"].ToString();
                values.firstdisb_date = objODBCDatareader["firstdisb_date"].ToString();
                values.last_disb_date = objODBCDatareader["lastdisb_date"].ToString();
                values.totaldisb_amount = objODBCDatareader["totaldisb_amount"].ToString();
            }
            objODBCDatareader.Close();

            return true;
        }

        public bool DaGetCreateAllocatedtls(string customer_gid, customerdetail values)
        {
            msSQL = " select a.customername,a.zonal_gid,f.zonal_name,a.state,state_gid,district_gid,district_name,zonal_riskmanager,assigned_RM, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as assignedRM, " +
                    " concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as  zonalRM " +
                    " from ocs_mst_tcustomer a" +
                    " left join hrm_mst_temployee b on a.assigned_RM=b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join hrm_mst_temployee d on a.zonal_riskmanager=d.employee_gid " +
                    " left join adm_mst_tuser e on e.user_gid=d.user_gid " +
                    " left join rsk_mst_tzonalmapping f on f.zonalmapping_gid=a.zonal_gid " +
                    " where a.customer_gid='" + customer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.customername = objODBCDatareader["customername"].ToString();
                values.zonal_gid = objODBCDatareader["zonal_gid"].ToString();
                values.zonal_name = objODBCDatareader["zonal_name"].ToString();
                values.state_name = objODBCDatareader["state"].ToString();
                values.state_gid = objODBCDatareader["state_gid"].ToString();
                values.district_gid = objODBCDatareader["district_gid"].ToString();
                values.district_name = objODBCDatareader["district_name"].ToString();
                values.ZonalRM_gid = objODBCDatareader["zonal_riskmanager"].ToString();
                values.ZonalRMname = objODBCDatareader["zonalRM"].ToString();
                values.assignedRM_gid = objODBCDatareader["assigned_RM"].ToString();
                values.assigned_RM = objODBCDatareader["assignedRM"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select assignedRM_name,date_format(lastvisit_date,'%d-%m-%Y') as lastvisit_date from rsk_trn_tallocationdtl " +
                  " where customer_gid = '" + customer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_customerlastvisit = new List<customerlastvisit>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_customerlastvisit.Add(new customerlastvisit
                    {
                        assignedRM_name = dr_datarow["assignedRM_name"].ToString(),
                        lastvisit_date = (dr_datarow["lastvisit_date"].ToString()),
                    });
                }
                values.customerlastvisit = get_customerlastvisit;
                values.qualified_status = "Re-Visit";
            }
            else
            {
                values.qualified_status = "Fresh";
            }
            dt_datatable.Dispose();


            return true;

        }

        public bool DaGetAllocatedRMList(mappingdtlList values)
        {
            msSQL = " select allocation_assignedRM,state_name,district_name,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as allocated_RM, " +
                   " count(a.allocation_assignedRM) as count_RMassigned from rsk_trn_tallocationdtl a " +
                   " left join hrm_mst_temployee b on a.allocation_assignedRM = b.employee_gid " +
                   " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                   " where allocation_assignedRM <> '' and (allocation_flag='Y' or allocation_flag='C')  group by a.allocation_assignedRM";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mappingdtl = new List<mappingdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_mappingdtl.Add(new mappingdtl
                    {
                        state_name = dt["state_name"].ToString(),
                        district_name = dt["district_name"].ToString(),
                        assigned_RM = dt["allocated_RM"].ToString(),
                        count_RMassigned = dt["count_RMassigned"].ToString(),
                        assignedRM_gid = dt["allocation_assignedRM"].ToString(),
                    });
                }
                values.mappingdtl = get_mappingdtl;
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaGetRMcurrentallocateddtl(string assignedRM_gid, rmallocationlist values)
        {
            msSQL = " select a.allocationdtl_gid,a.customer_gid,b.customer_name,b.customer_urn,a.state_name, a.completed_flag," +
                    " a.district_name,concat(f.user_firstname,' ',f.user_lastname,' / ',f.user_code) as assigedRMname,a.allocation_status, " +
                    " concat(h.user_firstname,' ',h.user_lastname,' / ',h.user_code) as allocated_by, a.visit_status," +
                    " concat(j.user_firstname,' ',j.user_lastname,' / ',j.user_code) as allocated_Zonal, " +
                    " cast(monthname(visit_allocated_date) as char) as visitallocatemonth,date_format(a.created_date,'%d-%m-%Y') as allocated_date," +
                    " date_format(k.lastvisit_date, '%d-%m-%Y') as lastvisit_date,DATEDIFF(CURDATE(), k.lastvisit_date) as count_visit" +
                    " from rsk_trn_tallocationdtl a " +
                    " left join rsk_trn_tallocatecustomerdtl b on a.allocationdtl_gid = b.allocationdtl_gid " +
                    " left join hrm_mst_temployee e on e.employee_gid=a.allocation_assignedRM" +
                    " left join adm_mst_tuser f on f.user_gid=e.user_gid " +
                    " left join hrm_mst_temployee g on g.employee_gid = a.allocated_by" +
                    " left join adm_mst_tuser h on h.user_gid=g.user_gid " +
                    " left join hrm_mst_temployee i on i.employee_gid = a.allocation_zonalRM " +
                    " left join adm_mst_tuser j on i.user_gid = j.user_gid " +
                    " left join rsk_trn_tcustomervisit k on k.customer_gid = a.customer_gid" +
                    " where a.allocation_assignedRM='" + assignedRM_gid + "' and visit_allocated_date <= NOW()" +
                    " and (a.allocation_flag='Y') order by allocationdtl_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mappingdtl = new List<rmallocation>();
            values.count_current = dt_datatable.Rows.Count;
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_mappingdtl.Add(new rmallocation
                    {
                        allocationdtl_gid = dt["allocationdtl_gid"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        customername = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        state_name = dt["state_name"].ToString(),
                        district_name = dt["district_name"].ToString(),
                        assigned_RM = dt["assigedRMname"].ToString(),
                        created_by = dt["allocated_by"].ToString(),
                        created_date = dt["allocated_date"].ToString(),
                        allocation_status = dt["allocation_status"].ToString(),
                        ZonalRMname = dt["allocated_Zonal"].ToString(),
                        completed_flag = dt["completed_flag"].ToString(),
                        visit_status = dt["visit_status"].ToString(),
                        visit_allocatemonth = dt["visitallocatemonth"].ToString(),
                        lastvisit_date = dt["lastvisit_date"].ToString(),
                        count_lastvisit = dt["count_visit"].ToString(),
                    });
                }
                values.rmallocation = get_mappingdtl;
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaGetRMCompleteddetails(string assignedRM_gid, rmallocationlist values)
        {
            msSQL = " select c.observation_flag,c.observation_reportgid,a.allocationdtl_gid,a.customer_gid,b.customer_name,b.customer_urn,a.state_name, a.completed_flag," +
                    " a.district_name,a.allocation_status,a.visit_status,cast(monthname(a.visit_allocated_date) as char) as visitallocatemonth," +
                    " concat(h.user_firstname,' ',h.user_lastname,' / ',h.user_code) as allocated_by, d.tier1_approvalstatus," +
                    " date_format(a.created_date,'%d-%m-%Y') as allocated_date,a.assignedRM_name,a.zonalRM_name,a.tier3_status, " +
                    " e.tier3_flag,e.tier2_approvedflag, " +
                    " date_format(k.lastvisit_date, '%d-%m-%Y') as lastvisit_date,DATEDIFF(CURDATE(), k.lastvisit_date) as count_visit, a.ATR_flag" +
                    " from rsk_trn_tallocationdtl a " +
                    " left join rsk_trn_tallocatecustomerdtl b on a.allocationdtl_gid = b.allocationdtl_gid " +
                    " left join rsk_trn_tobservationreport c on a.allocationdtl_gid = c.allocationdtl_gid " +
                    " left join rsk_trn_ttier1format d on c.observation_reportgid = d.observation_reportgid " +
                    " left join rsk_trn_ttierallocationdtl e on e.allocationdtl_gid= a.allocationdtl_gid " +
                    " left join hrm_mst_temployee g on g.employee_gid = a.allocated_by" +
                    " left join adm_mst_tuser h on h.user_gid=g.user_gid " +
                    " left join rsk_trn_tcustomervisit k on k.customer_gid = a.customer_gid" +
                    " where a.allocation_assignedRM='" + assignedRM_gid + "' and (a.allocation_flag='C' and a.allocate_external is null) order by allocationdtl_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mappingdtl = new List<rmallocation>();
            values.count_completed = dt_datatable.Rows.Count;
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_mappingdtl.Add(new rmallocation
                    {
                        allocationdtl_gid = dt["allocationdtl_gid"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        customername = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        state_name = dt["state_name"].ToString(),
                        district_name = dt["district_name"].ToString(),
                        assigned_RM = dt["assignedRM_name"].ToString(),
                        created_by = dt["allocated_by"].ToString(),
                        created_date = dt["allocated_date"].ToString(),
                        allocation_status = dt["allocation_status"].ToString(),
                        ZonalRMname = dt["zonalRM_name"].ToString(),
                        visit_allocatemonth = dt["visitallocatemonth"].ToString(),
                        completed_flag = dt["completed_flag"].ToString(),
                        visit_status = dt["visit_status"].ToString(),
                        observation_flag = dt["observation_flag"].ToString(),
                        observation_reportgid = dt["observation_reportgid"].ToString(),
                        tier1_approvalstatus = dt["tier1_approvalstatus"].ToString(),
                        tier3_status = dt["tier3_status"].ToString(),
                        tier2_flag = dt["tier2_approvedflag"].ToString(),
                        tier3_flag = dt["tier3_flag"].ToString(),
                        lastvisit_date = dt["lastvisit_date"].ToString(),
                        count_lastvisit = dt["count_visit"].ToString(),
                        ATR_flag = dt["ATR_flag"].ToString(),
                    });
                }
                values.rmallocation = get_mappingdtl;
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaGetRMExclusiondetails(string assignedRM_gid, exclusionAllocationlist values)
        {
            msSQL = " select c.observation_flag,c.observation_reportgid,a.allocationdtl_gid,a.customer_gid,b.customer_name,b.customer_urn,a.state_name, a.completed_flag," +
                    " a.district_name,a.allocation_status,a.visit_status,cast(monthname(a.visit_allocated_date) as char) as visitallocatemonth," +
                    " concat(h.user_firstname,' ',h.user_lastname,' / ',h.user_code) as allocated_by," +
                    " date_format(a.created_date,'%d-%m-%Y') as allocated_date,a.assignedRM_name,a.zonalRM_name, " +
                    " date_format(k.lastvisit_date, '%d-%m-%Y') as lastvisit_date,DATEDIFF(CURDATE(), k.lastvisit_date) as count_visit" +
                    " from rsk_trn_tallocationdtl a " +
                    " left join rsk_trn_tallocatecustomerdtl b on a.allocationdtl_gid = b.allocationdtl_gid " +
                    " left join rsk_trn_tobservationreport c on a.allocationdtl_gid = c.allocationdtl_gid " +
                    " left join hrm_mst_temployee g on g.employee_gid = a.allocated_by" +
                    " left join adm_mst_tuser h on h.user_gid=g.user_gid " +
                    " left join rsk_trn_tcustomervisit k on k.customer_gid = a.customer_gid" +
                    " where a.allocation_assignedRM='" + assignedRM_gid + "' and " +
                    " (a.allocation_excludedflag ='Y') order by allocationdtl_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mappingdtl = new List<exclusionAllocation>();
            values.count_exclusion = dt_datatable.Rows.Count;
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_mappingdtl.Add(new exclusionAllocation
                    {
                        allocationdtl_gid = dt["allocationdtl_gid"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        customername = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        state_name = dt["state_name"].ToString(),
                        district_name = dt["district_name"].ToString(),
                        assigned_RM = dt["assignedRM_name"].ToString(),
                        created_by = dt["allocated_by"].ToString(),
                        created_date = dt["allocated_date"].ToString(),
                        allocation_status = dt["allocation_status"].ToString(),
                        ZonalRMname = dt["zonalRM_name"].ToString(),
                        visit_allocatemonth = dt["visitallocatemonth"].ToString(),
                        lastvisit_date = dt["lastvisit_date"].ToString(),
                        count_lastvisit = dt["count_visit"].ToString(),
                    });
                }
                values.exclusionAllocation = get_mappingdtl;
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaGetRMupcomingallocateddtl(string assignedRM_gid, rmallocationlist values)
        {
            msSQL = " select a.allocationdtl_gid,a.customer_gid,b.customer_name,b.customer_urn,a.state_name, a.completed_flag," +
                    " a.district_name,a.allocation_status,cast(monthname(visit_allocated_date) as char) as visitallocatemonth, " +
                    " concat(h.user_firstname,' ',h.user_lastname,' / ',h.user_code) as allocated_by, " +
                    " date_format(a.created_date,'%d-%m-%Y') as allocated_date," +
                    " a.assignedRM_name,a.zonalRM_name, " +
                    " date_format(k.lastvisit_date, '%d-%m-%Y') as lastvisit_date,DATEDIFF(CURDATE(), k.lastvisit_date) as count_visit" +
                    " from rsk_trn_tallocationdtl a " +
                    " left join rsk_trn_tallocatecustomerdtl b on a.allocationdtl_gid = b.allocationdtl_gid " +
                    " left join hrm_mst_temployee g on g.employee_gid = a.allocated_by" +
                    " left join adm_mst_tuser h on h.user_gid=g.user_gid " +
                    " left join rsk_trn_tcustomervisit k on k.customer_gid = a.customer_gid" +
                    " where a.allocation_assignedRM='" + assignedRM_gid + "' and month(visit_allocated_date)= month(NOW() + interval 1 month) " +
                    " and (a.allocation_flag='Y') order by allocationdtl_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mappingdtl = new List<rmallocation>();
            values.count_upcoming = dt_datatable.Rows.Count;
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_mappingdtl.Add(new rmallocation
                    {
                        allocationdtl_gid = dt["allocationdtl_gid"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        customername = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        state_name = dt["state_name"].ToString(),
                        district_name = dt["district_name"].ToString(),
                        assigned_RM = dt["assignedRM_name"].ToString(),
                        created_by = dt["allocated_by"].ToString(),
                        created_date = dt["allocated_date"].ToString(),
                        allocation_status = dt["allocation_status"].ToString(),
                        ZonalRMname = dt["zonalRM_name"].ToString(),
                        visit_allocatemonth = dt["visitallocatemonth"].ToString(),
                        completed_flag = dt["completed_flag"].ToString(),
                        lastvisit_date = dt["lastvisit_date"].ToString(),
                        count_lastvisit = dt["count_visit"].ToString(),
                    });
                }
                values.rmallocation = get_mappingdtl;
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaPostAllocationTransfer(allocationTransfer values, string employee_gid)
        {
            //var count = 0;
            //foreach (string i in values.allocationdtl_gid)
            //{

            msSQL = " SELECT allocation_assignedRM,state_gid,district_gid from rsk_trn_tallocationdtl " +
                    " where allocationdtl_gid='" + values.allocation_Gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lstransferred_from = objODBCDatareader["allocation_assignedRM"].ToString();
                lstransfrom_stategid = objODBCDatareader["state_gid"].ToString();
                lstransfrom_districtgid = objODBCDatareader["district_gid"].ToString();
            }
            objODBCDatareader.Close();

            msGetGid = objcmnfunctions.GetMasterGID("RKAT");
            msSQL = " insert into rsk_trn_tallocationtransfer(" +
                   " allocation_transfergid," +
                   " allocationdtl_gid," +
                   " transferred_from," +
                   " transferFrom_stategid, " +
                   " transferFrom_districtgid, " +
                   " transferred_to," +
                   " transferTo_stategid," +
                   " transferTo_districtgid," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.allocation_Gid + "'," +
                   "'" + lstransferred_from + "'," +
                   "'" + lstransfrom_stategid + "'," +
                   "'" + lstransfrom_districtgid + "'," +
                   "'" + values.transferred_to + "'," +
                   "'" + values.transferTo_stategid + "'," +
                   "'" + values.transferTo_districtgid + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select state_name,district_name from rsk_mst_tRMmapping where state_gid='" + values.transferTo_stategid + "' " +
                    " and district_gid='" + values.transferTo_districtgid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsstate_name = objODBCDatareader["state_name"].ToString();
                lsdistrict_name = objODBCDatareader["district_name"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " update rsk_trn_tallocationdtl a " +
                    " set a.allocation_assignedRM='" + values.transferred_to + "'," +
                    " a.allocation_status='Allocated'" +
                    " where a.allocationdtl_gid='" + values.allocation_Gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //count = count + 1;
            //}

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Allocation Transferred Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }

        }

        public bool DaGetTransferDetails(string allocationdtl_gid, allocationTransfer values)
        {

            msSQL = " select a.zonal_gid,a.state_gid,a.customer_gid,concat(b.customername,' / ' ,b.customer_urn ) as customerdtl, " +
                    " concat(c.zonal_name,' / ',a.state_name,' / ' ,a.district_name ) as location,a.allocation_assignedRM,a.assignedRM_name, " +
                    " a.zonalRM_name,a.allocation_zonalRM " +
                    " from rsk_trn_tallocationdtl a " +
                    " left join ocs_mst_tcustomer b on a.customer_gid = b.customer_gid " +
                    " left join rsk_mst_tzonalmapping c on a.zonal_gid = c.zonalmapping_gid " +
                    " where a.allocationdtl_gid = '" + allocationdtl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.transferred_from = objODBCDatareader["allocation_assignedRM"].ToString();
                values.assignedRM_name = objODBCDatareader["assignedRM_name"].ToString();
                values.customerdtl = objODBCDatareader["customerdtl"].ToString();
                values.state_gid = objODBCDatareader["state_gid"].ToString();
                values.location = objODBCDatareader["location"].ToString();
                values.zonalRM_name = objODBCDatareader["zonalRM_name"].ToString();
                values.zonalRM_gid = objODBCDatareader["allocation_zonalRM"].ToString();
                values.zonal_gid = objODBCDatareader["zonal_gid"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select assigned_RM,concat(c.user_firstname,' ',c.user_lastname,' / ' ,c.user_code) as assignedRmname from rsk_mst_trmmapping a " +
                   " left join hrm_mst_temployee b on a.assigned_RM = b.employee_gid " +
                   " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                   " where a.zonalmapping_gid = '" + values.zonal_gid + "' and assigned_RM<> ''";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_assignedRMdtl = new List<assignedRMlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_assignedRMdtl.Add(new assignedRMlist
                    {
                        assigned_RMname = dt["assignedRmname"].ToString(),
                        assignedRM_gid = dt["assigned_RM"].ToString(),
                    });
                }
                values.assignedRM = get_assignedRMdtl;
            }
            dt_datatable.Dispose();
            if (mnResult != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DaGetExternalDetails(string allocationdtl_gid, externalAllcoation values)
        {

            msSQL = " select a.allocate_externalGid,a.customer_gid,concat(b.customername,' / ' ,b.customer_urn ) as customerdtl, " +
                    " concat(e.external_username,' / ', e.external_usercode) as externalname,date_format(a.requested_date,'%d-%m-%Y') as requested_date," +
                    " date_format(a.target_date,'%d-%m-%Y') as target_date," +
                    " concat(d.user_firstname,' ',d.user_lastname,' / ',d.user_code) as requestedby,a.requested_remarks from rsk_trn_tallocationdtl a " +
                    " left join hrm_mst_temployee c on c.employee_gid=a.requested_by" +
                    " left join adm_mst_tuser d on d.user_gid=c.user_gid" +
                    " left join rsk_mst_texternaluser e on a.allocate_externalGid=e.external_usergid " +
                    " left join ocs_mst_tcustomer b on a.customer_gid = b.customer_gid where a.allocationdtl_gid = '" + allocationdtl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.externalname = objODBCDatareader["externalname"].ToString();
                values.customerdtl = objODBCDatareader["customerdtl"].ToString();
                values.assigned_by = objODBCDatareader["requestedby"].ToString();
                values.assigned_date = objODBCDatareader["requested_date"].ToString();
                values.requested_remarks = objODBCDatareader["requested_remarks"].ToString();
                values.target_date = objODBCDatareader["target_date"].ToString();
            }
            objODBCDatareader.Close();

            if (mnResult != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DaGetExternalNamelist(externaldtlList values)
        {
            msSQL = " select external_usergid, concat(external_username,' / ', external_usercode) as externalname " +
                   " from rsk_mst_texternaluser where external_userstatus = 'Y'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_externaldtl = new List<externaldtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_externaldtl.Add(new externaldtl
                    {
                        external_usergid = dt["external_usergid"].ToString(),
                        external_username = dt["externalname"].ToString(),
                    });
                }
                values.externaldtl = get_externaldtl;
            }
            dt_datatable.Dispose();
            return true;
        }


        public bool DaPostUpdateAllocateExternal(string employee_gid, externalAllocate values)
        {
            msSQL = " select concat(external_usercode,' / ',external_username) as externalname " +
                   " from rsk_mst_texternaluser where external_usergid='" + values.external_usergid + "'";
            values.external_name = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " update rsk_trn_tallocationdtl set allocate_externalGid='" + values.external_usergid + "'," +
                    " target_date='" + Convert.ToDateTime(values.target_date).ToString("yyyy-MM-dd") + "'," +
                    " allocate_externalname='" + values.external_name + "'," +
                    " allocate_external='Y'," +
                    " allocation_flag='E'," +
                    " allocation_status='External'," +
                    " requested_remarks='" + values.external_allocateRemarks + "'," +
                    " requested_by='" + employee_gid + "'," +
                    " requested_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where allocationdtl_gid='" + values.allocationdtl_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select allocation_gid from rsk_trn_tallocationdtl  where allocationdtl_gid='" + values.allocationdtl_gid + "'";
            string lsallocation_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " update rsk_trn_tallocation set status='Allocated' " +
                    " where allocation_gid='" + lsallocation_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select tmpallocation_docgid,document_name,document_path from rsk_tmp_texternalallocationdoc where created_by='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetDocumentGid = objcmnfunctions.GetMasterGID("EVAL");

                    msSQL = " Insert into rsk_trn_texternalallocationdoc( " +
                              " allocation_documentGid," +
                              " allocationdtl_gid," +
                              " document_name," +
                              " document_path," +
                              " created_by," +
                              " created_date)" +
                              " values(" +
                              "'" + msGetDocumentGid + "', " +
                              "'" + values.allocationdtl_gid + "'," +
                              "'" + dt["document_name"].ToString() + "'," +
                              "'" + dt["document_path"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult == 1)
                    {
                        msSQL = "delete from rsk_tmp_texternalallocationdoc where tmpallocation_docgid ='" + dt["tmpallocation_docgid"].ToString() + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

            }
            dt_datatable.Dispose();

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "External Allocation Done Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }

        public bool DaPostExternalUpload(HttpRequest httpRequest, uploaddocument objfilename, string employee_gid, string user_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;

            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            //MemoryStream ms = new MemoryStream();
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string lsdocumenttype_gid = string.Empty;
            string project_flag = httpRequest.Form["project_flag"].ToString();
            String path = lspath;

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "RSK/ExternalDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
            string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
            try

            {

                if (httpRequest.Files.Count > 0)
                {
                    string lsfirstdocument_filepath = string.Empty;

                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {

                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        //string lsfile_gid = msdocument_gid + FileExtension;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
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


                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "RSK/ExternalDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "RSK/ExternalDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;
                         
                        //FileStream file = new FileStream(lspath, FileMode.Create, FileAccess.Write);
                        //ms.WriteTo(file);
                        //file.Close();
                        //ms.Close(); 
                        msSQL = " insert into rsk_tmp_texternalallocationdoc( " +
                                    " document_name ," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            objfilename.status = true;
                        }
                        else
                        {
                            objfilename.status = false;
                        }
                    }
                }
                msSQL = "select tmpallocation_docgid,document_name,document_path,created_date " +
                        " from rsk_tmp_texternalallocationdoc where created_by='" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<upload_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new upload_list
                        {

                            tmp_documentGid = (dr_datarow["tmpallocation_docgid"].ToString()),
                            document_name = dr_datarow["document_name"].ToString(),
                            document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                            created_date = dr_datarow["created_date"].ToString(),
                        });
                    }
                    objfilename.upload_list = get_filename;
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {

            }
            return true;
        }


        public bool DaGetExternalUploadcancel(string tmp_documentGid, uploaddocument objfilename, string employee_gid)
        {
            msSQL = "delete from rsk_tmp_texternalallocationdoc where tmpallocation_docgid='" + tmp_documentGid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select tmpallocation_docgid,document_name,document_path,created_date " +
                       " from rsk_tmp_texternalallocationdoc where created_by='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<upload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new upload_list
                    {

                        tmp_documentGid = (dr_datarow["tmpallocation_docgid"].ToString()),
                        document_name = dr_datarow["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                        created_date = dr_datarow["created_date"].ToString(),
                    });
                }
                objfilename.upload_list = get_filename;
            }
            dt_datatable.Dispose();

            if (mnResult != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool DaGetTmpExtDocumentClear(string employee_gid)
        {

            msSQL = "delete from rsk_tmp_texternalallocationdoc where created_by='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool DaGetExternaldocument(uploaddocument values, string allocationdtl_gid)
        {

            msSQL = " select a.allocation_documentGid,a.document_name,a.document_path,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by " +
                 " from rsk_trn_texternalallocationdoc a " +
                 " left join hrm_mst_temployee b on a.created_by=b.employee_gid " +
                 " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                 " where a.allocationdtl_gid='" + allocationdtl_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_fileseekname = new List<upload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_fileseekname.Add(new upload_list
                    {
                        allocation_documentGid = (dr_datarow["allocation_documentGid"].ToString()),
                        document_name = dr_datarow["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                        created_by = dr_datarow["created_by"].ToString(),
                    });
                }
                values.upload_list = get_fileseekname;
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaGetCustomerAllocation(customerList values)
        {

            msSQL = " (select customer_gid,customername from ocs_mst_tcustomer a where total_sanction >= 5000000 and customer_gid not in " +
                   " (select customer_gid from rsk_trn_tallocationdtl " +
                   " where (allocation_flag = 'Y' or allocation_flag = 'P')) group by customer_gid order by customername asc)" +
                   " UNION " +
                   " (select tmpcustomer_gid,customername from ocs_tmp_tcustomer a where total_sanction >= 5000000 and tmpcustomer_gid not in " +
                   " (select customer_gid from rsk_trn_tallocationdtl " +
                   " where (allocation_flag = 'Y' or allocation_flag = 'P')) group by tmpcustomer_gid order by customername asc)";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_customerdtl = new List<customerdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_customerdtl.Add(new customerdtl
                    {
                        customer_gid = dt["customer_gid"].ToString(),
                        customer_name = dt["customername"].ToString()
                    });
                }

                get_customerdtl.Add(new customerdtl
                {
                    customer_gid = "",
                    customer_name = ""
                });
                values.customerdtl = get_customerdtl;
            }
            dt_datatable.Dispose();

            return true;
        }


        public bool DaPostAllocateUpload(HttpRequest httpRequest, uploaddocument objfilename, string employee_gid, string user_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;

            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            //MemoryStream ms = new MemoryStream();
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string lsdocumenttype_gid = string.Empty;
            string lsdocument_type = httpRequest.Form["document_type"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();
            String path = lspath;

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "RSK/AllocationDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
            string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
            try

            {

                if (httpRequest.Files.Count > 0)
                {
                    string lsfirstdocument_filepath = string.Empty;

                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {

                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        //string lsfile_gid = msdocument_gid + FileExtension;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        ls_readStream = httpPostedFile.InputStream;
                        MemoryStream ms = new MemoryStream();
                        ls_readStream.CopyTo(ms);

                        byte[] bytes = ms.ToArray();

                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "RSK/AllocationDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "RSK/AllocationDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;


                        //lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "RSK/AllocationDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        //objcmnfunctions.uploadFile(lspath, lsfile_gid);
                        //FileStream file = new FileStream(lspath, FileMode.Create, FileAccess.Write);
                        //ms.WriteTo(file);
                        //file.Close();
                        //ms.Close();
                        //lspath = "../../erpdocument" + "/" + lscompany_code + "/" + "RSK/AllocationDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msSQL = " insert into rsk_tmp_tallocationdocument( " +
                                    " document_name ," +
                                    " document_path," +
                                    " document_type," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + "'," +
                                    "'" + lsdocument_type + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            objfilename.status = true;
                            objfilename.message = "Document Uploaded Successfully..!";
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error Occured..!";
                        }
                    }
                }
                msSQL = "select tmp_documentGid,document_type,document_name,document_path,created_date " +
                        " from rsk_tmp_tallocationdocument where created_by='" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<upload_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new upload_list
                        {
                            document_type = (dr_datarow["document_type"].ToString()),
                            tmp_documentGid = (dr_datarow["tmp_documentGid"].ToString()),
                            document_name = dr_datarow["document_name"].ToString(),
                            document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                            created_date = dr_datarow["created_date"].ToString(),
                        });
                    }
                    objfilename.upload_list = get_filename;
                }
                dt_datatable.Dispose();
            }
            catch(Exception ex)
            {

            }
            return true;
        }


        public bool DaAllocateUploadcancel(string tmp_documentGid, uploaddocument objfilename, string employee_gid)
        {

            msSQL = "delete from rsk_tmp_tallocationdocument where tmp_documentGid='" + tmp_documentGid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select tmp_documentGid,document_name,document_type,document_path,created_date " +
                       " from rsk_tmp_tallocationdocument where created_by='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<upload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new upload_list
                    {
                        document_type = (dr_datarow["document_type"].ToString()),
                        tmp_documentGid = (dr_datarow["tmp_documentGid"].ToString()),
                        document_name = dr_datarow["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                        created_date = dr_datarow["created_date"].ToString(),
                    });
                }
                objfilename.upload_list = get_filename;
            }
            dt_datatable.Dispose();


            if (mnResult != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool DaGettmpAllodocumentclear(string employee_gid)
        {


            msSQL = "delete from rsk_tmp_tallocationdocument where created_by='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool DaGetAllocationdocument(uploaddocument values, string allocationdtl_gid)
        {

            msSQL = " select a.allocation_documentGid,a.document_type,a.document_name,a.document_path, " +
                  " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, " +
                  " date_format(a.created_date,'%d-%m-%Y') as created_date " +
                 " from rsk_trn_tallocationdocument a " +
                 " left join hrm_mst_temployee b on a.created_by=b.employee_gid " +
                 " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                 " where a.allocationdtl_gid='" + allocationdtl_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_fileseekname = new List<upload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_fileseekname.Add(new upload_list
                    {
                        allocation_documentGid = (dr_datarow["allocation_documentGid"].ToString()),
                        document_name = dr_datarow["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                        document_type = (dr_datarow["document_type"].ToString()),
                        created_by = dr_datarow["created_by"].ToString(),
                        created_date = dr_datarow["created_date"].ToString(),
                    });
                }
                values.upload_list = get_fileseekname;
            }
            dt_datatable.Dispose();

            return true;
        }


        public bool DaGetAllocationHistory(string customer_gid, overallhistoryallocationlist values)
        {

            msSQL = " select a.customer_gid,b.customername,c.cancel_flag,b.customer_urn,k.observation_reportgid, " +
                   " a.allocationdtl_gid,a.allocation_status,a.allocate_external, " +
                   " concat(a.state_name,' / ',a.district_name) as allocatedLocation,a.allocation_flag, " +
                   " concat(f.user_firstname,' ',f.user_lastname,' / ',f.user_code) as assigned_RM, " +
                   " date_format(a.lastvisit_date,'%d-%m-%Y') as lastvisit_date, " +
                   " concat(h.user_firstname, ' ', h.user_lastname, ' / ', h.user_code) as ZonalRMname," +
                   " concat(j.user_firstname, ' ', j.user_lastname, ' / ', j.user_code) as AllocatedBy,l.tier1format_gid, " +
                   " date_format(a.created_date,'%d-%m-%Y') as allocated_date,date_format(a.completed_date,'%d-%m-%Y %h:%i %p') as completed_date" +
                   " from rsk_trn_tallocationdtl a " +
                   " left join ocs_mst_tcustomer b on a.customer_gid=b.customer_gid " +
                   " left join rsk_trn_tvisitreportgenerate c on c.allocationdtl_gid=a.allocationdtl_gid " +
                   " left join hrm_mst_temployee e on e.employee_gid = a.allocation_assignedRM " +
                   " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
                   " left join hrm_mst_temployee g on g.employee_gid = a.allocation_zonalRM " +
                   " left join adm_mst_tuser h on h.user_gid = g.user_gid " +
                   " left join hrm_mst_temployee i on i.employee_gid = a.allocated_by " +
                   " left join adm_mst_tuser j on i.user_gid = j.user_gid " +
                   " left join rsk_trn_tobservationreport k on k.allocationdtl_gid = a.allocationdtl_gid " +
                   " left join rsk_trn_ttier1format l on l.allocationdtl_gid=a.allocationdtl_gid " +
                   " where a.customer_gid='" + customer_gid + "' and a.completed_flag='Y' order by a.allocationdtl_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mappingdtl = new List<overallhistoryallocationdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_mappingdtl.Add(new overallhistoryallocationdtl
                    {
                        allocationdtl_gid = dt["allocationdtl_gid"].ToString(),
                        customername = dt["customername"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        location = dt["allocatedLocation"].ToString(),
                        assigned_RM = dt["assigned_RM"].ToString(),
                        ZonalRMname = dt["ZonalRMname"].ToString(),
                        allocation_flag = dt["allocation_flag"].ToString(),
                        allocation_status = dt["allocation_status"].ToString(),
                        lastvisit_date = dt["lastvisit_date"].ToString(),
                        created_by = dt["AllocatedBy"].ToString(),
                        created_date = dt["allocated_date"].ToString(),
                        completed_date = dt["completed_date"].ToString(),
                        reportcancel_flag = dt["cancel_flag"].ToString(),
                        allocate_external = dt["allocate_external"].ToString(),
                        observation_reportgid = dt["observation_reportgid"].ToString(),
                        tier1format_gid=dt["tier1format_gid"].ToString(),
                    });
                }
                values.overallhistoryallocationdtl = get_mappingdtl;
            }
            dt_datatable.Dispose();

 
            return true;
        }

        public bool DaPostlastvisitdate(string employee_gid, lastvisitdtl values)
        {

            msSQL = "Select date_format(lastvisit_date,'%Y-%m-%d') from rsk_trn_tcustomervisit where customer_gid='" + values.customer_gid + "'";
            string lslastvisit_date = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " insert into rsk_trn_thistorycustomervisit (" +
                    " customer_gid," +
                    " historyvisit_date," +
                    " latestvisit_date," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + values.customer_gid + "',";
            if (lslastvisit_date == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + lslastvisit_date + "',";
            }
            msSQL += "'" + values.lastvisit_date.ToString("yyyy-MM-dd") + "'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select customer_gid from rsk_trn_tcustomervisit where customer_gid='" + values.customer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                msSQL = " update rsk_trn_tcustomervisit set lastvisit_date='" + values.lastvisit_date.ToString("yyyy-MM-dd") + "' " +
                        " where customer_gid='" + values.customer_gid + "'";
                objODBCDatareader.Close();
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                objODBCDatareader.Close();
                msGet_visitGid = objcmnfunctions.GetMasterGID("CUVD");

                msSQL = "insert into rsk_trn_tcustomervisit(" +
                       " customervisit_gid ," +
                       " customer_gid," +
                       " lastvisit_date," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGet_visitGid + "'," +
                       "'" + values.customer_gid + "'," +
                       "'" + values.lastvisit_date.ToString("yyyy-MM-dd") + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Last Visit Date Updated Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }

        }

        public bool DaGetViewCancelReason(visistreportcancel values, string allocationdtl_gid)
        {
            msSQL = " select cancel_remarks,date_format(a.report_lastchangesdate, '%d-%m-%Y %h:%m %p') as created_date, " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by from rsk_trn_tvisitreportgenerate a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " where allocationdtl_gid='" + allocationdtl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.cancel_remarks = objODBCDatareader["cancel_remarks"].ToString();
                values.created_date = objODBCDatareader["created_date"].ToString();
                values.created_by = objODBCDatareader["created_by"].ToString();
            }
            objODBCDatareader.Close();


            if (mnResult != 0)
            {
                values.status = true;

                return true;
            }
            else
            {
                values.status = false;

                return false;
            }
        }

        public bool DaGetCaseAllocaCancelChanges(allocationlist values)
        {
            msSQL = " select a.customer_gid,f.customer_urn,format(a.sanction_amount, 2) as sanction_amount,f.vertical_code,f.customername, " +
         " b.allocationdtl_gid,b.allocation_status,b.allocate_external, " +
         " concat(e.zonal_name, ' / ', b.state_name, ' / ', b.district_name) as allocatedLocation,b.allocation_flag," +
         " date_format(d.lastvisit_date, '%d-%m-%Y') as lastvisit_date,DATEDIFF(CURDATE(), d.lastvisit_date) as count_visit," +
         " c.first_disb_date,c.last_disb_date,date_format(b.created_date, '%d-%m-%Y') as allocated_date," +
         " case when(d.lastvisit_date is null) then date_format(c.first_disb_date, '%d-%m-%Y') else" +
         " date_format(c.last_disb_date, '%d-%m-%Y') end as disbursement_date," +
         "  case when(d.lastvisit_date is null) then DATEDIFF(CURDATE(), c.first_disb_date) else" +
         " DATEDIFF(CURDATE(), c.last_disb_date) end as daypassed_disbursement," +
         "  case when(d.lastvisit_date is null) then date_format(c.first_disb_date, '%Y-%m-%d') else" +
         "  date_format(c.last_disb_date, '%Y-%m-%d') end as disbursementDate," +
         "  date_format(d.lastvisit_date, '%Y-%m-%d') as lastvisitDate," +
         " b.assignedRM_name,b.zonalRM_name from rsk_trn_tallocation a" +
         " left join rsk_trn_tallocationdtl b on a.customer_gid = b.customer_gid" +
         " left join rsk_trn_tcustomervisit d on d.customer_gid = a.customer_gid" +
         " left join rsk_mst_tzonalmapping e on b.zonal_gid = e.zonalmapping_gid" +
         " left join ocs_mst_tcustomer f on f.customer_gid = a.customer_gid" +
         " left join rsk_trn_tcustomerdisbursement c on f.customer_urn = c.customer_urn" +
         " where a.reportcancel_flag = 'Y'  group by a.customer_gid ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mappingdtl = new List<allocationdtl>();
            values.count_reportcancel = dt_datatable.Rows.Count;
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    if (dt["disbursement_date"].ToString() != "" && dt["lastvisit_date"].ToString() != "")
                    {
                        if (Convert.ToDateTime(dt["disbursementDate"]) > Convert.ToDateTime(dt["lastvisitDate"]))
                        {
                            lsdisbursement_date = dt["disbursement_date"].ToString();
                            lsdaypassed_visit = dt["daypassed_disbursement"].ToString();
                        }
                        else
                        {
                            lsdisbursement_date = lsdaypassed_visit = "-";
                        }
                    }
                    else
                    {
                        lsdisbursement_date = dt["disbursement_date"].ToString();
                        lsdaypassed_visit = dt["daypassed_disbursement"].ToString();
                    }

                    get_mappingdtl.Add(new allocationdtl
                    {
                        allocationdtl_gid = dt["allocationdtl_gid"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        customername = dt["customername"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical = dt["vertical_code"].ToString(),
                        location = dt["allocatedLocation"].ToString(),
                        assigned_RM = dt["assignedRM_name"].ToString(),
                        ZonalRMname = dt["zonalRM_name"].ToString(),
                        allocation_flag = dt["allocation_flag"].ToString(),
                        allocation_status = dt["allocation_status"].ToString(),
                        allocate_external = dt["allocate_external"].ToString(),
                        lastvisit_date = dt["lastvisit_date"].ToString(),
                        count_lastvisit = dt["count_visit"].ToString(),
                        allocated_date = dt["allocated_date"].ToString(),
                        disbursement_date = lsdisbursement_date,
                        daypassed_disbursement = lsdaypassed_visit,
                        sanction_amount = dt["sanction_amount"].ToString(),

                    });
                }
                values.allocationdtl = get_mappingdtl;
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaGetQualifiedAllocationSummary(qualifiedallocationlist values)
        {
            msSQL = " (select a.customer_urn,a.customername,format(a.total_sanction, 2) as total_sanction, " +
                    " case when(b.lastvisit_date is null) then DATEDIFF(CURDATE(), c.first_disb_date) else" +
                    " DATEDIFF(CURDATE(), c.last_disb_date) end as daypassed_disbursement," +
                    " DATEDIFF(CURDATE(), b.lastvisit_date) as daypassed_visit," +
                    " case when(DATEDIFF(CURDATE(), lastvisit_date) > 180) then date_format(c.last_disb_date, '%d-%m-%Y')" +
                    " else date_format(c.first_disb_date, '%d-%m-%Y') end as disbursement_date," +
                    " date_format(lastvisit_date, '%d-%m-%Y') as lastvisit_date ," +
                    " case when(DATEDIFF(CURDATE(), lastvisit_date) > 180 and c.last_disb_date > lastvisit_date) then 'Re-Visit'" +
                    " when(DATEDIFF(CURDATE(), lastvisit_date) > 180 and c.last_disb_date < lastvisit_date) then 'Re-Visit'" +
                    " else 'Fresh' end as qualified_status from ocs_mst_tcustomer a" +
                    " left join rsk_trn_tcustomervisit b on a.customer_urn = b.customer_urn " +
                    " left join rsk_trn_tcustomerdisbursement c on a.customer_urn = c.customer_urn " +
                    " where (((DATEDIFF(CURDATE(), first_disb_date)) > 31 and c.allocate_flag = 'N' and lastvisit_date is null and ac_status = '0') or " +
                    " ((DATEDIFF(CURDATE(), lastvisit_date)) > 180 and ac_status = '0' and c.allocate_flag = 'N')) " +
                    " and total_sanction >= 5000000 order by daypassed_visit desc, daypassed_disbursement asc limit 500)" +
                    " UNION " +
                    " (select a.customer_urn,a.customername,format(a.total_sanction, 2) as total_sanction, " +
                    " case when(b.lastvisit_date is null) then DATEDIFF(CURDATE(), c.first_disb_date) else" +
                    " DATEDIFF(CURDATE(), c.last_disb_date) end as daypassed_disbursement," +
                    " DATEDIFF(CURDATE(), b.lastvisit_date) as daypassed_visit," +
                    " case when(DATEDIFF(CURDATE(), lastvisit_date) > 180) then date_format(c.last_disb_date, '%d-%m-%Y')" +
                    " else date_format(c.first_disb_date, '%d-%m-%Y') end as disbursement_date," +
                    " date_format(lastvisit_date, '%d-%m-%Y') as lastvisit_date ," +
                    " case when(DATEDIFF(CURDATE(), lastvisit_date) > 180 and c.last_disb_date > lastvisit_date) then 'Re-Visit'" +
                    " when(DATEDIFF(CURDATE(), lastvisit_date) > 180 and c.last_disb_date < lastvisit_date) then 'Re-Visit'" +
                    " else 'Fresh' end as qualified_status from ocs_tmp_tcustomer a" +
                    " left join rsk_trn_tcustomervisit b on a.customer_urn = b.customer_urn " +
                    " left join rsk_trn_tcustomerdisbursement c on a.customer_urn = c.customer_urn " +
                    " where (((DATEDIFF(CURDATE(), first_disb_date)) > 31 and c.allocate_flag = 'N' and lastvisit_date is null and ac_status = '0') or " +
                    " ((DATEDIFF(CURDATE(), lastvisit_date)) > 180 and ac_status = '0' and c.allocate_flag = 'N')) " +
                    " and total_sanction >= 5000000 order by daypassed_visit desc, daypassed_disbursement asc limit 500)";
                    //" and total_sanction >= 25000 order by daypassed_visit desc, daypassed_disbursement asc limit 500)";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mappingdtl = new List<qualifiedallocation>();

            if (dt_datatable.Rows.Count != 0)
            {
                values.qualifiedallocation = dt_datatable.AsEnumerable().Select(row => new qualifiedallocation
                {
                    customer_urn = row["customer_urn"].ToString(),
                    customer_name = row["customername"].ToString(),
                    disbursement_date = row["disbursement_date"].ToString(),
                    daypassed_disbursement = row["daypassed_disbursement"].ToString(),
                    lastvisit_date = row["lastvisit_date"].ToString(),
                    qualified_status = row["qualified_status"].ToString(),
                    daypassed_visit = row["daypassed_visit"].ToString(),
                    total_sanction = row["total_sanction"].ToString(),

                }).ToList();
            }
            dt_datatable.Dispose();


            return true;
        }

        public bool DaGetQualifiedFreshAllocation(qualifiedallocationlist values)
        {
            msSQL = " (select a.customer_urn,a.customername,format(a.total_sanction, 2) as total_sanction, " +
                    " case when(b.lastvisit_date is null) then DATEDIFF(CURDATE(), c.first_disb_date) else" +
                    " DATEDIFF(CURDATE(), c.last_disb_date) end as daypassed_disbursement," +
                    " DATEDIFF(CURDATE(), b.lastvisit_date) as daypassed_visit," +
                    " case when(DATEDIFF(CURDATE(), lastvisit_date) > 180) then date_format(c.last_disb_date, '%d-%m-%Y')" +
                    " else date_format(c.first_disb_date, '%d-%m-%Y') end as disbursement_date," +
                    " date_format(lastvisit_date, '%d-%m-%Y') as lastvisit_date," +
                    " case when(DATEDIFF(CURDATE(), lastvisit_date) > 180 and c.last_disb_date > lastvisit_date) then 'Re-Visit'" +
                    " else 'Fresh' end as qualified_status from ocs_mst_tcustomer a" +
                    " left join rsk_trn_tcustomervisit b on a.customer_urn = b.customer_urn " +
                    " left join rsk_trn_tcustomerdisbursement c on a.customer_urn = c.customer_urn " +
                    " where (((DATEDIFF(CURDATE(), first_disb_date)) > 31 and c.allocate_flag = 'N' and c.exclusion_flag='N' and lastvisit_date is null and ac_status = '0')) " +
                    " and total_sanction >= 5000000 order by daypassed_visit desc, daypassed_disbursement asc limit 500)" +
                    " UNION " +
                    " (select a.customer_urn,a.customername,format(a.total_sanction, 2) as total_sanction, " +
                    " case when(b.lastvisit_date is null) then DATEDIFF(CURDATE(), c.first_disb_date) else" +
                    " DATEDIFF(CURDATE(), c.last_disb_date) end as daypassed_disbursement," +
                    " DATEDIFF(CURDATE(), b.lastvisit_date) as daypassed_visit," +
                    " case when(DATEDIFF(CURDATE(), lastvisit_date) > 180) then date_format(c.last_disb_date, '%d-%m-%Y')" +
                    " else date_format(c.first_disb_date, '%d-%m-%Y') end as disbursement_date," +
                    " date_format(lastvisit_date, '%d-%m-%Y') as lastvisit_date," +
                    " case when(DATEDIFF(CURDATE(), lastvisit_date) > 180 and c.last_disb_date > lastvisit_date) then 'Re-Visit'" +
                    " else 'Fresh' end as qualified_status from ocs_tmp_tcustomer a" +
                    " left join rsk_trn_tcustomervisit b on a.customer_urn = b.customer_urn " +
                    " left join rsk_trn_tcustomerdisbursement c on a.customer_urn = c.customer_urn " +
                    " where (((DATEDIFF(CURDATE(), first_disb_date)) > 31 and c.allocate_flag = 'N' and c.exclusion_flag='N' and lastvisit_date is null and ac_status = '0')) " +
                    " and total_sanction >= 5000000 order by daypassed_visit desc, daypassed_disbursement asc limit 500)";
                    //" and total_sanction >= 25000 order by daypassed_visit desc, daypassed_disbursement asc limit 500)";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mappingdtl = new List<qualifiedallocation>();
            if (dt_datatable.Rows.Count != 0)
            {
                values.qualifiedallocation = dt_datatable.AsEnumerable().Select(row => new qualifiedallocation
                {
                    customer_urn = row["customer_urn"].ToString(),
                    customer_name = row["customername"].ToString(),
                    disbursement_date = row["disbursement_date"].ToString(),
                    daypassed_disbursement = row["daypassed_disbursement"].ToString(),
                    lastvisit_date = row["lastvisit_date"].ToString(),
                    qualified_status = row["qualified_status"].ToString(),
                    daypassed_visit = row["daypassed_visit"].ToString(),
                    total_sanction = row["total_sanction"].ToString(),

                }).ToList();
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaGetQualifiedReVisitAllocation(qualifiedallocationlist values)
        {
            msSQL = " (select a.customer_urn,case when(DATEDIFF(CURDATE(), lastvisit_date) > 180 and c.last_disb_date > lastvisit_date) then 'Re-Visit' " +
                    " when(DATEDIFF(CURDATE(), lastvisit_date) > 180 and c.last_disb_date < lastvisit_date) then 'Re-Visit'" +
                    " else 'Fresh' end as qualified_status, a.customername,format(a.total_sanction, 2) as total_sanction," +
                    " case when(b.lastvisit_date is null) then DATEDIFF(CURDATE(), c.first_disb_date) else" +
                    " DATEDIFF(CURDATE(), c.last_disb_date) end as daypassed_disbursement," +
                    " DATEDIFF(CURDATE(), b.lastvisit_date) as daypassed_visit," +
                    " case when(DATEDIFF(CURDATE(), lastvisit_date) > 180) then date_format(c.last_disb_date, '%d-%m-%Y')" +
                    " else date_format(c.first_disb_date, '%d-%m-%Y') end as disbursement_date," +
                    " date_format(lastvisit_date, '%d-%m-%Y') as lastvisit_date" +
                    " from ocs_mst_tcustomer a" +
                    " left join rsk_trn_tcustomervisit b on a.customer_urn = b.customer_urn" +
                    " left join rsk_trn_tcustomerdisbursement c on a.customer_urn = c.customer_urn" +
                    " where ((DATEDIFF(CURDATE(), lastvisit_date)) > 180 and ac_status = '0' and c.exclusion_flag='N' and c.allocate_flag = 'N')" +
                    " and total_sanction >= 5000000 order by daypassed_visit desc, daypassed_disbursement asc limit 500)" +
                    " UNION " +
                    " (select a.customer_urn,case when(DATEDIFF(CURDATE(), lastvisit_date) > 180 and c.last_disb_date > lastvisit_date) then 'Re-Visit' " +
                    " when(DATEDIFF(CURDATE(), lastvisit_date) > 180 and c.last_disb_date < lastvisit_date) then 'Re-Visit'" +
                    " else 'Fresh' end as qualified_status, a.customername,format(a.total_sanction, 2) as total_sanction," +
                    " case when(b.lastvisit_date is null) then DATEDIFF(CURDATE(), c.first_disb_date) else" +
                    " DATEDIFF(CURDATE(), c.last_disb_date) end as daypassed_disbursement," +
                    " DATEDIFF(CURDATE(), b.lastvisit_date) as daypassed_visit," +
                    " case when(DATEDIFF(CURDATE(), lastvisit_date) > 180) then date_format(c.last_disb_date, '%d-%m-%Y')" +
                    " else date_format(c.first_disb_date, '%d-%m-%Y') end as disbursement_date," +
                    " date_format(lastvisit_date, '%d-%m-%Y') as lastvisit_date" +
                    " from ocs_tmp_tcustomer a" +
                    " left join rsk_trn_tcustomervisit b on a.customer_urn = b.customer_urn" +
                    " left join rsk_trn_tcustomerdisbursement c on a.customer_urn = c.customer_urn" +
                    " where ((DATEDIFF(CURDATE(), lastvisit_date)) > 180 and ac_status = '0' and c.exclusion_flag='N' and c.allocate_flag = 'N')" +
                    " and total_sanction >= 5000000 order by daypassed_visit desc, daypassed_disbursement asc limit 500)";
                    //" and total_sanction >= 25000 order by daypassed_visit desc, daypassed_disbursement asc limit 500)";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mappingdtl = new List<qualifiedallocation>();
            if (dt_datatable.Rows.Count != 0)
            {
                values.qualifiedallocation = dt_datatable.AsEnumerable().Select(row => new qualifiedallocation
                {
                    customer_urn = row["customer_urn"].ToString(),
                    customer_name = row["customername"].ToString(),
                    disbursement_date = row["disbursement_date"].ToString(),
                    daypassed_disbursement = row["daypassed_disbursement"].ToString(),
                    lastvisit_date = row["lastvisit_date"].ToString(),
                    qualified_status = row["qualified_status"].ToString(),
                    daypassed_visit = row["daypassed_visit"].ToString(),
                    total_sanction = row["total_sanction"].ToString(),
                }).ToList();
            }
            dt_datatable.Dispose();

            return true;
        }
        public bool DaGetQualifiedUnmatched(qualifiedallocationlist values)
        {
            msSQL = " select a.customer_urn,a.customer_name, a.Vertical, " +
                    " case when(b.lastvisit_date is null) then DATEDIFF(CURDATE(), a.first_disb_date) else " +
                    " DATEDIFF(CURDATE(), a.last_disb_date) end as daypassed_disbursement, " +
                    " DATEDIFF(CURDATE(), b.lastvisit_date) as daypassed_visit," +
                    " case when(DATEDIFF(CURDATE(), lastvisit_date) > 180) then date_format(last_disb_date, '%d-%m-%Y')" +
                    " else date_format(first_disb_date, '%d-%m-%Y') end as disbursement_date,date_format(lastvisit_date, '%d-%m-%Y') as lastvisit_date," +
                    " DATEDIFF(CURDATE(), first_disb_date) as daypassed_disbursement,DATEDIFF(CURDATE(), lastvisit_date) as daypassed_visit, " +
                    " case when(DATEDIFF(CURDATE(), lastvisit_date) > 180 and last_disb_date > lastvisit_date) then 'Re-Visit' " +
                    " else 'Fresh' end as qualified_status from rsk_trn_tcustomerdisbursement a " +
                    " left join rsk_trn_tcustomervisit b on a.customer_urn = b.customer_urn " +
                    " where a.customer_urn not in (select customer_urn from ocs_mst_tcustomer where customer_urn <> '') and " +
                    " (((DATEDIFF(CURDATE(), first_disb_date)) > 31 and ac_status='0' and a.allocate_flag = 'N') or " +
                    " ((DATEDIFF(CURDATE(), lastvisit_date)) > 180 and ac_status='0' and a.exclusion_flag='N' and a.allocate_flag = 'N')) " +
                    " and a.allocate_flag = 'N' order by daypassed_visit desc, daypassed_disbursement asc limit 500";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mappingdtl = new List<qualifiedallocation>();
            values.count_unmatchedqualified = dt_datatable.Rows.Count;
            if (dt_datatable.Rows.Count != 0)
            {
                values.qualifiedallocation = dt_datatable.AsEnumerable().Select(row => new qualifiedallocation
                {
                    customer_urn = row["customer_urn"].ToString(),
                    customer_name = row["customer_name"].ToString(),
                    disbursement_date = row["disbursement_date"].ToString(),
                    daypassed_disbursement = row["daypassed_disbursement"].ToString(),
                    lastvisit_date = row["lastvisit_date"].ToString(),
                    qualified_status = row["qualified_status"].ToString(),
                    daypassed_visit = row["daypassed_visit"].ToString(),
                    vertical = row["Vertical"].ToString(),

                }).ToList();
            }
            dt_datatable.Dispose();

            return true;
        }



        public bool DaGetBreachedAllocationSummary(breachedlist values)
        {
            msSQL = " select a.customer_gid,f.customer_urn,format(a.sanction_amount, 2) as sanction_amount,f.vertical_code,f.customername, " +
                " date_format(b.created_date, '%d-%m-%Y') as allocated_date,cast(monthname(visit_allocated_date) as char) as visitallocatemonth," +
         " b.allocationdtl_gid,b.allocation_status,concat(e.zonal_name, ' / ', b.state_name, ' / ', b.district_name) as allocatedLocation,b.allocation_flag," +
         " date_format(d.lastvisit_date, '%d-%m-%Y') as lastvisit_date,DATEDIFF(CURDATE(), d.lastvisit_date) as count_visit," +
         " case when(d.lastvisit_date is null) then date_format(c.first_disb_date, '%d-%m-%Y') else" +
         " date_format(c.last_disb_date, '%d-%m-%Y') end as disbursement_date," +
         "  case when(d.lastvisit_date is null) then DATEDIFF(CURDATE(), c.first_disb_date) else" +
         " DATEDIFF(CURDATE(), c.last_disb_date) end as daypassed_disbursement," +
         "  case when(d.lastvisit_date is null) then date_format(c.first_disb_date, '%Y-%m-%d') else" +
         "  date_format(c.last_disb_date, '%Y-%m-%d') end as disbursementDate," +
         "  date_format(d.lastvisit_date, '%Y-%m-%d') as lastvisitDate," +
         " b.assignedRM_name,b.zonalRM_name from rsk_trn_tallocation a" +
         " left join rsk_trn_tallocationdtl b on a.allocation_gid = b.allocation_gid " +
         " left join rsk_trn_tcustomervisit d on d.customer_gid = a.customer_gid" +
         " left join rsk_mst_tzonalmapping e on b.zonal_gid = e.zonalmapping_gid" +
         " left join ocs_mst_tcustomer f on f.customer_gid = a.customer_gid" +
         " left join rsk_trn_tcustomerdisbursement c on f.customer_urn = c.customer_urn" +
         " where DATEDIFF(CURDATE(), visit_allocated_date)>90 and b.lastvisit_date is null group by a.customer_gid ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            values.count_breached = dt_datatable.Rows.Count;
            var get_mappingdtl = new List<breacheddtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    if (dt["disbursement_date"].ToString() != "" && dt["lastvisit_date"].ToString() != "")
                    {
                        if (Convert.ToDateTime(dt["disbursementDate"]) > Convert.ToDateTime(dt["lastvisitDate"]))
                        {
                            lsdisbursement_date = dt["disbursement_date"].ToString();
                            lsdaypassed_visit = dt["daypassed_disbursement"].ToString();
                        }
                        else
                        {
                            lsdisbursement_date = lsdaypassed_visit = "-";
                        }
                    }
                    else
                    {
                        lsdisbursement_date = dt["disbursement_date"].ToString();
                        lsdaypassed_visit = dt["daypassed_disbursement"].ToString();
                    }

                    get_mappingdtl.Add(new breacheddtl
                    {
                        allocationdtl_gid = dt["allocationdtl_gid"].ToString(),
                        allocated_date = dt["allocated_date"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        customername = dt["customername"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical = dt["vertical_code"].ToString(),
                        location = dt["allocatedLocation"].ToString(),
                        assigned_RM = dt["assignedRM_name"].ToString(),
                        ZonalRMname = dt["zonalRM_name"].ToString(),
                        allocation_flag = dt["allocation_flag"].ToString(),
                        allocation_status = dt["allocation_status"].ToString(),
                        lastvisit_date = dt["lastvisit_date"].ToString(),
                        count_lastvisit = dt["count_visit"].ToString(),
                        disbursement_date = lsdisbursement_date,
                        daypassed_disbursement = lsdaypassed_visit,
                        visit_allocatemonth = dt["visitallocatemonth"].ToString(),
                        sanction_amount = dt["sanction_amount"].ToString(),
                    });
                }
                values.breacheddtl = get_mappingdtl;
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaGetCustomerGid(string customer_urn, customergid values)
        {
            msSQL = "select customer_gid from ocs_mst_tcustomer where customer_urn='" + customer_urn + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.customer_gid = objODBCDatareader["customer_gid"].ToString();
                objODBCDatareader.Close();
                values.status = true;
                return true;
            }
            else
            {
                objODBCDatareader.Close();
                values.status = false;
                return false;
            }

        }

        public bool DaGetHoldAllocation(string allocationdtl_gid, string allocationhold_reason, string user_gid, result values)
        {

            msSQL = "select visit_allocated_date from rsk_trn_tallocationdtl where allocationdtl_gid='" + allocationdtl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                string lsallocated_date = objODBCDatareader["visit_allocated_date"].ToString();
                objODBCDatareader.Close();
                msGet_visitGid = objcmnfunctions.GetMasterGID("ALHO");

                msSQL = "insert into rsk_trn_tallocation2hold(" +
                       " allocation2hold_gid ," +
                       " allocationdtl_gid," +
                       " oldallocated_date," +
                       " allocationhold_reason," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGet_visitGid + "'," +
                       "'" + allocationdtl_gid + "'," +
                       "'" + Convert.ToDateTime(lsallocated_date).ToString("yyyy-MM-dd") + "'," +
                       "'" + allocationhold_reason.Replace("'", "\\'") + "'," +
                       "'" + user_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update rsk_trn_tallocation2hold" +
                        " set allocated_date = CONCAT(LEFT('" + Convert.ToDateTime(lsallocated_date).ToString("yyyy-MM-dd") + "' + INTERVAL 1 MONTH, 7), '-01') " +
                        " where allocation2hold_gid = '" + msGet_visitGid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update rsk_trn_tallocationdtl" +
                    " set visit_allocated_date = CONCAT(LEFT('" + Convert.ToDateTime(lsallocated_date).ToString("yyyy-MM-dd") + "' + INTERVAL 1 MONTH, 7), '-01') " +
                    " where allocationdtl_gid = '" + allocationdtl_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Allocation Hold Successfully..!";
                values.status = true;
                return true;
            }
            else
            {
                objODBCDatareader.Close();
                values.message = "Error Occured..!";
                values.status = false;
                return false;
            }

        }

        public void DaGetAllocationPendingCount(string zonalmapping_gid, zonalwisecountList values)
        {
            msSQL = " select assigned_RM,concat(c.user_firstname,' ',c.user_lastname ,' / ',c.user_code) as riskmanager_name from  rsk_mst_trmmapping a " +
                     " left join hrm_mst_temployee b on a.assigned_RM = b.employee_gid " +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                     " where zonalmapping_gid = '" + zonalmapping_gid + "' and assigned_RM!='' group by assigned_RM";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getzonalallocationcount = new List<zonalwisecount>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    msSQL = " select a.count_fresh,b.count_revisit,c.count_overall from " +
                           " (select count(*) as count_fresh from rsk_trn_tallocationdtl " +
                           " where qualified_status = 'Fresh' and zonal_gid = '" + zonalmapping_gid + "' " +
                           " and allocation_assignedRM = '" + dr_datarow["assigned_RM"].ToString() + "' and allocation_status = 'Allocated') as a, " +
                           " (select count(*) as count_revisit from rsk_trn_tallocationdtl " +
                           " where qualified_status = 'Re-Visit'  and zonal_gid = '" + zonalmapping_gid + "' " +
                           " and allocation_assignedRM = '" + dr_datarow["assigned_RM"].ToString() + "' and allocation_status = 'Allocated') as b, " +
                           " (select count(*) as count_overall from rsk_trn_tallocationdtl where zonal_gid = '" + zonalmapping_gid + "' " +
                           " and allocation_assignedRM = '" + dr_datarow["assigned_RM"].ToString() + "' and allocation_status = 'Allocated') as c";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsallocation_pendingcount = Convert.ToInt16(objODBCDatareader["count_overall"].ToString());
                        lscount_fresh = objODBCDatareader["count_fresh"].ToString();
                        lscount_revisit = objODBCDatareader["count_revisit"].ToString();
                    }
                    objODBCDatareader.Close();

                    if (lsallocation_pendingcount > 12)
                    {
                        lscount_status = "Y";
                    }
                    else
                    {
                        lscount_status = "N";
                    }
                    getzonalallocationcount.Add(new zonalwisecount
                    {
                        riskmanager_name = dr_datarow["riskmanager_name"].ToString(),
                        pending_count = lsallocation_pendingcount,
                        count_status = lscount_status,
                        assigned_RM = dr_datarow["assigned_RM"].ToString(),
                        count_fresh = lscount_fresh,
                        count_revisit = lscount_revisit,
                    });
                }
                values.zonalwisecount = getzonalallocationcount;
            }
            dt_datatable.Dispose();
        }
        public void DaGetOverallZonalPendingCount(overallzonalcountList objvalues)
        {
            msSQL = " select a.count_current,b.count_upcoming,g.count_breached,c.count_completed,d.count_external,f.count_reportchanges,k.count_qualified from " +
                  " (select count(*) as count_current from rsk_trn_tallocationdtl where allocation_status = 'Allocated' and visit_allocated_date <= NOW())as a," +
                  " (select count(*) as count_upcoming from rsk_trn_tallocationdtl where allocation_status = 'Allocated' and visit_allocated_date > NOW())as b," +
                  " (select count(*) as count_completed from rsk_trn_tallocation where status = 'Completed')as c," +
                  " (select count(*) as count_external from rsk_trn_tallocationdtl where allocation_status = 'External')as d," +
                  " (select count(*) as count_pending from rsk_trn_tallocation where status = 'Pending')as e ," +
                  " (select count(*) as count_reportchanges from rsk_trn_tallocation where reportcancel_flag = 'Y')as f," +
                  " (select count(*) as count_breached from rsk_trn_tallocationdtl" +
                  " where DATEDIFF(CURDATE(), visit_allocated_date) > 90 and lastvisit_date is null)as g," +
                  " (select count(h.customer_urn) as count_qualified from ocs_mst_tcustomer h " +
                  " left join rsk_trn_tcustomervisit i on h.customer_urn = i.customer_urn" +
                  " left join rsk_trn_tcustomerdisbursement c on h.customer_urn = c.customer_urn" +
                  " where(((DATEDIFF(CURDATE(), first_disb_date)) > 31 and c.allocate_flag = 'N' and c.exclusion_flag='N' and lastvisit_date is null and ac_status = '0') or" +
                  " ((DATEDIFF(CURDATE(), lastvisit_date)) > 180 and ac_status = '0' and c.exclusion_flag='N' and c.allocate_flag = 'N'))" +
                  " and total_sanction >= 5000000) as k";
                  //" and total_sanction >= 25000) as k";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objvalues.count_current = objODBCDatareader["count_current"].ToString();
                objvalues.count_upcoming = objODBCDatareader["count_upcoming"].ToString();
                objvalues.count_completed = objODBCDatareader["count_completed"].ToString();
                objvalues.count_external = objODBCDatareader["count_external"].ToString();
                objvalues.count_breached = objODBCDatareader["count_breached"].ToString();
                objvalues.count_reportchanges = objODBCDatareader["count_reportchanges"].ToString();
                objvalues.count_qualified = objODBCDatareader["count_qualified"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select date_format(process_date,'%d-%m-%Y') as process_date, " +
                   " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as employee_name " +
                   " from lgl_trn_tmisdocumentimport a " +
                   " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                   " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                   " order by misdocumentimport_gid desc limit 1 ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objvalues.ADM_updatedby = objODBCDatareader["employee_name"].ToString();
                objvalues.ADM_updateddate = objODBCDatareader["process_date"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select zonalmapping_gid,zonal_name,zonalrisk_managername from rsk_mst_tzonalmapping order by zonal_name asc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getZonalcountdtl = new List<overallzonalcount>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    msSQL = " select count(*) from rsk_trn_tallocationdtl where zonal_gid='" + dr_datarow["zonalmapping_gid"].ToString() + "' " +
                            " and allocation_status = 'Allocated'";
                    lsallocation_pendingcount = Convert.ToInt16(objdbconn.GetExecuteScalar(msSQL));

                    getZonalcountdtl.Add(new overallzonalcount
                    {
                        zonal_name = dr_datarow["zonal_name"].ToString(),
                        zonal_riskmanager = dr_datarow["zonalrisk_managername"].ToString(),
                        zonalpending_count = lsallocation_pendingcount,
                        zonalmapping_gid = dr_datarow["zonalmapping_gid"].ToString(),
                    });
                }
                objvalues.overallzonalcount = getZonalcountdtl;
            }
            dt_datatable.Dispose();
            objvalues.status = true;
        }

        public bool DaGetMovetoCurrentAllocation(string allocationdtl_gid, string allocationmove_reason, string user_gid, result values)
        {

            msSQL = "select visit_allocated_date,MONTH(visit_allocated_date)as monthcount from rsk_trn_tallocationdtl where allocationdtl_gid='" + allocationdtl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                string lsallocated_date = objODBCDatareader["visit_allocated_date"].ToString();
                lsmonth = Convert.ToInt16(objODBCDatareader["monthcount"]);
                int sMonth = Convert.ToInt16(DateTime.Now.ToString("MM"));
                objODBCDatareader.Close();
                if (lsmonth != sMonth)
                {
                    msGet_visitGid = objcmnfunctions.GetMasterGID("ALMV");

                    msSQL = "insert into rsk_trn_tupcoming2currentallocation(" +
                           " upcoming2currentallocation_gid ," +
                           " allocationdtl_gid," +
                           " oldallocated_date," +
                           " allocation_reason," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGet_visitGid + "'," +
                           "'" + allocationdtl_gid + "'," +
                           "'" + Convert.ToDateTime(lsallocated_date).ToString("yyyy-MM-dd") + "'," +
                           "'" + allocationmove_reason.Replace("'", "\\'") + "'," +
                           "'" + user_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    do
                    {

                        msSQL = " update rsk_trn_tupcoming2currentallocation" +
                                 " set allocated_date = CONCAT(LEFT('" + Convert.ToDateTime(lsallocated_date).ToString("yyyy-MM-dd") + "' - INTERVAL 1 MONTH, 7), '-01') " +
                                 " where upcoming2currentallocation_gid = '" + msGet_visitGid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update rsk_trn_tallocationdtl" +
                            " set visit_allocated_date = CONCAT(LEFT('" + Convert.ToDateTime(lsallocated_date).ToString("yyyy-MM-dd") + "' - INTERVAL 1 MONTH, 7), '-01') " +
                            " where allocationdtl_gid = '" + allocationdtl_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        msSQL = "select MONTH(allocated_date)as monthcount,MONTH(created_date) as created_date,allocated_date from rsk_trn_tupcoming2currentallocation where upcoming2currentallocation_gid='" + msGet_visitGid + "'";
                        objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader1.HasRows == true)
                        {
                            lsmonthdesc = Convert.ToInt16(objODBCDatareader1["monthcount"]);
                            lscurmonth = Convert.ToInt16(objODBCDatareader1["created_date"]);

                            lsmontheql = lsmonthdesc - lscurmonth;
                            if (lsmontheql != 0)
                            {
                                lsallocated_date = objODBCDatareader1["allocated_date"].ToString();
                            }
                            objODBCDatareader1.Close();
                        }
                    } while (lsmontheql != 0);


                    values.message = "Allocation Moved to Current Successfully..!";
                    values.status = true;
                    return true;
                }
                else
                {
                    values.message = "Allocation Already moved to Current ..!";
                    values.status = false;
                    return false;
                }
            }
            else
            {
                objODBCDatareader.Close();
                values.message = "Error Occured..!";
                values.status = false;
                return false;
            }

        }

        public bool DaGetAllocationReport(allocationlist values)
        {
            msSQL = " select a.customer_gid,f.customer_urn,format(a.sanction_amount, 2) as sanction_amount,f.vertical_code,f.customername,p.entity_name, " +
                   " n.sanction_date, e.zonal_name, b.state_name, b.district_name," +
                   " b.allocationdtl_gid, date_format(b.created_date,'%d-%m-%Y') as allocated_date, " +
                   " case when (b.allocation_status = 'Allocated' and visit_allocated_date > NOW()) then 'Upcoming' " +
                   " when (b.allocation_status = 'Allocated' and visit_allocated_date <= NOW()) then 'Current' else b.allocation_status end as allocation_status, " +
                   " cast(monthname(visit_allocated_date) as char) as visitallocatemonth, cast(monthname(b.created_date) as char) as allocatedmonth," +
                   " concat(h.user_firstname,' ',h.user_lastname,' / ',h.user_code) as allocated_by, i.typeof_riskreview, i.typeof_loanvertical, " +
                   " f.constitution_name, i.turnover_lastFY, b.assignedRM_name,b.zonalRM_name, m.appointment_time, m.schedule_status," +
                   " date_format(m.appointment_date,'%d-%m-%Y') as appointmentdate, b.completed_date," +
                   " j.observation_flag, k.tier1_approvalstatus, l.tier3_flag,l.tier2_approvedflag,k.tier1format_gid, q.tier2_approval_status, "+
                   " date_format(x.lastvisit_date, '%d-%m-%Y') as lastvisit_date,DATEDIFF(CURDATE(), x.lastvisit_date) as count_visit" +
                   " from rsk_trn_tallocation a" +
                   " left join rsk_trn_tallocationdtl b on a.allocation_gid = b.allocation_gid " +
                   " left join rsk_trn_tcustomervisit d on d.customer_gid = a.customer_gid" +
                   " left join rsk_mst_tzonalmapping e on b.zonal_gid = e.zonalmapping_gid" +
                   " left join ocs_mst_tcustomer f on f.customer_gid = a.customer_gid" +
                   " left join hrm_mst_temployee g on g.employee_gid = b.allocated_by" +
                   " left join adm_mst_tuser h on h.user_gid=g.user_gid " +
                   " left join rsk_trn_tvisitreportgenerate i on i.allocationdtl_gid = b.allocationdtl_gid" +
                   " left join rsk_trn_tobservationreport j on b.allocationdtl_gid = j.allocationdtl_gid " +
                   " left join rsk_trn_ttier1format k on j.observation_reportgid = k.observation_reportgid " +
                   " left join rsk_trn_ttierallocationdtl l on l.allocationdtl_gid= b.allocationdtl_gid " +
                   " left join rsk_trn_tschedulelog m on m.allocationdtl_gid = b.allocationdtl_gid" +
                   " left join rsk_trn_tcustomerdisbursement c on f.customer_urn = c.customer_urn" +
                   " left join ocs_mst_tcustomer2sanction n on n.customer_gid=a.customer_gid" +
                   " left join adm_mst_tentity p on p.entity_gid=n.entity_gid " +
                   " left join rsk_trn_ttier2preparation q on q.tier2preparation_gid = l.tier2preparation_gid "+
                   " left join rsk_trn_tcustomervisit x on x.customer_gid = a.customer_gid group by b.allocationdtl_gid order by b.allocationdtl_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_allocationdtl = new List<allocationdtl>();
            values.count_currentallo = dt_datatable.Rows.Count;
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_allocationdtl.Add(new allocationdtl
                    {
                        customer_gid = dt["customer_gid"].ToString(),
                        customername = dt["customername"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        entity_name = dt["entity_name"].ToString(),
                        vertical = dt["vertical_code"].ToString(),
                        constitution = dt["constitution_name"].ToString(),
                        sanction_amount = dt["sanction_amount"].ToString(),
                        sanction_date = dt["sanction_date"].ToString(),
                        zonal_name = dt["zonal_name"].ToString(),
                        state_name = dt["state_name"].ToString(),
                        district_name = dt["district_name"].ToString(),
                        ZonalRMname = dt["zonalRM_name"].ToString(),
                        assigned_RM = dt["assignedRM_name"].ToString(),
                        visit_allocatemonth = dt["visitallocatemonth"].ToString(),
                        created_by = dt["allocated_by"].ToString(),
                        allocationdtl_gid = dt["allocationdtl_gid"].ToString(),
                        allocated_date = dt["allocated_date"].ToString(),
                        typeof_loanvertical = dt["typeof_loanvertical"].ToString(),
                        typeof_riskreview = dt["typeof_riskreview"].ToString(),
                        turnover_lastFY = dt["turnover_lastFY"].ToString(),
                        visited_date = dt["completed_date"].ToString(),
                        appointment_date = dt["appointmentdate"].ToString(),
                        appointment_time = dt["appointment_time"].ToString(),
                        schedule_status = dt["schedule_status"].ToString(),
                        allocation_status = dt["allocation_status"].ToString(),
                        observation_flag = dt["observation_flag"].ToString(),
                        tier1_approvalstatus = dt["tier1_approvalstatus"].ToString(),
                        tier2_flag = dt["tier2_approvedflag"].ToString(),
                        tier3_flag = dt["tier3_flag"].ToString(),
                        tier1format_gid = dt["tier1format_gid"].ToString(),
                        allocatedmonth = dt["allocatedmonth"].ToString(),
                        tier2_approval_status = dt["tier2_approval_status"].ToString(),
                        lastvisit_date = dt["lastvisit_date"].ToString(),
                        count_lastvisit = dt["count_visit"].ToString(),
                    });
                }
                values.allocationdtl = get_allocationdtl;
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaGetAllocationReportExcel(exportAllocation values)
        {
           msSQL = " select p.entity_name as Entity_Name, f.customer_urn as Customer_URN, f.customername as Customer_Name, f.constitution_name as Constitution_Name, f.vertical_code as Vertical_Code, " +
                   " format(a.sanction_amount, 2) as Sanction_Amount, n.sanction_date as Sanction_Date, e.zonal_name as Zone_Name, b.state_name as State_Name," +
                   " b.district_name as District_Name, b.zonalRM_name as Zonal_Risk_Manager," +
                   " b.assignedRM_name as Risk_Manager,cast(monthname(visit_allocated_date) as char) as Visit_Month," +
                   " concat(h.user_firstname,' ',h.user_lastname,' / ',h.user_code) as Allocated_By, " +
                   " date_format(b.created_date,'%d-%m-%Y') as Allocated_Date, cast(monthname(b.created_date) as char) as Allocated_Month, " +
                   " i.typeof_loanvertical as Type_of_Loan_Vertical," +
                   " i.typeof_riskreview as Type_of_Risk_Review, i.turnover_lastFY as Turnover_of_last_FY," +
                   " date_format(b.completed_date,'%d-%m-%Y') as Visit_Submission_Date, m.schedule_status as Schedule_Log_Details, " +
                   " date_format(x.lastvisit_date, '%d-%m-%Y') as Last_Visited_Date, DATEDIFF(CURDATE(), x.lastvisit_date) as Days_passed_Visit," +
                   " case when (b.allocation_status = 'Allocated' and visit_allocated_date > NOW()) then 'Upcoming' " +
                   " when (b.allocation_status = 'Allocated' and visit_allocated_date <= NOW()) then 'Current' else b.allocation_status end as allocation_status, " +
                   " case when j.observation_flag='N' then 'Pending' when j.observation_flag='Y' then'Approved' else '-' end as ATR_Status, " +
                   " k.tier1_approvalstatus, q.tier2_approval_status as Tier2_Status, " +
                   " case when l.tier3_flag='N' then 'Pending' when l.tier3_flag='Y' then'Completed' else '-' end as Tier3_Status from rsk_trn_tallocation a " +
                   " left join rsk_trn_tallocationdtl b on a.allocation_gid = b.allocation_gid " +
                   " left join rsk_trn_tcustomervisit d on d.customer_gid = a.customer_gid" +
                   " left join rsk_mst_tzonalmapping e on b.zonal_gid = e.zonalmapping_gid" +
                   " left join ocs_mst_tcustomer f on f.customer_gid = a.customer_gid" +
                   " left join hrm_mst_temployee g on g.employee_gid = b.allocated_by" +
                   " left join adm_mst_tuser h on h.user_gid=g.user_gid " +
                   " left join rsk_trn_tvisitreportgenerate i on i.allocationdtl_gid = b.allocationdtl_gid" +
                   " left join rsk_trn_tobservationreport j on b.allocationdtl_gid = j.allocationdtl_gid " +
                   " left join rsk_trn_ttier1format k on j.observation_reportgid = k.observation_reportgid " +
                   " left join rsk_trn_ttierallocationdtl l on l.allocationdtl_gid= b.allocationdtl_gid " +
                   " left join rsk_trn_tschedulelog m on m.allocationdtl_gid = b.allocationdtl_gid" +
                   " left join rsk_trn_tcustomerdisbursement c on f.customer_urn = c.customer_urn " +
                   " left join ocs_mst_tcustomer2sanction n on n.customer_gid=a.customer_gid" +
                   " left join adm_mst_tentity p on p.entity_gid=n.entity_gid " +
                   " left join rsk_trn_ttier2preparation q on q.tier2preparation_gid = l.tier2preparation_gid "+
                   " left join rsk_trn_tcustomervisit x on x.customer_gid = a.customer_gid group by b.allocationdtl_gid order by b.allocationdtl_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                string lscompany_code = string.Empty;
                MemoryStream ms = new MemoryStream();
                ExcelPackage excel = new ExcelPackage(ms);
                var workSheet = excel.Workbook.Worksheets.Add("AllocationReport");
                try
                {
                    msSQL = " select company_code from adm_mst_tcompany";

                    lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                    values.excel_path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "RSK/AllocationReportDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                    {
                        if ((!System.IO.Directory.Exists(values.excel_path)))
                            System.IO.Directory.CreateDirectory(values.excel_path);
                    }

                    values.excel_name = "AllocationReport" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".xlsx";
                    values.excel_path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "RSK/AllocationReportDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.excel_name;

                    workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                    FileInfo file = new FileInfo(values.excel_path);
                    using (var range = workSheet.Cells[1, 1, 1, 28])  //Address "A1:A18"
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.Green);
                        range.Style.Font.Color.SetColor(Color.White);
                    }
                    excel.SaveAs(ms);
                    dt_datatable.Dispose();
                    values.excel_path = lscompany_code + "/" + "RSK/AllocationReportDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.excel_name;
                    bool status;
                    status = objcmnstorage.UploadStream("erpdocument", values.excel_path, ms);
                    values.status = status;
                   
                    ms.Close();


                }
                catch (Exception ex)
                {
                    dt_datatable.Dispose();
                    values.message = ex.ToString();
                    values.status = false;
                }
            }
            else
            {
                dt_datatable.Dispose();
                values.message = "No Records Found..!";
                values.status = false;
            }
            values.excel_path = objcmnstorage.EncryptData((values.excel_path));
            return true;
        }

        public void GetAllocationReportSummaryreport(allocationSummary values)
        {
            try
            {
                msSQL = " select a.customer_gid,f.customer_urn,format(a.sanction_amount, 2) as sanction_amount,f.vertical_code,f.customername,p.entity_name, " +
                   " n.sanction_date, e.zonal_name, b.state_name, b.district_name," +
                   " b.allocationdtl_gid, date_format(b.created_date,'%d-%m-%Y') as allocated_date, " +
                   " case when (b.allocation_status = 'Allocated' and visit_allocated_date > NOW()) then 'Upcoming' " +
                   " when (b.allocation_status = 'Allocated' and visit_allocated_date <= NOW()) then 'Current' else b.allocation_status end as allocation_status, " +
                   " cast(monthname(visit_allocated_date) as char) as visitallocatemonth, cast(monthname(b.created_date) as char) as allocatedmonth," +
                   " concat(h.user_firstname,' ',h.user_lastname,' / ',h.user_code) as allocated_by, i.typeof_riskreview, i.typeof_loanvertical, " +
                   " f.constitution_name, i.turnover_lastFY, b.assignedRM_name,b.zonalRM_name, m.appointment_time, m.schedule_status," +
                   " date_format(m.appointment_date,'%d-%m-%Y') as appointmentdate, b.completed_date," +
                   " j.observation_flag, k.tier1_approvalstatus, l.tier3_flag,l.tier2_approvedflag,k.tier1format_gid, "+
                   " date_format(x.lastvisit_date, '%d-%m-%Y') as lastvisit_date,DATEDIFF(CURDATE(), x.lastvisit_date) as count_visit" +
                   " from rsk_trn_tallocation a" +
                   " left join rsk_trn_tallocationdtl b on a.allocation_gid = b.allocation_gid " +
                   " left join rsk_trn_tcustomervisit d on d.customer_gid = a.customer_gid" +
                   " left join rsk_mst_tzonalmapping e on b.zonal_gid = e.zonalmapping_gid" +
                   " left join ocs_mst_tcustomer f on f.customer_gid = a.customer_gid" +
                   " left join hrm_mst_temployee g on g.employee_gid = b.allocated_by" +
                   " left join adm_mst_tuser h on h.user_gid=g.user_gid " +
                   " left join rsk_trn_tvisitreportgenerate i on i.allocationdtl_gid = b.allocationdtl_gid" +
                   " left join rsk_trn_tobservationreport j on b.allocationdtl_gid = j.allocationdtl_gid " +
                   " left join rsk_trn_ttier1format k on j.observation_reportgid = k.observation_reportgid " +
                   " left join rsk_trn_ttierallocationdtl l on l.allocationdtl_gid= b.allocationdtl_gid " +
                   " left join rsk_trn_tschedulelog m on m.allocationdtl_gid = b.allocationdtl_gid" +
                   " left join rsk_trn_tcustomerdisbursement c on f.customer_urn = c.customer_urn" +
                   " left join ocs_mst_tcustomer2sanction n on n.customer_gid=a.customer_gid" +
                   " left join adm_mst_tentity p on p.entity_gid=n.entity_gid "+
                   " left join rsk_trn_tcustomervisit x on x.customer_gid = a.customer_gid where";

                if (values.customer_gid == null || values.customer_gid == "")
                {
                    msSQL += "  1=1 ";
                }
                else
                {

                    msSQL += "   b.customer_gid = '" + values.customer_gid + "'";
                }
                if (values.zonalmapping_gid == null || values.zonalmapping_gid == "")
                {
                    msSQL += " and  1=1 ";
                }
                else
                {

                    msSQL += " and  b.zonal_gid = '" + values.zonalmapping_gid + "'";
                }
                if (values.zonalrisk_manager == null || values.zonalrisk_manager == "")
                {
                    msSQL += " and 1=1 ";
                }
                else
                {

                    msSQL += " and b.allocation_zonalRM = '" + values.zonalrisk_manager + "'";
                }
                
                if (values.risk_manager == null || values.risk_manager == "")
                {
                    msSQL += " and 1=1 ";
                }
                else
                {

                    msSQL += " and b.allocation_assignedRM = '" + values.risk_manager + "'";
                }

                msSQL += " group by b.allocationdtl_gid order by b.allocationdtl_gid desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_allocationdtl = new List<allocationdtl>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        get_allocationdtl.Add(new allocationdtl
                        {
                            customer_gid = dt["customer_gid"].ToString(),
                            customername = dt["customername"].ToString(),
                            customer_urn = dt["customer_urn"].ToString(),
                            entity_name = dt["entity_name"].ToString(),
                            vertical = dt["vertical_code"].ToString(),
                            constitution = dt["constitution_name"].ToString(),
                            sanction_amount = dt["sanction_amount"].ToString(),
                            sanction_date = dt["sanction_date"].ToString(),
                            zonal_name = dt["zonal_name"].ToString(),
                            state_name = dt["state_name"].ToString(),
                            district_name = dt["district_name"].ToString(),
                            ZonalRMname = dt["zonalRM_name"].ToString(),
                            assigned_RM = dt["assignedRM_name"].ToString(),
                            visit_allocatemonth = dt["visitallocatemonth"].ToString(),
                            created_by = dt["allocated_by"].ToString(),
                            allocationdtl_gid = dt["allocationdtl_gid"].ToString(),
                            allocated_date = dt["allocated_date"].ToString(),
                            typeof_loanvertical = dt["typeof_loanvertical"].ToString(),
                            typeof_riskreview = dt["typeof_riskreview"].ToString(),
                            turnover_lastFY = dt["turnover_lastFY"].ToString(),
                            visited_date = dt["completed_date"].ToString(),
                            appointment_date = dt["appointmentdate"].ToString(),
                            appointment_time = dt["appointment_time"].ToString(),
                            schedule_status = dt["schedule_status"].ToString(),
                            allocation_status = dt["allocation_status"].ToString(),
                            observation_flag = dt["observation_flag"].ToString(),
                            tier1_approvalstatus = dt["tier1_approvalstatus"].ToString(),
                            tier2_flag = dt["tier2_approvedflag"].ToString(),
                            tier3_flag = dt["tier3_flag"].ToString(),
                            tier1format_gid = dt["tier1format_gid"].ToString(),
                            allocatedmonth = dt["allocatedmonth"].ToString(),
                            lastvisit_date = dt["lastvisit_date"].ToString(),
                            count_lastvisit = dt["count_visit"].ToString(),
                        });
                    }
                    values.allocationdtl = get_allocationdtl;
                }
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Success";
            }
            catch
            {
                values.status = false;
                values.message = "Failure";
            }
        }

        public void DaGetRiskCustomerList(Customers values, string customer_gid)
        {
            msSQL = " (select customer_gid,customername, customer_urn from ocs_mst_tcustomer a where customer_gid='"+ customer_gid +"' "+
                    " and (customer_urn<>'' or customer_urn<>null) group by customer_gid)" +
                    " UNION " +
                    " (select tmpcustomer_gid,customername, customer_urn from ocs_tmp_tcustomer a where tmpcustomer_gid='" + customer_gid + "' "+
                    " and (customer_urn<>'' or customer_urn<>null) group by tmpcustomer_gid)";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if(objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                msSQL =" (select customer_gid,customername,customer_urn from ocs_mst_tcustomer a where customer_gid='" + customer_gid + "' and total_sanction >= 5000000 "+
                    " group by customer_gid order by customername asc)" +
                    " UNION " +
                    " (select tmpcustomer_gid,customername,customer_urn from ocs_tmp_tcustomer a where tmpcustomer_gid='" + customer_gid + "' and total_sanction >= 5000000 "+
                    " group by tmpcustomer_gid order by customername asc)";
                objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                if(objODBCDatareader1.HasRows == true)
                {
                    values.customername = objODBCDatareader1["customername"].ToString();
                    objODBCDatareader1.Close();
                    msSQL = "select exclusion_historygid from rsk_trn_texclusionhistory where customer_name='" + values.customername +"'";
                    objODBCDatareader2 = objdbconn.GetDataReader(msSQL);
                    if(objODBCDatareader2.HasRows == false)
                    {
                        objODBCDatareader2.Close();
                        msSQL = " select count(allocation_status) from rsk_trn_tallocationdtl where customer_gid = '" + customer_gid +"'";
                        string lstotalallocation_count = objdbconn.GetExecuteScalar(msSQL);
                        msSQL = " select count(allocation_status) from rsk_trn_tallocationdtl where customer_gid = '" + customer_gid + "' " +
                                " and (allocation_status='Completed' or allocation_status='Excluded')";
                        string lsallocation_count = objdbconn.GetExecuteScalar(msSQL);
                        if(lstotalallocation_count != lsallocation_count)
                        {
                            values.message = "You can't allocate the customer, its visit status is pending.";
                            values.status = false;
                        }
                        else
                        {
                            values.status = true;
                        }
                    }
                    else
                    {
                        objODBCDatareader2.Close();
                        values.message = "You can't allocate the customer, its in exclusion list.";
                        values.status = false;
                    }
                }
                else
                {
                    objODBCDatareader1.Close();
                    values.message = "You can't allocate the customer, its sanction amount is less than 5000000.";
                    values.status = false;
                }
            }
            else
            {
                objODBCDatareader.Close();
                values.message = "You can't allocate the customer, its customer URN is empty.";
                values.status = false;
            }
        }
    }
}