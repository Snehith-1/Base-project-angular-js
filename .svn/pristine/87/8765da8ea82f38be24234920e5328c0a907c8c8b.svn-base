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
    public class DaCustomerManagement
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objodbcdatareader;
        DataTable dt_datatable;
        string msSQL, msGetGid;
        int mnresult, lspromotor_age, lsguarantor_age;
        double lspromotor_mobile;


        public bool DaGetCustomerList(customerlist objCustomer)
        {

            msSQL = " select a.customer_gid,a.customer_code,a.customername " +
                    " from ocs_mst_tcustomer a order by a.customername desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcustomer = new List<customernamedtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcustomer.Add(new customernamedtl
                    {
                        customer_gid = (dr_datarow["customer_gid"].ToString()),
                        customername = (dr_datarow["customername"].ToString()),
                    });
                }
                objCustomer.customernamedtl = getcustomer;
            }
            dt_datatable.Dispose();
            return true;
        }


        public void DaPostCustomerPromoter(customerPromoter values, string employee_gid)
        {

            msGetGid = objcmnfunctions.GetMasterGID("CUPR");
            msSQL = "insert into ocs_mst_tcustomer2promotor(" +
                      " customer2promotor_gid  ," +
                      " customer_gid ," +
                      " promoter_name ," +
                      " designation ," +
                      " promoter_age ," +
                      " mobile ," +
                      " created_by," +
                      " created_date)" +
                      " values(" +
                      "'" + msGetGid + "'," +
                      "'" + values.customer_gid + "'," +
                      "'" + values.promoter_name + "'," +
                      "'" + values.designation + "'," +
                      "'" + values.promoter_age + "'," +
                      "'" + values.mobile + "'," +
                      "'" + employee_gid + "'," +
                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnresult != 0)
            {
                values.status = true;
                values.message = "Customer Promotor Details Added Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }

        }


        public void DaPostcustomerGuarantors(customerGuarantors values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("CUGR");
            msSQL = "insert into ocs_mst_tcustomer2guarantor(" +
                      " customer2guarantor_gid  ," +
                      " customer_gid ," +
                      " guarantors_name ," +
                      " guarantor_age ," +
                      " networth ," +
                      " basisofNW ," +
                      " created_by," +
                      " created_date)" +
                      " values(" +
                      "'" + msGetGid + "'," +
                      "'" + values.customer_gid + "'," +
                      "'" + values.guarantors_name + "'," +
                      "'" + values.guarantor_age + "'," +
                      "'" + values.networth + "'," +
                      "'" + values.basisofNW + "'," +
                      "'" + employee_gid + "'," +
                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult != 0)
            {
                values.status = true;
                values.message = "Customer Guarantors Details Added Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }

        }

        public void DaPostCustomerPromoterEdit(customerPromoter values, string employee_gid)
        {

            msSQL = "update ocs_mst_tcustomer2promotor set" +
                    " promoter_name ='" + values.promoter_name + "'," +
                    " designation ='" + values.designation + "'," +
                    " promoter_age='" + values.promoter_age + "'," +
                    " mobile='" + values.mobile + "'," +
                    " updated_by ='" + employee_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where customer2promotor_gid='" + values.customer2promotor_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult != 0)
            {
                values.status = true;
                values.message = "Customer Promotor Details Updated Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }

        }


        public void DaPostCustomerGuarantorsEdit(customerGuarantors values, string employee_gid)
        {

            msSQL = "update ocs_mst_tcustomer2guarantor set" +
                   " guarantors_name ='" + values.guarantors_name + "'," +
                   " guarantor_age ='" + values.guarantor_age + "'," +
                   " networth='" + values.networth + "'," +
                   " basisofNW='" + values.basisofNW + "'," +
                   " updated_by ='" + employee_gid + "'," +
                   " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where customer2guarantor_gid='" + values.customer2guarantor_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnresult != 0)
            {
                values.status = true;
                values.message = "Customer Guarantors Details Updated Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }

        }

        public bool DaGetCustomerGuarantors(string customer_gid, customerGuarantorslist values)
        {

            msSQL = " select customer2guarantor_gid,guarantors_name,guarantor_age,networth,basisofNW " +
                    " from ocs_mst_tcustomer2guarantor where customer_gid='" + customer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_customerGurantors = new List<customerGuarantors>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    if (dr_datarow["guarantor_age"].ToString() != "")
                    {
                        lsguarantor_age = Convert.ToInt16(dr_datarow["guarantor_age"].ToString());
                    }

                    get_customerGurantors.Add(new customerGuarantors
                    {
                        guarantors_name = (dr_datarow["guarantors_name"].ToString()),
                        guarantor_age = lsguarantor_age,
                        networth = (dr_datarow["networth"].ToString()),
                        basisofNW = (dr_datarow["basisofNW"].ToString()),
                        customer2guarantor_gid = (dr_datarow["customer2guarantor_gid"].ToString())
                    });
                }
                values.customerGuarantors = get_customerGurantors;
            }
            dt_datatable.Dispose();

            return true;

        }

        public bool DaGetCustomerPromoter(string customer_gid, customerpromotorslist values)
        {
            msSQL = " select concat(c.user_firstname,' ',c.user_lastname,'/',c.user_code) as PPAname from ocs_mst_tcustomer a " +
                    " left join hrm_mst_temployee b on a.ppa_gid = b.employee_gid " +
                    " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                    " where customer_gid = '" + customer_gid + "'";
            values.PPAname = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select customer2promotor_gid,promoter_name,designation,promoter_age,mobile " +
                    " from ocs_mst_tcustomer2promotor where customer_gid='" + customer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_customerPromotor = new List<customerPromoter>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    if (dr_datarow["promoter_age"].ToString() != "")
                    {
                        lspromotor_age = Convert.ToInt16(dr_datarow["promoter_age"].ToString());
                    }

                    if (dr_datarow["mobile"].ToString() != "")
                    {
                        lspromotor_mobile = Convert.ToDouble(dr_datarow["mobile"].ToString());
                    }


                    get_customerPromotor.Add(new customerPromoter
                    {
                        customer2promotor_gid = (dr_datarow["customer2promotor_gid"].ToString()),
                        promoter_name = (dr_datarow["promoter_name"].ToString()),
                        designation = (dr_datarow["designation"].ToString()),
                        promoter_age = lspromotor_age,
                        mobile = lspromotor_mobile,
                    });
                }
                values.customerPromoter = get_customerPromotor;
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaPostPromoterdetail(string customer2promotor_gid, customerPromoter values)
        {

            msSQL = "delete from ocs_mst_tcustomer2promotor where customer2promotor_gid ='" + customer2promotor_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnresult != 0)
            {
                values.status = true;
                values.message = " Deleted Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }

        }

        public bool DaPostGuarantorsDetail(string customer2guarantor_gid, customerGuarantors values)
        {

            msSQL = "delete from ocs_mst_tcustomer2guarantor where customer2guarantor_gid ='" + customer2guarantor_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnresult != 0)
            {
                values.status = true;
                values.message = "Deleted Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }

        }

        public bool DaGetPromoterdetail(string customer2promotor_gid, customerPromoter values)
        {

            msSQL = "select promoter_name,designation,promoter_age,mobile from ocs_mst_tcustomer2promotor " +
                    " where customer2promotor_gid='" + customer2promotor_gid + "'";
            objodbcdatareader = objdbconn.GetDataReader(msSQL);
            if (objodbcdatareader.HasRows == true)
            {
                values.promoter_name = objodbcdatareader["promoter_name"].ToString();
                values.designation = objodbcdatareader["designation"].ToString();
                if (objodbcdatareader["promoter_age"].ToString() != "")
                {
                    values.promoter_age = Convert.ToInt32(objodbcdatareader["promoter_age"].ToString());
                }
                if (objodbcdatareader["mobile"].ToString() != "")
                {
                    values.mobile = Convert.ToDouble(objodbcdatareader["mobile"].ToString());
                }

            }
            objodbcdatareader.Close();

            return true;
        }

        public bool DaGetGuarantorsdetail(string customer2guarantor_gid, customerGuarantors values)
        {
            msSQL = " select guarantors_name,guarantor_age,networth,basisofNW from ocs_mst_tcustomer2guarantor " +
                    " where customer2guarantor_gid='" + customer2guarantor_gid + "'";
            objodbcdatareader = objdbconn.GetDataReader(msSQL);
            if (objodbcdatareader.HasRows == true)
            {
                values.guarantors_name = objodbcdatareader["guarantors_name"].ToString();
                if (objodbcdatareader["guarantor_age"].ToString() != "")
                {
                    values.guarantor_age = Convert.ToInt32(objodbcdatareader["guarantor_age"].ToString());
                }
                values.networth = objodbcdatareader["networth"].ToString();
                values.basisofNW = objodbcdatareader["basisofNW"].ToString();
            }
            objodbcdatareader.Close();

            return true;
        }

        public bool DaGetCollateraldetail(string customer_gid, customerCollaterallist values)
        {
  
            msSQL = " select a.customer_name,a.security_type,a.security_description,a.account_status,b.loanref_no, " +
                    " b.loan_title,b.sanctionref_no,b.sanction_date from ocs_trn_tcustomercollateral a " +
                    " left join ocs_trn_tcollateral2loan b on a.collateral_gid = b.collateral_gid where customer_gid = '" + customer_gid + "'" +
                    " group by a.collateral_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_customerCollateral = new List<customerCollateral>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_customerCollateral.Add(new customerCollateral
                    {
                        customer_name = (dr_datarow["customer_name"].ToString()),
                        security_type = (dr_datarow["security_type"].ToString()),
                        security_description = (dr_datarow["security_description"].ToString()),
                        account_status = (dr_datarow["account_status"].ToString()),

                    });
                }
                values.customerCollateral = get_customerCollateral;
            }
            dt_datatable.Dispose();
 
            return true;
        }

        public bool DaGetSanctionDtl(string customer_gid, sanctiondetailsList values)
        {

            msSQL = " select sanction_refno,date_format(sanction_date,'%d-%m-%Y') as sanction_date, " +
                    " format(sanction_amount,2) as sanction_amount,sanction_limit " +
                    " from ocs_mst_tcustomer2sanction where customer_gid='" + customer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_customerSanction = new List<sanctiondetails>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_customerSanction.Add(new sanctiondetails
                    {
                        sanction_refno = (dr_datarow["sanction_refno"].ToString()),
                        sanction_date = (dr_datarow["sanction_date"].ToString()),
                        sanction_amount = (dr_datarow["sanction_amount"].ToString()),
                        sanction_limit = (dr_datarow["sanction_limit"].ToString()),
                    });
                }
                values.sanctiondetails = get_customerSanction;
            }
            dt_datatable.Dispose();

            return true;
        }


        public bool DaGetAssignRMdetail(string customer_gid, assignedRM values)
        {
 
            msSQL = " select concat(customername,' / ',customer_urn) as customername,address,address2,region,ppa_gid, " +
                    "  state_gid,state,district_gid,assigned_RM,zonal_riskmanager " +
                    " from ocs_mst_tcustomer where customer_gid='" + customer_gid + "'";
            objodbcdatareader = objdbconn.GetDataReader(msSQL);
            if (objodbcdatareader.HasRows == true)
            {
                values.state_gid = objodbcdatareader["state_gid"].ToString();
                values.state_name = objodbcdatareader["state"].ToString();
                values.district_gid = objodbcdatareader["district_gid"].ToString();
                values.assigned_RM = objodbcdatareader["assigned_RM"].ToString();
                values.zonal_riskmanager = objodbcdatareader["zonal_riskmanager"].ToString();
                values.customer_name = objodbcdatareader["customername"].ToString();
                values.addressline1 = objodbcdatareader["address"].ToString();
                values.addressline2 = objodbcdatareader["address2"].ToString();
                values.ppa_gid = objodbcdatareader["ppa_gid"].ToString();
            }
            objodbcdatareader.Close();

            msSQL = " select a.zonalmapping_gid,zonal_name from rsk_mst_trmmapping a " +
                  " left join rsk_mst_tzonalmapping b on b.zonalmapping_gid = a.zonalmapping_gid " +
                  " where a.state_gid = '" + values.state_gid + "' limit 0,1";
            objodbcdatareader = objdbconn.GetDataReader(msSQL);
            if (objodbcdatareader.HasRows == true)
            {
                values.zonal_gid = objodbcdatareader["zonalmapping_gid"].ToString();
                values.zonal_name = objodbcdatareader["zonal_name"].ToString();
            }
            objodbcdatareader.Close();

            msSQL = "select district_gid,district_name from rsk_mst_tRMmapping where state_gid like '%" + values.state_gid + "%' order by district_name asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_districtdtl = new List<districtdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_districtdtl.Add(new districtdtl
                    {
                        district_gid = (dr_datarow["district_gid"].ToString()),
                        district_name = (dr_datarow["district_name"].ToString()),
                    });
                }
                values.districtdtl = get_districtdtl;
            }
            dt_datatable.Dispose();

            return true;
        }


        public bool DaGetsanctionloandetails(sanctionloan values, string customer_gid)
        {
            msSQL = " select customer2sanction_gid,a.sanction_refno,date_format(a.sanction_date,'%d-%m-%Y') as sanctiondate, " +
                  " a.sanction_type,format(sanction_amount, 2) as sanction_amount,facility_type," +
                  " a.entity,a.colanding_status,a.colander_name from ocs_mst_tcustomer2sanction a " +
                  " where a.customer_gid = '" + customer_gid + "' order by a.customer2sanction_gid desc ";

            //msSQL = " select customer2sanction_gid,a.sanction_refno,date_format(a.sanction_date,'%d-%m-%Y') as sanctiondate,a.tenure_months, " +
            //        " format(sanction_amount, 2) as sanction_amount,facility_type,concat(sanction_limit,'.00') as sanction_limit" +
            //        " from ocs_mst_tcustomer2sanction a " +
            //        " where a.customer_gid = '" + customer_gid + "' order by a.customer2sanction_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getsanctionlistdtl = new List<sanctionloanList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getsanctionlistdtl.Add(new sanctionloanList
                    {
                        sanction_amount = (dr_datarow["sanction_amount"].ToString()),
                        sanction_refno = (dr_datarow["sanction_refno"].ToString()),
                        sanction_date = (dr_datarow["sanctiondate"].ToString()),
                        sanction_gid = (dr_datarow["customer2sanction_gid"].ToString()),
                        facility_type = (dr_datarow["facility_type"].ToString()),
                        sanction_type = (dr_datarow["sanction_type"].ToString()),
                        entity = (dr_datarow["entity"].ToString()),
                        colanding_status = (dr_datarow["colanding_status"].ToString()),
                        colander_name = (dr_datarow["colander_name"].ToString()),
                        //sanction_amount = (dr_datarow["sanction_amount"].ToString()),
                        //sanction_limit = dr_datarow["sanction_limit"].ToString(),
                        //sanction_refno = (dr_datarow["sanction_refno"].ToString()),
                        //sanction_date = (dr_datarow["sanctiondate"].ToString()),
                        //sanction_gid=(dr_datarow["customer2sanction_gid"].ToString()),
                        //facility_type=(dr_datarow["facility_type"].ToString()),
                        //tenure_months=(dr_datarow["tenure_months"].ToString()),
                    });
                    values.sanctionloanList = getsanctionlistdtl;
                }
            }
            dt_datatable.Dispose();

            msSQL = " select file_name,file_path,document_name,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by, " +
                    " date_format(a.updated_date,'%d-%m-%Y') as updated_date   from rsk_mst_tsanctiondocumentdtl a " +
                    " left join hrm_mst_temployee b on b.employee_gid=a.updated_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " where customer_gid='" + customer_gid + "' and file_path<>''";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_fileseekname = new List<upload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_fileseekname.Add(new upload_list
                    {
                        document_name = dr_datarow["file_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dr_datarow["file_path"].ToString()),
                        document_type = dr_datarow["document_name"].ToString(),
                        created_by = dr_datarow["updated_by"].ToString(),
                        created_date = dr_datarow["updated_date"].ToString(),
                    });
                }
                values.upload_list = get_fileseekname;
            }
            dt_datatable.Dispose();

           
            values.status = true;
            return true;
        }

        public void DaGetloanListDetails(string sanction_gid, loanListdetail values)
        {
            msSQL = "select format(loanfacility_amount,2) as loanfacility_amount,loanfacility_type, " +
                  " format(document_limit,2) as document_limit,date_format(expiry_date, '%d-%m-%Y') as expiry_date,tenure,loanfacilityref_no,proposed_roi" +
                  " from ocs_mst_tsanction2loanfacilitytype  where customer2sanction_gid='" + sanction_gid + "'";

            //msSQL = " select sanction_gid,loanref_no,loan_title from ocs_trn_tloan " +
            //        " where sanction_gid='" + sanction_gid + "'";
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

        public void DaGetCustomerRMDetail(customerRMdtl objCustomer)
        {
            try
            {

                msSQL = " select a.customer_gid,a.customername,a.vertical_code,a.customer_urn,f.zonal_name,state,a.ppa_name, " +
                       " district_name,concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as zonalriskmanager, " +
                       " concat(e.user_firstname, ' ', e.user_lastname, '/', e.user_code) as riskmanager from ocs_mst_tcustomer a " +
                       " left join hrm_mst_temployee b on a.zonal_riskmanager = b.employee_gid " +
                       " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                       " left join hrm_mst_temployee d on a.assigned_RM = d.employee_gid " +
                       " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                       " left join rsk_mst_tzonalmapping f on a.zonal_gid = f.zonalmapping_gid " +
                       " order by a.customer_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    objCustomer.customer_list = dt_datatable.AsEnumerable().Select(row =>
                    new customer_list
                    {
                        customer_gid = (row["customer_gid"].ToString()),
                        customername = (row["customername"].ToString()),
                        vertical_code = (row["vertical_code"].ToString()),
                        customer_urn = (row["customer_urn"].ToString()),
                        state_name=(row["state"].ToString()),
                        district_name=(row["district_name"].ToString()),
                        zonal_name=(row["zonal_name"].ToString()),
                        zonal_riskmanager=(row["zonalriskmanager"].ToString()),
                        riskmanager_name=(row["riskmanager"].ToString()),
                        ppa_name=(row["ppa_name"].ToString())
                    }).ToList();
                    dt_datatable.Dispose();

                }
                objCustomer.status = true;
            }
            catch(Exception ex)
            {
                objCustomer.message = ex.Message.ToString();
                objCustomer.status = false;
            }

        }

        #region Escrow

        // Create Escrow

        public void DaPostEscrowCreate(escrow values,string user_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("ESCW");

            msSQL = " INSERT INTO rsk_mst_tcustomer2escrow(" +
                    " customer2escrow_gid," +
                    " customer2sanction_gid," +
                    " customer_gid," +
                    " disbursement_date," +
                    " transaction_date," +
                    " transactionref_no," +
                    " escrow_account_no," +
                    " dealer_name," +
                    " master_account_no," +
                    " amount," +
                    " beneficiary_customer_account_name," +
                    " sender_customer_account_name," +
                    " sender_customer_account_no," +
                    " remittance_info," +
                    " sender_branch_ifsc," +
                    " reference," +
                    " credit_time," +
                    " remarks,"+
                    " created_date,"+
                    " created_by)" +
                    " values("+
                    "'" + msGetGid + "'," +
                    "'" + values.sanction_gid + "'," +
                    "'" + values.customer_gid + "'," +
                    "'" + Convert.ToDateTime(values.disbursement_date ).ToString("yyyy-MM-dd") + "'," +
                    "'" + Convert.ToDateTime(values.transaction_date ).ToString("yyyy-MM-dd") + "'," +
                    "'" + values.transactionref_no.Trim () + "'," +
                    "'" + values.escrow_account_no.Trim () + "'," +
                    "'" + values.dealer_name.Trim () + "'," +
                    "'" + values.master_account_no.Trim () + "'," +
                    "'" + values.amount.Replace (",","") + "'," +
                    "'" + values.beneficiary_customer_account_name.Trim () + "'," +
                    "'" + values.sender_customer_account_name.Trim () + "'," +
                    "'" + values.sender_customer_account_no.Trim () + "'," +
                    "'" + values.remittance_info + "'," +
                    "'" + values.sender_branch_IFSC + "'," +
                    "'" + values.reference + "'," +
                    "'" + values.credit_time + "'," +
                    "'" + values.remarks.Replace ("'","").Trim () + "',"+
                    "CURRENT_TIMESTAMP,"+
                    "'"+ user_gid +"')";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if(mnresult ==1)
            {
                values.status = true;
                values.message = "Escrow Created Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        // Delete Escrow

        public void DaPostEscrowDelete(string escrow_gid,result objResult)
        {
            msSQL = " DELETE FROM rsk_mst_tcustomer2escrow WHERE customer2escrow_gid='" + escrow_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if(mnresult ==1)
            {
                objResult.status = true;
                objResult.message = "Escrow Deleted Successfully..!";
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured..!";
            }
        }

        // Escrow Summary

        public void DaGetEscrowSummary(string customer_gid,escrowSummaryList objResult)
        {
            msSQL = " SELECT a.customer2escrow_gid, b.sanction_refno,date_format(b.sanction_date,'%d-%m-%Y') as sanction_date,"+
                   " date_format(a.disbursement_date,'%d-%m-%Y') as disbursement_date," +
                    " a.transactionref_no,date_format(a.transaction_date, '%d-%m-%Y') as transaction_date," +
                    " a.amount,date_format(a.created_date, '%d-%m-%Y') as created_date,escrow_account_no" +
                    " FROM rsk_mst_tcustomer2escrow a" +
                    " INNER JOIN ocs_mst_tcustomer2sanction b on a.customer2sanction_gid = b.customer2sanction_gid" +
                    " WHERE a.customer_gid = '"+ customer_gid  + "' "+
                    "  ORDER BY a.created_date DESC";
            dt_datatable  = objdbconn .GetDataTable(msSQL );
            if (dt_datatable.Rows.Count != 0)
            {
                objResult .escrowSummary  = dt_datatable.AsEnumerable().Select(row => new escrowSummary
                {   escrow_gid=row["customer2escrow_gid"].ToString (),
                    sanction_refno = row["sanction_refno"].ToString(),
                    sanction_date = row["sanction_date"].ToString(),
                    disbursement_date = row["disbursement_date"].ToString(),
                    transactionref_no = row["transactionref_no"].ToString(),
                    transaction_date = row["transaction_date"].ToString(),
                    amount = row["amount"].ToString(),
                    created_date = row["created_date"].ToString(),
                    escrow_account_no = row["escrow_account_no"].ToString(),
                   
                }).ToList();
                objResult.status = true;
                objResult.message = "Success";

            }
            else
            {
                objResult.status  = false;
                objResult .message  = "No Records Found";
            }
            dt_datatable .Dispose();
        }
        // Escrow View
        public void DaGetEscrowView(string escrow_gid,escrow objResult)
        {
            msSQL = " SELECT " +
                " b.sanction_refno,date_format(b.sanction_date,'%d-%m-%Y') as sanction_date ," +
                " c.customername,c.customer_code,c.customer_urn,b.facility_type," +
                " date_format(a.disbursement_date,'%d-%m-%Y') as disbursement_date,date_format(a.transaction_date,'%d-%m-%Y') as transaction_date," +
                " a.transactionref_no, a.escrow_account_no," +
                " a.dealer_name, a.master_account_no, a.amount, a.beneficiary_customer_account_name, a.sender_customer_account_name," +
                " a.sender_customer_account_no," +
                " a.remittance_info," +
                " a.sender_branch_ifsc," +
                " a.reference," +
                " a.credit_time," +
                " a.remarks" +
                " FROM rsk_mst_tcustomer2escrow a" +
                " LEFT JOIN ocs_mst_tcustomer2sanction b ON a.customer2sanction_gid=b.customer2sanction_gid" +
                " LEFT JOIN ocs_mst_tcustomer c on b.customer_gid = c.customer_gid"+
                " WHERE customer2escrow_gid='" + escrow_gid + "'";
            objodbcdatareader = objdbconn.GetDataReader(msSQL);
            if(objodbcdatareader .HasRows ==true)
            {
                objResult.sanction_refno = objodbcdatareader["sanction_refno"].ToString();
                objResult.sanction_date = objodbcdatareader["sanction_date"].ToString();
                objResult.customer_name = objodbcdatareader["customername"].ToString();
                objResult.customer_code = objodbcdatareader["customer_code"].ToString();
                objResult.customer_urn = objodbcdatareader["customer_urn"].ToString();
                objResult.disbursement_date = objodbcdatareader["disbursement_date"].ToString();
                objResult.transaction_date = objodbcdatareader["transaction_date"].ToString();
                objResult.transactionref_no = objodbcdatareader["transactionref_no"].ToString();
                objResult.escrow_account_no = objodbcdatareader["escrow_account_no"].ToString();
                objResult.dealer_name = objodbcdatareader["dealer_name"].ToString();
                objResult.master_account_no = objodbcdatareader["master_account_no"].ToString();
                objResult.amount = objodbcdatareader["amount"].ToString();
                objResult.beneficiary_customer_account_name = objodbcdatareader["beneficiary_customer_account_name"].ToString();
                objResult.sender_customer_account_name = objodbcdatareader["sender_customer_account_name"].ToString();
                objResult.sender_customer_account_no = objodbcdatareader["sender_customer_account_no"].ToString();
                objResult.remittance_info = objodbcdatareader["remittance_info"].ToString();
                objResult.sender_branch_IFSC = objodbcdatareader["sender_branch_ifsc"].ToString();
                objResult.reference = objodbcdatareader["reference"].ToString();
                objResult.credit_time = objodbcdatareader["credit_time"].ToString();
                objResult.remarks = objodbcdatareader["remarks"].ToString();
                objResult.facility_type = objodbcdatareader["facility_type"].ToString();
                objResult.status = true;
                objResult.message = "Success";
            }
            else
            {
                objResult.status = false;
                objResult.message = "Failure";

            }
            objodbcdatareader.Close();
        }
        #endregion

        public void DaGetHistoryEscrowSummary(string allocationdtl_gid, escrowSummaryList objResult)
        {
            msSQL = " SELECT a.trncustomer2escrow_gid, b.sanction_refno,date_format(b.sanction_date,'%d-%m-%Y') as sanction_date," +
                   " date_format(a.disbursement_date,'%d-%m-%Y') as disbursement_date," +
                    " a.transactionref_no,date_format(a.transaction_date, '%d-%m-%Y') as transaction_date," +
                    " a.amount,date_format(a.created_date, '%d-%m-%Y') as created_date,escrow_account_no" +
                    " FROM rsk_trn_tcustomer2escrow a" +
                    " INNER JOIN ocs_mst_tcustomer2sanction b on a.customer2sanction_gid = b.customer2sanction_gid" +
                    " WHERE a.allocationdtl_gid = '" + allocationdtl_gid + "' " +
                    "  ORDER BY a.created_date DESC";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                objResult.escrowSummary = dt_datatable.AsEnumerable().Select(row => new escrowSummary
                {
                    escrow_gid = row["trncustomer2escrow_gid"].ToString(),
                    sanction_refno = row["sanction_refno"].ToString(),
                    sanction_date = row["sanction_date"].ToString(),
                    disbursement_date = row["disbursement_date"].ToString(),
                    transactionref_no = row["transactionref_no"].ToString(),
                    transaction_date = row["transaction_date"].ToString(),
                    amount = row["amount"].ToString(),
                    created_date = row["created_date"].ToString(),
                    escrow_account_no = row["escrow_account_no"].ToString(),

                }).ToList();
                objResult.status = true;
                objResult.message = "Success";

            }
            else
            {
                objResult.status = false;
                objResult.message = "No Records Found";
            }
            dt_datatable.Dispose();
        }
        // Escrow View
        public void DaGetHistoryEscrowView(string escrow_gid, escrow objResult)
        {
            msSQL = " SELECT " +
                " b.sanction_refno,date_format(b.sanction_date,'%d-%m-%Y') as sanction_date ," +
                " c.customername,c.customer_code,c.customer_urn,b.facility_type," +
                " date_format(a.disbursement_date,'%d-%m-%Y') as disbursement_date,date_format(a.transaction_date,'%d-%m-%Y') as transaction_date," +
                " a.transactionref_no, a.escrow_account_no," +
                " a.dealer_name, a.master_account_no, a.amount, a.beneficiary_customer_account_name, a.sender_customer_account_name," +
                " a.sender_customer_account_no," +
                " a.remittance_info," +
                " a.sender_branch_ifsc," +
                " a.reference," +
                " a.credit_time," +
                " a.remarks" +
                " FROM rsk_trn_tcustomer2escrow a" +
                " LEFT JOIN ocs_mst_tcustomer2sanction b ON a.customer2sanction_gid=b.customer2sanction_gid" +
                " LEFT JOIN ocs_mst_tcustomer c on b.customer_gid = c.customer_gid" +
                " WHERE trncustomer2escrow_gid='" + escrow_gid + "'";
            objodbcdatareader = objdbconn.GetDataReader(msSQL);
            if (objodbcdatareader.HasRows == true)
            {
                objResult.sanction_refno = objodbcdatareader["sanction_refno"].ToString();
                objResult.sanction_date = objodbcdatareader["sanction_date"].ToString();
                objResult.customer_name = objodbcdatareader["customername"].ToString();
                objResult.customer_code = objodbcdatareader["customer_code"].ToString();
                objResult.customer_urn = objodbcdatareader["customer_urn"].ToString();
                objResult.disbursement_date = objodbcdatareader["disbursement_date"].ToString();
                objResult.transaction_date = objodbcdatareader["transaction_date"].ToString();
                objResult.transactionref_no = objodbcdatareader["transactionref_no"].ToString();
                objResult.escrow_account_no = objodbcdatareader["escrow_account_no"].ToString();
                objResult.dealer_name = objodbcdatareader["dealer_name"].ToString();
                objResult.master_account_no = objodbcdatareader["master_account_no"].ToString();
                objResult.amount = objodbcdatareader["amount"].ToString();
                objResult.beneficiary_customer_account_name = objodbcdatareader["beneficiary_customer_account_name"].ToString();
                objResult.sender_customer_account_name = objodbcdatareader["sender_customer_account_name"].ToString();
                objResult.sender_customer_account_no = objodbcdatareader["sender_customer_account_no"].ToString();
                objResult.remittance_info = objodbcdatareader["remittance_info"].ToString();
                objResult.sender_branch_IFSC = objodbcdatareader["sender_branch_ifsc"].ToString();
                objResult.reference = objodbcdatareader["reference"].ToString();
                objResult.credit_time = objodbcdatareader["credit_time"].ToString();
                objResult.remarks = objodbcdatareader["remarks"].ToString();
                objResult.facility_type = objodbcdatareader["facility_type"].ToString();
                objResult.status = true;
                objResult.message = "Success";
            }
            else
            {
                objResult.status = false;
                objResult.message = "Failure";

            }
            objodbcdatareader.Close();
        }

        public void DaGetTrnSanctionDocumentUpload(uploaddocument values, string allocationdtl_gid,string customer_gid)
        {
            msSQL = " select rsksanction_documentgid,file_name,file_path,document_name,updated_by,updated_date " +
                    " from rsk_mst_tsanctiondocumentdtl a " +
                    " where customer_gid = '" + customer_gid + "' and file_path<> '' " +
                    " and a.rsksanction_documentgid not in (select rsksanction_documentgid from rsk_trn_tallocationdocument " +
                    " where customer_gid = '" + customer_gid + "' and allocationdtl_gid='" + allocationdtl_gid + "')";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string msGetDocumentGid = objcmnfunctions.GetMasterGID("DOAL");
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
                              "'" + allocationdtl_gid + "'," +
                              "'" + dt["rsksanction_documentgid"].ToString() + "'," +
                               "'" + customer_gid + "'," +
                              "'" + dt["file_name"].ToString() + "'," +
                              "'" + dt["file_path"].ToString() + "'," +
                              "'" + dt["document_name"].ToString().Replace("'", "\\'") + "'," +
                              "'" + dt["updated_by"].ToString() + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            dt_datatable.Dispose();

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
                        allocation_documentGid = dr_datarow["allocation_documentGid"].ToString(),
                        document_name = dr_datarow["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                        document_type = dr_datarow["document_type"].ToString(),
                        created_by = dr_datarow["created_by"].ToString(),
                        created_date = dr_datarow["created_date"].ToString(),
                    });
                }
                values.upload_list = get_fileseekname;
            }
            dt_datatable.Dispose();
        }
    }
}