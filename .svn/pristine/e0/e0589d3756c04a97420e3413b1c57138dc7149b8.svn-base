using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ems.ecms.Models;
using ems.utilities.Functions;
using System.Data;
using System.Data.Odbc;

namespace ems.ecms.DataAccess
{
    /// <summary>
    /// collateral Controller Class containing API methods for accessing the  DataAccess class DaCustomerCollateral 
    /// To create collateral details, delete collateral, get collateral details and update collateral details
    /// </summary>
    /// <remarks>Written by Sundar Rajan </remarks>
    public class DaCustomerCollateral
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunction = new cmnfunctions();
        OdbcDataReader objOdbcDataReader;
        string mssql, msGetGid,msGetGidCOL;
        string lsloanref_no, lsloan_title, lssanctionref_no, lssanction_date;
        DataTable dt_datatable;
        int mnresult;

        public void DaPostcreateCollateral(customercollateral values,string user_gid)
        {
           
            msGetGid = objcmnfunction.GetMasterGID("COLT");
            var count = 0;
            foreach (string i in values.loan_gid)
            {


                msGetGidCOL = objcmnfunction.GetMasterGID("COLL");

                mssql = " select loanref_no,loan_title,sanction_refno,sanction_date" +
                        " from ocs_trn_tloan" +
                        " where loan_gid='" + i + "'";
                objOdbcDataReader = objdbconn .GetDataReader(mssql);
                if (objOdbcDataReader.HasRows)
                {

                    lssanctionref_no = objOdbcDataReader["sanction_refno"].ToString();
                    lssanction_date = objOdbcDataReader["sanction_date"].ToString();
                    lsloanref_no = objOdbcDataReader["loanref_no"].ToString();
                    lsloan_title = objOdbcDataReader["loan_title"].ToString();

                    mssql = " insert into ocs_trn_tcollateral2loan(" +
                           " collateralloan_gid," +
                           " collateral_gid," +
                           " loan_gid," +
                           " sanctionref_no," +
                           " sanction_date," +
                           " loanref_no," +
                           " loan_title," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGidCOL + "'," +
                           "'" + msGetGid + "'," +
                           "'" + values.loan_gid + "'," +
                           "'" + lssanctionref_no + "'," +
                           "'" + lssanction_date + "'," +
                           "'" + lsloanref_no + "'," +
                           "'" + lsloan_title + "'," +
                           "'" + user_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnresult = objdbconn .ExecuteNonQuerySQL(mssql);
                }
                count = count + 1;
                objOdbcDataReader.Close();
            }
            if (count==0)
            {
                values .status = false;
                values .message = "failure";
            }



            mssql = " insert into ocs_trn_tcustomercollateral(" +
                    " collateral_gid," +
                    " customer_gid," +
                    " customer_name," +
                    " security_type," +
                    " securitytype_gid,"+
                    " security_description," +
                    " account_status," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.customer_gid + "'," +
                    "'" + values.customer_name.Replace ("'","")+ "'," +
                    "'" + values.security_type + "'," +
                    "'" + values.securitytype_gid + "',"+
                    "'" + values.security_description.Replace ("'","") + "'," +
                    "'" + values.account_status + "'," +
                    "'" + user_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

      mnresult = objdbconn .ExecuteNonQuerySQL(mssql);

            mssql = " insert into ocs_trn_collateralhistory"+
                    " ( collateral_gid,"+
                    " account_status,"+
                    " created_by," +
                    " created_date)"+
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'"+ values .account_status +"',"+
                    "'" + user_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnresult = objdbconn .ExecuteNonQuerySQL(mssql);

          
            if (mnresult != 0)
            {
                values.status = true;
                values.message = "success";
            }
            else
            {
                values.status = false ;
                values.message = "failure";
            }
        }
        public void DaGetCollateral(Mdlcollateral objcollateral)
        {
            mssql = " select a.collateral_gid,a.customer_gid,a.customer_name,a.security_type,a.security_description,a.account_status,"+
                    " date_format(a.created_date,'%d-%m-%Y %h:%i:%s %p') as created_date,concat(b.user_code,' / ',b.user_firstname,' ' ,b.user_lastname) as created_by" +
                    " from ocs_trn_tcustomercollateral a"+
                    " left join adm_mst_tuser b on a.created_by=b.user_gid order by a.created_date desc";
           
            dt_datatable = objdbconn.GetDataTable (mssql);
            var getcollateral = new List<customercollateral>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcollateral.Add(new customercollateral
                    {
                        collateral_gid = (dr_datarow["collateral_gid"].ToString()),
                        security_type = (dr_datarow["security_type"].ToString()),
                        customer_gid= (dr_datarow["customer_gid"].ToString()),
                        customer_name = (dr_datarow["customer_name"].ToString()),
                        security_description = (dr_datarow["security_description"].ToString()),
                        account_status = (dr_datarow["account_status"].ToString()),
                        created_date =(dr_datarow ["created_date"].ToString ()),
                        created_by=(dr_datarow ["created_by"].ToString ()),
                    });
                }
                objcollateral.customercollateral_list = getcollateral;
            }
            dt_datatable.Dispose();

            objcollateral.status = true;


        }
        public void DaGetCollateralDetails(string collateral_gid, customercollateral values )
        {
           
            mssql = " select customer_gid,customer_name,security_type,securitytype_gid,security_description,account_status" +
                    " from ocs_trn_tcustomercollateral" +
                    " where collateral_gid='"+ collateral_gid+"'";
            objOdbcDataReader = objdbconn .GetDataReader(mssql);
            if(objOdbcDataReader .HasRows )
            {
                values.customer_gid = objOdbcDataReader["customer_gid"].ToString();
                values.customer_name = objOdbcDataReader["customer_name"].ToString();
                values.security_type = objOdbcDataReader["security_type"].ToString();
                values.security_description = objOdbcDataReader["security_description"].ToString();
                values.account_status  = objOdbcDataReader["account_status"].ToString();
                values.securitytype_gid = objOdbcDataReader["securitytype_gid"].ToString();
            }
            objOdbcDataReader.Close();
            var getcollateral = new List<collateralloandetails>();

            mssql = " select concat(loanref_no,' / ',loan_title) as loan_title,sanctionref_no,sanction_date"+
                     " from ocs_trn_tcollateral2loan"+
                     " where collateral_gid='"+ collateral_gid+"'";
            dt_datatable =objdbconn .GetDataTable (mssql);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcollateral.Add(new collateralloandetails
                    {
                        loan_title = (dr_datarow["loan_title"].ToString()),
                        sanction_refno = (dr_datarow["sanctionref_no"].ToString()),
                        sanction_date = (dr_datarow["sanction_date"].ToString()),

                    });
                }
            }
            dt_datatable.Dispose();
            values.collateralloandetails_list = getcollateral;
            values.status = true;
            values.message = "success";
        }
        public void DaPostDeleteCollateral(string collateral_gid, customercollateral values)
        {
           
            mssql = " delete from ocs_trn_tcustomercollateral"+
                    " where collateral_gid='"+ collateral_gid + "'";
            mnresult = objdbconn .ExecuteNonQuerySQL(mssql);

            mssql = " delete from ocs_trn_tcollateral2loan" +
                    " where collateral_gid='" + collateral_gid + "'";
            mnresult = objdbconn .ExecuteNonQuerySQL(mssql);

            
            if (mnresult != 0)
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
        
        public void DaPostUpdateCollateralDetails( customercollateral values,string user_gid)
        {
            
            mssql = " update ocs_trn_tcustomercollateral set" +
                   " customer_gid='"+ values.customer_gid+"'," +
                  " customer_name='"+ values.customer_name+"'," +
                  " security_type='"+ values.security_type+"'," +
                  " securitytype_gid='"+ values .securitytype_gid +"',"+
                  " security_description='" + values.security_description+"'," +
                  " account_status='"+ values.account_status +"'," +
                  " updated_by='"+ user_gid +"',"+
                  " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'"+
                  " where collateral_gid='"+ values.collateral_gid +"'";
              
      mnresult = objdbconn .ExecuteNonQuerySQL(mssql);


            mssql = " insert into ocs_trn_collateralhistory" +
                    " (collateral_gid," +
                    " account_status," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + values.collateral_gid + "'," +
                    "'" + values.account_status + "'," +
                    "'" + user_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnresult = objdbconn .ExecuteNonQuerySQL(mssql);

          
            if (mnresult != 0)
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
        
    }
}