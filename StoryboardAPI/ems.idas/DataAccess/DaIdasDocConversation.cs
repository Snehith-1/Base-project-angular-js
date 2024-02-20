using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.idas.Models;
using ems.storage.Functions;


namespace ems.idas.DataAccess
{
    public class DaIdasDocConversation
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();

        DataTable dt_datatable;
        string msSQL;
        int mnResult=0;

        public bool DaGetUploadDocument(uploaddocumentlist values, string docconversation_gid)
        {

            msSQL = " SELECT a.conversationdocument_gid, a.document_path, a.document_name, a.document_title," +
                   " date_format(a.created_date, '%d-%m-%Y') as created_date,"+
                   " concat(b.user_code, ' / ', b.user_firstname, b.user_lastname) as user_name"+
                   " FROM ids_trn_tconversationdocument a"+
                   " LEFT JOIN adm_mst_tuser b on a.created_by = b.user_gid"+
                   " WHERE docconversation_gid = '"+ docconversation_gid +"'"+
                   " ORDER BY created_date DESC";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocList = new List<uploaddocument>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getDocList.Add(new uploaddocument
                    {
                        conversationdocument_gid = dt["conversationdocument_gid"].ToString(),
                        document_path =objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        document_name = dt["document_name"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        created_by =dt["user_name"].ToString (),
                        created_date=dt["created_date"].ToString()
                    });
                }
                values.uploaddocument = getDocList;
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "No records to display";
            }
            dt_datatable.Dispose();

            return true;
        }

        public void DaPostTypesOfCopy(types_of_copy objResult)
        {
            msSQL = " UPDATE ids_trn_tsanctiondocumentdtls SET"+
                    " types_of_copy='"+ objResult .type_copy +"'"+
                    " WHERE sanctiondocument_gid='"+ objResult .sanctiondocument_gid +"'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if(mnResult ==1)
            {
                objResult.status = true;
                objResult.message = "Types of Document Updated Successfully..!";
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured";

            }
        }
    }
}