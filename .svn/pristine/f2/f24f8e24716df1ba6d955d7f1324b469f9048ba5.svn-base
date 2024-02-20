using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.rsk.Models;

namespace ems.rsk.DataAccess
{
    public class DaDocumentation
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL, msGetGid, msRefGid;
        int mnResult;

        public bool DaPostDocumentationDtls(string employee_gid, documentationdtl values)

        {

            msGetGid = objcmnfunctions.GetMasterGID("CU2D");
 
            msRefGid = objcmnfunctions.GetMasterGID("DMS");

            msSQL = "Insert into ocs_mst_tcustomer2documentation( " +
                              " customer2document_gid," +
                              " documentation_refno," +
                              " documentation_name," +
                              " documentation_type," +
                              " created_by," +
                              " created_date)" +
                              " values(" +
                              "'" + msGetGid + "', " +
                              "'" + msRefGid + "'," +
                              "'" + values.documentation_name.Replace("'", "\\'") + "'," +
                              "'" + values.documentation_type.Replace("'", "\\'") + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";      
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Documentation Details are Added Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }

        public bool DaGetDocumentationDtlList(documentationlist values)
        {
            msSQL = " select customer2document_gid,documentation_refno,documentation_name,documentation_type, " +
                    " concat(documentation_refno,' / ',documentation_name) as documentrefname, " +
                    " concat (c.user_firstname, c.user_lastname, ' / ', c.user_code) as created_by, date_format(a.created_date, '%d-%m-%Y') as created_date" +
                    " from ocs_mst_tcustomer2documentation a " +
                    " inner join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " inner join adm_mst_tuser c on c.user_gid=b.user_gid order by customer2document_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentationlist = new List<documentationdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentationlist.Add(new documentationdtl
                    {
                        customer2document_gid = dt["customer2document_gid"].ToString(),
                        documentation_refno = dt["documentation_refno"].ToString(),
                        documentation_name = dt["documentation_name"].ToString(),
                        documentation_type = dt["documentation_type"].ToString(),
                        documentrefname = dt["documentrefname"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString()
                    });
                    values.documentationdtl = getdocumentationlist;
                }
            }
            dt_datatable.Dispose();
            return true;
        }


        public bool DaGetrskDocumentationDtlList(string customer2sanction_gid,documentationlist values)
        {
            msSQL = " SELECT customer2document_gid,documentation_refno,documentation_name,documentation_type, " +
                    " CONCAT(documentation_refno,' / ',documentation_name) as documentrefname " +
                    " FROM ocs_mst_tcustomer2documentation ORDER BY customer2document_gid DESC";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentationlist = new List<documentationdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentationlist.Add(new documentationdtl
                    {
                        customer2document_gid = dt["customer2document_gid"].ToString(),
                        documentation_refno = dt["documentation_refno"].ToString(),
                        documentation_name = dt["documentation_name"].ToString(),
                        documentation_type = dt["documentation_type"].ToString(),
                        documentrefname = dt["documentrefname"].ToString()
                    });
                    values.documentationdtl = getdocumentationlist;
                }
            }
            dt_datatable.Dispose();

            msSQL = "delete from rsk_tmp_tsanctiondocumentdtl where customer2sanction_gid='" + customer2sanction_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            return true;
        }

        public bool DaGetDocumentationDelete(string customer2document_gid, resultsample values)
        {
            msSQL = "delete from ocs_mst_tcustomer2documentation where customer2document_gid ='" + customer2document_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
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

        public bool DaGetDocumentationDtl(string customer2document_gid,documentationdtl values)
        {
            msSQL = " select documentation_refno,customer2document_gid,documentation_name,documentation_type" +
                    " from ocs_mst_tcustomer2documentation where customer2document_gid ='" + customer2document_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if(objODBCDatareader.HasRows==true)
            {
                values.documentation_refno = objODBCDatareader["documentation_refno"].ToString();
                values.documentation_name = objODBCDatareader["documentation_name"].ToString();
                values.documentation_type = objODBCDatareader["documentation_type"].ToString();
                values.customer2document_gid = objODBCDatareader["customer2document_gid"].ToString();
            }
            objODBCDatareader.Close();
            return true;
        }

        public bool DaPostDocumentationUpdate(string employee_gid, documentationdtl values)
        {

            msSQL = " update ocs_mst_tcustomer2documentation set " +
                    " documentation_name ='" + values.documentation_name.Replace("'", "\\'") + "', " +
                    " documentation_type='" + values.documentation_type.Replace("'", "\\'") + "', " +
                    " updated_by ='" + employee_gid + "', " +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where customer2document_gid='" + values.customer2document_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Documentation Details are Updated Successfully..!";
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