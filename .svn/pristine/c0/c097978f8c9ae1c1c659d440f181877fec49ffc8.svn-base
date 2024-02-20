using ems.masterng.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Web;

namespace ems.masterng.DataAccess
{
    public class DaMstNgProductChargesAddEdit
    {
        int mnResult;
        OdbcDataReader objODBCDatareader, objODBCDatareader1, objODBCDataReader;
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL, msGetGid, msGetGid1, msGetGID, msGetDocumentGid, lsapp_refno, msGetGidContactno;
        public void DaSubmitOverallLimit(string employee_gid, MdlProductCharges values)
        {

            msSQL = " update ocs_mst_tapplication set " +
                    " overalllimit_amount='" + values.overalllimit_amount + "'," +
                    " validityoveralllimit_year='" + values.validityoveralllimit_year + "'," +
                    " validityoveralllimit_month='" + values.validityoveralllimit_month + "'," +
                    " validityoveralllimit_days='" + values.validityoveralllimit_days + "'," +
                    " calculationoveralllimit_validity='" + values.calculationoveralllimit_validity + "'," +
                    " productcharge_flag='Y'," +
                    " productcharges_status='Incomplete'," +
                    " csa_applicability='" + values.csa_applicability + "'," +
                    " csaactivity_gid='" + values.csaactivity_gid + "'," +
                    " csaactivity_name='" + values.csaactivity_name + "'," +
                    " percentageoftotal_limit='" + values.percentageoftotal_limit + "'," +
                    " updated_by='" + employee_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                   " where application_gid='" + values.application_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Overall Limit Submitted Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }

        }

        public void DaGetProductChargesEdit(string application_gid, MdlProductCharges values)
        {
            try
            {
                msSQL = " select application_gid, overalllimit_amount, validityoveralllimit_year, validityoveralllimit_month, validityoveralllimit_days, calculationoveralllimit_validity," +
                        " enduse_purpose, processing_fee, processing_collectiontype, doc_charges, doccharge_collectiontype, fieldvisit_charge, fieldvisit_collectiontype, " +
                        " adhoc_fee, adhoc_collectiontype, life_insurance, lifeinsurance_collectiontype, acct_insurance, total_collect, total_deduct, productcharges_status " +                      
                        " from ocs_mst_tapplication where application_gid='" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.overalllimit_amount = objODBCDatareader["overalllimit_amount"].ToString();
                    values.validityoveralllimit_year = objODBCDatareader["validityoveralllimit_year"].ToString();
                    values.validityoveralllimit_month = objODBCDatareader["validityoveralllimit_month"].ToString();
                    values.validityoveralllimit_days = objODBCDatareader["validityoveralllimit_days"].ToString();
                    values.calculationoveralllimit_validity = objODBCDatareader["calculationoveralllimit_validity"].ToString();
                    values.enduse_purpose = objODBCDatareader["enduse_purpose"].ToString();
                    values.processing_fee = objODBCDatareader["processing_fee"].ToString();
                    values.processing_collectiontype = objODBCDatareader["processing_collectiontype"].ToString();
                    values.doc_charges = objODBCDatareader["doc_charges"].ToString();
                    values.doccharge_collectiontype = objODBCDatareader["doccharge_collectiontype"].ToString();
                    values.fieldvisit_charge = objODBCDatareader["fieldvisit_charge"].ToString();
                    values.fieldvisit_collectiontype = objODBCDatareader["fieldvisit_collectiontype"].ToString();
                    values.adhoc_fee = objODBCDatareader["adhoc_fee"].ToString();
                    values.adhoc_collectiontype = objODBCDatareader["adhoc_collectiontype"].ToString();
                    values.life_insurance = objODBCDatareader["life_insurance"].ToString();
                    values.lifeinsurance_collectiontype = objODBCDatareader["lifeinsurance_collectiontype"].ToString();
                    values.acct_insurance = objODBCDatareader["acct_insurance"].ToString();
                    values.total_collect = objODBCDatareader["total_collect"].ToString();
                    values.total_deduct = objODBCDatareader["total_deduct"].ToString();
                    values.productcharges_status = objODBCDatareader["productcharges_status"].ToString();
                   
                }
                if ( values.validityoveralllimit_year != "0")
                {
                    values.lsyearmonthday = "year";
                }
                if (values.validityoveralllimit_month != "0" )
                {
                    values.lsyearmonthday = "month";
                }
                if (values.validityoveralllimit_days != "0")
                {
                    values.lsyearmonthday = "day";
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
        public void DaGetEditLimit(string application_gid, MdlMstApplicationAdd values, string employee_gid)
        {

            msSQL = "select  format(overalllimit_amount,2,'en_IN') from ocs_mst_tapplication where application_gid='" + application_gid + "'";
            values.overalllimit_amount = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select sum(loanfacility_amount) from ocs_mst_tapplication2loan where application_gid='" + employee_gid + "'" +
                " or application_gid='" + application_gid + "'";
            values.loanfacility_amount = objdbconn.GetExecuteScalar(msSQL);

        }
        public void DaPostLoanDtl(string employee_gid, MdlMstLoanDtl values)
        {
            //string fsprimaryvaluechain_gid = string.Empty;
            //string fsprimaryvaluechain_name = string.Empty;
            //if (values.primaryvaluechain_list != null)
            //{
            //    for (var i = 0; i < values.primaryvaluechain_list.Count; i++)
            //    {
            //        fsprimaryvaluechain_gid += values.primaryvaluechain_list[i].valuechain_gid + ",";
            //        fsprimaryvaluechain_name += values.primaryvaluechain_list[i].valuechain_name + ",";

            //    }
            //    fsprimaryvaluechain_gid = fsprimaryvaluechain_gid.TrimEnd(',');
            //    fsprimaryvaluechain_name = fsprimaryvaluechain_name.TrimEnd(',');
            //}

            //string fssecondaryvaluechain_gid = string.Empty;
            //string fssecondaryvaluechain_name = string.Empty;
            //if (values.secondaryvaluechain_list != null)
            //{
            //    for (var i = 0; i < values.secondaryvaluechain_list.Count; i++)
            //    {
            //        fssecondaryvaluechain_gid += values.secondaryvaluechain_list[i].valuechain_gid + ",";
            //        fssecondaryvaluechain_name += values.secondaryvaluechain_list[i].valuechain_name + ",";

            //    }
            //    fssecondaryvaluechain_gid = fssecondaryvaluechain_gid.TrimEnd(',');
            //    fssecondaryvaluechain_name = fssecondaryvaluechain_name.TrimEnd(',');
            //}

            //msSQL = "select application2loan_gid from ocs_mst_tapplication2loan where producttype_gid='" + values.producttype_gid + "' and " +
            //    " productsubtype_gid='" + values.productsubtype_gid + "' and application_gid='" + values.application_gid + "'";
            //objODBCDatareader = objdbconn.GetDataReader(msSQL);
            //if (objODBCDatareader.HasRows == false)
            //{
            //    objODBCDatareader.Close();
            if (values.product_type == "Agri Receivable Finance (ARF)")
            {
                msSQL = "select application2buyer_gid from ocs_mst_tapplication2buyer  where application2loan_gid='" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    objODBCDatareader.Close();
                    values.message = "Kindly add atleast one Buyer";
                    values.status = false;
                    return;
                }
                else
                {
                    objODBCDatareader.Close();
                    msGetGid = objcmnfunctions.GetMasterGID("AP2L");

                    msSQL = " insert into ocs_mst_tapplication2loan(" +
                            " application2loan_gid ," +
                            " application_gid," +
                            " facilityrequested_date," +
                            " product_type," +
                            " producttype_gid," +
                            " productsub_type," +
                            " productsubtype_gid," +
                            " loantype_gid," +
                            " loan_type ," +
                            " loanfacility_amount," +
                            " rate_interest," +
                            " margin," +
                            " penal_interest," +
                            " facilityvalidity_year," +
                            " facilityvalidity_month," +
                            " facilityvalidity_days," +
                            " facilityoverall_limit ," +
                            " tenureproduct_year," +
                            " tenureproduct_month," +
                            " tenureproduct_days," +
                            " tenureoverall_limit ," +
                            " facility_type," +
                            " facility_mode," +
                            " principalfrequency_name," +
                            " principalfrequency_gid," +
                            " interestfrequency_name," +
                            " interestfrequency_gid," +
                            " program_gid," +
                            " program," +
                            //" primaryvaluechain_gid," +
                            //" primaryvaluechain_name," +
                            //" secondaryvaluechain_gid," +
                            //" secondaryvaluechain_name," +
                            " interest_status," +
                            " moratorium_status," +
                            " moratorium_type," +
                            " moratorium_startdate," +
                            " moratorium_enddate," +
                            " source_type," +
                            " guideline_value," +
                            " guideline_date," +
                            " marketvalue_date ," +
                            " market_value," +
                            " forcedsource_value," +
                            " collateralSSV_value," +
                            " forcedvalueassessed_on," +
                            " collateralobservation_summary," +
                            " enduse_purpose," +
                            " product_gid," +
                            " product_name," +
                            " variety_gid," +
                            " variety_name," +
                            " sector_name," +
                            " category_name," +
                            " botanical_name," +
                            " alternative_name," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + values.application_gid + "'," +
                            "null," +
                            "'" + values.product_type + "'," +
                            "'" + values.producttype_gid + "'," +
                            "'" + values.productsub_type + "'," +
                            "'" + values.productsubtype_gid + "'," +
                            "'" + values.loantype_gid + "'," +
                            "'" + values.loan_type + "'," +
                            "'" + values.facilityloan_amount + "'," +
                            "'" + values.rate_interest + "'," +
                            "'" + values.margin + "'," +
                            "'" + values.penal_interest + "'," +
                            "'" + values.facilityvalidity_year + "'," +
                            "'" + values.facilityvalidity_month + "'," +
                            "'" + values.facilityvalidity_days + "'," +
                            "'" + values.facilityoverall_limit + "'," +
                            "'" + values.tenureproduct_year + "'," +
                            "'" + values.tenureproduct_month + "'," +
                            "'" + values.tenureproduct_days + "'," +
                            "'" + values.tenureoverall_limit + "'," +
                            "'" + values.facility_type + "'," +
                            "'" + values.facility_mode + "'," +
                             "'" + values.principalfrequency_name + "'," +
                            "'" + values.principalfrequency_gid + "'," +
                            "'" + values.interestfrequency_name + "'," +
                            "'" + values.interestfrequency_gid + "'," +
                            "'" + values.program_gid + "'," +
                            "'" + values.program + "'," +
                            //"'" + fsprimaryvaluechain_gid + "'," +
                            //"'" + fsprimaryvaluechain_name + "'," +
                            //"'" + fssecondaryvaluechain_gid + "'," +
                            //"'" + fssecondaryvaluechain_name + "'," +
                            "'" + values.interest_status + "'," +
                            "'" + values.moratorium_status + "'," +
                            "'" + values.moratorium_type + "',";
                    if (values.moratorium_startdate == null || values.moratorium_startdate == "")
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += "'" + Convert.ToDateTime(values.moratorium_startdate).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    }
                    if (values.moratorium_enddate == null || values.moratorium_enddate == "")
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += "'" + Convert.ToDateTime(values.moratorium_enddate).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    }
                    msSQL += "'" + values.source_type + "',";
                    if (values.guideline_value == null || values.guideline_value == "")
                    {
                        msSQL += "'0.00',";
                    }
                    else
                    {
                        msSQL += "'" + values.guideline_value.Replace(",", "") + "',";
                    }
                    if (values.guideline_date == null || values.guideline_date == "")
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += "'" + Convert.ToDateTime(values.guideline_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    }
                    if (values.marketvalue_date == null || values.marketvalue_date == "")
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += "'" + Convert.ToDateTime(values.marketvalue_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    }
                    if (values.market_value == null || values.market_value == "")
                    {
                        msSQL += "'0.00',";
                    }
                    else
                    {
                        msSQL += "'" + values.market_value.Replace(",", "") + "',";
                    }
                    if (values.forcedsource_value == null || values.forcedsource_value == "")
                    {
                        msSQL += "'0.00',";
                    }
                    else
                    {
                        msSQL += "'" + values.forcedsource_value.Replace(",", "") + "',";
                    }
                    if (values.collateralSSV_value == null || values.collateralSSV_value == "")
                    {
                        msSQL += "'0.00',";
                    }
                    else
                    {
                        msSQL += "'" + values.collateralSSV_value.Replace(",", "") + "',";
                    }
                    if (values.forcedvalueassessed_on == null || values.forcedvalueassessed_on == "")
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += "'" + Convert.ToDateTime(values.forcedvalueassessed_on).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    }
                    if (values.collateralobservation_summary == null || values.collateralobservation_summary == "")
                    {
                        msSQL += "'',";
                    }
                    else
                    {
                        msSQL += "'" + values.collateralobservation_summary.Replace("'", "") + "',";
                    }
                    if (values.enduse_purpose == null || values.enduse_purpose == "")
                    {
                        msSQL += "'',";
                    }
                    else
                    {
                        msSQL += "'" + values.enduse_purpose.Replace("'", "") + "',";
                    }
                    msSQL += "'" + values.product_gid + "'," +
                             "'" + values.product_name + "'," +
                             "'" + values.variety_gid + "'," +
                             "'" + values.variety_name + "'," +
                             "'" + values.sector_name + "'," +
                             "'" + values.category_name + "'," +
                             "'" + values.botanical_name + "'," +
                             "'" + values.alternative_name + "'," +
                             "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        msSQL = "update ocs_mst_tapplication2buyer set application2loan_gid='" + msGetGid + "' where application2loan_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update ocs_mst_tapplication2product set application2loan_gid='" + msGetGid + "' where application2loan_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update ocs_mst_tapplication set productcharges_status='Completed',productcharge_flag='Y' where application_gid = '" + values.application_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        values.status = true;

                        values.message = "Loan details Added successfully";
                        values.application2loan_gid = msGetGid;


                        msSQL = "select application2loan_gid from ocs_mst_tapplication2loan where product_type='Agri Receivable Finance (ARF)' and application_gid='" + employee_gid + "' ";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            values.buyer_status = "Y";
                        }
                        objODBCDatareader.Close();

                        msSQL = "select application2loan_gid from ocs_mst_tapplication2loan where loan_type='Secured' and application_gid='" + employee_gid + "' ";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            values.collateral_status = "Y";
                        }
                        objODBCDatareader.Close();

                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error Occured while Adding Loan";
                    }
                }
            }
            else
            {

                msGetGid = objcmnfunctions.GetMasterGID("AP2L");
                msSQL = " insert into ocs_mst_tapplication2loan(" +
                       " application2loan_gid ," +
                       " application_gid," +
                       " facilityrequested_date," +
                       " product_type," +
                       " producttype_gid," +
                       " productsub_type," +
                       " productsubtype_gid," +
                       " loantype_gid," +
                       " loan_type ," +
                       " loanfacility_amount," +
                       " rate_interest," +
                        " margin," +
                       " penal_interest," +
                       " facilityvalidity_year," +
                       " facilityvalidity_month," +
                       " facilityvalidity_days," +
                       " facilityoverall_limit ," +
                       " tenureproduct_year," +
                       " tenureproduct_month," +
                       " tenureproduct_days," +
                       " tenureoverall_limit ," +
                       " facility_type," +
                       " facility_mode," +
                       " principalfrequency_name," +
                       " principalfrequency_gid," +
                       " interestfrequency_name," +
                       " interestfrequency_gid," +
                       " program_gid," +
                       " program," +                      
                       " interest_status," +
                       " moratorium_status," +
                       " moratorium_type," +
                       " moratorium_startdate," +
                       " moratorium_enddate," +
                       " source_type," +
                       " guideline_value," +
                       " guideline_date," +
                       " marketvalue_date ," +
                       " market_value," +
                       " forcedsource_value," +
                       " collateralSSV_value," +
                       " forcedvalueassessed_on," +
                       " collateralobservation_summary," +
                       " enduse_purpose," +
                       " product_gid," +
                       " product_name," +
                       " variety_gid," +
                       " variety_name," +
                       " sector_name," +
                       " category_name," +
                       " botanical_name," +
                       " alternative_name," +
                       " created_by," +
                       " created_date)" +
                           " values(" +
                           "'" + msGetGid + "'," +
                           "'" + values.application_gid + "'," +
                          "null," +
                           "'" + values.product_type + "'," +
                           "'" + values.producttype_gid + "'," +
                           "'" + values.productsub_type + "'," +
                           "'" + values.productsubtype_gid + "'," +
                           "'" + values.loantype_gid + "'," +
                           "'" + values.loan_type + "'," +
                           "'" + values.facilityloan_amount + "'," +
                           "'" + values.rate_interest + "'," +
                           "'" + values.margin + "'," +
                           "'" + values.penal_interest + "'," +
                           "'" + values.facilityvalidity_year + "'," +
                           "'" + values.facilityvalidity_month + "'," +
                           "'" + values.facilityvalidity_days + "'," +
                           "'" + values.facilityoverall_limit + "'," +
                           "'" + values.tenureproduct_year + "'," +
                           "'" + values.tenureproduct_month + "'," +
                           "'" + values.tenureproduct_days + "'," +
                           "'" + values.tenureoverall_limit + "'," +
                           "'" + values.facility_type + "'," +
                           "'" + values.facility_mode + "'," +
                            "'" + values.principalfrequency_name + "'," +
                           "'" + values.principalfrequency_gid + "'," +
                           "'" + values.interestfrequency_name + "'," +
                           "'" + values.interestfrequency_gid + "'," +
                           "'" + values.program_gid + "'," +
                           "'" + values.program + "'," +                          
                           "'" + values.interest_status + "'," +
                           "'" + values.moratorium_status + "'," +
                           "'" + values.moratorium_type + "',";
                if (values.moratorium_startdate == null || values.moratorium_startdate == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.moratorium_startdate).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }
                if (values.moratorium_enddate == null || values.moratorium_enddate == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.moratorium_enddate).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }
                msSQL += "'" + values.source_type + "',";
                if (values.guideline_value == null || values.guideline_value == "")
                {
                    msSQL += "'0.00',";
                }
                else
                {
                    msSQL += "'" + values.guideline_value.Replace(",", "") + "',";
                }
                if (values.guideline_date == null || values.guideline_date == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.guideline_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }
                if (values.marketvalue_date == null || values.marketvalue_date == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.marketvalue_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }
                if (values.market_value == null || values.market_value == "")
                {
                    msSQL += "'0.00',";
                }
                else
                {
                    msSQL += "'" + values.market_value.Replace(",", "") + "',";
                }
                if (values.forcedsource_value == null || values.forcedsource_value == "")
                {
                    msSQL += "'0.00',";
                }
                else
                {
                    msSQL += "'" + values.forcedsource_value.Replace(",", "") + "',";
                }
                if (values.collateralSSV_value == null || values.collateralSSV_value == "")
                {
                    msSQL += "'0.00',";
                }
                else
                {
                    msSQL += "'" + values.collateralSSV_value.Replace(",", "") + "',";
                }
                if (values.forcedvalueassessed_on == null || values.forcedvalueassessed_on == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.forcedvalueassessed_on).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }
                if (values.collateralobservation_summary == null || values.collateralobservation_summary == "")
                {
                    msSQL += "'',";
                }
                else
                {
                    msSQL += "'" + values.collateralobservation_summary.Replace("'", "") + "',";
                }
                if (values.enduse_purpose == null || values.enduse_purpose == "")
                {
                    msSQL += "'',";
                }
                else
                {
                    msSQL += "'" + values.enduse_purpose.Replace("'", "") + "',";
                }
                msSQL += "'" + values.product_gid + "'," +
                         "'" + values.product_name + "'," +
                         "'" + values.variety_gid + "'," +
                         "'" + values.variety_name + "'," +
                         "'" + values.sector_name + "'," +
                         "'" + values.category_name + "'," +
                         "'" + values.botanical_name + "'," +
                         "'" + values.alternative_name + "'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msSQL = "update ocs_mst_tapplication2buyer set application2loan_gid='" + msGetGid + "' where application2loan_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update ocs_mst_tapplication2product set application2loan_gid='" + msGetGid + "' where application2loan_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update ocs_mst_tapplication set productcharges_status='Completed',productcharge_flag='Y' where application_gid = '" + values.application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Loan details Added successfully";
                    values.application2loan_gid = msGetGid;




                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured while Adding Loan";
                }
            }
            //}
            //else
            //{
            //    objODBCDatareader.Close();
            //    values.status = false;
            //    values.message = "Already this Program Name added.";
            //}
        }
        public void DaPostLoanDtlCollateral(string employee_gid, MdlMstLoanDtl values)
        {
            if (values.guideline_value == null || values.guideline_value == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.guideline_value.Replace(",", "") + "',";
            }
            if (values.guideline_date == null || values.guideline_date == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.guideline_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if (values.marketvalue_date == null || values.marketvalue_date == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.marketvalue_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if (values.market_value == null || values.market_value == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.market_value.Replace(",", "") + "',";
            }
            if (values.forcedsource_value == null || values.forcedsource_value == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.forcedsource_value.Replace(",", "") + "',";
            }
            if (values.collateralSSV_value == null || values.collateralSSV_value == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.collateralSSV_value.Replace(",", "") + "',";
            }
            if (values.forcedvalueassessed_on == null || values.forcedvalueassessed_on == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.forcedvalueassessed_on).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if (values.collateralobservation_summary == null || values.collateralobservation_summary == "")
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.collateralobservation_summary.Replace("'", "") + "',";
            }
            msSQL = " update ocs_mst_tapplication2loan set " +
                    " application_gid ='" + values.application_gid + "'," +
                    " source_type='" + values.source_type + "'," +
                    " guideline_value='" + values.guideline_value + "'," +
                    " guideline_date='" + values.guideline_date + "'," +
                    " marketvalue_date ='" + values.marketvalue_date + "'," +                   
                    " market_value='" + values.market_value + "'," +
                    " forcedsource_value='" + values.forcedsource_value + "'," +
                    " collateralSSV_value='" + values.collateralSSV_value + "'," +
                    " forcedvalueassessed_on='" + values.forcedvalueassessed_on + "'," +
                    " collateralobservation_summary='" + values.collateralobservation_summary + "'" +                  
                   " where application2loan_gid='" + values.application2loan_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Collateral Details Submitted Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }

        }


        public void DaPostHypothecation(string employee_gid, MdlMstHypothecation values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("AP2C");
            msSQL = " insert into ocs_mst_tapplication2hypothecation(" +
                   " application2hypothecation_gid ," +
                   " application_gid," +
                   " securitytype_gid," +
                   " security_type," +
                   " security_description," +
                   " security_value ," +
                   " securityassessed_date," +
                   " asset_id," +
                   " roc_fillingid," +
                   " CERSAI_fillingid," +
                   " hypoobservation_summary," +
                   " primary_security," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.application_gid + "'," +
                   "'" + values.securitytype_gid + "'," +
                   "'" + values.security_type.Replace("'", "") + "',";
            if (values.security_description == null || values.security_description == "")
            {
                //msSQL += "'0.00',";
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + values.security_description.Replace("'", "") + "',";
            }

            if (values.security_value == null || values.security_value == "")
            {
                //msSQL += "'0.00',";
                msSQL += " security_value= '0.00',";
            }
            else
            {
                msSQL += "'" + values.security_value.Replace(",", "") + "',";
            }
            if (values.securityassessed_date == null)
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.securityassessed_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }

            msSQL += "'" + values.asset_id + "'," +
                     "'" + values.roc_fillingid + "'," +
                     "'" + values.CERSAI_fillingid + "',";
            if (values.hypoobservation_summary == null || values.hypoobservation_summary == "")
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.hypoobservation_summary.Replace("'", "") + "',";
            }
            if (values.primary_security == null || values.primary_security == "")
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.primary_security.Replace("'", "") + "',";
            }
            //"'" + values.primary_security.Replace("'", "") + "'," +
            msSQL += "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Hypothecation details Added successfully";

                msSQL = "update ocs_mst_tuploadhypothecationocument set application2hypothecation_gid='" + msGetGid + "' where application2hypothecation_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_mst_tapplication set hypothecation_flag='Y' where application_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

              
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while Adding";
            }
        }
        // Add Facility and Charges
        public void DaPostServiceCharges(string employee_gid, MdlProductCharges values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("AP2C");

            msSQL = "insert into ocs_mst_tapplicationservicecharge(" +
                " application2servicecharge_gid," +
                " application_gid," +
                " processing_fee," +
                " processing_collectiontype," +
                " doc_charges," +
                " doccharge_collectiontype," +
                " fieldvisit_charges," +
                " fieldvisit_charges_collectiontype," +
                " adhoc_fee," +
                " adhoc_collectiontype," +
                " life_insurance," +
                " lifeinsurance_collectiontype," +
                " acct_insurance," +
                " acctinsurance_collectiontype," +
                " total_collect," +
                " total_deduct," +
                " created_by," +
                " created_date) values(" +
                 "'" + msGetGid + "'," +
                       "'" + values.application_gid + "'," +
                       "'" + values.processing_fee + "'," +
                       "'" + values.processing_collectiontype + "'," +
                       "'" + values.doc_charges + "'," +
                       "'" + values.doccharge_collectiontype + "'," +
                       "'" + values.fieldvisit_charge + "'," +
                       "'" + values.fieldvisit_collectiontype + "'," +
                       "'" + values.adhoc_fee + "'," +
                       "'" + values.adhoc_collectiontype + "'," +
                       "'" + values.life_insurance + "'," +
                       "'" + values.lifeinsurance_collectiontype + "'," +
                       "'" + values.acct_insurance + "'," +
                       "'" + values.acctinsurance_collectiontype + "'," +
                       "'" + values.total_collect + "'," +
                       "'" + values.total_deduct + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "ServiceCharges added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }
    }
}