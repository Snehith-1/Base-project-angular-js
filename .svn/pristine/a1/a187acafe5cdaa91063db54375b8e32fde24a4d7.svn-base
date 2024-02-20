using ems.idas.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Web;
using System.Linq;
using ems.storage.Functions;

namespace ems.idas.DataAccess
{
    public class DaIdasCourierManagement
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        string msSQL, msGetGID, msGetGIDRef, msGetChildGid;
        OdbcDataReader objODBCDataReader;
        int mnResult;


        public bool DaGetCourierMgmt(string courier_type, MdlIdasCourierManagement objCourierList)
        {
            msSQL = " SELECT a.courierref_no,a.couriermgmt_gid,date_format(a.date_of_courier,'%d-%m-%Y') as date_of_courier," +
                    " date_format(a.created_date,'%d-%m-%Y') as created_date,a.courier_type," +
                    " concat(b.user_code,' ',b.user_firstname,' ',b.user_lastname) as created_by," +
                    " a.sanction_gid,a.sanctionref_no,a.customer_gid," +
                    " a.customer_name,a.document_type,a.sender_name,a.pod_no,a.couriercompany_name,a.courierhandover_to,a.ack_status " +
                    " FROM ids_trn_tcouriermgnt a" +
                    " left join adm_mst_tuser b on a.created_by=b.user_gid" +
                    " where a.courier_type='" + courier_type + "' order by a.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getCourierMgmt = new List<CourierMgmt>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getCourierMgmt.Add(new CourierMgmt
                    {
                        courierMgmt_gid = dt["couriermgmt_gid"].ToString(),
                        courierref_no = dt["courierref_no"].ToString(),
                        date_of_courier = dt["date_of_courier"].ToString(),
                        sanction_gid = dt["sanction_gid"].ToString(),
                        sanctionref_no = dt["sanctionref_no"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        document_type = dt["document_type"].ToString(),
                        sender_name = dt["sender_name"].ToString(),
                        pod_no = dt["pod_no"].ToString(),
                        couriercompany_name = dt["couriercompany_name"].ToString(),
                        courierhandover_to = dt["courierhandover_to"].ToString(),
                        ack_status = dt["ack_status"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        courier_type = dt["courier_type"].ToString(),
                    });
                }
                objCourierList.CourierMgmt = getCourierMgmt;
                objCourierList.status = true;
                objCourierList.message = "Data Fetched";
            }
            else
            {
                objCourierList.status = false;
                objCourierList.message = "No Record Found";
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DapostCourierDtl(string user_gid, CourierMgmt values)
        {
            msGetGID = objcmnfunctions.GetMasterGID("COMG");

            if (values.courier_type == "Courier Inward")
            {
                msGetGIDRef = objcmnfunctions.GetMasterGID("CI");
            }
            else if (values.courier_type == "Courier Outward")
            {
                msGetGIDRef = objcmnfunctions.GetMasterGID("CO");
            }
            else if (values.courier_type == "Physical Inward")
            {
                msGetGIDRef = objcmnfunctions.GetMasterGID("PI");
            }
            else if (values.courier_type == "Physical Outward")
            {
                msGetGIDRef = objcmnfunctions.GetMasterGID("PO");
            }

            if (values.MdlCourierByList.Count != 0)
            {
                for (int i = 0; i < values.MdlCourierByList.Count; i++)
                {

                    msGetChildGid = objcmnfunctions.GetMasterGID("CHBY");
                    msSQL = " INSERT INTO ids_trn_tcourierhandoverby(" +
                          " courierhandoverby_gid," +
                          " couriermgmt_gid ," +
                          " employee_gid ," +
                          " employee_name " +
                          " )" +
                          " VALUES(" +
                          "'" + msGetChildGid + "'," +
                          "'" + msGetGID + "'," +
                          "'" + values.MdlCourierByList[i].employee_gid + "'," +
                          "'" + values.MdlCourierByList[i].employee_name + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                }
            }

            if (values.MdlCourierToList.Count != 0)
            {
                for (int i = 0; i < values.MdlCourierToList.Count; i++)
                {

                    msGetChildGid = objcmnfunctions.GetMasterGID("CHTO");
                    msSQL = " INSERT INTO ids_trn_tcourierhandoverto(" +
                          " courierhandoverto_gid," +
                          " couriermgmt_gid ," +
                          " employee_gid ," +
                          " employee_name" +
                          " )" +
                          " VALUES(" +
                          "'" + msGetChildGid + "'," +
                          "'" + msGetGID + "'," +
                          "'" + values.MdlCourierToList[i].employee_gid + "'," +
                          "'" + values.MdlCourierToList[i].employee_name + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                }
            }


            msSQL = "INSERT INTO ids_trn_tcouriermgnt(" +
               " couriermgmt_gid," +
               " courierref_no," +
               " date_of_courier," +
               " sanction_gid," +
               " sanctionref_no," +
               " customer_gid," +
               " customer_name," +
               " document_type," +
               " sender_gid," +
               " sender_name," +
               " pod_no," +
               " couriercompany_name," +
               " courierhandover_to_gid," +
               " courierhandover_to," +
               " address," +
               " courier_type," +
               " remarks," +
               " created_by," +
               " created_date)" +
               " VALUES(" +
               "'" + msGetGID + "'," +
               "'" + msGetGIDRef + "'," +
               "'" + Convert.ToDateTime(values.date_of_courier).ToString("yyyy-MM-dd") + "'," +
               "'" + values.sanction_gid + "'," +
               "'" + values.sanctionref_no + "'," +
               "'" + values.customer_gid + "'," +
               "'" + values.customer_name.Replace("'", "") + "'," +
               "'" + values.document_type.Replace("'", "") + "'," +
               "(select group_concat(employee_gid) from ids_trn_tcourierhandoverby where couriermgmt_gid='" + msGetGID + "')," +
               "(select group_concat(employee_name) from ids_trn_tcourierhandoverby where couriermgmt_gid='" + msGetGID + "')," +
               "'" + values.pod_no.Replace("'", "") + "'," +
               "'" + values.couriercompany_name.Replace("'", "") + "'," +
               "(select group_concat(employee_gid) from ids_trn_tcourierhandoverto where couriermgmt_gid='" + msGetGID + "')," +
               "(select group_concat(employee_name) from ids_trn_tcourierhandoverto where couriermgmt_gid='" + msGetGID + "')," +
               "'" + values.address.Replace("'", "") + "'," +
               "'" + values.courier_type + "'," +
               "'" + values.remarks.Replace("'", "") + "'," +
               "'" + user_gid + "'," +
               "current_timestamp)";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult == 1)
            {
                msSQL = " INSERT INTO ids_trn_tcourierdocument(courierdocument_gid,couriermgmt_gid,document_name,document_title,document_gid,document_path,created_by,created_date)" +
                        " SELECT courierdocument_gid,'" + msGetGID + "', document_name, document_title,document_gid, document_path, created_by, created_date" +
                        " FROM ids_tmp_tcourierdocument " +
                        " WHERE created_by='" + user_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    result objResult = new result();
                    DaIdasTrnSentMail objDaSent = new DaIdasTrnSentMail();

                    objResult = objDaSent.DaPostCourierMail(msGetGID, user_gid);
                    if (objResult.status == false)
                    {
                        values.message = "Error Occured While Sending the Mail";
                        values.status = false;
                    }
                    else
                    {
                        msSQL = " delete from ids_tmp_tcourierdocument  WHERE created_by = '" + user_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        values.message = "Courier Details Added Successfully..!";
                        values.status = true;

                    }

                }


            }
            else
            {
                values.message = "Error Occured";
                values.status = false;
            }
            return true;
        }

        public void DaGetEditCourierDtl(CourierMgmt values, string courier_gid)
        {
            try
            {
                msSQL = " SELECT couriermgmt_gid,courierref_no," +
                        " cast( date_of_courier  as char)as date_of_courier, " +
                        " address,sanction_gid,sanctionref_no,customer_gid," +
                        " customer_name,document_type,sender_name,pod_no,couriercompany_name," +
                        " courierhandover_to,courier_type,remarks,date_format(ack_date,'%d-%m-%Y %h:%i %p') as ack_date," +
                        " address,ack_status,ackby_name,date_format(a.created_date,'%d-%m-%Y') as created_date," +
                        " CONCAT(b.user_code,' / ',b.user_firstname,b.user_lastname) as created_by FROM ids_trn_tcouriermgnt a" +
                        " LEFT JOIN adm_mst_tuser b on a.created_by=b.user_gid" +
                        " WHERE couriermgmt_gid='" + courier_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows)
                {
                    objODBCDataReader.Read();

                    values.courierref_no = objODBCDataReader["courierref_no"].ToString();
                    //values.date_of_courier = objODBCDataReader["date_of_courier"].ToString();
                    values.date_of_courier = Convert.ToDateTime(objODBCDataReader["date_of_courier"]).ToString("MM-dd-yyyy");
                    values.sanction_gid = objODBCDataReader["sanction_gid"].ToString();
                    values.sanctionref_no = objODBCDataReader["sanctionref_no"].ToString();
                    values.customer_gid = objODBCDataReader["customer_gid"].ToString();
                    values.customer_name = objODBCDataReader["customer_name"].ToString();
                    values.document_type = objODBCDataReader["document_type"].ToString();
                    values.sender_name = objODBCDataReader["sender_name"].ToString();
                    values.pod_no = objODBCDataReader["pod_no"].ToString();
                    values.couriercompany_name = objODBCDataReader["couriercompany_name"].ToString();
                    values.courierhandover_to = objODBCDataReader["courierhandover_to"].ToString();
                    values.courier_type = objODBCDataReader["courier_type"].ToString();
                    values.address = objODBCDataReader["address"].ToString();
                    values.ack_status = objODBCDataReader["ack_status"].ToString();
                    values.remarks = objODBCDataReader["remarks"].ToString();
                    values.ack_date = objODBCDataReader["ack_date"].ToString();
                    values.ackby_name = objODBCDataReader["ackby_name"].ToString();
                    values.created_date = objODBCDataReader["created_date"].ToString();
                    values.created_by = objODBCDataReader["created_by"].ToString();
                }
                objODBCDataReader.Close();

                msSQL = " SELECT a.courierdocument_gid, a.document_name,a.document_path,a.document_title," +
                        " CONCAT(b.user_code,' / ',b.user_firstname,b.user_lastname) as created_by,date_format(a.created_date,'%d-%m-%Y') as created_date" +
                        " FROM ids_trn_tcourierdocument a " +
                        " LEFT JOIN adm_mst_tuser b on a.created_by=b.user_gid" +
                        " WHERE a.couriermgmt_gid='" + courier_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getDocList = new List<uploadcourierdocument>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {

                        getDocList.Add(new uploadcourierdocument
                        {
                            courierdocument_gid = dt["courierdocument_gid"].ToString(),
                            document_name = dt["document_name"].ToString(),
                            document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                            document_title = dt["document_title"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString()
                        });
                    }
                    values.uploadcourierdocument = getDocList;
                }
                dt_datatable.Dispose();

                msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,'/',a.user_code) as employee_name," +
                        " b.employee_gid from adm_mst_tuser a " +
                        " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                        " WHERE user_status<>'N' ORDER BY a.user_firstname asc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employee = new List<employee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    values.MdlEmployee = dt_datatable.AsEnumerable().Select(row =>
                    new MdlEmployeeList
                    {
                        employee_gid = row["employee_gid"].ToString(),
                        employee_name = row["employee_name"].ToString()
                    }
                    ).ToList();
                }
                dt_datatable.Dispose();

                msSQL = " SELECT employee_gid,employee_name FROM ids_trn_tcourierhandoverby WHERE couriermgmt_gid='" + courier_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    values.MdlCourierByList = dt_datatable.AsEnumerable().Select(row => new MdlCourierByList
                    {
                        employee_gid = row["employee_gid"].ToString(),
                        employee_name = row["employee_name"].ToString()

                    }).ToList();


                }
                dt_datatable.Dispose();

                msSQL = " SELECT employee_gid,employee_name FROM ids_trn_tcourierhandoverto WHERE couriermgmt_gid='" + courier_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    values.MdlCourierToList = dt_datatable.AsEnumerable().Select(row => new MdlCourierToList
                    {
                        employee_gid = row["employee_gid"].ToString(),
                        employee_name = row["employee_name"].ToString()

                    }).ToList();


                }
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Data Fetched";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.Message;
            }
        }

        public void DaPostUpdateCourier(string user_gid, string employee_gid, CourierMgmt values, result objResult)
        {
            if (values.MdlCourierByList.Count != 0)
            {
                for (int i = 0; i < values.MdlCourierByList.Count; i++)
                {
                    msSQL = " SELECT courierhandoverby_gid FROM ids_trn_tcourierhandoverby WHERE couriermgmt_gid='" + values.courierMgmt_gid + "' and employee_gid='" + values.MdlCourierByList[i].employee_gid + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows == false)
                    {
                        objODBCDataReader.Close();
                        msGetChildGid = objcmnfunctions.GetMasterGID("CHBY");
                        msSQL = " INSERT INTO ids_trn_tcourierhandoverby(" +
                              " courierhandoverby_gid," +
                              " couriermgmt_gid ," +
                              " employee_gid ," +
                              " employee_name" +
                              " )" +
                              " VALUES(" +
                              "'" + msGetChildGid + "'," +
                              "'" + values.courierMgmt_gid + "'," +
                              "'" + values.MdlCourierByList[i].employee_gid + "'," +
                              "'" + values.MdlCourierByList[i].employee_name + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else
                    {
                        objODBCDataReader.Close();
                    }
                }
            }

            if (values.MdlCourierToList.Count != 0)
            {
                for (int i = 0; i < values.MdlCourierToList.Count; i++)
                {
                    msSQL = " SELECT courierhandoverto_gid FROM ids_trn_tcourierhandoverto WHERE couriermgmt_gid='" + values.courierMgmt_gid + "' and employee_gid='" + values.MdlCourierToList[i].employee_gid + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows == false)
                    {
                        objODBCDataReader.Close();
                        msGetChildGid = objcmnfunctions.GetMasterGID("CHTO");
                        msSQL = " INSERT INTO ids_trn_tcourierhandoverto(" +
                              " courierhandoverto_gid," +
                              " couriermgmt_gid ," +
                              " employee_gid ," +
                              " employee_name" +
                              " )" +
                              " VALUES(" +
                              "'" + msGetChildGid + "'," +
                              "'" + msGetGID + "'," +
                              "'" + values.MdlCourierToList[i].employee_gid + "'," +
                              "'" + values.MdlCourierToList[i].employee_name + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    }
                    else
                    {
                        objODBCDataReader.Close();
                    }
                }
            }



            msSQL = " UPDATE ids_trn_tcouriermgnt SET" +
                    " sanction_gid='" + values.sanction_gid + "'," +
                    " sanctionref_no='" + values.sanctionref_no + "'," +
                    " customer_gid='" + values.customer_gid + "'," +
                    " customer_name='" + values.customer_name.Replace("'", "") + "'," +
                    " document_type='" + values.document_type.Replace("'", "") + "'," +
                    " sender_name=(select group_concat(employee_name) from ids_trn_tcourierhandoverby where couriermgmt_gid='" + values.courierMgmt_gid + "')," +
                    " remarks='" + values.remarks.Replace("'", "") + "'," +
                    " pod_no='" + values.pod_no.Replace("'", "") + "'," +
                    " couriercompany_name='" + values.couriercompany_name.Replace("'", "") + "'," +
                    " courierhandover_to=(select group_concat(employee_name) from ids_trn_tcourierhandoverto where couriermgmt_gid='" + values.courierMgmt_gid + "')," +
                    " address='" + values.address.Replace("'", "") + "'," +
                    " courier_type='" + values.courier_type + "'," +
                    " updated_date=current_timestamp,";
            try
            {
                msSQL += " date_of_courier='" + Convert.ToDateTime(values.date_of_courier).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    " updated_by='" + user_gid + "'";
            }
            catch (Exception ex)
            {
                msSQL += " updated_by='" + user_gid + "'";
            }
            msSQL += " WHERE couriermgmt_gid='" + values.courierMgmt_gid + "'";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (values.ack_status == "Acknowledged")
            {
                msSQL = " UPDATE ids_trn_tcouriermgnt SET" +
                        " ack_status='" + values.ack_status + "'," +
                        " ack_date='" + Convert.ToDateTime(values.ack_date).ToString("yyyy-MM-dd") + "'," +
                        " ackby_gid='" + employee_gid + "'," +
                        " ackby_name=(select concat(user_code,' / ',user_firstname,user_lastname) from adm_mst_tuser where user_gid='" + user_gid + "')" +
                        " WHERE couriermgmt_gid='" + values.courierMgmt_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }

            if (mnResult == 1)
            {
                objResult.message = "Courier Details Updated Successfully..!";
                objResult.status = true;
            }
            else
            {
                objResult.message = "Error Occured";
                objResult.status = false;
            }
        }

        public void DaPostcourierdocupload(HttpRequest httpRequest, uploadcourierdocument objfilename, string employee_gid, string user_gid)
        {
            HttpPostedFile httpPostedFile;
            string lspath = string.Empty;
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
            string TrnGid = httpRequest.Form["Trn_Gid"];
            string lsdocument_title = httpRequest.Form["document_title"];
            string project_flag = httpRequest.Form["project_flag"].ToString();


            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = HttpContext.Current.Server.MapPath("../../../erpdocuments" + "/" + lscompany_code + "/" + "IDAS/CourierDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

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
                            objfilename.message = "File format is not supported";
                            objfilename.status = false;
                            return;
                        }
                        //lspath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "IDAS/CourierDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");

                        //objcmnfunctions.uploadFile(lspath, lsfile_gid);

                        //lspath = "../../../erp_documents" + "/" + lscompany_code + "/" + "IDAS/CourierDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "IDAS/CourierDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "IDAS/CourierDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";



                        msGetGID = objcmnfunctions.GetMasterGID("IDCM");


                        if (TrnGid == "")
                        {
                            msSQL = " insert into ids_tmp_tcourierdocument( " +
                                    " courierdocument_gid ," +
                                    " document_name ," +
                                    " document_title," +
                                    " document_gid ," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGID + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lsdocument_title.Replace("'","") + "'," +
                                      "'" + msdocument_gid + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + user_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        }
                        else
                        {
                            msSQL = " insert into ids_trn_tcourierdocument( " +
                                    " courierdocument_gid ," +
                                    " couriermgmt_gid," +
                                    " document_name ," +
                                    " document_title," +
                                    " document_gid ," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGID + "'," +
                                    "'" + TrnGid + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lsdocument_title.Replace("'", "") + "'," +
                                    "'" + msdocument_gid + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + user_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        }


                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                        if (mnResult != 0)
                        {
                            objfilename.status = true;
                            objfilename.message = "Document Uploaded Successfully";
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error Occured";
                        }
                    }
                }
            }
            catch
            {
            }
        }

        public void DaGetcourierdoc(uploadcourierdocumentlist objfilename, string user_gid)
        {

            msSQL = " SELECT a.courierdocument_gid, a.document_name,a.document_path ,a.document_title," +
                " CONCAT(b.user_code,' / ',b.user_firstname,b.user_lastname) as created_by,date_format(a.created_date,'%d-%m-%Y') as created_date" +
                " FROM ids_tmp_tcourierdocument a" +
                " LEFT JOIN adm_mst_tuser b on a.created_by=b.user_gid" +
                " WHERE a.created_by='" + user_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocList = new List<uploadcourierdocument>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getDocList.Add(new uploadcourierdocument
                    {
                        courierdocument_gid = dt["courierdocument_gid"].ToString(),
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        document_title = dt["document_title"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString()

                    });
                }
                objfilename.uploadcourierdocument = getDocList;
            }
            dt_datatable.Dispose();
        }

        public void DaDeleteCourierDoc(string courierdocument_gid, string user_gid, result objResult)
        {
            if (courierdocument_gid == "undefine")
            {
                msSQL = " DELETE FROM ids_tmp_tcourierdocument WHERE created_by='" + user_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = " DELETE FROM ids_tmp_tcourierdocument WHERE courierdocument_gid='" + courierdocument_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            }


            if (mnResult != 0)
            {
                objResult.status = true;
                objResult.message = "Courier Document Deleted Successfully";
            }
            else
            {
                objResult.status = false;
            }
        }

        public void DaGetCourierCount(courier_count values)
        {
            msSQL = " select count(*) from ids_trn_tcouriermgnt where courier_type = 'Courier Inward'";
            values.courier_inward = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(*) from ids_trn_tcouriermgnt where courier_type = 'Courier Outward'";
            values.courier_outward = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(*) from ids_trn_tcouriermgnt where courier_type = 'Physical Inward'";
            values.physical_inward = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(*) from ids_trn_tcouriermgnt where courier_type = 'Physical Outward'";
            values.physical_outward = objdbconn.GetExecuteScalar(msSQL);
        }

        public void DaCourierAckList(string employee_gid, MdlIdasCourierManagement objCourierList)
        {
            msSQL = " SELECT a.courierref_no, date_format(a.date_of_courier,'%d-%m-%Y') as date_of_courier, a.couriermgmt_gid, a.ack_status," +
                    " date_format(a.created_date,'%d-%m-%Y') as created_date,a.courier_type, concat(b.user_code,' ',b.user_firstname,' ',b.user_lastname) as created_by," +
                    " date_format(ack_date,'%d-%m-%Y') as ack_date, ackby_name FROM ids_trn_tcouriermgnt a" +
                    " left join adm_mst_tuser b on a.created_by=b.user_gid" +
                    " inner join ids_trn_tcourierhandoverto c on a.couriermgmt_gid=c.couriermgmt_gid" +
                    " where c.employee_gid='" + employee_gid + "' and ack_status='Pending' order by a.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getCourierMgmtPendig = new List<CourierAckPending>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getCourierMgmtPendig.Add(new CourierAckPending
                    {
                        courierMgmt_gid = dt["couriermgmt_gid"].ToString(),
                        courierref_no = dt["courierref_no"].ToString(),
                        date_of_courier = dt["date_of_courier"].ToString(),
                        ack_status = dt["ack_status"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        courier_type = dt["courier_type"].ToString(),
                        ack_date = dt["ack_date"].ToString(),
                        ackby_name = dt["ackby_name"].ToString(),
                    });
                }
                objCourierList.CourierAckPending = getCourierMgmtPendig;
            }
            dt_datatable.Dispose();

        }

        public void DaCourierAckView(string courierMgmt_gid, CourierMgmt values)
        {
            try
            {
                msSQL = " SELECT courierref_no, date_format(date_of_courier,'%d-%m-%Y') as date_of_courier," +
                        " address, sanctionref_no, customer_name, document_type, sender_name, pod_no, couriercompany_name," +
                        " courierhandover_to, courier_type, remarks,date_format(ack_date,'%d-%m-%Y') as ack_date, address, ack_status, ackby_name" +
                        " FROM ids_trn_tcouriermgnt WHERE couriermgmt_gid='" + courierMgmt_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows)
                {
                    objODBCDataReader.Read();

                    values.courierref_no = objODBCDataReader["courierref_no"].ToString();
                    values.date_of_courier = objODBCDataReader["date_of_courier"].ToString();
                    values.sanctionref_no = objODBCDataReader["sanctionref_no"].ToString();
                    values.customer_name = objODBCDataReader["customer_name"].ToString();
                    values.document_type = objODBCDataReader["document_type"].ToString();
                    values.sender_name = objODBCDataReader["sender_name"].ToString();
                    values.pod_no = objODBCDataReader["pod_no"].ToString();
                    values.couriercompany_name = objODBCDataReader["couriercompany_name"].ToString();
                    values.courierhandover_to = objODBCDataReader["courierhandover_to"].ToString();
                    values.courier_type = objODBCDataReader["courier_type"].ToString();
                    values.address = objODBCDataReader["address"].ToString();
                    values.ack_status = objODBCDataReader["ack_status"].ToString();
                    values.remarks = objODBCDataReader["remarks"].ToString();
                    values.ack_date = objODBCDataReader["ack_date"].ToString();
                    values.ackby_name = objODBCDataReader["ackby_name"].ToString();
                }
                objODBCDataReader.Close();
                values.status = true;
                values.message = "Data Fetched";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.Message;
            }

        }

        public result DaAckStatus(string employee_gid, CourierMgmt values)
        {
            msSQL = " UPDATE ids_trn_tcouriermgnt SET" +
                    " ack_status='Acknowledged'," +
                    " ack_date=current_timestamp," +
                    " ackby_gid='" + employee_gid + "'," +
                    " ackby_name=(select concat(a.user_firstname,' ',a.user_lastname,'/',a.user_code) from adm_mst_tuser a left join hrm_mst_temployee b ON a.user_gid=b.user_gid where b.employee_gid='" + employee_gid + "')" +
                    " WHERE couriermgmt_gid='" + values.courierMgmt_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Acknowledged Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
            return values;
        }

        public void DaGetACKNotification(string employee_gid, string user_gid, CourierMgmt values)
        {
            msSQL = " SELECT ack_status FROM ids_trn_tcouriermgnt a "+
                  " inner join ids_trn_tcourierhandoverto b on a.couriermgmt_gid=b.couriermgmt_gid" +
                  " WHERE a.ack_status = 'Pending'  and b.employee_gid='" + employee_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.ack_status = objODBCDataReader["ack_status"].ToString();
            }
            objODBCDataReader.Close();
        }
    }
}