using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.idas.Models;
using System.Configuration;
using System.Drawing;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Pdf;
using Spire.Pdf.HtmlConverter;
using System.Threading;
using ems.storage.Functions;
using Spire.Pdf.Graphics;

namespace ems.idas.DataAccess
{
    public class DaSanctionMst
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        string msSQL, msGetGid, msGetGid1, msGetGidCC;
        int mnResult, mnResult1;
        string lscustomer_urn;
        OdbcDataReader objODBCDataReader, objODBCDataReader1, objODBCDataReader2;
        HttpPostedFile httpPostedFile;
        string lspath, lscompany_code, lsdocument_type;
        string lsloanfacility_type;
        string lsreportstructure;
        string lsfinyear, lssanctionref_no, lsnewref_no;
        string lstemplate_content, fileName;
        string lsUpdatedBy;
        string lsUpdatedDate, lsdocument_path;
        string lscontent = string.Empty;


        public bool DaPostSanctionDetails(string employee_gid, sanctiondetails values)
        {
            if (values.vertical_code == "FPO")
            {
                if (values.paycard == "applicable")
                {
                    values.vertical_code = "FPO-PCS";
                }
            }
            if (values.entity != "SAMAGRO")
            {
                if (values.colanding_status == "No")
                {
                    msSQL = "select verticalref_no from ocs_mst_tentitysequenceno where entity_gid='" + values.entity_gid + "' ";
                    string lssequencecurval = objdbconn.GetExecuteScalar(msSQL);

                    int seq_curval = int.Parse(lssequencecurval) + 1;
                    double lsseq_curval = Math.Floor(Math.Log10(seq_curval) + 1);
                    if (lsseq_curval == 1)
                    {
                        lsnewref_no = values.vertical_code + "/" + "000" + Convert.ToInt32(seq_curval).ToString();
                    }
                    else if (lsseq_curval == 2)
                    {
                        lsnewref_no = values.vertical_code + "/" + "00" + Convert.ToInt32(seq_curval).ToString();
                    }
                    else if (lsseq_curval == 3)
                    {
                        lsnewref_no = values.vertical_code + "/" + "0" + Convert.ToInt32(seq_curval).ToString();
                    }
                    else if (lsseq_curval == 4)
                    {
                        lsnewref_no = values.vertical_code + "/" + Convert.ToInt32(seq_curval).ToString();
                    }

                    msSQL = "update ocs_mst_tentitysequenceno set verticalref_no='" + seq_curval + "' where entity_gid='" + values.entity_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }

                else
                {
                    if (values.colander_name == "DBS")
                    {
                        msSQL = "select colending_dbs from ocs_mst_tentitysequenceno where entity_gid='" + values.entity_gid + "'";
                        string lssequencecurval = objdbconn.GetExecuteScalar(msSQL);

                        int seq_curval = int.Parse(lssequencecurval) + 1;
                        double lsseq_curval = Math.Floor(Math.Log10(seq_curval) + 1);
                        if (lsseq_curval == 1)
                        {
                            lsnewref_no = values.colander_name + "/" + "000" + Convert.ToInt32(seq_curval).ToString();
                        }
                        else if (lsseq_curval == 2)
                        {
                            lsnewref_no = values.colander_name + "/" + "00" + Convert.ToInt32(seq_curval).ToString();
                        }
                        else if (lsseq_curval == 3)
                        {
                            lsnewref_no = values.colander_name + "/" + "0" + Convert.ToInt32(seq_curval).ToString();
                        }
                        else if (lsseq_curval == 4)
                        {
                            lsnewref_no = values.colander_name + "/" + Convert.ToInt32(seq_curval).ToString();
                        }

                        msSQL = "update ocs_mst_tentitysequenceno set colending_dbs='" + seq_curval + "' where entity_gid='" + values.entity_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else
                    {
                        msSQL = "select colending_aar from ocs_mst_tentitysequenceno where entity_gid='" + values.entity_gid + "' ";
                        string lssequencecurval = objdbconn.GetExecuteScalar(msSQL);

                        int seq_curval = int.Parse(lssequencecurval) + 1;
                        double lsseq_curval = Math.Floor(Math.Log10(seq_curval) + 1);
                        if (lsseq_curval == 1)
                        {
                            lsnewref_no = values.colander_name + "/" + "000" + Convert.ToInt32(seq_curval).ToString();
                        }
                        else if (lsseq_curval == 2)
                        {
                            lsnewref_no = values.colander_name + "/" + "00" + Convert.ToInt32(seq_curval).ToString();
                        }
                        else if (lsseq_curval == 3)
                        {
                            lsnewref_no = values.colander_name + "/" + "0" + Convert.ToInt32(seq_curval).ToString();
                        }
                        else if (lsseq_curval == 4)
                        {
                            lsnewref_no = values.colander_name + "/" + Convert.ToInt32(seq_curval).ToString();
                        }

                        msSQL = "update ocs_mst_tentitysequenceno set colending_aar='" + seq_curval + "' where entity_gid='" + values.entity_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
            }
            else
            {
                msSQL = "select verticalref_no from ocs_mst_tentitysequenceno where entity_gid='" + values.entity_gid + "' ";
                string lssequencecurval = objdbconn.GetExecuteScalar(msSQL);

                int seq_curval = int.Parse(lssequencecurval) + 1;
                double lsseq_curval = Math.Floor(Math.Log10(seq_curval) + 1);
                if (lsseq_curval == 1)
                {
                    lsnewref_no = "TF/" + "000" + Convert.ToInt32(seq_curval).ToString();
                }
                else if (lsseq_curval == 2)
                {
                    lsnewref_no = "TF/" + "00" + Convert.ToInt32(seq_curval).ToString();
                }
                else if (lsseq_curval == 3)
                {
                    lsnewref_no = "TF/" + "0" + Convert.ToInt32(seq_curval).ToString();
                }
                else if (lsseq_curval == 4)
                {
                    lsnewref_no = "TF/" + Convert.ToInt32(seq_curval).ToString();
                }

                msSQL = "update ocs_mst_tentitysequenceno set verticalref_no='" + seq_curval + "' where entity_gid='" + values.entity_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            msSQL = " SELECT concat(year(fyear_start) ,'-',(date_format(fyear_start,'%y')+1)) AS finyear FROM adm_mst_tyearendactivities" +
                          " ORDER BY finyear_gid DESC LIMIT 0, 1";
            lsfinyear = objdbconn.GetExecuteScalar(msSQL);

            //var lsnextyear = int.Parse(DateTime.Today.ToString("yy"))+1;

            //lsfinyear = lscuryear + "-" + lsnextyear;


            lssanctionref_no = values.entity + "/" + lsnewref_no + "/" + lsfinyear;

            msSQL = " SELECT customer2sanction_gid FROM ocs_mst_tcustomer2sanction WHERE sanction_refno='" + lssanctionref_no + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows)
            {
                objODBCDataReader.Close();
                values.status = false;
                values.message = "Sanction Reference Number Already Exist";
                return false;
            }
            objODBCDataReader.Close();
            msSQL = "select customer_urn from ocs_mst_tcustomer where customer_gid='" + values.customer_gid + "'";
            lscustomer_urn = objdbconn.GetExecuteScalar(msSQL);


            msGetGid = objcmnfunctions.GetMasterGID("CU2S");

            msSQL = " INSERT INTO ocs_mst_tcustomer2sanction( " +
                           " customer2sanction_gid," +
                           " sanction_refno," +
                           " sanction_date," +
                           " sanction_amount," +
                           " entity," +
                           " entity_gid," +
                           " customer_gid ," +
                           " customer_name," +
                           " customer_urn," +
                           " zonalhead_gid," +
                           " zonalhead_name," +
                           " businesshead_gid," +
                           " businesshead_name," +
                           " clustermgr_gid," +
                           " clustermgr_name," +
                           " creditmgr_gid," +
                           " creditmgr_name," +
                           " relationshipmgr_gid," +
                           " relationshipmgr_name," +
                           " vertical_gid," +
                           " vertical," +
                           " colanding_status," +
                           " colander_name," +
                           " sanction_branch_name," +
                           " sanction_branch_gid," +
                           " sanction_state_gid," +
                           " sanction_state_name," +
                           " paycard," +
                           " esdeclaration_status," +
                           " created_by," +
                           " created_date," +
                           " updated_by," +
                           " updated_date)" +
                           " values(" +
                           "'" + msGetGid + "', " +
                           "'" + lssanctionref_no + "'," +
                           "'" + Convert.ToDateTime(values.sanction_date).ToString("yyyy-MM-dd") + "'," +
                           "'" + values.sanction_amount + "'," +
                           "'" + values.entity + "'," +
                             "'" + values.entity_gid + "'," +
                           "'" + values.customer_gid.Trim() + "'," +
                           "'" + values.customername.Trim() + "'," +
                           "'" + lscustomer_urn + "'," +
                           "'" + values.zonalGid + "'," +
                           "'" + values.zonal_name.Trim() + "'," +
                           "'" + values.businessHeadGid + "'," +
                           "'" + values.businesshead_name.Trim() + "'," +
                           "'" + values.clustermanagerGid + "'," +
                           "'" + values.cluster_manager_name.Trim() + "'," +
                           "'" + values.creditmanagerGid + "'," +
                           "'" + values.creditmanager_name.Trim() + "'," +
                           "'" + values.relationshipMgmtGid + "'," +
                           "'" + values.relationshipmgmt_name.Trim() + "'," +
                           "'" + values.vertical_gid + "'," +
                           "'" + values.vertical_code.Trim() + "'," +
                           "'" + values.colanding_status + "'," +
                     "'" + values.colander_name + "'," +
                     "'" + values.sanction_branch_name + "'," +
                     "'" + values.sanction_branch_gid + "'," +
                     "'" + values.sanction_state_gid + "'," +
                     "'" + values.sanction_state_name + "'," +
                      "'" + values.paycard + "'," +
                      "'" + values.esdeclaration_status + "'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult1 = objdbconn.ExecuteNonQuerySQL(msSQL);



            // Sum of sanction Amount START ..//
            msSQL = "select sum(sanction_amount) from ocs_mst_tcustomer2sanction where customer_gid='" + values.customer_gid + "'";
            string lssanction_amount = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "update ocs_mst_tcustomer set total_sanction='" + lssanction_amount + "' where customer_gid='" + values.customer_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            // Sum of sanction Amount END..//

            msSQL = " select a.customer_gid,b.employee_mobileno,b.employee_emailid,a.address,a.state,a.postalcode,a.contact_no, " +
                   " a.email,a.contactperson from ocs_mst_tcustomer a " +
                   " left join hrm_mst_temployee b on b.employee_gid = a.relationship_manager " +
                   " where customer_gid = '" + values.customer_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                msSQL = " update ocs_mst_tcustomer2sanction set rm_emailid='" + objODBCDataReader["employee_emailid"].ToString() + "'," +
                        " address='" + objODBCDataReader["address"].ToString() + "'," +
                        " state='" + objODBCDataReader["state"].ToString() + "'," +
                        " pincode='" + objODBCDataReader["postalcode"].ToString() + "'," +
                        " contact_number='" + objODBCDataReader["contact_no"].ToString() + "'," +
                        " email_id='" + objODBCDataReader["email"].ToString() + "'," +
                        " contact_person='" + objODBCDataReader["contactperson"].ToString() + "'," +
                        " rm_phoneno='" + objODBCDataReader["employee_mobileno"].ToString() + "' where customer2sanction_gid='" + msGetGid + "'";
                objODBCDataReader.Close();
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            objODBCDataReader.Close();

            if (mnResult1 != 0)
            {
                // Update Event for CAM, MOM and Sanction Letter Upload
                msSQL = "update ids_trn_tuploadmomdocument set customer2sanction_gid='" + msGetGid + "' where customer2sanction_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ids_trn_tuploadcamdocument set customer2sanction_gid='" + msGetGid + "' where customer2sanction_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ids_trn_tuploadsanctionletter set customer2sanction_gid='" + msGetGid + "' where customer2sanction_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ids_trn_tuploadesdeclarationdocument set customer2sanction_gid='" + msGetGid + "' where customer2sanction_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ids_trn_tdeviationmaildocument set customer2sanction_gid='" + msGetGid + "' where customer2sanction_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Sanction Created Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Creating Sanction";
                return false;

            }


        }

        public bool DaGetSanctionDtlList(sanctiondetailsList values)
        {

            msSQL = " SELECT a.customer2sanction_gid,b.customer_urn,a.sanction_refno,date_format(a.sanction_date,'%d-%m-%Y') as sanction_date, " +
                   " format((sanction_amount),2) as sanction_amount,a.sanction_limit,b.customername,b.vertical_code," +
                   " concat(d.user_code,' / ',d.user_firstname,d.user_lastname) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                   " reset_flag FROM ocs_mst_tcustomer2sanction a " +
                   " LEFT JOIN ocs_mst_tcustomer b ON a.customer_gid = b.customer_gid" +
                   " LEFT JOIN hrm_mst_temployee c ON a.updated_by=c.employee_gid" +
                   " LEFT JOIN adm_mst_tuser d ON c.user_gid=d.user_gid where updated_flag='N'" +
                   " ORDER BY customer2sanction_gid DESC";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_sanctiondtl = new List<sanctiondetails>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    get_sanctiondtl.Add(new sanctiondetails
                    {
                        customer2sanction_gid = dt["customer2sanction_gid"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        sanction_date = dt["sanction_date"].ToString(),
                        sanction_amount = dt["sanction_amount"].ToString(),
                        sanction_limit = dt["sanction_limit"].ToString(),
                        customername = dt["customername"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_code = dt["vertical_code"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        reset_flag = dt["reset_flag"].ToString(),
                    });
                }
                values.sanctiondetails = get_sanctiondtl;
            }
            dt_datatable.Dispose();
            msSQL = " SELECT a.customer2sanction_gid,b.customer_urn,a.sanction_refno,date_format(a.sanction_date,'%d-%m-%Y') as sanction_date, " +
                 " format((sanction_amount),2) as sanction_amount,a.sanction_limit,b.customername,b.vertical_code," +
                 " concat(d.user_code,' / ',d.user_firstname,d.user_lastname) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                 " reset_flag FROM ocs_mst_tcustomer2sanction a " +
                 " LEFT JOIN ocs_mst_tcustomer b ON a.customer_gid = b.customer_gid" +
                 " LEFT JOIN hrm_mst_temployee c ON a.updated_by=c.employee_gid" +
                 " LEFT JOIN adm_mst_tuser d ON c.user_gid=d.user_gid where updated_flag='Y'" +
                 " ORDER BY customer2sanction_gid DESC";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcompletedsanctiondtl = new List<completed_sanctiondetails>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getcompletedsanctiondtl.Add(new completed_sanctiondetails
                    {
                        customer2sanction_gid = dt["customer2sanction_gid"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        sanction_date = dt["sanction_date"].ToString(),
                        sanction_amount = dt["sanction_amount"].ToString(),
                        sanction_limit = dt["sanction_limit"].ToString(),
                        customername = dt["customername"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_code = dt["vertical_code"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        reset_flag = dt["reset_flag"].ToString()
                    });
                }
                values.completed_sanctiondetails = getcompletedsanctiondtl;
            }
            dt_datatable.Dispose();

            msSQL = "select count(customer2sanction_gid) from ocs_mst_tcustomer2sanction where updated_flag='N'";
            values.pending_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(customer2sanction_gid) from ocs_mst_tcustomer2sanction where updated_flag='Y'";
            values.completed_count = objdbconn.GetExecuteScalar(msSQL);

            return true;
        }

        public bool DaGetPendingSanctionDtlList(sanctiondetailsList values)
        {

            msSQL = " SELECT a.customer2sanction_gid,b.customer_urn,a.sanction_refno,date_format(a.sanction_date,'%d-%m-%Y') as sanction_date, " +
                   " format((sanction_amount),2) as sanction_amount,a.sanction_limit,b.customername,b.vertical_code," +
                   " concat(d.user_code,' / ',d.user_firstname,d.user_lastname) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                   " reset_flag FROM ocs_mst_tcustomer2sanction a " +
                   " LEFT JOIN ocs_mst_tcustomer b ON a.customer_gid = b.customer_gid" +
                   " LEFT JOIN hrm_mst_temployee c ON a.updated_by=c.employee_gid" +
                   " LEFT JOIN adm_mst_tuser d ON c.user_gid=d.user_gid where updated_flag='N'" +
                   " ORDER BY customer2sanction_gid DESC";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_sanctiondtl = new List<sanctiondetails>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    get_sanctiondtl.Add(new sanctiondetails
                    {
                        customer2sanction_gid = dt["customer2sanction_gid"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        sanction_date = dt["sanction_date"].ToString(),
                        sanction_amount = dt["sanction_amount"].ToString(),
                        sanction_limit = dt["sanction_limit"].ToString(),
                        customername = dt["customername"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_code = dt["vertical_code"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        reset_flag = dt["reset_flag"].ToString(),
                    });
                }
                values.sanctiondetails = get_sanctiondtl;
            }
            dt_datatable.Dispose();


            msSQL = "select count(customer2sanction_gid) from ocs_mst_tcustomer2sanction where updated_flag='N'";
            values.pending_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(customer2sanction_gid) from ocs_mst_tcustomer2sanction where updated_flag='Y'";
            values.completed_count = objdbconn.GetExecuteScalar(msSQL);

            return true;
        }


        public bool DaGetCompletedSanctionDtlList(sanctiondetailsList values)
        {
            msSQL = " SELECT a.customer2sanction_gid,b.customer_urn,a.sanction_refno,date_format(a.sanction_date,'%d-%m-%Y') as sanction_date, " +
                 " format((sanction_amount),2) as sanction_amount,a.sanction_limit,b.customername,b.vertical_code, sanctionletter_status, " +
                 " concat(d.user_code,' / ',d.user_firstname,d.user_lastname) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                 " reset_flag, sanction_status FROM ocs_mst_tcustomer2sanction a " +
                 " LEFT JOIN ocs_mst_tcustomer b ON a.customer_gid = b.customer_gid" +
                 " LEFT JOIN hrm_mst_temployee c ON a.updated_by=c.employee_gid" +
                 " LEFT JOIN adm_mst_tuser d ON c.user_gid=d.user_gid where updated_flag='Y'" +
                 " ORDER BY customer2sanction_gid DESC";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcompletedsanctiondtl = new List<completed_sanctiondetails>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcompletedsanctiondtl.Add(new completed_sanctiondetails
                    {
                        customer2sanction_gid = dt["customer2sanction_gid"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        sanction_date = dt["sanction_date"].ToString(),
                        sanction_amount = dt["sanction_amount"].ToString(),
                        sanction_limit = dt["sanction_limit"].ToString(),
                        customername = dt["customername"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_code = dt["vertical_code"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        reset_flag = dt["reset_flag"].ToString(),
                        sanctionletter_status = dt["sanctionletter_status"].ToString(),
                        sanction_status = dt["sanction_status"].ToString(),
                    });
                }
                values.completed_sanctiondetails = getcompletedsanctiondtl;
            }
            dt_datatable.Dispose();

            msSQL = "select count(customer2sanction_gid) from ocs_mst_tcustomer2sanction where updated_flag='N'";
            values.pending_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(customer2sanction_gid) from ocs_mst_tcustomer2sanction where updated_flag='Y'";
            values.completed_count = objdbconn.GetExecuteScalar(msSQL);

            return true;
        }

        public bool DaPostSanctionUpdate(sanctiondetails values, string user_gid, string employee_gid)
        {
            msSQL = " select customer2sanction_gid from ocs_mst_tcustomer2sanction" +
                " where sanction_refno='" + values.sanction_refno + "'";
            var sanctionGID = objdbconn.GetExecuteScalar(msSQL);
            if (sanctionGID != "")
            {
                if (sanctionGID != values.customer2sanction_gid)
                {
                    values.message = "Santion Reference Number Already Exist";
                    values.status = false;
                    return false;
                }

            }

            if (values.sanction_type == "Existing Customer")
            {
                if ((values.natureof_proposal == null) || (values.natureof_proposal == ""))
                {
                    values.message = "Kindly Select Nature of Proposal";
                    values.status = false;
                    return false;
                }
            }

            msSQL = "select customer_gid, customer_name, customer_urn, updated_by, updated_date from ocs_mst_tcustomer2sanction" +
                " where customer2sanction_gid = '" + values.customer2sanction_gid + "'";

            objODBCDataReader = objdbconn.GetDataReader(msSQL);

            if (objODBCDataReader.HasRows == true)
            {
                lsUpdatedBy = objODBCDataReader["updated_by"].ToString();
                lsUpdatedDate = objODBCDataReader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("SUPL");



                    msSQL = " insert into ocs_trn_tsanctionupdatedlog(" +
                              " sanctionupdatedlog_gid," +
                              " customer2sanction_gid," +
                              " sanction_refno," +
                              " customer_gid," +
                              " customer_name," +
                              " customer_urn," +
                              " lastupdated_by," +
                              " lastupdated_date," +
                              " created_by," +
                              " created_date)" +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.customer2sanction_gid + "'," +
                              "'" + values.sanction_refno + "'," +
                              "'" + objODBCDataReader["customer_gid"].ToString() + "'," +
                              "'" + objODBCDataReader["customer_name"].ToString() + "'," +
                              "'" + objODBCDataReader["customer_urn"].ToString() + "'," +
                              "'" + objODBCDataReader["updated_by"].ToString() + "'," +
                              "'" + objODBCDataReader["updated_date"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }

            }

            objODBCDataReader.Close();



            msSQL = " UPDATE ocs_mst_tcustomer2sanction SET" +
                    " sanction_refno='" + values.sanction_refno + "'," +
                    " sanction_date='" + Convert.ToDateTime(values.sanction_date).ToString("yyyy-MM-dd") + "'," +
                    " sanction_amount='" + values.sanction_amount.Replace(",", "").Trim() + "'," +
                    " vertical='" + values.vertical + "'," +
                    " entity='" + values.entity + "',";

            if (values.purpose_lending == null)
            {
                msSQL += " purpose_lending='',";

            }
            else
            {
                msSQL += " purpose_lending='" + values.purpose_lending.Replace("'", "\\'") + "',";
            }
            if (values.facility_secure_type == null)
            {
                msSQL += " facility_secure_type='',";

            }
            else
            {
                msSQL += " facility_secure_type='" + values.facility_secure_type + "',";
            }
            if (values.product_solution == null)
            {
                msSQL += " product_solution='',";

            }
            else
            {
                msSQL += " product_solution='" + values.product_solution.Replace("'", "\\'") + "',";

            }
            if (values.major_intervention == null)
            {
                msSQL += " major_intervention='',";

            }
            else
            {
                msSQL += " major_intervention='" + values.major_intervention.Replace("'", "\\'") + "',";
            }
            if (values.primary_value_chain == null)
            {
                msSQL += " primary_value_chain='',";

            }
            else
            {
                msSQL += " primary_value_chain='" + values.primary_value_chain.Replace("'", "\\'") + "',";
            }
            if (values.secondary_value_chain == null)
            {
                msSQL += " secondary_value_chain='',";

            }
            else
            {
                msSQL += " secondary_value_chain='" + values.secondary_value_chain.Replace("'", "\\'") + "',";
            }
            msSQL += " sanction_branch_name='" + values.sanction_branch_name + "'," +
                          " sanction_branch_gid='" + values.sanction_branch_gid + "'," +
                          " sanction_state_gid='" + values.sanction_state_gid + "'," +
                          " sanction_state_name='" + values.sanction_state_name + "'," +
                          " status_ofBAL='" + values.status_ofBAL + "'," +
                          " virtual_accountno='" + values.virtual_accountno + "'," +
                          " ccapproved_by='" + values.ccapproved_by + "'," +
                          " ccapprovedby_gid='" + values.ccapprovedby_gid + "'," +
                          " ccapproved_date='" + Convert.ToDateTime(values.ccapproved_date).ToString("yyyy-MM-dd") + "'," +
                          " ccdecision='" + values.ccdecision + "'," +
                          " ccfeedback='" + values.ccfeedback + "'," +
                          " general_remarks='" + values.general_remarks + "'," +
                           " sanction_type='" + values.sanction_type + "'," +
                           " natureof_proposal='" + values.natureof_proposal + "'," +
                           " colanding_status='" + values.colanding_status + "'," +
                           " colander_name='" + values.colander_name + "'," +
                           " updated_flag='Y'," +
                           " external_rating='" + values.external_rating + "'," +
                           " business_description='" + values.business_description + "'," +
                           " associate_name='" + values.associate_name + "'," +
                           " sa_payout='" + values.sa_payout + "'," +
                           " typeof_enterprise='" + values.typeof_enterprise + "'," +
                           " risk_categorization='" + values.risk_categorization + "'," +
                           " es_application='" + values.es_application + "'," +
                           " esrisk_categorization='" + values.esrisk_categorization + "'," +
                           " internal_rating='" + values.internal_rating + "'," +
                           " status_ofPSL='" + values.status_ofPSL + "'," +
                           " msme_classification='" + values.msme_classification + "'," +
                           " industry='" + values.industry + "'," +
                           " invesment_pme='" + values.invesment_pme + "'," +
                           " turn_over='" + values.turn_over + "'," +
                           " regno_msme='" + values.regno_msme + "'," +
                           " validity_months='" + values.validity_months + "'," +
                          " penal_interest='" + values.penal_interest + "'," +
                          " paycard = '" + values.paycard + "'," +
                          " esdeclaration_status = '" + values.esdeclaration_status + "'," +
                          " updated_by='" + employee_gid + "'," +
                          " updated_date=current_timestamp" +
                          " WHERE customer2sanction_gid='" + values.customer2sanction_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {



                msSQL = " UPDATE ocs_trn_tloan SET" +
                                 " sanction_refno='" + values.sanction_refno.Replace("'", "\\'") + "'," +
                                 " sanction_date='" + Convert.ToDateTime(values.sanction_date).ToString("yyyy-MM-dd") + "'" +
                                 " WHERE sanction_gid='" + values.customer2sanction_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "select sum(sanction_amount) from ocs_mst_tcustomer2sanction " +
                                    " where customer_gid = (select customer_gid from ocs_mst_tcustomer2sanction " +
                                    " where customer2sanction_gid ='" + values.customer2sanction_gid + "')";
                string lssanction_amount = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "update ocs_mst_tcustomer set total_sanction='" + lssanction_amount + "' " +
                       "where customer_gid=(select customer_gid from ocs_mst_tcustomer2sanction " +
                       " where customer2sanction_gid ='" + values.customer2sanction_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " UPDATE ocs_trn_tdeferral a " +
                       " LEFT JOIN ocs_trn_tloan b on a.loan_gid=b.loan_gid" +
                       " SET" +
                       " a.sanction_refno='" + values.sanction_refno.Replace("'", "\\'") + "'," +
                       " a.sanction_date='" + Convert.ToDateTime(values.sanction_date).ToString("yyyy-MM-dd") + "'" +
                       " WHERE b.sanction_gid='" + values.customer2sanction_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " UPDATE ocs_trn_tdeferral2loan a " +
                        " LEFT JOIN ocs_trn_tloan b on a.loan_gid=b.loan_gid" +
                        " SET" +
                        " a.sanction_refno='" + values.sanction_refno.Replace("'", "\\'") + "'," +
                       " a.sanction_date='" + Convert.ToDateTime(values.sanction_date).ToString("yyyy-MM-dd") + "'" +
                       " WHERE b.sanction_gid='" + values.customer2sanction_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //Document Upload..//
                msSQL = "update ids_trn_tuploadgeneraldocument set customer2sanction_gid='" + values.customer2sanction_gid + "' where customer2sanction_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ids_trn_tuploadmomdocument set customer2sanction_gid='" + values.customer2sanction_gid + "' where customer2sanction_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ids_trn_tuploadcamdocument set customer2sanction_gid='" + values.customer2sanction_gid + "' where customer2sanction_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ids_trn_tuploadsanctionletter set customer2sanction_gid='" + values.customer2sanction_gid + "' where customer2sanction_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ids_trn_tuploadesdeclarationdocument set customer2sanction_gid='" + values.customer2sanction_gid + "' where customer2sanction_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ids_trn_tdeviationmaildocument set customer2sanction_gid='" + values.customer2sanction_gid + "' where customer2sanction_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //Completed..//
                //Buyer...//

                msSQL = "update ids_mst_taddbuyer set customer2sanction_gid='" + values.customer2sanction_gid + "' where customer2sanction_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //Completed..//
                //CC Member//
                msSQL = "update ocs_mst_tsanction2ccmemberlist set customer2sanction_gid='" + values.customer2sanction_gid + "' where customer2sanction_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //Completed..//
                //Facility Type//
                msSQL = "update ocs_mst_tsanction2loanfacilitytype set customer2sanction_gid='" + values.customer2sanction_gid + "' where customer2sanction_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " select group_concat(loanfacility_type) from ocs_mst_tsanction2loanfacilitytype where customer2sanction_gid='" + values.customer2sanction_gid + "'";


                lsloanfacility_type = objdbconn.GetExecuteScalar(msSQL);


                msSQL = "update ocs_mst_tcustomer2sanction set facility_type='" + lsloanfacility_type + "' where customer2sanction_gid='" + values.customer2sanction_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //Completed..//

                msSQL = "delete from ocs_mst_tsanction2loanfacilitytype where delete_flag='Y' and updated_by='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "delete from ocs_mst_tsanction2ccmemberlist where delete_flag='Y' and updated_by='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "delete from ids_mst_taddbuyer where delete_flag='Y' and updated_by='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "delete from ids_trn_tuploadbaldocument where delete_flag='Y' and updated_by='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "delete from ids_trn_tuploadcamdocument where delete_flag='Y' and updated_by='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "delete from ids_trn_tuploadmomdocument where delete_flag='Y' and updated_by='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "delete from ids_trn_tuploadgeneraldocument where delete_flag='Y' and updated_by='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "delete from ids_trn_tuploadsanctionletter where delete_flag='Y' and updated_by='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "delete from ids_trn_tuploadesdeclarationdocument where delete_flag='Y' and updated_by='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "delete from ids_trn_tdeviationmaildocument where delete_flag='Y' and updated_by='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }
            if (mnResult == 1)
            {

                values.status = true;
                values.message = "Sanction Updated Successfully...!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }
        }

        public bool DaPostSanctionDetails1(sanctiondetails values, string user_gid)
        {
            msSQL = " UPDATE ocs_mst_tcustomer2sanction SET" +
                   " approval_authority='" + values.approval_authority + "'," +
                   " earlier_sanctionrefno='" + values.earlier_sanctionrefno + "'," +
                   " CCapproval_date='" + Convert.ToDateTime(values.CCapproval_date).ToString("yyyy-MM-dd") + "'," +
                   " nature_ofproposal='" + values.nature_ofproposal + "'," +
                   " classification_MSME='" + values.classification_MSME.Replace("'", "\\'") + "'," +
                   " sanction_validity='" + values.sanction_validity.Replace("'", "\\'") + "'," +
                   " sanction_expirydate='" + Convert.ToDateTime(values.sanction_expirydate).ToString("yyyy-MM-dd") + "'," +
                   " sanction_reviewdate='" + Convert.ToDateTime(values.sanction_reviewdate).ToString("yyyy-MM-dd") + "'," +
                   " constitution='" + values.constitution.Replace("'", "\\'") + "'," +
                   " authorized_signatoryGid='" + values.authorized_signatory + "'," +
                   " existing_limit='" + values.existing_limit + "'," +
                   " additional_proposedlimit='" + values.additional_proposedlimit + "'," +
                   " overall_limit='" + values.overall_limit + "'," +
                   " purpose='" + values.purpose.Replace("'", "\\'") + "'," +
                   " tenure_months='" + values.tenure_months.Replace("'", "\\'") + "'," +
                   " repayment_principal='" + values.repayment_principal.Replace("'", "\\'") + "'," +
                   " repayment_interest='" + values.repayment_interest.Replace("'", "\\'") + "'," +
                   " primary_security='" + values.primary_security.Replace("'", "\\'") + "'," +
                   " collateral_security='" + values.collateral_security.Replace("'", "\\'") + "'," +
                   " updated_by='" + user_gid + "'," +
                   " updated_date=current_timestamp" +
                   " WHERE customer2sanction_gid='" + values.customer2sanction_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Sanction Details Updated Successfully...!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }
        }


        public bool DaPostSanctionDetails2(sanctiondetails values, string user_gid)
        {
            msSQL = " UPDATE ocs_mst_tcustomer2sanction SET" +
                  " securitycheque_bankname='" + values.securitycheque_bankname.Replace("'", "\\'") + "'," +
                  " securitycheque_accountnumber='" + values.securitycheque_accountnumber.Replace("'", "\\'") + "'," +
                  " margin='" + values.margin.Replace("'", "\\'") + "'," +
                  " rateof_interest='" + values.rateof_interest.Replace("'", "\\'") + "'," +
                  " penal_interest='" + values.penal_interest.Replace("'", "\\'") + "'," +
                  " processing_fee='" + values.processing_fee.Replace("'", "\\'") + "'," +
                  " bankand_chequeno='" + values.bankand_chequeno.Replace("'", "\\'") + "'," +
                  " cheque_realizationdate='" + Convert.ToDateTime(values.cheque_realizationdate).ToString("yyyy-MM-dd") + "'," +
                  " documentation_clientvisitcharge='" + values.documentation_clientvisitcharge.Replace("'", "\\'") + "'," +
                  " GST_number='" + values.GST_number.Replace("'", "\\'") + "'," +
                  " modeof_operations='" + values.modeof_operations.Replace("'", "\\'") + "'," +
                  " specific_condition='" + values.specific_condition.Replace("'", "\\'") + "'," +
                  " dateof_receiptDocsVetting='" + values.dateof_receiptDocsVetting + "'," +
                  " NACH_form='" + values.NACH_form.Replace("'", "\\'") + "'," +
                  " escrow_account='" + values.escrow_account + "'," +
                  " virtual_accountno='" + values.virtual_accountno.Replace("'", "\\'") + "'," +
                  " nameofthe_buyers='" + values.nameofthe_buyers.Replace("'", "\\'") + "'," +
                  " status_ofBAL='" + values.status_ofBAL.Replace("'", "\\'") + "', " +
                  " roc_applicable='" + values.roc_applicable.Replace("'", "\\'") + "'," +
                  " roc_status='" + values.roc_status.Replace("'", "\\'") + "'," +
                  " cersai_status='" + values.cersai_status.Replace("'", "\\'") + "', " +
                  " nesl_status='" + values.nesl_status.Replace("'", "\\'") + "'," +
                  " updated_by='" + user_gid + "'," +
                  " updated_date=current_timestamp" +
                  " WHERE customer2sanction_gid='" + values.customer2sanction_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Sanction Details Updated Successfully...!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }
        }

        public bool DaPostSanctionDetails3(sanctiondetails values, string user_gid)
        {
            msSQL = " UPDATE ocs_mst_tcustomer2sanction SET" +
                  " predisbursement_deferal='" + values.predisbursement_deferal.Replace("'", "\\'") + "'," +
                  " dateof_deviation='" + Convert.ToDateTime(values.dateof_deviation).ToString("yyyy-MM-dd") + "'," +
                  " statuspre_disbursementdeferal='" + values.statuspre_disbursementdeferal.Replace("'", "\\'") + "'," +
                  " postdisbursement_covanent='" + values.postdisbursement_covanent.Replace("'", "\\'") + "'," +
                  " statuspost_disbursementcovanent='" + values.statuspost_disbursementcovanent.Replace("'", "\\'") + "'," +
                  " dateof_releaseorder='" + Convert.ToDateTime(values.dateof_releaseorder).ToString("yyyy-MM-dd") + "'," +
                  " roissuing_totalamount='" + values.roissuing_totalamount.Replace("'", "\\'") + "'," +
                  " casesvetted_bycad='" + values.casesvetted_bycad.Replace("'", "\\'") + "'," +
                  " originaldocs_receivedHO='" + values.originaldocs_receivedHO.Replace("'", "\\'") + "'," +
                  " scanneduploaded_Drive='" + values.scanneduploaded_Drive.Replace("'", "\\'") + "'," +
                  " monitoring_visit='" + values.monitoring_visit.Replace("'", "\\'") + "'," +
                  " bank_statement='" + values.bank_statement.Replace("'", "\\'") + "'," +
                  " audited_financials='" + values.audited_financials.Replace("'", "\\'") + "'," +
                  " stock_statement='" + values.stock_statement.Replace("'", "\\'") + "'," +
                  " purchase_statement='" + values.purchase_statement.Replace("'", "\\'") + "'," +
                  " sales_statement='" + values.sales_statement.Replace("'", "\\'") + "'," +
                  " debtors_statement='" + values.debtors_statement.Replace("'", "\\'") + "'," +
                  " provisionalfinancial_gst='" + values.provisionalfinancial_gst.Replace("'", "\\'") + "'," +
                  " roc_30daysfromSLonetime='" + values.roc_30daysfromSLonetime.Replace("'", "\\'") + "'," +
                  " noliability_certificate='" + values.noliability_certificate.Replace("'", "\\'") + "'," +
                  " buyerconfirmation_letter='" + values.buyerconfirmation_letter.Replace("'", "\\'") + "'," +
                  " copyof_warehousereceipt='" + values.copyof_warehousereceipt.Replace("'", "\\'") + "'," +
                  " insurance_30daysfromSLonetime='" + values.insurance_30daysfromSLonetime.Replace("'", "\\'") + "'," +
                  " loandisbursement_dtlfarmermember='" + values.loandisbursement_dtlfarmermember.Replace("'", "\\'") + "'," +
                  " others='" + values.others.Replace("'", "\\'") + "'," +
                  " updated_by='" + user_gid + "'," +
                  " updated_date=current_timestamp" +
                  " WHERE customer2sanction_gid='" + values.customer2sanction_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Sanction Details Updated Successfully...!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }
        }

        public void DaGetSanctionEdit(string sanction_gid, sanctiondetails values)
        {
            try
            {
                msSQL = " SELECT a.entity,a.customer2sanction_gid,b.customer_gid,b.customer_urn,a.sanction_refno,sanction_date,b.customer_code,a.state,a.batch_status, " +
                "  format((sanction_amount), 2) as sanction_amount,b.customername, date_format(sanction_date,'%d-%m-%Y') as sanctionDate," +
                 " b.zonal_head,b.zonal_name,b.business_head,b.businesshead_name,b.relationship_manager,b.relationshipmgmt_name, " +
                 " b.cluster_manager_gid,b.cluster_manager_name,b.creditmanager_gid,b.creditmgmt_name, " +
                 " a.facility_type,a.facilitytype_gid,a.collateral_security,a.entity_gid, " +
                 " b.contactperson,a.address,b.address2,b.mobileno,approval_authority, " +
                 " b.vertical_code,loan_type,CCapproval_date,nature_ofproposal,classification_MSME,sanction_validity, " +
                 " sanction_expirydate ,sanction_reviewdate , " +
                 " earlier_sanctionrefno,constitution,pincode,contact_number,email_id ,contact_person ,rm_name ,rm_phoneno ,rm_emailid , " +
                 " authorized_signatoryGid ,credit_manager ,existing_limit ,additional_proposedlimit ,overall_limit ,revisied_limit , " +
                 " purpose ,tenure_months ,repayment_principal ,repayment_interest ,primary_security ,personal_guarantee , " +
                 " securitycheque_bankname ,securitycheque_accountnumber ,margin ,rateof_interest ,penal_interest ,processing_fee ,bankand_chequeno, " +
                 " cheque_realizationdate ,documentation_clientvisitcharge ,a.GST_number ,modeof_operations ,specific_condition ,dateof_receiptDocsVetting , " +
                 " NACH_form ,escrow_account ,virtual_accountno ,nameofthe_buyers ,status_ofBAL ,roc_applicable ,roc_status ,cersai_status ,nesl_status, " +
                 " predisbursement_deferal ,dateof_deviation ,statuspre_disbursementdeferal ,postdisbursement_covanent ,statuspost_disbursementcovanent , " +
                 " dateof_releaseorder ,roissuing_totalamount ,casesvetted_bycad ,originaldocs_receivedHO ,scanneduploaded_Drive ,monitoring_visit ,bank_statement , " +
                 " audited_financials ,stock_statement ,purchase_statement ,sales_statement ,debtors_statement ,provisionalfinancial_gst ,roc_30daysfromSLonetime , " +
                 " noliability_certificate ,buyerconfirmation_letter ,copyof_warehousereceipt ,insurance_30daysfromSLonetime ,loandisbursement_dtlfarmermember , " +
                 " others,a.purpose_lending,a.facility_secure_type,a.product_solution,a.major_intervention,a.primary_value_chain,a.secondary_value_chain, " +
                 " sanction_branch_gid,sanction_branch_name,sanction_state_gid,sanction_state_name,ccapproved_by,ccapproved_date,date_format(ccapproved_date,'%d-%m-%Y') as ccapprovedDate,ccdecision," +
                 " ccfeedback,general_remarks,sanction_type,natureof_proposal,colanding_status,colander_name,a.external_rating,a.business_description," +
                 " a.associate_name,a.sa_payout,a.typeof_enterprise,a.risk_categorization, a.internal_rating,a.status_ofPSL,a.msme_classification," +
                 " a.industry,a.invesment_pme,a.turn_over,a.regno_msme,a.validity_months,a.paycard,ccapprovedby_gid, a.es_application, a.esrisk_categorization," +
                 " a.esdeclaration_status,a.makerfile_path,a.makerfile_name, checkerletter_flag, checkerapproval_flag FROM ocs_mst_tcustomer2sanction a " +
                 " LEFT JOIN ocs_mst_tcustomer b ON a.customer_gid = b.customer_gid " +
                 " WHERE customer2sanction_gid ='" + sanction_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows)
                {
                    values.ccapproved_date = objODBCDataReader["ccapprovedDate"].ToString();
                    values.batch_status = objODBCDataReader["batch_status"].ToString();
                    values.customer2sanction_gid = objODBCDataReader["customer2sanction_gid"].ToString();
                    values.sanction_refno = objODBCDataReader["sanction_refno"].ToString();
                    values.sanction_date = objODBCDataReader["sanctionDate"].ToString();

                    if (objODBCDataReader["sanction_date"].ToString() != "")
                    {
                        values.sanctionDate = Convert.ToDateTime(objODBCDataReader["sanction_date"].ToString());
                    }
                    values.facility_type = objODBCDataReader["facility_type"].ToString();
                    values.entity = objODBCDataReader["entity"].ToString();
                    values.customer_gid = objODBCDataReader["customer_gid"].ToString();
                    values.customername = objODBCDataReader["customername"].ToString();
                    values.customer_urn = objODBCDataReader["customer_urn"].ToString();
                    values.customercode = objODBCDataReader["customer_code"].ToString();
                    values.approval_authority = objODBCDataReader["approval_authority"].ToString();
                    values.state = objODBCDataReader["state"].ToString();
                    values.vertical = objODBCDataReader["vertical_code"].ToString();

                    values.loan_type = objODBCDataReader["loan_type"].ToString();

                    if (objODBCDataReader["CCapproval_date"].ToString() != "")
                    {
                        values.ccapproval_date = Convert.ToDateTime(objODBCDataReader["CCapproval_date"].ToString());
                    }
                    values.nature_ofproposal = objODBCDataReader["nature_ofproposal"].ToString();
                    values.classification_MSME = objODBCDataReader["classification_MSME"].ToString();
                    values.sanction_validity = objODBCDataReader["sanction_validity"].ToString();

                    if (objODBCDataReader["sanction_expirydate"].ToString() != "")
                    {
                        values.sanctionexpiry_Date = Convert.ToDateTime(objODBCDataReader["sanction_expirydate"].ToString());
                    }
                    if (objODBCDataReader["sanction_reviewdate"].ToString() != "")
                    {
                        values.sanctionreview_Date = Convert.ToDateTime(objODBCDataReader["sanction_reviewdate"].ToString());
                    }
                    values.earlier_sanctionrefno = objODBCDataReader["earlier_sanctionrefno"].ToString();
                    values.constitution = objODBCDataReader["constitution"].ToString();
                    values.pincode = objODBCDataReader["pincode"].ToString();
                    values.contact_number = objODBCDataReader["contact_number"].ToString();
                    values.email_id = objODBCDataReader["email_id"].ToString();
                    values.contact_person = objODBCDataReader["contact_person"].ToString();
                    values.rm_phoneno = objODBCDataReader["rm_phoneno"].ToString();
                    values.rm_emailid = objODBCDataReader["rm_emailid"].ToString();
                    values.authorized_signatory = objODBCDataReader["authorized_signatoryGid"].ToString();
                    values.credit_manager = objODBCDataReader["credit_manager"].ToString();
                    values.existing_limit = objODBCDataReader["existing_limit"].ToString();
                    values.additional_proposedlimit = objODBCDataReader["additional_proposedlimit"].ToString();
                    values.overall_limit = objODBCDataReader["overall_limit"].ToString();
                    values.revisied_limit = objODBCDataReader["revisied_limit"].ToString();
                    values.purpose = objODBCDataReader["purpose"].ToString();
                    values.tenure_months = objODBCDataReader["tenure_months"].ToString();
                    values.repayment_principal = objODBCDataReader["repayment_principal"].ToString();
                    values.repayment_interest = objODBCDataReader["repayment_interest"].ToString();
                    values.primary_security = objODBCDataReader["primary_security"].ToString();
                    values.collateral_security = objODBCDataReader["collateral_security"].ToString();
                    values.personal_guarantee = objODBCDataReader["personal_guarantee"].ToString();
                    values.securitycheque_bankname = objODBCDataReader["securitycheque_bankname"].ToString();
                    values.securitycheque_accountnumber = objODBCDataReader["securitycheque_accountnumber"].ToString();
                    values.margin = objODBCDataReader["margin"].ToString();
                    values.rateof_interest = objODBCDataReader["rateof_interest"].ToString();
                    values.penal_interest = objODBCDataReader["penal_interest"].ToString();
                    values.processing_fee = objODBCDataReader["processing_fee"].ToString();
                    values.bankand_chequeno = objODBCDataReader["bankand_chequeno"].ToString();
                    if (objODBCDataReader["cheque_realizationdate"].ToString() != "")
                    {
                        values.chequerealizationDate = Convert.ToDateTime(objODBCDataReader["cheque_realizationdate"].ToString());
                    }
                    values.documentation_clientvisitcharge = objODBCDataReader["documentation_clientvisitcharge"].ToString();
                    values.GST_number = objODBCDataReader["GST_number"].ToString();
                    values.modeof_operations = objODBCDataReader["modeof_operations"].ToString();
                    values.specific_condition = objODBCDataReader["specific_condition"].ToString();
                    values.dateof_receiptDocsVetting = objODBCDataReader["dateof_receiptDocsVetting"].ToString();
                    values.NACH_form = objODBCDataReader["NACH_form"].ToString();
                    values.escrow_account = objODBCDataReader["escrow_account"].ToString();
                    values.virtual_accountno = objODBCDataReader["virtual_accountno"].ToString();
                    values.nameofthe_buyers = objODBCDataReader["nameofthe_buyers"].ToString();
                    values.status_ofBAL = objODBCDataReader["status_ofBAL"].ToString();
                    values.roc_applicable = objODBCDataReader["roc_applicable"].ToString();
                    values.roc_status = objODBCDataReader["roc_status"].ToString();
                    values.cersai_status = objODBCDataReader["cersai_status"].ToString();
                    values.nesl_status = objODBCDataReader["nesl_status"].ToString();
                    values.predisbursement_deferal = objODBCDataReader["predisbursement_deferal"].ToString();

                    if (objODBCDataReader["dateof_deviation"].ToString() != "")
                    {
                        values.deviation_Date = Convert.ToDateTime(objODBCDataReader["dateof_deviation"].ToString());
                    }
                    if (objODBCDataReader["dateof_releaseorder"].ToString() != "")
                    {
                        values.releaseorder_Date = Convert.ToDateTime(objODBCDataReader["dateof_releaseorder"].ToString());
                    }

                    values.statuspre_disbursementdeferal = objODBCDataReader["statuspre_disbursementdeferal"].ToString();
                    values.postdisbursement_covanent = objODBCDataReader["postdisbursement_covanent"].ToString();
                    values.statuspost_disbursementcovanent = objODBCDataReader["statuspost_disbursementcovanent"].ToString();
                    values.roissuing_totalamount = objODBCDataReader["roissuing_totalamount"].ToString();
                    values.casesvetted_bycad = objODBCDataReader["casesvetted_bycad"].ToString();
                    values.originaldocs_receivedHO = objODBCDataReader["originaldocs_receivedHO"].ToString();
                    values.scanneduploaded_Drive = objODBCDataReader["scanneduploaded_Drive"].ToString();
                    values.monitoring_visit = objODBCDataReader["monitoring_visit"].ToString();
                    values.bank_statement = objODBCDataReader["bank_statement"].ToString();
                    values.audited_financials = objODBCDataReader["audited_financials"].ToString();
                    values.stock_statement = objODBCDataReader["stock_statement"].ToString();
                    values.purchase_statement = objODBCDataReader["purchase_statement"].ToString();
                    values.sales_statement = objODBCDataReader["sales_statement"].ToString();
                    values.debtors_statement = objODBCDataReader["debtors_statement"].ToString();
                    values.provisionalfinancial_gst = objODBCDataReader["provisionalfinancial_gst"].ToString();
                    values.roc_30daysfromSLonetime = objODBCDataReader["roc_30daysfromSLonetime"].ToString();
                    values.noliability_certificate = objODBCDataReader["noliability_certificate"].ToString();
                    values.buyerconfirmation_letter = objODBCDataReader["buyerconfirmation_letter"].ToString();
                    values.copyof_warehousereceipt = objODBCDataReader["copyof_warehousereceipt"].ToString();
                    values.insurance_30daysfromSLonetime = objODBCDataReader["insurance_30daysfromSLonetime"].ToString();
                    values.loandisbursement_dtlfarmermember = objODBCDataReader["loandisbursement_dtlfarmermember"].ToString();
                    values.others = objODBCDataReader["others"].ToString();
                    values.zonal_name = objODBCDataReader["zonal_name"].ToString();
                    values.zonalGid = objODBCDataReader["zonal_head"].ToString();
                    values.businessHeadGid = objODBCDataReader["business_head"].ToString();
                    values.businesshead_name = objODBCDataReader["businesshead_name"].ToString();
                    values.relationshipMgmtGid = objODBCDataReader["relationship_manager"].ToString();
                    values.relationshipmgmt_name = objODBCDataReader["relationshipmgmt_name"].ToString();
                    values.clustermanagerGid = objODBCDataReader["cluster_manager_gid"].ToString();
                    values.cluster_manager_name = objODBCDataReader["cluster_manager_name"].ToString();
                    values.creditmanagerGid = objODBCDataReader["creditmanager_gid"].ToString();
                    values.creditmanager_name = objODBCDataReader["creditmgmt_name"].ToString();
                    values.contactperson = objODBCDataReader["contactperson"].ToString();
                    values.mobileno = objODBCDataReader["mobileno"].ToString();
                    values.addressline1 = objODBCDataReader["address"].ToString();
                    values.addressline2 = objODBCDataReader["address2"].ToString();
                    values.purpose_lending = objODBCDataReader["purpose_lending"].ToString();
                    values.facility_secure_type = objODBCDataReader["facility_secure_type"].ToString();
                    values.product_solution = objODBCDataReader["product_solution"].ToString();
                    values.major_intervention = objODBCDataReader["major_intervention"].ToString();
                    values.primary_value_chain = objODBCDataReader["primary_value_chain"].ToString();
                    values.secondary_value_chain = objODBCDataReader["secondary_value_chain"].ToString();
                    values.sanction_branch_gid = objODBCDataReader["sanction_branch_gid"].ToString();
                    values.sanction_branch_name = objODBCDataReader["sanction_branch_name"].ToString();
                    values.sanction_state_gid = objODBCDataReader["sanction_state_gid"].ToString();
                    values.sanction_state_name = objODBCDataReader["sanction_state_name"].ToString();
                    values.ccapproved_by = objODBCDataReader["ccapproved_by"].ToString();
                    values.ccdecision = objODBCDataReader["ccdecision"].ToString();
                    values.ccfeedback = objODBCDataReader["ccfeedback"].ToString();
                    values.general_remarks = objODBCDataReader["general_remarks"].ToString();
                    values.external_rating = objODBCDataReader["external_rating"].ToString();
                    values.business_description = objODBCDataReader["business_description"].ToString();
                    values.associate_name = objODBCDataReader["associate_name"].ToString();
                    values.sa_payout = objODBCDataReader["sa_payout"].ToString();
                    values.typeof_enterprise = objODBCDataReader["typeof_enterprise"].ToString();
                    values.risk_categorization = objODBCDataReader["risk_categorization"].ToString();
                    //values.applicability_category = objODBCDataReader["applicability_category"].ToString();
                    values.internal_rating = objODBCDataReader["internal_rating"].ToString();
                    values.status_ofPSL = objODBCDataReader["status_ofPSL"].ToString();
                    values.msme_classification = objODBCDataReader["msme_classification"].ToString();
                    values.industry = objODBCDataReader["industry"].ToString();
                    values.invesment_pme = objODBCDataReader["invesment_pme"].ToString();
                    values.turn_over = objODBCDataReader["turn_over"].ToString();
                    values.regno_msme = objODBCDataReader["regno_msme"].ToString();
                    values.validity_months = objODBCDataReader["validity_months"].ToString();
                    values.penal_interest = objODBCDataReader["penal_interest"].ToString();
                    if (objODBCDataReader["ccapproved_date"].ToString() != "")
                    {
                        values.ccapprovedDate = Convert.ToDateTime(objODBCDataReader["ccapproved_date"].ToString());
                    }
                    string amount1 = objODBCDataReader["sanction_amount"].ToString();
                    decimal parsed1 = decimal.Parse(amount1, System.Globalization.CultureInfo.InvariantCulture);
                    System.Globalization.CultureInfo indianformat1 = new System.Globalization.CultureInfo("hi-IN");
                    string text1 = string.Format(indianformat1, "{0:c}", parsed1);

                    msSQL = "select substring('" + text1 + "',2,20)";
                    string sanction_amount = objdbconn.GetExecuteScalar(msSQL);
                    values.sanction_amount = sanction_amount;

                    values.sanction_type = objODBCDataReader["sanction_type"].ToString();
                    values.natureof_proposal = objODBCDataReader["natureof_proposal"].ToString();
                    values.colander_name = objODBCDataReader["colander_name"].ToString();
                    values.colanding_status = objODBCDataReader["colanding_status"].ToString();
                    values.entity_gid = objODBCDataReader["entity_gid"].ToString();
                    values.paycard = objODBCDataReader["paycard"].ToString();
                    values.ccapprovedby_gid = objODBCDataReader["ccapprovedby_gid"].ToString();
                    values.esrisk_categorization = objODBCDataReader["esrisk_categorization"].ToString();
                    values.es_application = objODBCDataReader["es_application"].ToString();
                    values.esdeclaration_status = objODBCDataReader["esdeclaration_status"].ToString();
                    values.checkerletter_flag = objODBCDataReader["checkerletter_flag"].ToString();
                    values.checkerapproval_flag = objODBCDataReader["checkerapproval_flag"].ToString();
                }
                objODBCDataReader.Close();


                msSQL = " select generaldocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type, " +
                     " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                     " from ids_trn_tuploadgeneraldocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                     " and b.user_gid = c.user_gid and customer2sanction_gid='" + sanction_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getfilename = new List<UploadgeneralDocumentList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getfilename.Add(new UploadgeneralDocumentList
                        {
                            document_name = (dr_datarow["document_name"].ToString()),
                            document_gid = (dr_datarow["generaldocument_gid"].ToString()),
                            document_type = dr_datarow["document_type"].ToString(),
                            uploaded_by = dr_datarow["uploaded_by"].ToString(),
                            updated_date = dr_datarow["uploaded_date"].ToString(),
                            document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                        }) ;
                    }
                    values.UploadgeneralDocumentList = getfilename;
                }
                dt_datatable.Dispose();

                msSQL = " select momdocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_type,document_path, " +
                 " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                 " from ids_trn_tuploadmomdocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                 " and b.user_gid = c.user_gid and customer2sanction_gid='" + sanction_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getMOM_filename = new List<UploadMOMDocumentList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getMOM_filename.Add(new UploadMOMDocumentList
                        {
                            document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                            document_name = (dr_datarow["document_name"].ToString()),
                            document_type = dr_datarow["document_type"].ToString(),
                            document_gid = (dr_datarow["momdocument_gid"].ToString()),
                            uploaded_by = dr_datarow["uploaded_by"].ToString(),
                            updated_date = dr_datarow["uploaded_date"].ToString()
                        });
                    }
                    values.UploadMOMDocumentList = getMOM_filename;
                }
                dt_datatable.Dispose();

                msSQL = " select camdocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_type,document_path, " +
                  " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                  " from ids_trn_tuploadcamdocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                  " and b.user_gid = c.user_gid and customer2sanction_gid='" + sanction_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getCAM_filename = new List<UploadCOMDocumentList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getCAM_filename.Add(new UploadCOMDocumentList
                        {
                            document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                            document_name = (dr_datarow["document_name"].ToString()),
                            document_type = dr_datarow["document_type"].ToString(),
                            document_gid = (dr_datarow["camdocument_gid"].ToString()),
                            uploaded_by = dr_datarow["uploaded_by"].ToString(),
                            updated_date = dr_datarow["uploaded_date"].ToString()
                        });
                    }
                    values.UploadCOMDocumentList = getCAM_filename;
                }
                dt_datatable.Dispose();

                msSQL = " select sanctionletter_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type, " +
                     " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                     " from ids_trn_tuploadsanctionletter a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                     " and b.user_gid = c.user_gid and customer2sanction_gid='" + sanction_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_sanfilename = new List<UploadSANDocumentList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_sanfilename.Add(new UploadSANDocumentList
                        {
                            document_name = (dr_datarow["document_name"].ToString()),
                            document_gid = (dr_datarow["sanctionletter_gid"].ToString()),
                            document_type = dr_datarow["document_type"].ToString(),
                            uploaded_by = dr_datarow["uploaded_by"].ToString(),
                            updated_date = dr_datarow["uploaded_date"].ToString(),
                            document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString())
                        });
                    }
                    values.UploadSANDocumentList = get_sanfilename;
                }
                dt_datatable.Dispose();

                msSQL = " select esdeclaration_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                    " from ids_trn_tuploadesdeclarationdocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                    " and b.user_gid = c.user_gid and customer2sanction_gid='" + sanction_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_esdeclarationfilename = new List<UploadES_DocumentList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_esdeclarationfilename.Add(new UploadES_DocumentList
                        {
                            document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                            document_name = (dr_datarow["document_name"].ToString()),
                            document_gid = (dr_datarow["esdeclaration_gid"].ToString()),
                            document_type = dr_datarow["document_type"].ToString(),
                            uploaded_by = dr_datarow["uploaded_by"].ToString(),
                            updated_date = dr_datarow["uploaded_date"].ToString()
                        });
                    }
                    values.UploadES_DocumentList = get_esdeclarationfilename;
                }
                dt_datatable.Dispose();

                msSQL = " select maildocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                    " from ids_trn_tdeviationmaildocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                    " and b.user_gid = c.user_gid and customer2sanction_gid='" + sanction_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_mailfilename = new List<DeviationMail_DocumentList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_mailfilename.Add(new DeviationMail_DocumentList
                        {
                            document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                            document_name = (dr_datarow["document_name"].ToString()),
                            document_gid = (dr_datarow["maildocument_gid"].ToString()),
                            document_type = dr_datarow["document_type"].ToString(),
                            uploaded_by = dr_datarow["uploaded_by"].ToString(),
                            updated_date = dr_datarow["uploaded_date"].ToString()
                        });
                    }
                    values.DeviationMail_DocumentList = get_mailfilename;
                }
                dt_datatable.Dispose();

                msSQL = " select  addbuyer_gid,if (document_name is null,'---',document_name) as document_name,baldocument_gid," +
                    " concat(date_format(a.created_date, '%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,buyer_name," +
                    " buyer_exposure,concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as uploaded_by from ids_mst_taddbuyer a" +
                    " left join ids_trn_tuploadbaldocument d on a.addbuyer_gid = d.buyer_gid" +
                     " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                     " left  join adm_mst_tuser c on b.user_gid = c.user_gid where" +
                     " a.customer2sanction_gid ='" + sanction_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<buyer_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new buyer_list
                        {
                            document_name = (dr_datarow["document_name"].ToString()),
                            document_path =objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                            buyer_gid = (dr_datarow["addbuyer_gid"].ToString()),
                            buyer_name = dr_datarow["buyer_name"].ToString(),
                            uploaded_by = dr_datarow["uploaded_by"].ToString(),
                            uploaded_date = dr_datarow["uploaded_date"].ToString(),
                            buyer_exposure = dr_datarow["buyer_exposure"].ToString(),
                            baldocument_gid = dr_datarow["baldocument_gid"].ToString()
                        });
                    }
                    values.buyer_list = get_filename;
                }
                dt_datatable.Dispose();



                msSQL = "select ccmemberlist_gid,ccmember_name,ccmember_gid,ccmember_remarks,ccgroup_name from ocs_mst_tsanction2ccmemberlist " +
                " where customer2sanction_gid='" + sanction_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_mdlccmember = new List<mdlccmember>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_mdlccmember.Add(new mdlccmember
                        {
                            ccmemberlist_gid = (dr_datarow["ccmemberlist_gid"].ToString()),
                            ccmember_name = (dr_datarow["ccmember_name"].ToString()),
                            ccmember_gid = (dr_datarow["ccmember_gid"].ToString()),
                            ccmember_remarks = (dr_datarow["ccmember_remarks"].ToString()),
                            ccgroup_name = (dr_datarow["ccgroup_name"].ToString()),
                        });
                    }
                    values.mdlccmember = get_mdlccmember;
                }
                dt_datatable.Dispose();
                msSQL = "select sanction2loanfacilitytype_gid,loanfacility_gid,format(loanfacility_amount,2) as loanfacility_amount,loanfacility_type, " +
                " format(document_limit,2) as document_limit,margin,date_format(expiry_date, '%d-%m-%Y') as expiry_date,revolving_type,tenure," +
                " interchangeability,if(report_structure='','---',report_structure) as report_structure,loanfacilityref_no,proposed_roi" +
                " from ocs_mst_tsanction2loanfacilitytype  where customer2sanction_gid='" + sanction_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getloanfacilitytype = new List<loanfacilitytype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getloanfacilitytype.Add(new loanfacilitytype_list
                        {
                            sanction2loanfacilitytype_gid = (dr_datarow["sanction2loanfacilitytype_gid"].ToString()),
                            loanfacility_gid = (dr_datarow["loanfacility_gid"].ToString()),
                            loanfacility_amount = (dr_datarow["loanfacility_amount"].ToString()),
                            loanfacility_type = (dr_datarow["loanfacility_type"].ToString()),
                            document_limit = (dr_datarow["document_limit"].ToString()),
                            expiry_date = (dr_datarow["expiry_date"].ToString()),
                            revolving_type = (dr_datarow["revolving_type"].ToString()),
                            tenure = (dr_datarow["tenure"].ToString()),
                            margin = (dr_datarow["margin"].ToString()),
                            interchangeability = (dr_datarow["interchangeability"].ToString()),
                            report_structure = (dr_datarow["report_structure"].ToString()),
                            loanfacilityref_no = (dr_datarow["loanfacilityref_no"].ToString()),
                            proposed_roi = (dr_datarow["proposed_roi"].ToString()),
                        });
                    }
                    values.loanfacilitytype_list = getloanfacilitytype;
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }

        }

        public void DaPostExcelUpload(HttpRequest httpRequest, string employee_gid, result values)
        {

            HttpFileCollection httpFileCollection;

            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms = new MemoryStream();
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string lsdocumenttype_gid = string.Empty;
            string document_name = httpRequest.Form["document_name"];
            string lsuploadtype = httpRequest.Form["uploadtype"];
            String path = lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = "SELECT * from adm_mst_tcompany where 1=1";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                lscompany_code = objODBCDataReader["company_code"].ToString();
            }
            objODBCDataReader.Close();
            path = ConfigurationManager.AppSettings["file_path"] + "../../../erpdocument" + "/" + lscompany_code + "/" + "Master/SanctionExcelImport/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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
                        string lsfile_gid = msdocument_gid + FileExtension;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        if ((FileExtension == ".csv"))
                        {
                            ls_readStream = httpPostedFile.InputStream;
                            ls_readStream.CopyTo(ms);

                            byte[] bytes = ms.ToArray();
                            if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                            {
                                values.message = "File format is not supported";
                                values.status = false;
                                return;
                            }

                            //CopyStream(ms, ls_readStream);
                            //lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "Master/SanctionExcelImport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + lsfile_gid;
                            //FileStream file = new FileStream(lspath, FileMode.Create, FileAccess.Write);
                            //ms.WriteTo(file);
                            //file.Close();
                            //ms.Close();
                            FileStream file = new FileStream(lspath, FileMode.Create, FileAccess.Write);
                            bool status;
                            status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/SanctionExcelImport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);                         
                            file.Close();
                            ms.Close();

                            msGetGid = objcmnfunctions.GetMasterGID("SAIM");
                            msSQL = " insert into ocs_trn_tsancimportfiledtl( " +
                                    " sancimport_gid," +
                                    " file_path," +
                                    " file_name," +
                                    " file_type," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + lspath + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lsuploadtype + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            string lsreppath = lspath.Replace('\\', '/');

                            msSQL = " LOAD DATA INFILE '" + lsreppath + "'" +
                                    " INTO TABLE ocs_tmp_tsanctionimport" +
                                    " FIELDS TERMINATED BY ','" +
                                    " ENCLOSED BY '\"' " +
                                    " LINES TERMINATED BY '\r\n'" +
                                    " IGNORE 1 LINES" +
                                    " (@CustomerURN, @SanctionRefNo, @ApprovalAuthority, @Vertical, @LoanType, @CCApprovalDate, @SanctionDate, @NatureofProposal, @ClassificationofMSME," +
                                    " @ValidityinMonths, @ExpiryDate, @ReviewDate, @CustomerName, @EalierSanctionReference, @Constitution, @Address, @State, @Pincode, @ContactNumber," +
                                    " @EmailID, @ContactPerson, @RMName, @RMPhoneNo, @RMEmailID, @SanctionLetterAuthorizedSignatory, @CreditManager, @TypeofFacility," +
                                    " @ExistingLimit, @AdditionalProposedLimit, @OverallLimit, @Purpose, @TenureMonths, @RepaymentPrincipal," +
                                    " @RepaymentInterest, @PrimarySecurity, @CollateralSecurity, @PersonalGuarantee, @SecurityChequeBankName," +
                                    " @SecurityChequeAccountNumber, @Margin, @RateofInterest, @PenalInterest, @ProcessingFee, @BankandChequeNo," +
                                    " @ChequeRealizationDate, @DocumentationChargeClientVisitCharge," +
                                    " @GSTNumber,@DocumentationList, @ModeofOperations, @SpecificConditions, @DateofReceiptofDocsforVetting, @NACHForm," +
                                    " @EscrowAccount, @VirtualAccountNo, @Nameofthebuyers, @StatusofBAL, @ROCApplicable, @ROCStatus, @CERSAIStatus, @NeSLStatus," +
                                    " @PredisbursementDeferal, @DateofDeviation, @StatusPredisbursementDeferal, @Postdisbursementcovanent, @StatusPostdisbursementcovanent," +
                                    " @DateofReleaseOrder, @ROissuingTotalamount, @CasesvettedbytheCAD, @OriginalDocsreceivedatHO, @ScannedanduploadedinDrive," +
                                    " @Monitoringvisit, @BankStatement, @AuditedFinancials, @StockStatement, @PurchaseStatement, @SalesStatement, @DebtorsStatement," +
                                    " @ProvisionalFinancialwithGST, @ROC30daysfromSLonetime, @NoLiabilityCertificatefromFIBankOnetime, @BuyerConfirmationLetter," +
                                    " @CopyofWarehouseReceipt30daysfromSLonetime, @Insurance30daysfromSLonetime, @LoandisbursementdetailstoFarmermember30daysfromSLonetime," +
                                    " @Others)" +
                                    " set" +
                                    " customer_urn = @CustomerURN, sanction_refno = @SanctionRefNo, approval_authority = @ApprovalAuthority, vertical = @Vertical, loan_type = @LoanType," +
                                    " CCapproval_date = @CCApprovalDate, sanction_date = @SanctionDate, nature_ofproposal = @NatureofProposal, classification_MSME = @ClassificationofMSME," +
                                    " sanction_validity = @ValidityinMonths, sanction_expirydate = @ExpiryDate, sanction_reviewdate = @ReviewDate, customer_name = @CustomerName, earlier_sanctionrefno = @EalierSanctionReference," +
                                    " constitution = @Constitution, address = @Address, state = @State, pincode = @Pincode, contact_number = @ContactNumber," +
                                    " email_id = @EmailID, contact_person = @ContactPerson, rm_name = @RMName, rm_phoneno = @RMPhoneNo, rm_emailid = @RMEmailID," +
                                    " authorized_signatory = @SanctionLetterAuthorizedSignatory, credit_manager = @CreditManager, facility_type = @TypeofFacility," +
                                    " existing_limit = @ExistingLimit, additional_proposedlimit = @AdditionalProposedLimit, overall_limit = @OverallLimit," +
                                    " purpose = @Purpose, tenure_months = @TenureMonths, repayment_principal = @RepaymentPrincipal," +
                                    " repayment_interest = @RepaymentInterest, primary_security = @PrimarySecurity, collateral_security = @CollateralSecurity," +
                                    " personal_guarantee = @PersonalGuarantee, securitycheque_bankname = @SecurityChequeBankName," +
                                    " securitycheque_accountnumber = @SecurityChequeAccountNumber, margin = @Margin, rateof_interest = @RateofInterest," +
                                    " penal_interest = @PenalInterest, processing_fee = @ProcessingFee, bankand_chequeno = @BankandChequeNo," +
                                    " cheque_realizationdate = @ChequeRealizationDate, documentation_clientvisitcharge = @DocumentationChargeClientVisitCharge," +
                                    " GST_number = @GSTNumber,documentation_list=@DocumentationList ,modeof_operations = @ModeofOperations, specific_condition = @SpecificConditions," +
                                    " dateof_receiptDocsVetting = @DateofReceiptofDocsforVetting, NACH_form = @NACHForm," +
                                    " escrow_account = @EscrowAccount, virtual_accountno = @VirtualAccountNo, nameofthe_buyers = @Nameofthebuyers, status_ofBAL = @StatusofBAL," +
                                    " roc_applicable = @ROCApplicable, roc_status = @ROCStatus, cersai_status = @CERSAIStatus," +
                                    " nesl_status = @NeSLStatus, predisbursement_deferal = @PredisbursementDeferal, dateof_deviation = @DateofDeviation," +
                                    " statuspre_disbursementdeferal = @StatusPredisbursementDeferal, postdisbursement_covanent = @Postdisbursementcovanent," +
                                    " statuspost_disbursementcovanent = @StatusPostdisbursementcovanent," +
                                    " dateof_releaseorder = @DateofReleaseOrder, roissuing_totalamount = @ROissuingTotalamount, casesvetted_bycad = @CasesvettedbytheCAD," +
                                    " originaldocs_receivedHO = @OriginalDocsreceivedatHO," +
                                    " scanneduploaded_Drive = @ScannedanduploadedinDrive, monitoring_visit = @Monitoringvisit, bank_statement = @BankStatement," +
                                    " audited_financials = @AuditedFinancials, stock_statement = @StockStatement, purchase_statement = @PurchaseStatement," +
                                    " sales_statement = @SalesStatement, debtors_statement = @DebtorsStatement," +
                                    " provisionalfinancial_gst = @ProvisionalFinancialwithGST," +
                                    " roc_30daysfromSLonetime = @ROC30daysfromSLonetime, noliability_certificate = @NoLiabilityCertificatefromFIBankOnetime," +
                                    " buyerconfirmation_letter = @BuyerConfirmationLetter, copyof_warehousereceipt = @CopyofWarehouseReceipt30daysfromSLonetime," +
                                    " insurance_30daysfromSLonetime = @Insurance30daysfromSLonetime, loandisbursement_dtlfarmermember = @LoandisbursementdetailstoFarmermember30daysfromSLonetime," +
                                    " others = @Others";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            if (mnResult == 1)
                            {
                                msSQL = " UPDATE ocs_mst_tcustomer2sanction a " +
                                        " INNER JOIN  ocs_tmp_tsanctionimport b ON a.sanction_refno = b.sanction_refno " +
                                        " LEFT JOIN adm_mst_tuser c on b.authorized_signatory=c.user_code " +
                                        " LEFT JOIN hrm_mst_temployee d on c.user_gid=d.user_gid " +
                                        " LEFT JOIN ocs_mst_tvertical e on a.vertical=e.vertical " +
                                        " SET a.approval_authority = b.approval_authority, a.vertical = b.vertical,a.vertical_gid=e.vertical_gid,a.CCapproval_date = STR_TO_DATE(b.CCapproval_date, '%d/%m/%Y')," +
                                        " a.nature_ofproposal = b.nature_ofproposal,a.classification_MSME = b.classification_MSME,a.loan_type = b.loan_type,a.sanction_validity = b.sanction_validity," +
                                        " a.sanction_expirydate = STR_TO_DATE(b.sanction_expirydate, '%d/%m/%Y'),a.sanction_reviewdate = STR_TO_DATE(b.sanction_reviewdate, '%d/%m/%Y')," +
                                        " a.earlier_sanctionrefno = b.earlier_sanctionrefno,a.constitution = b.constitution,a.address = b.address,a.state = b.state,a.pincode = b.pincode," +
                                        " a.contact_number = b.contact_number,a.email_id = b.email_id,a.contact_person = b.contact_person,a.rm_code = b.rm_name,a.rm_phoneno = b.rm_phoneno," +
                                        " a.rm_emailid = b.rm_emailid,a.authorized_signatory = b.authorized_signatory,a.authorized_signatoryGid=d.employee_gid,a.credit_manager = b.credit_manager,a.facility_type = b.facility_type," +
                                        " a.existing_limit = b.existing_limit,a.additional_proposedlimit = b.additional_proposedlimit,a.overall_limit = b.overall_limit,a.purpose = b.purpose," +
                                        " a.tenure_months = b.tenure_months,a.repayment_principal = b.repayment_principal,a.repayment_interest = b.repayment_interest,a.primary_security = b.primary_security," +
                                        " a.personal_guarantee = b.personal_guarantee,a.securitycheque_bankname = b.securitycheque_bankname,a.securitycheque_accountnumber = b.securitycheque_accountnumber," +
                                        " a.margin = b.margin,a.rateof_interest = b.rateof_interest,a.penal_interest = b.penal_interest,a.processing_fee = b.processing_fee,a.bankand_chequeno = b.bankand_chequeno," +
                                        " a.cheque_realizationdate = b.cheque_realizationdate,a.documentation_clientvisitcharge = b.documentation_clientvisitcharge,a.GST_number = b.GST_number," +
                                        " a.modeof_operations = b.modeof_operations,a.specific_condition = b.specific_condition,a.dateof_receiptDocsVetting = STR_TO_DATE(b.dateof_receiptDocsVetting, '%d/%m/%Y')," +
                                        " a.NACH_form = b.NACH_form,a.escrow_account = b.escrow_account,a.virtual_accountno = b.virtual_accountno,a.nameofthe_buyers = b.nameofthe_buyers," +
                                        " a.status_ofBAL = b.status_ofBAL,a.roc_applicable = b.roc_applicable,a.roc_status = b.roc_status,a.cersai_status = b.cersai_status,a.nesl_status = b.nesl_status," +
                                        " a.predisbursement_deferal = b.predisbursement_deferal,a.dateof_deviation = STR_TO_DATE(b.dateof_deviation, '%d/%m/%Y'),a.statuspre_disbursementdeferal = b.statuspre_disbursementdeferal," +
                                        " a.postdisbursement_covanent = b.postdisbursement_covanent,a.statuspost_disbursementcovanent = b.statuspost_disbursementcovanent," +
                                        " a.dateof_releaseorder = STR_TO_DATE(b.dateof_releaseorder, '%d/%m/%Y'),a.roissuing_totalamount = b.roissuing_totalamount,a.casesvetted_bycad = b.casesvetted_bycad," +
                                        " a.originaldocs_receivedHO = b.originaldocs_receivedHO,a.scanneduploaded_Drive = b.scanneduploaded_Drive,a.monitoring_visit = b.monitoring_visit," +
                                        " a.bank_statement = b.bank_statement,a.audited_financials = b.audited_financials,a.stock_statement = b.stock_statement,a.purchase_statement = b.purchase_statement," +
                                        " a.sales_statement = b.sales_statement,a.debtors_statement = b.debtors_statement,a.provisionalfinancial_gst = b.provisionalfinancial_gst," +
                                        " a.roc_30daysfromSLonetime = b.roc_30daysfromSLonetime,a.noliability_certificate = b.noliability_certificate,a.buyerconfirmation_letter = b.buyerconfirmation_letter," +
                                        " a.copyof_warehousereceipt = b.copyof_warehousereceipt,a.insurance_30daysfromSLonetime = b.insurance_30daysfromSLonetime," +
                                        " a.loandisbursement_dtlfarmermember = b.loandisbursement_dtlfarmermember,a.others = b.others,a.updated_by = '" + employee_gid + "',a.updated_date = current_timestamp";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                string msGetSanctionGid = objcmnfunctions.GetMasterGID("CU2S");
                                msSQL = " insert into ocs_mst_tcustomer2sanction(customer2sanction_gid,file_type, customer_urn,customer_gid, sanction_refno, approval_authority, vertical, " +
                                        " vertical_gid,CCapproval_date,sanction_date,nature_ofproposal,classification_MSME," +
                                        " loan_type, sanction_validity, sanction_expirydate, sanction_reviewdate, customer_name, earlier_sanctionrefno, constitution, " +
                                        " zonalhead_gid, zonalhead_name, businesshead_gid, businesshead_name,clustermgr_gid,clustermgr_name,creditmgr_gid,creditmgr_name," +
                                        " relationshipmgr_gid,relationshipmgr_name,rm_phoneno, rm_emailid, " +
                                        " address, state, pincode, contact_number, email_id, contact_person," +
                                        " authorized_signatory,authorized_signatoryGid, facility_type, existing_limit, additional_proposedlimit, overall_limit, purpose, " +
                                        " tenure_months, repayment_principal, repayment_interest, primary_security, collateral_security, personal_guarantee, " +
                                        " securitycheque_bankname, securitycheque_accountnumber, margin, rateof_interest, penal_interest, " +
                                        " processing_fee, bankand_chequeno, cheque_realizationdate, documentation_clientvisitcharge, GST_number,documentation_list, " +
                                        " modeof_operations, specific_condition, dateof_receiptDocsVetting, NACH_form, escrow_account, " +
                                        " virtual_accountno, nameofthe_buyers, status_ofBAL, roc_applicable, roc_status, cersai_status, " +
                                        " nesl_status, predisbursement_deferal, dateof_deviation, statuspre_disbursementdeferal, postdisbursement_covanent, " +
                                        " statuspost_disbursementcovanent, dateof_releaseorder, roissuing_totalamount, casesvetted_bycad, originaldocs_receivedHO, " +
                                        " scanneduploaded_Drive, monitoring_visit, bank_statement, audited_financials, stock_statement, purchase_statement, " +
                                        " sales_statement, debtors_statement, provisionalfinancial_gst, roc_30daysfromSLonetime, noliability_certificate, " +
                                        " buyerconfirmation_letter, copyof_warehousereceipt, insurance_30daysfromSLonetime, loandisbursement_dtlfarmermember, " +
                                        " others, created_by, created_date) " +
                                        " (select concat('" + msGetSanctionGid + "',tmpsanction_gid),'" + lsuploadtype + "' ,a.customer_urn,e.customer_gid, sanction_refno, approval_authority, vertical, " +
                                        " e.vertical_gid,STR_TO_DATE(CCapproval_date, '%d/%m/%Y') ,STR_TO_DATE(sanction_date, '%d/%m/%Y'),nature_ofproposal,classification_MSME, " +
                                        " loan_type, sanction_validity,STR_TO_DATE(sanction_expirydate, '%d/%m/%Y') , STR_TO_DATE(sanction_reviewdate, '%d/%m/%Y') , e.customername, earlier_sanctionrefno, constitution, " +
                                        " e.zonal_head,e.zonal_name,e.business_head,e.businesshead_name,e.cluster_manager_gid,e.cluster_manager_name,e.creditmanager_gid,e.creditmgmt_name," +
                                        " e.relationship_manager,e.relationshipmgmt_name,d.employee_mobileno,d.employee_emailid" +
                                        " e.address, e.state, e.postalcode, e.contact_no, e.email, e.contactperson, " +
                                        " authorized_signatory,c.employee_gid as authorizedsignatoryGid,facility_type, existing_limit, additional_proposedlimit, overall_limit, purpose, " +
                                        " tenure_months, repayment_principal, repayment_interest, primary_security, collateral_security, personal_guarantee, " +
                                        " securitycheque_bankname, securitycheque_accountnumber, margin, rateof_interest, penal_interest, " +
                                        " processing_fee, bankand_chequeno, cheque_realizationdate, documentation_clientvisitcharge, a.GST_number,documentation_list, " +
                                        " modeof_operations, specific_condition, dateof_receiptDocsVetting, NACH_form, escrow_account, " +
                                        " virtual_accountno, nameofthe_buyers, status_ofBAL, roc_applicable, roc_status, cersai_status," +
                                        " nesl_status, predisbursement_deferal, STR_TO_DATE(dateof_deviation, '%d/%m/%Y'), statuspre_disbursementdeferal, postdisbursement_covanent, " +
                                        " statuspost_disbursementcovanent,STR_TO_DATE(dateof_releaseorder, '%d/%m/%Y') , roissuing_totalamount, casesvetted_bycad, originaldocs_receivedHO, " +
                                        " scanneduploaded_Drive, monitoring_visit, bank_statement, audited_financials, stock_statement, purchase_statement, " +
                                        " sales_statement, debtors_statement, provisionalfinancial_gst, roc_30daysfromSLonetime, noliability_certificate, " +
                                        " buyerconfirmation_letter, copyof_warehousereceipt, insurance_30daysfromSLonetime, loandisbursement_dtlfarmermember, " +
                                        " others, '" + employee_gid + "', current_timestamp from ocs_tmp_tsanctionimport a" +
                                        " left join adm_mst_tuser b on a.authorized_signatory=b.user_code " +
                                        " left join hrm_mst_temployee c on c.user_gid=b.user_gid " +
                                        " left join ocs_mst_tcustomer e on a.customer_urn=e.customer_urn " +
                                        " left join hrm_mst_temployee d on d.employee_gid=e.relationshipmgr_gid" +
                                        " LEFT JOIN ocs_mst_tvertical f on a.vertical=f.vertical" +
                                        " where a.customer_urn<>'' and a.customer_urn in (select customer_urn from ocs_mst_tcustomer) " +
                                        " and sanction_refno not in (select sanction_refno from ocs_mst_tcustomer2sanction))";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                                if (mnResult == 1)
                                {
                                    msSQL = "delete from ocs_tmp_tsanctionimport";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    values.message = "Sanction Details are Inserted Successfully..!";
                                    values.status = true;
                                }
                                else
                                {
                                    values.message = "Check the Details..!";
                                    values.status = false;
                                }
                            }
                            else
                            {
                                values.message = "Check the Excel Upload Format..!";
                                values.status = false;
                            }

                        }
                        else
                        {
                            values.message = "File Format is not Supported..!";
                            values.status = false;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.ToString();
            }


        }

        public bool DaPostCAMddocument(HttpRequest httpRequest, UploadDocumentname objfilename, string employee_gid)
        {
            UploadDocumentList objdocumentmodel = new UploadDocumentList();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms = new MemoryStream();
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string lsdocumenttype_gid = string.Empty;
            String path = lspath;


            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";

            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "../../../erpdocument" + "/" + lscompany_code + "/" + "IDAS/CAMdocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
            string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
            string lsfirstdocument_filepath = string.Empty;

            httpFileCollection = httpRequest.Files;

            httpPostedFile = httpFileCollection[0];
            string FileExtension = httpPostedFile.FileName;
            //string lsfile_gid = msdocument_gid + FileExtension;
            string lsfile_gid = msdocument_gid;
            FileExtension = Path.GetExtension(FileExtension).ToLower();
            lsfile_gid = lsfile_gid + FileExtension;
            if ((FileExtension == ".xls") || (FileExtension == ".xlsx") || (FileExtension == ".doc") || (FileExtension == ".docx") || (FileExtension == ".pdf") || (FileExtension == ".zip") || (FileExtension == ".msg") || (FileExtension == ".oft") || (FileExtension == ".txt") || (FileExtension == ".txtx"))
            {
                ls_readStream = httpPostedFile.InputStream;
                ls_readStream.CopyTo(ms);
                //lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "IDAS/CAMdocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                //objcmnfunctions.uploadFile(lspath, lsfile_gid);

                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "IDAS/CAMdocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                ms.Close();
                lspath = "erpdocument" + "/" + lscompany_code + "/" + "IDAS/CAMdocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";




                msGetGid = objcmnfunctions.GetMasterGID("CAMD");
                msSQL = " insert into ids_trn_tuploadcamdocument( " +
                             " camdocument_gid," +
                             " document_name, " +
                             " document_path, " +
                             " customer2sanction_gid," +
                             " created_by ," +
                             " created_date " +
                             " )values(" +
                             "'" + msGetGid + "'," +
                             "'" + httpPostedFile.FileName + "'," +
                             "'" + lspath + msdocument_gid + FileExtension + "'," +
                             "'" + employee_gid + "'," +
                             "'" + employee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msSQL = " select camdocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path, " +
                       " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                       " from ids_trn_tuploadcamdocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                       " and b.user_gid = c.user_gid and customer2sanction_gid='" + employee_gid + "'";

                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var get_filename = new List<UploadDocumentList>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            get_filename.Add(new UploadDocumentList
                            {
                                document_path = (dr_datarow["document_path"].ToString()),
                                document_name = (dr_datarow["document_name"].ToString()),
                                document_gid = (dr_datarow["camdocument_gid"].ToString()),
                                uploaded_by = dr_datarow["uploaded_by"].ToString(),
                                updated_date = dr_datarow["uploaded_date"].ToString()
                            });
                        }
                        objfilename.UploadDocumentList = get_filename;
                    }
                    dt_datatable.Dispose();

                    objfilename.status = true;
                    objfilename.message = "CAM Document uploaded successfully";
                    return true;
                }
                else
                {
                    objfilename.status = false;
                    objfilename.message = "Error Occured while uploading CAM document";
                    return false;
                }
            }
            else
            {
                objfilename.status = false;
                objfilename.message = "File format is not supported";
                return false;
            }
        }

        public bool DaPostMOMddocument(HttpRequest httpRequest, UploadDocumentname objfilename, string employee_gid)
        {
            UploadDocumentList objdocumentmodel = new UploadDocumentList();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms = new MemoryStream();
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string lsdocumenttype_gid = string.Empty;
            String path = lspath;


            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";

            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "IDAS/MOMdocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

            if ((!System.IO.Directory.Exists(path)))
                System.IO.Directory.CreateDirectory(path);

            string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
            string lsfirstdocument_filepath = string.Empty;

            httpFileCollection = httpRequest.Files;

            httpPostedFile = httpFileCollection[0];
            string FileExtension = httpPostedFile.FileName;
            //string lsfile_gid = msdocument_gid + FileExtension;
            string lsfile_gid = msdocument_gid;
            FileExtension = Path.GetExtension(FileExtension).ToLower();
            lsfile_gid = lsfile_gid + FileExtension;
            ls_readStream = httpPostedFile.InputStream;
            ls_readStream.CopyTo(ms);
            //lspath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "IDAS/MOMdocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
            //objcmnfunctions.uploadFile(lspath, lsfile_gid);

            //lspath = "../../../erp_documents" + "/" + lscompany_code + "/" + "IDAS/MOMdocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

            bool status;
            status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "IDAS/MOMdocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
            ms.Close();
            lspath = "erpdocument" + "/" + lscompany_code + "/" + "IDAS/MOMdocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";



            msGetGid = objcmnfunctions.GetMasterGID("MOMD");
            msSQL = " insert into ids_trn_tuploadmomdocument( " +
                         " momdocument_gid," +
                         " document_name, " +
                         " document_path, " +
                         " customer2sanction_gid," +
                         " created_by ," +
                         " created_date " +
                         " )values(" +
                         "'" + msGetGid + "'," +
                         "'" + httpPostedFile.FileName + "'," +
                         "'" + lspath + msdocument_gid + FileExtension + "'," +
                         "'" + employee_gid + "'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = " select momdocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path, " +
                 " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                 " from ids_trn_tuploadmomdocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                 " and b.user_gid = c.user_gid and customer2sanction_gid='" + employee_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<UploadMOMDocumentList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new UploadMOMDocumentList
                        {
                            document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                            document_name = (dr_datarow["document_name"].ToString()),
                            document_gid = (dr_datarow["momdocument_gid"].ToString()),
                            uploaded_by = dr_datarow["uploaded_by"].ToString(),
                            updated_date = dr_datarow["uploaded_date"].ToString()
                        });
                    }
                    objfilename.UploadMOMDocumentList = get_filename;
                }
                dt_datatable.Dispose();

                objfilename.status = true;
                objfilename.message = "MOM Document uploaded successfully";
                return true;
            }
            else
            {
                objfilename.status = false;
                objfilename.message = "Error Occured while uploading MOM document";
                return false;
            }



        }

        public bool DaPostgeneraldocument(HttpRequest httpRequest, UploadDocumentname objfilename, string employee_gid, string user_gid)
        {
            UploadDocumentList objdocumentmodel = new UploadDocumentList();
            HttpFileCollection httpFileCollection;

            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms = new MemoryStream();
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string lsdocumenttype_gid = string.Empty;

            String path = lspath;

            string document_type = httpRequest.Form["document_type"].ToString();


            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";

            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "IDAS/Generaldocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

            if ((!System.IO.Directory.Exists(path)))
                System.IO.Directory.CreateDirectory(path);

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
                        ls_readStream.CopyTo(ms);
                        //lspath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "IDAS/generaldocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
                        //objcmnfunctions.uploadFile(lspath, lsfile_gid);
                        //lspath = "../../../erp_documents" + "/" + lscompany_code + "/" + "IDAS/generaldocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "IDAS/Generaldocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();

                        if (document_type == "undefined")
                        {
                            lsdocument_type = "";
                        }
                        else
                        {
                            lsdocument_type = document_type;
                        }

                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "IDAS/Generaldocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("DOCS");
                        msSQL = " insert into ids_trn_tuploadgeneraldocument( " +
                                    " generaldocument_gid," +
                                    " customer2sanction_gid ," +
                                    " document_name," +
                                    " document_path," +
                                    " document_type ," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + lsdocument_type.Replace("'", "") + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                msSQL = " select generaldocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type, " +
                     " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                     " from ids_trn_tuploadgeneraldocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                     " and b.user_gid = c.user_gid and customer2sanction_gid='" + employee_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<UploadDocumentList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new UploadDocumentList
                        {
                            document_name = (dr_datarow["document_name"].ToString()),
                            document_path = ((dr_datarow["document_path"].ToString())),
                            document_gid = (dr_datarow["generaldocument_gid"].ToString()),
                            document_type = dr_datarow["document_type"].ToString(),
                            uploaded_by = dr_datarow["uploaded_by"].ToString(),
                            updated_date = dr_datarow["uploaded_date"].ToString()
                        });
                    }
                    objfilename.UploadDocumentList = get_filename;
                }
                dt_datatable.Dispose();
            }
            catch
            {

            }
            if (mnResult == 1)
            {
                objfilename.status = true;
                objfilename.message = "Document Uploaded Successfully";
                return true;
            }
            else
            {
                objfilename.status = false;
                objfilename.message = "Error Ocuured while uploading document";
                return false;
            }
        }

        public bool DaEditgeneraldocument(HttpRequest httpRequest, UploadDocumentname objfilename, string employee_gid, string user_gid)
        {
            UploadDocumentList objdocumentmodel = new UploadDocumentList();
            HttpFileCollection httpFileCollection;

            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms = new MemoryStream();
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string lsdocumenttype_gid = string.Empty;
            string project_flag = httpRequest.Form["project_flag"].ToString();
            String path = lspath;

            string document_type = httpRequest.Form["document_type"].ToString();


            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";

            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "IDAS/Generaldocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

            if ((!System.IO.Directory.Exists(path)))
                System.IO.Directory.CreateDirectory(path);

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
                        ls_readStream.CopyTo(ms);
                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.status = false;
                            objfilename.message = "File format is not supported";
                            return false;
                        }
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "IDAS/Generaldocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();

                        //lspath = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "IDAS/generaldocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
                        //objcmnfunctions.uploadFile(lspath, lsfile_gid);
                        //FileStream file = new FileStream(lspath, FileMode.Create, FileAccess.Write);
                        //ms.WriteTo(file);
                        //file.Close();
                        //ms.Close();
                        if (document_type == "undefined")
                        {
                            lsdocument_type = "";
                        }
                        else
                        {
                            lsdocument_type = document_type;
                        }
                        //lspath = "../../../erp_documents" + "/" + lscompany_code + "/" + "IDAS/generaldocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "IDAS/Generaldocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";



                        msGetGid = objcmnfunctions.GetMasterGID("DOCS");
                        msSQL = " insert into ids_trn_tuploadgeneraldocument( " +
                                    " generaldocument_gid," +
                                    " customer2sanction_gid ," +
                                    " document_name," +
                                    " document_path," +
                                    " document_type ," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + lsdocument_type.Replace("'", "") + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

            }
            catch
            {

            }
            if (mnResult == 1)
            {
                objfilename.status = true;
                objfilename.message = "Document Uploaded Successfully";
                return true;
            }
            else
            {
                objfilename.status = false;
                objfilename.message = "Error Ocuured while uploading document";
                return false;
            }
        }

        public void Dagetcamdoc_delete(string employee_gid, UploadDocumentname values)
        {
            msSQL = "delete from ids_trn_tuploadcamdocument where customer2sanction_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "CAM Document deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occrued while deleting document";
                values.status = false;
            }
        }

        public void Dagetmomdoc_delete(string employee_gid, UploadDocumentname values)
        {
            msSQL = "delete from ids_trn_tuploadmomdocument where customer2sanction_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "MOM Document deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occrued while deleting document";
                values.status = false;
            }
        }

        public void DaGetDocumentCancel(string document_gid, UploadDocumentname values, string employee_gid)
        {
            msSQL = "update ids_trn_tuploadgeneraldocument set delete_flag='Y', updated_by='" + employee_gid + "' where generaldocument_gid='" + document_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Document deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occrued while deleting document";
                values.status = false;
            }
        }

        public void DaGetDocumentCancel_add(string document_gid, UploadDocumentname values, string employee_gid)
        {
            msSQL = "delete from  ids_trn_tuploadgeneraldocument where generaldocument_gid='" + document_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Document deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occrued while deleting document";
                values.status = false;
            }
        }

        public void DaGetcamdoc_delete(string document_gid, UploadDocumentname values, string employee_gid)
        {
            msSQL = "update ids_trn_tuploadcamdocument set delete_flag='Y', updated_by='" + employee_gid + "' where camdocument_gid='" + document_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Document deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occrued while deleting document";
                values.status = false;
            }
        }

        public void DaGetcamdoc_delete_add(string document_gid, UploadDocumentname values, string employee_gid)
        {
            msSQL = "delete from ids_trn_tuploadcamdocument where camdocument_gid='" + document_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Document deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occrued while deleting document";
                values.status = false;
            }
        }

        public void DaGetmomdoc_delete(string document_gid, UploadDocumentname values, string employee_gid)
        {
            msSQL = "update ids_trn_tuploadmomdocument set delete_flag='Y', updated_by='" + employee_gid + "'  where momdocument_gid='" + document_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Document deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occrued while deleting document";
                values.status = false;
            }
        }

        public void DaGetmomdoc_delete_add(string document_gid, UploadDocumentname values, string employee_gid)
        {
            msSQL = "delete from ids_trn_tuploadmomdocument where momdocument_gid='" + document_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Document deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occrued while deleting document";
                values.status = false;
            }
        }

        public bool DaGetTempDelete(string employee_gid, result value)
        {

            msSQL = " delete from ids_trn_tuploadcamdocument where customer2sanction_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from ids_trn_tuploadmomdocument where customer2sanction_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from ids_trn_tuploadgeneraldocument where customer2sanction_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from ids_trn_tuploadsanctionletter where customer2sanction_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from ids_mst_taddbuyer where customer2sanction_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from ocs_mst_tsanction2ccmemberlist where customer2sanction_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from ocs_mst_tsanction2loanfacilitytype where customer2sanction_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from ids_trn_tuploadbaldocument where customer2sanction_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from ids_trn_tuploadesdeclarationdocument where customer2sanction_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from ids_trn_tdeviationmaildocument where customer2sanction_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //update
            msSQL = " update ids_trn_tuploadcamdocument set delete_flag='N' where delete_flag='Y' and updated_by='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update ids_trn_tuploadmomdocument set delete_flag='N' where delete_flag='Y' and updated_by='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update ids_trn_tuploadgeneraldocument set delete_flag='N' where delete_flag='Y' and updated_by='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update ids_mst_taddbuyer set delete_flag='N' where delete_flag='Y'and updated_by='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update ocs_mst_tsanction2ccmemberlist set delete_flag='N' where delete_flag='Y'and updated_by='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update ocs_mst_tsanction2loanfacilitytype set delete_flag='N' where delete_flag='Y'and updated_by='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update ids_trn_tuploadbaldocument set delete_flag='N' where delete_flag='Y' and updated_by='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update ids_trn_tuploadesdeclarationdocument set delete_flag='N' where delete_flag='Y' and updated_by='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update ids_trn_tdeviationmaildocument set delete_flag='N' where delete_flag='Y' and updated_by='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)

            {
                value.status = true;

                return true;
            }
            else
            {
                value.status = false;

                return false;
            }
        }

        public void DaEditmomdoc(string sanction_gid, result values)
        {
            msSQL = "delete from ids_trn_tuploadmomdocument where customer2sanction_gid='" + sanction_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "MOM Document deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occrued while deleting document";
                values.status = false;
            }

        }

        public void DaEditcamdoc(string sanction_gid, result values)
        {
            msSQL = "delete from ids_trn_tuploadcamdocument where customer2sanction_gid='" + sanction_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "CAM Document deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occrued while deleting document";
                values.status = false;
            }

        }

        public void DaGetgeneraldocment(string sanction_gid, UploadDocumentname values, string employee_gid)
        {
            msSQL = " select generaldocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type, " +
                     " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                     " from ids_trn_tuploadgeneraldocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                     " and b.user_gid = c.user_gid and ( customer2sanction_gid='" + employee_gid + "' or customer2sanction_gid='" + sanction_gid + "')" +
                     " and delete_flag='N'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<UploadDocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new UploadDocumentList
                    {
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                        document_gid = (dr_datarow["generaldocument_gid"].ToString()),
                        document_type = dr_datarow["document_type"].ToString(),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        updated_date = dr_datarow["uploaded_date"].ToString()
                    });
                }
                values.UploadDocumentList = get_filename;
            }
            dt_datatable.Dispose();

        }

        public void Dagetmandatoryfile_check(string esdeclaration_status, result values, string employee_gid)
        {
            try
            {
                msSQL = "select * from ids_trn_tuploadmomdocument where customer2sanction_gid='" + employee_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    objODBCDataReader.Close();
                    msSQL = "select * from ids_trn_tuploadcamdocument where customer2sanction_gid='" + employee_gid + "'";
                    objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader1.HasRows == true)
                    {
                        objODBCDataReader1.Close();
                        if (esdeclaration_status == "Yes")
                        {
                            msSQL = "select * from ids_trn_tuploadesdeclarationdocument where customer2sanction_gid='" + employee_gid + "'";
                            objODBCDataReader2 = objdbconn.GetDataReader(msSQL);
                            if (objODBCDataReader2.HasRows == false)
                            {
                                objODBCDataReader2.Close();
                                values.message = "Kindly upload E & S Declaration Document";
                                values.status = false;
                            }
                            else
                            {
                                values.status = true;
                            }
                        }
                        else
                        {
                            values.status = true;
                        }
                    }
                    else
                    {
                        values.message = "Kindly upload CAM Document";
                        values.status = false;
                    }

                }
                else
                {
                    values.message = "Kindly upload MOM Document";
                    values.status = false;
                }
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGeteditmandatory_check(result values, string sanction_gid, string esdeclaration_status, string employee_gid)
        {
            try
            {
                msSQL = "select * from ids_trn_tuploadmomdocument where customer2sanction_gid='" + employee_gid + "' or  customer2sanction_gid='" + sanction_gid + "' and" +
                    " delete_flag='N'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    objODBCDataReader.Close();
                    msSQL = "select * from ids_trn_tuploadcamdocument where customer2sanction_gid='" + employee_gid + "' or  customer2sanction_gid='" + sanction_gid + "'and " +
                     " delete_flag='N'";
                    objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader1.HasRows == false)
                    {
                        objODBCDataReader1.Close();

                        values.message = "Kindly upload CAM Document";
                        values.status = false;
                    }
                    else
                    {
                        msSQL = "select * from ocs_mst_tsanction2ccmemberlist where customer2sanction_gid='" + employee_gid + "' or  " +
                            " customer2sanction_gid='" + sanction_gid + "' and delete_flag = 'N'";
                        objODBCDataReader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDataReader.HasRows == false)
                        {
                            objODBCDataReader.Close();
                            values.status = false;
                            values.message = "Kindly Add Atleast one CC Member";

                        }
                        else
                        {
                            msSQL = "select * from ocs_mst_tsanction2loanfacilitytype where customer2sanction_gid='" + employee_gid + "'" +
                                "  or  customer2sanction_gid='" + sanction_gid + "' and delete_flag = 'N'";
                            objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
                            if (objODBCDataReader1.HasRows == false)
                            {
                                objODBCDataReader1.Close();
                                values.status = false;
                                values.message = "Kindly Add Loan Facility Type";

                            }
                            else
                            {
                                if (esdeclaration_status == "No")
                                {
                                    msSQL = "select * from ids_trn_tdeviationmaildocument where customer2sanction_gid='" + employee_gid + "' or  customer2sanction_gid='" + sanction_gid + "' and" +
                   " delete_flag='N'";
                                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDataReader.HasRows == false)
                                    {
                                        objODBCDataReader.Close();
                                        values.status = false;
                                        values.message = "Kindly Upload Deviation Mail Document";
                                    }
                                    else
                                    {
                                        values.status = true;
                                    }
                                }

                                else
                                {
                                    msSQL = "select * from ids_trn_tuploadesdeclarationdocument where customer2sanction_gid='" + employee_gid + "' or  customer2sanction_gid='" + sanction_gid + "' and" +
                    " delete_flag='N'";
                                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDataReader.HasRows == false)
                                    {
                                        objODBCDataReader.Close();
                                        values.status = false;
                                        values.message = "Kindly Upload E & S Declaration Document";
                                    }
                                    else
                                    {
                                        values.status = true;
                                    }
                                }
                            }

                        }

                    }


                }
                else
                {
                    values.message = "Kindly upload MOM Document";
                    values.status = false;
                }

            }
            catch
            {
                values.status = false;
            }
        }

        public bool DapostBALdocument(HttpRequest httpRequest, UploadDocumentname objfilename, string employee_gid, string user_gid)
        {
            UploadDocumentList objdocumentmodel = new UploadDocumentList();
            HttpFileCollection httpFileCollection;

            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms = new MemoryStream();
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
            path = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "IDAS/BALdocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

            if ((!System.IO.Directory.Exists(path)))
                System.IO.Directory.CreateDirectory(path);

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
                        ls_readStream.CopyTo(ms);

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.status = false;
                            objfilename.message = "File format is not supported";
                            return false;
                        }
                        //lspath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "IDAS/generaldocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
                        //objcmnfunctions.uploadFile(lspath, lsfile_gid);
                        //lspath = "../../../erp_documents" + "/" + lscompany_code + "/" + "IDAS/generaldocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "IDAS/BALdocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "IDAS/BALdocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("DOCS");
                        msSQL = " insert into ids_trn_tuploadbaldocument( " +
                                    " baldocument_gid," +
                                    " buyer_gid ," +
                                    " document_name," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

            }
            catch
            {

            }
            if (mnResult == 1)
            {
                objfilename.status = true;

                return true;
            }
            else
            {
                objfilename.status = false;

                return false;
            }
        }

        public void DaGetBuyerinfo(Mdladdbuyer values, string employee_gid)
        {
            msSQL = " select  addbuyer_gid,if (document_name is null,'---',document_name) as document_name,baldocument_gid," +
                    " concat(date_format(a.created_date, '%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,buyer_name," +
                    " buyer_exposure,concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as uploaded_by from ids_mst_taddbuyer a" +
                    " left join ids_trn_tuploadbaldocument d on a.addbuyer_gid = d.buyer_gid" +
                     " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                     " left  join adm_mst_tuser c on b.user_gid = c.user_gid where" +
                     " a.customer2sanction_gid ='" + employee_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<buyer_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new buyer_list
                    {
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = ((dr_datarow["document_path"].ToString())),
                        buyer_gid = (dr_datarow["addbuyer_gid"].ToString()),
                        buyer_name = dr_datarow["buyer_name"].ToString(),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        uploaded_date = dr_datarow["uploaded_date"].ToString(),
                        buyer_exposure = dr_datarow["buyer_exposure"].ToString(),
                        baldocument_gid = dr_datarow["baldocument_gid"].ToString()
                    });
                }
                values.buyer_list = get_filename;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetBuyerinfoEdit(Mdladdbuyer values, string employee_gid, string sanction_gid)
        {
            msSQL = " select  addbuyer_gid,if (document_name is null,'---',document_name) as document_name,baldocument_gid," +
                    " concat(date_format(a.created_date, '%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,buyer_name," +
                    " buyer_exposure,concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as uploaded_by from ids_mst_taddbuyer a" +
                    " left join ids_trn_tuploadbaldocument d on a.addbuyer_gid = d.buyer_gid" +
                     " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                     " left  join adm_mst_tuser c on b.user_gid = c.user_gid where" +
                     " (a.customer2sanction_gid ='" + employee_gid + "' or a.customer2sanction_gid ='" + sanction_gid + "') and a.delete_flag='N'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<buyer_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new buyer_list
                    {
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                        buyer_gid = (dr_datarow["addbuyer_gid"].ToString()),
                        buyer_name = dr_datarow["buyer_name"].ToString(),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        uploaded_date = dr_datarow["uploaded_date"].ToString(),
                        buyer_exposure = dr_datarow["buyer_exposure"].ToString(),
                        baldocument_gid = dr_datarow["baldocument_gid"].ToString()
                    });
                }
                values.buyer_list = get_filename;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaPostBuyerInfo(string employee_gid, Mdladdbuyer values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("ABUY");
            msSQL = "insert into ids_mst_taddbuyer (" +
                  " addbuyer_gid," +
                  " buyer_name," +
                  " buyer_exposure," +
                  " customer2sanction_gid," +
                  " created_by," +
                  " created_date" +
                  " )values(" +
                  "'" + msGetGid + "'," +
                  "'" + values.buyer_name + "'," +
                  "'" + values.buyer_exposure + "'," +
                  "'" + employee_gid + "'," +
                  "'" + employee_gid + "'," +
                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "update ids_trn_tuploadbaldocument set buyer_gid='" + msGetGid + "' where buyer_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Buyer Added Successfully";

                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured while adding";
                }
            }
        }

        public void DaGetbuyerCancel(string buyer_gid, UploadDocumentname values, string employee_gid)
        {
            msSQL = "update ids_mst_taddbuyer set delete_flag='Y', updated_by='" + employee_gid + "' where addbuyer_gid='" + buyer_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "update ids_trn_tuploadbaldocument set delete_flag='Y', updated_by='" + employee_gid + "' where buyer_gid='" + buyer_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Document deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occrued while deleting document";
                values.status = false;
            }
        }

        public void DaGetbuyerCancel_add(string buyer_gid, UploadDocumentname values, string employee_gid)
        {
            msSQL = "delete from ids_mst_taddbuyer where addbuyer_gid='" + buyer_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from  ids_trn_tuploadbaldocument where buyer_gid='" + buyer_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Document deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occrued while deleting document";
                values.status = false;
            }
        }

        public bool DaEditCAMddocument(HttpRequest httpRequest, UploadDocumentname objfilename, string employee_gid)
        {
            UploadDocumentList objdocumentmodel = new UploadDocumentList();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms = new MemoryStream();
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string lsdocumenttype_gid = string.Empty;
            String path = lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();

            string document_type = httpRequest.Form["document_type"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";

            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "IDAS/CAMdocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
            string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
            string lsfirstdocument_filepath = string.Empty;

            httpFileCollection = httpRequest.Files;

            httpPostedFile = httpFileCollection[0];
            string FileExtension = httpPostedFile.FileName;
            //string lsfile_gid = msdocument_gid + FileExtension;
            string lsfile_gid = msdocument_gid;
            FileExtension = Path.GetExtension(FileExtension).ToLower();
            lsfile_gid = lsfile_gid + FileExtension;
            ls_readStream = httpPostedFile.InputStream;
            ls_readStream.CopyTo(ms);

            byte[] bytes = ms.ToArray();
            if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
            {
                objfilename.status = false;
                objfilename.message = "File format is not supported";
                return false;
            }
            //lspath = HttpContext.Current.Server.MapPath("../../../erpdocuments" + "/" + lscompany_code + "/" + "IDAS/CAMdocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
            //objcmnfunctions.uploadFile(lspath, lsfile_gid);



            bool status;
            status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "IDAS/CAMdocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
            ms.Close();
            lspath = "erpdocument" + "/" + lscompany_code + "/" + "IDAS/CAMdocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

            if (document_type == "undefined")
            {
                lsdocument_type = "";
            }
            else
            {
                lsdocument_type = document_type;
            }

            //lspath = "../../../erp_documents" + "/" + lscompany_code + "/" + "IDAS/CAMdocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
            msGetGid = objcmnfunctions.GetMasterGID("CAMD");
            msSQL = " insert into ids_trn_tuploadcamdocument( " +
                         " camdocument_gid," +
                         " document_name, " +
                         " document_path, " +
                         " customer2sanction_gid," +
                         " document_type ," +
                         " created_by ," +
                         " created_date " +
                         " )values(" +
                         "'" + msGetGid + "'," +
                         "'" + httpPostedFile.FileName + "'," +
                         "'" + lspath + msdocument_gid + FileExtension + "'," +
                         "'" + employee_gid + "'," +
                         "'" + lsdocument_type.Replace("'", "") + "'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                objfilename.status = true;
                objfilename.message = "CAM Document uploaded successfully";
                return true;
            }
            else
            {
                objfilename.status = false;
                objfilename.message = "Error Occured while uploading CAM document";
                return false;
            }
        }

        public bool DaEditMOMddocument(HttpRequest httpRequest, UploadDocumentname objfilename, string employee_gid)
        {
            UploadDocumentList objdocumentmodel = new UploadDocumentList();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms = new MemoryStream();
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string lsdocumenttype_gid = string.Empty;
            String path = lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();

            string document_type = httpRequest.Form["document_type"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";

            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "IDAS/MOMdocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

            if ((!System.IO.Directory.Exists(path)))
                System.IO.Directory.CreateDirectory(path);

            string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
            string lsfirstdocument_filepath = string.Empty;

            httpFileCollection = httpRequest.Files;

            httpPostedFile = httpFileCollection[0];
            string FileExtension = httpPostedFile.FileName;
            //string lsfile_gid = msdocument_gid + FileExtension;
            string lsfile_gid = msdocument_gid;
            FileExtension = Path.GetExtension(FileExtension).ToLower();
            lsfile_gid = lsfile_gid + FileExtension;
            ls_readStream = httpPostedFile.InputStream;
            ls_readStream.CopyTo(ms);

            byte[] bytes = ms.ToArray();
            if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
            {
                objfilename.status = false;
                objfilename.message = "File format is not supported";
                return false;
            }
                //lspath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "IDAS/MOMdocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
                //objcmnfunctions.uploadFile(lspath, lsfile_gid);



                bool status;
            status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "IDAS/MOMdocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
            ms.Close();
            lspath = "erpdocument" + "/" + lscompany_code + "/" + "IDAS/MOMdocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

            if (document_type == "undefined")
            {
                lsdocument_type = "";
            }
            else
            {
                lsdocument_type = document_type;
            }

            //lspath = "../../../erp_documents" + "/" + lscompany_code + "/" + "IDAS/MOMdocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

            msGetGid = objcmnfunctions.GetMasterGID("MOMD");
            msSQL = " insert into ids_trn_tuploadmomdocument( " +
                         " momdocument_gid," +
                         " document_name, " +
                         " document_path, " +
                         " customer2sanction_gid," +
                          " document_type ," +
                         " created_by ," +
                         " created_date " +
                         " )values(" +
                         "'" + msGetGid + "'," +
                         "'" + httpPostedFile.FileName + "'," +
                         "'" + lspath + msdocument_gid + FileExtension + "'," +
                         "'" + employee_gid + "'," +
                         "'" + lsdocument_type.Replace("'", "") + "'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                objfilename.status = true;
                objfilename.message = "MOM Document uploaded successfully";
                return true;
            }
            else
            {
                objfilename.status = false;
                objfilename.message = "Error Occured while uploading MOM document";
                return false;
            }



        }

        public void DaGetmomdocment(string sanction_gid, UploadDocumentname values, string employee_gid)
        {
            msSQL = " select momdocument_gid,document_type,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path, " +
                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                " from ids_trn_tuploadmomdocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                " and b.user_gid = c.user_gid and  ( customer2sanction_gid='" + employee_gid + "' or customer2sanction_gid='" + sanction_gid + "') and delete_flag='N'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<UploadMOMDocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new UploadMOMDocumentList
                    {
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_gid = (dr_datarow["momdocument_gid"].ToString()),
                        document_type = (dr_datarow["document_type"].ToString()),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        updated_date = dr_datarow["uploaded_date"].ToString()
                    });
                }
                values.UploadMOMDocumentList = get_filename;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetcamdocment(string sanction_gid, UploadDocumentname values, string employee_gid)
        {
            msSQL = " select camdocument_gid,document_type,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path, " +
                  " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                  " from ids_trn_tuploadcamdocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                  " and b.user_gid = c.user_gid and  ( customer2sanction_gid='" + employee_gid + "' or customer2sanction_gid='" + sanction_gid + "') and delete_flag='N'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<UploadDocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new UploadDocumentList
                    {
                        document_path = (dr_datarow["document_path"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_gid = (dr_datarow["camdocument_gid"].ToString()),
                        document_type = (dr_datarow["document_type"].ToString()),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        updated_date = dr_datarow["uploaded_date"].ToString()
                    });
                }
                values.UploadDocumentList = get_filename;
            }
            dt_datatable.Dispose();
            values.status = true;

        }

        public void Dapostccmembers(string employee_gid, mdlccmembers values)
        {

            if ((values.ccmember_name != null) || (values.ccmember_name != ""))
            {
                if ((values.ccmember_remarks != null) || (values.ccmember_remarks != " "))
                {
                    msSQL = "select ccmember_gid from ocs_mst_tsanction2ccmemberlist " +
                     " where customer2sanction_gid='" + employee_gid + "' and ccmember_gid='" + values.ccmember_gid + "'  and ccgroup_name='" + values.ccgroup_name + "' ";
                    string ccmember_gid = objdbconn.GetExecuteScalar(msSQL);

                    if (ccmember_gid == "")
                    {
                        msGetGidCC = objcmnfunctions.GetMasterGID("SCMR");
                        msSQL = " insert into ocs_mst_tsanction2ccmemberlist(" +
                                " ccmemberlist_gid," +
                                " customer2sanction_gid," +
                                " ccmember_gid," +
                                " ccmember_name," +
                                " ccmember_remarks," +
                                " ccgroup_name," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGidCC + "'," +
                                "'" + employee_gid + "'," +
                                "'" + values.ccmember_gid + "'," +
                                "'" + values.ccmember_name + "'," +
                                 "'" + values.ccmember_remarks + "'," +
                                 "'" + values.ccgroup_name + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult != 0)
                        {
                            values.message = "CC Member Added Successfully";
                            values.status = true;
                        }
                    }
                    else
                    {
                        values.message = "CC Member already added";
                        values.status = false;
                    }
                }
                else
                {
                    values.message = "Kindly Enter Remarks ";
                    values.status = false;
                }
            }
            else
            {
                values.message = "Kindly Select CC member  ";
                values.status = false;
            }
        }

        public void Daupdateccmembers(string employee_gid, mdlccmembers values)
        {

            if ((values.ccmember_gid != null) || (values.ccmember_gid != ""))
            {
                if ((values.ccmember_remarks != null) || (values.ccmember_remarks != " "))
                {
                    msSQL = "select ccmember_gid from ocs_mst_tsanction2ccmemberlist " +
                     " where (customer2sanction_gid='" + values.sanction_gid + "' or customer2sanction_gid='" + employee_gid + "')" +
                     " and ccmember_gid='" + values.ccmember_gid + "' and ccgroup_name='" + values.ccgroup_name + "' and delete_flag='N' ";
                    string ccmember_gid = objdbconn.GetExecuteScalar(msSQL);

                    if (ccmember_gid == "")
                    {
                        msGetGidCC = objcmnfunctions.GetMasterGID("SCMR");
                        msSQL = " insert into ocs_mst_tsanction2ccmemberlist(" +
                                " ccmemberlist_gid," +
                                " customer2sanction_gid," +
                                " ccmember_gid," +
                                " ccmember_name," +
                                " ccgroup_name," +
                                " ccmember_remarks," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGidCC + "'," +
                                "'" + employee_gid + "'," +
                                "'" + values.ccmember_gid + "'," +
                                "'" + values.ccmember_name + "'," +
                                "'" + values.ccgroup_name + "'," +
                                 "'" + values.ccmember_remarks + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult != 0)
                        {
                            values.message = "CC Member Added Successfully";
                            values.status = true;
                        }
                    }
                    else
                    {
                        values.message = "CC Member already added";
                        values.status = false;
                    }
                }
                else
                {
                    values.message = "Kindly Enter Remarks ";
                    values.status = false;
                }
            }
            else
            {
                values.message = "Kindly Select CC member  ";
                values.status = false;
            }
        }

        public void DaGetCCmembers(string employee_gid, mdlccmembers values)
        {
            msSQL = "select ccmemberlist_gid,ccmember_name,ccmember_gid,ccmember_remarks,ccgroup_name from ocs_mst_tsanction2ccmemberlist " +
                " where customer2sanction_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mdlccmember = new List<mdlccmember>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_mdlccmember.Add(new mdlccmember
                    {
                        ccmemberlist_gid = (dr_datarow["ccmemberlist_gid"].ToString()),
                        ccmember_name = (dr_datarow["ccmember_name"].ToString()),
                        ccmember_gid = (dr_datarow["ccmember_gid"].ToString()),
                        ccmember_remarks = (dr_datarow["ccmember_remarks"].ToString()),
                        ccgroup_name = (dr_datarow["ccgroup_name"].ToString()),
                    });
                }
                values.mdlccmember = get_mdlccmember;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void Dadeleteccmember(string ccmemberlist_gid, mdlccmembers values, string employee_gid)
        {
            msSQL = "update ocs_mst_tsanction2ccmemberlist set delete_flag='Y', updated_by='" + employee_gid + "'  where ccmemberlist_gid='" + ccmemberlist_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "CC Member Deleted Successfully";
                values.status = true;
            }
        }

        public void Dadeleteccmember_add(string ccmemberlist_gid, mdlccmembers values, string employee_gid)
        {
            msSQL = "delete from  ocs_mst_tsanction2ccmemberlist  where ccmemberlist_gid='" + ccmemberlist_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "CC Member Deleted Successfully";
                values.status = true;
            }
        }

        public void DaEditccmember(string employee_gid, string sanction_gid, mdlccmembers values)
        {
            msSQL = "select ccmemberlist_gid,ccmember_name,ccmember_gid,ccmember_remarks,ccgroup_name from ocs_mst_tsanction2ccmemberlist " +
                " where (customer2sanction_gid='" + employee_gid + "' or customer2sanction_gid='" + sanction_gid + "') and delete_flag='N'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mdlccmember = new List<mdlccmember>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_mdlccmember.Add(new mdlccmember
                    {
                        ccmemberlist_gid = (dr_datarow["ccmemberlist_gid"].ToString()),
                        ccmember_name = (dr_datarow["ccmember_name"].ToString()),
                        ccmember_gid = (dr_datarow["ccmember_gid"].ToString()),
                        ccmember_remarks = (dr_datarow["ccmember_remarks"].ToString()),
                        ccgroup_name = (dr_datarow["ccgroup_name"].ToString()),
                    });
                }
                values.mdlccmember = get_mdlccmember;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void Dapostloanfacilitytype(string employee_gid, Mdlloanfacility_type values)
        {
            long sanction_amount = Convert.ToInt64(values.sanction_amount.Replace(",", "").Replace(".00", ""));
            long totalloanfacility_amount = Convert.ToInt64(values.totalloanfacility_amount.Replace(",", "").Replace(".00", ""));
            long loanfacility_amount = Convert.ToInt64(values.loanfacility_amount.Replace(",", "").Replace(".00", ""));
            long documentlimit = Convert.ToInt64(values.document_limit.Replace(",", "").Replace(".00", ""));
            long totaldocumentlimit = Convert.ToInt64(values.total_documentlimit.Replace(",", "").Replace(".00", ""));

            if ((sanction_amount - totalloanfacility_amount) < (loanfacility_amount))
            {
                values.message = "Loan Facility Amount Exceeded From Sanction Amount";
                values.status = false;

            }
            else
            {
                if ((sanction_amount - totaldocumentlimit) < (documentlimit))
                {
                    values.message = "Document Limit Exceeded From Sanction Amount";
                    values.status = false;

                }

                else
                {


                    msSQL = "select count(sanction2loanfacilitytype_gid) as count from ocs_mst_tsanction2loanfacilitytype where " +
                     "delete_flag='N' and customer2sanction_gid='" + employee_gid + "'";
                    string lscount = objdbconn.GetExecuteScalar(msSQL);

                    int count = Convert.ToInt16(lscount);
                    int Ref_no = count + 1;
                    string loanfacilityref_no = "LFT" + Ref_no;
                    if (values.report_structure == "ODLIM")
                    {
                        lsreportstructure = values.report_structure;


                    }
                    else
                    {
                        msSQL = "select loanfacilityref_no from ocs_mst_tsanction2loanfacilitytype where sanction2loanfacilitytype_gid='" + values.report_structure + "'";
                        lsreportstructure = objdbconn.GetExecuteScalar(msSQL);
                    }


                    msGetGid1 = objcmnfunctions.GetMasterGID("CLON");
                    msSQL = " insert into ocs_mst_tsanction2loanfacilitytype(" +
                            " sanction2loanfacilitytype_gid," +
                            " customer2sanction_gid," +
                            " loanfacilityref_no," +
                            " loanfacility_gid," +
                            " loanfacility_type," +
                            " loanfacility_amount," +
                            " document_limit," +
                            " margin," +
                            " tenure," +
                            " revolving_type," +
                            " expiry_date," +
                            " interchangeability," +
                            " applicable_condition," +
                            " report_structure," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid1 + "'," +
                            "'" + employee_gid + "'," +
                             "'" + loanfacilityref_no + "'," +
                            "'" + values.loanfacility_gid + "'," +
                            "'" + values.loanfacility_type + "',";
                    if (values.loanfacility_amount == null)
                    {
                        msSQL += "'',";
                    }
                    else
                    {
                        msSQL += "'" + values.loanfacility_amount.Replace(",", "") + "',";

                    }
                    if (values.document_limit == null)
                    {
                        msSQL += "'',";
                    }
                    else
                    {
                        msSQL += "'" + values.document_limit.Replace(",", "") + "',";

                    }
                    msSQL += "'" + values.margin + "'," +
                                    "'" + values.tenure + "'," +
                                    "'" + values.revolving_type + "',";
                    if ((values.expiry_date == null) || (values.expiry_date == ""))
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += "'" + Convert.ToDateTime(values.expiry_date).ToString("yyyy-MM-dd") + "',";

                    }
                    msSQL += "'" + values.interchangeability + "'," +
                      "'" + values.applicable_condition + "'," +
                                        "'" + lsreportstructure + "'," +
                                     "'" + employee_gid + "'," +
                                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        values.message = "Loan Facility Type Added Successfully";
                        values.status = true;
                    }
                    else
                    {
                        values.message = "Error Occured while adding";
                        values.status = false;
                    }

                }

            }
        }

        public void Daupdateloanfacilitytype(string employee_gid, Mdlloanfacility_type values)
        {
            long sanction_amount = Convert.ToInt64(values.sanction_amount.Replace(",", "").Replace(".00", ""));
            long totalloanfacility_amount = Convert.ToInt64(values.totalloanfacility_amount.Replace(",", "").Replace(".00", ""));
            long loanfacility_amount = Convert.ToInt64(values.loanfacility_amount.Replace(",", "").Replace(".00", ""));
            long documentlimit = Convert.ToInt64(values.document_limit.Replace(",", "").Replace(".00", ""));
            long totaldocumentlimit = Convert.ToInt64(values.total_documentlimit.Replace(",", "").Replace(".00", ""));

            if ((sanction_amount - totalloanfacility_amount) < (loanfacility_amount))
            {
                values.message = "Loan Facility Amount Exceeded From Sanction Amount";
                values.status = false;

            }
            else
            {
                if ((sanction_amount - totaldocumentlimit) < (documentlimit))
                {
                    values.message = "Document Limit Exceeded From Sanction Amount";
                    values.status = false;

                }

                else
                {

                    msSQL = "select count(sanction2loanfacilitytype_gid) as count from ocs_mst_tsanction2loanfacilitytype where delete_flag='N' and " +
                        " ( customer2sanction_gid='" + values.customer2sanction_gid + "' or customer2sanction_gid='" + employee_gid + "')";
                    string lscount = objdbconn.GetExecuteScalar(msSQL);

                    int count = Convert.ToInt16(lscount);
                    int Ref_no = count + 1;
                    string loanfacilityref_no = "LFT" + Ref_no;
                    if (values.report_structure == "ODLIM")
                    {
                        lsreportstructure = values.report_structure;


                    }
                    else
                    {
                        msSQL = "select loanfacilityref_no from ocs_mst_tsanction2loanfacilitytype where sanction2loanfacilitytype_gid='" + values.report_structure + "'";
                        lsreportstructure = objdbconn.GetExecuteScalar(msSQL);
                    }
                    msGetGid1 = objcmnfunctions.GetMasterGID("CLON");
                    msSQL = " insert into ocs_mst_tsanction2loanfacilitytype(" +
                            " sanction2loanfacilitytype_gid," +
                            " customer2sanction_gid," +
                            " loanfacilityref_no," +
                            " loanfacility_gid," +
                            " loanfacility_type," +
                            " loanfacility_amount," +
                            " document_limit," +
                            " margin," +
                            " tenure," +
                            " revolving_type," +
                            " expiry_date," +
                            " interchangeability," +
                            " applicable_condition," +
                            " report_structure," +
                            " proposed_roi," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid1 + "'," +
                            "'" + employee_gid + "'," +
                            "'" + loanfacilityref_no + "'," +
                            "'" + values.loanfacility_gid + "'," +
                            "'" + values.loanfacility_type + "',";
                    if (values.loanfacility_amount == null)
                    {
                        msSQL += "'',";
                    }
                    else
                    {
                        msSQL += "'" + values.loanfacility_amount.Replace(",", "") + "',";

                    }
                    if (values.document_limit == null)
                    {
                        msSQL += "'',";
                    }
                    else
                    {
                        msSQL += "'" + values.document_limit.Replace(",", "") + "',";

                    }
                    msSQL += "'" + values.margin + "'," +
                                    "'" + values.tenure + "'," +
                                    "'" + values.revolving_type + "',";
                    if ((values.expiry_date == null) || (values.expiry_date == ""))
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += "'" + Convert.ToDateTime(values.expiry_date).ToString("yyyy-MM-dd") + "',";

                    }
                    msSQL += "'" + values.interchangeability + "'," +
                             "'" + values.applicable_condition + "'," +
                                     "'" + lsreportstructure + "'," +
                                     "'" + values.proposed_roi + "'," +
                                     "'" + employee_gid + "'," +
                                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        values.message = "Loan Facility Type Added Successfully";
                        values.status = true;
                    }
                    else
                    {
                        values.message = "Error Occured while adding";
                        values.status = false;
                    }

                }

            }
        }

        public void DaGetloanfacilitytype(string employee_gid, Mdlloanfacility_type values)
        {
            msSQL = "select sanction2loanfacilitytype_gid,loanfacility_gid,format(loanfacility_amount,2) as loanfacility_amount,loanfacility_type, " +
                " format(document_limit,2) as document_limit,margin,date_format(expiry_date, '%d-%m-%Y') as expiry_date,revolving_type,tenure," +
                " interchangeability,if(report_structure='','NA',report_structure) as report_structure,loanfacilityref_no" +
                " from ocs_mst_tsanction2loanfacilitytype  where customer2sanction_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getloanfacilitytype = new List<loanfacilitytype_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getloanfacilitytype.Add(new loanfacilitytype_list
                    {
                        sanction2loanfacilitytype_gid = (dr_datarow["sanction2loanfacilitytype_gid"].ToString()),
                        loanfacility_gid = (dr_datarow["loanfacility_gid"].ToString()),
                        loanfacility_amount = (dr_datarow["loanfacility_amount"].ToString()),
                        loanfacility_type = (dr_datarow["loanfacility_type"].ToString()),
                        document_limit = (dr_datarow["document_limit"].ToString()),
                        expiry_date = (dr_datarow["expiry_date"].ToString()),
                        revolving_type = (dr_datarow["revolving_type"].ToString()),
                        tenure = (dr_datarow["tenure"].ToString()),
                        margin = (dr_datarow["margin"].ToString()),
                        interchangeability = (dr_datarow["interchangeability"].ToString()),
                        report_structure = (dr_datarow["report_structure"].ToString()),
                        loanfacilityref_no = (dr_datarow["loanfacilityref_no"].ToString()),
                    });
                }
                values.loanfacilitytype_list = getloanfacilitytype;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void Dadeleteloanfacilitytype(string sanction2loanfacilitytype_gid, Mdlloanfacility_type values, string employee_gid)
        {
            msSQL = "update ocs_mst_tsanction2loanfacilitytype set delete_flag='Y', updated_by='" + employee_gid + "' where sanction2loanfacilitytype_gid='" + sanction2loanfacilitytype_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Loan Facility Type Deleted Successfully";
                values.status = true;
            }
        }

        public void Dadeleteloanfacilitytype_add(string sanction2loanfacilitytype_gid, Mdlloanfacility_type values, string employee_gid)
        {
            msSQL = "delete from  ocs_mst_tsanction2loanfacilitytype where sanction2loanfacilitytype_gid='" + sanction2loanfacilitytype_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Loan Facility Type Deleted Successfully";
                values.status = true;
            }
        }

        public void DaEditLoanfacilitytype(string sanction_gid, string employee_gid, Mdlloanfacility_type values)
        {
            msSQL = "select sanction2loanfacilitytype_gid,loanfacility_gid,format(loanfacility_amount,2) as loanfacility_amount,loanfacility_type, " +
                " format(document_limit,2) as document_limit,margin,date_format(expiry_date, '%d-%m-%Y') as expiry_date,revolving_type,tenure, interchangeability," +
                " if(report_structure='','NA',report_structure) as report_structure,loanfacilityref_no,proposed_roi " +
                " from ocs_mst_tsanction2loanfacilitytype  where (customer2sanction_gid='" + sanction_gid + "' or customer2sanction_gid='" + employee_gid + "') and" +
                " delete_flag='N'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getloanfacilitytype = new List<loanfacilitytype_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getloanfacilitytype.Add(new loanfacilitytype_list
                    {
                        sanction2loanfacilitytype_gid = (dr_datarow["sanction2loanfacilitytype_gid"].ToString()),
                        loanfacility_gid = (dr_datarow["loanfacility_gid"].ToString()),
                        loanfacility_amount = (dr_datarow["loanfacility_amount"].ToString()),
                        loanfacility_type = (dr_datarow["loanfacility_type"].ToString()),
                        document_limit = (dr_datarow["document_limit"].ToString()),
                        expiry_date = (dr_datarow["expiry_date"].ToString()),
                        revolving_type = (dr_datarow["revolving_type"].ToString()),
                        tenure = (dr_datarow["tenure"].ToString()),
                        margin = (dr_datarow["margin"].ToString()),
                        interchangeability = (dr_datarow["interchangeability"].ToString()),
                        report_structure = (dr_datarow["report_structure"].ToString()),
                        loanfacilityref_no = (dr_datarow["loanfacilityref_no"].ToString()),
                        proposed_roi = (dr_datarow["proposed_roi"].ToString()),
                    });
                }
                values.loanfacilitytype_list = getloanfacilitytype;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetvalidation(Mdlloanfacility_type values, string employee_gid)
        {
            msSQL = "select sum(loanfacility_amount) as loanfacility_amount,sum(document_limit) as document_limit" +
                " from ocs_mst_tsanction2loanfacilitytype  where customer2sanction_gid='" + employee_gid + "' and interchangeability<>'yes' and applicable_condition<>'Yes'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.totalloanfacility_amount = objODBCDataReader["loanfacility_amount"].ToString();
                values.total_documentlimit = objODBCDataReader["document_limit"].ToString();
            }
            objODBCDataReader.Close();
            values.status = true;

        }

        public void Daeditvalidation(Mdlloanfacility_type values, string sanction_gid, string employee_gid)
        {
            msSQL = "select sum(loanfacility_amount) as loanfacility_amount,sum(document_limit) as document_limit from ocs_mst_tsanction2loanfacilitytype  where " +
                " (customer2sanction_gid = '" + employee_gid + "' or customer2sanction_gid = '" + sanction_gid + "' ) and interchangeability<>'yes' " +
                " and delete_flag<>'Y' and applicable_condition<>'Yes'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.totalloanfacility_amount = objODBCDataReader["loanfacility_amount"].ToString();
                values.total_documentlimit = objODBCDataReader["document_limit"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = "select max(loanfacility_amount)  from ocs_mst_tsanction2loanfacilitytype where customer2sanction_gid='" + sanction_gid + "'and delete_flag<>'Y'" +
                " and interchangeability='yes'";
            string lsamount = objdbconn.GetExecuteScalar(msSQL);
            if (lsamount == "")
            {
                values.interchangeability_amount = "0";
            }
            else
            {
                values.interchangeability_amount = lsamount;
            }
            values.status = true;

        }

        public void DaGetloanfacilityref_no(Mdlloanfacility_type values, string employee_gid)
        {
            try
            {
                msSQL = " select loanfacilityref_no,sanction2loanfacilitytype_gid from ocs_mst_tsanction2loanfacilitytype where interchangeability<>'No' and loanfacilityref_no<>'ODLIM'" +
                        " and customer2sanction_gid='" + employee_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getblimit_info = new List<loanfacilitytype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getblimit_info.Add(new loanfacilitytype_list
                        {
                            loanfacilityref_no = (dr_datarow["loanfacilityref_no"].ToString()),
                            sanction2loanfacilitytype_gid = (dr_datarow["sanction2loanfacilitytype_gid"].ToString())
                        });
                    }
                    values.loanfacilitytype_list = getblimit_info;

                }
                dt_datatable.Dispose();
            }
            catch
            {
                values.status = false;
            }

        }

        public void DaEditloanfacilityref_no(Mdlloanfacility_type values, string employee_gid, string sanction_gid)
        {
            try
            {
                msSQL = " select loanfacilityref_no,sanction2loanfacilitytype_gid from ocs_mst_tsanction2loanfacilitytype where interchangeability<>'No' and loanfacilityref_no<>'ODLIM'" +
                        " and ( customer2sanction_gid='" + employee_gid + "' or   customer2sanction_gid='" + sanction_gid + "')";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getblimit_info = new List<loanfacilitytype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    getblimit_info.Add(new loanfacilitytype_list
                    {
                        loanfacilityref_no = "ODLIM",
                        sanction2loanfacilitytype_gid = "ODLIM",
                    });
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {

                        getblimit_info.Add(new loanfacilitytype_list
                        {
                            loanfacilityref_no = (dr_datarow["loanfacilityref_no"].ToString()),
                            sanction2loanfacilitytype_gid = (dr_datarow["sanction2loanfacilitytype_gid"].ToString())
                        });
                    }
                    values.loanfacilitytype_list = getblimit_info;

                }
                dt_datatable.Dispose();
            }
            catch
            {
                values.status = false;
            }

        }

        public void DaSanction_cancel(sanctiondetails values, string employee_gid)
        {
            try
            {
                msSQL = "select customer2sanction_gid from ids_trn_tlsa where customer2sanction_gid='" + values.customer2sanction_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    objODBCDataReader.Close();
                    values.message = "You can't able to cancel or reset this Sanction because this is tagged to LSA";
                    values.status = false;
                }
                else
                {
                    msGetGid = objcmnfunctions.GetMasterGID("HC2S");
                    msSQL = "select sanction_refno,sanction_date,sanction_amount,entity,entity_gid,colanding_status,colander_name,customer_urn,customer2sanction_gid," +
                        " vertical from ocs_mst_tcustomer2sanction where customer2sanction_gid = '" + values.customer2sanction_gid + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows == true)
                    {

                        msSQL = "insert into ocs_mst_thistorysanctionreset(" +
                            " historysanctionreset_gid," +
                            " customer2sanction_gid," +
                            " sanction_refno," +
                             " customer_urn," +
                             " sanction_date," +
                             " sanction_amount," +
                             " entity," +
                             " entity_gid," +
                             " colanding_status," +
                             " colander_name," +
                             " reset_remarks," +
                             " reset_date," +
                             " reseted_by)" +
                             " values (" +
                             " '" + msGetGid + "'," +
                             " '" + objODBCDataReader["customer2sanction_gid"].ToString() + "'," +
                             " '" + objODBCDataReader["sanction_refno"].ToString() + "'," +
                             " '" + objODBCDataReader["customer_urn"].ToString() + "'," +
                             " '" + objODBCDataReader["sanction_date"].ToString() + "'," +
                             " '" + objODBCDataReader["sanction_amount"].ToString() + "'," +
                             " '" + objODBCDataReader["entity"].ToString() + "'," +
                             " '" + objODBCDataReader["entity_gid"].ToString() + "'," +
                             " '" + objODBCDataReader["colanding_status"].ToString() + "'," +
                             " '" + objODBCDataReader["colander_name"].ToString() + "'," +
                             " '" + values.general_remarks.Replace("'", "") + "'," +
                              " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                              "'" + employee_gid + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        string vertical_code = objODBCDataReader["vertical"].ToString();
                        objODBCDataReader.Close();
                        msSQL = "select entity_name from adm_mst_tentity where entity_gid='" + values.entity_gid + "'";
                        string entity_name = objdbconn.GetExecuteScalar(msSQL);

                        if (entity_name != "SAMAGRO")
                        {
                            if (values.colanding_status == "No")
                            {
                                msSQL = "select verticalref_no from ocs_mst_tentitysequenceno where entity_gid='" + values.entity_gid + "' ";
                                string lssequencecurval = objdbconn.GetExecuteScalar(msSQL);

                                int seq_curval = int.Parse(lssequencecurval) + 1;
                                double lsseq_curval = Math.Floor(Math.Log10(seq_curval) + 1);
                                if (lsseq_curval == 1)
                                {
                                    lsnewref_no = vertical_code + "/" + "000" + Convert.ToInt32(seq_curval).ToString();
                                }
                                else if (lsseq_curval == 2)
                                {
                                    lsnewref_no = vertical_code + "/" + "00" + Convert.ToInt32(seq_curval).ToString();
                                }
                                else if (lsseq_curval == 3)
                                {
                                    lsnewref_no = vertical_code + "/" + "0" + Convert.ToInt32(seq_curval).ToString();
                                }
                                else if (lsseq_curval == 4)
                                {
                                    lsnewref_no = vertical_code + "/" + Convert.ToInt32(seq_curval).ToString();
                                }

                                msSQL = "update ocs_mst_tentitysequenceno set verticalref_no='" + seq_curval + "' where entity_gid='" + values.entity_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            }

                            else
                            {
                                if (values.colander_name == "DBS")
                                {
                                    msSQL = "select colending_dbs from ocs_mst_tentitysequenceno where entity_gid='" + values.entity_gid + "'";
                                    string lssequencecurval = objdbconn.GetExecuteScalar(msSQL);

                                    int seq_curval = int.Parse(lssequencecurval) + 1;
                                    double lsseq_curval = Math.Floor(Math.Log10(seq_curval) + 1);
                                    if (lsseq_curval == 1)
                                    {
                                        lsnewref_no = values.colander_name + "/" + "000" + Convert.ToInt32(seq_curval).ToString();
                                    }
                                    else if (lsseq_curval == 2)
                                    {
                                        lsnewref_no = values.colander_name + "/" + "00" + Convert.ToInt32(seq_curval).ToString();
                                    }
                                    else if (lsseq_curval == 3)
                                    {
                                        lsnewref_no = values.colander_name + "/" + "0" + Convert.ToInt32(seq_curval).ToString();
                                    }
                                    else if (lsseq_curval == 4)
                                    {
                                        lsnewref_no = values.colander_name + "/" + Convert.ToInt32(seq_curval).ToString();
                                    }

                                    msSQL = "update ocs_mst_tentitysequenceno set colending_dbs='" + seq_curval + "' where entity_gid='" + values.entity_gid + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                                else
                                {
                                    msSQL = "select colending_aar from ocs_mst_tentitysequenceno where entity_gid='" + values.entity_gid + "' ";
                                    string lssequencecurval = objdbconn.GetExecuteScalar(msSQL);

                                    int seq_curval = int.Parse(lssequencecurval) + 1;
                                    double lsseq_curval = Math.Floor(Math.Log10(seq_curval) + 1);
                                    if (lsseq_curval == 1)
                                    {
                                        lsnewref_no = values.colander_name + "/" + "000" + Convert.ToInt32(seq_curval).ToString();
                                    }
                                    else if (lsseq_curval == 2)
                                    {
                                        lsnewref_no = values.colander_name + "/" + "00" + Convert.ToInt32(seq_curval).ToString();
                                    }
                                    else if (lsseq_curval == 3)
                                    {
                                        lsnewref_no = values.colander_name + "/" + "0" + Convert.ToInt32(seq_curval).ToString();
                                    }
                                    else if (lsseq_curval == 4)
                                    {
                                        lsnewref_no = values.colander_name + "/" + Convert.ToInt32(seq_curval).ToString();
                                    }

                                    msSQL = "update ocs_mst_tentitysequenceno set colending_aar='" + seq_curval + "' where entity_gid='" + values.entity_gid + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                            }
                        }
                        else
                        {
                            msSQL = "select verticalref_no from ocs_mst_tentitysequenceno where entity_gid='" + values.entity_gid + "' ";
                            string lssequencecurval = objdbconn.GetExecuteScalar(msSQL);

                            int seq_curval = int.Parse(lssequencecurval) + 1;
                            double lsseq_curval = Math.Floor(Math.Log10(seq_curval) + 1);
                            if (lsseq_curval == 1)
                            {
                                lsnewref_no = "TF/" + "000" + Convert.ToInt32(seq_curval).ToString();
                            }
                            else if (lsseq_curval == 2)
                            {
                                lsnewref_no = "TF/" + "00" + Convert.ToInt32(seq_curval).ToString();
                            }
                            else if (lsseq_curval == 3)
                            {
                                lsnewref_no = "TF/" + "0" + Convert.ToInt32(seq_curval).ToString();
                            }
                            else if (lsseq_curval == 4)
                            {
                                lsnewref_no = "TF/" + Convert.ToInt32(seq_curval).ToString();
                            }

                            msSQL = "update ocs_mst_tentitysequenceno set verticalref_no='" + seq_curval + "' where entity_gid='" + values.entity_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }


                        var lscuryear = DateTime.Today.Year;
                        var lsnextyear = int.Parse(DateTime.Today.ToString("yy")) + 1;

                        lsfinyear = lscuryear + "-" + lsnextyear;

                        lssanctionref_no = entity_name + "/" + lsnewref_no + "/" + lsfinyear;



                        msSQL = "update ocs_mst_tcustomer2sanction set sanction_refno='" + lssanctionref_no + "'," +
                             " reset_flag='Y'," +
                            " entity='" + entity_name + "'," +
                            " entity_gid='" + values.entity_gid + "'," +
                            " colanding_status='" + values.colanding_status + "'," +
                              " colander_name='" + values.colander_name + "'," +
                              " updated_by='" + employee_gid + "'," +
                              " updated_date=current_timestamp" +
                              " WHERE customer2sanction_gid='" + values.customer2sanction_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult != 0)
                        {
                            values.message = "Sanction Reseted Successfully";
                            values.status = true;
                        }
                        else
                        {
                            values.message = "Error Occured while Reseting";
                            values.status = true;
                        }
                    }
                }
            }
            catch (Exception ce)
            {

                values.status = false;
            }

        }

        public void DaGetSanctioninfo(sanctiondetails values, string employee_gid, string sanction_gid)
        {
            try
            {
                msSQL = "select b.customer_urn,a.sanction_refno,date_format(a.sanction_date,'%d-%m-%Y') as sanction_date, " +
                 " format((sanction_amount),2) as sanction_amount,b.customername,a.entity,a.entity_gid,a.colanding_status,a.colander_name from ocs_mst_tcustomer2sanction a" +
                 " left join ocs_mst_tcustomer b on a.customer_gid=b.customer_gid where customer2sanction_gid='" + sanction_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    values.customername = objODBCDataReader["customername"].ToString();
                    values.sanction_refno = objODBCDataReader["sanction_refno"].ToString();
                    values.customer_urn = objODBCDataReader["customer_urn"].ToString();
                    values.sanction_date = objODBCDataReader["sanction_date"].ToString();
                    values.sanction_amount = objODBCDataReader["sanction_amount"].ToString();
                    values.entity = objODBCDataReader["sanction_amount"].ToString();
                    values.entity_gid = objODBCDataReader["entity_gid"].ToString();
                    values.colanding_status = objODBCDataReader["colanding_status"].ToString();
                    values.colander_name = objODBCDataReader["colander_name"].ToString();
                }
                objODBCDataReader.Close();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }

        }

        public void DaGethistoryvisible(sanctionhistory values, string customer2sanction_gid)
        {
            try
            {
                msSQL = "select customer_urn,customer_name from ocs_mst_tcustomer2sanction where customer2sanction_gid='" + customer2sanction_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    values.customer_name = objODBCDataReader["customer_name"].ToString();
                    values.customer_urn = objODBCDataReader["customer_urn"].ToString();
                }
                objODBCDataReader.Close();

                msSQL = "select sanction_refno,entity,colanding_status,if(colanding_status='No','---',colander_name) as colander_name ,reset_remarks," +
                    " date_format(reset_date,'%d-%m-%Y %h:%i %p') as reset_date,concat(c.user_firstname,' ',c.user_lastname,'/',c.user_code) as reseted_by" +
                    " from ocs_mst_thistorysanctionreset a" +
                    " left join hrm_mst_temployee b on a.reseted_by=b.employee_gid" +
                    " left join adm_mst_tuser c on b.user_gid=c.user_gid where customer2sanction_gid='" + customer2sanction_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gethistorySanctionDtlsEdit = new List<historySanctionDtlsEdit>();
                if (dt_datatable.Rows.Count != 0)
                {

                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {

                        gethistorySanctionDtlsEdit.Add(new historySanctionDtlsEdit
                        {
                            sanction_refno = (dr_datarow["sanction_refno"].ToString()),
                            entity = (dr_datarow["entity"].ToString()),
                            colending_status = (dr_datarow["colanding_status"].ToString()),
                            colending_name = (dr_datarow["colander_name"].ToString()),
                            reset_remarks = (dr_datarow["reset_remarks"].ToString()),
                            reset_date = (dr_datarow["reset_date"].ToString()),
                            reset_by = (dr_datarow["reseted_by"].ToString()),

                        });
                    }
                    values.historySanctionDtlsEdit = gethistorySanctionDtlsEdit;

                }
                dt_datatable.Dispose();

                values.status = true;
            }
            catch
            {
                values.status = false;
            }

        }

        public bool DaGetSanctionsummary(sanctiondetailsList values)
        {

            msSQL = " SELECT a.customer2sanction_gid,b.customer_urn,a.sanction_refno,date_format(a.sanction_date,'%d-%m-%Y') as sanction_date, " +
                   " format((sanction_amount),2) as sanction_amount,a.sanction_limit,b.customername,b.vertical_code," +
                   " concat(d.user_code,' / ',d.user_firstname,d.user_lastname) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                   " reset_flag FROM ocs_mst_tcustomer2sanction a " +
                   " LEFT JOIN ocs_mst_tcustomer b ON a.customer_gid = b.customer_gid" +
                   " LEFT JOIN hrm_mst_temployee c ON a.updated_by=c.employee_gid" +
                   " LEFT JOIN adm_mst_tuser d ON c.user_gid=d.user_gid " +
                   " ORDER BY customer2sanction_gid DESC";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_sanctiondtl = new List<sanctiondetails>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    get_sanctiondtl.Add(new sanctiondetails
                    {
                        customer2sanction_gid = dt["customer2sanction_gid"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        sanction_date = dt["sanction_date"].ToString(),
                        sanction_amount = dt["sanction_amount"].ToString(),
                        sanction_limit = dt["sanction_limit"].ToString(),
                        customername = dt["customername"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_code = dt["vertical_code"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        reset_flag = dt["reset_flag"].ToString(),
                    });
                }
                values.sanctiondetails = get_sanctiondtl;
            }
            dt_datatable.Dispose();

            return true;
        }

        public void Getcamdocmentadd(UploadDocumentname values, string employee_gid)
        {
            msSQL = " select camdocument_gid,document_name,document_type,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path, " +
                  " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                  " from ids_trn_tuploadcamdocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                  " and b.user_gid = c.user_gid and  ( customer2sanction_gid='" + employee_gid + "') and delete_flag='N'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<UploadCOMDocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new UploadCOMDocumentList
                    {
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_gid = (dr_datarow["camdocument_gid"].ToString()),
                        document_type = (dr_datarow["document_type"].ToString()),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        updated_date = dr_datarow["uploaded_date"].ToString()
                    });
                }
                values.UploadCOMDocumentList = get_filename;
            }
            dt_datatable.Dispose();
            values.status = true;

        }

        public void Getmomdocmentadd(UploadDocumentname values, string employee_gid)
        {
            msSQL = " select momdocument_gid,document_name,document_type,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path, " +
               " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
               " from ids_trn_tuploadmomdocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
               " and b.user_gid = c.user_gid and  ( customer2sanction_gid='" + employee_gid + "') and delete_flag='N'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<UploadMOMDocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new UploadMOMDocumentList
                    {
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_gid = (dr_datarow["momdocument_gid"].ToString()),
                        document_type = (dr_datarow["document_type"].ToString()),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        updated_date = dr_datarow["uploaded_date"].ToString()
                    });
                }
                values.UploadMOMDocumentList = get_filename;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public bool DaUploadsanctionletter(HttpRequest httpRequest, UploadDocumentname objfilename, string employee_gid, string user_gid)
        {
            UploadSANDocumentList objdocumentmodel = new UploadSANDocumentList();
            HttpFileCollection httpFileCollection;

            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms = new MemoryStream();
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string lsdocumenttype_gid = string.Empty;

            String path = lspath;

            string document_type = httpRequest.Form["document_type"].ToString();


            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";

            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "IDAS/sanctionletter/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

            if ((!System.IO.Directory.Exists(path)))
                System.IO.Directory.CreateDirectory(path);

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
                        ls_readStream.CopyTo(ms);
                        //lspath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "IDAS/sanctionletter/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
                        //objcmnfunctions.uploadFile(lspath, lsfile_gid);



                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "IDAS/sanctionletter/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "IDAS/sanctionletter/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        if (document_type == "undefined")
                        {
                            lsdocument_type = "";
                        }
                        else
                        {
                            lsdocument_type = document_type;
                        }
                        //lspath = "../../../erp_documents" + "/" + lscompany_code + "/" + "IDAS/sanctionletter/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        msGetGid = objcmnfunctions.GetMasterGID("DOCSL");
                        msSQL = " insert into ids_trn_tuploadsanctionletter( " +
                                    " sanctionletter_gid," +
                                    " customer2sanction_gid ," +
                                    " document_name," +
                                    " document_path," +
                                    " document_type ," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + lsdocument_type.Replace("'", "") + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

            }
            catch
            {

            }
            if (mnResult == 1)
            {
                objfilename.status = true;
                objfilename.message = "Sanction Letter Uploaded Successfully";
                return true;
            }
            else
            {
                objfilename.status = false;
                objfilename.message = "Error Ocuured while uploading document";
                return false;
            }
        }

        public void DaGetsanctionletter(UploadDocumentname values, string employee_gid)
        {
            msSQL = " select sanctionletter_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type, " +
                     " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                     " from ids_trn_tuploadsanctionletter a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                     " and b.user_gid = c.user_gid and ( customer2sanction_gid='" + employee_gid + "') and delete_flag='N'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_sanfilename = new List<UploadSANDocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_sanfilename.Add(new UploadSANDocumentList
                    {
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_gid = (dr_datarow["sanctionletter_gid"].ToString()),
                        document_type = dr_datarow["document_type"].ToString(),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        updated_date = dr_datarow["uploaded_date"].ToString()
                    });
                }
                values.UploadSANDocumentList = get_sanfilename;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetEditsanctionletter(string sanction_gid, UploadDocumentname values, string employee_gid)
        {
            msSQL = " select sanctionletter_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                    " from ids_trn_tuploadsanctionletter a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                    " and b.user_gid = c.user_gid and ( customer2sanction_gid='" + employee_gid + "' or customer2sanction_gid='" + sanction_gid + "') and delete_flag='N'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_sanfilename = new List<UploadSANDocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_sanfilename.Add(new UploadSANDocumentList
                    {
                        document_path = (dr_datarow["document_path"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_gid = (dr_datarow["sanctionletter_gid"].ToString()),
                        document_type = dr_datarow["document_type"].ToString(),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        updated_date = dr_datarow["uploaded_date"].ToString()
                    });
                }
                values.UploadSANDocumentList = get_sanfilename;
            }
        }

        public void DaGetsanctionletterCancel(string document_gid, UploadDocumentname values, string employee_gid)
        {
            msSQL = "update ids_trn_tuploadsanctionletter set delete_flag='Y', updated_by='" + employee_gid + "'  where sanctionletter_gid='" + document_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Document deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occrued while deleting document";
                values.status = false;
            }
        }

        public void DaGetPenalInterest(Mdlloanfacility_type values, string sanction_gid, string employee_gid)
        {

            msSQL = "select (26-proposed_roi) as proposed_roi,group_concat(loanfacility_type) as facility_type from ocs_mst_tsanction2loanfacilitytype where " +
                " (customer2sanction_gid='" + sanction_gid + "' or customer2sanction_gid='" + employee_gid + "') group by proposed_roi";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getloanfacilitytype_list = new List<loanfacilitytype_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {

                    getloanfacilitytype_list.Add(new loanfacilitytype_list
                    {
                        penal_interest = (dr_datarow["proposed_roi"].ToString()),
                        loanfacility_type = (dr_datarow["facility_type"].ToString())
                    });
                }
                values.loanfacilitytype_list = getloanfacilitytype_list;
                values.status = true;
            }
            else
            {
                values.status = false;
            }
            dt_datatable.Dispose();
        }

        public bool DaGetUpdateProposedROI(Mdlloanfacility_type values, string employee_gid)
        {

            msSQL = " update ocs_mst_tsanction2loanfacilitytype set " +
                   " proposed_roi='" + values.proposed_roi + "'" +
                   " where sanction2loanfacilitytype_gid='" + values.sanction2loanfacilitytype_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                values.status = true;
                values.message = "ROI Updated Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }

        public bool DaUploades_declarationdocument(HttpRequest httpRequest, UploadDocumentname objfilename, string employee_gid, string user_gid)
        {
            UploadES_DocumentList objdocumentmodel = new UploadES_DocumentList();
            HttpFileCollection httpFileCollection;

            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms = new MemoryStream();
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string lsdocumenttype_gid = string.Empty;
            string project_flag = httpRequest.Form["project_flag"].ToString();
            String path = lspath;

            string document_type = httpRequest.Form["document_type"].ToString();


            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";

            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "IDAS/esdeclarationdocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

            if ((!System.IO.Directory.Exists(path)))
                System.IO.Directory.CreateDirectory(path);

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
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.status = false;
                            objfilename.message = "File format is not supported";
                            return false;
                        }
                        //lspath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "IDAS/esdeclarationdocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
                        //objcmnfunctions.uploadFile(lspath, lsfile_gid);


                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "IDAS/esdeclarationdocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "IDAS/esdeclarationdocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                        if (document_type == "undefined")
                        {
                            lsdocument_type = "";
                        }
                        else
                        {
                            lsdocument_type = document_type;
                        }
                        //lspath = "../../../erp_documents" + "/" + lscompany_code + "/" + "IDAS/esdeclarationdocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        msGetGid = objcmnfunctions.GetMasterGID("DOESD");
                        msSQL = " insert into ids_trn_tuploadesdeclarationdocument( " +
                                    " esdeclaration_gid," +
                                    " customer2sanction_gid ," +
                                    " document_name," +
                                    " document_path," +
                                    " document_type ," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + lsdocument_type.Replace("'", "") + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

            }
            catch
            {

            }
            if (mnResult == 1)
            {
                objfilename.status = true;
                objfilename.message = "E & S Declaration Document Uploaded Successfully";
                return true;
            }
            else
            {
                objfilename.status = false;
                objfilename.message = "Error Ocuured while uploading document";
                return false;
            }
        }

        public void DaGetesdocument(UploadDocumentname values, string employee_gid)
        {
            msSQL = " select esdeclaration_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                    " from ids_trn_tuploadesdeclarationdocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                    " and b.user_gid = c.user_gid and ( customer2sanction_gid='" + employee_gid + "') and delete_flag='N'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_esdeclarationfilename = new List<UploadES_DocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_esdeclarationfilename.Add(new UploadES_DocumentList
                    {
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_gid = (dr_datarow["esdeclaration_gid"].ToString()),
                        document_type = dr_datarow["document_type"].ToString(),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        updated_date = dr_datarow["uploaded_date"].ToString()
                    });
                }
                values.UploadES_DocumentList = get_esdeclarationfilename;
            }
        }

        public void DaGetuploadesdocument_delete(string document_gid, UploadDocumentname values, string employee_gid)
        {
            msSQL = "update ids_trn_tuploadesdeclarationdocument set delete_flag='Y', updated_by='" + employee_gid + "'  where esdeclaration_gid='" + document_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Document deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occrued while deleting document";
                values.status = false;
            }
        }

        public bool DaUploadmaildocument(HttpRequest httpRequest, UploadDocumentname objfilename, string employee_gid, string user_gid)
        {
            UploadES_DocumentList objdocumentmodel = new UploadES_DocumentList();
            HttpFileCollection httpFileCollection;

            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms = new MemoryStream();
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string lsdocumenttype_gid = string.Empty;
            string project_flag = httpRequest.Form["project_flag"].ToString();
            String path = lspath;

            string document_type = httpRequest.Form["document_type"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";

            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "IDAS/deviationmaildocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

            if ((!System.IO.Directory.Exists(path)))
                System.IO.Directory.CreateDirectory(path);

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
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.status = false;
                            objfilename.message = "File format is not supported";
                            return false;
                        }
                        //lspath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "IDAS/deviationmaildocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
                        //objcmnfunctions.uploadFile(lspath, lsfile_gid);


                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "IDAS/deviationmaildocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "IDAS/deviationmaildocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";



                        if (document_type == "undefined")
                        {
                            lsdocument_type = "";
                        }
                        else
                        {
                            lsdocument_type = document_type;
                        }
                        //lspath = "../../../erp_documents" + "/" + lscompany_code + "/" + "IDAS/deviationmaildocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        msGetGid = objcmnfunctions.GetMasterGID("DODMA");
                        msSQL = " insert into ids_trn_tdeviationmaildocument( " +
                                    " maildocument_gid," +
                                    " customer2sanction_gid ," +
                                    " document_name," +
                                    " document_path," +
                                    " document_type ," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + lsdocument_type.Replace("'", "") + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

            }
            catch
            {

            }
            if (mnResult == 1)
            {
                objfilename.status = true;
                objfilename.message = "Deviation Mail Document Uploaded Successfully";
                return true;
            }
            else
            {
                objfilename.status = false;
                objfilename.message = "Error Ocuured while uploading document";
                return false;
            }
        }

        public void DaGetMaildocument(UploadDocumentname values, string employee_gid)
        {
            msSQL = " select maildocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                    " from ids_trn_tdeviationmaildocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                    " and b.user_gid = c.user_gid and ( customer2sanction_gid='" + employee_gid + "') and delete_flag='N'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mailfilename = new List<DeviationMail_DocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_mailfilename.Add(new DeviationMail_DocumentList
                    {
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_gid = (dr_datarow["maildocument_gid"].ToString()),
                        document_type = dr_datarow["document_type"].ToString(),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        updated_date = dr_datarow["uploaded_date"].ToString()
                    });
                }
                values.DeviationMail_DocumentList = get_mailfilename;
            }
        }

        public void DaMaildocument_delete(string document_gid, UploadDocumentname values, string employee_gid)
        {
            msSQL = "update ids_trn_tdeviationmaildocument set delete_flag='Y', updated_by='" + employee_gid + "'  where maildocument_gid='" + document_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Document deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occrued while deleting document";
                values.status = false;
            }
        }

        public void DaGetuploadesdocumentadd_delete(string document_gid, UploadDocumentname values, string employee_gid)
        {
            msSQL = "delete from ids_trn_tuploadesdeclarationdocument where esdeclaration_gid='" + document_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Document deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occrued while deleting document";
                values.status = false;
            }
        }

        public void DaMaildocumentadd_delete(string document_gid, UploadDocumentname values, string employee_gid)
        {
            msSQL = "delete from ids_trn_tdeviationmaildocument where maildocument_gid='" + document_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Document deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occrued while deleting document";
                values.status = false;
            }
        }
        
        public void DaUpdateCheckerApproval(template_list values, string employee_gid)
        {
            msSQL = " update ocs_mst_tcustomer2sanction set checkerapproval_flag='Y', sanction_status='" + values.sanction_status + "', checkerapproved_by='" + employee_gid + "',";
            if (values.reject_remarks == "" || values.reject_remarks == null || values.reject_remarks == "undefined")
            {
                msSQL += " checkerreject_remarks='', ";
            }
            else
            {
                msSQL += " checkerreject_remarks='" + values.reject_remarks.Replace("'", "") + "', ";
            }
            msSQL += " checkerapproved_on='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where customer2sanction_gid='" + values.sanction_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = " select template_name, template_gid, template_content from ocs_mst_tcustomer2sanction where customer2sanction_gid='" + values.sanction_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    values.template_name = objODBCDataReader["template_name"].ToString();
                    values.template_gid = objODBCDataReader["template_gid"].ToString();
                    values.template_content = objODBCDataReader["template_content"].ToString();
                }
                objODBCDataReader.Close();

                msGetGid = objcmnfunctions.GetMasterGID("SLAL");
                msSQL = "insert into ids_trn_tsanctionapprovallog(" +
                    " sanctionapprovallog_gid, " +
                    " sanction_gid," +
                    " template_gid, " +
                    " template_name, " +
                    " template_content, " +
                    " sanctionletter_flag," +
                    " sanction_status," +
                    " checkerpushback_remarks," +
                    " checkerreject_remarks," +
                    " created_by," +
                    " created_date)" +
                    " values (" +
                    "'" + msGetGid + "'," +
                    "'" + values.sanction_gid + "'," +
                    "'" + values.template_gid + "'," +
                    "'" + values.template_name + "'," +
                    "'" + values.template_content.Replace("'", "''") + "'," +
                    "'Y'," +
                    "'" + values.sanction_status + "'," +
                    "'',";
                if (values.reject_remarks == "" || values.reject_remarks == null || values.reject_remarks == "undefined")
                {
                    msSQL += " '', ";
                }
                else
                {
                    msSQL += "'" + values.reject_remarks.Replace("'", "") + "',";
                }
                msSQL += "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (values.sanction_status == "Approved")
                {
                    values.message = "Checker Approved Successfully";
                    values.status = true;
                }
                else
                {
                    values.message = "Checker Rejected Successfully";
                    values.status = true;
                }
            }
            else
            {
                values.message = "Error Occrued";
                values.status = false;
            }
        }

        public bool DaGetTemplate_list(mdltemplate values)
        {
            msSQL = " select a.template_gid,template_name from adm_mst_ttemplate a" +
                    " left join adm_trn_ttemplate2module b on a.template_gid = b.template_gid where template_name like '%Sanction%'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gettemplatelist = new List<template_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    gettemplatelist.Add(new template_list
                    {
                        template_gid = (dr_datarow["template_gid"].ToString()),
                        template_name = (dr_datarow["template_name"].ToString()),
                    });
                }
                values.template_list = gettemplatelist;
            }
            dt_datatable.Dispose();


            values.status = true;
            return true;
        }

        public bool DaSanctionContent(template_list values)
        {
            msSQL = " update ocs_mst_tcustomer2sanction set template_name='" + values.template_name + "', template_gid='" + values.template_gid + "' where customer2sanction_gid='" + values.sanction_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //Get Template Content
            msSQL = " select  a.template_content from adm_mst_ttemplate a " +
              " where a.template_gid='" + values.template_gid + "'";
            lstemplate_content = objdbconn.GetExecuteScalar(msSQL);

            lscontent = lstemplate_content;

            if (values.lstab == "pending")
            {
                msSQL = " select a.sanction_refno, a.customer_name, b.contactperson, b.mobileno, b.mobileno, b.email, b.address, b.relationship_manager,"+
                        " b.relationshipmgmt_name, c.employee_mobileno, c.employee_emailid" +
                        " from ocs_mst_tcustomer2sanction a " +
                        " LEFT JOIN ocs_mst_tcustomer b ON a.customer_gid = b.customer_gid " +
                        " LEFT JOIN hrm_mst_temployee c ON c.employee_gid = b.relationship_manager " +
                        " where customer2sanction_gid='" + values.sanction_gid + "'";
                objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader1.HasRows == true)
                {
                    objODBCDataReader1.Read();
                    values.sanction_refno = objODBCDataReader1["sanction_refno"].ToString();
                    values.customer_name = objODBCDataReader1["customer_name"].ToString();
                    values.address = objODBCDataReader1["address"].ToString();
                    values.mobileno = objODBCDataReader1["mobileno"].ToString();
                    values.email = objODBCDataReader1["email"].ToString();
                    values.contactperson = objODBCDataReader1["contactperson"].ToString();
                    values.relationshipmgmt_name = objODBCDataReader1["relationshipmgmt_name"].ToString();
                    values.employee_mobileno = objODBCDataReader1["employee_mobileno"].ToString();
                    values.employee_mailid = objODBCDataReader1["employee_emailid"].ToString();
                }
                objODBCDataReader1.Close();

                lscontent = lscontent.Replace("contact_person", values.contactperson);
                lscontent = lscontent.Replace("mobile_no", values.mobileno + ",");
                lscontent = lscontent.Replace("address", values.address);
                lscontent = lscontent.Replace("email", values.email);
                lscontent = lscontent.Replace("customer_name", values.customer_name);
                lscontent = lscontent.Replace("sanction_ref_no", values.sanction_refno);
                lscontent = lscontent.Replace("relationshipmgmt_name", values.relationshipmgmt_name);
                lscontent = lscontent.Replace("employee_mobileno", values.employee_mobileno);
                lscontent = lscontent.Replace("employee_mailid", values.employee_mailid);

                lscontent = lscontent.Replace("validity_months","");
                lscontent = lscontent.Replace("ccapproved_date", "");
                lscontent = lscontent.Replace("revolving_type", "");
                lscontent = lscontent.Replace("tenure", "");
                lscontent = lscontent.Replace("loanfacility_type", "");
                lscontent = lscontent.Replace("proposed_roi", "");
                lscontent = lscontent.Replace("loanfacility_amount", "");
                lscontent = lscontent.Replace("margin", "");
                lscontent = lscontent.Replace("purpose_lending", "");
                lscontent = lscontent.Replace("interest_amount", "");
                lscontent = lscontent.Replace("facilityamount_words", "");
                lscontent = lscontent.Replace("addoncharge", "");
                lscontent = lscontent.Replace("addonwords", "");

                values.template_content = lscontent;
            }
            else
            {
                msSQL = " select a.sanction_refno, a.customer_name, date_format(a.ccapproved_date,'%d-%m-%Y') as ccapproved_date, a.validity_months , " +
                    " b.contactperson, b.mobileno, b.mobileno, b.email, b.address, a.purpose_lending, b.relationship_manager,b.relationshipmgmt_name, c.employee_mobileno, c.employee_emailid" +
                    " from ocs_mst_tcustomer2sanction a " +
                    " LEFT JOIN ocs_mst_tcustomer b ON a.customer_gid = b.customer_gid " +
                    " LEFT JOIN hrm_mst_temployee c ON c.employee_gid = b.relationship_manager " +
                    " where customer2sanction_gid='" + values.sanction_gid + "'";
                objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader1.HasRows == true)
                {
                    objODBCDataReader1.Read();
                    values.sanction_refno = objODBCDataReader1["sanction_refno"].ToString();
                    values.customer_name = objODBCDataReader1["customer_name"].ToString();
                    values.ccapproved_date = objODBCDataReader1["ccapproved_date"].ToString();
                    values.address = objODBCDataReader1["address"].ToString();
                    values.mobileno = objODBCDataReader1["mobileno"].ToString();
                    values.email = objODBCDataReader1["email"].ToString();
                    values.contactperson = objODBCDataReader1["contactperson"].ToString();
                    values.purpose_lending = objODBCDataReader1["purpose_lending"].ToString();
                    values.validity_months = objODBCDataReader1["validity_months"].ToString();
                    values.relationshipmgmt_name = objODBCDataReader1["relationshipmgmt_name"].ToString();
                    values.employee_mobileno = objODBCDataReader1["employee_mobileno"].ToString();
                    values.employee_mailid = objODBCDataReader1["employee_emailid"].ToString();
                }
                
                lscontent = lscontent.Replace("contact_person", values.contactperson);
                lscontent = lscontent.Replace("mobile_no", values.mobileno + ",");
                lscontent = lscontent.Replace("address", values.address);
                lscontent = lscontent.Replace("ccapproved_date", values.ccapproved_date);
                lscontent = lscontent.Replace("email", values.email);
                lscontent = lscontent.Replace("customer_name", values.customer_name);
                lscontent = lscontent.Replace("sanction_ref_no", values.sanction_refno);
                lscontent = lscontent.Replace("validity_months", values.validity_months);
                lscontent = lscontent.Replace("relationshipmgmt_name", values.relationshipmgmt_name);
                lscontent = lscontent.Replace("employee_mobileno", values.employee_mobileno);
                lscontent = lscontent.Replace("employee_mailid", values.employee_mailid);

                msSQL = "select sanction2loanfacilitytype_gid,loanfacility_gid,format(loanfacility_amount,2) as loanfacility_amount,loanfacility_type, " +
                   " format(document_limit,2) as document_limit,margin,date_format(expiry_date, '%d-%m-%Y') as expiry_date,revolving_type,tenure, " +
                   " interchangeability,loanfacilityref_no,SUBSTRING_INDEX(proposed_roi, '%', 1) proposed_roi" +
                   " from ocs_mst_tsanction2loanfacilitytype  where customer2sanction_gid='" + values.sanction_gid + "'";
                objODBCDataReader2 = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader2.HasRows == true)
                {
                    objODBCDataReader1.Read();
                    values.loanfacility_type = objODBCDataReader2["loanfacility_type"].ToString();
                    values.document_limit = objODBCDataReader2["document_limit"].ToString();
                    values.revolving_type = objODBCDataReader2["revolving_type"].ToString();
                    values.tenure = objODBCDataReader2["tenure"].ToString();
                    values.loanfacility_amount = objODBCDataReader2["loanfacility_amount"].ToString();
                    values.proposed_roi = objODBCDataReader2["proposed_roi"].ToString();
                    values.margin = objODBCDataReader2["margin"].ToString();
                }

                double proposed_roi = Convert.ToDouble(values.proposed_roi);
                double loanfacility_amount = Convert.ToDouble(values.loanfacility_amount);


                double interest_amount = (loanfacility_amount * (proposed_roi / 100));
                int interestamount = Convert.ToInt32(interest_amount);
                values.interest_amount = Convert.ToString(interest_amount);

                double addoncharge = (loanfacility_amount * 1 / 100);
                int addon_charge = Convert.ToInt32(addoncharge);
                values.interest_amount = Convert.ToString(interest_amount);
                values.addoncharge = Convert.ToString(addoncharge);

                int facilityamount = Convert.ToInt32(loanfacility_amount);

                string interest_words = objcmnfunctions.NumberToWords(interestamount);
                string facilityamount_words = objcmnfunctions.NumberToWords(facilityamount);
                string addonwords = objcmnfunctions.NumberToWords(addon_charge);

                lscontent = lscontent.Replace("revolving_type", values.revolving_type);
                lscontent = lscontent.Replace("tenure", values.tenure);
                lscontent = lscontent.Replace("loanfacility_type", values.loanfacility_type);
                lscontent = lscontent.Replace("proposed_roi", values.proposed_roi);
                lscontent = lscontent.Replace("loanfacility_amount", values.loanfacility_amount);
                lscontent = lscontent.Replace("margin", values.margin);
                lscontent = lscontent.Replace("purpose_lending", values.purpose_lending);
                lscontent = lscontent.Replace("interest_amount", values.interest_amount);
                lscontent = lscontent.Replace("facilityamount_words", facilityamount_words);
                lscontent = lscontent.Replace("addoncharge", values.addoncharge);
                lscontent = lscontent.Replace("addonwords", addonwords);

                values.template_content = lscontent;
                objODBCDataReader2.Close();
                objODBCDataReader1.Close();
            }
            values.status = true;
            return true;

        }

        public bool DaPostTemplateSanction2Facility(template_list values)
        {
            msSQL = " update ocs_mst_tcustomer2sanction set template_name='" + values.template_name + "', template_gid='" + values.template_gid + "' where customer2sanction_gid='" + values.sanction_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //Get Template Content
            msSQL = " select  a.template_content from adm_mst_ttemplate a " +
              " where a.template_gid='" + values.template_gid + "'";
            lstemplate_content = objdbconn.GetExecuteScalar(msSQL);

            lscontent = lstemplate_content;
            if(values.lstab == "pending")
            {
                msSQL = " select a.sanction_refno, a.customer_name, b.contactperson, b.mobileno, b.mobileno, b.email, b.address, b.relationship_manager,"+
                        " b.relationshipmgmt_name, c.employee_mobileno, c.employee_emailid " +
                        " from ocs_mst_tcustomer2sanction a " +
                        " LEFT JOIN ocs_mst_tcustomer b ON a.customer_gid = b.customer_gid " +
                        " LEFT JOIN hrm_mst_temployee c ON c.employee_gid = b.relationship_manager " +
                        " where customer2sanction_gid='" + values.sanction_gid + "'";
                objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader1.HasRows == true)
                {
                    objODBCDataReader1.Read();
                    values.sanction_refno = objODBCDataReader1["sanction_refno"].ToString();
                    values.customer_name = objODBCDataReader1["customer_name"].ToString();
                    values.address = objODBCDataReader1["address"].ToString();
                    values.mobileno = objODBCDataReader1["mobileno"].ToString();
                    values.email = objODBCDataReader1["email"].ToString();
                    values.contactperson = objODBCDataReader1["contactperson"].ToString();
                    values.relationshipmgmt_name = objODBCDataReader1["relationshipmgmt_name"].ToString();
                    values.employee_mobileno = objODBCDataReader1["employee_mobileno"].ToString();
                    values.employee_mailid = objODBCDataReader1["employee_emailid"].ToString();
                }
                objODBCDataReader1.Close();
                lscontent = lscontent.Replace("contact_person", values.contactperson);
                lscontent = lscontent.Replace("mobile_no", values.mobileno + ",");
                lscontent = lscontent.Replace("address", values.address);
                lscontent = lscontent.Replace("email", values.email);
                lscontent = lscontent.Replace("customer_name", values.customer_name);
                lscontent = lscontent.Replace("sanction_ref_no", values.sanction_refno);
                lscontent = lscontent.Replace("relationshipmgmt_name", values.relationshipmgmt_name);
                lscontent = lscontent.Replace("employee_mobileno", values.employee_mobileno);
                lscontent = lscontent.Replace("employee_mailid", values.employee_mailid);
                
                lscontent = lscontent.Replace("revolving_type", "");
                lscontent = lscontent.Replace("tenure1", "");
                lscontent = lscontent.Replace("tenure2", "");
                lscontent = lscontent.Replace("loanfacility_type1", "");
                lscontent = lscontent.Replace("loanfacility_type2", "");
                lscontent = lscontent.Replace("proposed_roi", "");
                lscontent = lscontent.Replace("loanfacility_amount1", "");
                lscontent = lscontent.Replace("loanfacility_amount2", "");
                lscontent = lscontent.Replace("margin1", "");
                lscontent = lscontent.Replace("margin2", "");
                lscontent = lscontent.Replace("purpose_lending", "");
                lscontent = lscontent.Replace("interest_amount", "");
                lscontent = lscontent.Replace("facilityamount1_words", "");
                lscontent = lscontent.Replace("facilityamount2_words", "");

                values.template_content = lscontent;
            }
            else
            {
                msSQL = " select a.sanction_refno, a.customer_name, date_format(a.ccapproved_date,'%d-%m-%Y') as ccapproved_date, a.validity_months , " +
               " b.contactperson, b.mobileno, b.mobileno, b.email, b.address, a.purpose_lending,  b.relationship_manager,b.relationshipmgmt_name, c.employee_mobileno, c.employee_emailid " +
               " from ocs_mst_tcustomer2sanction a " +
               " LEFT JOIN ocs_mst_tcustomer b ON a.customer_gid = b.customer_gid " +
               " LEFT JOIN hrm_mst_temployee c ON c.employee_gid = b.relationship_manager " +
                     " where customer2sanction_gid='" + values.sanction_gid + "'";
                objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader1.HasRows == true)
                {
                    objODBCDataReader1.Read();
                    values.sanction_refno = objODBCDataReader1["sanction_refno"].ToString();
                    values.customer_name = objODBCDataReader1["customer_name"].ToString();
                    values.ccapproved_date = objODBCDataReader1["ccapproved_date"].ToString();
                    values.address = objODBCDataReader1["address"].ToString();
                    values.mobileno = objODBCDataReader1["mobileno"].ToString();
                    values.email = objODBCDataReader1["email"].ToString();
                    values.contactperson = objODBCDataReader1["contactperson"].ToString();
                    values.purpose_lending = objODBCDataReader1["purpose_lending"].ToString();
                    values.validity_months = objODBCDataReader1["validity_months"].ToString();
                    values.relationshipmgmt_name = objODBCDataReader1["relationshipmgmt_name"].ToString();
                    values.employee_mobileno = objODBCDataReader1["employee_mobileno"].ToString();
                    values.employee_mailid = objODBCDataReader1["employee_emailid"].ToString();
                }
                
                lscontent = lscontent.Replace("contact_person", values.contactperson);
                lscontent = lscontent.Replace("mobile_no", values.mobileno + ",");
                lscontent = lscontent.Replace("address", values.address);
                lscontent = lscontent.Replace("ccapproved_date", values.ccapproved_date);
                lscontent = lscontent.Replace("email", values.email);
                lscontent = lscontent.Replace("customer_name", values.customer_name);
                lscontent = lscontent.Replace("sanction_ref_no", values.sanction_refno);
                lscontent = lscontent.Replace("validity_months", values.validity_months);
                lscontent = lscontent.Replace("relationshipmgmt_name", values.relationshipmgmt_name);
                lscontent = lscontent.Replace("employee_mobileno", values.employee_mobileno);
                lscontent = lscontent.Replace("employee_mailid", values.employee_mailid);
                
                msSQL = "select sanction2loanfacilitytype_gid,loanfacility_gid,format(loanfacility_amount,2) as loanfacility_amount,loanfacility_type, " +
                   " format(document_limit,2) as document_limit,margin,revolving_type,tenure, SUBSTRING_INDEX(proposed_roi, '%', 1) proposed_roi," +
                   " interchangeability" +
                   " from ocs_mst_tsanction2loanfacilitytype  where customer2sanction_gid='" + values.sanction_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getloanfacilitytype = new List<loanfacilitytype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getloanfacilitytype.Add(new loanfacilitytype_list
                        {
                            sanction2loanfacilitytype_gid = (dr_datarow["sanction2loanfacilitytype_gid"].ToString()),
                            loanfacility_gid = (dr_datarow["loanfacility_gid"].ToString()),
                            loanfacility_amount = (dr_datarow["loanfacility_amount"].ToString()),
                            loanfacility_type = (dr_datarow["loanfacility_type"].ToString()),
                            document_limit = (dr_datarow["document_limit"].ToString()),
                            revolving_type = (dr_datarow["revolving_type"].ToString()),
                            tenure = (dr_datarow["tenure"].ToString()),
                            margin = (dr_datarow["margin"].ToString()),
                            interchangeability = (dr_datarow["interchangeability"].ToString()),
                            proposed_roi = (dr_datarow["proposed_roi"].ToString()),

                        });
                    }
                    values.loanfacilitytype_list = getloanfacilitytype;
                }
                dt_datatable.Dispose();
                
                String proposedroi1 = values.loanfacilitytype_list[0].proposed_roi;
                String proposedroi2 = values.loanfacilitytype_list[1].proposed_roi;
                String loanfacilityamount1 = values.loanfacilitytype_list[0].loanfacility_amount;
                String loanfacilityamount2 = values.loanfacilitytype_list[1].loanfacility_amount;
                
                double proposed_roi1 = Convert.ToDouble(proposedroi1);
                double loanfacility_amount1 = Convert.ToDouble(loanfacilityamount1);
                double loanfacility_amount2 = Convert.ToDouble(loanfacilityamount2);
                
                double interest_amount = ((loanfacility_amount1 + loanfacility_amount2) * (proposed_roi1 / 100));
                int interestamount = Convert.ToInt32(interest_amount);
                values.interest_amount = Convert.ToString(interest_amount);

                double addoncharge = ((loanfacility_amount1 + loanfacility_amount2) * 1 / 100);
                int addon_charge = Convert.ToInt32(addoncharge);
                values.interest_amount = Convert.ToString(interest_amount);

                int facilityamount1 = Convert.ToInt32(loanfacility_amount1);
                int facilityamount2 = Convert.ToInt32(loanfacility_amount2);

                string interest_words = objcmnfunctions.NumberToWords(interestamount);
                string facilityamount1_words = objcmnfunctions.NumberToWords(facilityamount1);
                string facilityamount2_words = objcmnfunctions.NumberToWords(facilityamount2);

                lscontent = lscontent.Replace("revolving_type1", values.loanfacilitytype_list[0].revolving_type);
                lscontent = lscontent.Replace("revolving_type2", values.loanfacilitytype_list[1].revolving_type);
                lscontent = lscontent.Replace("tenure1", values.loanfacilitytype_list[0].tenure);
                lscontent = lscontent.Replace("tenure2", values.loanfacilitytype_list[1].tenure);
                lscontent = lscontent.Replace("loanfacility_type1", values.loanfacilitytype_list[0].loanfacility_type);
                lscontent = lscontent.Replace("loanfacility_type2", values.loanfacilitytype_list[1].loanfacility_type);
                lscontent = lscontent.Replace("proposed_roi", values.loanfacilitytype_list[1].proposed_roi);
                lscontent = lscontent.Replace("loanfacility_amount1", values.loanfacilitytype_list[0].loanfacility_amount);
                lscontent = lscontent.Replace("loanfacility_amount2", values.loanfacilitytype_list[1].loanfacility_amount);
                lscontent = lscontent.Replace("margin1", values.loanfacilitytype_list[0].margin);
                lscontent = lscontent.Replace("margin2", values.loanfacilitytype_list[1].margin);
                lscontent = lscontent.Replace("purpose_lending", values.purpose_lending);
                lscontent = lscontent.Replace("interest_amount", values.interest_amount);
                lscontent = lscontent.Replace("facilityamount1_words", facilityamount1_words);
                lscontent = lscontent.Replace("facilityamount2_words", facilityamount2_words);

                values.template_content = lscontent;
                objODBCDataReader1.Close();
            }
            values.status = true;
            return true;

        }

        public bool DaPostTemplateSanctionStandbyLine(template_list values)
        {
            msSQL = " update ocs_mst_tcustomer2sanction set template_name='" + values.template_name + "', template_gid='" + values.template_gid + "' where customer2sanction_gid='" + values.sanction_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //Get Template Content
            msSQL = " select  a.template_content from adm_mst_ttemplate a " +
              " where a.template_gid='" + values.template_gid + "'";
            lstemplate_content = objdbconn.GetExecuteScalar(msSQL);

            lscontent = lstemplate_content;

            if(values.lstab == "pending")
            {
                msSQL = " select a.sanction_refno, a.customer_name, b.contactperson, b.mobileno, b.mobileno, b.email, b.address" +
                        " from ocs_mst_tcustomer2sanction a " +
                        " LEFT JOIN ocs_mst_tcustomer b on a.customer_gid = b.customer_gid " +
                        " where customer2sanction_gid='" + values.sanction_gid + "'";
                objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader1.HasRows == true)
                {
                    objODBCDataReader1.Read();
                    values.sanction_refno = objODBCDataReader1["sanction_refno"].ToString();
                    values.customer_name = objODBCDataReader1["customer_name"].ToString();
                    values.address = objODBCDataReader1["address"].ToString();
                    values.mobileno = objODBCDataReader1["mobileno"].ToString();
                    values.email = objODBCDataReader1["email"].ToString();
                    values.contactperson = objODBCDataReader1["contactperson"].ToString();
                }
                objODBCDataReader1.Close();
                lscontent = lscontent.Replace("contact_person", values.contactperson);
                lscontent = lscontent.Replace("mobile_no", values.mobileno + ",");
                lscontent = lscontent.Replace("address", values.address);
                lscontent = lscontent.Replace("email", values.email);
                lscontent = lscontent.Replace("customer_name", values.customer_name);
                lscontent = lscontent.Replace("sanction_ref_no", values.sanction_refno);
                
                lscontent = lscontent.Replace("ccapproved_date", "");
                lscontent = lscontent.Replace("validity_months", "");
                lscontent = lscontent.Replace("revolving_type", "");
                lscontent = lscontent.Replace("tenure", "");
                lscontent = lscontent.Replace("loanfacility_type", "");
                lscontent = lscontent.Replace("proposed_roi", "");
                lscontent = lscontent.Replace("loanfacility_amount", "");
                lscontent = lscontent.Replace("margin", "");
                lscontent = lscontent.Replace("purpose_lending", "");
                lscontent = lscontent.Replace("interest_amount", "");
                lscontent = lscontent.Replace("facilityamount_words", "");

                values.template_content = lscontent;
            }
            else
            {
                msSQL = " select a.sanction_refno, a.customer_name, date_format(a.ccapproved_date,'%d-%m-%Y') as ccapproved_date, a.validity_months , " +
               " b.contactperson, b.mobileno, b.mobileno, b.email, b.address, a.purpose_lending " +
               " from ocs_mst_tcustomer2sanction a " +
               " LEFT JOIN ocs_mst_tcustomer b on a.customer_gid = b.customer_gid " +
                     " where customer2sanction_gid='" + values.sanction_gid + "'";
                objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader1.HasRows == true)
                {
                    objODBCDataReader1.Read();
                    values.sanction_refno = objODBCDataReader1["sanction_refno"].ToString();
                    values.customer_name = objODBCDataReader1["customer_name"].ToString();
                    values.ccapproved_date = objODBCDataReader1["ccapproved_date"].ToString();
                    values.address = objODBCDataReader1["address"].ToString();
                    values.mobileno = objODBCDataReader1["mobileno"].ToString();
                    values.email = objODBCDataReader1["email"].ToString();
                    values.contactperson = objODBCDataReader1["contactperson"].ToString();
                    values.purpose_lending = objODBCDataReader1["purpose_lending"].ToString();
                    values.validity_months = objODBCDataReader1["validity_months"].ToString();
                }
                
                lscontent = lscontent.Replace("contact_person", values.contactperson);
                lscontent = lscontent.Replace("mobile_no", values.mobileno + ",");
                lscontent = lscontent.Replace("address", values.address);
                lscontent = lscontent.Replace("ccapproved_date", values.ccapproved_date);
                lscontent = lscontent.Replace("email", values.email);
                lscontent = lscontent.Replace("customer_name", values.customer_name);
                lscontent = lscontent.Replace("sanction_ref_no", values.sanction_refno);
                lscontent = lscontent.Replace("validity_months", values.validity_months);

                msSQL = "select sanction2loanfacilitytype_gid,loanfacility_gid,format(loanfacility_amount,2) as loanfacility_amount,loanfacility_type, " +
                   " format(document_limit,2) as document_limit,margin,date_format(expiry_date, '%d-%m-%Y') as expiry_date,revolving_type,tenure," +
                   " interchangeability,if(report_structure='','---',report_structure) as report_structure,loanfacilityref_no,proposed_roi" +
                   " from ocs_mst_tsanction2loanfacilitytype  where customer2sanction_gid='" + values.sanction_gid + "'";
                objODBCDataReader2 = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader2.HasRows == true)
                {
                    objODBCDataReader1.Read();
                    values.loanfacility_type = objODBCDataReader2["loanfacility_type"].ToString();
                    values.document_limit = objODBCDataReader2["document_limit"].ToString();
                    values.revolving_type = objODBCDataReader2["revolving_type"].ToString();
                    values.tenure = objODBCDataReader2["tenure"].ToString();
                    values.loanfacility_amount = objODBCDataReader2["loanfacility_amount"].ToString();
                    values.proposed_roi = objODBCDataReader2["proposed_roi"].ToString();
                    values.margin = objODBCDataReader2["margin"].ToString();
                }
                String proposedroi = values.proposed_roi;

                string p1 = proposedroi.Substring(0, 2);

                double proposed_roi = Convert.ToDouble(p1);
                double loanfacility_amount = Convert.ToDouble(values.loanfacility_amount);
                
                double interest_amount = (loanfacility_amount * (proposed_roi / 100));
                int interestamount = Convert.ToInt32(interest_amount);
                values.interest_amount = Convert.ToString(interest_amount);

                double addoncharge = (loanfacility_amount * (1 / 100));
                int addon_charge = Convert.ToInt32(addoncharge);
                values.interest_amount = Convert.ToString(interest_amount);

                int facilityamount = Convert.ToInt32(loanfacility_amount);

                string interest_words = objcmnfunctions.NumberToWords(interestamount);
                string facilityamount_words = objcmnfunctions.NumberToWords(facilityamount);

                lscontent = lscontent.Replace("revolving_type", values.revolving_type);
                lscontent = lscontent.Replace("tenure", values.tenure);
                lscontent = lscontent.Replace("loanfacility_type", values.loanfacility_type);
                lscontent = lscontent.Replace("proposed_roi", values.proposed_roi);
                lscontent = lscontent.Replace("loanfacility_amount", values.loanfacility_amount);
                lscontent = lscontent.Replace("margin", values.margin);
                lscontent = lscontent.Replace("purpose_lending", values.purpose_lending);
                lscontent = lscontent.Replace("interest_amount", values.interest_amount);
                lscontent = lscontent.Replace("facilityamount_words", values.facilityamount_words);

                values.template_content = lscontent;
                objODBCDataReader2.Close();
                objODBCDataReader1.Close();
            }
            values.status = true;
            return true;
        }

        public bool DaPostTemplateSanctionMultipleFacility(template_list values)
        {
            msSQL = " update ocs_mst_tcustomer2sanction set template_name='" + values.template_name + "', template_gid='" + values.template_gid + "' where customer2sanction_gid='" + values.sanction_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //Get Template Content
            msSQL = " select  a.template_content from adm_mst_ttemplate a " +
              " where a.template_gid='" + values.template_gid + "'";
            lstemplate_content = objdbconn.GetExecuteScalar(msSQL);

            lscontent = lstemplate_content;
            if (values.lstab == "pending")
            {
                msSQL = " select a.sanction_refno, a.customer_name, b.contactperson, b.mobileno, b.mobileno, b.email, b.address " +
                        " from ocs_mst_tcustomer2sanction a " +
                        " LEFT JOIN ocs_mst_tcustomer b on a.customer_gid = b.customer_gid " +
                        " where customer2sanction_gid='" + values.sanction_gid + "'";
                objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader1.HasRows == true)
                {
                    objODBCDataReader1.Read();
                    values.sanction_refno = objODBCDataReader1["sanction_refno"].ToString();
                    values.customer_name = objODBCDataReader1["customer_name"].ToString();
                    values.address = objODBCDataReader1["address"].ToString();
                    values.mobileno = objODBCDataReader1["mobileno"].ToString();
                    values.email = objODBCDataReader1["email"].ToString();
                    values.contactperson = objODBCDataReader1["contactperson"].ToString();
                }
                objODBCDataReader1.Close();
                lscontent = lscontent.Replace("contact_person", values.contactperson);
                lscontent = lscontent.Replace("mobile_no", values.mobileno + ",");
                lscontent = lscontent.Replace("address", values.address);
                lscontent = lscontent.Replace("email", values.email);
                lscontent = lscontent.Replace("customer_name", values.customer_name);
                lscontent = lscontent.Replace("sanction_ref_no", values.sanction_refno);
                
                lscontent = lscontent.Replace("ccapproved_date", "");
                lscontent = lscontent.Replace("validity_months", "");
                lscontent = lscontent.Replace("revolving_type1", "");
                lscontent = lscontent.Replace("revolving_type2", "");
                lscontent = lscontent.Replace("revolving_type3", "");
                lscontent = lscontent.Replace("tenure1", "");
                lscontent = lscontent.Replace("tenure2", "");
                lscontent = lscontent.Replace("tenure3", "");
                lscontent = lscontent.Replace("loanfacility_type1", "");
                lscontent = lscontent.Replace("loanfacility_type2", "");
                lscontent = lscontent.Replace("loanfacility_type3", "");
                lscontent = lscontent.Replace("proposed_roi1", "");
                lscontent = lscontent.Replace("proposed_roi2", "");
                lscontent = lscontent.Replace("proposed_roi3", "");
                lscontent = lscontent.Replace("loanfacility_amount1", "");
                lscontent = lscontent.Replace("loanfacility_amount2", "");
                lscontent = lscontent.Replace("loanfacility_amount3", "");
                lscontent = lscontent.Replace("margin1", "");
                lscontent = lscontent.Replace("margin2", "");
                lscontent = lscontent.Replace("margin3", "");
                lscontent = lscontent.Replace("purpose_lending", "");
                lscontent = lscontent.Replace("interest_amount", "");
                lscontent = lscontent.Replace("facilityamount1_words", "");
                lscontent = lscontent.Replace("facilityamount2_words", "");

                values.template_content = lscontent;
            }
            else
            {
                msSQL = " select a.sanction_refno, a.customer_name, date_format(a.ccapproved_date,'%d-%m-%Y') as ccapproved_date, a.validity_months , b.contactperson," +
                        " b.mobileno, b.mobileno, b.email, b.address, a.purpose_lending, b.relationship_manager,b.relationshipmgmt_name, c.employee_mobileno, c.employee_emailid " +
                        " from ocs_mst_tcustomer2sanction a " +
                        " LEFT JOIN ocs_mst_tcustomer b on a.customer_gid = b.customer_gid " +
                        " LEFT JOIN hrm_mst_temployee c ON c.employee_gid = b.relationship_manager " +
                        " where customer2sanction_gid='" + values.sanction_gid + "'";
                objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader1.HasRows == true)
                {
                    objODBCDataReader1.Read();
                    values.sanction_refno = objODBCDataReader1["sanction_refno"].ToString();
                    values.customer_name = objODBCDataReader1["customer_name"].ToString();
                    values.ccapproved_date = objODBCDataReader1["ccapproved_date"].ToString();
                    values.address = objODBCDataReader1["address"].ToString();
                    values.mobileno = objODBCDataReader1["mobileno"].ToString();
                    values.email = objODBCDataReader1["email"].ToString();
                    values.contactperson = objODBCDataReader1["contactperson"].ToString();
                    values.purpose_lending = objODBCDataReader1["purpose_lending"].ToString();
                    values.validity_months = objODBCDataReader1["validity_months"].ToString();
                    values.relationshipmgmt_name = objODBCDataReader1["relationshipmgmt_name"].ToString();
                    values.employee_mobileno = objODBCDataReader1["employee_mobileno"].ToString();
                    values.employee_mailid = objODBCDataReader1["employee_emailid"].ToString();
                }
                
                lscontent = lscontent.Replace("contact_person", values.contactperson);
                lscontent = lscontent.Replace("mobile_no", values.mobileno + ",");
                lscontent = lscontent.Replace("address", values.address);
                lscontent = lscontent.Replace("ccapproved_date", values.ccapproved_date);
                lscontent = lscontent.Replace("email", values.email);
                lscontent = lscontent.Replace("customer_name", values.customer_name);
                lscontent = lscontent.Replace("sanction_ref_no", values.sanction_refno);
                lscontent = lscontent.Replace("validity_months", values.validity_months);
                lscontent = lscontent.Replace("relationshipmgmt_name", values.relationshipmgmt_name);
                lscontent = lscontent.Replace("employee_mobileno", values.employee_mobileno);
                lscontent = lscontent.Replace("employee_mailid", values.employee_mailid);

                msSQL = "select sanction2loanfacilitytype_gid,loanfacility_gid,format(loanfacility_amount,2) as loanfacility_amount,loanfacility_type, " +
                   " format(document_limit,2) as document_limit,margin,revolving_type,tenure, SUBSTRING_INDEX(proposed_roi, '%', 1) proposed_roi," +
                   " interchangeability" +
                   " from ocs_mst_tsanction2loanfacilitytype  where customer2sanction_gid='" + values.sanction_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getloanfacilitytype = new List<loanfacilitytype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getloanfacilitytype.Add(new loanfacilitytype_list
                        {
                            sanction2loanfacilitytype_gid = (dr_datarow["sanction2loanfacilitytype_gid"].ToString()),
                            loanfacility_gid = (dr_datarow["loanfacility_gid"].ToString()),
                            loanfacility_amount = (dr_datarow["loanfacility_amount"].ToString()),
                            loanfacility_type = (dr_datarow["loanfacility_type"].ToString()),
                            document_limit = (dr_datarow["document_limit"].ToString()),
                            revolving_type = (dr_datarow["revolving_type"].ToString()),
                            tenure = (dr_datarow["tenure"].ToString()),
                            margin = (dr_datarow["margin"].ToString()),
                            interchangeability = (dr_datarow["interchangeability"].ToString()),
                            proposed_roi = (dr_datarow["proposed_roi"].ToString()),

                        });
                    }
                    values.loanfacilitytype_list = getloanfacilitytype;
                }
                dt_datatable.Dispose();
                
                String proposedroi1 = values.loanfacilitytype_list[0].proposed_roi;
                String proposedroi2 = values.loanfacilitytype_list[1].proposed_roi;
                String loanfacilityamount1 = values.loanfacilitytype_list[0].loanfacility_amount;
                String loanfacilityamount2 = values.loanfacilitytype_list[1].loanfacility_amount;
                String loanfacilityamount3 = values.loanfacilitytype_list[2].loanfacility_amount;


                double proposed_roi1 = Convert.ToDouble(proposedroi1);
                double loanfacility_amount1 = Convert.ToDouble(loanfacilityamount1);
                double loanfacility_amount2 = Convert.ToDouble(loanfacilityamount2);
                double loanfacility_amount3 = Convert.ToDouble(loanfacilityamount3);


                double interest_amount = ((loanfacility_amount1 + loanfacility_amount2) * (proposed_roi1 / 100));
                int interestamount = Convert.ToInt32(interest_amount);
                values.interest_amount = Convert.ToString(interest_amount);

                double addoncharge = ((loanfacility_amount1 + loanfacility_amount2) * 1 / 100);
                int addon_charge = Convert.ToInt32(addoncharge);
                values.interest_amount = Convert.ToString(interest_amount);

                int facilityamount1 = Convert.ToInt32(loanfacility_amount1);
                int facilityamount2 = Convert.ToInt32(loanfacility_amount2);

                string interest_words = objcmnfunctions.NumberToWords(interestamount);
                string facilityamount1_words = objcmnfunctions.NumberToWords(facilityamount1);
                string facilityamount2_words = objcmnfunctions.NumberToWords(facilityamount2);

                lscontent = lscontent.Replace("revolving_type1", values.loanfacilitytype_list[0].revolving_type);
                lscontent = lscontent.Replace("revolving_type2", values.loanfacilitytype_list[1].revolving_type);
                lscontent = lscontent.Replace("revolving_type3", values.loanfacilitytype_list[2].revolving_type);
                lscontent = lscontent.Replace("tenure1", values.loanfacilitytype_list[0].tenure);
                lscontent = lscontent.Replace("tenure2", values.loanfacilitytype_list[1].tenure);
                lscontent = lscontent.Replace("tenure3", values.loanfacilitytype_list[2].tenure);
                lscontent = lscontent.Replace("loanfacility_type1", values.loanfacilitytype_list[0].loanfacility_type);
                lscontent = lscontent.Replace("loanfacility_type2", values.loanfacilitytype_list[1].loanfacility_type);
                lscontent = lscontent.Replace("loanfacility_type3", values.loanfacilitytype_list[2].loanfacility_type);
                lscontent = lscontent.Replace("proposed_roi1", values.loanfacilitytype_list[0].proposed_roi);
                lscontent = lscontent.Replace("proposed_roi2", values.loanfacilitytype_list[1].proposed_roi);
                lscontent = lscontent.Replace("proposed_roi3", values.loanfacilitytype_list[2].proposed_roi);
                lscontent = lscontent.Replace("loanfacility_amount1", values.loanfacilitytype_list[0].loanfacility_amount);
                lscontent = lscontent.Replace("loanfacility_amount2", values.loanfacilitytype_list[1].loanfacility_amount);
                lscontent = lscontent.Replace("loanfacility_amount3", values.loanfacilitytype_list[2].loanfacility_amount);
                lscontent = lscontent.Replace("margin1", values.loanfacilitytype_list[0].margin);
                lscontent = lscontent.Replace("margin2", values.loanfacilitytype_list[1].margin);
                lscontent = lscontent.Replace("margin3", values.loanfacilitytype_list[2].margin);
                lscontent = lscontent.Replace("purpose_lending", values.purpose_lending);
                lscontent = lscontent.Replace("interest_amount", values.interest_amount);
                lscontent = lscontent.Replace("facilityamount1_words", facilityamount1_words);
                lscontent = lscontent.Replace("facilityamount2_words", facilityamount2_words);

                values.template_content = lscontent;
                objODBCDataReader1.Close();
            }
            values.status = true;
            return true;
        }

        public bool DaPostTemplateDBSColending(template_list values)
        {
            msSQL = " update ocs_mst_tcustomer2sanction set template_name='" + values.template_name + "', template_gid='" + values.template_gid + "' where customer2sanction_gid='" + values.sanction_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //Get Template Content
            msSQL = " select  a.template_content from adm_mst_ttemplate a " +
              " where a.template_gid='" + values.template_gid + "'";
            lstemplate_content = objdbconn.GetExecuteScalar(msSQL);

            lscontent = lstemplate_content;
            if(values.lstab == "pending")
            {
                msSQL = " select a.sanction_refno, a.customer_name, template_name, b.contactperson, b.mobileno, b.mobileno, b.email, b.address,"+
                        " b.relationship_manager,b.relationshipmgmt_name, c.employee_mobileno, c.employee_emailid" +
                        " from ocs_mst_tcustomer2sanction a " +
                        " LEFT JOIN ocs_mst_tcustomer b ON a.customer_gid = b.customer_gid " +
                        " LEFT JOIN hrm_mst_temployee c ON c.employee_gid = b.relationship_manager " +
                        " where customer2sanction_gid='" + values.sanction_gid + "'";
                objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader1.HasRows == true)
                {
                    objODBCDataReader1.Read();
                    values.sanction_refno = objODBCDataReader1["sanction_refno"].ToString();
                    values.customer_name = objODBCDataReader1["customer_name"].ToString();
                    values.address = objODBCDataReader1["address"].ToString();
                    values.mobileno = objODBCDataReader1["mobileno"].ToString();
                    values.email = objODBCDataReader1["email"].ToString();
                    values.contactperson = objODBCDataReader1["contactperson"].ToString();
                    values.relationshipmgmt_name = objODBCDataReader1["relationshipmgmt_name"].ToString();
                    values.employee_mobileno = objODBCDataReader1["employee_mobileno"].ToString();
                    values.employee_mailid = objODBCDataReader1["employee_emailid"].ToString();
                    values.template_name = objODBCDataReader1["template_name"].ToString();
                }
                objODBCDataReader1.Close();
                lscontent = lscontent.Replace("contact_person", values.contactperson);
                lscontent = lscontent.Replace("mobile_no", values.mobileno + ",");
                lscontent = lscontent.Replace("address", values.address);
                lscontent = lscontent.Replace("email", values.email);
                lscontent = lscontent.Replace("customer_name", values.customer_name);
                lscontent = lscontent.Replace("sanction_ref_no", values.sanction_refno);
                lscontent = lscontent.Replace("relationshipmgmt_name", values.relationshipmgmt_name);
                lscontent = lscontent.Replace("employee_mobileno", values.employee_mobileno);
                lscontent = lscontent.Replace("employee_mailid", values.employee_mailid);
                
                lscontent = lscontent.Replace("ccapproved_date", "");
                lscontent = lscontent.Replace("validity_months", "");
                lscontent = lscontent.Replace("revolving_type", "");
                lscontent = lscontent.Replace("tenure", "");
                lscontent = lscontent.Replace("loanfacility_type", "");
                lscontent = lscontent.Replace("proposed_roi", "");
                lscontent = lscontent.Replace("loanfacility_amount", "");
                lscontent = lscontent.Replace("margin", "");
                lscontent = lscontent.Replace("purpose_lending", "");
                lscontent = lscontent.Replace("interest_amount", "");
                lscontent = lscontent.Replace("facilityamount_words", "");
                lscontent = lscontent.Replace("addoncharge", "");
                lscontent = lscontent.Replace("addonwords", "");

                values.template_content = lscontent;
            }
            else
            {
                msSQL = " select a.sanction_refno, a.customer_name, date_format(a.ccapproved_date,'%d-%m-%Y') as ccapproved_date, a.validity_months , template_name," +
               " b.contactperson, b.mobileno, b.mobileno, b.email, b.address, a.purpose_lending, b.relationship_manager,b.relationshipmgmt_name, c.employee_mobileno, c.employee_emailid" +
               " from ocs_mst_tcustomer2sanction a " +
               " LEFT JOIN ocs_mst_tcustomer b ON a.customer_gid = b.customer_gid " +
               " LEFT JOIN hrm_mst_temployee c ON c.employee_gid = b.relationship_manager " +
               " where customer2sanction_gid='" + values.sanction_gid + "'";
                objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader1.HasRows == true)
                {
                    objODBCDataReader1.Read();
                    values.sanction_refno = objODBCDataReader1["sanction_refno"].ToString();
                    values.customer_name = objODBCDataReader1["customer_name"].ToString();
                    values.ccapproved_date = objODBCDataReader1["ccapproved_date"].ToString();
                    values.address = objODBCDataReader1["address"].ToString();
                    values.mobileno = objODBCDataReader1["mobileno"].ToString();
                    values.email = objODBCDataReader1["email"].ToString();
                    values.contactperson = objODBCDataReader1["contactperson"].ToString();
                    values.purpose_lending = objODBCDataReader1["purpose_lending"].ToString();
                    values.validity_months = objODBCDataReader1["validity_months"].ToString();
                    values.relationshipmgmt_name = objODBCDataReader1["relationshipmgmt_name"].ToString();
                    values.employee_mobileno = objODBCDataReader1["employee_mobileno"].ToString();
                    values.employee_mailid = objODBCDataReader1["employee_emailid"].ToString();
                    values.template_name = objODBCDataReader1["template_name"].ToString();
                }

                lscontent = lscontent.Replace("contact_person", values.contactperson);
                lscontent = lscontent.Replace("mobile_no", values.mobileno + ",");
                lscontent = lscontent.Replace("address", values.address);
                lscontent = lscontent.Replace("ccapproved_date", values.ccapproved_date);
                lscontent = lscontent.Replace("email", values.email);
                lscontent = lscontent.Replace("customer_name", values.customer_name);
                lscontent = lscontent.Replace("sanction_ref_no", values.sanction_refno);
                lscontent = lscontent.Replace("validity_months", values.validity_months);
                lscontent = lscontent.Replace("relationshipmgmt_name", values.relationshipmgmt_name);
                lscontent = lscontent.Replace("employee_mobileno", values.employee_mobileno);
                lscontent = lscontent.Replace("employee_mailid", values.employee_mailid);

                msSQL = "select sanction2loanfacilitytype_gid,loanfacility_gid,format(loanfacility_amount,2) as loanfacility_amount,loanfacility_type, " +
                   " format(document_limit,2) as document_limit,margin,date_format(expiry_date, '%d-%m-%Y') as expiry_date,revolving_type,tenure, " +
                   " interchangeability,loanfacilityref_no,SUBSTRING_INDEX(proposed_roi, '%', 1) proposed_roi" +
                   " from ocs_mst_tsanction2loanfacilitytype  where customer2sanction_gid='" + values.sanction_gid + "'";
                objODBCDataReader2 = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader2.HasRows == true)
                {
                    values.loanfacility_type = objODBCDataReader2["loanfacility_type"].ToString();
                    values.document_limit = objODBCDataReader2["document_limit"].ToString();
                    values.revolving_type = objODBCDataReader2["revolving_type"].ToString();
                    values.tenure = objODBCDataReader2["tenure"].ToString();
                    values.loanfacility_amount = objODBCDataReader2["loanfacility_amount"].ToString();
                    values.proposed_roi = objODBCDataReader2["proposed_roi"].ToString();
                    values.margin = objODBCDataReader2["margin"].ToString();
                }

                double proposed_roi = Convert.ToDouble(values.proposed_roi);
                double loanfacility_amount = Convert.ToDouble(values.loanfacility_amount);


                double interest_amount = (loanfacility_amount * (proposed_roi / 100));
                int interestamount = Convert.ToInt32(interest_amount);
                values.interest_amount = Convert.ToString(interest_amount);

                double addoncharge = (loanfacility_amount * 0.75 / 100);
                int addon_charge = Convert.ToInt32(addoncharge);
                values.interest_amount = Convert.ToString(interest_amount);
                values.addoncharge = Convert.ToString(addoncharge);

                int facilityamount = Convert.ToInt32(loanfacility_amount);

                string interest_words = objcmnfunctions.NumberToWords(interestamount);
                string facilityamount_words = objcmnfunctions.NumberToWords(facilityamount);
                string addonwords = objcmnfunctions.NumberToWords(addon_charge);

                lscontent = lscontent.Replace("revolving_type", values.revolving_type);
                lscontent = lscontent.Replace("tenure", values.tenure);
                lscontent = lscontent.Replace("loanfacility_type", values.loanfacility_type);
                lscontent = lscontent.Replace("proposed_roi", values.proposed_roi);
                lscontent = lscontent.Replace("loanfacility_amount", values.loanfacility_amount);
                lscontent = lscontent.Replace("margin", values.margin);
                lscontent = lscontent.Replace("purpose_lending", values.purpose_lending);
                lscontent = lscontent.Replace("interest_amount", values.interest_amount);
                lscontent = lscontent.Replace("facilityamount_words", facilityamount_words);
                lscontent = lscontent.Replace("addoncharge", values.addoncharge);
                lscontent = lscontent.Replace("addonwords", addonwords);

                values.template_content = lscontent;
                objODBCDataReader2.Close();
                objODBCDataReader1.Close();
            }
            values.status = true;
            return true;

        }

        public bool DaSanctionLetterSubmit(template_list values, string employee_gid)
        {
            msSQL = " select template_name, template_gid from ocs_mst_tcustomer2sanction where customer2sanction_gid='" + values.sanction_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.template_name = objODBCDataReader["template_name"].ToString();
                values.template_gid = objODBCDataReader["template_gid"].ToString();
            }
            objODBCDataReader.Close();
            msGetGid = objcmnfunctions.GetMasterGID("SLGG");
            msSQL = "insert into ids_trn_tsanctionlettergenerate(" +
                " sanctionlettergenerate_gid, " +
                " sanction_gid," +
                " template_gid, " +
                " template_name, " +
                " template_content, " +
                " sanctionlettergenerated_by," +
                " sanctionlettergenerated_date," +
                " created_by," +
                " created_date)" +
                " values (" +
                "'" + msGetGid + "'," +
                "'" + values.sanction_gid + "'," +
                "'" + values.template_gid + "'," +
                "'" + values.template_name + "'," +
                "'" + values.template_content.Replace("'", "''") + "'," +
                "'" + employee_gid + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                "'" + employee_gid + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "IDAS/SanctionLetterGeneration/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                {
                    if ((!System.IO.Directory.Exists(values.lspath)))
                        System.IO.Directory.CreateDirectory(values.lspath);
                }

                values.lsname = "Sanction_Letter" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".docx";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "IDAS/SanctionLetterGeneration/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;

                msSQL = " update ocs_mst_tcustomer2sanction set makerfile_path='" + values.lspath + "', makerfile_name='" + values.lsname + "', sanctionletter_status='Generated'," +
                        " template_content='" + values.template_content.Replace("'", "''") + "', makersubmitted_by='"+ employee_gid  + "',"+
                        " makersubmitted_on='"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where customer2sanction_gid='" + values.sanction_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                lspath = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "IDAS/SanctionLetterGeneration/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
                string lsfile_gid = objcmnfunctions.GetMasterGID("UPLF");

                // Save the HTML string as HTML File.
                string htmlFolderPath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "IDAS/SanctionLetterHTML/";
                if ((!System.IO.Directory.Exists(values.lspath)))
                    System.IO.Directory.CreateDirectory(values.lspath);

                string htmlFilePath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "IDAS/SanctionLetterHTML/sanctionletterdoc.html";
                File.WriteAllText(htmlFilePath, values.template_content);

                Spire.Doc.Document document = new Spire.Doc.Document();

                document.LoadFromFile(htmlFilePath, Spire.Doc.FileFormat.Html, XHTMLValidationType.None);

                //Read Header and Footer File

                Document doc1 = new Document();
                doc1.LoadFromFile(ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "TmpFile/Logo/headerfile.docx");
                HeaderFooter header = doc1.Sections[0].HeadersFooters.Header;

                Document doc2 = new Document(htmlFilePath);

                foreach (Section section in doc2.Sections)
                {
                    foreach (DocumentObject obj in header.ChildObjects)
                    {
                        section.HeadersFooters.Header.ChildObjects.Add(obj.Clone());
                    }
                }

                // Document
                string lssanctionletterfile = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "TmpFile/Footer/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + lsfile_gid + "/");
                {
                    if ((!System.IO.Directory.Exists(lssanctionletterfile)))
                        System.IO.Directory.CreateDirectory(lssanctionletterfile);
                }
                lssanctionletterfile = lssanctionletterfile + values.lsname;
                doc2.SaveToFile(lssanctionletterfile, Spire.Doc.FileFormat.Docx);

                Document doc3 = new Document();
                doc3.LoadFromFile(lssanctionletterfile);
                // Inser Footer Number
                HeaderFooter footer = doc3.Sections[0].HeadersFooters.Footer;
                Paragraph footerParagraph = footer.AddParagraph();
                footerParagraph.AppendField("page number", FieldType.FieldPage);
                footerParagraph.AppendText(" of ");
                footerParagraph.AppendField("number of pages", FieldType.FieldNumPages);
                footerParagraph.Format.HorizontalAlignment = HorizontalAlignment.Center;
                
                // Set Margin
                doc3.Sections[0].PageSetup.Margins.Left = 27.9f;
                doc3.Sections[0].PageSetup.Margins.Right = 27.9f;
                
                doc3.SaveToFile(values.lspath, Spire.Doc.FileFormat.Docx);

                File.Delete(htmlFilePath);

                values.status = true;
                values.message = "Sanction Letter Submitted Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
                return true;
            }
        }

        public bool DaGetTemplateDetails(mdltemplate values, string sanction_gid)
        {
            msSQL = " select sanctionletter_status, template_name, template_content, makerfile_name, makerfile_path, sanctionletter_flag, checkerapproval_flag," +
                    " checkerletter_flag, checkerpushback_remarks, digitalsignature_flag, date_format(checkerupdated_on, '%d-%m-%Y') as checkerupdated_on," +
                    " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as checkerupdated_by, date_format(makersubmitted_on, '%d-%m-%Y') as makersubmitted_on," +
                    " concat(e.user_firstname, ' ', e.user_lastname, '/', e.user_code) as makersubmitted_by " +
                    " from ocs_mst_tcustomer2sanction a "+
                    " left join hrm_mst_temployee b on b.employee_gid = a.checkerupdated_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join hrm_mst_temployee d on d.employee_gid = a.makersubmitted_by " +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                    " where customer2sanction_gid='" + sanction_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.sanctionletter_status = objODBCDataReader["sanctionletter_status"].ToString();
                values.template_name = objODBCDataReader["template_name"].ToString();
                values.template_content = objODBCDataReader["template_content"].ToString();
                values.makerfile_name = objODBCDataReader["makerfile_name"].ToString();
                values.makerfile_path = objcmnstorage.EncryptData(objODBCDataReader["makerfile_path"].ToString());
                values.sanctionletter_flag = objODBCDataReader["sanctionletter_flag"].ToString();
                values.checkerapproval_flag = objODBCDataReader["checkerapproval_flag"].ToString();
                values.checkerletter_flag = objODBCDataReader["checkerletter_flag"].ToString();
                values.checkerpushback_remarks = objODBCDataReader["checkerpushback_remarks"].ToString();
                values.digitalsignature_flag = objODBCDataReader["digitalsignature_flag"].ToString();
                values.checkerupdated_by = objODBCDataReader["checkerupdated_by"].ToString();
                values.checkerupdated_on = objODBCDataReader["checkerupdated_on"].ToString();
                values.makersubmitted_by = objODBCDataReader["makersubmitted_by"].ToString();
                values.makersubmitted_on = objODBCDataReader["makersubmitted_on"].ToString();
            }
            objODBCDataReader.Close();

            values.status = true;
            return true;
        }

        public bool DaGetTemplateLogDetails(mdltemplate values, string sanctionapprovallog_gid, string sanction_gid)
        {
            msSQL = " select template_name, template_content, sanctionletter_flag, sanction_status" +
                    " from ids_trn_tsanctionapprovallog " +
                    " where sanctionapprovallog_gid='" + sanctionapprovallog_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.template_name = objODBCDataReader["template_name"].ToString();
                values.template_content = objODBCDataReader["template_content"].ToString();
                values.sanction_status = objODBCDataReader["sanction_status"].ToString();
            }
            objODBCDataReader.Close();

            values.status = true;
            return true;
        }

        public bool DaSanctionLetterSummary(string sanction_gid, sanctiondetailsList values)
        {
            msSQL = " SELECT a.sanctionapprovallog_gid, a.sanction_gid, a.sanction_status, concat(c.user_firstname, c.user_lastname, ' / ', c.user_code) as created_by, " +
                   " date_format(a.created_date, '%d-%m-%Y') as created_date" +
                   " FROM ids_trn_tsanctionapprovallog a " +
                   " LEFT JOIN hrm_mst_temployee b ON a.created_by=b.employee_gid" +
                   " LEFT JOIN adm_mst_tuser c ON c.user_gid=b.user_gid where a.sanctionletter_flag='Y' and sanction_gid= '" + sanction_gid + "' and sanction_status='Approval Pending'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_sanctiondtl = new List<sanctiondetails>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_sanctiondtl.Add(new sanctiondetails
                    {
                        sanctionapprovallog_gid = dt["sanctionapprovallog_gid"].ToString(),
                        customer2sanction_gid = dt["sanction_gid"].ToString(),
                        sanction_status = dt["sanction_status"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                    });
                }
                values.sanctiondetails = get_sanctiondtl;
            }
            dt_datatable.Dispose();

            return true;
        }

        public void DaPostProceedToChecker(template_list values, string employee_gid)
        {
            msSQL = "update ocs_mst_tcustomer2sanction set sanctionletter_flag='Y', sanction_status='Approval Pending' where customer2sanction_gid='" + values.sanction_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = " select template_name, template_gid from ocs_mst_tcustomer2sanction where customer2sanction_gid='" + values.sanction_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    values.template_name = objODBCDataReader["template_name"].ToString();
                    values.template_gid = objODBCDataReader["template_gid"].ToString();
                }
                objODBCDataReader.Close();

                msGetGid = objcmnfunctions.GetMasterGID("SLAL");
                msSQL = "insert into ids_trn_tsanctionapprovallog(" +
                    " sanctionapprovallog_gid, " +
                    " sanction_gid," +
                    " template_gid, " +
                    " template_name, " +
                    " template_content, " +
                    " sanctionletter_flag," +
                    " sanction_status," +
                    " checkerpushback_remarks," +
                    " checkerreject_remarks," +
                    " created_by," +
                    " created_date)" +
                    " values (" +
                    "'" + msGetGid + "'," +
                    "'" + values.sanction_gid + "'," +
                    "'" + values.template_gid + "'," +
                    "'" + values.template_name + "'," +
                    "'" + values.template_content.Replace("'", "''") + "'," +
                    "'Y'," +
                    "'Approval Pending'," +
                    "''," +
                    "''," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Sanction Letter Status Updated Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occrued";
                values.status = false;
            }
        }

        public bool DaSanctionToCheckerSummary(sanctiondetailsList values)
        {
            msSQL = " SELECT a.customer2sanction_gid,b.customer_urn,a.sanction_refno,date_format(a.sanction_date,'%d-%m-%Y') as sanction_date, " +
                   " format((sanction_amount),2) as sanction_amount,a.sanction_limit,b.customername,b.vertical_code," +
                   " concat(d.user_code,' / ',d.user_firstname,d.user_lastname) as makersubmitted_by,date_format(a.makersubmitted_on,'%d-%m-%Y %h:%i %p') as makersubmitted_on," +
                   " reset_flag FROM ocs_mst_tcustomer2sanction a " +
                   " LEFT JOIN ocs_mst_tcustomer b ON a.customer_gid = b.customer_gid" +
                   " LEFT JOIN hrm_mst_temployee c ON a.makersubmitted_by=c.employee_gid" +
                   " LEFT JOIN adm_mst_tuser d ON c.user_gid=d.user_gid where a.sanctionletter_flag='Y' and a.checkerletter_flag='N' " +
                   " ORDER BY customer2sanction_gid DESC ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_sanctiondtl = new List<sanctiondetails>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_sanctiondtl.Add(new sanctiondetails
                    {
                        customer2sanction_gid = dt["customer2sanction_gid"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        sanction_date = dt["sanction_date"].ToString(),
                        sanction_amount = dt["sanction_amount"].ToString(),
                        sanction_limit = dt["sanction_limit"].ToString(),
                        customername = dt["customername"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_code = dt["vertical_code"].ToString(),
                        created_by = dt["makersubmitted_by"].ToString(),
                        created_date = dt["makersubmitted_on"].ToString(),
                        reset_flag = dt["reset_flag"].ToString(),
                    });
                }
                values.sanctiondetails = get_sanctiondtl;
            }
            dt_datatable.Dispose();

            return true;
        }

        public void DaPostProceedToApproval(string employee_gid, template_list values)
        {
            msSQL = " update ocs_mst_tcustomer2sanction set checkerletter_flag='Y', checkerupdated_by='"+ employee_gid  + "', "+
                    " checkerupdated_on='"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "' where customer2sanction_gid='" + values.sanction_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = " select template_name, template_gid, template_content from ocs_mst_tcustomer2sanction where customer2sanction_gid='" + values.sanction_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    values.template_name = objODBCDataReader["template_name"].ToString();
                    values.template_gid = objODBCDataReader["template_gid"].ToString();
                    values.template_content = objODBCDataReader["template_content"].ToString();
                }
                objODBCDataReader.Close();

                msGetGid = objcmnfunctions.GetMasterGID("SLAL");
                msSQL = "insert into ids_trn_tsanctionapprovallog(" +
                    " sanctionapprovallog_gid, " +
                    " sanction_gid," +
                    " template_gid, " +
                    " template_name, " +
                    " template_content, " +
                    " sanctionletter_flag," +
                    " sanction_status," +
                    " checkerpushback_remarks," +
                    " checkerreject_remarks," +
                    " created_by," +
                    " created_date)" +
                    " values (" +
                    "'" + msGetGid + "'," +
                    "'" + values.sanction_gid + "'," +
                    "'" + values.template_gid + "'," +
                    "'" + values.template_name + "'," +
                    "'" + values.template_content.Replace("'", "''") + "'," +
                    "'Y'," +
                    "'Checker Approval Pending'," +
                    "''," +
                    "''," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Checker Status Updated Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occrued";
                values.status = false;
            }
        }

        public void DaPusbackToMaker(template_list values, string employee_gid)
        {
            msSQL = " update ocs_mst_tcustomer2sanction set sanctionletter_flag='N', checkerpushback_remarks='" + values.pushback_remarks.Replace("'", "") + "'," +
                    " sanction_status='Pushback' where customer2sanction_gid='" + values.sanction_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = " select template_name, template_gid, template_content from ocs_mst_tcustomer2sanction where customer2sanction_gid='" + values.sanction_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    values.template_name = objODBCDataReader["template_name"].ToString();
                    values.template_gid = objODBCDataReader["template_gid"].ToString();
                    values.template_content = objODBCDataReader["template_content"].ToString();
                }
                objODBCDataReader.Close();

                msGetGid = objcmnfunctions.GetMasterGID("SLAL");
                msSQL = "insert into ids_trn_tsanctionapprovallog(" +
                    " sanctionapprovallog_gid, " +
                    " sanction_gid," +
                    " template_gid, " +
                    " template_name, " +
                    " template_content, " +
                    " sanctionletter_flag," +
                    " sanction_status," +
                    " checkerpushback_remarks," +
                    " checkerreject_remarks," +
                    " created_by," +
                    " created_date)" +
                    " values (" +
                    "'" + msGetGid + "'," +
                    "'" + values.sanction_gid + "'," +
                    "'" + values.template_gid + "'," +
                    "'" + values.template_name + "'," +
                    "'" + values.template_content.Replace("'", "''") + "'," +
                    "'N'," +
                    "'Pushback'," +
                    "'" + values.pushback_remarks.Replace("'", "") + "'," +
                    "''," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Sanction Pushbacked Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occrued";
                values.status = false;
            }
        }

        public bool DaCheckerApprovalSummary(sanctiondetailsList values)
        {

            msSQL = " SELECT a.customer2sanction_gid,b.customer_urn,a.sanction_refno,date_format(a.sanction_date,'%d-%m-%Y') as sanction_date, sanction_status," +
                   " format((sanction_amount),2) as sanction_amount,b.customername,b.vertical_code,checkerapproval_flag, a.checkerreject_remarks," +
                   " concat(d.user_code,' / ',d.user_firstname,d.user_lastname) as checkerupdated_by,date_format(a.checkerupdated_on,'%d-%m-%Y %h:%i %p') as checkerupdated_on" +
                   " FROM ocs_mst_tcustomer2sanction a " +
                   " LEFT JOIN ocs_mst_tcustomer b ON a.customer_gid = b.customer_gid" +
                   " LEFT JOIN hrm_mst_temployee c ON a.checkerupdated_by=c.employee_gid" +
                   " LEFT JOIN adm_mst_tuser d ON c.user_gid=d.user_gid where a.checkerletter_flag='Y'" +
                   " ORDER BY customer2sanction_gid DESC ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_sanctiondtl = new List<sanctiondetails>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    get_sanctiondtl.Add(new sanctiondetails
                    {
                        customer2sanction_gid = dt["customer2sanction_gid"].ToString(),
                        sanction_refno = dt["sanction_refno"].ToString(),
                        sanction_date = dt["sanction_date"].ToString(),
                        sanction_amount = dt["sanction_amount"].ToString(),
                        customername = dt["customername"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_code = dt["vertical_code"].ToString(),
                        created_by = dt["checkerupdated_by"].ToString(),
                        created_date = dt["checkerupdated_on"].ToString(),
                        checkerapproval_flag = dt["checkerapproval_flag"].ToString(),
                        checkerreject_remarks = dt["checkerreject_remarks"].ToString(),
                        sanction_status = dt["sanction_status"].ToString(),
                    });
                }
                values.sanctiondetails = get_sanctiondtl;
            }
            dt_datatable.Dispose();

            return true;
        }

        public void DaPostDigitalSignature(string sanction_gid, string employee_gid, template_list values)
        {
            msSQL = " SELECT template_content FROM ocs_mst_tcustomer2sanction where customer2sanction_gid='" + sanction_gid + "'";
            string lstemplatecontent = objdbconn.GetExecuteScalar(msSQL);
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "IDAS/SanctionLetterGenerationPDF/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                {
                    if ((!System.IO.Directory.Exists(values.lspath)))
                        System.IO.Directory.CreateDirectory(values.lspath);
                }

                values.lsname = "Sanction_Letter" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".docx";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "IDAS/SanctionLetterGenerationPDF/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;

                string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                string lsfile_gid = msdocument_gid;
                
                // Save the HTML string as HTML File.
                string htmlFilePath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "IDAS/SanctionLetterHTML/sanctionletterdoc.html";
                File.WriteAllText(htmlFilePath, lstemplatecontent);

                Spire.Doc.Document document = new Spire.Doc.Document();

                document.LoadFromFile(htmlFilePath, Spire.Doc.FileFormat.Html, XHTMLValidationType.None);

                //Read Header and Footer File

                Document doc1 = new Document();
                doc1.LoadFromFile(ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "TmpFile/Logo/headerfile_sanction.docx");
                HeaderFooter header = doc1.Sections[0].HeadersFooters.Header;

                Document doc2 = new Document(htmlFilePath);

                foreach (Section section in doc2.Sections)
                {
                    foreach (DocumentObject obj in header.ChildObjects)
                    {
                        section.HeadersFooters.Header.ChildObjects.Add(obj.Clone());
                    }
                }

                // Document
                string lsprefilfile = HttpContext.Current.Server.MapPath("/erpdocument" + "/" + lscompany_code + "/" + "TmpFile/Footer/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + lsfile_gid + "/");
                {
                    if ((!System.IO.Directory.Exists(lsprefilfile)))
                        System.IO.Directory.CreateDirectory(lsprefilfile);
                }
                lsprefilfile = lsprefilfile + values.lsname;
                doc2.SaveToFile(lsprefilfile, Spire.Doc.FileFormat.Docx);

                Document doc3 = new Document();
                doc3.LoadFromFile(lsprefilfile);
                //// Inser Footer Image
                HeaderFooter footer = doc3.Sections[0].HeadersFooters.Footer;
                Paragraph footerParagraph = footer.AddParagraph();
              
                msSQL = " SELECT document_path FROM ids_mst_tdigitalsignature where employee_gid='" + employee_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if(objODBCDataReader.HasRows == true)
                {
                    lsdocument_path = (objODBCDataReader["document_path"].ToString());
                }
                objODBCDataReader.Close();

                footerParagraph.AppendPicture(Image.FromFile(lsdocument_path));
                footerParagraph.Format.HorizontalAlignment = HorizontalAlignment.Left;
                //// Inser Footer Page Number
                HeaderFooter footer1 = doc3.Sections[0].HeadersFooters.Footer;
                Paragraph footerParagraph1 = footer1.AddParagraph();
                footerParagraph1.AppendField("page number", FieldType.FieldPage);
                footerParagraph1.AppendText(" of ");
                footerParagraph1.AppendField("number of pages", FieldType.FieldNumPages);
                footerParagraph1.Format.HorizontalAlignment = HorizontalAlignment.Center;
                //// Inser WaterMark
                TextWatermark txtWatermark = new TextWatermark();
                //set the text watermark with text string, font, color and layout.
                txtWatermark.Text = "Samunnati";
                txtWatermark.FontSize = 23;
                txtWatermark.Color = Color.Gray;
                txtWatermark.Layout = WatermarkLayout.Diagonal;
                //add the text watermark
                doc3.Watermark = txtWatermark;
                //Protect Word
                doc3.Protect(ProtectionType.AllowOnlyReading, "Welcome@123");
                
                // Set Margin
                doc3.Sections[0].PageSetup.Margins.Left = 27.9f;
                doc3.Sections[0].PageSetup.Margins.Right = 27.9f;

                doc3.SaveToFile(values.lspath, Spire.Doc.FileFormat.Docx);
                File.Delete(htmlFilePath);

                msSQL = " update ocs_mst_tcustomer2sanction set digitalsignature_flag='Y', makerfile_path='" + values.lspath + "', makerfile_name='" + values.lsname + "'" +
                       " where customer2sanction_gid='" + sanction_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Digital Signature Uploaded Successfully";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
        }

        public void DaGetPDFGenerate(string sanction_gid, string employee_gid, template_list values)
        {
            try
            {
                msSQL = " SELECT template_content FROM ocs_mst_tcustomer2sanction where customer2sanction_gid='" + sanction_gid + "'";
                string finalData = objdbconn.GetExecuteScalar(msSQL);

                fileName = fileName.Replace(".html", ".pdf");

                PdfDocument pdf = new PdfDocument();

                PdfHtmlLayoutFormat htmlLayoutFormat = new PdfHtmlLayoutFormat();

                htmlLayoutFormat.IsWaiting = false;

                PdfPageSettings setting = new PdfPageSettings();

                setting.Size = PdfPageSize.A2;

                Thread thread = new Thread(() =>
                {
                    pdf.LoadFromHTML(finalData, true, setting, htmlLayoutFormat);
                });

                thread.SetApartmentState(ApartmentState.STA);

                thread.Start();

                thread.Join();

                msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                string lsreportabspath = ConfigurationManager.AppSettings["file_path"] + "erpdocument" + "/" + lscompany_code + "/" + "Master/HighmarkReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                {
                    if ((!System.IO.Directory.Exists(lsreportabspath)))
                        System.IO.Directory.CreateDirectory(lsreportabspath);
                }

                lsreportabspath = lsreportabspath + fileName;

                pdf.SaveToFile(lsreportabspath, Spire.Pdf.FileFormat.PDF);

                //msSQL = " SELECT template_content FROM ocs_mst_tcustomer2sanction where customer2sanction_gid='" + sanction_gid + "'";
                //string lstemplatecontent = objdbconn.GetExecuteScalar(msSQL);

                //PdfDocument pdf = new PdfDocument();

                //PdfHtmlLayoutFormat htmlLayoutFormat = new PdfHtmlLayoutFormat();

                //htmlLayoutFormat.IsWaiting = false;

                //PdfPageSettings setting = new PdfPageSettings();

                //setting.Size = PdfPageSize.A2;

                //Thread thread = new Thread(() =>
                //{
                //    pdf.LoadFromHTML(lstemplatecontent, true, setting, htmlLayoutFormat);
                //});

                //thread.SetApartmentState(ApartmentState.STA);

                //thread.Start();

                //thread.Join();

                //values.lsname1 = "Sanction_Letter" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".pdf";
                //values.lspath1 = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "IDAS/SanctionLetterGenerationPDF/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname1;

                //pdf.SaveToFile(values.lspath1, Spire.Pdf.FileFormat.PDF);

                msSQL = " SELECT makerfile_path, makerfile_name FROM ocs_mst_tcustomer2sanction where customer2sanction_gid='" + sanction_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    values.lspath = objODBCDataReader["makerfile_path"].ToString();
                    values.lsname = objODBCDataReader["makerfile_name"].ToString();
                }
                objODBCDataReader.Close();

                values.lsname1 = "Sanction_Letter" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".pdf";
                values.lspath1 = ConfigurationManager.AppSettings["file_path"] + "erpdocument" + "/" + lscompany_code + "/" + "IDAS/SanctionLetterGenerationPDF/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname1;

                Spire.Doc.Document document = new Spire.Doc.Document();

                Document doc4 = new Document();
                doc4.LoadFromFile(values.lspath);

                //Convert Word to PDF
                doc4.SaveToFile(values.lspath1, Spire.Doc.FileFormat.PDF);

                values.status = true;
                values.message = "PDF downloaded Successfully";
                values.lspath = objcmnstorage.EncryptData(values.lspath);
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
        }

        public bool DaSanctionLetterLogDownload(string sanctionapprovallog_gid, template_list values)
        {
            msSQL = " select template_content from ids_trn_tsanctionapprovallog where sanctionapprovallog_gid='" + sanctionapprovallog_gid + "'";
            string lstemplate_content = objdbconn.GetExecuteScalar(msSQL);

            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "IDAS/SanctionLetterGeneration/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                {
                    if ((!System.IO.Directory.Exists(values.lspath)))
                        System.IO.Directory.CreateDirectory(values.lspath);
                }

                values.lsname = "Sanction_Letter" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".docx";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "erpdocument" + "/" + lscompany_code + "/" + "IDAS/SanctionLetterGeneration/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                
                lspath = HttpContext.Current.Server.MapPath("/erpdocument" + "/" + lscompany_code + "/" + "IDAS/SanctionLetterGeneration/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
                string lsfile_gid = objcmnfunctions.GetMasterGID("UPLF");

                // Save the HTML string as HTML File.
                string htmlFilePath = ConfigurationManager.AppSettings["file_path"] + "erpdocument" + "/" + lscompany_code + "/" + "IDAS/SanctionLetterHTML/sanctionletterdoc.html";
                File.WriteAllText(htmlFilePath, lstemplate_content);

                Spire.Doc.Document document = new Spire.Doc.Document();

                document.LoadFromFile(htmlFilePath, Spire.Doc.FileFormat.Html, XHTMLValidationType.None);

                //Read Header and Footer File

                Document doc1 = new Document();
                doc1.LoadFromFile(ConfigurationManager.AppSettings["file_path"] + "erpdocument" + "/" + lscompany_code + "/" + "TmpFile/Logo/headerfile.docx");
                HeaderFooter header = doc1.Sections[0].HeadersFooters.Header;

                Document doc2 = new Document(htmlFilePath);

                foreach (Section section in doc2.Sections)
                {
                    foreach (DocumentObject obj in header.ChildObjects)
                    {
                        section.HeadersFooters.Header.ChildObjects.Add(obj.Clone());
                    }
                }

                // Document
                string lssanctionletterfile = HttpContext.Current.Server.MapPath("/erpdocument" + "/" + lscompany_code + "/" + "TmpFile/Footer/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + lsfile_gid + "/");
                {
                    if ((!System.IO.Directory.Exists(lssanctionletterfile)))
                        System.IO.Directory.CreateDirectory(lssanctionletterfile);
                }
                lssanctionletterfile = lssanctionletterfile + values.lsname;
                doc2.SaveToFile(lssanctionletterfile, Spire.Doc.FileFormat.Docx);

                Document doc3 = new Document();
                doc3.LoadFromFile(lssanctionletterfile);
                // Inser Footer Number
                HeaderFooter footer = doc3.Sections[0].HeadersFooters.Footer;
                Paragraph footerParagraph = footer.AddParagraph();
                footerParagraph.AppendField("page number", FieldType.FieldPage);
                footerParagraph.AppendText(" of ");
                footerParagraph.AppendField("number of pages", FieldType.FieldNumPages);
                footerParagraph.Format.HorizontalAlignment = HorizontalAlignment.Center;

                // Set Margin
                doc3.Sections[0].PageSetup.Margins.Left = 27.9f;
                doc3.Sections[0].PageSetup.Margins.Right = 27.9f;

                doc3.SaveToFile(values.lspath, Spire.Doc.FileFormat.Docx);

                File.Delete(htmlFilePath);

                values.lspath = objcmnstorage.EncryptData(values.lspath);

                values.status = true;
                values.message = "Document Downloaded Successfully";
                return true;
            }
           catch
            {
                values.status = false;
                values.message = "Error Occurred";
                return true;
            }
        }

        public void DaSanctionDocAttachment(HttpRequest httpRequest, UploadDocumentname objResult, string employee_gid, string user_gid)
        {
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string servicerequest_gid = string.Empty;
            String path = lspath;
            string lssanction_gid = httpRequest.Form["sanction_gid"];
            string project_flag = httpRequest.Form["project_flag"].ToString();


            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = HttpContext.Current.Server.MapPath("/erpdocument" + "/" + lscompany_code + "/" + "IDAS/SanctionDocAttachment/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
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
                        //string lsfile_gid = msdocument_gid + FileExtension;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        Stream ls_readStream;
                        ls_readStream = httpPostedFile.InputStream;
                        MemoryStream ms = new MemoryStream();
                        ls_readStream.CopyTo(ms);
                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objResult.status = false;
                            objResult.message = "File format is not supported";
                            return;
                        }
                        //lspath = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "IDAS/SanctionDocAttachment/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");

                        //FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                        //ms.WriteTo(file);
                        //file.Close();
                        //ms.Close();
                        //lspath = "../../../erp_documents" + "/" + lscompany_code + "/" + "IDAS/SanctionDocAttachment/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "IDAS/SanctionDocAttachment/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "IDAS/SanctionDocAttachment/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";




                        msGetGid = objcmnfunctions.GetMasterGID("SLDG");
                        msSQL = " insert into ids_trn_tsanctiondocattachement( " +
                                    " sanctiondoc_gid,"+
                                    " sanction_gid," +
                                    " document_name ," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + lssanction_gid + "'," +
                                    "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + user_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult != 0)
                        {
                            objResult.status = true;
                            objResult.message = "Document Uploaded Successfully";
                        }
                        else
                        {
                            objResult.status = false;
                            objResult.message = "Error Occured";
                        }
                    }
                }
                else
                {
                    objResult.status = false;
                    objResult.message = "Error Occured";
                }
            }
            catch (Exception ex)
            {
                objResult.status = false;
                objResult.message = ex.Message;
            }
        }

        public void DaSanctionDocumentList(string sanction_gid, UploadDocumentname objfilename)
        {
            msSQL = " select sanctiondoc_gid, sanction_gid,document_name,document_path from ids_trn_tsanctiondocattachement " +
                               " where sanction_gid='" + sanction_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<UploadDocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new UploadDocumentList
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        sanction_gid = dt["sanction_gid"].ToString(),
                        sanctiondoc_gid = dt["sanctiondoc_gid"].ToString(),
                    });
                    objfilename.UploadDocumentList = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaSanctionDocumentDelete(string sanctiondoc_gid, string sanction_gid, UploadDocumentname objfilename)
        {
            msSQL = "delete from ids_trn_tsanctiondocattachement where sanctiondoc_gid='" + sanctiondoc_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                objfilename.message = "Document Deleted Successfully";
                objfilename.status = true;
            }
            else
            {
                objfilename.message = "Error Occured";
                objfilename.status = false;

            }
        }
    }
}
