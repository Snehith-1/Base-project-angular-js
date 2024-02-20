using ems.master.Models;
using ems.utilities.Functions;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Pdf;
using Spire.Pdf.HtmlConverter;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Globalization;
using ems.storage.Functions;
using System.Net;

/// <summary>
/// (It's used for pages in CAD Flow)CADFlow DataAccess Class accessed by API methods from related Controller class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash</remarks>

namespace ems.master.DataAccess
{
    public class DaMstCadFlow
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable, dt_child, dt_childindividual, dt_childgroup;
        string msSQL, msGetGid, msGetGid1, msGetGidCC, msGetGid2, msGetGid3, lscadapplication_gid;
        int mnResult, mnResult1, mnResult2, mnResultCAD;
        HttpPostedFile httpPostedFile;
        OdbcDataReader objODBCDataReader, objODBCDataReader1, objODBCDataReader2, objODBCDatareader, objODBCDatareader1;
        string lssanctionref_no, lstemplate_content, lscompany_code, lspath, lsdocument_path, fileName;
        string msGetRef, msGetGID, lsdocument_code, lsdocument_name, lsdocumenttype_name, lscompanydocument_name, lsindividualdocument_name, lsgroupdocument_name, lsdocumenttype_gid;
        string lscontent = string.Empty;
        string lsapplication_gid, lsapplication_gidcontact;
        Random rand = new Random();
        private string cc_mailid;
        private IEnumerable<string> lsCCReceipients, lsBCCReceipients;
        private string body, body1;
        private string sub;
        private string sub1;
        private int ls_port;
        private string ls_server;
        private string ls_username;
        private string ls_password;
        private string tomail_id, lsBccmail_id;
        private object cc;
        private string lsemail_toaddress;
        private string ls_makerfile_name, ls_makerfile_path, ls_sanction_amount;
        string lsrmclustermanager_gid, lsrmregionalhead_gid, lsrmzonalhead_gid, lsrmbusinesshead_gid, lsappclustermanager_gid, lsappregionalhead_gid, lsappzonalhead_gid, lsappbusinesshead_gid, lshierarchychange_flag;
        string ls_relationshipmanager_name, ls_customerref_name, ls_product_name, ls_institution_gid, tomail_id1, ls_relationshipmanager_gid;
        string lsemployee_mobileno, lsemployee_emailid, lscluster_name, ls_overalllimit_amount, ls_clustermanager_gid;


        public void DaCADSanctionSummaryCount(string user_gid, string employee_gid, CadSanctionCount values)
        {

            msSQL = " select(select count(*) from ocs_trn_tprocesstype_assign a  " +
                   " left join ocs_trn_tcadapplication b on a.application_gid = b.application_gid " +
                   " where maker_approvalflag = 'N' and b.sanction_approvalflag = 'N' " +
                   " and maker_gid = '" + employee_gid + "' and menu_gid = 'CADMGTSAN') as MakerPendingCount, " +
                   "  (select count(*) from ocs_trn_tprocesstype_assign a " +
                   "  left join ocs_trn_tcadapplication b on a.application_gid = b.application_gid " +
                    "  left join ocs_trn_tapplication2sanction d on d.application_gid = a.application_gid  " +
                   "  where maker_approvalflag = 'Y' and b.sanction_approvalflag = 'N' and d.sanction_status not in ('Approved')  " +
                   "  and maker_gid = '" + employee_gid + "' and menu_gid = 'CADMGTSAN') as MakerFollowUpCount, " +
                   "  (select count(*) from ocs_trn_tprocesstype_assign a " +
                   "  left join ocs_trn_tcadapplication b on a.application_gid = b.application_gid " +
                   " left join ocs_trn_tapplication2sanction c on a.application_gid = c.application_gid " +
                   "  where checker_approvalflag = 'N' and b.sanction_approvalflag = 'N' " +
                   "  and checker_gid = '" + employee_gid + "' and menu_gid = 'CADMGTSAN' and c.sanction_status in ('Pushback','Checker Approval Pending')) as CheckerPendingCount,   " +
                   "  (select count(*) from ocs_trn_tprocesstype_assign " +
                   "  where checker_approvalflag = 'Y' and maker_approvalflag = 'Y'  and  approver_approvalflag = 'N'" +
                   "  and checker_gid = '" + employee_gid + "' and menu_gid = 'CADMGTSAN') as CheckerFollowUpCount,  " +
                   "  (select count(*) from ocs_trn_tprocesstype_assign " +
                   "  where approver_approvalflag = 'N' and checker_approvalflag = 'Y' " +
                   "  and approver_gid = '" + employee_gid + "' and menu_gid = 'CADMGTSAN')  as ApproverPendingCount,  " +
                   "  (select count(*) from ocs_trn_tprocesstype_assign " +
                   "  where approver_approvalflag = 'Y' and checker_approvalflag = 'Y'  and menu_gid = 'CADMGTSAN' " +
                   "  and(maker_gid = '" + employee_gid + "' or checker_gid = '" + employee_gid + "' or approver_gid = '" + employee_gid + "')) as CompletedCount; ";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {

                values.MakerPendingCount = objODBCDataReader["MakerPendingCount"].ToString();
                values.MakerFollowUpCount = objODBCDataReader["MakerFollowUpCount"].ToString();
                values.CheckerPendingCount = objODBCDataReader["CheckerPendingCount"].ToString();
                values.CheckerFollowUpCount = objODBCDataReader["CheckerFollowUpCount"].ToString();
                values.ApproverPendingCount = objODBCDataReader["ApproverPendingCount"].ToString();
                values.CompletedCount = objODBCDataReader["CompletedCount"].ToString();

            }
            objODBCDataReader.Close();

        }

        public void DaGetBuyerList(string application_gid, MdlMstCAD values, string employee_gid)
        {
            msSQL = " select application2loan_gid from ocs_trn_tcadapplication2loan where application_gid='" + application_gid + "' and product_type='Agri Receivable Finance (ARF)'";
            string lsapplication2loan_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select application2buyer_gid,buyer_gid,buyer_name,buyer_limit,availed_limit,balance_limit,margin,bill_tenure " +
                   " from ocs_trn_tcadapplication2buyer where application2loan_gid='" + lsapplication2loan_gid + "'";
                   //" union" +
                   //" select creditbuyer_gid as application2buyer_gid,buyer_gid,buyer_name,format(buyer_limit,'en-IN')as buyer_limit,format(availed_limit,'en-IN')as availed_limit, " +
                   //" format(balance_limit,'en-IN')as balance_limit, margin, bill_tenuredays as bill_tenure from ocs_trn_tcadcreditbuyer" +
                   //" where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getrmbuyer_list = new List<rmbuyer_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getrmbuyer_list.Add(new rmbuyer_list
                    {
                        application2buyer_gid = (dr_datarow["application2buyer_gid"].ToString()),
                        buyer_gid = (dr_datarow["buyer_gid"].ToString()),
                        buyer_name = (dr_datarow["buyer_name"].ToString()),
                        buyer_limit = (dr_datarow["buyer_limit"].ToString()),
                        availed_limit = (dr_datarow["availed_limit"].ToString()),
                        balance_limit = (dr_datarow["balance_limit"].ToString()),
                        bill_tenuredays = (dr_datarow["bill_tenure"].ToString()),
                        margin = (dr_datarow["margin"].ToString())
                    });
                }
                values.rmbuyer_list = getrmbuyer_list;
            }
            dt_datatable.Dispose();

            msSQL = " select creditbuyer_gid as application2buyer_gid,buyer_gid,buyer_name,format(buyer_limit,'en-IN')as buyer_limit,format(availed_limit,'en-IN')as availed_limit, " +
                   " format(balance_limit,'en-IN')as balance_limit, margin, bill_tenuredays as bill_tenure from ocs_trn_tcadcreditbuyer" +
                   " where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcreditbuyer_list = new List<creditbuyer_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcreditbuyer_list.Add(new creditbuyer_list
                    {
                        application2buyer_gid = (dr_datarow["application2buyer_gid"].ToString()),
                        buyer_gid = (dr_datarow["buyer_gid"].ToString()),
                        buyer_name = (dr_datarow["buyer_name"].ToString()),
                        buyer_limit = (dr_datarow["buyer_limit"].ToString()),
                        availed_limit = (dr_datarow["availed_limit"].ToString()),
                        balance_limit = (dr_datarow["balance_limit"].ToString()),
                        bill_tenuredays = (dr_datarow["bill_tenure"].ToString()),
                        margin = (dr_datarow["margin"].ToString())
                    });
                }
                values.creditbuyer_list = getcreditbuyer_list;
            }
            dt_datatable.Dispose();

            values.status = true;
        }


        public bool DaGetTempDelete(string employee_gid, string application_gid, result value)
        {

            msSQL = " delete from ocs_trn_tuploadesdeclarationdocument where application2sanction_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from ocs_trn_tdeviationmaildocument where application2sanction_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from ocs_trn_tlimitproductinfo where application_gid='" + application_gid + "' and application2sanction_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update ocs_trn_tcadapplication2loan set limit_product='N' where application_gid='" + application_gid + "'";
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

        public void DaGetCADBasicView(string application_gid, MdlMstCAD values)
        {
            try
            {
                msSQL = " select vertical_gid from ocs_trn_tcadapplication where application_gid='" + application_gid + "'";
                string lsvertical_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select entity_name from ocs_mst_tvertical where vertical_gid='" + lsvertical_gid + "'";
                string lsentity_name = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select entity_gid, entity_name from adm_mst_tentity where entity_name='" + lsentity_name + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    values.entity_gid = objODBCDataReader["entity_gid"].ToString();
                    values.entity_name = objODBCDataReader["entity_name"].ToString();
                }
                objODBCDataReader.Close();

                msSQL = " select date_format(approved_date,'%d-%m-%Y %H:%i %p') as approved_date from ocs_trn_tAppcreditapproval" +
                        " where application_gid='" + application_gid + "' and hierary_level='3'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    values.creditapproved_date = objODBCDataReader["approved_date"].ToString();
                }
                objODBCDataReader.Close();

                msSQL = " select vertical_code from ocs_mst_tvertical where vertical_gid='" + lsvertical_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    values.vertical_code = objODBCDataReader["vertical_code"].ToString();
                }
                objODBCDataReader.Close();

                msSQL = " select stakeholder_type, stakeholdertype_gid from ocs_trn_tcadinstitution where application_gid = '" + application_gid + "'" +
                        " union " +
                        " select stakeholder_type, stakeholdertype_gid from ocs_trn_tcadcontact where application_gid = '" + application_gid + "' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getsanctiontype_list = new List<sanctiontype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getsanctiontype_list.Add(new sanctiontype_list
                        {
                            sanctiontype_gid = dt["stakeholdertype_gid"].ToString(),
                            sanctiontype_name = dt["stakeholder_type"].ToString(),
                        });
                    }
                }
                values.sanctiontype_list = getsanctiontype_list;
                dt_datatable.Dispose();

                values.status = true;
                values.message = "success";
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }


        public void DaGetProductList(string application_gid, MdlMstCAD values)
        {
            try
            {
                msSQL = "select product_type, producttype_gid from ocs_trn_tcadapplication2loan where application_gid='" + application_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getproducttype = new List<producttype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getproducttype.Add(new producttype_list
                        {
                            product_type = (dr_datarow["product_type"].ToString()),
                            producttype_gid = (dr_datarow["producttype_gid"].ToString()),
                        });
                    }
                    values.producttype_list = getproducttype;
                }
                dt_datatable.Dispose();

                values.status = true;
            }
            catch
            {
                values.status = false;
            }

        }
        public void DaGetcreditheadsview(MdlappCreditassign objVisitor, string application_gid)
        {
            try
            {
                msSQL = " select credithead_name,creditnationalmanager_name,creditregionalmanager_name,creditmanager_name,creditgroup_name,remarks " +
                        " from ocs_trn_tcadapplication  where application_gid = '" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {

                    objVisitor.credithead_name = objODBCDatareader["credithead_name"].ToString();
                    objVisitor.nationalcredit_name = objODBCDatareader["creditnationalmanager_name"].ToString();
                    objVisitor.regionalcredit_name = objODBCDatareader["creditregionalmanager_name"].ToString();
                    objVisitor.creditmanager_name = objODBCDatareader["creditmanager_name"].ToString();
                    objVisitor.creditgroup_name = objODBCDatareader["creditgroup_name"].ToString();
                    objVisitor.remarks = objODBCDatareader["remarks"].ToString();

                }
                objODBCDatareader.Close();

                objVisitor.status = true;
            }
            catch
            {
                objVisitor.status = false;
            }
        }

        public void DaGetapplicationhierarchylist(applicationhierarchy values, string application_gid, string employee_gid)
        {
            try
            {
                msSQL = " select clustermanager_name,zonalhead_name,regionalhead_name,businesshead_name from ocs_trn_tcadapplication where application_gid = '" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.clusterhead = objODBCDatareader["clustermanager_name"].ToString();
                    values.zonalhead = objODBCDatareader["zonalhead_name"].ToString();
                    values.regionhead = objODBCDatareader["regionalhead_name"].ToString();
                    values.businesshead = objODBCDatareader["businesshead_name"].ToString();
                }

                objODBCDatareader.Close();
       

                msSQL = " select a.employeereporting_to,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as level_zero,b.employee_gid, " +
                        "  concat( g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as level_one  " +
                        "  from adm_mst_tmodule2employee a " +
                        "  left join hrm_mst_temployee b on b.employee_gid = a.employee_gid " +
                        "  left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        "  left join hrm_mst_temployee f on a.employeereporting_to = f.employee_gid " +
                        "  left join adm_mst_tuser g on g.user_gid = f.user_gid  " +
                        "  where a.module_gid in  (select module_gid_parent from adm_mst_tmodule where module_gid in  " +
                        "  (select modulereportingto_gid from adm_mst_tcompany)) and c.user_status = 'Y' and b.employee_gid ='" + employee_gid + "'  group by a.employee_gid";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.level_zero = objODBCDatareader["level_zero"].ToString();
                    values.level_one = objODBCDatareader["level_one"].ToString();
                    objODBCDatareader.Close();
                }


                objODBCDatareader.Close();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaGetSanctionToList(string sanctiontype_name, string application_gid, MdlMstCAD values)
        {
            if (sanctiontype_name == "Applicant")
            {
                msSQL = " select applicant_type from ocs_trn_tcadapplication where application_gid = '" + application_gid + "'";
                string lsapplicant_type = objdbconn.GetExecuteScalar(msSQL);
                if (lsapplicant_type == "Institution")
                {
                    msSQL = " select company_name, institution_gid from ocs_trn_tcadinstitution where application_gid = '" + application_gid + "'" +
                            " and stakeholder_type='Applicant'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getsanctionto_list = new List<sanctionto_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getsanctionto_list.Add(new sanctionto_list
                            {
                                sanctionto_name = dt["company_name"].ToString(),
                                sanctionto_gid = dt["institution_gid"].ToString(),
                            });
                        }
                    }
                    values.sanctionto_list = getsanctionto_list;
                    dt_datatable.Dispose();
                }
                else
                {
                    msSQL = " select concat(first_name, ' ', middle_name, ' ', last_name) as individual_name, contact_gid from ocs_trn_tcadcontact" +
                            " where application_gid = '" + application_gid + "' and stakeholder_type='Applicant'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getsanctionto_list = new List<sanctionto_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            getsanctionto_list.Add(new sanctionto_list
                            {
                                sanctionto_name = dt["individual_name"].ToString(),
                                sanctionto_gid = dt["contact_gid"].ToString(),
                            });
                        }
                    }
                    values.sanctionto_list = getsanctionto_list;
                    dt_datatable.Dispose();
                }
            }
            else
            {
                msSQL = " select a.company_name as sanctionto_name, a.institution_gid as sanctionto_gid from ocs_trn_tcadinstitution a " +
                        " inner join ocs_trn_tcadapplication b on a.application_gid = b.application_gid " +
                        " where b.application_gid = '" + application_gid + "' and a.stakeholder_type = '" + sanctiontype_name + "'" +
                        " union " +
                        " select concat(first_name, ' ', middle_name, ' ', last_name) as sanctionto_name, a.contact_gid as sanctionto_gid from ocs_trn_tcadcontact a " +
                        " inner join ocs_trn_tcadapplication b on a.application_gid = b.application_gid " +
                        " where b.application_gid = '" + application_gid + "' and a.stakeholder_type = '" + sanctiontype_name + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getsanctionto_list = new List<sanctionto_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getsanctionto_list.Add(new sanctionto_list
                        {
                            sanctionto_name = dt["sanctionto_name"].ToString(),
                            sanctionto_gid = dt["sanctionto_gid"].ToString(),
                        });
                    }
                }
                values.sanctionto_list = getsanctionto_list;
                dt_datatable.Dispose();
            }
        }


        public void DaGetContactPersonDetail(string sanctionto_gid, MdlMstCAD values)
        {
            msSQL = " select contact_gid from ocs_trn_tcadcontact where contact_gid = '" + sanctionto_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Close();
                msSQL = "select concat(first_name,',',middle_name,',',last_name) as contactperson_name from ocs_trn_tcadcontact where contact_gid = '" + sanctionto_gid + "'";
                objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader1.HasRows == true)
                {
                    values.contactperson_name = objODBCDataReader1["contactperson_name"].ToString();
                }
                objODBCDataReader1.Close();

                //msSQL = "select concat(addressline1,',',addressline2) as primary_address from ocs_mst_tcontact2address where contact_gid = '" + sanctionto_gid + "' and primary_status='Yes'";
                //objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
                //if (objODBCDataReader1.HasRows == true)
                //{
                //    values.primary_address = objODBCDataReader1["primary_address"].ToString();
                //}
                //objODBCDataReader1.Close();

                msSQL = " select concat(addressline1,',',addressline2) as address, contact2address_gid as address_gid from ocs_trn_tcadcontact2address where contact_gid = '" + sanctionto_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var geaddress_list = new List<cadaddress_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        geaddress_list.Add(new cadaddress_list
                        {
                            address_gid = dt["address_gid"].ToString(),
                            address = dt["address"].ToString(),
                        });
                    }
                }
                values.cadaddress_list = geaddress_list;
                dt_datatable.Dispose();

                msSQL = " select mobile_no, contact2mobileno_gid as mobileno_gid from ocs_trn_tcadcontact2mobileno where contact_gid = '" + sanctionto_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmobileno_list = new List<cadmobileno_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getmobileno_list.Add(new cadmobileno_list
                        {
                            mobileno_gid = dt["mobileno_gid"].ToString(),
                            mobile_no = dt["mobile_no"].ToString(),
                        });
                    }
                }
                values.cadmobileno_list = getmobileno_list;
                dt_datatable.Dispose();

                msSQL = " select email_address, contact2email_gid as email_gid from ocs_trn_tcadcontact2email where contact_gid = '" + sanctionto_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getemail_list = new List<cademail_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getemail_list.Add(new cademail_list
                        {
                            email_gid = dt["email_gid"].ToString(),
                            email_address = dt["email_address"].ToString(),
                        });
                    }
                }
                values.cademail_list = getemail_list;
                dt_datatable.Dispose();

            }
            else
            {
                objODBCDataReader.Close();
                msSQL = "select concat(contactperson_firstname,',',contactperson_middlename,',',contactperson_lastname) as contactperson_name from ocs_trn_tcadinstitution where institution_gid = '" + sanctionto_gid + "'";
                objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader1.HasRows == true)
                {
                    values.contactperson_name = objODBCDataReader1["contactperson_name"].ToString();
                }
                objODBCDataReader1.Close();

                //msSQL = "select concat(addressline1,',',addressline2) as primary_address from ocs_mst_tinstitution2address where institution_gid = '" + sanctionto_gid + "' and primary_status='Yes'";
                //objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
                //if (objODBCDataReader1.HasRows == true)
                //{
                //    values.primary_address = objODBCDataReader1["primary_address"].ToString();
                //}
                //objODBCDataReader1.Close();
                msSQL = " select concat(addressline1,',',addressline2) as address, institution2address_gid as address_gid from ocs_trn_tcadinstitution2address where institution_gid = '" + sanctionto_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var geaddress_list = new List<cadaddress_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        geaddress_list.Add(new cadaddress_list
                        {
                            address_gid = dt["address_gid"].ToString(),
                            address = dt["address"].ToString(),
                        });
                    }
                }
                values.cadaddress_list = geaddress_list;
                dt_datatable.Dispose();

                msSQL = " select mobile_no, institution2mobileno_gid as mobileno_gid from ocs_trn_tcadinstitution2mobileno where institution_gid = '" + sanctionto_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmobileno_list = new List<cadmobileno_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getmobileno_list.Add(new cadmobileno_list
                        {
                            mobileno_gid = dt["mobileno_gid"].ToString(),
                            mobile_no = dt["mobile_no"].ToString(),
                        });
                    }
                }
                values.cadmobileno_list = getmobileno_list;
                dt_datatable.Dispose();

                msSQL = " select email_address, institution2email_gid as email_gid from ocs_trn_tcadinstitution2email where institution_gid = '" + sanctionto_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getemail_list = new List<cademail_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getemail_list.Add(new cademail_list
                        {
                            email_gid = dt["email_gid"].ToString(),
                            email_address = dt["email_address"].ToString(),
                        });
                    }
                }
                values.cademail_list = getemail_list;
                dt_datatable.Dispose();
            }

        }


        public void DaGetLoanDetail(string application2loan_gid, cadloanfacilitytype_list values)
        {
            msSQL = " select application2loan_gid,interchangeability,report_structure,document_limit, date_format(expiry_date,'%Y-%m-%d') as expiry_date" +
                    " from ocs_trn_tcadapplication2loan " +
                    " where application2loan_gid='" + application2loan_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.application2loan_gid = objODBCDataReader["application2loan_gid"].ToString();
                values.interchangeability = objODBCDataReader["interchangeability"].ToString();
                values.report_structure = objODBCDataReader["report_structure"].ToString();
                values.document_limit = objODBCDataReader["document_limit"].ToString();
                values.expiry_date = objODBCDataReader["expiry_date"].ToString();
            }
            objODBCDataReader.Close();
        }

        public void DaPostlimitproductinfo(string employee_gid, limitandproducts values)
        {
            msSQL = " select producttype_gid,product_type,productsubtype_gid,productsub_type,loanfacility_amount " +
                   " from ocs_trn_tcadapplication2loan where application2loan_gid = '" + values.application2loan_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.producttype_gid = objODBCDataReader["producttype_gid"].ToString();
                values.product_type = objODBCDataReader["product_type"].ToString();
                values.productsubtype_gid = objODBCDataReader["productsubtype_gid"].ToString();
                values.productsub_type = objODBCDataReader["productsub_type"].ToString();
                values.loanfacility_amount = objODBCDataReader["loanfacility_amount"].ToString();
            }
            objODBCDataReader.Close();

            string msGetLimitGid = objcmnfunctions.GetMasterGID("LPIG");
            msSQL = " insert into ocs_trn_tlimitproductinfo(" +
                        " limitproductinfodtl_gid," +
                        " application_gid, " +
                        " application2sanction_gid," +
                        " application2loan_gid, " +
                        " producttype_gid, " +
                        " product_type, " +
                        " productsubtype_gid," +
                        " productsub_type," +
                        " loanfacility_amount, " +
                        " interchangeability," +
                        " report_structure_gid, " +
                        " report_structure," +
                        " odlim_amount," +
                        " odlim_condition," +
                        " documented_limit," +
                        " dateof_Expiry," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetLimitGid + "'," +
                        "'" + values.application_gid + "'," +
                        "'" + employee_gid + "'," +
                        "'" + values.application2loan_gid + "'," +
                        "'" + values.producttype_gid + "'," +
                        "'" + values.product_type + "'," +
                        "'" + values.productsubtype_gid + "'," +
                        "'" + values.productsub_type + "'," +
                        "'" + values.loanfacility_amount + "'," +
                        "'" + values.interchangeability + "',";
            if (values.report_structuregid == null)
                msSQL += "'',";
            else
                msSQL += "'" + values.report_structuregid + "',";
            if (values.report_structure == null)
                msSQL += "'',";
            else
                msSQL += "'" + values.report_structure.Replace("'", "").Trim() + "',";
            msSQL += "'" + values.odlim_amount.Replace(",", "") + "'," +
                       "'" + values.odlim_condition + "'," +
                       "'" + values.documented_limit.Replace(",", "") + "'," +
                       "'" + Convert.ToDateTime(values.dateof_Expiry).ToString("yyyy-MM-dd") + "',";
            msSQL += "'" + employee_gid + "'," +
                     "current_timestamp)";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "update ocs_trn_tcadapplication2loan set limit_product='Y' where application2loan_gid ='" + values.application2loan_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.status = true;
                values.message = "Product Details are Added Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }


        public void DaUpdateLoanDetails(string employee_gid, cadloanfacilitytype_list values)
        {
            msSQL = " update ocs_trn_tcadapplication2loan set " +
                     " interchangeability='" + values.interchangeability + "'," +
                     " report_structure='" + values.report_structure + "'," +
                     " document_limit='" + values.document_limit.Replace(",", "").Trim() + "'," +
                     " expiry_date='" + Convert.ToDateTime(values.expiry_date).ToString("yyyy-MM-dd") + "'," +
                     " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where application2loan_gid='" + values.application2loan_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.message = "Loan Details Updated Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }


        public void DaGetProductChargesDtl(string application_gid, MdlMstProductChargesView values)
        {
            try
            {
                msSQL = " select application2loan_gid,date_format(facilityrequested_date, '%d-%m-%Y') as facilityrequested_date, product_type,limit_product, " +
                        " productsub_type, loanfacility_amount, loan_type, rate_interest, penal_interest, facilityoverall_limit, " +
                        " tenureoverall_limit, facility_type, facility_mode, principalfrequency_name, interestfrequency_name, " +
                        " interest_status, moratorium_type, moratorium_status, date_format(moratorium_startdate, '%d-%m-%Y') as moratorium_startdate, " +
                        " date_format(moratorium_enddate, '%d-%m-%Y') as moratorium_enddate,scheme_type,tenureproduct_month,tenureproduct_days, " +
                        " product_gid,product_name,sector_name,category_name,variety_gid,variety_name,botanical_name,alternative_name,program_gid,program " +
                        " from ocs_trn_tcadapplication2loan " +
                        " where application_gid='" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmstloansummary_list = new List<mstLoan_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmstloansummary_list.Add(new mstLoan_list
                        {
                            application2loan_gid = (dr_datarow["application2loan_gid"].ToString()),
                            facilityrequested_date = (dr_datarow["facilityrequested_date"].ToString()),
                            product_type = (dr_datarow["product_type"].ToString()),
                            productsub_type = (dr_datarow["productsub_type"].ToString()),
                            loanfacility_amount = (dr_datarow["loanfacility_amount"].ToString()),
                            loan_type = (dr_datarow["loan_type"].ToString()),
                            rate_interest = (dr_datarow["rate_interest"].ToString()),
                            penal_interest = (dr_datarow["penal_interest"].ToString()),
                            facilityoverall_limit = (dr_datarow["facilityoverall_limit"].ToString()),
                            tenureoverall_limit = (dr_datarow["tenureoverall_limit"].ToString()),
                            facility_type = (dr_datarow["facility_type"].ToString()),
                            facility_mode = (dr_datarow["facility_mode"].ToString()),
                            principalfrequency_name = (dr_datarow["principalfrequency_name"].ToString()),
                            interestfrequency_name = (dr_datarow["interestfrequency_name"].ToString()),
                            interest_status = (dr_datarow["interest_status"].ToString()),
                            moratorium_status = (dr_datarow["moratorium_status"].ToString()),
                            moratorium_type = (dr_datarow["moratorium_type"].ToString()),
                            moratorium_startdate = (dr_datarow["moratorium_startdate"].ToString()),
                            moratorium_enddate = (dr_datarow["moratorium_enddate"].ToString()),
                            scheme_type = (dr_datarow["scheme_type"].ToString()),
                            tenureproduct_days = (dr_datarow["tenureproduct_days"].ToString()),
                            tenureproduct_month = (dr_datarow["tenureproduct_month"].ToString()),
                            limit_product = (dr_datarow["limit_product"].ToString()),
                            product_gid = (dr_datarow["product_gid"].ToString()),
                            product_name = (dr_datarow["product_name"].ToString()),
                            sector_name = (dr_datarow["sector_name"].ToString()),
                            category_name = (dr_datarow["category_name"].ToString()),
                            variety_gid = (dr_datarow["variety_gid"].ToString()),
                            variety_name = (dr_datarow["variety_name"].ToString()),
                            botanical_name = (dr_datarow["botanical_name"].ToString()),
                            alternative_name = (dr_datarow["alternative_name"].ToString()),
                            program_gid = (dr_datarow["program_gid"].ToString()),
                            program = (dr_datarow["program"].ToString())
                        });
                    }
                    values.mstLoan_list = getmstloansummary_list;
                }
                dt_datatable.Dispose();

                msSQL = " select overalllimit_amount, validityoveralllimit_year, validityoveralllimit_month, validityoveralllimit_days, " +
                                " calculationoveralllimit_validity from ocs_trn_tcadapplication " +
                                " where application_gid='" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.overalllimit_amount = objODBCDatareader["overalllimit_amount"].ToString();
                    values.validityoveralllimit_year = objODBCDatareader["validityoveralllimit_year"].ToString();
                    values.validityoveralllimit_month = objODBCDatareader["validityoveralllimit_month"].ToString();
                    values.validityoveralllimit_days = objODBCDatareader["validityoveralllimit_days"].ToString();
                    values.calculationoveralllimit_validity = objODBCDatareader["calculationoveralllimit_validity"].ToString();
                }
                objODBCDatareader.Close();


                msSQL = " select application2loan_gid, source_type, guideline_value, date_format(guideline_date, '%d-%m-%Y') as guideline_date, " +
                        " date_format(marketvalue_date, '%d-%m-%Y') as marketvalue_date, market_value, forcedsource_value, " +
                        " collateralSSV_value, date_format(forcedvalueassessed_on, '%d-%m-%Y') as forcedvalueassessed_on, " +
                        " collateralobservation_summary,product_type,productsub_type from ocs_trn_tcadapplication2loan  " +
                        " where application_gid='" + application_gid + "' and loan_type ='Secured'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmstcollateral_list = new List<mstcollateral_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmstcollateral_list.Add(new mstcollateral_list
                        {
                            application2loan_gid = (dr_datarow["application2loan_gid"].ToString()),
                            source_type = (dr_datarow["source_type"].ToString()),
                            guideline_value = (dr_datarow["guideline_value"].ToString()),
                            guideline_date = (dr_datarow["guideline_date"].ToString()),
                            marketvalue_date = (dr_datarow["marketvalue_date"].ToString()),
                            market_value = (dr_datarow["market_value"].ToString()),
                            forcedsource_value = (dr_datarow["forcedsource_value"].ToString()),
                            collateralSSV_value = (dr_datarow["collateralSSV_value"].ToString()),
                            forcedvalueassessed_on = (dr_datarow["forcedvalueassessed_on"].ToString()),
                            collateralobservation_summary = (dr_datarow["collateralobservation_summary"].ToString()),
                            product_type = (dr_datarow["product_type"].ToString()),
                            productsub_type = (dr_datarow["productsub_type"].ToString()),
                        });
                    }
                    values.mstcollateral_list = getmstcollateral_list;
                }
                dt_datatable.Dispose();

                msSQL = "select processing_fee, processing_collectiontype, doc_charges, doccharge_collectiontype, fieldvisit_charges," +
                        " fieldvisit_charges_collectiontype, adhoc_fee, adhoc_collectiontype, life_insurance, lifeinsurance_collectiontype, " +
                        " acct_insurance, total_collect, total_deduct,product_type,producttype_gid,acctinsurance_collectiontype from ocs_trn_tcadapplicationservicecharge " +
                        " where application_gid='" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmstservicecharge_list = new List<servicecharge_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmstservicecharge_list.Add(new servicecharge_List
                        {
                            producttype_gid = (dr_datarow["producttype_gid"].ToString()),
                            product_type = (dr_datarow["product_type"].ToString()),
                            processing_fee = (dr_datarow["processing_fee"].ToString()),
                            processing_collectiontype = (dr_datarow["processing_collectiontype"].ToString()),
                            doc_charges = (dr_datarow["doc_charges"].ToString()),
                            doccharge_collectiontype = (dr_datarow["doccharge_collectiontype"].ToString()),
                            fieldvisit_charge = (dr_datarow["fieldvisit_charges"].ToString()),
                            fieldvisit_collectiontype = (dr_datarow["fieldvisit_charges_collectiontype"].ToString()),
                            adhoc_fee = (dr_datarow["adhoc_fee"].ToString()),
                            adhoc_collectiontype = (dr_datarow["adhoc_collectiontype"].ToString()),
                            life_insurance = (dr_datarow["life_insurance"].ToString()),
                            lifeinsurance_collectiontype = (dr_datarow["lifeinsurance_collectiontype"].ToString()),
                            acct_insurance = (dr_datarow["acct_insurance"].ToString()),
                            acctinsurance_collectiontype = (dr_datarow["acctinsurance_collectiontype"].ToString()),
                            total_collect = (dr_datarow["total_collect"].ToString()),
                            total_deduct = (dr_datarow["total_deduct"].ToString())
                        });
                    }
                    values.servicecharge_List = getmstservicecharge_list;
                }
                dt_datatable.Dispose();

                msSQL = " select application2hypothecation_gid, security_type, security_description, security_value, " +
                        " date_format(securityassessed_date, '%d-%m-%Y') as securityassessed_date, asset_id, roc_fillingid, " +
                        " CERSAI_fillingid, hypoobservation_summary, primary_security " +
                        " from ocs_trn_tcadapplication2hypothecation " +
                        " where application_gid='" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.application2hypothecation_gid = objODBCDatareader["application2hypothecation_gid"].ToString();
                    values.security_type = objODBCDatareader["security_type"].ToString();
                    values.security_description = objODBCDatareader["security_description"].ToString();
                    values.security_value = objODBCDatareader["security_value"].ToString();
                    values.securityassessed_date = objODBCDatareader["securityassessed_date"].ToString();
                    values.asset_id = objODBCDatareader["asset_id"].ToString();
                    values.roc_fillingid = objODBCDatareader["roc_fillingid"].ToString();
                    values.CERSAI_fillingid = objODBCDatareader["CERSAI_fillingid"].ToString();
                    values.hypoobservation_summary = objODBCDatareader["hypoobservation_summary"].ToString();
                    values.primary_security = objODBCDatareader["primary_security"].ToString();
                }
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }

        }


        public void DaGetPurposeofLoan(string application2loan_gid, MdlMstProductChargesView values)
        {
            try
            {
                msSQL = " select enduse_purpose from ocs_trn_tcadapplication2loan where application2loan_gid='" + application2loan_gid + "'";
                values.enduse_purpose = objdbconn.GetExecuteScalar(msSQL);
                values.status = true;
                values.message = "success";

            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }

        }
        public void DaGetLoanProgramValueChain(string application2loan_gid, MdlMstProductChargesView values)
        {
            try
            {
                msSQL = " select program, primaryvaluechain_name, secondaryvaluechain_name,product_gid,product_name, " +
                        " variety_gid,variety_name,sector_name,category_name,botanical_name,alternative_name from ocs_trn_tcadapplication2loan where application2loan_gid='" + application2loan_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.program = objODBCDatareader["program"].ToString();
                    values.primaryvaluechain_name = objODBCDatareader["primaryvaluechain_name"].ToString();
                    values.secondaryvaluechain_name = objODBCDatareader["secondaryvaluechain_name"].ToString();
                    values.product_gid = objODBCDatareader["product_gid"].ToString();
                    values.product_name = objODBCDatareader["product_name"].ToString();
                    values.variety_gid = objODBCDatareader["variety_gid"].ToString();
                    values.variety_name = objODBCDatareader["variety_name"].ToString();
                    values.sector_name = objODBCDatareader["sector_name"].ToString();
                    values.category_name = objODBCDatareader["category_name"].ToString();
                    values.botanical_name = objODBCDatareader["botanical_name"].ToString();
                    values.alternative_name = objODBCDatareader["alternative_name"].ToString();
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

        public void DaCADSanctionDtls(string sanction_gid, cadsanctiondetails values)
        {
            try
            {
                msSQL = " SELECT a.entity,a.application2sanction_gid,b.application_gid,a.sanction_refno,sanction_date,a.state,a.batch_status, " +
                "  format((sanction_amount), 2) as sanction_amount,a.sanctionto_name, date_format(sanction_date,'%d-%m-%Y') as sanctionDate," +
                 " a.esdeclaration_status,a.makerfile_path,a.makerfile_name, checkerletter_flag, checkerapproval_flag FROM ocs_trn_tapplication2sanction a " +
                 " LEFT JOIN ocs_trn_tcadapplication b ON a.application_gid = b.application_gid " +
                 " WHERE application2sanction_gid ='" + sanction_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows)
                {
                    values.sanction_refno = objODBCDataReader["sanction_refno"].ToString();
                    values.batch_status = objODBCDataReader["batch_status"].ToString();
                    values.application2sanction_gid = objODBCDataReader["application2sanction_gid"].ToString();
                    values.sanction_amount = objODBCDataReader["sanction_amount"].ToString();
                    values.sanction_date = objODBCDataReader["sanctionDate"].ToString();
                    values.sanctionto_name = objODBCDataReader["sanctionto_name"].ToString();

                    if (objODBCDataReader["sanction_date"].ToString() != "")
                    {
                        values.sanctionDate = Convert.ToDateTime(objODBCDataReader["sanction_date"].ToString());
                    }
                }
                objODBCDataReader.Close();

            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }

        }

        public bool DaSanctionContent(cadtemplate_list values)
        {
            //msSQL = " update ocs_trn_tapplication2sanction set template_name='" + values.template_name + "', template_gid='" + values.template_gid + "' where application2sanction_gid='" + values.sanction_gid + "'";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //Get Template Content 
            msSQL = " select  a.template_content from adm_mst_ttemplate a where a.template_gid='" + values.template_gid + "'";
            lstemplate_content = objdbconn.GetExecuteScalar(msSQL);

            lscontent = lstemplate_content;
            string lsapplication_no = "", lssanction_date = "", lsapplication_gid = "", lssanctionto_name = "";
            msSQL = " select a.sanction_refno, a.application_name, a.sanctionto_name, a.ccapproved_date, a.validity_months,b.application_no, date_format(a.sanction_date, '%d-%m-%Y') as sanction_date, " +
                    " a.contactperson_name, a.contactperson_number, a.contactpersonemail_address, a.contactperson_address, a.purpose_lending, b.relationshipmanager_gid," +
                    " b.relationshipmanager_name, c.employee_mobileno,a.application_gid, c.employee_emailid" +
                    " from ocs_trn_tapplication2sanction a " +
                    " LEFT JOIN ocs_trn_tcadapplication b on a.application_gid = b.application_gid " +
                    " LEFT JOIN hrm_mst_temployee c ON c.employee_gid = b.relationshipmanager_gid " +
                    " where application2sanction_gid='" + values.sanction_gid + "'";

            objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader1.HasRows == true)
            {
                values.sanction_refno = objODBCDataReader1["sanction_refno"].ToString();
                lsapplication_gid = objODBCDataReader1["application_gid"].ToString();
                values.application_name = objODBCDataReader1["application_name"].ToString();
                lsapplication_no = objODBCDataReader1["application_no"].ToString();
                lssanction_date = objODBCDataReader1["sanction_date"].ToString();
                values.ccapproved_date = objODBCDataReader1["ccapproved_date"].ToString();
                values.address = objODBCDataReader1["contactperson_address"].ToString();
                values.mobileno = objODBCDataReader1["contactperson_number"].ToString();
                values.email = objODBCDataReader1["contactpersonemail_address"].ToString();
                values.contactperson = objODBCDataReader1["contactperson_name"].ToString();
                values.purpose_lending = objODBCDataReader1["purpose_lending"].ToString();
                values.validity_months = objODBCDataReader1["validity_months"].ToString();
                values.relationshipmgmt_name = objODBCDataReader1["relationshipmanager_name"].ToString();
                values.employee_mobileno = objODBCDataReader1["employee_mobileno"].ToString();
                values.employee_mailid = objODBCDataReader1["employee_emailid"].ToString();
                lssanctionto_name = objODBCDataReader1["sanctionto_name"].ToString();
            }
            objODBCDataReader1.Close();
            lscontent = lscontent.Replace("sanctionto_name", lssanctionto_name);
            lscontent = lscontent.Replace("contact_person", values.contactperson);
            lscontent = lscontent.Replace("mobile_no", values.mobileno);
            lscontent = lscontent.Replace("applicatication_refno", lsapplication_no);
            lscontent = lscontent.Replace("sanction_date", lssanction_date);
            lscontent = lscontent.Replace("ToContactaddress", values.address);
            lscontent = lscontent.Replace("ccapproved_date", values.ccapproved_date);
            lscontent = lscontent.Replace("email", values.email);
            lscontent = lscontent.Replace("application_name", values.application_name);
            lscontent = lscontent.Replace("sanction_refno", values.sanction_refno);
            lscontent = lscontent.Replace("validity_months", values.validity_months);
            lscontent = lscontent.Replace("relationshipmgmt_name", values.relationshipmgmt_name);
            lscontent = lscontent.Replace("employee_mobileno", values.employee_mobileno);
            lscontent = lscontent.Replace("employee_mailid", values.employee_mailid);

            //msSQL = "select sanction2loanfacilitytype_gid,loanfacility_gid,format(loanfacility_amount,2) as loanfacility_amount,loanfacility_type, " +
            //   " format(document_limit,2) as document_limit,margin,date_format(expiry_date, '%d-%m-%Y') as expiry_date,revolving_type,tenure, " +
            //   " interchangeability,loanfacilityref_no,SUBSTRING_INDEX(proposed_roi, '%', 1) proposed_roi" +
            //   " from ocs_trn_tsanction2loanfacilitytype  where application2sanction_gid='" + values.sanction_gid + "'";
            msSQL = " select concat(product_type,'-', productsub_type) as product_type,format(loanfacility_amount,2) as facility_limit, " +
                  " enduse_purpose as purposeofloan, facility_mode ,rate_interest as margin,tenureoverall_limit " +
                  " from ocs_trn_tcadapplication2loan where application_gid = '" + lsapplication_gid + "'";
            objODBCDataReader2 = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader2.HasRows == true)
            {
                values.loanfacility_type = objODBCDataReader2["product_type"].ToString();
                values.loanfacility_amount = objODBCDataReader2["facility_limit"].ToString();
                values.revolving_type = objODBCDataReader2["facility_mode"].ToString();
                values.tenure = objODBCDataReader2["tenureoverall_limit"].ToString();
                values.purpose_lending = objODBCDataReader2["purposeofloan"].ToString();
                values.margin = objODBCDataReader2["margin"].ToString();
            }
            objODBCDataReader2.Close();
            //double proposed_roi = Convert.ToDouble(values.proposed_roi);
            double loanfacility_amount = Convert.ToDouble(values.loanfacility_amount);


            //double interest_amount = (loanfacility_amount * (proposed_roi / 100));
            //int interestamount = Convert.ToInt32(interest_amount);
            //values.interest_amount = Convert.ToString(interest_amount);

            double addoncharge = (loanfacility_amount * 1 / 100);
            //int addon_charge = Convert.ToInt32(addoncharge);
            //values.interest_amount = Convert.ToString(interest_amount);
            //values.addoncharge = Convert.ToString(addoncharge);

            int facilityamount = Convert.ToInt32(loanfacility_amount);

            //string interest_words = NumberToWords(interestamount);
            string facilityamount_words = NumberToWords(facilityamount);
            //string addonwords = NumberToWords(addon_charge);

            lscontent = lscontent.Replace("loanfacility_type", values.loanfacility_type);
            lscontent = lscontent.Replace("loanfacility_amount", values.loanfacility_amount);
            lscontent = lscontent.Replace("facilityamount_words", facilityamount_words);
            lscontent = lscontent.Replace("tenure", values.tenure);
            lscontent = lscontent.Replace("purpose_lending", values.purpose_lending);
            lscontent = lscontent.Replace("revolving_type", values.revolving_type);
            lscontent = lscontent.Replace("margin", values.margin);
            //lscontent = lscontent.Replace("interest_amount", values.interest_amount);

            //lscontent = lscontent.Replace("addoncharge", values.addoncharge);
            //lscontent = lscontent.Replace("addonwords", addonwords);
            //msSQL = " select  a.template_content from adm_mst_ttemplate a where a.template_gid='" + values.template_gid + "'";
            //lstemplate_content = objdbconn.GetExecuteScalar(msSQL);
            //lscontent = lstemplate_content;

            values.template_content = lscontent;
            values.status = true;
            return true;

        }

        public bool DaPostTemplateSanctionMultipleFacility(cadtemplate_list values)
        {
            //msSQL = " update ocs_trn_tapplication2sanction set template_name='" + values.template_name + "', template_gid='" + values.template_gid + "' where application2sanction_gid='" + values.sanction_gid + "'";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //Get Template Content
            msSQL = " select  a.template_content from adm_mst_ttemplate a " +
              " where a.template_gid='" + values.template_gid + "'";
            lstemplate_content = objdbconn.GetExecuteScalar(msSQL);

            lscontent = lstemplate_content;
            string lsapplication_gid = "", lsapplication_no = "", lssanction_date = "", lssanctionto_name = "";
            msSQL = " select a.application_gid, a.sanction_refno, a.application_name, a.ccapproved_date, a.validity_months ,b.application_no, " +
                    " date_format(a.sanction_date, '%d-%m-%Y') as sanction_date,a.sanctionto_name, " +
                    " a.contactperson_name, a.contactperson_number, a.contactpersonemail_address, a.contactperson_address, a.purpose_lending, b.relationshipmanager_gid," +
                    " b.relationshipmanager_name, c.employee_mobileno, c.employee_emailid" +
                    " from ocs_trn_tapplication2sanction a " +
                    " LEFT JOIN ocs_trn_tcadapplication b on a.application_gid = b.application_gid " +
                    " LEFT JOIN hrm_mst_temployee c ON c.employee_gid = b.relationshipmanager_gid " +
                    " where application2sanction_gid='" + values.sanction_gid + "'";
            objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader1.HasRows == true)
            {
                objODBCDataReader1.Read();
                values.sanction_refno = objODBCDataReader1["sanction_refno"].ToString();
                values.application_name = objODBCDataReader1["application_name"].ToString();
                lsapplication_gid = objODBCDataReader1["application_gid"].ToString();
                values.ccapproved_date = objODBCDataReader1["ccapproved_date"].ToString();
                values.address = objODBCDataReader1["contactperson_address"].ToString();
                values.mobileno = objODBCDataReader1["contactperson_number"].ToString();
                values.email = objODBCDataReader1["contactpersonemail_address"].ToString();
                values.contactperson = objODBCDataReader1["contactperson_name"].ToString();
                values.purpose_lending = objODBCDataReader1["purpose_lending"].ToString();
                values.validity_months = objODBCDataReader1["validity_months"].ToString();
                values.relationshipmgmt_name = objODBCDataReader1["relationshipmanager_name"].ToString();
                values.employee_mobileno = objODBCDataReader1["employee_mobileno"].ToString();
                values.employee_mailid = objODBCDataReader1["employee_emailid"].ToString();
                lsapplication_no = objODBCDataReader1["application_no"].ToString();
                lssanction_date = objODBCDataReader1["sanction_date"].ToString();
                lssanctionto_name = objODBCDataReader1["sanctionto_name"].ToString();
            }
            objODBCDataReader1.Close();
            lscontent = lscontent.Replace("sanctionto_name", lssanctionto_name);
            lscontent = lscontent.Replace("contact_person", values.contactperson);
            lscontent = lscontent.Replace("mobile_no", values.mobileno + ",");
            lscontent = lscontent.Replace("address", values.address);
            lscontent = lscontent.Replace("ccapproved_date", values.ccapproved_date);
            lscontent = lscontent.Replace("email", values.email);
            lscontent = lscontent.Replace("application_name", values.application_name);
            lscontent = lscontent.Replace("sanction_refno", values.sanction_refno);
            lscontent = lscontent.Replace("validity_months", values.validity_months);
            lscontent = lscontent.Replace("relationshipmgmt_name", values.relationshipmgmt_name);
            lscontent = lscontent.Replace("employee_mobileno", values.employee_mobileno);
            lscontent = lscontent.Replace("employee_mailid", values.employee_mailid);
            lscontent = lscontent.Replace("sanction_date", lssanction_date);
            lscontent = lscontent.Replace("application_no", lsapplication_no);


            msSQL = " select concat(product_type,'-', productsub_type) as product_type,format(loanfacility_amount,2) as facility_limit, " +
                    " enduse_purpose as purposeofloan, facility_mode ,rate_interest as margin,tenureoverall_limit " +
                    " from ocs_trn_tcadapplication2loan where application_gid = '" + lsapplication_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getloanfacilitytype = new List<cadloanfacilitytype_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getloanfacilitytype.Add(new cadloanfacilitytype_list
                    {
                        loanfacility_amount = (dr_datarow["facility_limit"].ToString()),
                        loanfacility_type = (dr_datarow["product_type"].ToString()),
                        revolving_type = (dr_datarow["facility_mode"].ToString()),
                        tenure = (dr_datarow["tenureoverall_limit"].ToString()),
                        margin = (dr_datarow["margin"].ToString()),
                        loanTitle = (dr_datarow["purposeofloan"].ToString()),
                    });
                }
                values.cadloanfacilitytype_list = getloanfacilitytype;
            }
            dt_datatable.Dispose();

            if (values.cadloanfacilitytype_list != null && values.cadloanfacilitytype_list.Count != 0)
            {
                int j = 1;
                for (int i = 0; i < values.cadloanfacilitytype_list.Count; i++)
                {

                    String loanfacilityamount = values.cadloanfacilitytype_list[i].loanfacility_amount;
                    double loanfacility_amount = Convert.ToDouble(loanfacilityamount);
                    int facilityamount = Convert.ToInt32(loanfacility_amount);
                    string facilityamount_words = NumberToWords(facilityamount);

                    lscontent = lscontent.Replace("revolving_type" + j + "", values.cadloanfacilitytype_list[i].revolving_type);
                    lscontent = lscontent.Replace("tenure" + j + "", values.cadloanfacilitytype_list[i].tenure);
                    lscontent = lscontent.Replace("loanfacility_type" + j + "", values.cadloanfacilitytype_list[i].loanfacility_type);
                    lscontent = lscontent.Replace("loanfacility_amount" + j + "", values.cadloanfacilitytype_list[i].loanfacility_amount);
                    lscontent = lscontent.Replace("margin" + j + "", values.cadloanfacilitytype_list[i].margin);
                    lscontent = lscontent.Replace("purpose_lending" + j + "", values.cadloanfacilitytype_list[i].loanTitle);
                    lscontent = lscontent.Replace("facilityamount" + j + "_words", facilityamount_words);
                    j++;
                }
                if (values.cadloanfacilitytype_list.Count == 2)
                {
                    lscontent = lscontent.Replace("revolving_type3", "");
                    lscontent = lscontent.Replace("tenure3", "");
                    lscontent = lscontent.Replace("loanfacility_type3", "");
                    lscontent = lscontent.Replace("loanfacility_amount3", "");
                    lscontent = lscontent.Replace("margin3", "");
                    lscontent = lscontent.Replace("purpose_lending3", "");
                    lscontent = lscontent.Replace("facilityamount3_words", "");
                }
                //String proposedroi1 = values.cadloanfacilitytype_list[0].proposed_roi;
                //String proposedroi2 = values.cadloanfacilitytype_list[1].proposed_roi;
                //String loanfacilityamount1 = values.cadloanfacilitytype_list[0].loanfacility_amount;
                //String loanfacilityamount2 = values.cadloanfacilitytype_list[1].loanfacility_amount; 


                ////double proposed_roi1 = Convert.ToDouble(proposedroi1);
                //double loanfacility_amount1 = Convert.ToDouble(loanfacilityamount1);
                //double loanfacility_amount2 = Convert.ToDouble(loanfacilityamount2);


                ////double interest_amount = ((loanfacility_amount1 + loanfacility_amount2) * (proposed_roi1 / 100));
                ////int interestamount = Convert.ToInt32(interest_amount);
                ////values.interest_amount = Convert.ToString(interest_amount);

                ////double addoncharge = ((loanfacility_amount1 + loanfacility_amount2) * 1 / 100);
                ////int addon_charge = Convert.ToInt32(addoncharge);
                ////values.interest_amount = Convert.ToString(interest_amount);

                //int facilityamount1 = Convert.ToInt32(loanfacility_amount1);
                //int facilityamount2 = Convert.ToInt32(loanfacility_amount2);


                ////string interest_words = NumberToWords(interestamount);
                //string facilityamount1_words = NumberToWords(facilityamount1);
                //string facilityamount2_words = NumberToWords(facilityamount2);


                //lscontent = lscontent.Replace("revolving_type1", values.cadloanfacilitytype_list[0].revolving_type);
                //lscontent = lscontent.Replace("revolving_type2", values.cadloanfacilitytype_list[1].revolving_type);
                //lscontent = lscontent.Replace("tenure1", values.cadloanfacilitytype_list[0].tenure);
                //lscontent = lscontent.Replace("tenure2", values.cadloanfacilitytype_list[1].tenure);
                //lscontent = lscontent.Replace("loanfacility_type1", values.cadloanfacilitytype_list[0].loanfacility_type);
                //lscontent = lscontent.Replace("loanfacility_type2", values.cadloanfacilitytype_list[1].loanfacility_type);

                ////lscontent = lscontent.Replace("proposed_roi1", values.cadloanfacilitytype_list[0].proposed_roi);
                ////lscontent = lscontent.Replace("proposed_roi2", values.cadloanfacilitytype_list[1].proposed_roi);
                ////lscontent = lscontent.Replace("proposed_roi3", values.cadloanfacilitytype_list[2].proposed_roi);
                //lscontent = lscontent.Replace("loanfacility_amount1", values.cadloanfacilitytype_list[0].loanfacility_amount);
                //lscontent = lscontent.Replace("loanfacility_amount2", values.cadloanfacilitytype_list[1].loanfacility_amount);
                //lscontent = lscontent.Replace("margin1", values.cadloanfacilitytype_list[0].margin);
                //lscontent = lscontent.Replace("margin2", values.cadloanfacilitytype_list[1].margin);
                //lscontent = lscontent.Replace("purpose_lending1", values.cadloanfacilitytype_list[0].loanTitle);
                //lscontent = lscontent.Replace("purpose_lending2", values.cadloanfacilitytype_list[1].loanTitle);
                ////lscontent = lscontent.Replace("interest_amount", values.interest_amount);
                //lscontent = lscontent.Replace("facilityamount1_words", facilityamount1_words);
                //lscontent = lscontent.Replace("facilityamount2_words", facilityamount2_words);

                //if (values.cadloanfacilitytype_list.Count == 3)
                //{
                //    String loanfacilityamount3 = values.cadloanfacilitytype_list[2].loanfacility_amount;
                //    double loanfacility_amount3 = Convert.ToDouble(loanfacilityamount3);
                //    int facilityamount3 = Convert.ToInt32(loanfacility_amount3);
                //    string facilityamount3_words = NumberToWords(facilityamount3);
                //    lscontent = lscontent.Replace("revolving_type3", values.cadloanfacilitytype_list[2].revolving_type);
                //    lscontent = lscontent.Replace("tenure3", values.cadloanfacilitytype_list[2].tenure);
                //    lscontent = lscontent.Replace("loanfacility_type3", values.cadloanfacilitytype_list[2].loanfacility_type);
                //    lscontent = lscontent.Replace("loanfacility_amount3", values.cadloanfacilitytype_list[2].loanfacility_amount);
                //    lscontent = lscontent.Replace("margin3", values.cadloanfacilitytype_list[2].margin);
                //    lscontent = lscontent.Replace("purpose_lending3", values.cadloanfacilitytype_list[2].loanTitle);
                //    lscontent = lscontent.Replace("facilityamount3_words", facilityamount3_words);
                //}
            }
            //msSQL = " select  a.template_content from adm_mst_ttemplate a " +
            // " where a.template_gid='" + values.template_gid + "'";
            //lstemplate_content = objdbconn.GetExecuteScalar(msSQL);
            // lscontent = lstemplate_content;

            values.template_content = lscontent;


            values.status = true;
            return true;
        }


        public void DaUpdateCheckerApproval(cadtemplate_list values, string employee_gid)
        {
            if (values.sanction_status == "Approved")
            {
                msSQL = " update ocs_trn_tapplication2sanction set checkerapproval_flag='Y', sanction_status='" + values.sanction_status + "', checkerapproved_by='" + employee_gid + "',";
                if (values.reject_remarks == "" || values.reject_remarks == null || values.reject_remarks == "undefined")
                {
                    msSQL += " checkerreject_remarks='', ";
                }
                else
                {
                    msSQL += " checkerreject_remarks='" + values.reject_remarks.Replace("'", "") + "', ";
                }
                msSQL += " checkerapproved_on='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where application2sanction_gid='" + values.sanction_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = " update ocs_trn_tapplication2sanction set checkerapproval_flag='R', sanction_status='" + values.sanction_status + "', checkerapproved_by='" + employee_gid + "',";
                if (values.reject_remarks == "" || values.reject_remarks == null || values.reject_remarks == "undefined")
                {
                    msSQL += " checkerreject_remarks='', ";
                }
                else
                {
                    msSQL += " checkerreject_remarks='" + values.reject_remarks.Replace("'", "") + "', ";
                }
                msSQL += " checkerapproved_on='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where application2sanction_gid='" + values.sanction_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult != 0)
            {
                string lsprocesstypeassign_gid = "", lsapplication_gid = "";
                msSQL = " select a.processtypeassign_gid,a.application_gid from ocs_trn_tprocesstype_assign a " +
                      " left join ocs_trn_tapplication2sanction b on a.application_gid = b.application_gid " +
                      " where application2sanction_gid = '" + values.sanction_gid + "' and menu_gid ='" + getMenuClass.Sanction + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsprocesstypeassign_gid = objODBCDatareader["processtypeassign_gid"].ToString();
                    lsapplication_gid = objODBCDatareader["application_gid"].ToString();
                }
                objODBCDatareader.Close();

                if (lsprocesstypeassign_gid != "" && values.sanction_status == "Approved")
                {
                    msSQL = " update ocs_trn_tprocesstype_assign set approver_approvalflag='Y', " +
                            " approver_approveddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            " overall_approvalstatus='Approved'  where processtypeassign_gid='" + lsprocesstypeassign_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_trn_tcadapplication set sanction_approvalflag='Y' " +
                            " where application_gid='" + lsapplication_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msSQL = " select template_name, template_gid, template_content from ocs_trn_tapplication2sanction " +
                        " where application2sanction_gid='" + values.sanction_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    values.template_name = objODBCDataReader["template_name"].ToString();
                    values.template_gid = objODBCDataReader["template_gid"].ToString();
                    values.template_content = objODBCDataReader["template_content"].ToString();
                }
                objODBCDataReader.Close();

                msGetGid = objcmnfunctions.GetMasterGID("ASLL");
                msSQL = "insert into ocs_trn_tsanctionapprovallog(" +
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

                    //Customer mail


                    //try
                    //{

                    //    msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                    //          " FROM adm_mst_tcompany";
                    //    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    //    if (objODBCDatareader.HasRows == true)
                    //    {
                    //        ls_server = objODBCDatareader["pop_server"].ToString();
                    //        ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                    //        ls_username = objODBCDatareader["pop_username"].ToString();
                    //        ls_password = objODBCDatareader["pop_password"].ToString();
                    //    }
                    //    objODBCDatareader.Close();

                    //    msSQL = " SELECT format((sanction_amount), 2) as sanction_amount, makerfile_path, makerfile_name from ocs_trn_tapplication2sanction " +         
                    //  " WHERE application2sanction_gid ='" + values.sanction_gid + "'";
                    //    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    //    if (objODBCDatareader.HasRows == true)
                    //    {
                    //        ls_sanction_amount = objODBCDatareader["sanction_amount"].ToString();
                    //        ls_makerfile_path = objODBCDatareader["makerfile_path"].ToString();
                    //        ls_makerfile_name = objODBCDatareader["makerfile_name"].ToString();

                    //    }
                    //    objODBCDatareader.Close();

                    //    msSQL = " select relationshipmanager_gid,overalllimit_amount,clustermanager_gid,relationshipmanager_name,customerref_name,product_name from ocs_trn_tcadapplication where application_gid = '" + lsapplication_gid + "'";
                    //objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDatareader1.HasRows == true)
                    //{
                    //    ls_overalllimit_amount = objODBCDatareader1["overalllimit_amount"].ToString();
                    //    ls_clustermanager_gid = objODBCDatareader1["clustermanager_gid"].ToString();
                    //    ls_relationshipmanager_gid = objODBCDatareader1["relationshipmanager_gid"].ToString();
                    //    ls_relationshipmanager_name = objODBCDatareader1["relationshipmanager_name"].ToString();
                    //    ls_customerref_name = objODBCDatareader1["customerref_name"].ToString();
                    //    ls_product_name = objODBCDatareader1["product_name"].ToString();

                    //}
                    //objODBCDatareader1.Close();
                    //msSQL = " select employee_mobileno, employee_emailid, concat(c.user_firstname,' ',c.user_lastname,' || ',c.user_code) as cluster_name from hrm_mst_temployee b " +
                    // " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    // "  where employee_gid = '" + ls_clustermanager_gid + "'";
                    //objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDatareader1.HasRows == true)
                    //{
                    //    lsemployee_mobileno = objODBCDatareader1["employee_mobileno"].ToString();
                    //    lsemployee_emailid = objODBCDatareader1["employee_emailid"].ToString();
                    //    lscluster_name = objODBCDatareader1["cluster_name"].ToString();


                    //}
                    //objODBCDatareader1.Close();



                    //    msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + ls_relationshipmanager_gid + "'";
                    //string cc_employee_emailid = objdbconn.GetExecuteScalar(msSQL);

                    //msSQL = " select institution_gid from  ocs_mst_tinstitution where stakeholder_type = 'Applicant' and application_gid = '" + lsapplication_gid + "'";
                    //objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDatareader1.HasRows == true)
                    //{
                    //    ls_institution_gid = objODBCDatareader1["institution_gid"].ToString();

                    //    msSQL = "select email_address from ocs_mst_tinstitution2email where primary_status = 'Yes' and institution_gid ='" + ls_institution_gid + "'";
                    //    lsemail_toaddress = objdbconn.GetExecuteScalar(msSQL);

                    //}
                    //else
                    //{

                    //    msSQL = " select contact_gid from  ocs_mst_tcontact where stakeholder_type = 'Applicant' and application_gid = '" + lsapplication_gid + "'";
                    //    string lscontact_gid = objdbconn.GetExecuteScalar(msSQL);

                    //    msSQL = "select email_address from ocs_mst_tcontact2email where primary_status = 'Yes' and contact_gid ='" + lscontact_gid + "'";
                    //    lsemail_toaddress = objdbconn.GetExecuteScalar(msSQL);

                    //}
                    //tomail_id1 = lsemail_toaddress;


                    //sub1 = "  Application Status ";
                    ////body = "<style>table, th, td {border: 1px solid black;border-collapse: collapse;}</style>";
                    ////body = body + "<table style='border-right: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;'><tr><td style='border-right-color:white;align:center;'>";
                    //body1 = body1 + "<br />";
                    ////body = body + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <img style='height:150px; width:380px;' src='" + lssource + "'><br />";
                    ////body = body + "<br />";
                    //body1 = body1 + " &nbsp&nbsp Dear Customer,<br />";
                    //body1 = body1 + "<br />";

                    //body1 = body1 + "&nbsp&nbsp We are pleased to inform you that you have been sanctioned INR(" + ls_sanction_amount + ").<br />";
                    //body1 = body1 + "<br />";
                    //body1 = body1 + "&nbsp&nbsp Reach out to your relationship manager (" + ls_relationshipmanager_name + ") for initiating sanction documentation.<br />";
                    //body1 = body1 + "<br />";
                    //body1 = body1 + "&nbsp&nbsp Regards,";
                    //body1 = body1 + "<br />";
                    //body1 = body1 + "&nbsp&nbsp Samunnati<br /> ";
                    //body1 = body1 + "<br />";
                    //body1 = body1 + "&nbsp&nbsp Note: If you are not able to reach out to the RM, please contact (" + lscluster_name + "/" + lsemployee_emailid + "/ " + lsemployee_mobileno + ")<br /> ";
                    //body1 = body1 + "<br />";
                    //    //body = body + "</td><td>&nbsp&nbsp</td></tr></table>";
                    //    MailMessage message1 = new MailMessage();
                    //SmtpClient smtp1 = new SmtpClient();
                    //message1.From = new MailAddress(ls_username);
                    //message1.To.Add(new MailAddress(tomail_id1));
                    //lsBccmail_id = ConfigurationManager.AppSettings["ApprovalBccMail"].ToString();

                    //    System.Net.Mail.Attachment attachment;
                    //    attachment = new System.Net.Mail.Attachment(ls_makerfile_path);
                    //    attachment.Name = ls_makerfile_name;
                    //    message1.Attachments.Add(attachment);

                    //    if (lsBccmail_id != null & lsBccmail_id != string.Empty & lsBccmail_id != "")
                    //{
                    //    lsBCCReceipients = lsBccmail_id.Split(',');
                    //    if (lsBccmail_id.Length == 0)
                    //    {
                    //        message1.Bcc.Add(new MailAddress(lsBccmail_id));
                    //    }
                    //    else
                    //    {
                    //        foreach (string BCCEmail in lsBCCReceipients)
                    //        {
                    //            message1.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id
                    //        }
                    //    }
                    //}
                    //cc_mailid = "" + cc_employee_emailid + "";

                    //if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                    //{
                    //    lsCCReceipients = cc_mailid.Split(',');
                    //    if (cc_mailid.Length == 0)
                    //    {
                    //        message1.CC.Add(new MailAddress(cc_mailid));
                    //    }
                    //    else
                    //    {
                    //        foreach (string CCEmail in lsCCReceipients)
                    //        {
                    //            message1.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                    //        }
                    //    }
                    //}

                    //message1.Subject = sub1;
                    //message1.IsBodyHtml = true; //to make message body as html  
                    //message1.Body = body1;
                    //smtp1.Port = ls_port;
                    //smtp1.Host = ls_server; //for gmail host  
                    //smtp1.EnableSsl = true;
                    //smtp1.UseDefaultCredentials = false;
                    //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    //smtp1.Credentials = new NetworkCredential(ls_username, ls_password);
                    //smtp1.DeliveryMethod = SmtpDeliveryMethod.Network;
                    //smtp1.Send(message1);


                    //}
                    //catch (Exception ex)
                    //{
                    //    values.message = ex.ToString();
                    //    values.status = false;

                    //}
                    values.message = "Sanction Approved Successfully";
                    values.status = true;
                }
                else
                {
                    values.message = "Sanction Rejected Successfully";
                    values.status = true;
                }
            }
            else
            {
                values.message = "Error Occrued";
                values.status = false;
            }
        }

        public static string NumberToWords1(int number)
        {
            if (number == 0) { return "zero"; }
            if (number < 0) { return "minus " + NumberToWords1(Math.Abs(number)); }
            string words = "";
            if ((number / 10000000) > 0) { words += NumberToWords1(number / 10000000) + " Crore "; number %= 10000000; }
            if ((number / 100000) > 0) { words += NumberToWords1(number / 100000) + " Lakh "; number %= 100000; }
            if ((number / 1000) > 0) { words += NumberToWords1(number / 1000) + " Thousand "; number %= 1000; }
            if ((number / 100) > 0) { words += NumberToWords1(number / 100) + " Hundred "; number %= 100; }
            if (number > 0)
            {
                if (words != "") { words += "and "; }
                var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "seventy", "Eighty", "Ninety" };
                if (number < 20) { words += unitsMap[number]; }
                else { words += tensMap[number / 10]; if ((number % 10) > 0) { words += "-" + unitsMap[number % 10]; } }
            }
            return words;
        }

        public string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }

        public void DaGetApplicationBasicView(string application_gid, MdlMstApplicationView values)
        {
            try
            {
                msSQL = " select a.application_gid, a.application_no, customer_urn, customerref_name as customer_name, vertical_name, verticaltaggs_name, " +
                        " constitution_name, businessunit_name, vernacular_language, sa_status, sa_id, sa_name, a.social_capital, a.trade_capital," +
                        " designation_type, landline_no, concat_ws(' ', contactpersonfirst_name, contactpersonmiddle_name, contactpersonlast_name) as contactperson_name," +
                        " GROUP_CONCAT(distinct(b.primaryvaluechain_name) SEPARATOR ', ') as primaryvaluechain_name, a.region, " +
                        " date_format(a.headapproval_date,'%d-%m-%Y %h:%i %p') as businessapproved_date, date_format(a.cccompleted_date,'%d-%m-%Y %h:%i %p') as ccapproved_date," +
                        " GROUP_CONCAT(distinct(c.secondaryvaluechain_name) SEPARATOR ', ') as secondaryvaluechain_name,momapproval_flag,approval_status,creditgroup_name, " +
                        " docchecklist_makerflag,docchecklist_checkerflag,docchecklist_approvalflag,product_gid,product_name, " +
                        " sector_name,category_name,variety_gid,variety_name,botanical_name,alternative_name,program_gid,program_name, " +
                        " case when e.urn = '' then d.urn else e.urn end as customer_urnno from ocs_trn_tcadapplication a" +
                        " left join ocs_mst_tapplication2primaryvaluechain b on b.application_gid = a.application_gid " +
                        " left join ocs_mst_tapplication2secondaryvaluechain c on c.application_gid = a.application_gid " +
                        " left join ocs_trn_tcadinstitution d on d.application_gid = a.application_gid " +
                        " left join ocs_trn_tcadcontact e on e.application_gid = a.application_gid " +
                        " where a.application_gid='" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.application_no = objODBCDatareader["application_no"].ToString();
                    values.customer_urn = objODBCDatareader["customer_urn"].ToString();
                    values.customer_name = objODBCDatareader["customer_name"].ToString();
                    values.vertical_name = objODBCDatareader["vertical_name"].ToString();
                    values.verticaltaggs_name = objODBCDatareader["verticaltaggs_name"].ToString();
                    values.constitution_name = objODBCDatareader["constitution_name"].ToString();
                    values.businessunit_name = objODBCDatareader["businessunit_name"].ToString();
                    values.vernacular_language = objODBCDatareader["vernacular_language"].ToString();
                    values.sa_status = objODBCDatareader["sa_status"].ToString();
                    values.sa_id = objODBCDatareader["sa_id"].ToString();
                    values.sa_name = objODBCDatareader["sa_name"].ToString();
                    values.landline_no = objODBCDatareader["landline_no"].ToString();
                    values.designation_type = objODBCDatareader["designation_type"].ToString();
                    values.contactperson_name = objODBCDatareader["contactperson_name"].ToString();
                    values.primaryvaluechain_name = objODBCDatareader["primaryvaluechain_name"].ToString();
                    values.secondaryvaluechain_name = objODBCDatareader["secondaryvaluechain_name"].ToString();
                    values.social_capital = objODBCDatareader["social_capital"].ToString();
                    values.trade_capital = objODBCDatareader["trade_capital"].ToString();
                    values.momapproval_flag = objODBCDatareader["momapproval_flag"].ToString();
                    values.approval_status = objODBCDatareader["approval_status"].ToString();
                    values.creditgroup_name = objODBCDatareader["creditgroup_name"].ToString();
                    values.businessapproved_date = objODBCDatareader["businessapproved_date"].ToString();
                    values.ccapproved_date = objODBCDatareader["ccapproved_date"].ToString();
                    values.region = objODBCDatareader["region"].ToString();
                    values.docchecklist_makerflag = objODBCDatareader["docchecklist_makerflag"].ToString();
                    values.docchecklist_checkerflag = objODBCDatareader["docchecklist_checkerflag"].ToString();
                    values.docchecklist_approvalflag = objODBCDatareader["docchecklist_approvalflag"].ToString();
                    values.product_gid = objODBCDatareader["product_gid"].ToString();
                    values.product_name = objODBCDatareader["product_name"].ToString();
                    values.sector_name = objODBCDatareader["sector_name"].ToString();
                    values.category_name = objODBCDatareader["category_name"].ToString();
                    values.variety_gid = objODBCDatareader["variety_gid"].ToString();
                    values.variety_name = objODBCDatareader["variety_name"].ToString();
                    values.botanical_name = objODBCDatareader["botanical_name"].ToString();
                    values.alternative_name = objODBCDatareader["alternative_name"].ToString();
                    values.program_gid = objODBCDatareader["program_gid"].ToString();
                    values.program_name = objODBCDatareader["program_name"].ToString();
                    values.customer_urnno = objODBCDatareader["customer_urnno"].ToString();
                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();

                msSQL = " select application_gid from ocs_trn_tcadinstitution " +
                        " where application_gid='" + application_gid + "' and (stakeholder_type='Applicant' or stakeholder_type='Borrower')";
                lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

                if (lsapplication_gid != "")
                {
                    values.borrower_flag = "Y";
                    values.borrower_type = "Institution";
                }
                else
                {
                    msSQL = " select application_gid from ocs_trn_tcadcontact " +
                            " where application_gid='" + application_gid + "' and (stakeholder_type='Applicant' or stakeholder_type='Borrower')";
                    lsapplication_gidcontact = objdbconn.GetExecuteScalar(msSQL);
                    if (lsapplication_gidcontact != "")
                    {
                        values.borrower_flag = "N";
                        values.borrower_type = "Individual";
                    }
                    else
                    {
                        values.borrower_type = "";
                        values.borrower_flag = "";
                    }
                }

                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }



        public void DaGetIndividualList(string application_gid, MdlCreditView values)
        {
            msSQL = " select a.contact_gid, a.application_gid, concat_ws(' ', first_name, last_name, middle_name) as individual_name, " +
                      " a.pan_no, aadhar_no, date_format(individual_dob, '%d-%m-%Y') as individual_dob,a.age,designation_name," +
                      " main_occupation, date_format(a.created_date, '%d-%m-%Y') as created_date, stakeholder_type,institution_name,credit_status, " +
                      " (select count(*) from ocs_trn_tcadgroupdocumentchecklist where credit_gid =a.contact_gid " +
                      " and(untagged_type is null or untagged_type = 'N')) as OverallDeferralcount, " +
                      " (select count(*) from ocs_trn_tcadgroupcovenantdocumentchecklist where credit_gid = a.contact_gid " +
                      " and(untagged_type is null or untagged_type = 'N')) as overallCovenantCount, " +
                      " (select count(*) from ocs_trn_tcadgroupdocumentchecklist where credit_gid = a.contact_gid " +
                      " and(untagged_type is null or untagged_type = 'N') and overall_docstatus in ('Waived','Satisfied')) as verifieddeferraldoc, " +
                      " (select count(*) from ocs_trn_tcadgroupcovenantdocumentchecklist where credit_gid = a.contact_gid " +
                      " and(untagged_type is null or untagged_type = 'N') and overall_docstatus in ('Waived','Satisfied'))  as verifiedcovenantdoc, " +
                     " (select count(tagquery_gid) from ocs_trn_ttagquery a  " +
                     " left join ocs_trn_tcadgroupcovenantdocumentchecklist c on a.groupdocumentchecklist_gid=c.groupcovdocumentchecklist_gid " +
                     " and a.application_gid=c.application_gid  " +
                     " left join ocs_trn_tcadgroupdocumentchecklist b on a.groupdocumentchecklist_gid=b.groupdocumentchecklist_gid " +
                     " and a.application_gid=b.application_gid   " +
                     " where  (b.credit_gid = a.contact_gid or c.credit_gid = a.contact_gid)  and fromphysical_document='N'  and (query_status!='Closed' or query_status!='Cancelled')) as QueryPendingCount, " +
                     " (select count(tagquery_gid) from ocs_trn_ttagquery a  " +
                     " left join ocs_trn_tcadgroupcovenantdocumentchecklist c on a.groupdocumentchecklist_gid=c.groupcovdocumentchecklist_gid " +
                     " and a.application_gid=c.application_gid  " +
                     " left join ocs_trn_tcadgroupdocumentchecklist b on a.groupdocumentchecklist_gid=b.groupdocumentchecklist_gid " +
                     " and a.application_gid=b.application_gid   " +
                     " where  (b.credit_gid = a.contact_gid or c.credit_gid = a.contact_gid)  and fromphysical_document='N' and ( query_status='Closed' or query_status='Cancelled')) as QueryClosedCount " +
                      " from ocs_trn_tcadcontact a" +
                      //" left join hrm_mst_temployee b on b.employee_gid = a.created_by" +
                      //" left join adm_mst_tuser c on b.user_gid = c.user_gid" +
                      " where application_gid = '" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getindividualList = new List<individual_List>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getindividualList.Add(new individual_List
                    {
                        contact_gid = (dr_datarow["contact_gid"].ToString()),
                        individual_name = (dr_datarow["individual_name"].ToString()),
                        pan_no = (dr_datarow["pan_no"].ToString()),
                        aadhar_no = (dr_datarow["aadhar_no"].ToString()),
                        individual_dob = (dr_datarow["individual_dob"].ToString()),
                        main_occupation = (dr_datarow["main_occupation"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        //created_by = (dr_datarow["created_by"].ToString()),
                        stakeholder_type = (dr_datarow["stakeholder_type"].ToString()),
                        company_name = (dr_datarow["institution_name"].ToString()),
                        credit_status = (dr_datarow["credit_status"].ToString()),
                        age = (dr_datarow["age"].ToString()),
                        designation_name = (dr_datarow["designation_name"].ToString()),
                        overallCovenantCount = (dr_datarow["overallCovenantCount"].ToString()),
                        OverallDeferralcount = (dr_datarow["OverallDeferralcount"].ToString()),
                        verifieddeferraldoc = (dr_datarow["verifieddeferraldoc"].ToString()),
                        verifiedcovenantdoc = (dr_datarow["verifiedcovenantdoc"].ToString()),
                        QueryPendingCount = (dr_datarow["QueryPendingCount"].ToString()),
                        QueryClosedCount = (dr_datarow["QueryClosedCount"].ToString()),
                    });
                }
                values.individual_List = getindividualList;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetInstitutionList(string application_gid, MdlCreditView values)
        {
            msSQL = " select a.institution_gid, a.application_gid, cin_no, companytype_name, " +
                     " company_name, companypan_no, date_format(date_incorporation, '%d-%m-%Y') as date_incorporation," +
                     " date_format(a.created_date, '%d-%m-%Y') as created_date, stakeholder_type,credit_status, " +
                     " (select count(*) from ocs_trn_tcadgroupdocumentchecklist where credit_gid =a.institution_gid " +
                     " and(untagged_type is null or untagged_type = 'N')) as OverallDeferralcount, " +
                     " (select count(*) from ocs_trn_tcadgroupcovenantdocumentchecklist where credit_gid = a.institution_gid " +
                     " and(untagged_type is null or untagged_type = 'N')) as overallCovenantCount, " +
                     " (select count(*) from ocs_trn_tcadgroupdocumentchecklist where credit_gid = a.institution_gid " +
                     " and(untagged_type is null or untagged_type = 'N') and overall_docstatus in ('Waived','Satisfied')) as verifieddeferraldoc, " +
                     " (select count(*) from ocs_trn_tcadgroupcovenantdocumentchecklist where credit_gid = a.institution_gid " +
                     " and(untagged_type is null or untagged_type = 'N') and overall_docstatus in ('Waived','Satisfied'))  as verifiedcovenantdoc, " +
                     " (select count(tagquery_gid) from ocs_trn_ttagquery a  " +
                     " left join ocs_trn_tcadgroupcovenantdocumentchecklist c on a.groupdocumentchecklist_gid=c.groupcovdocumentchecklist_gid " +
                     " and a.application_gid=c.application_gid  " +
                     " left join ocs_trn_tcadgroupdocumentchecklist b on a.groupdocumentchecklist_gid=b.groupdocumentchecklist_gid " +
                     " and a.application_gid=b.application_gid   " +
                     " where  (b.credit_gid = a.institution_gid or c.credit_gid = a.institution_gid) and fromphysical_document='N' and (query_status!='Closed' or query_status!='Cancelled')) as QueryPendingCount, " +
                     " (select count(tagquery_gid) from ocs_trn_ttagquery a  " +
                     " left join ocs_trn_tcadgroupcovenantdocumentchecklist c on a.groupdocumentchecklist_gid=c.groupcovdocumentchecklist_gid " +
                     " and a.application_gid=c.application_gid  " +
                     " left join ocs_trn_tcadgroupdocumentchecklist b on a.groupdocumentchecklist_gid=b.groupdocumentchecklist_gid " +
                     " and a.application_gid=b.application_gid   " +
                     " where  (b.credit_gid = a.institution_gid or c.credit_gid = a.institution_gid) and fromphysical_document='N' and ( query_status='Closed' or query_status='Cancelled')) as QueryClosedCount " +
                     " from ocs_trn_tcadinstitution a" +
                     //" left join hrm_mst_temployee b on b.employee_gid = a.created_by" +
                     //" left join adm_mst_tuser c on b.user_gid = c.user_gid" +
                     " where application_gid = '" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getinstitutionList = new List<institution_List>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getinstitutionList.Add(new institution_List
                    {
                        institution_gid = (dr_datarow["institution_gid"].ToString()),
                        company_name = (dr_datarow["company_name"].ToString()),
                        companypan_no = (dr_datarow["companypan_no"].ToString()),
                        cin_no = (dr_datarow["cin_no"].ToString()),
                        companytype_name = (dr_datarow["companytype_name"].ToString()),
                        date_incorporation = (dr_datarow["date_incorporation"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        //created_by = (dr_datarow["created_by"].ToString()),
                        stakeholder_type = (dr_datarow["stakeholder_type"].ToString()),
                        credit_status = (dr_datarow["credit_status"].ToString()),
                        overallCovenantCount = (dr_datarow["overallCovenantCount"].ToString()),
                        OverallDeferralcount = (dr_datarow["OverallDeferralcount"].ToString()),
                        verifieddeferraldoc = (dr_datarow["verifieddeferraldoc"].ToString()),
                        verifiedcovenantdoc = (dr_datarow["verifiedcovenantdoc"].ToString()),
                        QueryPendingCount = (dr_datarow["QueryPendingCount"].ToString()),
                        QueryClosedCount = (dr_datarow["QueryClosedCount"].ToString()),
                    });
                }
                values.institution_List = getinstitutionList;
            }
            dt_datatable.Dispose();
            values.status = true;
        }


        public void DaGetGroupSummary(string application_gid, MdlMstGroup values)
        {
            msSQL = " select a.group_gid,a.group_name,date_format(a.date_of_formation,'%d-%m-%Y') as date_of_formation,a.group_status, a.group_type," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,credit_status," +
                    " (select count(*) from ocs_trn_tcadgroupdocumentchecklist where credit_gid =a.group_gid " +
                    " and(untagged_type is null or untagged_type = 'N')) as OverallDeferralcount, " +
                    " (select count(*) from ocs_trn_tcadgroupcovenantdocumentchecklist where credit_gid = a.group_gid " +
                    " and(untagged_type is null or untagged_type = 'N')) as overallCovenantCount, " +
                    " (select count(*) from ocs_trn_tcadgroupdocumentchecklist where credit_gid = a.group_gid " +
                    " and(untagged_type is null or untagged_type = 'N') and overall_docstatus in ('Waived','Satisfied')) as verifieddeferraldoc, " +
                    " (select count(*) from ocs_trn_tcadgroupcovenantdocumentchecklist where credit_gid = a.group_gid " +
                    " and(untagged_type is null or untagged_type = 'N') and overall_docstatus in ('Waived','Satisfied'))  as verifiedcovenantdoc, " +
                     " (select count(tagquery_gid) from ocs_trn_ttagquery a  " +
                     " left join ocs_trn_tcadgroupcovenantdocumentchecklist c on a.groupdocumentchecklist_gid=c.groupcovdocumentchecklist_gid " +
                     " and a.application_gid=c.application_gid  " +
                     " left join ocs_trn_tcadgroupdocumentchecklist b on a.groupdocumentchecklist_gid=b.groupdocumentchecklist_gid " +
                     " and a.application_gid=b.application_gid   " +
                     " where  (b.credit_gid = a.group_gid or c.credit_gid = a.group_gid)  and fromphysical_document='N' and query_status!='Closed') as QueryPendingCount, " +
                     " (select count(tagquery_gid) from ocs_trn_ttagquery a  " +
                     " left join ocs_trn_tcadgroupcovenantdocumentchecklist c on a.groupdocumentchecklist_gid=c.groupcovdocumentchecklist_gid " +
                     " and a.application_gid=c.application_gid  " +
                     " left join ocs_trn_tcadgroupdocumentchecklist b on a.groupdocumentchecklist_gid=b.groupdocumentchecklist_gid " +
                     " and a.application_gid=b.application_gid   " +
                     " where  (b.credit_gid = a.group_gid or c.credit_gid = a.group_gid) and fromphysical_document='N' and query_status='Closed') as QueryClosedCount " +
                    " from ocs_trn_tcadgroup a " +
                    //" left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    //" left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " where application_gid='" + application_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getgroup_list = new List<group_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getgroup_list.Add(new group_list
                    {
                        group_gid = (dr_datarow["group_gid"].ToString()),
                        group_name = (dr_datarow["group_name"].ToString()),
                        date_of_formation = (dr_datarow["date_of_formation"].ToString()),
                        //created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        group_status = (dr_datarow["group_status"].ToString()),
                        group_type = (dr_datarow["group_type"].ToString()),
                        credit_status = (dr_datarow["credit_status"].ToString()),
                        overallCovenantCount = (dr_datarow["overallCovenantCount"].ToString()),
                        OverallDeferralcount = (dr_datarow["OverallDeferralcount"].ToString()),
                        verifieddeferraldoc = (dr_datarow["verifieddeferraldoc"].ToString()),
                        verifiedcovenantdoc = (dr_datarow["verifiedcovenantdoc"].ToString()),
                        QueryPendingCount = (dr_datarow["QueryPendingCount"].ToString()),
                        QueryClosedCount = (dr_datarow["QueryClosedCount"].ToString()),
                    });
                }
            }
            values.group_list = getgroup_list;
            dt_datatable.Dispose();
        }


        public void DaGetGrouptoMemberList(string group_gid, MdlMstGroupMember values)
        {
            msSQL = " select a.contact_gid,a.pan_no,a.aadhar_no,concat(first_name, ' ',middle_name,' ',last_name) as individual_name,stakeholder_type,credit_status," +
                    " (select count(*) from ocs_trn_tcadgroupdocumentchecklist where credit_gid =a.contact_gid " +
                    " and(untagged_type is null or untagged_type = 'N')) as OverallDeferralcount, " +
                    " (select count(*) from ocs_trn_tcadgroupcovenantdocumentchecklist where credit_gid = a.contact_gid " +
                    " and(untagged_type is null or untagged_type = 'N')) as overallCovenantCount, " +
                    " (select count(*) from ocs_trn_tcadgroupdocumentchecklist where credit_gid = a.contact_gid " +
                    " and(untagged_type is null or untagged_type = 'N') and overall_docstatus in ('Waived','Satisfied')) as verifieddeferraldoc, " +
                    " (select count(*) from ocs_trn_tcadgroupcovenantdocumentchecklist where credit_gid = a.contact_gid " +
                    " and(untagged_type is null or untagged_type = 'N') and overall_docstatus in ('Waived','Satisfied'))  as verifiedcovenantdoc, " +
                     " (select count(tagquery_gid) from ocs_trn_ttagquery a  " +
                     " left join ocs_trn_tcadgroupcovenantdocumentchecklist c on a.groupdocumentchecklist_gid=c.groupcovdocumentchecklist_gid " +
                     " and a.application_gid=c.application_gid  " +
                     " left join ocs_trn_tcadgroupdocumentchecklist b on a.groupdocumentchecklist_gid=b.groupdocumentchecklist_gid " +
                     " and a.application_gid=b.application_gid   " +
                     " where  (b.credit_gid = a.institution_gid or c.credit_gid = a.institution_gid) and fromphysical_document='N' and query_status!='Closed') as QueryPendingCount, " +
                     " (select count(tagquery_gid) from ocs_trn_ttagquery a  " +
                     " left join ocs_trn_tcadgroupcovenantdocumentchecklist c on a.groupdocumentchecklist_gid=c.groupcovdocumentchecklist_gid " +
                     " and a.application_gid=c.application_gid  " +
                     " left join ocs_trn_tcadgroupdocumentchecklist b on a.groupdocumentchecklist_gid=b.groupdocumentchecklist_gid " +
                     " and a.application_gid=b.application_gid   " +
                     " where  (b.credit_gid = a.institution_gid or c.credit_gid = a.institution_gid) and fromphysical_document='N' and query_status='Closed') as QueryClosedCount " +
                    " from ocs_trn_tcadcontact a " +
                    " where group_gid='" + group_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getgroupmember_list = new List<groupmember_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getgroupmember_list.Add(new groupmember_list
                    {
                        contact_gid = (dr_datarow["contact_gid"].ToString()),
                        individual_name = (dr_datarow["individual_name"].ToString()),
                        pan_no = (dr_datarow["pan_no"].ToString()),
                        aadhar_no = (dr_datarow["aadhar_no"].ToString()),
                        stakeholder_type = (dr_datarow["stakeholder_type"].ToString()),
                        credit_status = (dr_datarow["credit_status"].ToString()),
                        overallCovenantCount = (dr_datarow["overallCovenantCount"].ToString()),
                        OverallDeferralcount = (dr_datarow["OverallDeferralcount"].ToString()),
                        verifieddeferraldoc = (dr_datarow["verifieddeferraldoc"].ToString()),
                        verifiedcovenantdoc = (dr_datarow["verifiedcovenantdoc"].ToString()),
                        QueryPendingCount = (dr_datarow["QueryPendingCount"].ToString()),
                        QueryClosedCount = (dr_datarow["QueryClosedCount"].ToString()),
                    });
                }
            }
            values.groupmember_list = getgroupmember_list;
            dt_datatable.Dispose();
        }

        public void DaPostDocChecklistMakerSubmit(string employee_gid, MdlDocChecklistdetails values)
        {
            msSQL = " select maker_gid from ocs_trn_tprocesstype_assign a " +
                  " where a.processtype_name = 'Accept' and a.menu_gid = 'CADMGTDCL' and a.application_gid = '" + values.application_gid + "'";
            values.maker_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select checker_gid from ocs_trn_tprocesstype_assign a " +
                   " where a.processtype_name = 'Accept' and a.menu_gid = 'CADMGTDCL' and a.application_gid = '" + values.application_gid + "'";
            values.checker_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select approver_gid from ocs_trn_tprocesstype_assign a " +
                   " where a.processtype_name = 'Accept' and a.menu_gid = 'CADMGTDCL' and a.application_gid = '" + values.application_gid + "'";
            values.approver_gid = objdbconn.GetExecuteScalar(msSQL);

            if (values.maker_gid == values.checker_gid)
            {
                if (values.maker_gid == values.approver_gid)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("A2DC");
                    msSQL = " insert into ocs_trn_tapplication2docchecklist(" +
                            " application2docchecklist_gid ," +
                            " application_gid," +
                            " application_no," +
                            " customer_name ," +
                            " makersubmitted_by," +
                            " makersubmitted_on ," +
                            " maker_flag," +
                            " checkerapproved_by," +
                            " checkerapproved_on," +
                            " checker_flag," +
                            " approved_by," +
                            " approved_on," +
                            " approval_flag," +
                            " approval_status," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + values.application_gid + "'," +
                            "'" + values.application_no + "'," +
                            "'" + values.customer_name.Replace("'", "") + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'Y'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'Y'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'Y'," +
                            "'Approved'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        msSQL = " update ocs_trn_tcadapplication set  docchecklist_makerflag='Y',docchecklist_checkerflag='Y',docchecklist_approvalflag='Y' where application_gid='" + values.application_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update ocs_trn_tprocesstype_assign set maker_approvalflag='Y', maker_approveddate= NOW()," +
                                " checker_approvalflag='Y', checker_approveddate= NOW(), " +
                                " approver_approvalflag ='Y', approver_approveddate= NOW() " +
                                " where application_gid='" + values.application_gid + "' and menu_gid='" + getMenuClass.DocumentChecklist + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        values.status = true;
                        values.message = "Document Checklist Approved Successfully";
                    }
                    else
                    {
                        values.message = "Error Occured";
                        values.status = false;
                    }
                }
                else
                {
                    msGetGid = objcmnfunctions.GetMasterGID("A2DC");
                    msSQL = " insert into ocs_trn_tapplication2docchecklist(" +
                            " application2docchecklist_gid ," +
                            " application_gid," +
                            " application_no," +
                            " customer_name ," +
                            " makersubmitted_by," +
                            " makersubmitted_on ," +
                            " maker_flag," +
                            " checkerapproved_by," +
                            " checkerapproved_on," +
                            " checker_flag," +
                            " approval_status," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + values.application_gid + "'," +
                            "'" + values.application_no + "'," +
                            "'" + values.customer_name.Replace("'", "") + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'Y'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'Y'," +
                            "'Approval Pending'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        msSQL = " update ocs_trn_tcadapplication set docchecklist_makerflag='Y',docchecklist_checkerflag='Y' where application_gid='" + values.application_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update ocs_trn_tprocesstype_assign set maker_approvalflag='Y', maker_approveddate= NOW(), " +
                                " checker_approvalflag='Y', checker_approveddate= NOW() " +
                                " where application_gid='" + values.application_gid + "' and menu_gid='" + getMenuClass.DocumentChecklist + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        values.status = true;
                        values.message = "Document Checklist Submitted for Approval";
                    }
                    else
                    {
                        values.message = "Error Occured While Adding";
                        values.status = false;
                    }
                }
            }
            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("A2DC");
                msSQL = " insert into ocs_trn_tapplication2docchecklist(" +
                        " application2docchecklist_gid ," +
                        " application_gid," +
                        " application_no," +
                        " customer_name ," +
                        " makersubmitted_by," +
                        " makersubmitted_on ," +
                        " maker_flag," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.application_gid + "'," +
                        "'" + values.application_no + "'," +
                        "'" + values.customer_name.Replace("'", "") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'Y'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msSQL = " update ocs_trn_tcadapplication set docchecklist_makerflag='Y' where application_gid='" + values.application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_trn_tprocesstype_assign set maker_approvalflag='Y', maker_approveddate= NOW() " +
                           " where application_gid='" + values.application_gid + "' and menu_gid='" + getMenuClass.DocumentChecklist + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Document Checklist Submitted for Checker";
                }
                else
                {
                    values.message = "Error Occured While Adding";
                    values.status = false;
                }
            }
        }

        public void DaPostDocChecklistCheckerSubmit(string employee_gid, MdlDocChecklistdetails values)
        {
            msSQL = " select maker_gid from ocs_trn_tprocesstype_assign a " +
                   " where a.processtype_name = 'Accept' and a.menu_gid = 'CADMGTDCL' and a.application_gid = '" + values.application_gid + "'";
            values.maker_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select checker_gid from ocs_trn_tprocesstype_assign a " +
                   " where a.processtype_name = 'Accept' and a.menu_gid = 'CADMGTDCL' and a.application_gid = '" + values.application_gid + "'";
            values.checker_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select approver_gid from ocs_trn_tprocesstype_assign a " +
                   " where a.processtype_name = 'Accept' and a.menu_gid = 'CADMGTDCL' and a.application_gid = '" + values.application_gid + "'";
            values.approver_gid = objdbconn.GetExecuteScalar(msSQL);

            if (values.checker_gid == values.approver_gid)
            {
                msSQL = " update ocs_trn_tapplication2docchecklist set checker_flag = 'Y'," +
                        " checkerapproved_by='" + employee_gid + "', checkerapproved_on = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                        " approved_by='" + employee_gid + "', approved_on = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                        " approval_status = 'Approved', " +
                        " approval_flag = 'Y' " +
                        " where application_gid='" + values.application_gid + "'";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msSQL = " update ocs_trn_tcadapplication set docchecklist_checkerflag='Y',docchecklist_approvalflag='Y' where application_gid='" + values.application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_trn_tprocesstype_assign set checker_approvalflag='Y', checker_approveddate= NOW(), " +
                            " approver_approvalflag='Y', approver_approveddate= NOW() " +
                            " where application_gid='" + values.application_gid + "' and menu_gid='" + getMenuClass.DocumentChecklist + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "Document Checklist Approved Successfully";
                }
                else
                {
                    values.message = "Error Occured While Adding";
                    values.status = false;
                }
            }
            else
            {
                msSQL = " update ocs_trn_tapplication2docchecklist set checkerapproved_by='" + employee_gid + "', " +
                        " checkerapproved_on = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                        " approval_status = 'Approval Pending', " +
                        " checker_flag = 'Y' " +
                        " where application_gid='" + values.application_gid + "'";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msSQL = " update ocs_trn_tcadapplication set docchecklist_checkerflag='Y' where application_gid='" + values.application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_trn_tprocesstype_assign set checker_approvalflag='Y', checker_approveddate= NOW()" +
                            " where application_gid='" + values.application_gid + "' and menu_gid='" + getMenuClass.DocumentChecklist + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "Document Checklist Submitted for Approval";
                }
                else
                {
                    values.message = "Error Occured While Adding";
                    values.status = false;
                }
            }
        }

        public void DaPostDocChecklistApproval(string employee_gid, MdlDocChecklistdetails values)
        {
            msSQL = " update ocs_trn_tapplication2docchecklist set approved_by='" + employee_gid + "', " +
                    " approved_on = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                    " approval_status = 'Approved', " +
                    " approval_flag = 'Y' " +
                    " where application_gid='" + values.application_gid + "'";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = " update ocs_trn_tcadapplication set docchecklist_approvalflag='Y' where application_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_trn_tprocesstype_assign set approver_approvalflag='Y', approver_approveddate= NOW()" +
                        " where application_gid='" + values.application_gid + "' and menu_gid='" + getMenuClass.DocumentChecklist + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Document Checklist Approved Successfully";
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;
            }
        }

        public bool DaSanctionCommonTemplate(cadtemplate_list values)
        {

            //Get Template Content
            msSQL = " select  a.template_content from adm_mst_ttemplate a " +
              " where a.template_gid='" + values.template_gid + "'";
            lstemplate_content = objdbconn.GetExecuteScalar(msSQL);

            lscontent = lstemplate_content;
            string lsapplication_gid = "", lsapplication_no = "", lssanction_date = "", lssanctionto_name = "";
            string lssanctiontill_date = "", lsonemonthlesser_sanctiontilldate = "", lscontactpersonaddress_gid = "";
            msSQL = " select a.application_gid, a.sanction_refno, a.application_name, a.ccapproved_date, a.validity_months ,b.application_no, " +
                    " date_format(a.sanction_date, '%d %b %Y') as sanction_date,a.sanctionto_name,a.contactpersonaddress_gid, " +
                    "  a.contactperson_name, a.contactperson_number, a.contactpersonemail_address, a.contactperson_address, a.purpose_lending, b.relationshipmanager_gid," +
                    " b.relationshipmanager_name, c.employee_mobileno, c.employee_emailid, " +
                    " date_format(sanctiontill_date, '%d %b %Y') as sanctiontill_date, " +
                    " date_format(sanctiontill_date - INTERVAL 1 MONTH, '%d %b %Y') as onemonthlesser_sanctiontilldate " +
                    " from ocs_trn_tapplication2sanction a " +
                    " LEFT JOIN ocs_trn_tcadapplication b on a.application_gid = b.application_gid " +
                    " LEFT JOIN hrm_mst_temployee c ON c.employee_gid = b.relationshipmanager_gid " +
                    " where application2sanction_gid='" + values.sanction_gid + "'";
            objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader1.HasRows == true)
            {
                objODBCDataReader1.Read();
                values.sanction_refno = objODBCDataReader1["sanction_refno"].ToString();
                values.application_name = objODBCDataReader1["application_name"].ToString();
                lsapplication_gid = objODBCDataReader1["application_gid"].ToString();
                values.ccapproved_date = objODBCDataReader1["ccapproved_date"].ToString();
                values.address = objODBCDataReader1["contactperson_address"].ToString();
                values.mobileno = objODBCDataReader1["contactperson_number"].ToString();
                values.email = objODBCDataReader1["contactpersonemail_address"].ToString();
                values.contactperson = objODBCDataReader1["contactperson_name"].ToString();
                values.purpose_lending = objODBCDataReader1["purpose_lending"].ToString();
                values.validity_months = objODBCDataReader1["validity_months"].ToString();
                values.relationshipmgmt_name = objODBCDataReader1["relationshipmanager_name"].ToString();
                values.employee_mobileno = objODBCDataReader1["employee_mobileno"].ToString();
                values.employee_mailid = objODBCDataReader1["employee_emailid"].ToString();
                lsapplication_no = objODBCDataReader1["application_no"].ToString();
                lssanction_date = objODBCDataReader1["sanction_date"].ToString();
                lssanctionto_name = objODBCDataReader1["sanctionto_name"].ToString();
                lssanctiontill_date = objODBCDataReader1["sanctiontill_date"].ToString();
                lsonemonthlesser_sanctiontilldate = objODBCDataReader1["onemonthlesser_sanctiontilldate"].ToString();
                lscontactpersonaddress_gid = objODBCDataReader1["contactpersonaddress_gid"].ToString();
            }
            objODBCDataReader1.Close();
            values.contactperson = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(values.contactperson.ToLower());
            values.address = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(values.address.ToLower());
            string lscity = "", lsstate = "";
            msSQL = " select concat(city,' - ',postal_code) as city, state from ocs_trn_tcadcontact2address where contact2address_gid = '" + lscontactpersonaddress_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lscity = objODBCDatareader["city"].ToString();
                lsstate = objODBCDatareader["state"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select concat(city,' - ',postal_code) as city,state from ocs_trn_tcadinstitution2address where institution2address_gid = '" + lscontactpersonaddress_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lscity = objODBCDatareader["city"].ToString();
                lsstate = objODBCDatareader["state"].ToString();
            }
            objODBCDatareader.Close();
            lscity = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lscity.ToLower());
            lsstate = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lsstate.ToLower());
            lscontent = lscontent.Replace("city_postalcode", lscity);
            lscontent = lscontent.Replace("city_state", lsstate);
            lscontent = lscontent.Replace("contact_person", values.contactperson);
            lscontent = lscontent.Replace("mobile_no", values.mobileno + ",");
            lscontent = lscontent.Replace("address", values.address);
            lscontent = lscontent.Replace("ccapproved_date", values.ccapproved_date);
            lscontent = lscontent.Replace("email", values.email);
            lscontent = lscontent.Replace("application_name", values.application_name);
            lscontent = lscontent.Replace("sanction_refno", values.sanction_refno);
            lscontent = lscontent.Replace("validity_months", values.validity_months);
            lscontent = lscontent.Replace("relationshipmgmt_name", values.relationshipmgmt_name);
            lscontent = lscontent.Replace("employee_mobileno", values.employee_mobileno);
            lscontent = lscontent.Replace("employee_mailid", values.employee_mailid);
            lscontent = lscontent.Replace("sanction_date", lssanction_date);
            lscontent = lscontent.Replace("application_no", lsapplication_no);
            lscontent = lscontent.Replace("sanctiontill_date", lssanctiontill_date);
            lscontent = lscontent.Replace("onemonthlesser_sanctiontilldate", lsonemonthlesser_sanctiontilldate);

            msSQL = " SELECT  concat(@a:= @a + 1, '.', first_name, ' ', middle_name, '', last_name) as individualname " +
                    " FROM ocs_trn_tcadcontact, " +
                    " (SELECT @a:= 0) AS a where application_gid = '" + lsapplication_gid + "' and stakeholder_type = 'Guarantor'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getPersonalGuarantee = new List<PersonalGuarantee>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getPersonalGuarantee.Add(new PersonalGuarantee
                    {
                        Guarantor_name = (dr_datarow["individualname"].ToString()),
                    });
                }
            }
            dt_datatable.Dispose();
            string lsGuarantor_name = string.Join("<br>", getPersonalGuarantee.Select(u => u.Guarantor_name));
            lscontent = lscontent.Replace("lsguarantor_name", lsGuarantor_name);
            lscontent = lscontent.Replace("sanctionto_Borrower", lssanctionto_name.ToUpper());
            msSQL = "select applicant_type from ocs_trn_tcadapplication where application_gid='" + lsapplication_gid + "'";
            string lsapplicant_type = objdbconn.GetExecuteScalar(msSQL);
            string lsapplicant_name = "";
            if (lsapplicant_type == "Individual")
            {
                msSQL = " select concat(first_name,middle_name,last_name) as customer_name  " +
                        " from ocs_trn_tcadcontact where application_gid='" + lsapplication_gid + "' and stakeholder_type in ('Applicant','Borrower')";
                lsapplicant_name = objdbconn.GetExecuteScalar(msSQL);
            }

            else
            {
                lssanctionto_name = "M/s. " + lssanctionto_name.ToUpper();
                msSQL = " select  company_name from ocs_trn_tcadinstitution where " +
                       " application_gid='" + lsapplication_gid + "' and stakeholder_type in ('Applicant','Borrower')";
                lsapplicant_name = objdbconn.GetExecuteScalar(msSQL);
            }
            lscontent = lscontent.Replace("sanctionto_name", lssanctionto_name);

            lscontent = lscontent.Replace("lsBorrower_Name", lsapplicant_name.ToUpper());
            msSQL = " select processing_fee from ocs_trn_tcadapplicationservicecharge where application_gid='" + lsapplication_gid + "'";
            string lsprocessing_fee = objdbconn.GetExecuteScalar(msSQL);
            if (lsprocessing_fee != "")
            {
                double processingfee_amount = Convert.ToDouble(lsprocessing_fee);
                int processingamount = Convert.ToInt32(processingfee_amount);
                string processingamount_words = NumberToWords(processingamount);
                processingamount_words = "Rupees " + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(processingamount_words.ToLower());
                lscontent = lscontent.Replace("lsprocessingamount_words", processingamount_words);
            }
            else
            {
                lscontent = lscontent.Replace("lsprocessingamount_words", "");
            }

            lscontent = lscontent.Replace("lsprocessing_fee", lsprocessing_fee);

            string lscreditbankdtl = "";
            msSQL = " select bankaccount_name,bankaccount_number from ocs_trn_tcadcreditbankdtl where application_gid= '" + lsapplication_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    lscreditbankdtl += " with <b>" + dr_datarow["bankaccount_name"].ToString() + "</b> bearing Account Number <b>" + dr_datarow["bankaccount_number"].ToString() + "</b>,";
                }
                lscreditbankdtl = lscreditbankdtl.TrimEnd(',');
                lscontent = lscontent.Replace("lsSpecificcreditbankdtl", lscreditbankdtl);
            }
            else
            {
                lscontent = lscontent.Replace("lsSpecificcreditbankdtl", "with <b>XXXXXXXXXXXX</b> bearing Account Number <b>YYYYYYYYYYYYY</b> ");
            }
            dt_datatable.Dispose();

            msSQL = " select concat(product_type,'-', productsub_type) as product_type,format(loanfacility_amount,2) as facility_limit, " +
                " enduse_purpose as purposeofloan,facilityoverall_limit, facility_mode ,rate_interest as margin,tenureoverall_limit, " +
                " principalfrequency_gid, principalfrequency_name, interestfrequency_gid, interestfrequency_name " +
                " from ocs_trn_tcadapplication2loan where application_gid = '" + lsapplication_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getloanfacilitytype = new List<cadloanfacilitytype_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getloanfacilitytype.Add(new cadloanfacilitytype_list
                    {
                        loanfacility_amount = (dr_datarow["facility_limit"].ToString()),
                        loanfacility_type = (dr_datarow["product_type"].ToString()),
                        revolving_type = (dr_datarow["facility_mode"].ToString()),
                        tenure = (dr_datarow["tenureoverall_limit"].ToString()),
                        margin = (dr_datarow["margin"].ToString()),
                        loanTitle = (dr_datarow["purposeofloan"].ToString()),
                        total_documentlimit = (dr_datarow["facilityoverall_limit"].ToString()),
                        principalfrequency_gid = (dr_datarow["principalfrequency_gid"].ToString()),
                        principalfrequency_name = (dr_datarow["principalfrequency_name"].ToString()),
                        interestfrequency_gid = (dr_datarow["interestfrequency_gid"].ToString()),
                        interestfrequency_name = (dr_datarow["interestfrequency_name"].ToString()),
                    });
                }
                values.cadloanfacilitytype_list = getloanfacilitytype;
            }
            dt_datatable.Dispose();

            if (values.cadloanfacilitytype_list != null && values.cadloanfacilitytype_list.Count != 0)
            {
                int j = 1;
                for (int i = 0; i < values.cadloanfacilitytype_list.Count; i++)
                {

                    String loanfacilityamount = values.cadloanfacilitytype_list[i].loanfacility_amount;
                    double loanfacility_amount = Convert.ToDouble(loanfacilityamount);
                    int facilityamount = Convert.ToInt32(loanfacility_amount);
                    string facilityamount_words = NumberToWords1(facilityamount);

                    lscontent = lscontent.Replace("revolving_type" + j + "", values.cadloanfacilitytype_list[i].revolving_type);
                    lscontent = lscontent.Replace("tenure" + j + "", values.cadloanfacilitytype_list[i].tenure);
                    lscontent = lscontent.Replace("loanfacility_type" + j + "", values.cadloanfacilitytype_list[i].loanfacility_type);
                    lscontent = lscontent.Replace("loanfacility_amount" + j + "", values.cadloanfacilitytype_list[i].loanfacility_amount);
                    lscontent = lscontent.Replace("rateofinterest"+ j +"lbl", values.cadloanfacilitytype_list[i].margin);
                    lscontent = lscontent.Replace("purpose_lending" + j + "", values.cadloanfacilitytype_list[i].loanTitle);
                    lscontent = lscontent.Replace("facilityamount" + j + "_words", facilityamount_words);
                    lscontent = lscontent.Replace("facilityoverall_limit" + j + "", values.cadloanfacilitytype_list[i].total_documentlimit);
                    if (values.cadloanfacilitytype_list[i].principalfrequency_gid != "")
                    {
                        lscontent = lscontent.Replace("PrincipalFrequencyDtl" + j + "", "Principal Frequency: " + values.cadloanfacilitytype_list[i].principalfrequency_name);
                    }
                    else
                        lscontent = lscontent.Replace("PrincipalFrequencyDtl" + j + "", "");
                    if (values.cadloanfacilitytype_list[i].interestfrequency_gid != "")
                    {
                        string comma = "";
                        if (values.cadloanfacilitytype_list[i].principalfrequency_gid != "")
                            comma = ",";
                        lscontent = lscontent.Replace("InterestFrequencyDtl" + j + "", "" + comma + " Interest Frequency: " + values.cadloanfacilitytype_list[i].interestfrequency_name);
                    }
                    else
                        lscontent = lscontent.Replace("InterestFrequencyDtl" + j + "", "");

                    lscontent = lscontent.Replace("lblMode_Operation" + j + "", "");
                    lscontent = lscontent.Replace("margin" + j + "lbl", "Nil");
                    j++;
                }
                for (int i = values.cadloanfacilitytype_list.Count; i <= 5; i++)
                {
                    lscontent = lscontent.Replace("revolving_type" + i + "", "");
                    lscontent = lscontent.Replace("tenure" + i + "", "");
                    lscontent = lscontent.Replace("loanfacility_type" + i + "", "");
                    lscontent = lscontent.Replace("loanfacility_amount" + i + "", "");
                    lscontent = lscontent.Replace("margin" + i + "lbl", "");
                    lscontent = lscontent.Replace("purpose_lending" + i + "", "");
                    lscontent = lscontent.Replace("facilityamount" + i + "_words", "");
                    lscontent = lscontent.Replace("facilityoverall_limit" + i + "", "");
                    lscontent = lscontent.Replace("PrincipalFrequencyDtl" + i + "", "");
                    lscontent = lscontent.Replace("InterestFrequencyDtl" + i + "", "");
                    lscontent = lscontent.Replace("lblMode_Operation" + i + "", "");
                    lscontent = lscontent.Replace("rateofinterest" + i + "lbl", "");
                }
            }

            msSQL = " select (select sum(doc_charges)  from ocs_trn_tcadapplicationservicecharge " +
                    " where application_gid = '" + lsapplication_gid + "' and doccharge_collectiontype = 'Collect') as doccharge_Collect, " +
                    " (select sum(doc_charges)  from ocs_trn_tcadapplicationservicecharge " +
                    " where application_gid = '" + lsapplication_gid + "' and doccharge_collectiontype = 'Deduct') as doccharge_Deduct, " +
                    " (select sum(fieldvisit_charges)  from ocs_trn_tcadapplicationservicecharge " +
                    " where application_gid = '" + lsapplication_gid + "' and fieldvisit_charges_collectiontype = 'Collect') as fieldvisitcharges_Collect, " +
                    " (select sum(fieldvisit_charges)  from ocs_trn_tcadapplicationservicecharge " +
                    " where application_gid = '" + lsapplication_gid + "' and fieldvisit_charges_collectiontype = 'Deduct') as fieldvisitcharges_Deduct";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                string doccharge_Collect = objODBCDatareader["doccharge_Collect"].ToString();
                string doccharge_Deduct = objODBCDatareader["doccharge_Deduct"].ToString();
                string fieldvisitcharges_Collect = objODBCDatareader["fieldvisitcharges_Collect"].ToString();
                string fieldvisitcharges_Deduct = objODBCDatareader["fieldvisitcharges_Deduct"].ToString();
                string lsdocumentclientvistcharges = "";
                // Documentation Charges 
                if (doccharge_Collect != "" && doccharge_Collect != null)
                {
                    int docchargecollect = Convert.ToInt32(doccharge_Collect);
                    string doccharge_words = NumberToWords1(docchargecollect);
                    lsdocumentclientvistcharges = "Document Charges - " + docchargecollect + " (Rupees " + doccharge_words + " Only) - Collect ";
                }
                if (doccharge_Deduct != "" && doccharge_Deduct != null)
                {
                    int docchargeDeduct = Convert.ToInt32(doccharge_Deduct);
                    string doccharge_words = NumberToWords1(docchargeDeduct);
                    lsdocumentclientvistcharges += "/ " + docchargeDeduct + " (Rupees " + doccharge_words + " Only) - Deduct";
                }
                // Field Visit Charges 
                if (fieldvisitcharges_Collect != "" && fieldvisitcharges_Collect != null)
                {
                    int fieldvisitchargesCollect = Convert.ToInt32(fieldvisitcharges_Collect);
                    string fieldvisitcharge_words = NumberToWords1(fieldvisitchargesCollect);
                    lsdocumentclientvistcharges += ", Client Visit Charges - " + fieldvisitchargesCollect + " (Rupees " + fieldvisitcharge_words + " Only) - Collect ";
                }
                if (fieldvisitcharges_Deduct != "" && fieldvisitcharges_Deduct != null)
                {
                    int fieldvisitchargesDeduct = Convert.ToInt32(fieldvisitcharges_Deduct);
                    string fieldvisitcharge_words = NumberToWords1(fieldvisitchargesDeduct);
                    lsdocumentclientvistcharges += "/ " + fieldvisitchargesDeduct + " (Rupees " + fieldvisitcharge_words + " Only) - Deduct";
                }
                lscontent = lscontent.Replace("lsdocumentclientvistcharges", lsdocumentclientvistcharges);
            }
            else
            {
                lscontent = lscontent.Replace("lsdocumentclientvistcharges", "");
            }
            objODBCDatareader.Close();
            values.template_content = lscontent;
            msSQL = " select  a.template_content from ocs_mst_ttemplate a " +
             " where a.template_gid='OCTE2023011915'";
            values.template_content = objdbconn.GetExecuteScalar(msSQL);

            values.status = true;
            return true;
        }

        public void DaGetproductDropDown(string employee_gid, MdlProductDropDown values)
        {
            //Loan Product
            msSQL = " SELECT loanproduct_gid,loanproduct_name FROM ocs_mst_tloanproduct a" +
                       " where status='Y' order by a.loanproduct_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getloanproduct_list = new List<loanproductlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getloanproduct_list.Add(new loanproductlist
                    {
                        loanproduct_gid = (dr_datarow["loanproduct_gid"].ToString()),
                        loanproduct_name = (dr_datarow["loanproduct_name"].ToString()),
                    });
                }
                values.loanproductlist = getloanproduct_list;
            }
            dt_datatable.Dispose();
            //LoanType
            msSQL = " SELECT loantype_gid,loan_type FROM ocs_mst_tloantype a" +
                       " where status_log='Y' order by a.loantype_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getloantypelist = new List<loantypelist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getloantypelist.Add(new loantypelist
                    {
                        loantype_gid = (dr_datarow["loantype_gid"].ToString()),
                        loan_type = (dr_datarow["loan_type"].ToString()),
                    });
                }
                values.loantypelist = getloantypelist;
            }
            dt_datatable.Dispose();
            //Principal Frequency
            msSQL = " SELECT principalfrequency_gid,principalfrequency_name FROM ocs_mst_tprincipalfrequency a" +
                        " where status='Y' order by a.principalfrequency_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getprincipalfrequencylist = new List<principalfrequencylist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getprincipalfrequencylist.Add(new principalfrequencylist
                    {
                        principalfrequency_gid = (dr_datarow["principalfrequency_gid"].ToString()),
                        principalfrequency_name = (dr_datarow["principalfrequency_name"].ToString()),
                    });
                }
                values.principalfrequencylist = getprincipalfrequencylist;
            }
            dt_datatable.Dispose();
            //Interest Frequency
            msSQL = " SELECT  interestfrequency_gid,interestfrequency_name FROM ocs_mst_tinterestfrequency a" +
                       " where status='Y' order by a.interestfrequency_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getinterestfrequency = new List<interestfrequencylist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getinterestfrequency.Add(new interestfrequencylist
                    {
                        interestfrequency_gid = (dr_datarow["interestfrequency_gid"].ToString()),
                        interestfrequency_name = (dr_datarow["interestfrequency_name"].ToString()),
                    });
                }
                values.interestfrequencylist = getinterestfrequency;
            }
            dt_datatable.Dispose();

            //Buyer
            msSQL = " SELECT buyer_gid,concat(buyer_name,' / ',buyer_code) as buyer_name " +
                    " from ocs_mst_tbuyer where creditstatus_Approval in ('Y','N') and creditActive_status = 'Y' order by buyer_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbuyerlist = new List<buyerlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbuyerlist.Add(new buyerlist
                    {
                        buyer_name = (dr_datarow["buyer_name"].ToString()),
                        buyer_gid = (dr_datarow["buyer_gid"].ToString()),
                    });
                }
                values.buyerlist = getbuyerlist;
            }
            dt_datatable.Dispose();
            //Security Type
            msSQL = " select securitytype_gid,security_type from ocs_trn_tsecuritytype a" +
                   " where status_log='Y' order by securitytype_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getSecurity = new List<securitytype_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getSecurity.Add(new securitytype_list
                    {
                        securitytype_gid = (dr_datarow["securitytype_gid"].ToString()),
                        security_type = (dr_datarow["security_type"].ToString()),
                    });
                }
                values.securitytype_list = getSecurity;
            }
            dt_datatable.Dispose();

            //Program
            msSQL = " SELECT  program_gid,program FROM ocs_mst_tprogram a" +
                       " where status='Y' order by a.program_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getprogramlist = new List<programlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getprogramlist.Add(new programlist
                    {
                        program_gid = (dr_datarow["program_gid"].ToString()),
                        program = (dr_datarow["program"].ToString()),
                    });
                }
                values.programlist = getprogramlist;
            }
            dt_datatable.Dispose();

            values.status = true;
        }

        public void DaGetScannedGeneralInfo(mdlscannedgeneral values, string application_gid)
        {
            msSQL = " select maker_name,checker_name,approver_name,concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as RM_name " +
                    " from ocs_trn_tcadapplication a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " left join ocs_trn_tprocesstype_assign d on d.application_gid = a.application_gid " +
                    " where d.menu_gid = '" + getMenuClass.ScannedDocument + "' and a.application_gid='" + application_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.maker_name = objODBCDataReader["maker_name"].ToString();
                values.checker_name = objODBCDataReader["checker_name"].ToString();
                values.approver_name = objODBCDataReader["approver_name"].ToString();
                values.RM_name = objODBCDataReader["RM_name"].ToString();
            }
            objODBCDataReader.Close();

        }

        public void DaCADAppSanctionCount(string user_gid, string employee_gid, CadSanctionCount values)
        {
            msSQL = " select count(application_gid) as cadsanction_count from ocs_trn_tcadapplication a " +
                    " where a.process_type = 'Accept'";
            values.cadmaker_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(application2sanction_gid) as cadchecker_count from ocs_trn_tapplication2sanction a " +
                     " where a.sanctionletter_flag='Y' and a.checkerletter_flag='N' ";
            values.cadchecker_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(application2sanction_gid) as cadcheckerapproval_count from ocs_trn_tapplication2sanction a " +
                     " where a.checkerletter_flag='Y'";
            values.cadcheckerapproval_count = objdbconn.GetExecuteScalar(msSQL);
        }

        public void DaBankAccountDetailsList(string application_gid, MdlMstBankAccountDetails values)
        {
            msSQL = " select a.group_gid,a.group2bank_gid,ifsc_code,bank_accountno,accountholder_name,bank_name,bank_branch,group_name from ocs_trn_tcadgroup2bank a " +
                    " left join ocs_trn_tcadgroup b on a.group_gid = b.group_gid " +
                    " left join ocs_trn_tcadapplication c on c.application_gid = b.application_gid " +
                    " where c.application_gid = '" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstbankacctdtl_list = new List<mstbankacctdtl_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstbankacctdtl_list.Add(new mstbankacctdtl_list
                    {
                        group2bank_gid = (dr_datarow["group2bank_gid"].ToString()),
                        group_gid = (dr_datarow["group_gid"].ToString()),
                        ifsc_code = (dr_datarow["ifsc_code"].ToString()),
                        bank_accountno = (dr_datarow["bank_accountno"].ToString()),
                        accountholder_name = (dr_datarow["accountholder_name"].ToString()),
                        bank_name = (dr_datarow["bank_name"].ToString()),
                        bank_branch = (dr_datarow["bank_branch"].ToString()),
                        group_name = (dr_datarow["group_name"].ToString())
                    });
                }
                values.mstbankacctdtl_list = getmstbankacctdtl_list;
            }
            dt_datatable.Dispose();

            msSQL = "select creditbankdtl_gid,credit_gid,application_gid,bank_name,branch_name,bank_address,micr_code,ifsc_code,bankaccount_name," +
                 " bankaccounttype_gid,bankaccounttype_name,bankaccount_number,confirmbankaccountnumber,joinaccount_status,joinaccount_name," +
                 " chequebook_status,DATE_FORMAT(accountopen_date,'%d-%m-%Y') as accountopen_date, bankaccount_name as accountholder_name, " +
                 " joinaccount_name as jointaccountholder_name" +
                 " from ocs_mst_tcreditbankdtl where application_gid= '" + application_gid + "' and credit_gid like 'APIN%'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getinstitutionbankacc_list = new List<institutionbankacc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getinstitutionbankacc_list.Add(new institutionbankacc_list
                    {
                        creditbankdtl_gid = dt["creditbankdtl_gid"].ToString(),
                        bank_name = dt["bank_name"].ToString(),
                        branch_name = dt["branch_name"].ToString(),
                        bank_address = dt["bank_address"].ToString(),
                        micr_code = dt["micr_code"].ToString(),
                        ifsc_code = dt["ifsc_code"].ToString(),
                        bankaccount_name = dt["bankaccount_name"].ToString(),
                        bankaccounttype_name = dt["bankaccounttype_name"].ToString(),
                        bankaccount_number = dt["bankaccount_number"].ToString(),
                        confirmbankaccountnumber = dt["confirmbankaccountnumber"].ToString(),
                        joinaccount_status = dt["joinaccount_status"].ToString(),
                        joinaccount_name = dt["joinaccount_name"].ToString(),
                        chequebook_status = dt["chequebook_status"].ToString(),
                        accountopen_date = dt["accountopen_date"].ToString(),
                        accountholder_name = dt["accountholder_name"].ToString(),
                        jointaccountholder_name = dt["jointaccountholder_name"].ToString(),
                    });
                    values.institutionbankacc_list = getinstitutionbankacc_list;
                }
            }
            dt_datatable.Dispose();
            msSQL = "select creditbankdtl_gid,credit_gid,application_gid,bank_name,branch_name,bank_address,micr_code,ifsc_code,bankaccount_name," +
                 " bankaccounttype_gid,bankaccounttype_name,bankaccount_number,confirmbankaccountnumber,joinaccount_status,joinaccount_name," +
                 " chequebook_status,DATE_FORMAT(accountopen_date,'%d-%m-%Y') as accountopen_date, bankaccount_name as accountholder_name, " +
                 " joinaccount_name as jointaccountholder_name" +
                 " from ocs_mst_tcreditbankdtl where application_gid= '" + application_gid + "' and credit_gid like 'CTCT%'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getindividualbankacc_list = new List<individualbankacc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getindividualbankacc_list.Add(new individualbankacc_list
                    {
                        creditbankdtl_gid = dt["creditbankdtl_gid"].ToString(),
                        bank_name = dt["bank_name"].ToString(),
                        branch_name = dt["branch_name"].ToString(),
                        bank_address = dt["bank_address"].ToString(),
                        micr_code = dt["micr_code"].ToString(),
                        ifsc_code = dt["ifsc_code"].ToString(),
                        bankaccount_name = dt["bankaccount_name"].ToString(),
                        bankaccounttype_name = dt["bankaccounttype_name"].ToString(),
                        bankaccount_number = dt["bankaccount_number"].ToString(),
                        confirmbankaccountnumber = dt["confirmbankaccountnumber"].ToString(),
                        joinaccount_status = dt["joinaccount_status"].ToString(),
                        joinaccount_name = dt["joinaccount_name"].ToString(),
                        chequebook_status = dt["chequebook_status"].ToString(),
                        accountopen_date = dt["accountopen_date"].ToString(),
                        accountholder_name = dt["accountholder_name"].ToString(),
                        jointaccountholder_name = dt["jointaccountholder_name"].ToString(),
                    });
                    values.individualbankacc_list = getindividualbankacc_list;
                }
            }
            dt_datatable.Dispose();
            msSQL = "select creditbankdtl_gid,credit_gid,application_gid,bank_name,branch_name,bank_address,micr_code,ifsc_code,bankaccount_name," +
                " bankaccounttype_gid,bankaccounttype_name,bankaccount_number,confirmbankaccountnumber,joinaccount_status,joinaccount_name," +
                " chequebook_status,DATE_FORMAT(accountopen_date,'%d-%m-%Y') as accountopen_date, bankaccount_name as accountholder_name, " +
                " joinaccount_name as jointaccountholder_name" +
                " from ocs_mst_tcreditbankdtl where application_gid= '" + application_gid + "' and credit_gid like 'GRUP%'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getgroupbankacc_list = new List<groupbankacc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getgroupbankacc_list.Add(new groupbankacc_list
                    {
                        creditbankdtl_gid = dt["creditbankdtl_gid"].ToString(),
                        bank_name = dt["bank_name"].ToString(),
                        branch_name = dt["branch_name"].ToString(),
                        bank_address = dt["bank_address"].ToString(),
                        micr_code = dt["micr_code"].ToString(),
                        ifsc_code = dt["ifsc_code"].ToString(),
                        bankaccount_name = dt["bankaccount_name"].ToString(),
                        bankaccounttype_name = dt["bankaccounttype_name"].ToString(),
                        bankaccount_number = dt["bankaccount_number"].ToString(),
                        confirmbankaccountnumber = dt["confirmbankaccountnumber"].ToString(),
                        joinaccount_status = dt["joinaccount_status"].ToString(),
                        joinaccount_name = dt["joinaccount_name"].ToString(),
                        chequebook_status = dt["chequebook_status"].ToString(),
                        accountopen_date = dt["accountopen_date"].ToString(),
                        accountholder_name = dt["accountholder_name"].ToString(),
                        jointaccountholder_name = dt["jointaccountholder_name"].ToString(),
                    });
                    values.groupbankacc_list = getgroupbankacc_list;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetlsaProductname(string application_gid, LsaProductnamelist values)
        {
            try
            {
                msSQL = " SELECT application2loan_gid,concat(product_type,' / ',productsub_type) as product_type FROM ocs_trn_tcadapplication2loan a " +
                        " where application_gid = '" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getbankname_list = new List<LsaProductname>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getbankname_list.Add(new LsaProductname
                        {
                            application2loan_gid = (dr_datarow["application2loan_gid"].ToString()),
                            product_type = (dr_datarow["product_type"].ToString()),
                        });
                    }
                    values.LsaProductname = getbankname_list;
                }
                dt_datatable.Dispose();
            }
            catch
            {
            }
        }

        public static class getMenuClass
        {
            public const string
                 DocumentChecklist = "CADMGTDCL",
                 Sanction = "CADMGTSAN",
                 ScannedDocument = "CADMGTDTS",
                 Physical_document = "CADMGTPYD";
        }
    }

}