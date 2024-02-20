using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.ecms.Models;
using ems.utilities.Functions;

namespace ems.ecms.DataAccess
{
    /// <summary>
    /// loan Controller Class containing API methods for accessing the  DataAccess class DaLoan
    ///     Loan  - Create loan, sanction loan, loan details, loan details summary, 
    /// </summary>
    /// <remarks>Written by Sundar Rajan </remarks>
    public class DaLoan
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        
        DataTable dt_datatable;
        string msSQL, msGetGid;
        int mnResult,i;
        string  sanction_refno , sanction_date = string .Empty ;
        // loanCreate
        public void DaPostLoanFacility(createLoan values, string employee_gid)
        {
            

            msGetGid = objcmnfunctions.GetMasterGID("LNMR");
            msSQL = " insert into ocs_mst_tloan(" +
                    " loanmaster_gid," +
                    " loan_title," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.remarks.Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

          
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "success";
            }
            else
            {
                values.status = false;
                values.message = "success";
            }

        }
        // createLoan
       public void DaPostCreateLoan(createLoan values, string employee_gid)
        {
                msSQL = "select loanref_no from ocs_trn_tloan where loanref_no='" + values.loanRefNo.Trim() + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    
                    values.status = false;
                    values.message = "Loan Ref No. Already Exists";
                    return;
                }
                objODBCDatareader.Close();
          

            msGetGid = objcmnfunctions.GetMasterGID("LOAN");
            msSQL = " insert into ocs_trn_tloan(" +
                    " loan_gid," +
                    " sanction_gid," +
                    " loanref_no," +
                     " sanction_refno," +
                     " sanction_date," +
                    " loanmaster_gid," +
                    " loan_title," +
                    " customer_gid," +
                    " zonal_gid," +
                    " businesshead_gid," +
                    " relationshipmgmt_gid," +
                    " cluster_manager_gid," +
                    " creditmanager_gid," +
                    " customer_name," +
                    " zonal_name," +
                    " businesshead_name," +
                    " relationshipmgmt_name," +
                    " cluster_manager_name," +
                     " creditmgmt_name," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.sanctionGid + "'," +
                    "'" + values.loanRefNo.Replace("'", "") + "'," +
                    "'" + values.sanctionRefno + "',";
            if (values.sanctionDate == null)
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.sanctionDate).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + values.loanmaster_gid + "'," +
                    "'" + values.loanTitle.Replace("'", "").Replace("\n", "") + "'," +
                    "'" + values.customerGid + "'," +
                    "'" + values.zonalGid + "'," +
                    "'" + values.businessHeadGid + "'," +
                    "'" + values.relationshipMgmtGid + "'," +
                    "'" + values.clustermanagerGid + "'," +
                    "'" + values.creditmanager_gid + "'," +
                    "'" + values.customer_name.Replace("    ", "").Replace("\n", "") + "'," +
                    "'" + values.zonal_name.Replace("    ", "").Replace("\n", "") + "'," +
                    "'" + values.businesshead_name.Replace("    ", "").Replace("\n", "") + "'," +
                    "'" + values.relationshipmgmt_name.Replace("    ", "").Replace("\n", "") + "'," +
                    "'" + values.cluster_manager_name.Replace("    ", "").Replace("\n", "") + "'," +
                    "'" + values.creditmanager_name.Replace("    ", "").Replace("\n", "") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false ;
                values.message = "Error Occured";
            }

        }
        // Get Loan Facility Type with sanction
        public void DaGetSantionFacilityType(string customer2sanction_gid,loanfaciity_list objResult)
        {
            msSQL = " SELECT loanfacility_gid,loanfacility_type " +
                    " FROM ocs_mst_tsanction2loanfacilitytype" +
                    " WHERE customer2sanction_gid='" + customer2sanction_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                objResult.count_loan = dt_datatable.Rows.Count;
                objResult.loanfacility = dt_datatable.AsEnumerable().Select(row => new loanfacility
                {
                    facility_gid = row["loanfacility_gid"].ToString(),
                    facility_type = row["loanfacility_type"].ToString()

                }).ToList();
                dt_datatable.Dispose();
                objResult .status = true;
                objResult .message = "Succeess";
            }
            else
            {
                dt_datatable.Dispose();
                objResult .status = false;
                objResult .message  = "No Records Found";
            }

        }
        // getLoanmasterSummary

        public void DaGetLoan(loan objloan)
        {
            try
            {
                msSQL = " select loanmaster_gid,loan_title,created_date from ocs_mst_tloan a " +
                    " where 1=1 order by a.loanmaster_gid desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var objgetLoan = new List<LoanDetails>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        objgetLoan.Add(new LoanDetails
                        {
                            loanmaster_gid = (dr_datarow["loanmaster_gid"].ToString()),
                            loanTitle = (dr_datarow["loan_title"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),

                        });
                    }
                    objloan.loanDetails = objgetLoan;
                }
                dt_datatable.Dispose();
                objloan.status = true;
                objloan.message = "Success";
            }
            catch (Exception ex)
            {
                objloan.status = true;
                objloan.message = ex.Message .ToString();
            }
         
           
        }
        // loan_list
        public void DaGetLoanList(loanmaster objLoan)
        {
            try
            {
                msSQL = " SELECT loanmaster_gid,loan_title FROM  ocs_mst_tloan";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getEntity = new List<loanmasterdtls>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getEntity.Add(new loanmasterdtls
                        {
                            loanmaster_gid = (dr_datarow["loanmaster_gid"].ToString()),
                            loanTitle = (dr_datarow["loan_title"].ToString()),

                        });
                    }
                    objLoan.loanmasterdtls = getEntity;
                }
                dt_datatable.Dispose();
                objLoan.status = true;
            }
            catch(Exception ex)
            {
                objLoan.status = false ;
                objLoan.message = ex.Message.ToString();

            }
           
        }
        // editloanmaster
        public void DaGetEditLoan(string loanmaster_gid, loanedit values)
        {
            try
            {
                msSQL = " select loan_title from ocs_mst_tloan where loanmaster_gid='" + loanmaster_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {

                    values.loanTitleedit = objODBCDatareader["loan_title"].ToString();
                    values.loanmaster_gid = loanmaster_gid;
                }
                objODBCDatareader.Close();
                values.status = true;
            }
            catch(Exception ex)
            {
                values.status = false;
                values.message = ex.Message.ToString();
            }
           
          
        }

        // updateloanmaster
        public void DaPostupdateloanFacility(string employee_gid, loanedit values)
        {
            
            msSQL = " update ocs_mst_tloan set " +
                 " loan_title='" + values.loanTitleedit + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where loanmaster_gid='" + values.loanmaster_gid + "' ";
            mnResult = objdbconn .ExecuteNonQuerySQL(msSQL);
         
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "Failure";
            }
        }
        // deleteloanmaster
        public void DaGetDeleteLoan(string loanmaster_gid, loanedit values)
        {
           
            msSQL = " delete from ocs_mst_tloan where loanmaster_gid='" + loanmaster_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
           
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "Failure";
            }
        }

        // getLoanSummary
        public void DaGetLoanSummary(loan objloan)
        {
            try
            {
                msSQL = " select a.loan_gid,d.vertical_code,a.loanref_no, a.loan_title, date_format(a.created_date,'%d-%m-%Y %h:%i:%s %p') as created_date," +
                           " d.customername ,a.sanction_refno,date_format(a.sanction_date,'%d-%m-%Y') as sanction_date," +
                           " concat(f.user_code,'/',f.user_firstname,f.user_lastname) as Created_by " +
                           " from ocs_trn_tloan a" +
                           " left join ocs_mst_tcustomer d on a.customer_gid = d.customer_gid" +
                           " left join hrm_mst_temployee e on a.created_by = e.employee_gid" +
                           " left join adm_mst_tuser f on f.user_gid = e.user_gid" +
                           " where 1=1 order by a.loan_gid desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var objgetLoan = new List<LoanDetails>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        objgetLoan.Add(new LoanDetails
                        {

                            vertical_code = (dr_datarow["vertical_code"].ToString()),
                            loanRefNo = (dr_datarow["loanref_no"].ToString()),
                            loanTitle = (dr_datarow["loan_title"].ToString()),
                            created_by = (dr_datarow["Created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            customerName = (dr_datarow["customername"].ToString()),
                            loan_gid = (dr_datarow["loan_gid"].ToString()),
                            sanctionRefno = (dr_datarow["sanction_refno"].ToString()),
                            sanctionDate = (dr_datarow["sanction_date"].ToString()),
                        });
                    }
                    objloan.loanDetails = objgetLoan;
                }
                dt_datatable.Dispose();
                objloan.status = true;
            }
            catch(Exception ex)
            {
                objloan.status = false;
                objloan.message = ex.Message.ToString();
            }
          
        }
        // getcustomer_getheads

        public void DaGetCustomerHeads(mdlheadsofcustomer values, string customer_gid)
        {
            try
            {
                msSQL = " select a.zonal_head,a.business_head,a.relationship_manager,a.cluster_manager_gid,a.creditmanager_gid,a.vertical_code,a.vertical_gid," +
                   " case when a.zonal_head = '' then 'NA' else a.zonal_name end as zonal_name," +
                   " case when a.business_head = '' then 'NA' else a.businesshead_name end as businesshead_name, " +
                   " case when a.cluster_manager_gid = '' then 'NA' else a.cluster_manager_name end as cluster_manager_name, " +
                   " case when a.relationship_manager = '' then 'NA' else a.relationshipmgmt_name end as relationshipmgmt_name, " +
                    " case when a.creditmanager_gid = '' then 'NA' else a.creditmgmt_name end as creditmgmt_name " +
                   " from ocs_mst_tcustomer a where customer_gid='" + customer_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.zonalGid = objODBCDatareader["zonal_head"].ToString();
                    values.businessHeadGid = objODBCDatareader["business_head"].ToString();
                    values.relationshipMgmtGid = objODBCDatareader["relationship_manager"].ToString();
                    values.clustermanagerGid = objODBCDatareader["cluster_manager_gid"].ToString();
                    values.creditmanager_gid = objODBCDatareader["creditmanager_gid"].ToString();
                    values.zonal_name = objODBCDatareader["zonal_name"].ToString();
                    values.businesshead_name = objODBCDatareader["businesshead_name"].ToString();
                    values.rm_name = objODBCDatareader["relationshipmgmt_name"].ToString();
                    values.cluster_manager_name = objODBCDatareader["cluster_manager_name"].ToString();
                    values.creditmgmt_name = objODBCDatareader["creditmgmt_name"].ToString();
                    values.vertical_code = objODBCDatareader["vertical_code"].ToString();
                    values.vertical_gid = objODBCDatareader["vertical_gid"].ToString();
                }
                objODBCDatareader.Close();
                msSQL = " select sanction_refno,customer2sanction_gid,sanction_date  " +
                      " from ocs_mst_tcustomer2sanction where customer_gid='" + customer_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_sanctiondtl = new List<sanctiondtl>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        get_sanctiondtl.Add(new sanctiondtl
                        {
                            sanctionrefno = dt["sanction_refno"].ToString(),
                            sanction_Gid = dt["customer2sanction_gid"].ToString(),
                        });
                    }
                    values.sanctiondtl = get_sanctiondtl;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.Message.ToString();
            }
        }
        // getLoandetails

        public void DaGetLoanDetails(LoanDetails values,string loan_gid)
        {
            try
            {
                msSQL = " select a.sanction_gid,a.loanmaster_gid,b.vertical_code,b.customer_gid,a.loan_title,concat(b.customer_code,'/',b.customername) as customername,b.vertical_gid," +
                  " a.loanref_no,a.zonal_gid,a.businesshead_gid,a.relationshipmgmt_gid,a.cluster_manager_gid,a.creditmanager_gid,a.sanction_refno, " +
                  " date_format(a.sanction_date,'%d-%m-%Y') as sanction_date,a.sanction_date as sanctionDate," +
                  " case when a.zonal_gid = '' then 'NA' else a.zonal_name end as zonal_name," +
                  " case when a.businesshead_gid = '' then 'NA' else a.businesshead_name end as businesshead_name, " +
                  " case when a.cluster_manager_gid = '' then 'NA' else a.cluster_manager_name end as cluster_manager_name, " +
                  " case when a.relationshipmgmt_gid = '' then 'NA' else a.relationshipmgmt_name end as relationshipmgmt_name, " +
                  " case when a.creditmanager_gid = '' then 'NA' else a.creditmgmt_name end as creditmgmt_name " +
                  " from ocs_trn_tloan a " +
                  " left join ocs_mst_tcustomer b on a.customer_gid=b.customer_gid " +
                  " where a.loan_gid='" + loan_gid + "' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.customerName = objODBCDatareader["customername"].ToString();
                    values.vertical_code = objODBCDatareader["vertical_code"].ToString();
                    values.loanmaster_gid = objODBCDatareader["loanmaster_gid"].ToString();
                    values.loanTitle = objODBCDatareader["loan_title"].ToString();
                    values.customer_gid = objODBCDatareader["customer_gid"].ToString();
                    values.loanRefNo = objODBCDatareader["loanref_no"].ToString();
                    values.sanctionRefno = objODBCDatareader["sanction_refno"].ToString();
                    if(objODBCDatareader["sanctionDate"].ToString()=="")
                    {

                    }
                    else
                    {
                        values.sanctionDate = Convert.ToDateTime(objODBCDatareader["sanctionDate"]).ToString("yyyy-MM-dd");
                    }
                    values.sanction_date = objODBCDatareader["sanction_date"].ToString();
                    values.sanction_Gid = objODBCDatareader["sanction_gid"].ToString();
                     values.vertical_gid = objODBCDatareader["vertical_gid"].ToString();
                    values.zonal_gid = objODBCDatareader["zonal_gid"].ToString();
                    values.businesshead_gid = objODBCDatareader["businesshead_gid"].ToString();
                    values.relationshipmgmt_gid = objODBCDatareader["relationshipmgmt_gid"].ToString();
                    values.clustermanagerGid = objODBCDatareader["cluster_manager_gid"].ToString();
                    values.creditmanager_gid = objODBCDatareader["creditmanager_gid"].ToString();
                    values.zonal_name = objODBCDatareader["zonal_name"].ToString();
                    values.businesshead_name = objODBCDatareader["businesshead_name"].ToString();
                    values.rm_name = objODBCDatareader["relationshipmgmt_name"].ToString();
                    values.cluster_manager_name = objODBCDatareader["cluster_manager_name"].ToString();
                    values.creditmgmt_name = objODBCDatareader["creditmgmt_name"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.Message.ToString();
            }
                  }
        // getLoandeferraldetails
        public void DaGetLoanDeferraldetails(LoanDetails values, string loan_gid)
        {
           
           try
            {
                msSQL = " select a.loanmaster_gid,b.vertical_code,b.customer_gid,a.loan_title,b.customer_code,b.customername,b.vertical_gid,d.entity_name,c.sanction_branch_name,d.entity_gid,c.sanction_branch_gid," +
                                   " a.loanref_no,a.zonal_gid,a.businesshead_gid,a.relationshipmgmt_gid,a.cluster_manager_gid,a.creditmanager_gid,a.sanction_refno,a.sanction_date," +
                                   " case when a.zonal_gid = '' then 'NA' else a.zonal_name end as zonal_name," +
                                   " case when a.businesshead_gid = '' then 'NA' else a.businesshead_name end as businesshead_name, " +
                                   " case when a.cluster_manager_gid = '' then 'NA' else a.cluster_manager_name end as cluster_manager_name, " +
                                   " case when a.relationshipmgmt_gid = '' then 'NA' else a.relationshipmgmt_name end as relationshipmgmt_name ," +
                                     " case when a.creditmanager_gid = '' then 'NA' else a.creditmgmt_name end as creditmgmt_name " +
                                   " from ocs_trn_tloan a " +
                                   " left join ocs_mst_tcustomer b on a.customer_gid=b.customer_gid " +
                                   " left join ocs_mst_tcustomer2sanction c on a.sanction_gid=c.customer2sanction_gid " +
                                   "left join adm_mst_tentity d on c.entity_gid=d.entity_gid " +
                                   " where a.loan_gid='" + loan_gid + "' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.customer_code = objODBCDatareader["customer_code"].ToString();
                    values.customerName = objODBCDatareader["customername"].ToString();
                    values.vertical_code = objODBCDatareader["vertical_code"].ToString();
                    values.loanmaster_gid = objODBCDatareader["loanmaster_gid"].ToString();
                    values.loanTitle = objODBCDatareader["loan_title"].ToString();
                    values.customer_gid = objODBCDatareader["customer_gid"].ToString();
                    values.loanRefNo = objODBCDatareader["loanref_no"].ToString();
                    values.sanctionRefno = objODBCDatareader["sanction_refno"].ToString();
                    values.entityName = objODBCDatareader["entity_name"].ToString();
                    values.entity_gid = objODBCDatareader["entity_gid"].ToString();
                    values.branchName = objODBCDatareader["sanction_branch_name"].ToString();
                    values.branch_gid = objODBCDatareader["sanction_branch_gid"].ToString();
                    if (objODBCDatareader["sanction_date"].ToString() == "")
                    {

                    }
                    else
                    {
                        values.sanctionDate = Convert.ToDateTime(objODBCDatareader["sanction_date"]).ToString("dd-MM-yyyy");

                    }
                    values.vertical_gid = objODBCDatareader["vertical_gid"].ToString();
                    values.zonal_gid = objODBCDatareader["zonal_gid"].ToString();
                    values.businesshead_gid = objODBCDatareader["businesshead_gid"].ToString();
                    values.relationshipmgmt_gid = objODBCDatareader["relationshipmgmt_gid"].ToString();
                    values.clustermanagerGid = objODBCDatareader["cluster_manager_gid"].ToString();
                    values.creditmanager_gid = objODBCDatareader["creditmanager_gid"].ToString();
                    values.zonal_name = objODBCDatareader["zonal_name"].ToString();
                    values.businesshead_name = objODBCDatareader["businesshead_name"].ToString();
                    values.rm_name = objODBCDatareader["relationshipmgmt_name"].ToString();
                    values.cluster_manager_name = objODBCDatareader["cluster_manager_name"].ToString();
                    values.creditmgmt_name = objODBCDatareader["creditmgmt_name"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;
            }
            catch(Exception ex)
            {
                values.status = false;
                values.message = ex.Message.ToString();
            }   
                
                }
        // getcriticallity

        public void DaGetCriticallity(MDLcriticallity values, string deferral)
        {

            try
            {
                msSQL = " select criticallity,comments from ocs_mst_tdeferral where deferraltype_gid='" + deferral + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.criticallity = objODBCDatareader["criticallity"].ToString();
                    values.comments = objODBCDatareader["comments"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
          
           
        }
        // getcriticallitycov
        public void DaGetCriticallityCov(MDLcriticallity values, string covenanttype)
        {

            try
            {
                msSQL = " select criticallity,comments from ocs_mst_tcovenanttype where covenanttype_gid='" + covenanttype + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.criticallity = objODBCDatareader["criticallity"].ToString();
                    values.comments = objODBCDatareader["comments"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;
            }
            catch(Exception ex)
            {
                values.status = false;
                values.message = ex.Message.ToString();
            }
        }
        //loan
        public void DaGetLoan(loandetails values)
        {
            
            msSQL = " select loan_gid,concat(loanref_no,'/',loan_title) as loan_title from ocs_trn_tloan ";
            dt_datatable = objdbconn .GetDataTable (msSQL);
            var get_filename = new List<loanlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new loanlist
                    {
                        loanTitle = (dr_datarow["loan_title"].ToString()),
                        loan_gid = (dr_datarow["loan_gid"].ToString()),
                    });
                }
                values.loan_details = get_filename;
                dt_datatable.Dispose();
                values.status = true;
               
            }

            else
            {
                dt_datatable.Dispose();
                values.status = false;
            }
        }
        // updateloan
        public void DaPostUpdateLoan(string employee_gid, loanedit values)
        {
            if (values.loanRefNoedit != null && values.loanRefNoedit != "")
            {
                msSQL = " select loan_gid from ocs_trn_tloan" +
                       " where loanref_no='" + values.loanRefNoedit + "'";
                var loan_gid = objdbconn.GetExecuteScalar(msSQL);
                if (loan_gid != "")
                {
                    if (loan_gid != values.loan_gid)
                    {
                        values.message = "Loan Ref No. Already Exists";
                        values.status = false;
                        return;
                    }

                }
            }


            if (Convert.ToDateTime(values.sanctionDateedit).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {
              

                msSQL = " update ocs_trn_tloan set " +
                     " customer_gid='" + values.customer_gid + "'," +
                     " customer_name='" + values.customer_name.Replace("    ", "").Replace("\n", "") + "'," +
                     " loanref_no='" + values.loanRefNoedit + "'," +
                     " sanction_refno='" + values.sanctionrefnoedit + "'," +
                     " loanmaster_gid='" + values.loanmaster_gid + "'," +
                     " loan_title='" + values.loanTitleedit.Replace("\n", "") + "'," +
                     " zonal_gid='" + values.zonalGid + "'," +
                     " businesshead_gid='" + values.businessHeadGid + "'," +
                     " relationshipmgmt_gid='" + values.relationshipMgmtGid + "'," +
                     " cluster_manager_gid='" + values.clustermanagerGid + "'," +
                       " creditmanager_gid='" + values.creditmanager_gid + "'," +
                     " zonal_name='" + values.zonal_name.Replace("    ", "").Replace("\n", "") + "'," +
                     " businesshead_name='" + values.businesshead_name.Replace("    ", "").Replace("\n", "") + "'," +
                     " cluster_manager_name='" + values.cluster_manager_name.Replace("    ", "").Replace("\n", "") + "'," +
                     " relationshipmgmt_name='" + values.relationshipmgmt_name.Replace("    ", "").Replace("\n", "") + "'," +
                      " creditmgmt_name='" + values.creditmanager_name.Replace("    ", "").Replace("\n", "") + "'," +
                       " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where loan_gid='" + values.loan_gid + "' and customer_gid='" + values.customer_gid + "' ";
                    mnResult = objdbconn .ExecuteNonQuerySQL(msSQL);

                
                  msSQL = " update ocs_trn_tdeferral2loan set " +
                        " sanction_refno='" + values.sanctionrefnoedit + "'" +
                        " where loan_gid='" + values.loan_gid + "' and customer_gid='" + values.customer_gid + "'";
                mnResult = objdbconn .ExecuteNonQuerySQL(msSQL);



                msSQL = " select deferral_gid from ocs_trn_tdeferral2loan where loan_gid='" + values.loan_gid + "'" +
                 " and customer_gid='" + values.customer_gid + "'";
                dt_datatable = objdbconn .GetDataTable (msSQL);
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    msSQL = " select deferral_gid from ocs_trn_tdeferral2loan where deferral_gid='" + dr_datarow["deferral_gid"].ToString() + "' ";
                    objODBCDatareader = objdbconn .GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows)
                    {

                        i = objODBCDatareader.RecordsAffected;
                    }
                    objODBCDatareader.Close();


                    if (i != 1 & i != 0)
                    {
                        msSQL = " select sanction_refno from ocs_trn_tdeferral2loan where deferral_gid = '" + dr_datarow["deferral_gid"].ToString() + "'";
                        dt_datatable = objdbconn .GetDataTable (msSQL);
                        foreach (DataRow dr in dt_datatable.Rows)
                        {


                            sanction_refno += "'" + dr["sanction_refno"].ToString() + "',";
                        }
                        dt_datatable.Dispose();

                        msSQL = " update ocs_trn_tdeferral set " +
                                " sanction_refno='" + sanction_refno.Replace("'", "").TrimEnd(',').TrimStart(',') + "'" +
                                " where deferral_gid='" + dr_datarow["deferral_gid"].ToString() + "' and customer_gid='" + values.customer_gid + "'";
                        mnResult = objdbconn .ExecuteNonQuerySQL(msSQL);

                    }
                    else if (i == 1)
                    {
                        msSQL = " update ocs_trn_tdeferral set " +
                               " sanction_refno='" + values.sanctionrefnoedit + "'" +
                               " where deferral_gid='" + dr_datarow["deferral_gid"].ToString() + "' and customer_gid='" + values.customer_gid + "'";
                        mnResult = objdbconn .ExecuteNonQuerySQL(msSQL);
                    }


                }

                dt_datatable.Dispose();  
            }           
            else
            {
              

                msSQL = " update ocs_trn_tloan set " +
                    " customer_gid='" + values.customer_gid + "'," +
                    " customer_name='" + values.customer_name.Replace("    ", "").Replace("\n", "") + "'," +
                    " loanref_no='" + values.loanRefNoedit + "'," +
                    " sanction_refno='" + values.sanctionrefnoedit + "'," +
                    " sanction_date='" + Convert.ToDateTime(values.sanctionDateedit).ToString("yyyy-MM-dd") + "'," +
                    " loanmaster_gid='" + values.loanmaster_gid + "'," +
                         " loan_title='" + values.loanTitleedit.Replace("\n", "") + "'," +
                         " zonal_gid='" + values.zonalGid + "'," +
                         " businesshead_gid='" + values.businessHeadGid + "'," +
                         " relationshipmgmt_gid='" + values.relationshipMgmtGid + "'," +
                         " cluster_manager_gid='" + values.clustermanagerGid + "'," +
                           " creditmanager_gid='" + values.creditmanager_gid + "'," +
                         " zonal_name='" + values.zonal_name.Replace("    ", "").Replace("\n", "") + "'," +
                         " businesshead_name='" + values.businesshead_name.Replace("    ", "").Replace("\n", "") + "'," +
                         " cluster_manager_name='" + values.cluster_manager_name.Replace("    ", "").Replace("\n", "") + "'," +
                         " relationshipmgmt_name='" + values.relationshipmgmt_name.Replace("    ", "").Replace("\n", "") + "'," +
                            " creditmgmt_name='" + values.creditmanager_name.Replace("    ", "").Replace("\n", "") + "'," +
                           " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where loan_gid='" + values.loan_gid + "' and customer_gid='" + values.customer_gid + "'  ";
                    mnResult = objdbconn .ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_trn_tdeferral2loan set " +
                        " sanction_refno='" + values.sanctionrefnoedit + "'," +
                        " sanction_date='" + Convert.ToDateTime(values.sanctionDateedit).ToString("yyyy-MM-dd 00:00:00") + "'" +
                        " where loan_gid='" + values.loan_gid + "' and customer_gid='" + values.customer_gid + "' ";
                mnResult = objdbconn .ExecuteNonQuerySQL(msSQL);



                msSQL = " select deferral_gid from ocs_trn_tdeferral2loan where loan_gid='" + values.loan_gid + "'" +
                       " and customer_gid='" + values.customer_gid + "'";
                dt_datatable = objdbconn .GetDataTable (msSQL);
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    msSQL = " select deferral_gid from ocs_trn_tdeferral2loan where deferral_gid='" + dr_datarow["deferral_gid"].ToString() + "' ";
                    objODBCDatareader = objdbconn .GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows)
                    {

                        i = objODBCDatareader.RecordsAffected;
                    }
                    objODBCDatareader.Close();


                    if (i != 1 & i != 0)
                    {
                        msSQL = " select date_format(sanction_date, '%d-%m-%Y') as sanction_date,sanction_refno from ocs_trn_tdeferral2loan where deferral_gid = '" + dr_datarow["deferral_gid"].ToString() + "'";
                        dt_datatable = objdbconn .GetDataTable (msSQL);
                        foreach (DataRow dr in dt_datatable.Rows)
                        {
                            sanction_date += "'" + dr["sanction_date"].ToString() + "',";
                            sanction_refno += "'" + dr["sanction_refno"].ToString() + "',";
                        }
                        dt_datatable.Dispose();

                        msSQL = " update ocs_trn_tdeferral set " +
                                " sanction_refno='" + sanction_refno.Replace("'", "").TrimEnd(',').TrimStart(',') + "'," +
                                " sanction_date='" + sanction_date.Replace("'", "").TrimEnd(',').TrimStart(',') + "'" +
                                " where deferral_gid='" + dr_datarow["deferral_gid"].ToString() + "' and customer_gid='" + values.customer_gid + "'";
                        mnResult = objdbconn .ExecuteNonQuerySQL(msSQL);

                    }
                    else if (i == 1)
                    {
                        msSQL = " update ocs_trn_tdeferral set " +
                               " sanction_refno='" + values.sanctionrefnoedit + "'," +
                               " sanction_date='" + Convert.ToDateTime(values.sanctionDateedit).ToString("dd-MM-yyyy") + "'" +
                               " where deferral_gid='" + dr_datarow["deferral_gid"].ToString() + "' and customer_gid='" + values.customer_gid + "'";
                        mnResult = objdbconn .ExecuteNonQuerySQL(msSQL);
                    }


                }

                dt_datatable.Dispose();
            }
            values.status = true;
            
        }

        public void DaGetSanctionDate(sanctiondtl values, string sanction_gid)
        {
            msSQL = " select date_format(sanction_date,'%d-%m-%Y') as sanctiondate,sanction_date, " +
                   " facilitytype_gid,facility_type from ocs_mst_tcustomer2sanction " +
                    " where customer2sanction_gid='" + sanction_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.sanctiondate = objODBCDatareader["sanctiondate"].ToString();
                if (objODBCDatareader["sanctionDate"].ToString() != "")
                {
                    values.Sanction_Date = Convert.ToDateTime(objODBCDatareader["sanction_date"]).ToString("yyyy-MM-dd");
                }
                values.facility_type = objODBCDatareader["facility_type"].ToString();
                values.facilitytype_gid = objODBCDatareader["facilitytype_gid"].ToString();
            }
            objODBCDatareader.Close();
        }

        // update loan ref no
        public void DaPostNewLoanRef(NewloanRef  objResult,string employee_gid)
        {
            if (objResult.newloanref_no != null && objResult.newloanref_no != "")
            {
                msSQL = " select loan_gid from ocs_trn_tloan" +
                       " where loanref_no='" + objResult.newloanref_no  + "'";
                var loan_gid = objdbconn.GetExecuteScalar(msSQL);
                if (loan_gid != "")
                {
                    if (loan_gid != objResult .loan_gid)
                    {
                        objResult .message = "Loan Ref No. Already Exists";
                        objResult .status = false;
                        return;
                    }

                }
            }
            else
            {
                objResult.message = "Loan Ref No. is mandatory";
                objResult.status = false;
                return;
            }
            msSQL = " INSERT INTO ocs_trn_thistoryloanref_no(" +
                    " loan_gid," +
                    " oldloanref_no," +
                    " newloanref_no," +
                    " created_by," +
                    " created_date)" +
                    " VALUES(" +
                    "'" + objResult.loan_gid + "'," +
                    "'" + objResult.oldloanref_no + "'," +
                    "'" + objResult.newloanref_no + "'," +
                    "'" + employee_gid + "'," +
                    "current_timestamp)";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if(mnResult ==1)
            {
                msSQL = " UPDATE ocs_trn_tloan SET" +
                        " loanref_no='" + objResult.newloanref_no + "'," +
                        " updated_by='" + employee_gid + "'," +
                        " updated_date=current_timestamp" +
                        " WHERE loan_gid='" + objResult.loan_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " UPDATE ocs_trn_tdeferral SET "+
                        " loanref_no='"+ objResult.newloanref_no.Replace("'","") +"'"+
                        " WHERE loan_gid='" + objResult.loan_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " select a.allocate_loangid,b.tier3_status from rsk_trn_tallocateloan a " +
                       " left join rsk_trn_tallocationdtl b on a.allocationdtl_gid = b.allocationdtl_gid " +
                       " WHERE a.loan_gid='" + objResult.loan_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                foreach (DataRow dr in dt_datatable.Rows)
                {
                    if (dr["tier3_status"].ToString() == "Pending")
                    {
                        msSQL = " UPDATE rsk_trn_tallocateloan SET " +
                               " loanref_no='" + objResult.newloanref_no.Replace("'", "") + "'" +
                               " WHERE allocate_loangid='" + dr["allocate_loangid"].ToString() + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();

            }

            if (mnResult ==1)
            {
                objResult.status = true;
                objResult.message = "Loan Ref No. Updated Successfully";
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured";
            }
              
        }
        
    }

}