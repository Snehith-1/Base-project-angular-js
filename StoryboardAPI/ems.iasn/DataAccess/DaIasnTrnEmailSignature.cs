using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.iasn.Models;
using ems.utilities.Functions;

namespace ems.iasn.DataAccess
{
    public class DaIasnTrnEmailSignature
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDataReader;
        string msSQL;
        string msGetGid;
        int mnResult = 0;

        public result DaPostEmailSignature(MdlEmailSignature values, string user_gid)
        {
            result objResult = new Models.result();

            msSQL = " SELECT emailsignautre_gid FROM isn_trn_temailsignature" +
                    " WHERE created_by='" + user_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == false)
            {
                objODBCDataReader.Close();
                msGetGid = objcmnfunctions.GetMasterGID("SIGN");

                msSQL = " INSERT INTO isn_trn_temailsignature(" +
                  " emailsignautre_gid," +
                  " emailsignature," +
                  " created_by)" +
                  " VALUES(" +
                  "'" + msGetGid + "'," +
                  "'" + values.emailsignature.Replace("'", "\'") + "'," +
                  "'" + user_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



                if (mnResult == 1)
                {
                    objResult.status = true;
                    objResult.message = "Email Signature Created Successfully";
                }
                else
                {
                    objResult.status = false;
                    objResult.message = "Error Occureed";
                }
                return objResult;

            }
            else
            {
                objODBCDataReader.Close();

                msSQL = " UPDATE isn_trn_temailsignature SET" +
                   " emailsignature='" + values.emailsignature.Replace("'", "\'") + "'," +
                   " updated_date=current_timestamp,updated_by='" + user_gid + "'" +
                   " WHERE created_by='" + user_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    objResult.status = true;
                    objResult.message = "Email Signature Updated Successfully";
                }
                else
                {
                    objResult.status = false;
                    objResult.message = "Error Occureed";
                }
                return objResult;

            }


        }

        public void DaGetEmailSugnature(string user_gid, MdlEmailSignature values)
        {
            msSQL = " SELECT emailsignature" +
                    " FROM isn_trn_temailsignature" +
                    " WHERE created_by='" + user_gid + "'";
            values.emailsignature = objdbconn.GetExecuteScalar(msSQL);
        }

    }
}
