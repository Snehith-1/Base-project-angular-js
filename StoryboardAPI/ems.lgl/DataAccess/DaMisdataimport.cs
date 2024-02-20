using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using System.IO;
using ems.lgl.Models;
using ems.utilities.Functions;
using ems.storage.Functions;
using System.Configuration;

namespace ems.lgl.DataAccess
{
    public class DaMisdataimport
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader;
        HttpPostedFile httpPostedFile;
        DataTable dt_datatable;
        string msSQL, msGetGid, msGETGID;
        int mnResult, mnResult1;
        string lspath, lsnatureof_credit_amount;
        string file_date, current_date, lsfile_name;
        string lsaccount_no, lsod_days,lsurn, lsdntemplate_content,lsvertical;
        string  lscontent = string.Empty;
        string  address2 = string.Empty;
        string lstotaldn_count, lseligibledn_count, lsdngenerated_count,lsdnsent_count, lsdnskip_count, lsreverted_count, lsdn_hold,lsdnpending;
        public bool DaPostExcelUpload(HttpRequest httpRequest, string employee_gid,excel values)
        {
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
            try
            {
                msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
              //string mysql_path= "C:/ProgramData/MySQL/MySQL Server 5.7/uploads"+"/ erp_documents" + " / " + lscompany_code + " / " + "LGL / MISALLdata / " + DateTime.Now.Year + " / " + DateTime.Now.Month;
              //  {
              //      if ((!System.IO.Directory.Exists(mysql_path)))
              //          System.IO.Directory.CreateDirectory(mysql_path);
              //  }
                path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "LGL/MISALLdata/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
                {
                    if ((!System.IO.Directory.Exists(path)))
                        System.IO.Directory.CreateDirectory(path);
                }
                if (httpRequest.Files.Count > 0)
                {
                    string lsfirstdocument_filepath = string.Empty;
                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {
                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        string file_name = httpPostedFile.FileName;
                        lsfile_name = file_name;
                        string lsfile_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        if (FileExtension != ".csv")
                        {
                            values.message = "The uploaded file format is not accepted. Kindly select .csv format";
                            return false;
                        }
                        file_name = Path.GetFileNameWithoutExtension(file_name);
                        string name = file_name;
                        string[] splits = name.Split('-');
                        try
                        {
                            file_date = splits[2] + "-" + splits[3] + "-" + splits[4];
                        }
                        catch (Exception ex)
                        {
                            values.status = false;
                            values.message = "Select correct file fotmat";
                            return false;
                        }
                        current_date = DateTime.Now.ToString("yyyy-MM-dd");

                        if (file_date == current_date)
                        {
                            msSQL = "select date_format(file_date,'%d-%m-%Y') as file_date from lgl_tmp_tmisdata  where file_date='" + current_date + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                objODBCDatareader.Close();
                                values.status = false;
                                values.message = "Already Imported All Data Modified";
                                return false;
                            }
                            else
                            {
                                objODBCDatareader.Close();
                                msGETGID = objcmnfunctions.GetMasterGID("MDIT");
                                lsfile_gid = msGETGID + FileExtension;
                                ls_readStream = httpPostedFile.InputStream;
                                ls_readStream.CopyTo(ms);

                                byte[] bytes = ms.ToArray();
                                if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                                {
									values.status = false;
                                    values.message = "File format is not supported";
                                    return false;
                                }

                                try
                                {
                                    bool status;
                                    status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "LGL/MISALLdata/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msGETGID + FileExtension, ms);
                                    ms.Close();
                                    lspath = "erpdocument" + "/" + lscompany_code + "/" + "LGL/MISALLdata/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                                    //lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "LGL/MISALLdata/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                                    //objcmnfunctions.uploadFile(lspath, lsfile_gid);
                                    string lsreppath = lspath.Replace('\\', '/') + "/" + lsfile_gid;

                                    msSQL = "delete from lgl_tmp_tmisdata ";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    msSQL = " LOAD DATA INFILE '" + lsreppath + "'" +
                                            " INTO TABLE lgl_tmp_tmisdata" +
                                            " FIELDS TERMINATED BY ','" +
                                            " ENCLOSED BY '\"' " +
                                            " LINES TERMINATED BY '\r\n'" +
                                            " IGNORE 1 LINES" +
                                            " (@AcNo, @Disbdate,@DisbAmt,@Maturitydate,@PrincipalRepaid,@Interest,@ODDAYS,@RPA,@TotalMiscChar,@LatechargeDue,@Latecharge,@Ledger," +
                                            " @Adjustedbalanceforaccrual,@CurrentPrincipal,@TotalIntdue,@Acstatus,@Accloseddate,@Lastpayment,@Accrued,@OrginalNoof,@Payment,@Tag," +
                                            " @URN,@Dateoffirst,@Interest,@Paymentcurrent,@Tenure,@Frequency,@ScheduldePayment,@NetPayoffAmt,@UncollectedPrincipal,@UncollectedInterest," +
                                            " @ScheduledPayment,@AccountName,@ProductType,@ProductCode,@BranchCode,@BranchName,@PenalInterestRat,@GlSubhead,@OpenedOnValueDate," +
                                            " @NextDemandRunDat,@LastDemandRunDate,@LastBookingDat,@LastArchiveDat,@NumAdvanceSatisfiedDema,@BookedNotDuePenalInter,@BookedNormalIntere," +
                                            " @BookedPenalIntere,@TotalDisbursedAmo,@TotalRepaidAmou,@TotalPenalInterestR,@TotalDemandDu,@TotalFeePaid,@GurantorCustomerId,@PhoneNo.," +
                                            " @OpenUserId,@Fee1Category,@Fee1Amount,@Fee1GrossAmount,@Fee1PaymentStatus,@Fee2Category,@Fee2Amount,@Fee2GrossAmount,@Fee2PaymentStatus," +
                                            " @Fee3Category,@Fee3Amount,@Fee3GrossAmount,@Fee3PaymentStatus,@Fee4Category,@Fee4Amount,@Fee4GrossAmount,@Fee4Payment,@Fee5Category," +
                                            " @Fee5Amount,@Fee5GrossAmount,@Fee5PaymentStatus,@Customername,@GuarantorName,@GuarantorPhno,@CustomerBranchName,@AssetCategory,@ProvisionedAmount," +
                                            " @CustomerType,@Vertical,@DocumentType,@DocumentNum,@PrimaryValueChain,@SecondaryValueChain,@Intervention,@Hypothecatedto,@LoanSourcingDetails," +
                                            " @K2BNameReferralName,@K2BCodeReferralCode,@SamunnatiPaycard,@Collateral,@Address1,@Address2,@Address3,@City,@State,@PinCode,@PhoneNo.,@EmailId," +
                                            " @GSTIN,@ROName,@MoratoriumInterest,@MoratoriumTenure,@MoratoriumPayOffAmount,@NormalInterestSuspense,@PenalIneterstSuspense,@CollateralValue," +
                                            " @SBU,@CurrentRM,@ProductSolutions,@CustomerUMRN,@GuarantorUMRN,@Subproduct)" +
                                            " set account_no = @AcNo, disbursement_date = @Disbdate, disbursement_amount = @DisbAmt,maturity_date = @Maturitydate," +
                                            " principal_repaid = @PrincipalRepaid, interest1 = @Interest, od_days = @ODDAYS, rpa = @RPA,total_misc_char = @TotalMiscChar," +
                                            " late_charge_due = @LatechargeDue, late_charge = @Latecharge, ledger = @Ledger,adjusted_balance_accrual = @Adjustedbalanceforaccrual," +
                                            " current_principal = @CurrentPrincipal, total_Int_due = @TotalIntdue, ac_status = @Acstatus, ac_closed_date = @Accloseddate," +
                                            " last_payment = @Lastpayment, accrued = @Accrued, original_no = @OrginalNoof, payment = @Payment, tag = @Tag, urn = @URN," +
                                            " date_of_first = @Dateoffirst, interest2 = @Interest, payment_current = @Paymentcurrent," +
                                            " tenure = @Tenure, frequency = @Frequency, schedulde_payment = @ScheduldePayment, Net_Payoff_Amt = @NetPayoffAmt," +
                                            " Uncollected_Principal = @UncollectedPrincipal, Uncollected_Interest = @UncollectedInterest, Scheduled_Payment = @ScheduledPayment," +
                                            " AccountName = @AccountName, ProductType = @ProductType, ProductCode = @ProductCode," +
                                            " BranchCode = @BranchCode, BranchName = @BranchName, PenalInterestRat = @PenalInterestRat, GlSubhead = @GlSubhead," +
                                            " openedonvaluedate = @OpenedOnValueDate, nextdemandrundat = @NextDemandRunDat, lastdemandrundate = @LastDemandRunDate," +
                                            " lastbookingdat = @LastBookingDat, lastarchivedat = @LastArchiveDat,numadvanceaatisfieddema = @NumAdvanceSatisfiedDema," +
                                            " BookedNotDuePenalInter = @BookedNotDuePenalInter, BookedNormalIntere = @BookedNormalIntere," +
                                            " BookedPenalIntere = @BookedPenalIntere, TotalDisbursedAmo = @TotalDisbursedAmo, TotalRepaidAmou = @TotalRepaidAmou," +
                                            " TotalPenalInterestR = @TotalPenalInterestR, TotalDemandDu = @TotalDemandDu, TotalFeePaid = @TotalFeePaid," +
                                            " GurantorCustomerId = @GurantorCustomerId, PhoneNo = @PhoneNo., Open_UserId = @OpenUserId, Fee1_Category = @Fee1Category," +
                                            " Fee1_Amount = @Fee1Amount, Fee1_Gross_Amount = @Fee1GrossAmount, Fee1_Payment_Status = @Fee1PaymentStatus, Fee2_Category = @Fee2Category," +
                                            " Fee2_Amount = @Fee2Amount, Fee2_Gross_Amount = @Fee2GrossAmount,Fee2_Payment_Status = @Fee2PaymentStatus, Fee3_Category = @Fee3Category," +
                                            " Fee3_Amount = @Fee3Amount, Fee3_Gross_Amount = @Fee3GrossAmount, Fee3_Payment_Status = @Fee3PaymentStatus, Fee4_Category = @Fee4Category," +
                                            " Fee4_Amount = @Fee4Amount, Fee4_Gross_Amount = @Fee4GrossAmount, Fee4_Payment = @Fee4Payment, Fee5_Category = @Fee5Category," +
                                            " Fee5_Amount = @Fee5Amount, Fee5_Gross_Amount = @Fee5GrossAmount, Fee5_Payment_Status = @Fee5PaymentStatus," +
                                            " Customer_name = @Customername, Guarantor_Name = @GuarantorName, Guarantor_Ph_no = @GuarantorPhno," +
                                            " Customer_BranchName = @CustomerBranchName, Asset_Category = @AssetCategory, Provisioned_Amount = @ProvisionedAmount," +
                                            " Customer_Type = @CustomerType, Vertical = @Vertical, Document_Type = @DocumentType, Document_Num = @DocumentNum," +
                                            " Primary_Value_Chain = @PrimaryValueChain, Secondary_Value_Chain = @SecondaryValueChain, Intervention = @Intervention," +
                                            " Hypothecated_to = @Hypothecatedto, Loan_Sourcing_Details = @LoanSourcingDetails," +
                                            " K2BName_Referral_Name = @K2BNameReferralName, K2BCode_Referral_Code = @K2BCodeReferralCode," +
                                            " Samunnati_Pay_card = @SamunnatiPaycard, Collateral = @Collateral, Address1 = @Address1, Address2 = @Address2, Address3 = @Address3," +
                                            " City = @City, State = @State, Pin_code = @PinCode, phone_no = @PhoneNo., Email_id = @EmailId," +
                                            " gstin = @GSTIN, RO_Name = @ROName, Moratorium_Interest = @MoratoriumInterest, Moratorium_Tenure = @MoratoriumTenure," +
                                            " MoratoriumPayOffAmount = @MoratoriumPayOffAmount, NormalInterestSuspense = @NormalInterestSuspense," +
                                            " PenalIneterstSuspense = @PenalIneterstSuspense, Collateral_Value = @CollateralValue," +
                                            " SBU = @SBU, Current_RM = @CurrentRM, Product_Solutions = @ProductSolutions, Customer_UMRN = @CustomerUMRN," +
                                            " Guarantor_UMRN = @GuarantorUMRN, Subproduct = @Subproduct";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    if(mnResult==0)
                                    {
                                        string mysql_path = "C:/ProgramData/MySQL/MySQL Server 5.7/uploads/";


                                        objcmnfunctions.uploadFile(mysql_path, lsfile_gid);



                                        string lsmysql_path = mysql_path + lsfile_gid;
                                        msSQL = "delete from lgl_tmp_tmisdata ";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                        msSQL = " LOAD DATA INFILE '" + lsmysql_path + "'" +
                                                " INTO TABLE lgl_tmp_tmisdata" +
                                                " FIELDS TERMINATED BY ','" +
                                                " ENCLOSED BY '\"' " +
                                                " LINES TERMINATED BY '\r\n'" +
                                                " IGNORE 1 LINES" +
                                                " (@AcNo, @Disbdate,@DisbAmt,@Maturitydate,@PrincipalRepaid,@Interest,@ODDAYS,@RPA,@TotalMiscChar,@LatechargeDue,@Latecharge,@Ledger," +
                                                " @Adjustedbalanceforaccrual,@CurrentPrincipal,@TotalIntdue,@Acstatus,@Accloseddate,@Lastpayment,@Accrued,@OrginalNoof,@Payment,@Tag," +
                                                " @URN,@Dateoffirst,@Interest,@Paymentcurrent,@Tenure,@Frequency,@ScheduldePayment,@NetPayoffAmt,@UncollectedPrincipal,@UncollectedInterest," +
                                                " @ScheduledPayment,@AccountName,@ProductType,@ProductCode,@BranchCode,@BranchName,@PenalInterestRat,@GlSubhead,@OpenedOnValueDate," +
                                                " @NextDemandRunDat,@LastDemandRunDate,@LastBookingDat,@LastArchiveDat,@NumAdvanceSatisfiedDema,@BookedNotDuePenalInter,@BookedNormalIntere," +
                                                " @BookedPenalIntere,@TotalDisbursedAmo,@TotalRepaidAmou,@TotalPenalInterestR,@TotalDemandDu,@TotalFeePaid,@GurantorCustomerId,@PhoneNo.," +
                                                " @OpenUserId,@Fee1Category,@Fee1Amount,@Fee1GrossAmount,@Fee1PaymentStatus,@Fee2Category,@Fee2Amount,@Fee2GrossAmount,@Fee2PaymentStatus," +
                                                " @Fee3Category,@Fee3Amount,@Fee3GrossAmount,@Fee3PaymentStatus,@Fee4Category,@Fee4Amount,@Fee4GrossAmount,@Fee4Payment,@Fee5Category," +
                                                " @Fee5Amount,@Fee5GrossAmount,@Fee5PaymentStatus,@Customername,@GuarantorName,@GuarantorPhno,@CustomerBranchName,@AssetCategory,@ProvisionedAmount," +
                                                " @CustomerType,@Vertical,@DocumentType,@DocumentNum,@PrimaryValueChain,@SecondaryValueChain,@Intervention,@Hypothecatedto,@LoanSourcingDetails," +
                                                " @K2BNameReferralName,@K2BCodeReferralCode,@SamunnatiPaycard,@Collateral,@Address1,@Address2,@Address3,@City,@State,@PinCode,@PhoneNo.,@EmailId," +
                                                " @GSTIN,@ROName,@MoratoriumInterest,@MoratoriumTenure,@MoratoriumPayOffAmount,@NormalInterestSuspense,@PenalIneterstSuspense,@CollateralValue," +
                                                " @SBU,@CurrentRM,@ProductSolutions,@CustomerUMRN,@GuarantorUMRN,@Subproduct)" +
                                                " set account_no = @AcNo, disbursement_date = @Disbdate, disbursement_amount = @DisbAmt,maturity_date = @Maturitydate," +
                                                " principal_repaid = @PrincipalRepaid, interest1 = @Interest, od_days = @ODDAYS, rpa = @RPA,total_misc_char = @TotalMiscChar," +
                                                " late_charge_due = @LatechargeDue, late_charge = @Latecharge, ledger = @Ledger,adjusted_balance_accrual = @Adjustedbalanceforaccrual," +
                                                " current_principal = @CurrentPrincipal, total_Int_due = @TotalIntdue, ac_status = @Acstatus, ac_closed_date = @Accloseddate," +
                                                " last_payment = @Lastpayment, accrued = @Accrued, original_no = @OrginalNoof, payment = @Payment, tag = @Tag, urn = @URN," +
                                                " date_of_first = @Dateoffirst, interest2 = @Interest, payment_current = @Paymentcurrent," +
                                                " tenure = @Tenure, frequency = @Frequency, schedulde_payment = @ScheduldePayment, Net_Payoff_Amt = @NetPayoffAmt," +
                                                " Uncollected_Principal = @UncollectedPrincipal, Uncollected_Interest = @UncollectedInterest, Scheduled_Payment = @ScheduledPayment," +
                                                " AccountName = @AccountName, ProductType = @ProductType, ProductCode = @ProductCode," +
                                                " BranchCode = @BranchCode, BranchName = @BranchName, PenalInterestRat = @PenalInterestRat, GlSubhead = @GlSubhead," +
                                                " openedonvaluedate = @OpenedOnValueDate, nextdemandrundat = @NextDemandRunDat, lastdemandrundate = @LastDemandRunDate," +
                                                " lastbookingdat = @LastBookingDat, lastarchivedat = @LastArchiveDat,numadvanceaatisfieddema = @NumAdvanceSatisfiedDema," +
                                                " BookedNotDuePenalInter = @BookedNotDuePenalInter, BookedNormalIntere = @BookedNormalIntere," +
                                                " BookedPenalIntere = @BookedPenalIntere, TotalDisbursedAmo = @TotalDisbursedAmo, TotalRepaidAmou = @TotalRepaidAmou," +
                                                " TotalPenalInterestR = @TotalPenalInterestR, TotalDemandDu = @TotalDemandDu, TotalFeePaid = @TotalFeePaid," +
                                                " GurantorCustomerId = @GurantorCustomerId, PhoneNo = @PhoneNo., Open_UserId = @OpenUserId, Fee1_Category = @Fee1Category," +
                                                " Fee1_Amount = @Fee1Amount, Fee1_Gross_Amount = @Fee1GrossAmount, Fee1_Payment_Status = @Fee1PaymentStatus, Fee2_Category = @Fee2Category," +
                                                " Fee2_Amount = @Fee2Amount, Fee2_Gross_Amount = @Fee2GrossAmount,Fee2_Payment_Status = @Fee2PaymentStatus, Fee3_Category = @Fee3Category," +
                                                " Fee3_Amount = @Fee3Amount, Fee3_Gross_Amount = @Fee3GrossAmount, Fee3_Payment_Status = @Fee3PaymentStatus, Fee4_Category = @Fee4Category," +
                                                " Fee4_Amount = @Fee4Amount, Fee4_Gross_Amount = @Fee4GrossAmount, Fee4_Payment = @Fee4Payment, Fee5_Category = @Fee5Category," +
                                                " Fee5_Amount = @Fee5Amount, Fee5_Gross_Amount = @Fee5GrossAmount, Fee5_Payment_Status = @Fee5PaymentStatus," +
                                                " Customer_name = @Customername, Guarantor_Name = @GuarantorName, Guarantor_Ph_no = @GuarantorPhno," +
                                                " Customer_BranchName = @CustomerBranchName, Asset_Category = @AssetCategory, Provisioned_Amount = @ProvisionedAmount," +
                                                " Customer_Type = @CustomerType, Vertical = @Vertical, Document_Type = @DocumentType, Document_Num = @DocumentNum," +
                                                " Primary_Value_Chain = @PrimaryValueChain, Secondary_Value_Chain = @SecondaryValueChain, Intervention = @Intervention," +
                                                " Hypothecated_to = @Hypothecatedto, Loan_Sourcing_Details = @LoanSourcingDetails," +
                                                " K2BName_Referral_Name = @K2BNameReferralName, K2BCode_Referral_Code = @K2BCodeReferralCode," +
                                                " Samunnati_Pay_card = @SamunnatiPaycard, Collateral = @Collateral, Address1 = @Address1, Address2 = @Address2, Address3 = @Address3," +
                                                " City = @City, State = @State, Pin_code = @PinCode, phone_no = @PhoneNo., Email_id = @EmailId," +
                                                " gstin = @GSTIN, RO_Name = @ROName, Moratorium_Interest = @MoratoriumInterest, Moratorium_Tenure = @MoratoriumTenure," +
                                                " MoratoriumPayOffAmount = @MoratoriumPayOffAmount, NormalInterestSuspense = @NormalInterestSuspense," +
                                                " PenalIneterstSuspense = @PenalIneterstSuspense, Collateral_Value = @CollateralValue," +
                                                " SBU = @SBU, Current_RM = @CurrentRM, Product_Solutions = @ProductSolutions, Customer_UMRN = @CustomerUMRN," +
                                                " Guarantor_UMRN = @GuarantorUMRN, Subproduct = @Subproduct";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                       
                                    }
                                    msSQL = "update lgl_tmp_tmisdata set file_date='" + file_date + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                                    msGETGID = objcmnfunctions.GetMasterGID("MDIT");
                                    msSQL = "insert into lgl_trn_tmisdocumentimport (" +
                                        " misdocumentimport_gid ," +
                                        " imported_date, " +
                                        " imported_by, " +
                                        " file_name, " +
                                        " status," +
                                        " created_by, " +
                                        " created_date)" +
                                        " values (" +
                                        "'" + msGETGID + "'," +
                                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                        "'" + employee_gid + "'," +
                                        "'" + lsfile_name + "'," +
                                        "'Pending'," +
                                        "'" + employee_gid + "'," +
                                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                                catch
                                {
                                    string mysql_path = "C:/ProgramData/MySQL/MySQL Server 5.7/uploads/";


                                    objcmnfunctions.uploadFile(mysql_path, lsfile_gid);



                                    string lsmysql_path = mysql_path + lsfile_gid;
                                    msSQL = "delete from lgl_tmp_tmisdata ";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    msSQL = " LOAD DATA INFILE '" + lsmysql_path + "'" +
                                            " INTO TABLE lgl_tmp_tmisdata" +
                                            " FIELDS TERMINATED BY ','" +
                                            " ENCLOSED BY '\"' " +
                                            " LINES TERMINATED BY '\r\n'" +
                                            " IGNORE 1 LINES" +
                                            " (@AcNo, @Disbdate,@DisbAmt,@Maturitydate,@PrincipalRepaid,@Interest,@ODDAYS,@RPA,@TotalMiscChar,@LatechargeDue,@Latecharge,@Ledger," +
                                            " @Adjustedbalanceforaccrual,@CurrentPrincipal,@TotalIntdue,@Acstatus,@Accloseddate,@Lastpayment,@Accrued,@OrginalNoof,@Payment,@Tag," +
                                            " @URN,@Dateoffirst,@Interest,@Paymentcurrent,@Tenure,@Frequency,@ScheduldePayment,@NetPayoffAmt,@UncollectedPrincipal,@UncollectedInterest," +
                                            " @ScheduledPayment,@AccountName,@ProductType,@ProductCode,@BranchCode,@BranchName,@PenalInterestRat,@GlSubhead,@OpenedOnValueDate," +
                                            " @NextDemandRunDat,@LastDemandRunDate,@LastBookingDat,@LastArchiveDat,@NumAdvanceSatisfiedDema,@BookedNotDuePenalInter,@BookedNormalIntere," +
                                            " @BookedPenalIntere,@TotalDisbursedAmo,@TotalRepaidAmou,@TotalPenalInterestR,@TotalDemandDu,@TotalFeePaid,@GurantorCustomerId,@PhoneNo.," +
                                            " @OpenUserId,@Fee1Category,@Fee1Amount,@Fee1GrossAmount,@Fee1PaymentStatus,@Fee2Category,@Fee2Amount,@Fee2GrossAmount,@Fee2PaymentStatus," +
                                            " @Fee3Category,@Fee3Amount,@Fee3GrossAmount,@Fee3PaymentStatus,@Fee4Category,@Fee4Amount,@Fee4GrossAmount,@Fee4Payment,@Fee5Category," +
                                            " @Fee5Amount,@Fee5GrossAmount,@Fee5PaymentStatus,@Customername,@GuarantorName,@GuarantorPhno,@CustomerBranchName,@AssetCategory,@ProvisionedAmount," +
                                            " @CustomerType,@Vertical,@DocumentType,@DocumentNum,@PrimaryValueChain,@SecondaryValueChain,@Intervention,@Hypothecatedto,@LoanSourcingDetails," +
                                            " @K2BNameReferralName,@K2BCodeReferralCode,@SamunnatiPaycard,@Collateral,@Address1,@Address2,@Address3,@City,@State,@PinCode,@PhoneNo.,@EmailId," +
                                            " @GSTIN,@ROName,@MoratoriumInterest,@MoratoriumTenure,@MoratoriumPayOffAmount,@NormalInterestSuspense,@PenalIneterstSuspense,@CollateralValue," +
                                            " @SBU,@CurrentRM,@ProductSolutions,@CustomerUMRN,@GuarantorUMRN,@Subproduct)" +
                                            " set account_no = @AcNo, disbursement_date = @Disbdate, disbursement_amount = @DisbAmt,maturity_date = @Maturitydate," +
                                            " principal_repaid = @PrincipalRepaid, interest1 = @Interest, od_days = @ODDAYS, rpa = @RPA,total_misc_char = @TotalMiscChar," +
                                            " late_charge_due = @LatechargeDue, late_charge = @Latecharge, ledger = @Ledger,adjusted_balance_accrual = @Adjustedbalanceforaccrual," +
                                            " current_principal = @CurrentPrincipal, total_Int_due = @TotalIntdue, ac_status = @Acstatus, ac_closed_date = @Accloseddate," +
                                            " last_payment = @Lastpayment, accrued = @Accrued, original_no = @OrginalNoof, payment = @Payment, tag = @Tag, urn = @URN," +
                                            " date_of_first = @Dateoffirst, interest2 = @Interest, payment_current = @Paymentcurrent," +
                                            " tenure = @Tenure, frequency = @Frequency, schedulde_payment = @ScheduldePayment, Net_Payoff_Amt = @NetPayoffAmt," +
                                            " Uncollected_Principal = @UncollectedPrincipal, Uncollected_Interest = @UncollectedInterest, Scheduled_Payment = @ScheduledPayment," +
                                            " AccountName = @AccountName, ProductType = @ProductType, ProductCode = @ProductCode," +
                                            " BranchCode = @BranchCode, BranchName = @BranchName, PenalInterestRat = @PenalInterestRat, GlSubhead = @GlSubhead," +
                                            " openedonvaluedate = @OpenedOnValueDate, nextdemandrundat = @NextDemandRunDat, lastdemandrundate = @LastDemandRunDate," +
                                            " lastbookingdat = @LastBookingDat, lastarchivedat = @LastArchiveDat,numadvanceaatisfieddema = @NumAdvanceSatisfiedDema," +
                                            " BookedNotDuePenalInter = @BookedNotDuePenalInter, BookedNormalIntere = @BookedNormalIntere," +
                                            " BookedPenalIntere = @BookedPenalIntere, TotalDisbursedAmo = @TotalDisbursedAmo, TotalRepaidAmou = @TotalRepaidAmou," +
                                            " TotalPenalInterestR = @TotalPenalInterestR, TotalDemandDu = @TotalDemandDu, TotalFeePaid = @TotalFeePaid," +
                                            " GurantorCustomerId = @GurantorCustomerId, PhoneNo = @PhoneNo., Open_UserId = @OpenUserId, Fee1_Category = @Fee1Category," +
                                            " Fee1_Amount = @Fee1Amount, Fee1_Gross_Amount = @Fee1GrossAmount, Fee1_Payment_Status = @Fee1PaymentStatus, Fee2_Category = @Fee2Category," +
                                            " Fee2_Amount = @Fee2Amount, Fee2_Gross_Amount = @Fee2GrossAmount,Fee2_Payment_Status = @Fee2PaymentStatus, Fee3_Category = @Fee3Category," +
                                            " Fee3_Amount = @Fee3Amount, Fee3_Gross_Amount = @Fee3GrossAmount, Fee3_Payment_Status = @Fee3PaymentStatus, Fee4_Category = @Fee4Category," +
                                            " Fee4_Amount = @Fee4Amount, Fee4_Gross_Amount = @Fee4GrossAmount, Fee4_Payment = @Fee4Payment, Fee5_Category = @Fee5Category," +
                                            " Fee5_Amount = @Fee5Amount, Fee5_Gross_Amount = @Fee5GrossAmount, Fee5_Payment_Status = @Fee5PaymentStatus," +
                                            " Customer_name = @Customername, Guarantor_Name = @GuarantorName, Guarantor_Ph_no = @GuarantorPhno," +
                                            " Customer_BranchName = @CustomerBranchName, Asset_Category = @AssetCategory, Provisioned_Amount = @ProvisionedAmount," +
                                            " Customer_Type = @CustomerType, Vertical = @Vertical, Document_Type = @DocumentType, Document_Num = @DocumentNum," +
                                            " Primary_Value_Chain = @PrimaryValueChain, Secondary_Value_Chain = @SecondaryValueChain, Intervention = @Intervention," +
                                            " Hypothecated_to = @Hypothecatedto, Loan_Sourcing_Details = @LoanSourcingDetails," +
                                            " K2BName_Referral_Name = @K2BNameReferralName, K2BCode_Referral_Code = @K2BCodeReferralCode," +
                                            " Samunnati_Pay_card = @SamunnatiPaycard, Collateral = @Collateral, Address1 = @Address1, Address2 = @Address2, Address3 = @Address3," +
                                            " City = @City, State = @State, Pin_code = @PinCode, phone_no = @PhoneNo., Email_id = @EmailId," +
                                            " gstin = @GSTIN, RO_Name = @ROName, Moratorium_Interest = @MoratoriumInterest, Moratorium_Tenure = @MoratoriumTenure," +
                                            " MoratoriumPayOffAmount = @MoratoriumPayOffAmount, NormalInterestSuspense = @NormalInterestSuspense," +
                                            " PenalIneterstSuspense = @PenalIneterstSuspense, Collateral_Value = @CollateralValue," +
                                            " SBU = @SBU, Current_RM = @CurrentRM, Product_Solutions = @ProductSolutions, Customer_UMRN = @CustomerUMRN," +
                                            " Guarantor_UMRN = @GuarantorUMRN, Subproduct = @Subproduct";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    msSQL = "update lgl_tmp_tmisdata set file_date='" + file_date + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                                    msGETGID = objcmnfunctions.GetMasterGID("MDIT");
                                    msSQL = "insert into lgl_trn_tmisdocumentimport (" +
                                        " misdocumentimport_gid ," +
                                        " imported_date, " +
                                        " imported_by, " +
                                        " file_name, " +
                                        " status," +
                                        " created_by, " +
                                        " created_date)" +
                                        " values (" +
                                        "'" + msGETGID + "'," +
                                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                        "'" + employee_gid + "'," +
                                        "'" + lsfile_name + "'," +
                                        "'Pending'," +
                                        "'" + employee_gid + "'," +
                                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                           }
                        }
                        else
                        {
                            values.status = false;
                            values.message = "Check the File. It accepted only current date";
                            return false;
                        }
                    }
                }
                else
                {
                    values.status = false;
                    values.message = "Select File";
                    return false;
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.ToString();
                return false;
            }
                      
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "All Data Modified Imported Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Importing!";
                return false;
            }
        }

        public bool DaGetMisDataDetail(MdlMisdataimportlist values)
        {
            msSQL = " select misdocumentimport_gid,date_format(imported_date,'%d-%m-%Y %H:%i %p') as imported_date,"+
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as imported_by," +
                    " date_format(process_date,'%d-%m-%Y %H:%i %p') as process_date,status,file_name " +
                    " from lgl_trn_tmisdocumentimport a, hrm_mst_temployee b," +
                    " adm_mst_tuser c where a.imported_by = b.employee_gid and b.user_gid = c.user_gid order by misdocumentimport_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdataimport = new List<imported_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getdataimport.Add(new imported_list
                    {
                        misdocumentimport_gid = dr_datarow["misdocumentimport_gid"].ToString(),
                        imported_date = dr_datarow["imported_date"].ToString(),
                        imported_by = dr_datarow["imported_by"].ToString(),
                        process_date = dr_datarow["process_date"].ToString(),
                        status = dr_datarow["status"].ToString(),
                        file_name = dr_datarow["file_name"].ToString()
                    });
                }
                values.imported_list = getdataimport;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }

        public bool DaPostProcessData(MdlMisdataimportlist values, string employee_gid)
        {
            msSQL = "select count(distinct urn) from lgl_trn_tmisdata";
            lstotaldn_count = objdbconn.GetExecuteScalar(msSQL);

           msSQL="select count(distinct urn) from lgl_trn_tmisdata where od_days=16 or od_days=31 or od_days=46";
            lseligibledn_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(distinct urn) from lgl_trn_tmisdata where dn_status = 'DN1 Generated' or dn_status = 'DN2 Generated' or dn_status = 'DN3 Generated' or"+
                      " dn_status = 'DN Generated'";
            lsdngenerated_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(distinct urn) from lgl_trn_tmisdata where dn_status='Pending'";
            lsdnpending = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(distinct urn) from lgl_trn_tmisdata where dn_status = 'DN1 Skip' or dn_status = 'DN2 Skip' or dn_status = 'DN3 Skip'"+
                    " or dn_status = 'DN Skip'";
            lsdnskip_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(distinct urn) from lgl_trn_tmisdata where dn_status = 'DN1 Sent' or dn_status = 'DN2 Sent' or dn_status = 'DN3 Sent'"+
                    " or dn_status = 'DN Sent'";
            lsdnsent_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(distinct urn) from lgl_trn_tmisdata where dn_status = 'DN1 Reverted' or dn_status = 'DN2 Reverted' or dn_status = 'DN3 Reverted' or"+
                  " dn_status = 'DN Reverted'";
            lsreverted_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(distinct urn) from lgl_trn_tmisdata where dn_status = 'DN1 Hold' or dn_status = 'DN2 Hold' or dn_status = 'DN3 Hold'"+
                    " or dn_status = 'DN Hold'";
            lsdn_hold = objdbconn.GetExecuteScalar(msSQL);

            msGETGID = objcmnfunctions.GetMasterGID("DNCS");
            msSQL = "insert into lgl_rpt_tdntat(" +
                " dntat_gid ," +
                " dn_date,"+
                " dntotal_cases," +
                " dneligiblecases_today," +
                " dngenerated," +
                " dnsent," +
                " dnskip,"+
                " dnhold,"+
                " dnreverted,"+
                " dnpending )" +
                " values (" +
                "'" + msGETGID + "'," +
                "'"+ DateTime.Now.AddDays(-1).ToString ("yyyy-MM-dd")+"',"+
                  "'" + lstotaldn_count + "'," +
                  "'" + lseligibledn_count + "'," +
                  "'" + lsdngenerated_count + "'," +
                  "'" + lsdnsent_count + "'," +
                  "'" + lsdnskip_count + "'," +
                  "'" + lsdn_hold + "'," +
                  "'" + lsreverted_count+"',"+
                  "'" + lsdnpending + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



            msSQL = "insert into lgl_trn_tmisdata(misdata_gid,account_no,disbursement_date,disbursement_amount, maturity_date,od_days,ac_status,ac_closed_date,"+
                      " last_payment,payment,urn,tenure,frequency,schedulde_payment,AccountName,ProductType,ProductCode,nextdemandrundat,lastdemandrundate,"+
                      " Customer_name,Guarantor_Name,GurantorCustomerId,,Vertical,RO_Name,address1,address2,address3,city,state,pincode,Net_Payoff_Amt,TotalDemandDu)" +
                      " select concat('MISD', misdata_gid) as misdata_gid,account_no," +
                      " str_to_date(disbursement_date,'%m/%d/%Y') as disbursement_date,disbursement_amount,"+
                      " str_to_date(maturity_date,'%m/%d/%Y') as maturity_date," +
                      " od_days,ac_status,str_to_date(ac_closed_date,'%m/%d/%Y') as ac_closed_date, str_to_date(last_payment,'%m/%d/%Y') as last_payment,payment,urn,tenure,frequency," +
                      " str_to_date(schedulde_payment,'%m/%d/%Y') as schedulde_payment,AccountName,ProductType,ProductCode,"+
                      " str_to_date(nextdemandrundat,'%m/%d/%Y') as nextdemandrundat," +
                      " str_to_date(lastdemandrundate,'%m/%d/%Y') as lastdemandrundate,Customer_name,Guarantor_Name,GurantorCustomerId,Customer_Type,Vertical,RO_Name, " +
                      "  Address1,Address2,Address3,City,State,Pin_code,Net_Payoff_Amt,TotalDemandDu " +
                      " from lgl_tmp_tmisdata where ac_status=0 and (od_days>0 and od_days<=600) and account_no not in (select account_no from lgl_trn_tmisdata)";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 0)
            {
                msSQL = "insert into lgl_trn_tmisdata(misdata_gid,account_no,disbursement_date,disbursement_amount, maturity_date,od_days,ac_status,ac_closed_date,last_payment,payment,urn," +
                                   " tenure,frequency,schedulde_payment,AccountName,ProductType,ProductCode,nextdemandrundat,lastdemandrundate,Customer_name,Guarantor_Name,GurantorCustomerId," +
                                   " Customer_Type,Vertical,RO_Name,address1,address2,address3,city,state,pincode,Net_Payoff_Amt,TotalDemandDu) select concat('MISD', misdata_gid) as misdata_gid,account_no," +
                                   " str_to_date(disbursement_date,'%d-%m/%Y') as disbursement_date,disbursement_amount,str_to_date(maturity_date,'%d-%m/%Y') as maturity_date," +
                                   " od_days,ac_status,str_to_date(ac_closed_date,,'%d-%m/%Y') as ac_closed_date,last_payment,payment,urn,tenure,frequency," +
                                   " str_to_date(schedulde_payment,'%d-%m/%Y'),AccountName,ProductType,ProductCode,str_to_date(nextdemandrundat,'%d-%m/%Y') as nextdemandrundat," +
                                   " str_to_date(lastdemandrundate,'%d-%m/%Y') as lastdemandrundate,Customer_name,Guarantor_Name,GurantorCustomerId,Customer_Type,Vertical,RO_Name, " +
                                    " Address1,Address2,Address3,City,State,Pin_code,Net_Payoff_Amt,TotalDemandDu " +
                                   " from lgl_tmp_tmisdata where ac_status=0 and (od_days>0 and od_days<=600) and account_no not in (select account_no from lgl_trn_tmisdata)";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 0)
               {
                    msSQL = "insert into lgl_trn_tmisdata(misdata_gid,account_no, disbursement_date,disbursement_amount, maturity_date,od_days,ac_status,ac_closed_date,last_payment,payment,urn," +
                                   " tenure,frequency,schedulde_payment,AccountName,ProductType,ProductCode,nextdemandrundat,lastdemandrundate,Customer_name,Guarantor_Name,GurantorCustomerId," +
                                   " Customer_Type,Vertical,RO_Name,address1,address2,address3,city,state,pincode,Net_Payoff_Amt,TotalDemandDu) select concat('MISD', misdata_gid) as misdata_gid,account_no," +
                                   " disbursement_date,disbursement_amount, maturity_date," +
                                   " od_days,ac_status, ac_closed_date,last_payment,payment,urn,tenure,frequency," +
                                   " schedulde_payment,AccountName,ProductType,ProductCode, nextdemandrundat," +
                                   " lastdemandrundate,Customer_name,Guarantor_Name,GurantorCustomerId,Customer_Type,Vertical,RO_Name, " +
                                   " Address1,Address2,Address3,City,State,Pin_code,Net_Payoff_Amt,TotalDemandDu " +
                                   " from lgl_tmp_tmisdata where ac_status=0 and (od_days>0 and od_days<=600) and account_no not in (select account_no from lgl_trn_tmisdata)";
                    mnResult1 = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult1 == 0)
                    {
                        values.status = false;
                        values.message = "Check the Date Format";
                        return false;
                    }
                }
   
            }    
            //OD Days updation       
                    msSQL = "update lgl_trn_tmisdata a" +
                               " inner join lgl_tmp_tmisdata b on a.account_no = b.account_no and a.urn = b.urn" +
                               " set a.od_days = b.od_days,a.last_payment=b.last_payment,a.schedulde_payment=b.schedulde_payment where a.account_no = b.account_no";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //Account Status updation
                msSQL = "update lgl_trn_tmisdata a" +
                       " inner join lgl_tmp_tmisdata b on a.account_no = b.account_no and a.urn = b.urn" +
                       " set a.ac_status = b.ac_status where a.account_no = b.account_no";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //Over Due updation
            msSQL = "update lgl_trn_tmisdata a" +
                   " inner join lgl_tmp_tmisdata b on a.account_no = b.account_no and a.urn = b.urn" +
                   " set a.TotalDemandDu = b.TotalDemandDu where a.account_no = b.account_no";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //Outstanding updation
            msSQL = "update lgl_trn_tmisdata a" +
                   " inner join lgl_tmp_tmisdata b on a.account_no = b.account_no and a.urn = b.urn" +
                   " set a.Net_Payoff_Amt = b.Net_Payoff_Amt where a.account_no = b.account_no";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //Disbursement date updation
            msSQL = "update lgl_trn_tmisdata a" +
                    " inner join lgl_tmp_tmisdata b on a.account_no = b.account_no and a.urn = b.urn" +
                    " set a.disbursement_date = b.disbursement_date where a.account_no = b.account_no";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            // Sum of Nature of Credit Amount(DN Template)
            msSQL = "update lgl_trn_tmisdata a," +
                      " (select b.urn, sum(b.TotalDemandDu) as TotalDemandDu from lgl_tmp_tmisdata b" +
                      " group by b.urn)  c" +
                      " set natureof_credit_amount = b.TotalDemandDu where a.urn = c.urn";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            // For Risk Max and Min disbursement date insertion
            msSQL = "insert into rsk_trn_tcustomerdisbursement (customerdisb_gid,customer_urn,first_disb_date,last_disb_date,total_disbamount,customer_name, Vertical)" +
                    " (select concat('RMAN', misdata_gid) as customerdisb_gid,urn,min(disbursement_date)," +
                    " max(disbursement_date),sum(disbursement_amount),Customer_name, Vertical from lgl_tmp_tmisdata where urn not in (select customer_urn from rsk_trn_tcustomerdisbursement) " +
                    " group by urn)";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

           

            msSQL = " update rsk_trn_tcustomerdisbursement a," +
                        " (select urn, min(str_to_date(disbursement_date,'%d-%m-%Y')) as disb_date from lgl_tmp_tmisdata b" +
                        "  group by urn)  c" +
                        " set first_disb_date = disb_date where a.customer_urn = c.urn";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update rsk_trn_tcustomerdisbursement a," +
                        " (select urn, max(str_to_date(disbursement_date,'%d-%m-%Y')) as disb_date from lgl_tmp_tmisdata b" +
                        " group by urn)  c" +
                        " set last_disb_date = disb_date where a.customer_urn = c.urn";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            // Risk, Account Status
            msSQL = " update rsk_trn_tcustomerdisbursement b," +
                    " (SELECT urn,(select ac_status from lgl_tmp_tmisdata x where x.urn=a.urn order by str_to_date(disbursement_date,'%d-%m-%Y') desc limit 0, 1) as st" +
                    " FROM lgl_tmp_tmisdata a group by urn)  c" +
                    " set ac_status = st where b.customer_urn = c.urn";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //For Risk, Total Disbursement amount updation
            msSQL = " update rsk_trn_tcustomerdisbursement a," +
                   " (select urn, sum(disbursement_amount) as disb_amount from lgl_tmp_tmisdata b" +
                   " group by urn)  c" +
                    " set total_disbamount = disb_amount where a.customer_urn = c.urn";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "insert into lgl_trn_trecoveredcases(select * from lgl_trn_tmisdata where od_days=0)";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult!=0)
            {
              msSQL="select urn from lgl_trn_tmisdata where od_days=0";
                dt_datatable = objdbconn.GetDataTable(msSQL);
             
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        msSQL = "update lgl_trn_tdncases set status='Closed' where urn='" + dr_datarow["urn"] +"'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update lgl_trn_tcourierdetails set case_flag='Closed' where urn='" + dr_datarow["urn"] + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);                      

                        msSQL=" update lgl_trn_tsanctiondtl set status='Closed' where customer_urn='"+ dr_datarow["urn"] + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update lgl_tmp_tdnformat set status='Closed' where customer_urn='" + dr_datarow["urn"] + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update lgl_trn_tmisdata set acknowledgement_status='---' , dn_status='Repayment Done' where customer_urn='" + dr_datarow["urn"] + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();

                //msSQL = "delete from lgl_trn_tmisdata where od_days=0";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            msSQL = "update lgl_trn_tmisdocumentimport set process_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                " status='Processed'," +
                " updated_by ='" + employee_gid + "'," +
                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where misdocumentimport_gid='" + values.misdocumentimport_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
            values.message = "All Data Modified Processed Successfully";
                return true;
        }

        public bool DaGetImportedData(string employee_gid, string misdocumentimport_gid, MdlMisdataimportlist values)
        {            
            msSQL = "select file_name,date_format(imported_date,'%d-%m-%Y %H:%i %p') as imported_date from lgl_trn_tmisdocumentimport"+
                " where misdocumentimport_gid='" + misdocumentimport_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.file_name = objODBCDatareader["file_name"].ToString();
                values.imported_date = objODBCDatareader["imported_date"].ToString();
            }
            objODBCDatareader.Close();
            values.status = true;
            return true;
        }

        public bool DaGetRepaymentCases(MdlMisdataimportlist values)
        {
            msSQL = " select concat('MISD',misdata_gid) as misdata_gid,count(urn),max(od_days) as max_od_days,"+
                      " date_format(disbursement_date,'%d-%m-%Y') as disbursement_date,disbursement_amount," +
                      " date_format(maturity_date, '%d-%m-%Y') as maturity_date,od_days,ac_status,ac_closed_date,last_payment,payment,urn," +
                      " tenure,frequency,date_format(schedulde_payment, '%d-%m-%Y') as schedulde_payment,AccountName,ProductType,ProductCode," +
                      " date_format(nextdemandrundat, '%d-%m-%Y') as nextdemandrundat,date_format(lastdemandrundate, '%d-%m-%Y') as lastdemandrundate," +
                      " Customer_name,Guarantor_Name,Customer_Type,Vertical,RO_Name,Net_Payoff_Amt,TotalDemandDu from lgl_trn_tmisdata group by urn order by count(urn) desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMisdataimport = new List<MdlMisdataimport>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getMisdataimport.Add(new MdlMisdataimport
                    {
                        od_days = (dr_datarow["max_od_days"].ToString()),
                        AccountName = (dr_datarow["AccountName"].ToString()),
                        ProductType = (dr_datarow["ProductType"].ToString()),
                        ProductCode = (dr_datarow["ProductCode"].ToString()),
                        Customer_name = (dr_datarow["Customer_name"].ToString()),
                        Guarantor_Name = (dr_datarow["Guarantor_Name"].ToString()),
                        Customer_Type = (dr_datarow["Customer_Type"].ToString()),
                        Vertical = (dr_datarow["Vertical"].ToString()),
                        RO_Name = (dr_datarow["RO_Name"].ToString()),
                        urn = (dr_datarow["urn"].ToString()),
                        TotalDemandDu = (dr_datarow["TotalDemandDu"].ToString()),
                        Net_Payoff_Amt = (dr_datarow["Net_Payoff_Amt"].ToString())
                    });
                }
                values.mdlMisdataimport = getMisdataimport;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }

        public bool DaGetDN1List(MdlMisdataimportlist values)
        {           

            msSQL = " select account_no,count(urn),max(od_days) as max_od_days,urn,acknowledgement_status," +
                      " Customer_name,Guarantor_Name,Vertical,DN_status from lgl_trn_tmisdata  where od_days between 16 and 30" +
                      " and urn not in (select urn from lgl_trn_tmisdata where od_days between 31 and 120 group by urn) and Vertical <>'CBO'" +
                      " and natureof_credit_amount >=5000 group by urn order by od_days desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMisdataimport = new List<MdlMisdataimport>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getMisdataimport.Add(new MdlMisdataimport
                    {
                        od_days = (dr_datarow["max_od_days"].ToString()),
                       Customer_name = (dr_datarow["Customer_name"].ToString()),
                        Guarantor_Name = (dr_datarow["Guarantor_Name"].ToString()),
                        Vertical = (dr_datarow["Vertical"].ToString()),
                        urn = (dr_datarow["urn"].ToString()),
                        DNstatus = dr_datarow["DN_status"].ToString(),
                        account_no = dr_datarow["account_no"].ToString(),
                        acknowledgement_status = dr_datarow["acknowledgement_status"].ToString(),
                    });
                }
                values.mdlMisdataimport = getMisdataimport;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
       }

        public bool DaGetDN2List(MdlMisdataimportlist values)
        {          
            msSQL = " select account_no,count(urn),max(od_days) as max_od_days,urn,acknowledgement_status," +
                      " Customer_name,Guarantor_Name,Vertical,DN_status from lgl_trn_tmisdata  where od_days between 31 and 45" +
                      " and urn not in (select urn from lgl_trn_tmisdata where od_days between 46 and 120 group by urn) and Vertical <>'CBO' " +
                      " and natureof_credit_amount >=5000 group by urn order by od_days desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMisdataimport = new List<MdlMisdataimport>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getMisdataimport.Add(new MdlMisdataimport
                    {
                        od_days = (dr_datarow["max_od_days"].ToString()),
                        Customer_name = (dr_datarow["Customer_name"].ToString()),
                        Guarantor_Name = (dr_datarow["Guarantor_Name"].ToString()),
                        Vertical = (dr_datarow["Vertical"].ToString()),
                        urn = (dr_datarow["urn"].ToString()),
                        DNstatus = dr_datarow["DN_status"].ToString(),
                        account_no = dr_datarow["account_no"].ToString(),
                        acknowledgement_status = dr_datarow["acknowledgement_status"].ToString()
                    });
                }
                values.mdlMisdataimport = getMisdataimport;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }

        public bool DaGetDN3List(MdlMisdataimportlist values)
        {           
            msSQL = " select account_no,count(urn),max(od_days) as max_od_days,urn,acknowledgement_status," +
                       " Customer_name,Guarantor_Name,Vertical,DN_status from lgl_trn_tmisdata  where od_days between 46 and 60" +
                      " and urn not in (select urn from lgl_trn_tmisdata where od_days between 61 and 120 group by urn) and Vertical <>'CBO' " +
                      " and natureof_credit_amount >=5000 group by urn order by od_days desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMisdataimport = new List<MdlMisdataimport>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getMisdataimport.Add(new MdlMisdataimport
                    {
                        od_days = (dr_datarow["max_od_days"].ToString()),
                       Customer_name = (dr_datarow["Customer_name"].ToString()),
                        Guarantor_Name = (dr_datarow["Guarantor_Name"].ToString()),                    
                        Vertical = (dr_datarow["Vertical"].ToString()),                      
                        urn = (dr_datarow["urn"].ToString()),
                        DNstatus = dr_datarow["DN_status"].ToString(),
                        account_no = dr_datarow["account_no"].ToString(),
                        acknowledgement_status = dr_datarow["acknowledgement_status"].ToString()
                    });
                }
                values.mdlMisdataimport = getMisdataimport;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }

        public bool DaGetDNCount(DNcount values)
        {           
            //msSQL = "select count(distinct urn) from lgl_trn_tmisdata where od_days between 16 and 30 and urn not in( select urn from lgl_trn_tmisdata where " +
            //    " od_days between 31 and 120) and Vertical <>'CBO' and natureof_credit_amount >=5000 ";
            //values.dn1_total_count = objdbconn.GetExecuteScalar(msSQL);

            //msSQL = "select count(distinct urn) from lgl_trn_tmisdata where od_days=16  and  urn not in"+
            //    " ( select urn from lgl_trn_tmisdata where od_days between 17 and 120) and Vertical <>'CBO' and natureof_credit_amount >=5000";
            //values.dn1_today_count = objdbconn.GetExecuteScalar(msSQL);

            //msSQL = "select count(distinct urn) from lgl_trn_tmisdata where od_days between 31 and 45 and urn not in( select urn from lgl_trn_tmisdata where " +
            //    " od_days between 46 and 120) and Vertical <>'CBO' and natureof_credit_amount >=5000";
            //values.dn2_total_count = objdbconn.GetExecuteScalar(msSQL);

            //msSQL = "select count(distinct urn) from lgl_trn_tmisdata where od_days=31 and Vertical <>'CBO' and natureof_credit_amount >=5000";
            //values.dn2_today_count = objdbconn.GetExecuteScalar(msSQL);

            //msSQL = "select count(distinct urn) from lgl_trn_tmisdata where od_days between 46 and 60 and urn not in( select urn from lgl_trn_tmisdata where " +
            //    " od_days between 61 and 120) and Vertical <>'CBO' and natureof_credit_amount >=5000";
            //values.dn3_total_count = objdbconn.GetExecuteScalar(msSQL);

            //msSQL = "select count(distinct urn) from lgl_trn_tmisdata where od_days=46 and Vertical <>'CBO' and natureof_credit_amount >=5000";
            //values.dn3_today_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select date_format(process_date,'%d-%m-%Y') as process_date,concat(c.user_firstname,' ',c.user_lastname,'/',c.user_code) as employee_name," +
                    " date_format(imported_date,'%d-%m-%Y') as import_date from lgl_trn_tmisdocumentimport a" +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                    " left join adm_mst_tuser c on b.user_gid = c.user_gid order by misdocumentimport_gid desc  limit 1";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Read();
                values.employee_name = objODBCDatareader["employee_name"].ToString();
                values.process_date = objODBCDatareader["process_date"].ToString();
                values.import_date = objODBCDatareader["import_date"].ToString();
            }
            objODBCDatareader.Close();
            values.status = true;
            return true;
        }

        public bool DaGetCustomer2Loan(string urn, string employee_gid, MdlMisdataimportlist values)
        {           
            msSQL = " select a.account_no,a.Customer_name,a.urn,a.od_days,a.maturity_date,a.schedulde_payment,a.frequency,a.tenure,a.RO_name,a.vertical,a.Guarantor_Name," +
                " b.DN_status,format(b.TotalDemandDu,2) as TotalDemandDu,format(b.Net_Payoff_Amt,2) as Net_Payoff_Amt from lgl_tmp_tmisdata a" +
                " left join lgl_trn_tmisdata b on a.urn = b.urn and a.account_no=b.account_no where a.urn = b.urn and b.urn = '" + urn + "' and"+
                " a.ac_status = '0' group by a.account_no";
           dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcustomer2Loan = new List<MdlMisdataimport>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcustomer2Loan.Add(new MdlMisdataimport
                    {
                        od_days = (dr_datarow["od_days"].ToString()),
                        account_no = (dr_datarow["account_no"].ToString()),
                        maturity_date = (dr_datarow["maturity_date"].ToString()),
                        schedulde_payment = (dr_datarow["schedulde_payment"].ToString()),
                        Customer_name = (dr_datarow["Customer_name"].ToString()),
                        Guarantor_Name = (dr_datarow["Guarantor_Name"].ToString()),
                        Vertical = (dr_datarow["Vertical"].ToString()),
                        RO_Name = (dr_datarow["RO_Name"].ToString()),
                        urn = (dr_datarow["urn"].ToString()),
                        frequency = dr_datarow["frequency"].ToString(),
                        tenure = dr_datarow["tenure"].ToString(),
                        DNstatus = dr_datarow["DN_status"].ToString(),
                        TotalDemandDu = dr_datarow["TotalDemandDu"].ToString(),
                        Net_Payoff_Amt = dr_datarow["Net_Payoff_Amt"].ToString()
                    });
                }
                values.mdlMisdataimport = getcustomer2Loan;
            }
            dt_datatable.Dispose();
            msSQL = "select customer_name from lgl_trn_tmisdata where urn='" + urn + "' group by urn";
            values.customer_name = objdbconn.GetExecuteScalar(msSQL);
          
            msSQL = "select DN1status,DN3status,DN2status,cbo_status from lgl_trn_tdncases where urn='" + urn + "' and status <> 'Closed' ";
              
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.DN1status = objODBCDatareader["DN1status"].ToString();
                values.DN2status = objODBCDatareader["DN2status"].ToString();
                values.DN3status = objODBCDatareader["DN3status"].ToString();
                values.cbo_status = objODBCDatareader["cbo_status"].ToString();
                objODBCDatareader.Close();
            }
            else
            {
                values.DN1status = "Pending";
                values.DN2status = "Pending";
                values.DN3status = "Pending";
                values.cbo_status = "Pending";
                objODBCDatareader.Close();
            }

            
         
            values.status = true;
            return true;
        }

        public bool getdn1eligiblecase_da(string urn, string employee_gid, MdlMisdataimportlist values)
        {            
            msSQL = " select a.account_no,a.Customer_name,a.urn,a.od_days,a.maturity_date,a.schedulde_payment,a.frequency,a.tenure,a.RO_name,a.vertical,a.Guarantor_Name," +
                " b.DN_status from lgl_tmp_tmisdata a" +
                " left join lgl_trn_tmisdata b on a.urn = b.urn where a.urn = b.urn and b.urn = '" + urn + "' and od_days between 16 and 30 group by a.account_no";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcustomer2Loan = new List<MdlMisdataimport>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcustomer2Loan.Add(new MdlMisdataimport
                    {
                        od_days = (dr_datarow["od_days"].ToString()),
                        account_no = (dr_datarow["account_no"].ToString()),
                        maturity_date = (dr_datarow["maturity_date"].ToString()),
                        schedulde_payment = (dr_datarow["schedulde_payment"].ToString()),
                        Customer_name = (dr_datarow["Customer_name"].ToString()),
                        Guarantor_Name = (dr_datarow["Guarantor_Name"].ToString()),
                        Vertical = (dr_datarow["Vertical"].ToString()),
                        RO_Name = (dr_datarow["RO_Name"].ToString()),
                        urn = (dr_datarow["urn"].ToString()),
                        frequency = dr_datarow["frequency"].ToString(),
                        tenure = dr_datarow["tenure"].ToString(),
                        DNstatus = dr_datarow["DN_status"].ToString()
                    });
                }
                values.mdlMisdataimport = getcustomer2Loan;
            }
            dt_datatable.Dispose();
            msSQL = "select customer_name from lgl_trn_tmisdata where urn='" + urn + "' group by urn";
            values.customer_name = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select DN1status,DN3status,DN2status from lgl_trn_tdncases where urn='" + urn + "' and status <> 'Closed' ";
   
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.DN1status = objODBCDatareader["DN1status"].ToString();
                values.DN2status = objODBCDatareader["DN2status"].ToString();
                values.DN3status = objODBCDatareader["DN3status"].ToString();
                objODBCDatareader.Close();
            }
            else
            {
                values.DN1status = "Pending";
                values.DN2status = "Pending";
                values.DN3status = "Pending";
                objODBCDatareader.Close();
            }
            
            values.status = true;
            return true;
        }

        public bool DaPostDN1Status(MdlMisdataimportlist values, string employee_gid)
        {
            msSQL = "select max(od_days) ,account_no from lgl_trn_tmisdata where urn='" + values.urn + "' group by urn";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsaccount_no = objODBCDatareader["account_no"].ToString();
            }
            objODBCDatareader.Close();
            msGetGid = objcmnfunctions.GetMasterGID("DNCS");
            msSQL = " insert into lgl_trn_tdncases(" +
                 " dncase_gid ," +
                 " urn," +
                 " tempdn1format_gid,"+
                 " account_no ," +
                 " dn1status," +
                 " dn1status_created_by," +
                 " dn1status_created_date,"+
                 " dn1couriersent_date )" +
                 " values (" +
                 "'" + msGetGid + "'," +
                 "'" + values.urn + "'," +
                 "'" + values.tempdn1format_gid + "'," +
                 "'" + lsaccount_no + "'," +
                 "'DN1 Sent'," +
                 "'" + employee_gid + "'," +
                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',"+
                 "'"+ Convert.ToDateTime(values.courier_date).ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msGETGID = objcmnfunctions.GetMasterGID("DNCR");
            msSQL = "insert into lgl_trn_tcourierdetails (" +
                " courier_gid," +
                " dncase_gid," +
                " urn," +
                " courier_refno," +
                " courier_center," +
                " courier_date," +
                " couriered_by," +
                " remarks, " +
                " courier_status," +
                " created_by," +
                " created_date)" +
                " values (" +
                "'" + msGETGID + "'," +
                "'" + msGetGid + "'," +
                "'" + values.urn + "'," +
                "'" + values.courier_refno.Replace("'", " ") + "'," +
                "'" + values.courier_center.Replace("'", " ") + "'," +
                "'" + Convert.ToDateTime(values.courier_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                "'" + values.couriered_by.Replace("'", " ") + "',";
            if(values.courier_remarks==null|| values.courier_remarks=="")
            {
                msSQL = "'',";
            }
            else
            {
                msSQL += "'" + values.courier_remarks.Replace("'"," ") + "',";
            }
              
              msSQL+=  "'DN1 Sent'," +
                "'" + employee_gid + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update lgl_trn_tmisdata set DN_status='DN1 Sent'," +
                " acknowledgement_status='Pending'," +
                " updated_by='" + employee_gid + "'," +
                "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            values.status = true;
            return true;
        }

        public bool DaPostDNskip(MdlMisdataimportlist values, string employee_gid)
        {
            msSQL = "select max(od_days) ,account_no from lgl_trn_tmisdata where urn='" + values.urn + "' group by urn";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsaccount_no = objODBCDatareader["account_no"].ToString();
            }
            objODBCDatareader.Close();

            msGetGid = objcmnfunctions.GetMasterGID("DNCS");
            msSQL = " insert into lgl_trn_tdnskipcase(" +
                 " dnskipcase_gid ," +
                 " urn," +
                 " validity," +
                 " skip_reason," +
                 " created_by," +
                 " created_date )" +
                 " values (" +
                 "'" + msGetGid + "'," +
                   "'" + values.urn + "'," +
                   "'" + Convert.ToDateTime(values.valid_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
              if (values.skip_reason == null || values.skip_reason == "")
            {
                msSQL = "'',";
            }
            else
            {
                msSQL += "'" + values.skip_reason.Replace("'", " ") + "',";
            }

            msSQL +=  "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')"; 
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update lgl_trn_tmisdata set DN_status='DN Skip'," +
                 " acknowledgement_status='DN Skipped'," +
                " updated_by='" + employee_gid + "'," +
                "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            values.status = true;
            return true;
        }

        public bool DaPostDN2Status(MdlMisdataimportlist values, string employee_gid)
        {           
            msSQL = "select max(od_days) ,account_no from lgl_trn_tmisdata where urn='" + values.urn + "' group by urn";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsaccount_no = objODBCDatareader["account_no"].ToString();
            }
            objODBCDatareader.Close();
            msSQL = "select urn from lgl_trn_tdncases where urn='" + values.urn + "' and  status<>'Closed'";
            string dncase_urn = objdbconn.GetExecuteScalar(msSQL);
            if (dncase_urn == "")
            {
                msGetGid = objcmnfunctions.GetMasterGID("DNCS");
                msSQL = " insert into lgl_trn_tdncases(" +
                     " dncase_gid ," +
                     " urn," +
                     " tempdn1format_gid,"+
                     " account_no ," +
                     " dn2status," +
                     " dn2status_updated_by," +
                     " dn2status_updated_date,"+
                     " dn2couriersent_date )" +
                     " values (" +
                     "'" + msGetGid + "'," +
                     "'" + values.urn + "'," +
                     "'" + values.tempdn1format_gid + "'," +
                     "'" + lsaccount_no + "'," +
                     "'DN2 Sent'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',"+
                     "'" + Convert.ToDateTime(values.courier_date).ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update lgl_trn_tmisdata set DN_status='DN2 Sent'," +
                    " acknowledgement_status = 'Pending'," +
                    " updated_by='" + employee_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = " update lgl_trn_tdncases set " +
               " dn2status='DN2 Sent'," +
               " dn2status_updated_by='" + employee_gid + "'," +
               " dn2status_updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',"+
               " dn2couriersent_date='" + Convert.ToDateTime(values.courier_date).ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'"+
               " and status<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update lgl_trn_tmisdata set DN_status='DN2 Sent'," +
                      " acknowledgement_status='Pending'," +
                      " updated_by='" + employee_gid + "'," +
                      " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            msSQL = "select urn from lgl_trn_tcourierdetails where urn='" + values.urn + "' and case_flag <>'Closed'";
            string lsurn = objdbconn.GetExecuteScalar(msSQL);
            if (lsurn == "")
            {
                msGETGID = objcmnfunctions.GetMasterGID("DNCR");
                msSQL = "insert into lgl_trn_tcourierdetails (" +
                    " courier_gid," +
                    " dncase_gid," +
                    " urn," +
                    " dn2courier_refno," +
                    " dn2courier_center," +
                    " dn2courier_date," +
                    " dn2couriered_by," +
                    " dn2remarks, " +
                    " courier_status,"+
                    " created_by," +
                    " created_date)" +
                    " values (" +
                    "'" + msGETGID + "'," +
                    "'" + msGetGid + "'," +
                    "'" + values.urn + "'," +
                    "'" + values.courier_refno.Replace("'", " ") + "'," +
                    "'" + values.courier_center.Replace("'", " ") + "'," +
                    "'" + Convert.ToDateTime(values.courier_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    "'" + values.couriered_by.Replace("'", " ") + "',";
                if (values.courier_remarks == null || values.courier_remarks == "")
                {
                    msSQL = "'',";
                }
                else
                {
                    msSQL += "'" + values.courier_remarks.Replace("'", " ") + "',";
                }

                msSQL += "'DN2 Sent'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = "update  lgl_trn_tcourierdetails set dn2courier_refno='" + values.courier_refno.Replace("'", " ") + "'," +
                    " dn2courier_center='" + values.courier_center.Replace("'", " ") + "'," +
                    " dn2courier_date='" + Convert.ToDateTime(values.courier_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    " dn2couriered_by='" + values.couriered_by.Replace("'", " ") + "',";
                if (values.courier_remarks == null || values.courier_remarks == "")
                {
                   msSQL+= " dn2remarks='',";
                }
                else
                {
                    msSQL += " dn2remarks='" + values.courier_remarks.Replace("'", " ") + "',";
                }

                msSQL += " courier_status='DN2 Sent' where urn='" + values.urn + "' and case_flag <>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            values.status = true;
            return true;
        }

        public bool DaPostDN2skip(MdlMisdataimportlist values, string employee_gid)
        {
            msSQL = "select max(od_days) ,account_no from lgl_trn_tmisdata where urn='" + values.urn + "' group by urn";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsaccount_no = objODBCDatareader["account_no"].ToString();
            }
            objODBCDatareader.Close();
            msSQL = "select urn from lgl_trn_tdncases where urn='" + values.urn + "' and status<>'Closed'";
            string dncase_urn = objdbconn.GetExecuteScalar(msSQL);
            if (dncase_urn == "")
            {
                msGetGid = objcmnfunctions.GetMasterGID("DNCS");
                msSQL = " insert into lgl_trn_tdncases(" +
                     " dncase_gid ," +
                     " urn," +
                     " tempdn1format_gid,"+
                     " account_no ," +
                     " dn2status," +
                     " dn2status_updated_by," +
                     " dn2status_updated_date," +
                     " dn2couriersent_date)"+
                     " values (" +
                     "'" + msGetGid + "'," +
                     "'" + values.urn + "'," +
                      "'" + values.tempdn1format_gid + "'," +
                     "'" + lsaccount_no + "'," +
                     "'DN2 Skip'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',"+
                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')"; ;
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update lgl_trn_tmisdata set DN_status='DN2 Skip'," +
                     " acknowledgement_status='DN2 Skipped'," +
                  " updated_by='" + employee_gid + "'," +
                  "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = " update lgl_trn_tdncases set " +
                      " dn2status='DN2 Skip'," +
                      " dn2status_updated_by='" + employee_gid + "'," +
                      " dn2status_updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                      " dn2couriersent_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'" +
                      " and status<>'Closed'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update lgl_trn_tmisdata set DN_status='DN2 Skip'," +
                     " acknowledgement_status='DN2 Skipped'," +
                " updated_by='" + employee_gid + "'," +
                "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            values.status = true;
            return true;
        }

        public bool DaPostDN3Status(MdlMisdataimportlist values, string employee_gid)
        {         
            msSQL = "select max(od_days) ,account_no from lgl_trn_tmisdata where urn='" + values.urn + "' group by urn";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsaccount_no = objODBCDatareader["account_no"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = "select urn from lgl_trn_tdncases where urn='" + values.urn + "' and status<>'Closed'";
            string dncase_urn = objdbconn.GetExecuteScalar(msSQL);
            if (dncase_urn == "")
            {
                msGetGid = objcmnfunctions.GetMasterGID("DNCS");
                msSQL = " insert into lgl_trn_tdncases(" +
                     " dncase_gid ," +
                     " urn," +
                     " tempdn1format_gid,"+
                     " account_no ," +
                     " dn3status," +
                     " dn3status_updated_by," +
                     " dn3status_updated_date,"+
                     " dn2couriersent_date )" +
                     " values (" +
                     "'" + msGetGid + "'," +
                     "'" + values.urn + "'," +
                     "'" + values.tempdn1format_gid + "'," +
                     "'" + lsaccount_no + "'," +
                     "'DN3 Sent'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',"+
                     "'" + Convert.ToDateTime(values.courier_date).ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update lgl_trn_tmisdata set DN_status='DN3 Sent'," +
                    " acknowledgement_status = 'Pending'," +
                  " updated_by='" + employee_gid + "'," +
                  "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = " update lgl_trn_tdncases set " +
               " dn3status='DN3 Sent'," +
               " dn3status_updated_by='" + employee_gid + "'," +
               " dn3status_updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
               " dn3couriersent_date='" + Convert.ToDateTime(values.courier_date).ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'" +
                " and status<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update lgl_trn_tmisdata set DN_status='DN3 Sent'," +
                      " acknowledgement_status='Pending'," +
                    " updated_by='" + employee_gid + "'," +
                    "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            msSQL = "select urn from lgl_trn_tcourierdetails where urn='" + values.urn + "' and case_flag <>'Closed'";
            string lsurn = objdbconn.GetExecuteScalar(msSQL);
            if (lsurn == "")
            {
                msGETGID = objcmnfunctions.GetMasterGID("DNCR");
                msSQL = "insert into lgl_trn_tcourierdetails (" +
                    " courier_gid," +
                    " dncase_gid," +
                    " urn," +
                    " dn3courier_refno," +
                    " dn3courier_center," +
                    " dn3courier_date," +
                    " dn3couriered_by," +
                    " dn3remarks, " +
                    " courier_status," +
                    " created_by," +
                    " created_date)" +
                    " values (" +
                    "'" + msGETGID + "'," +
                    "'" + msGetGid + "'," +
                    "'" + values.urn + "'," +
                    "'" + values.courier_refno.Replace("'", " ") + "'," +
                    "'" + values.courier_center.Replace("'", " ") + "'," +
                    "'" + Convert.ToDateTime(values.courier_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    "'" + values.couriered_by.Replace("'", " ") + "',";
                if (values.courier_remarks == null || values.courier_remarks == "")
                {
                    msSQL = "'',";
                }
                else
                {
                    msSQL += "'" + values.courier_remarks.Replace("'", " ") + "',";
                }

                msSQL +="'DN3 Sent'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = "update  lgl_trn_tcourierdetails set dn3courier_refno='" + values.courier_refno.Replace("'", " ") + "'," +
                    " dn3courier_center='" + values.courier_center.Replace("'", " ") + "'," +
                    " dn3courier_date='" + Convert.ToDateTime(values.courier_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    " dn3couriered_by='" + values.couriered_by.Replace("'", " ") + "',";
                if (values.courier_remarks == null || values.courier_remarks == "")
                {
                    msSQL += " dn3remarks='',";
                }
                else
                {
                    msSQL += " dn3remarks='" + values.courier_remarks.Replace("'", " ") + "',";
                }
               msSQL+=" courier_status='DN3 Sent' where urn='" + values.urn + "' and case_flag <>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            values.status = true;
            return true;
        }

        public bool DaPostDN3skip(MdlMisdataimportlist values, string employee_gid)
        {
             msSQL = "select max(od_days) ,account_no from lgl_trn_tmisdata where urn='" + values.urn + "' group by urn";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsaccount_no = objODBCDatareader["account_no"].ToString();
            }
            objODBCDatareader.Close();
            msSQL = "select urn from lgl_trn_tdncases where urn='" + values.urn + "' and status<>'Closed'";
            string dncase_urn = objdbconn.GetExecuteScalar(msSQL);
            if (dncase_urn == "")
            {
                msGetGid = objcmnfunctions.GetMasterGID("DNCS");
                msSQL = " insert into lgl_trn_tdncases(" +
                     " dncase_gid ," +
                     " urn," +
                     " account_no ," +
                     " dn3status," +
                     " dn3status_updated_by," +
                     " dn3status_updated_date," +
                     " dn3couriersent_date)" +
                     " values (" +
                     "'" + msGetGid + "'," +
                     "'" + values.urn + "'," +
                     "'" + lsaccount_no + "'," +
                     "'DN3 Skip'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',"+
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update lgl_trn_tmisdata set DN_status='DN3 Skip'," +
                     " acknowledgement_status='DN3 Skipped'," +
                  " updated_by='" + employee_gid + "'," +
                  "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = " update lgl_trn_tdncases set " +
                     " dn3status='DN3 Skip'," +
                     " dn3status_updated_by='" + employee_gid + "'," +
                     " dn3status_updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',"+
                     " dn3couriersent_date = '"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "' where urn='" + values.urn + "' and status<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update lgl_trn_tmisdata set DN_status='DN3 Skip'," +
                     " acknowledgement_status='DN3 Skipped'," +
                    " updated_by='" + employee_gid + "'," +
                    "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            values.status = true;
            return true;
        }

        public bool DaGetCustomerDNGID(string urn, string employee_gid, MdlMisdataimportlist values)
        {           
            msSQL = "select dncase_gid " +
                " from lgl_trn_tdncases where urn='" + urn + "' and status<>'Closed'";
            values.dncase_gid = objdbconn.GetExecuteScalar(msSQL);
            values.status = true;
            return true;
        }

        public bool DaGetDN1Status(string urn, string employee_gid, MdlDN_History values)
        {           
            msSQL = "select DN1status,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as dn1status_created_by, d.DN1template_content,e.dn1annexuredocument_name,e.dn1annexuredocument_path," +
                " date_format(dn1status_created_date,'%d-%m-%Y') as dn1status_created_date,date_format(dn1couriersent_date,'%d-%m-%Y') as dn1couriersent_date,d.tempdn1format_gid " +
                " from lgl_trn_tdncases a,hrm_mst_temployee b, adm_mst_tuser c, lgl_tmp_tdnformat d, lgl_trn_tsanctiondtl e where urn='" + urn + "' and a.status<>'Closed' " +
                " and a.dn1status_created_by=b.employee_gid and b.user_gid=c.user_gid and d.customer_urn = a.urn and e.customer_urn = a.urn group by e.customer_urn";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if(objODBCDatareader.HasRows ==true)
            {

                values.DN1status = objODBCDatareader["DN1status"].ToString();
                values.DN1template_content = objODBCDatareader["DN1template_content"].ToString();
                values.dn1status_created_by = objODBCDatareader["dn1status_created_by"].ToString();
                values.dn1status_created_date = objODBCDatareader["dn1status_created_date"].ToString();
                values.dn1couriersent_date = objODBCDatareader["dn1couriersent_date"].ToString();
                values.dn1annexuredocument_name = objODBCDatareader["dn1annexuredocument_name"].ToString();
                //values.dn1annexuredocument_path = HttpContext.Current.Server.MapPath(objODBCDatareader["dn1annexuredocument_path"].ToString());
                values.dn1annexuredocument_path = objcmnstorage.EncryptData(objODBCDatareader["dn1annexuredocument_path"].ToString());
                values.tempdn1format_gid = objODBCDatareader["tempdn1format_gid"].ToString();
                
             }
            objODBCDatareader.Close();


            msSQL = "select DN2status,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as dn2status_created_by,d.DN2template_content,e.dn2annexuredocument_name,e.dn2annexuredocument_path," +
               " date_format(dn2status_updated_date,'%d-%m-%Y') as dn2status_created_date,date_format(dn2couriersent_date,'%d-%m-%Y') as dn2couriersent_date,d.tempdn1format_gid " +
               " from lgl_trn_tdncases a,hrm_mst_temployee b, adm_mst_tuser c, lgl_tmp_tdnformat d,lgl_trn_tsanctiondtl e where urn='" + urn + "' and a.status<>'Closed'" +
               " and a.dn2status_updated_by=b.employee_gid and b.user_gid=c.user_gid and d.customer_urn = a.urn and e.customer_urn = a.urn  group by e.customer_urn";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.DN2status = objODBCDatareader["DN2status"].ToString();
                values.DN2template_content = objODBCDatareader["DN2template_content"].ToString();
                values.dn2status_created_by = objODBCDatareader["dn2status_created_by"].ToString();
                values.dn2status_created_date = objODBCDatareader["dn2status_created_date"].ToString();
                values.dn2couriersent_date = objODBCDatareader["dn2couriersent_date"].ToString();
                values.dn2annexuredocument_name = objODBCDatareader["dn2annexuredocument_name"].ToString();
                //values.dn2annexuredocument_path = HttpContext.Current.Server.MapPath(objODBCDatareader["dn2annexuredocument_path"].ToString());
                values.dn2annexuredocument_path = objcmnstorage.EncryptData(objODBCDatareader["dn2annexuredocument_path"].ToString());
                values.tempdn1format_gid = objODBCDatareader["tempdn1format_gid"].ToString();
                
            }
            objODBCDatareader.Close();


            msSQL = "select DN3status,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as dn3status_created_by,d.DN3template_content,e.dn3annexuredocument_name,e.dn3annexuredocument_path," +
              " date_format(dn3status_updated_date,'%d-%m-%Y') as dn3status_created_date,date_format(dn3couriersent_date,'%d-%m-%Y') as dn3couriersent_date,d.tempdn1format_gid " +
              " from lgl_trn_tdncases a,hrm_mst_temployee b, adm_mst_tuser c, lgl_tmp_tdnformat d,lgl_trn_tsanctiondtl e where urn='" + urn + "' and a.status<>'Closed'" +
              " and a.dn3status_updated_by=b.employee_gid and b.user_gid=c.user_gid and d.customer_urn = a.urn and e.customer_urn = a.urn  group by e.customer_urn";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.DN3status = objODBCDatareader["DN3status"].ToString();
                values.DN3template_content = objODBCDatareader["DN3template_content"].ToString();
                values.dn3status_created_by = objODBCDatareader["dn3status_created_by"].ToString();
                values.dn3status_created_date = objODBCDatareader["dn3status_created_date"].ToString();
                values.dn3couriersent_date = objODBCDatareader["dn3couriersent_date"].ToString();
                values.dn3annexuredocument_name = objODBCDatareader["dn3annexuredocument_name"].ToString();
                //values.dn3annexuredocument_path = HttpContext.Current.Server.MapPath(objODBCDatareader["dn3annexuredocument_path"].ToString());
                values.dn3annexuredocument_path = objcmnstorage.EncryptData(objODBCDatareader["dn3annexuredocument_path"].ToString());
                values.tempdn1format_gid = objODBCDatareader["tempdn1format_gid"].ToString();
                
            }
            objODBCDatareader.Close();

            msSQL = "select cbo_status,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as cbostatus_updatedby," +
            " date_format(cbostatus_updateddate,'%d-%m-%Y') as cbostatus_updateddate,date_format(cbo_courier_sentdate,'%d-%m-%Y') as cbo_courier_sentdate " +
            " from lgl_trn_tdncases a,hrm_mst_temployee b, adm_mst_tuser c where urn='" + urn + "' and status<>'Closed'" +
            " and a.cbostatus_updatedby=b.employee_gid and b.user_gid=c.user_gid";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.cbo_status = objODBCDatareader["cbo_status"].ToString();
                values.cbostatus_updatedby = objODBCDatareader["cbostatus_updatedby"].ToString();
                values.cbostatus_updateddate = objODBCDatareader["cbostatus_updateddate"].ToString();
                values.cbo_courier_sentdate = objODBCDatareader["cbo_courier_sentdate"].ToString();
              
            }
            objODBCDatareader.Close();
            values.status = true;
            return true;
        }

        public bool DaGetCourierInfo(string urn, string employee_gid, courierinfo values)
        {
            msSQL = "select couriered_by,courier_center,date_format(courier_date,'%d-%m-%Y') as courier_date,courier_refno,remarks,dn2courier_refno,status," +
                "dn2courier_center,date_format(dn2courier_date,'%d-%m-%Y') as dn2courier_date,dn2couriered_by,dn2remarks,dn3courier_refno," +
                "dn3courier_center,date_format(dn3courier_date,'%d-%m-%Y') as dn3courier_date,dn3couriered_by,dn3remarks,dn2courier_status,dn3courier_status, " +
                "CBOcourier_refno,CBOcourier_center,date_format(CBOcourier_date,'%d-%m-%Y') as CBOcourier_date,CBOcouriered_by,CBOremarks,CBOcourier_status," +
                "courier_status from lgl_trn_tcourierdetails where urn='" + urn + "' and case_flag <>'Closed'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.couriered_by = objODBCDatareader["couriered_by"].ToString();
                values.courier_center = objODBCDatareader["courier_center"].ToString();
                values.courier_date = objODBCDatareader["courier_date"].ToString();
                values.courier_refno = objODBCDatareader["courier_refno"].ToString();
                values.courier_remarks = objODBCDatareader["remarks"].ToString();
                values.dn1courier_status = objODBCDatareader["status"].ToString();
                values.dn2couriered_by = objODBCDatareader["dn2couriered_by"].ToString();
                values.dn2courier_center = objODBCDatareader["dn2courier_center"].ToString();
                values.dn2courier_date = objODBCDatareader["dn2courier_date"].ToString();
                values.dn2remarks = objODBCDatareader["dn2remarks"].ToString();
                values.dn2courier_refno = objODBCDatareader["dn2courier_refno"].ToString();
                values.dn2courier_status = objODBCDatareader["dn2courier_status"].ToString();
                values.dn3courier_refno = objODBCDatareader["dn3courier_refno"].ToString();
                values.dn3couriered_by = objODBCDatareader["dn3couriered_by"].ToString();
                values.dn3courier_center = objODBCDatareader["dn3courier_center"].ToString();
                values.dn3courier_date = objODBCDatareader["dn3courier_date"].ToString();
                values.dn3remarks = objODBCDatareader["dn3remarks"].ToString();
                values.dn3courier_status = objODBCDatareader["dn3courier_status"].ToString();
                values.CBOcourier_refno = objODBCDatareader["CBOcourier_refno"].ToString();
                values.CBOcourier_status = objODBCDatareader["CBOcourier_status"].ToString();
                values.CBOcouriered_by = objODBCDatareader["CBOcouriered_by"].ToString();
                values.CBOcourier_center = objODBCDatareader["CBOcourier_center"].ToString();
                values.CBOcourier_date = objODBCDatareader["CBOcourier_date"].ToString();
                values.CBOremarks = objODBCDatareader["CBOremarks"].ToString();
                values.courier_status = objODBCDatareader["courier_status"].ToString();
            }
            objODBCDatareader.Close();

            values.status = true;
            return true;
        }

        public bool DaPostDn1ackStatus(courierinfo values, string employee_gid)
        {          
            if ((values.returened_date) == DateTime.MinValue)
            {
                msSQL = "update lgl_trn_tcourierdetails set status='DN Delivered'," +
                   " delivered_date ='" + Convert.ToDateTime(values.delivered_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                  " updated_by='" + employee_gid + "'," +
                  " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "' and case_flag<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update lgl_trn_tmisdata set acknowledgement_status='DN Delivered'," +
                          " updated_by='" + employee_gid + "'," +
                             "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);                
            }
            else
            {
                msSQL = "update lgl_trn_tcourierdetails set status='DN Returned'," +
                             " returened_date ='" + Convert.ToDateTime(values.returened_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                             " updated_by='" + employee_gid + "'," +
                             " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "' and case_flag<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update lgl_trn_tmisdata set  acknowledgement_status='DN Returned'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);                
            }

            values.status = true;
            return true;
        }

        public bool DaPostDn2ackStatus(courierinfo values, string employee_gid)
        {         
            if ((values.returened_date) == DateTime.MinValue)
            {
                msSQL = "update lgl_trn_tcourierdetails set dn2courier_status='DN2 Delivered'," +
                   " dn2delivered_date ='" + Convert.ToDateTime(values.delivered_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                  " updated_by='" + employee_gid + "'," +
                  " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "' and case_flag<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update lgl_trn_tmisdata set acknowledgement_status='DN2 Delivered'," +
                          " updated_by='" + employee_gid + "'," +
                             "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);                
            }
            else
            {
                msSQL = "update lgl_trn_tcourierdetails set dn2courier_status='DN2 Returned'," +
                              " dn2returened_date ='" + Convert.ToDateTime(values.returened_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                             " updated_by='" + employee_gid + "'," +
                             " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "' and case_flag<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update lgl_trn_tmisdata set  acknowledgement_status='DN2 Returned'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);                
            }
            values.status = true;
            return true;
        }

        public bool DaPostDn3ackStatus(courierinfo values, string employee_gid)
        {           
            if ((values.returened_date) == DateTime.MinValue)
            {
                msSQL = "update lgl_trn_tcourierdetails set dn3courier_status='DN2 Delivered'," +
                   " dn3delivered_date ='" + Convert.ToDateTime(values.delivered_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                  " updated_by='" + employee_gid + "'," +
                  " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "' and case_flag<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update lgl_trn_tmisdata set acknowledgement_status='DN3 Delivered'," +
                          " updated_by='" + employee_gid + "'," +
                          " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);               
            }
            else
            {
                msSQL = "update lgl_trn_tcourierdetails set dn3courier_status='DN3 Returned'," +
                              " dn3returened_date ='" + Convert.ToDateTime(values.returened_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                             " updated_by='" + employee_gid + "'," +
                             " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "' and case_flag<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update lgl_trn_tmisdata set  acknowledgement_status='DN3 Returned'," +
                        " updated_by='" + employee_gid + "'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);                
            }
            values.status = true;
            return true;
        }

        public bool DaGetDN1CourierInfo(string urn, string employee_gid, courierinfo values)
        {            
            msSQL = "select couriered_by,courier_center,date_format(courier_date,'%d-%m-%Y %h:%i %p') as courier_date,courier_refno" +
                " from lgl_trn_tcourierdetails where urn='" + urn + "' and case_flag<>'Closed'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.couriered_by = objODBCDatareader["couriered_by"].ToString();
                values.courier_center = objODBCDatareader["courier_center"].ToString();
                values.courier_date = objODBCDatareader["courier_date"].ToString();
                values.courier_refno = objODBCDatareader["courier_refno"].ToString();
            }
            objODBCDatareader.Close();
            values.status = true;
            return true;
        }

        public bool DaGetTemplateDN1Content(template_list values, string urn)
        {
            msSQL = " select a.template_gid, a.template_name, a.template_content from adm_mst_ttemplate a " +
                    " left join adm_mst_ttemplatetype b on b.templatetype_gid = a.templatetype_gid " +
                    " where a.templatetype_gid='1'";           
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                lsdntemplate_content = objODBCDatareader["template_content"].ToString();
            }
            objODBCDatareader.Close();
              msSQL = " select customername,email,contactperson,address,address2,mobileno,email from ocs_mst_tcustomer " +
                        " where customer_urn='" + urn + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Read();
                    values.customer_mail = objODBCDatareader["email"].ToString();
                    values.customer_name = objODBCDatareader["customername"].ToString();
                    values.address1 = objODBCDatareader["address"].ToString();
                    values.address2 = objODBCDatareader["address2"].ToString();
                    values.mobile_no = objODBCDatareader["mobileno"].ToString();
                    values.email_address= objODBCDatareader["email"].ToString();
                }
                objODBCDatareader.Close();
                msSQL = "select max(cast(od_days as unsigned))  as od_days  from lgl_trn_tmisdata where urn='" + urn + "' group by urn";
                lsod_days = objdbconn.GetExecuteScalar(msSQL);

                lscontent = lsdntemplate_content;
                lscontent = lscontent.Replace("now_date", DateTime.Now.ToString("dd-MM-yyyy"));
                lscontent = lscontent.Replace("customer_name", values.customer_name);
            lscontent = lscontent.Replace("Customer", values.customer_name + ",");
            lscontent = lscontent.Replace("addressline1", values.address1);
                lscontent = lscontent.Replace("addressline2", values.address2 );
                lscontent = lscontent.Replace("od_days", lsod_days);
                lscontent = lscontent.Replace("mobile_no", values.mobile_no);
                lscontent = lscontent.Replace("email_address", values.email_address);
           
          
            msSQL = "select dn1sanctionref_no,format(dn1sanction_amount,2) as dn1sanction_amount,date_format(dn1sanction_date,'%d-%m-%Y') as dn1sanction_date,"+
                " dn1ref_no from lgl_trn_tsanctiondtl where customer_urn='" + urn + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Read();
                values.dn1sanctionref_no = objODBCDatareader["dn1sanctionref_no"].ToString();
                values.dn1sanction_amount = objODBCDatareader["dn1sanction_amount"].ToString();
                values.dn1sanction_date = objODBCDatareader["dn1sanction_date"].ToString();
                values.dn1ref_no = objODBCDatareader["dn1ref_no"].ToString();

                string fare = objODBCDatareader["dn1sanction_amount"].ToString();
                decimal parsed = decimal.Parse(fare, System.Globalization.CultureInfo.InvariantCulture);
                System.Globalization.CultureInfo hindi = new System.Globalization.CultureInfo("hi-IN");
                string text = string.Format(hindi, "{0:c}", parsed);

                msSQL = "select substring('"+ text +"',2,20)";
                string dn1sanction_amount = objdbconn.GetExecuteScalar(msSQL);
                //   var amount = new intl.NumberFormat("en-IN").format(objODBCDatareader["dn1sanction_amount"].ToString());

                lscontent = lscontent.Replace("sanctionref_no", values.dn1sanctionref_no);
                lscontent = lscontent.Replace("sanction_date", values.dn1sanction_date);
                lscontent = lscontent.Replace("sanction_amount", dn1sanction_amount);
                lscontent = lscontent.Replace("dn1ref_no", values.dn1ref_no);
            }
            objODBCDatareader.Close();

            msSQL = "select format(natureof_credit_amount,2) as amount  from lgl_trn_tmisdata where urn='"+ urn +"' group by urn";
            lsnatureof_credit_amount = objdbconn.GetExecuteScalar(msSQL);

            string amount = lsnatureof_credit_amount;
            decimal parsed1 = decimal.Parse(amount, System.Globalization.CultureInfo.InvariantCulture);
            System.Globalization.CultureInfo indian1 = new System.Globalization.CultureInfo("hi-IN");
            string amount_format = string.Format(indian1, "{0:c}", parsed1);

            msSQL = "select substring('" + amount_format + "',2,20)";
            string totaldemand_amount = objdbconn.GetExecuteScalar(msSQL);

            lscontent = lscontent.Replace("totaldemand_amount", totaldemand_amount);

            values.template_content = lscontent;
            values.status = true;
            return true;
        }

        public bool DaGetTemplateDN2Content(template_list values,string urn)
        {
            msSQL = " select a.template_gid, a.template_name, a.template_content from adm_mst_ttemplate a " +
                    " left join adm_mst_ttemplatetype b on b.templatetype_gid = a.templatetype_gid " +
                    " where a.templatetype_gid='10'";
            
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                lsdntemplate_content = objODBCDatareader["template_content"].ToString();
            }
            objODBCDatareader.Close();
           
                msSQL = " select customername,email,contactperson,address,address2,mobileno,email from ocs_mst_tcustomer " +
                        " where customer_urn='" + urn + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Read();
                    values.customer_mail = objODBCDatareader["email"].ToString();
                    values.customer_name = objODBCDatareader["customername"].ToString();
                    values.address1 = objODBCDatareader["address"].ToString();
                    values.address2 = objODBCDatareader["address2"].ToString();
                    values.mobile_no = objODBCDatareader["mobileno"].ToString();
                    values.email_address = objODBCDatareader["email"].ToString();
                }
                objODBCDatareader.Close();
                msSQL = "select max(cast(od_days as unsigned))  as od_days  from lgl_trn_tmisdata where urn='" + urn + "' group by urn";
                lsod_days = objdbconn.GetExecuteScalar(msSQL);

                lscontent = lsdntemplate_content;
                lscontent = lscontent.Replace("now_date", DateTime.Now.ToString("dd-MM-yyyy"));
                lscontent = lscontent.Replace("customer_name", values.customer_name);
            lscontent = lscontent.Replace("Customer", values.customer_name + ",");
            lscontent = lscontent.Replace("addressline1", values.address1);
            lscontent = lscontent.Replace("addressline2", values.address2);
            lscontent = lscontent.Replace("od_days", lsod_days);
                lscontent = lscontent.Replace("mobile_no", values.mobile_no);
                lscontent = lscontent.Replace("email_address", values.email_address);
           
          

            msSQL = "select dn2sanctionref_no,format(dn2sanction_amount,2) as dn2sanction_amount,date_format(dn2sanction_date,'%d-%m-%Y') as dn2sanction_date," +
                "  dn1ref_no,date_format(dn1generate_date,'%d-%m-%Y') as dn1sanction_date," +
                " dn2ref_no from lgl_trn_tsanctiondtl where customer_urn='" + urn + "' and status<>'Closed'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Read();
                values.dn2sanctionref_no = objODBCDatareader["dn2sanctionref_no"].ToString();

                string fare = objODBCDatareader["dn2sanction_amount"].ToString();
                decimal parsed = decimal.Parse(fare, System.Globalization.CultureInfo.InvariantCulture);
                System.Globalization.CultureInfo hindi = new System.Globalization.CultureInfo("hi-IN");
                string text = string.Format(hindi, "{0:c}", parsed);

                msSQL = "select substring('" + text + "',2,20)";
                string dn2sanction_amount = objdbconn.GetExecuteScalar(msSQL);

                values.dn2sanction_date = objODBCDatareader["dn2sanction_date"].ToString();
                values.dn2ref_no = objODBCDatareader["dn2ref_no"].ToString();

                if (objODBCDatareader["dn1ref_no"].ToString() != "")
                {
                    values.dn1sanctionref_no = objODBCDatareader["dn1ref_no"].ToString();
                    values.dn1sanction_date = objODBCDatareader["dn1sanction_date"].ToString();
                }
                else
                {
                    values.dn1sanctionref_no = "_______";
                    values.dn1sanction_date = "_______";
                }
                lscontent = lscontent.Replace("dn2sanctionref_no", values.dn2sanctionref_no);
                lscontent = lscontent.Replace("dn2sanction_date", values.dn2sanction_date);
                lscontent = lscontent.Replace("dn2sanction_amount", dn2sanction_amount);
                lscontent = lscontent.Replace("dn2ref_no", values.dn2ref_no);
                lscontent = lscontent.Replace("dn1ref_no", values.dn1sanctionref_no);
                lscontent = lscontent.Replace("dn1sanction_date", values.dn1sanction_date);
            }
            objODBCDatareader.Close();

            msSQL = "select format(natureof_credit_amount,2) as amount  from lgl_trn_tmisdata where urn='" + urn + "' group by urn";
            lsnatureof_credit_amount = objdbconn.GetExecuteScalar(msSQL);

            string amount = lsnatureof_credit_amount;
            decimal parsed1 = decimal.Parse(amount, System.Globalization.CultureInfo.InvariantCulture);
            System.Globalization.CultureInfo indian1 = new System.Globalization.CultureInfo("hi-IN");
            string amount_format = string.Format(indian1, "{0:c}", parsed1);

            msSQL = "select substring('" + amount_format + "',2,20)";
            string totaldemand_amount = objdbconn.GetExecuteScalar(msSQL);

            lscontent = lscontent.Replace("totaldemand_amount", totaldemand_amount);
            values.template_content = lscontent;

            values.status = true;
            return true;
        }

        public bool DaGetTemplateDN3Content(template_list values,string urn)
        {
            msSQL = " select a.template_gid, a.template_name, a.template_content from adm_mst_ttemplate a " +
                    " left join adm_mst_ttemplatetype b on b.templatetype_gid = a.templatetype_gid " +
                    " where a.templatetype_gid='20'";          
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                lsdntemplate_content = objODBCDatareader["template_content"].ToString();
            }
            objODBCDatareader.Close();
               msSQL = " select customername,email,contactperson,address,address2,mobileno,email from ocs_mst_tcustomer " +
                         " where customer_urn='" + urn + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Read();
                    values.customer_mail = objODBCDatareader["email"].ToString();
                    values.customer_name = objODBCDatareader["customername"].ToString();
                    values.address1 = objODBCDatareader["address"].ToString();
                    values.address2 = objODBCDatareader["address2"].ToString();
                    values.mobile_no = objODBCDatareader["mobileno"].ToString();
                    values.email_address = objODBCDatareader["email"].ToString();
                }
                objODBCDatareader.Close();
                msSQL = "select max(cast(od_days as unsigned))  as od_days  from lgl_trn_tmisdata where urn='" + urn + "' group by urn";
                lsod_days = objdbconn.GetExecuteScalar(msSQL);

                lscontent = lsdntemplate_content;
                lscontent = lscontent.Replace("now_date", DateTime.Now.ToString("dd-MM-yyyy"));
                lscontent = lscontent.Replace("customer_name", values.customer_name);
            lscontent = lscontent.Replace("Customer", values.customer_name + ",");
            lscontent = lscontent.Replace("addressline1", values.address1);
            lscontent = lscontent.Replace("addressline2", values.address2);
            lscontent = lscontent.Replace("od_days", lsod_days);
                lscontent = lscontent.Replace("mobile_no", values.mobile_no);       
                lscontent = lscontent.Replace("email_address", values.email_address);

           
            
            msSQL = "select dn3sanctionref_no,format(dn3sanction_amount,2) as dn3sanction_amount,date_format(dn3sanction_date,'%d-%m-%Y') as dn3sanction_date," +
               " dn3ref_no, dn2ref_no,date_format(dn2generate_date,'%d-%m-%Y') as dn2sanction_date, "+
               " dn1ref_no,date_format(dn1generate_date,'%d-%m-%Y') as dn1sanction_date " +
               " from lgl_trn_tsanctiondtl where customer_urn='" + urn + "'and status<>'Closed'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Read();
                values.dn3sanctionref_no = objODBCDatareader["dn3sanctionref_no"].ToString();
             
                string fare = objODBCDatareader["dn3sanction_amount"].ToString();
                decimal parsed = decimal.Parse(fare, System.Globalization.CultureInfo.InvariantCulture);
                System.Globalization.CultureInfo hindi = new System.Globalization.CultureInfo("hi-IN");
                string text = string.Format(hindi, "{0:c}", parsed);

                msSQL = "select substring('" + text + "',2,20)";
                string dn3sanction_amount = objdbconn.GetExecuteScalar(msSQL);

                values.dn3sanction_date = objODBCDatareader["dn3sanction_date"].ToString();
                values.dn3ref_no = objODBCDatareader["dn3ref_no"].ToString();

                if(objODBCDatareader["dn2ref_no"].ToString()!="")
                {
                    values.dn2sanctionref_no = objODBCDatareader["dn2ref_no"].ToString();
                    values.dn2sanction_date = objODBCDatareader["dn2sanction_date"].ToString();
                }
                else
                {
                    values.dn2sanctionref_no = "_______";
                    values.dn2sanction_date = "________";
                }
                if (objODBCDatareader["dn1ref_no"].ToString() != "")
                {
                    values.dn1sanctionref_no = objODBCDatareader["dn1ref_no"].ToString();
                    values.dn1sanction_date = objODBCDatareader["dn1sanction_date"].ToString();
                }
                else
                {
                    values.dn1sanctionref_no = "_______";
                    values.dn1sanction_date = "________";
                }

                lscontent = lscontent.Replace("dn3sanctionref_no", values.dn3sanctionref_no);
                lscontent = lscontent.Replace("dn3sanction_date", values.dn3sanction_date);
                lscontent = lscontent.Replace("dn3sanction_amount", dn3sanction_amount);
                lscontent = lscontent.Replace("dn3ref_no", values.dn3ref_no);
                lscontent = lscontent.Replace("dn2ref_no", values.dn2sanctionref_no);
                lscontent = lscontent.Replace("dn2sanction_date", values.dn2sanction_date);
                lscontent = lscontent.Replace("dn1ref_no", values.dn1sanctionref_no);
                lscontent = lscontent.Replace("dn1sanction_date", values.dn1sanction_date);
            }
            objODBCDatareader.Close();

            msSQL = "select format(natureof_credit_amount,2) as amount  from lgl_trn_tmisdata where urn='" + urn + "' group by urn";
            lsnatureof_credit_amount = objdbconn.GetExecuteScalar(msSQL);

            string amount = lsnatureof_credit_amount;
            decimal parsed1 = decimal.Parse(amount, System.Globalization.CultureInfo.InvariantCulture);
            System.Globalization.CultureInfo indian1 = new System.Globalization.CultureInfo("hi-IN");
            string amount_format = string.Format(indian1, "{0:c}", parsed1);

            msSQL = "select substring('" + amount_format + "',2,20)";
            string totaldemand_amount = objdbconn.GetExecuteScalar(msSQL);

            lscontent = lscontent.Replace("totaldemand_amount", totaldemand_amount);

            values.template_content = lscontent;

            values.status = true;
            return true;
        }

        public bool DaGetTemplateDNCBOContent(template_list values, string urn)
        {
            msSQL = " select a.template_gid, a.template_name, a.template_content from adm_mst_ttemplate a " +
                    " left join adm_mst_ttemplatetype b on b.templatetype_gid = a.templatetype_gid " +
                    " where a.templatetype_gid='40'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                lsdntemplate_content = objODBCDatareader["template_content"].ToString();
            }
            objODBCDatareader.Close();
            msSQL = "select Vertical from lgl_trn_tmisdata where urn='" + urn + "' group by urn";
            lsvertical = objdbconn.GetExecuteScalar(msSQL);
            if (lsvertical != "CBO")
            {
                msSQL = " select customername,email,contactperson,address,address2,mobileno,email from ocs_mst_tcustomer " +
                         " where customer_urn='" + urn + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Read();
                    values.customer_mail = objODBCDatareader["email"].ToString();
                    values.customer_name = objODBCDatareader["customername"].ToString();
                    values.address1 = objODBCDatareader["address"].ToString();
                    values.address2 = objODBCDatareader["address2"].ToString();
                    values.mobile_no = objODBCDatareader["mobileno"].ToString();
                    values.email_address = objODBCDatareader["email"].ToString();
                }
                objODBCDatareader.Close();
                msSQL = "select  max(cast(od_days as unsigned))  as od_days from lgl_trn_tmisdata where urn='" + urn + "' group by urn";
                lsod_days = objdbconn.GetExecuteScalar(msSQL);

                lscontent = lsdntemplate_content;
                lscontent = lscontent.Replace("now_date", DateTime.Now.ToString("dd-MM-yyyy"));
                lscontent = lscontent.Replace("customer_name", values.customer_name);
                lscontent = lscontent.Replace("Customer", values.customer_name + ",");
                lscontent = lscontent.Replace("addressline1", values.address1);
                lscontent = lscontent.Replace("addressline2", values.address2);
                lscontent = lscontent.Replace("od_days", lsod_days);
                lscontent = lscontent.Replace("mobile_no", values.mobile_no);
                lscontent = lscontent.Replace("email_address", values.email_address);

            }
            else
            {
                msSQL = " select max(od_days) as od_days,address1,address2,state,city,pincode,Guarantor_name from lgl_trn_tmisdata " +
                    " where urn='" + urn + "'group by urn ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Read();
                    values.max_od_days = objODBCDatareader["od_days"].ToString();
                    values.state = objODBCDatareader["state"].ToString();
                    values.city = objODBCDatareader["city"].ToString();
                    values.pincode = objODBCDatareader["pincode"].ToString();
                    values.address1 = objODBCDatareader["address1"].ToString();
                    values.address2 = objODBCDatareader["address2"].ToString();
                    values.guarantor_name = objODBCDatareader["Guarantor_name"].ToString();
                }
                lscontent = lsdntemplate_content;
                lscontent = lscontent.Replace("now_date", DateTime.Now.ToString("dd-MM-yyyy"));
                lscontent = lscontent.Replace("customer_name", values.guarantor_name);
                lscontent = lscontent.Replace("Customer", values.customer_name + ",");
                lscontent = lscontent.Replace("addressline1", values.address1);
                lscontent = lscontent.Replace("addressline2", values.address2);
                lscontent = lscontent.Replace("od_days", values.max_od_days);
                objODBCDatareader.Close();
            }
            msSQL = "select dnCBOsanctionref_no,format(dnCBOsanction_amount,2) as dnCBOsanction_amount,date_format(dnCBOsanction_date,'%d-%m-%Y') as dnCBOsanction_date," +
               " dnCBOref_no from lgl_trn_tsanctiondtl where customer_urn='" + urn + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Read();
                values.dnCBOsanctionref_no = objODBCDatareader["dnCBOsanctionref_no"].ToString();          
                values.dnCBOsanction_date = objODBCDatareader["dnCBOsanction_date"].ToString();
                values.dnCBOref_no = objODBCDatareader["dnCBOref_no"].ToString();

                string fare = objODBCDatareader["dnCBOsanction_amount"].ToString();
                decimal parsed = decimal.Parse(fare, System.Globalization.CultureInfo.InvariantCulture);
                System.Globalization.CultureInfo indian = new System.Globalization.CultureInfo("hi-IN");
                string text = string.Format(indian, "{0:c}", parsed);

                msSQL = "select substring('" + text + "',2,20)";
                string dnCBOsanction_amount = objdbconn.GetExecuteScalar(msSQL);


                lscontent = lscontent.Replace("dnCBOsanctionref_no", values.dnCBOsanctionref_no);
                lscontent = lscontent.Replace("dnCBOsanction_date", values.dnCBOsanction_date);
                lscontent = lscontent.Replace("dnCBOsanction_amount", dnCBOsanction_amount);
                lscontent = lscontent.Replace("dnCBOref_no", values.dnCBOref_no);
               
            }
            objODBCDatareader.Close();

            msSQL = "select format(natureof_credit_amount,2) as amount  from lgl_trn_tmisdata where urn='" + urn + "' group by urn";
            lsnatureof_credit_amount = objdbconn.GetExecuteScalar(msSQL);

            string amount = lsnatureof_credit_amount;
            decimal parsed1 = decimal.Parse(amount, System.Globalization.CultureInfo.InvariantCulture);
            System.Globalization.CultureInfo indian1 = new System.Globalization.CultureInfo("hi-IN");
            string amount_format = string.Format(indian1, "{0:c}", parsed1);

            msSQL = "select substring('" + amount_format + "',2,20)";
            string totaldemand_amount = objdbconn.GetExecuteScalar(msSQL);

            lscontent = lscontent.Replace("totaldemand_amount", totaldemand_amount);
            values.template_content = lscontent;

            values.status = true;
            return true;
        }

        public bool DaPostDN1Generate(MdlMisdataimportlist values, string employee_gid)
        {
            msGETGID = objcmnfunctions.GetMasterGID("DN1F");
            msSQL = "insert into lgl_tmp_tdnformat(" +
                " tempdn1format_gid, " +
                " DN1template_content, " +
                " dn1generated_by,"+
                " dn1generated_date,"+
                " customer_urn," +
                " created_by," +
                " created_date)" +
                " values (" +
                "'" + msGETGID + "'," +
                "'" + values.template_content + "'," +
                "'" + employee_gid + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                "'" + values.urn + "'," +
                "'" + employee_gid + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL="update lgl_trn_tsanctiondtl set tempdn1format_gid='"+ msGETGID + "' where customer_urn='" + values.urn + "' and status<>'Closed'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update lgl_trn_tmisdata set DN_status='DN1 Generated'," +
                " acknowledgement_status='--'," +
               " updated_by='" + employee_gid + "'," +
               "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msGetGid = objcmnfunctions.GetMasterGID("LGST");
            msSQL = "insert into lgl_trn_tdnstatus(" +
                " dnstatus_gid, " +
                " dn1status, " +
                " customer_urn," +
                " created_by," +
                " created_date)" +
                " values (" +
                "'" + msGetGid + "'," +
                "'DN1 Generated'," +
                "'" + values.urn + "'," +
                "'" + employee_gid + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL=" update lgl_trn_tsanctiondtl set dn1generate_date='"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "'"+
                " where customer_urn='" + values.urn + "' and status<>'Closed'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
            return true;
        }

        public bool DaPostDN2Generate(MdlMisdataimportlist values, string employee_gid)
        {            
            msSQL = "select customer_urn from lgl_tmp_tdnformat where customer_urn='" + values.urn + "' and status<>'Closed'";
            lsurn = objdbconn.GetExecuteScalar(msSQL);
            if (lsurn=="")
            {
                msGETGID = objcmnfunctions.GetMasterGID("DN1F");
                msSQL = "insert into lgl_tmp_tdnformat(" +
                    " tempdn1format_gid, " +
                    " DN2template_content, " +
                    " dn2generated_by,"+
                    " dn2generated_date,"+
                    " customer_urn," +
                    " created_by," +
                    " created_date)" +
                    " values (" +
                    "'" + msGETGID + "'," +
                    "'" + values.template_content + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    "'" + values.urn + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "update lgl_trn_tsanctiondtl set tempdn1format_gid='" + msGETGID + "' where customer_urn='" + values.urn + "' and status<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "update lgl_trn_tmisdata set DN_status='DN2 Generated'," +
                    " acknowledgement_status='--'," +
                   " updated_by='" + employee_gid + "'," +
                   "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update lgl_trn_tsanctiondtl set dn2generate_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
               " where customer_urn='" + values.urn + "' and status<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = "update lgl_tmp_tdnformat set DN2template_content='" + values.template_content + "'," +
                    " dn2generated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    " dn2generated_by ='" + employee_gid + "',"+
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    " updated_by ='" + employee_gid + "' where customer_urn='" + values.urn + "' and status<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update lgl_trn_tmisdata set DN_status='DN2 Generated'," +
                    " acknowledgement_status='--'," +
                   " updated_by='" + employee_gid + "'," +
                   "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update lgl_trn_tsanctiondtl set dn2generate_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
             " where customer_urn='" + values.urn + "' and status<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            msSQL = "select customer_urn from lgl_trn_tdnstatus where customer_urn='" + values.urn + "'  and status<>'Closed'";
            lsurn = objdbconn.GetExecuteScalar(msSQL);
            if (lsurn == "")
            {
                msGetGid = objcmnfunctions.GetMasterGID("LGST");
                msSQL = "insert into lgl_trn_tdnstatus(" +
                    " dnstatus_gid, " +
                    " dn2status, " +
                    " customer_urn," +
                    " created_by," +
                    " created_date)" +
                    " values (" +
                    "'" + msGetGid + "'," +
                    "'DN2 Generated'," +
                    "'" + values.urn + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = "update lgl_trn_tdnstatus set dn2status='DN2 Generated'," +
                    " updated_by ='" + employee_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where customer_urn='" + values.urn + "' and status<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            values.status = true;
            return true;
        }

        public bool DaPostDN3Generate(MdlMisdataimportlist values, string employee_gid)
        {          
            msSQL = "select customer_urn from lgl_tmp_tdnformat where customer_urn='" + values.urn + "' and status<>'Closed'";
            lsurn = objdbconn.GetExecuteScalar(msSQL);
            if (lsurn == "")
            {
                msGETGID = objcmnfunctions.GetMasterGID("DN1F");
                msSQL = "insert into lgl_tmp_tdnformat(" +
                    " tempdn1format_gid, " +
                    " DN3template_content, " +
                    " dn3generated_by, " +
                    " dn3generated_date," +
                    " customer_urn," +
                    " created_by," +
                    " created_date)" +
                    " values (" +
                    "'" + msGETGID + "'," +
                    "'" + values.template_content + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',"+
                    "'" + values.urn + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "update lgl_trn_tsanctiondtl set tempdn1format_gid='" + msGETGID + "' where customer_urn='" + values.urn + "' and status<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "update lgl_trn_tmisdata set DN_status='DN3 Generated'," +
                    " acknowledgement_status='--'," +
                   " updated_by='" + employee_gid + "'," +
                   "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update lgl_trn_tsanctiondtl set dn3generate_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
             " where customer_urn='" + values.urn + "' and status<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = "update lgl_tmp_tdnformat set DN3template_content='" + values.template_content + "'," +
                    " dn3generated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    " dn3generated_by ='" + employee_gid + "',"+
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    " updated_by ='" + employee_gid + "' where customer_urn='" + values.urn + "' and status<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update lgl_trn_tmisdata set DN_status='DN3 Generated'," +
                    " acknowledgement_status='--'," +
                   " updated_by='" + employee_gid + "'," +
                   "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update lgl_trn_tsanctiondtl set dn3generate_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where customer_urn='" + values.urn + "' and status<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            msSQL = "select customer_urn from lgl_trn_tdnstatus where customer_urn='" + values.urn + "' and status<>'Closed'";
            lsurn = objdbconn.GetExecuteScalar(msSQL);
            if (lsurn == "")
            {
                msGetGid = objcmnfunctions.GetMasterGID("LGST");
                msSQL = "insert into lgl_trn_tdnstatus(" +
                    " dnstatus_gid, " +
                    " dn3status, " +
                    " customer_urn," +
                    " created_by," +
                    " created_date)" +
                    " values (" +
                    "'" + msGetGid + "'," +
                    "'DN3 Generated'," +
                    "'" + values.urn + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = "update lgl_trn_tdnstatus set dn3status='DN3 Generated'," +
                    " updated_by ='" + employee_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' and status<>'Closed'" +
                    " where customer_urn='" + values.urn + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            values.status = true;
            return true;
        }

        public bool DaGetTemplateDN1ContentDTL(string urn,MdlMisdataimportlist values)
        {           
            msSQL = "select DN_status from lgl_trn_tmisdata where urn='" + urn + "'";
            values.DN1_status = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select DN1template_content,DN2template_content,DN3template_content,cbotemplate_content" +
                " from lgl_tmp_tdnformat where customer_urn='" + urn + "' and status<>'Closed'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.template_content = objODBCDatareader["DN1template_content"].ToString();
                values.dn2_content = objODBCDatareader["DN2template_content"].ToString();
                values.dn3_content = objODBCDatareader["DN3template_content"].ToString();
                values.cbotemplate_content = objODBCDatareader["cbotemplate_content"].ToString();
            }
            objODBCDatareader.Close();

            values.status = true;
            return true;
        }

        public bool DaGetRecoveredCases(MdlMisdataimportlist values)
        {            
            msSQL = " select account_no,count(urn),max(od_days) as max_od_days,urn,acknowledgement_status," +
                      " Customer_name,if(Guarantor_Name='','---',Guarantor_Name) as Guarantor_Name,Vertical,DN_status, " +
                       " if(dn1status is null,'---',dn1status) as dn1status,if(dn2status is null,'---',dn2status) as dn2status,"+
                       "  if (dn3status is null,'---',dn3status) as dn3status from lgl_trn_trecoveredcases  a" +
                       " left join lgl_trn_tdnstatus b on a.urn=b.customer_urn   group by urn";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMisdataimport = new List<MdlMisdataimport>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getMisdataimport.Add(new MdlMisdataimport
                    {
                        od_days = (dr_datarow["max_od_days"].ToString()),
                         Customer_name = (dr_datarow["Customer_name"].ToString()),
                        Guarantor_Name = (dr_datarow["Guarantor_Name"].ToString()),
                         Vertical = (dr_datarow["Vertical"].ToString()),
                         urn = (dr_datarow["urn"].ToString()),
                        DNstatus = dr_datarow["DN_status"].ToString(),
                        account_no = dr_datarow["account_no"].ToString(),
                        acknowledgement_status = dr_datarow["acknowledgement_status"].ToString(),
                        DN1status = dr_datarow["DN1status"].ToString(),
                        DN2status = dr_datarow["DN2status"].ToString(),
                        DN3status = dr_datarow["DN3status"].ToString()
                    });
                }
                values.mdlMisdataimport = getMisdataimport;
            }
            dt_datatable.Dispose();

            values.status = true;
            return true;
        }

        public bool DaGetdnList_cbo(MdlMisdataimportlist values)
        {
            msSQL = " select account_no,count(urn),max(od_days) as max_od_days,urn,acknowledgement_status,cbo_status," +
                      " Customer_name,Guarantor_Name,Vertical,DN_status from lgl_trn_tmisdata  where od_days between 30 and 90" +
                      " and urn not in (select urn from lgl_trn_tmisdata where od_days between 91 and 120 group by urn) and Vertical ='CBO'" +
                      " and natureof_credit_amount >=5000 group by urn order by od_days desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMisdataimport = new List<MdlMisdataimport>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getMisdataimport.Add(new MdlMisdataimport
                    {
                        od_days = (dr_datarow["max_od_days"].ToString()),
                       Customer_name = (dr_datarow["Customer_name"].ToString()),
                        Guarantor_Name = (dr_datarow["Guarantor_Name"].ToString()),
                         Vertical = (dr_datarow["Vertical"].ToString()),
                        urn = (dr_datarow["urn"].ToString()),
                        DNstatus = dr_datarow["DN_status"].ToString(),
                        account_no = dr_datarow["account_no"].ToString(),
                        acknowledgement_status = dr_datarow["acknowledgement_status"].ToString(),
                        cbo_status=dr_datarow["cbo_status"].ToString ()
                    });
                }
                values.mdlMisdataimport = getMisdataimport;
            }
            dt_datatable.Dispose();

            values.status = true;
            return true;
        }

        public bool DaPostCBOGenerate(MdlMisdataimportlist values, string employee_gid)
        {
            msSQL = "select customer_urn from lgl_tmp_tdnformat where customer_urn='" + values.urn + "' and status<>'Closed'";
            lsurn = objdbconn.GetExecuteScalar(msSQL);
            if (lsurn == "")
            {
                msGETGID = objcmnfunctions.GetMasterGID("DN1F");
                msSQL = "insert into lgl_tmp_tdnformat(" +
                    " tempdn1format_gid, " +
                    " cbotemplate_content, " +
                    " customer_urn," +
                    " created_by," +
                    " created_date)" +
                    " values (" +
                    "'" + msGETGID + "'," +
                    "'" + values.template_content + "'," +
                    "'" + values.urn + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update lgl_trn_tmisdata set DN_status='DN Generated'," +
                    " acknowledgement_status='--'," +
                   " updated_by='" + employee_gid + "'," +
                   "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = "update lgl_tmp_tdnformat set cbotemplate_content='" + values.template_content + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    " updated_by ='" + employee_gid + "' where customer_urn='" + values.urn + "'  and status<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update lgl_trn_tmisdata set DN_status='DN Generated'," +
                    " acknowledgement_status='--'," +
                   " updated_by='" + employee_gid + "'," +
                   "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            msSQL = "select customer_urn from lgl_trn_tdnstatus where customer_urn='" + values.urn + "'  and status<>'Closed'";
            lsurn = objdbconn.GetExecuteScalar(msSQL);
            if (lsurn == "")
            {
                msGetGid = objcmnfunctions.GetMasterGID("LGST");
                msSQL = "insert into lgl_trn_tdnstatus(" +
                    " dnstatus_gid, " +
                    " cbo_status, " +
                    " customer_urn," +
                    " created_by," +
                    " created_date)" +
                    " values (" +
                    "'" + msGetGid + "'," +
                    "'DN Generated'," +
                    "'" + values.urn + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = "update lgl_trn_tdnstatus set cbo_status='DN Generated'," +
                    " updated_by ='" + employee_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where customer_urn='" + values.urn + "'  and status<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            values.status = true;
            return true;
        }

        public bool DaGetCustomerDetails(string customer_gid, customeredit values)
        {
           msSQL = " select a.customer_gid,a.vertical_code,a.customer_code,a.customername, " +
                   " case when a.address<>'' then a.address else '-' end as address,address2, " +
                   " case when a.mobileno<>'' then a.mobileno else '-' end as mobileno, " +
                   " case when a.contactperson<>'' then a.contactperson else '-' end as contactperson, " +
                   " case when a.creditmanager_gid='' then 'NA' else a.creditmgmt_name end as creditmgmt_name," +
                   " case when a.zonal_head = '' then 'NA' else a.zonal_name end as zonal_head," +
                   " case when a.business_head = '' then 'NA' else a.businesshead_name end as business_head, " +
                   " case when a.cluster_manager_gid = '' then 'NA' else a.cluster_manager_name end as cluster_manager, " +
                   " case when a.relationship_manager = '' then 'NA' else a.relationshipmgmt_name end as relationship_manager,a.customer_urn " +
                   " from ocs_mst_tcustomer a where customer_gid='" + customer_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.customerCodeedit = objODBCDatareader["customer_code"].ToString();
                values.customerNameedit = objODBCDatareader["customername"].ToString();
                values.vertical_code = objODBCDatareader["vertical_code"].ToString();
                values.businesshead_name = objODBCDatareader["business_head"].ToString();
                values.zonal_name = objODBCDatareader["zonal_head"].ToString();
                values.cluster_manager_name = objODBCDatareader["cluster_manager"].ToString();
                values.relationshipmgmt_name = objODBCDatareader["relationship_manager"].ToString();
                values.creditmanager_name = objODBCDatareader["creditmgmt_name"].ToString();
                values.customer_urnedit = objODBCDatareader["customer_urn"].ToString();
                values.contactPersonedit = objODBCDatareader["contactperson"].ToString();
                values.addressline1edit = objODBCDatareader["address"].ToString();
                values.addressline2edit = objODBCDatareader["address2"].ToString();
                values.mobileNoedit = objODBCDatareader["mobileno"].ToString();
                values.customer_urnedit = objODBCDatareader["customer_urn"].ToString();
            }
            objODBCDatareader.Close();

            values.status = true;
            return true;
        }

        public bool DaPostCBOStatus(MdlMisdataimportlist values, string employee_gid)
        {
           msSQL = "select max(od_days) ,account_no from lgl_trn_tmisdata where urn='" + values.urn + "' group by urn";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsaccount_no = objODBCDatareader["account_no"].ToString();
            }
            objODBCDatareader.Close();
            msGetGid = objcmnfunctions.GetMasterGID("DNCS");
            msSQL = " insert into lgl_trn_tdncases(" +
                 " dncase_gid ," +
                 " urn," +
                 " account_no ," +
                 " cbo_status," +
                 " cbostatus_updatedby," +
                 " cbostatus_updateddate,"+
                 " cbo_courier_sentdate )" +               
                 " values (" +
                 "'" + msGetGid + "'," +
                 "'" + values.urn + "'," +
                 "'" + lsaccount_no + "'," +
                 "'DN Sent'," +
                 "'" + employee_gid + "'," +
                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                 "'" + Convert.ToDateTime(values.courier_date).ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msGETGID = objcmnfunctions.GetMasterGID("DNCR");
            msSQL = "insert into lgl_trn_tcourierdetails (" +
                " courier_gid," +
                " dncase_gid," +
                " urn," +
                " CBOcourier_refno ," +
                " CBOcourier_center," +
                " CBOcourier_date," +
                " CBOcouriered_by," +
                " CBOremarks, " +
                " created_by," +
                " created_date)" +
                " values (" +
                "'" + msGETGID + "'," +
                "'" + msGetGid + "'," +
                "'" + values.urn + "'," +
                "'" + values.courier_refno + "'," +
                "'" + values.courier_center + "'," +
                "'" + Convert.ToDateTime(values.courier_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                "'" + values.couriered_by + "'," +
                "'" + values.courier_remarks + "'," +
                "'" + employee_gid + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update lgl_trn_tmisdata set DN_status='DN Sent'," +
                " acknowledgement_status='Pending'," +
                " updated_by='" + employee_gid + "'," +
                "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
            return true;
        }

        public bool DaPostCBOskip(MdlMisdataimportlist values, string employee_gid)
        {
            msSQL = "select max(od_days) ,account_no from lgl_trn_tmisdata where urn='" + values.urn + "' group by urn";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsaccount_no = objODBCDatareader["account_no"].ToString();
            }
            objODBCDatareader.Close();
            msGetGid = objcmnfunctions.GetMasterGID("DNCS");
            msSQL = " insert into lgl_trn_tdncases(" +
                 " dncase_gid ," +
                 " urn," +
                 " account_no," +
                 " cbo_status," +
                 " cbostatus_updatedby," +
                 " cbostatus_updateddate)" +
                 " values (" +
                 "'" + msGetGid + "'," +
                   "'" + values.urn + "'," +
                   "'" + lsaccount_no + "'," +
                   "'DN Skip'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update lgl_trn_tmisdata set DN_status='DN Skip'," +
                 " acknowledgement_status='DN Skipped'," +
                " updated_by='" + employee_gid + "'," +
                "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
            return true;
        }

        public bool DaPostdnCBOackStatus(courierinfo values, string employee_gid)
        {
            if ((values.returened_date) == DateTime.MinValue)
            {
                msSQL = "update lgl_trn_tcourierdetails set CBOcourier_status='DN Delivered'," +
                   " CBOdelivered_date  ='" + Convert.ToDateTime(values.delivered_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                  " updated_by='" + employee_gid + "'," +
                  " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "' and case_flag<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update lgl_trn_tmisdata set acknowledgement_status='DN Delivered'," +
                          " updated_by='" + employee_gid + "'," +
                             "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = "update lgl_trn_tcourierdetails set CBOcourier_status='DN Returned'," +
                              " CBOreturened_date  ='" + Convert.ToDateTime(values.returened_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                             " updated_by='" + employee_gid + "'," +
                             " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "' and case_flag<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update lgl_trn_tmisdata set  acknowledgement_status='DN Returned'," +
           " updated_by='" + employee_gid + "'," +
           "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            values.status = true;
            return true;
        }

        public bool DaPostRevertDN1(MdlMisdataimportlist values, string employee_gid)
        {
            msSQL= "delete from lgl_tmp_tdnformat where customer_urn='"+ values.urn+ "' and status<>'Closed'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from lgl_trn_tdnstatus where customer_urn='" + values.urn + "' and status<>'Closed'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update lgl_trn_tsanctiondtl set dn1_flag='N' where customer_urn='" + values.urn + "' and status <>'Closed'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update lgl_trn_tmisdata set DN_status='DN1 Reverted'," +
                " revert_flag='Y',"+
                " remarks='" + values.remarks + "',"+
                    " acknowledgement_status='---'," +
                   " updated_by='" + employee_gid + "'," +
                   "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "DN1 Reverted Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while reverting DN1";
                return false;
            }
        }

        public bool DaPostRevertDN2(MdlMisdataimportlist values, string employee_gid)
        {
            msSQL = "delete from lgl_tmp_tdnformat where customer_urn='" + values.urn + "'  and status<>'Closed'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from lgl_trn_tdnstatus where customer_urn='" + values.urn + "'  and status<>'Closed'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            msSQL = "update lgl_trn_tsanctiondtl set dn2_flag='N' where customer_urn='" + values.urn + "' and status <>'Closed'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update lgl_trn_tmisdata set DN_status='DN2 Reverted'," +
                " revert_flag='Y'," +
                " remarks='" + values.remarks + "'," +
                    " acknowledgement_status='---'," +
                   " updated_by='" + employee_gid + "'," +
                   "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "DN2 Reverted Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while reverting DN1";
                return false;
            }
        }

        public bool DaPostRevertDN3(MdlMisdataimportlist values, string employee_gid)
        {
            msSQL = "delete from lgl_tmp_tdnformat where customer_urn='" + values.urn + "' and status<>'Closed'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from lgl_trn_tdnstatus where customer_urn='" + values.urn + "' and status<>'Closed'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            msSQL = "update lgl_trn_tsanctiondtl set dn3_flag='N' where customer_urn='" + values.urn + "' and status <>'Closed'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update lgl_trn_tmisdata set DN_status='DN3 Reverted'," +
                " revert_flag='Y'," +
                " remarks='" + values.remarks + "'," +
                    " acknowledgement_status='---'," +
                   " updated_by='" + employee_gid + "'," +
                   "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "DN Reverted Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while reverting DN";
                return false;
            }
        }

        public bool DaPostRevertDN_CBO(MdlMisdataimportlist values, string employee_gid)
        {
            msSQL = "delete from lgl_tmp_tdnformat where customer_urn='" + values.urn + "' and status<>'Closed'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from lgl_trn_tdnstatus where customer_urn='" + values.urn + "'  and status<>'Closed'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update lgl_trn_tsanctiondtl set dnCBO_flag='N' where customer_urn='" + values.urn + "' and status <>'Closed'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            msSQL = "update lgl_trn_tmisdata set DN_status='DN Reverted'," +
                " revert_flag='Y'," +
                " remarks='" + values.remarks + "'," +
                    " acknowledgement_status='---'," +
                   " updated_by='" + employee_gid + "'," +
                   "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "DN3 Reverted Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while reverting DN1";
                return false;
            }
        }

        public bool DaGetRevertDetails(string urn, dnrevert values)
        {
            msSQL = "select dn_status, concat(c.user_firstname, ' ', c.user_lastname) as updated_by, "+
                    " date_format(a.updated_date, '%d-%m-%Y') as updated_date,a.remarks from lgl_trn_tmisdata a" +
                    " left join hrm_mst_temployee b on a.updated_by = b.employee_gid"+
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid where urn='" + urn + "'";
         objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.dn_status = objODBCDatareader["dn_status"].ToString();
                values.updated_by = objODBCDatareader["updated_by"].ToString();
                values.updated_date = objODBCDatareader["updated_date"].ToString();
                values.remarks = objODBCDatareader["remarks"].ToString();
              }
            objODBCDatareader.Close();
            values.status = true;
            return true;
        }

        public bool DaPostHoldDN_CBO(MdlMisdataimportlist values, string employee_gid)
        {          
            msSQL = "update lgl_trn_tmisdata set DN_status='DN Hold'," +
                " remarks='" + values.remarks + "'," +
                    " acknowledgement_status='---'," +
                   " updated_by='" + employee_gid + "'," +
                   "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL= "update lgl_trn_tdnstatus set cbo_status='DN Hold',"+
                 " updated_by='" + employee_gid + "'," +
                   "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where customer_urn='" + values.urn + "' status<>'Closed'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "DN Status updated Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while holding DN";
                return false;
            }
        }

        public bool DaPostHoldDN1(MdlMisdataimportlist values, string employee_gid)
        {
            msSQL = "update lgl_trn_tmisdata set DN_status='DN1 Hold'," +
                " remarks='" + values.remarks + "'," +
                    " acknowledgement_status='---'," +
                   " updated_by='" + employee_gid + "'," +
                   "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update lgl_trn_tdnstatus set dn1status='DN1 Hold'," +
                " updated_by='" + employee_gid + "'," +
                  "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where customer_urn='" + values.urn + "' and status<>'Closed'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "DN1 Status updated Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while holding DN1";
                return false;
            }
        }

        public bool DaPostHoldDN2(MdlMisdataimportlist values, string employee_gid)
        {
            msSQL = "update lgl_trn_tmisdata set DN_status='DN2 Hold'," +
                " remarks='" + values.remarks + "'," +
                    " acknowledgement_status='---'," +
                   " updated_by='" + employee_gid + "'," +
                   "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update lgl_trn_tdnstatus set dn2status='DN2 Hold'," +
               " updated_by='" + employee_gid + "'," +
                 "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where customer_urn='" + values.urn + "' and status<>'Closed'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "DN2 Status updated Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while holding DN";
                return false;
            }
        }

        public bool DaPostHoldDN3(MdlMisdataimportlist values, string employee_gid)
        {
            msSQL = "update lgl_trn_tmisdata set DN_status='DN3 Hold'," +
                " remarks='" + values.remarks + "'," +
                    " acknowledgement_status='---'," +
                   " updated_by='" + employee_gid + "'," +
                   "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update lgl_trn_tdnstatus set dn3status='DN3 Hold'," +
               " updated_by='" + employee_gid + "'," +
                 "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where customer_urn='" + values.urn + "'and status<>'Closed'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "DN3 Status updated Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while holding DN3";
                return false;
            }
        }

        public bool DaPostunholdDN_CBO(MdlMisdataimportlist values, string employee_gid)
        {
            msSQL = "update lgl_trn_tmisdata set DN_status='DN Generated'," +
                    " acknowledgement_status='---'," +
                   " updated_by='" + employee_gid + "'," +
                   "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update lgl_trn_tdnstatus set cbo_status='DN Generated'," +
                 " updated_by='" + employee_gid + "'," +
                   "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where customer_urn='" + values.urn + "' and status<>'Closed'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "DN Status updated Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while holding DN";
                return false;
            }
        }

        public bool DaPostUnholdDN1(MdlMisdataimportlist values, string employee_gid)
        {
            msSQL = "update lgl_trn_tmisdata set DN_status='DN1 Generated'," +
                    " acknowledgement_status='---'," +
                   " updated_by='" + employee_gid + "'," +
                   "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update lgl_trn_tdnstatus set dn1status='DN1 Generated'," +
                " updated_by='" + employee_gid + "'," +
                  "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where customer_urn='" + values.urn + "' and status<>'Closed'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "DN1 Status updated Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while holding DN1";
                return false;
            }
        }

        public bool DaPostUnholdDN2(MdlMisdataimportlist values, string employee_gid)
        {
            msSQL = "update lgl_trn_tmisdata set DN_status='DN2 Generated'," +
                    " acknowledgement_status='---'," +
                   " updated_by='" + employee_gid + "'," +
                   "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update lgl_trn_tdnstatus set dn2status='DN2 Generated'," +
               " updated_by='" + employee_gid + "'," +
                 "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where customer_urn='" + values.urn + "' and status<>'Closed'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "DN2 Status updated Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while holding DN";
                return false;
            }
        }

        public bool DaPostUnholdDN3(MdlMisdataimportlist values, string employee_gid)
        {
            msSQL = "update lgl_trn_tmisdata set DN_status='DN3 Generated'," +
                    " acknowledgement_status='---'," +
                   " updated_by='" + employee_gid + "'," +
                   "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update lgl_trn_tdnstatus set dn3status='DN3 Generated'," +
               " updated_by='" + employee_gid + "'," +
                 "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where customer_urn='" + values.urn + "' and status<>'Closed'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "DN3 Status updated Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while holding DN3";
                return false;
            }
        }
        public bool DaPostDN1sanctiondtl(sanctiondtl values, string employee_gid)
        {
            msSQL = "select customer_urn from lgl_trn_tsanctiondtl where customer_urn='" + values.urn + "'  and status<>'Closed'";
            lsurn = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select sanction_date from ocs_mst_tcustomer2sanction where sanction_refno='" + values.dn1sanctionref_no + "' and customer_urn='" + values.urn + "'";
            string lssanction_date = objdbconn.GetExecuteScalar(msSQL);

            if (lsurn == "")
            {
               
                msGetGid = objcmnfunctions.GetMasterGID("DNSA");
                msSQL = "insert into lgl_trn_tsanctiondtl(" +
                      " sanction_gid," +
                      " customer_urn," +
                      " dn1ref_no," +
                      " dn1sanctionref_no," +
                      " dn1sanction_date," +
                      " dn1sanction_amount," +
                      " dn1_flag, " +
                      " created_by ," +
                      " created_date)" +
                      " values (" +
                      "'" + msGetGid + "'," +
                       "'" + values.urn + "'," +
                       "'" + values.dn1ref_no + "'," +
                       "'" + values.dn1sanctionref_no + "',";

               
                if ((lssanction_date==null) || (lssanction_date == ""))
                {
                    msSQL += "'" + Convert.ToDateTime(values.dn1sanction_date).AddDays(1).ToString("yyyy-MM-dd") + "',";
                   
                }
                else
                {
                    msSQL += "'" +lssanction_date + "',";
                }
               
                msSQL += "'" + values.dn1sanction_amount.Replace(",", "") + "'," +
                  "'Y'," +
                  "'" + employee_gid + "'," +
                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
              
            }
            else
            {
            
                msSQL = " update lgl_trn_tsanctiondtl set dn1ref_no='" + values.dn1ref_no + "'," +
                     " dn1sanctionref_no='" + values.dn1sanctionref_no + "',";
                if ((lssanction_date == null) || (lssanction_date == ""))
                {
                    msSQL += "dn1sanction_date='" + Convert.ToDateTime(values.dn1sanction_date).AddDays(1).ToString("yyyy-MM-dd") + "',";

                }
                else
                {
                    msSQL += "dn1sanction_date='" + lssanction_date + "',";
                }

                msSQL += " dn1sanction_amount='" + values.dn1sanction_amount.Replace(",", "") + "'," +
                       " dn1_flag='Y'," +
                       " updated_by='" + employee_gid + "'," +
                       " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where customer_urn='" + values.urn + "'  and status<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            if (mnResult != 0)
            {
                values.message = "Sanction Details Added Successfully";
                values.status = true;
                return true;
            }
            else
            {
                values.message = "Error Occured while adding";
                values.status = false;
                return false;
            }
        }

        public bool DaPostDN2sanctiondtl(sanctiondtl values, string employee_gid)
        {
            msSQL = "select customer_urn from lgl_trn_tsanctiondtl where customer_urn='" + values.urn + "'  and status<>'Closed'";
            lsurn = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select sanction_date from ocs_mst_tcustomer2sanction where sanction_refno='" + values.dn2sanctionref_no + "' and customer_urn='" + values.urn + "'";
            string lssanction_date = objdbconn.GetExecuteScalar(msSQL);

            if (lsurn == "")
            {
                
                msGetGid = objcmnfunctions.GetMasterGID("DNSA");
                msSQL = "insert into lgl_trn_tsanctiondtl(" +
                      " sanction_gid," +
                      " customer_urn," +
                      " dn2ref_no," +
                      " dn2sanctionref_no," +
                      " dn2sanction_date," +
                      " dn2sanction_amount," +
                      " dn2_flag," +
                      " created_by ," +
                      " created_date)" +
                      " values (" +
                      "'" + msGetGid + "'," +
                       "'" + values.urn + "'," +
                       "'" + values.dn2ref_no + "'," +
                       "'" + values.dn2sanctionref_no + "',";
                if ((lssanction_date == null) || (lssanction_date == ""))
                {
                    msSQL += "'" + Convert.ToDateTime(values.dn2sanction_date).AddDays(1).ToString("yyyy-MM-dd") + "',";

                }
                else
                {
                    msSQL += "'" + lssanction_date + "',";
                }

                msSQL += "'" + values.dn2sanction_amount.Replace(",", "") + "'," +
                       "'Y',"+
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = " update lgl_trn_tsanctiondtl set dn2ref_no='" + values.dn2ref_no + "'," +
                       " dn2sanctionref_no='" + values.dn2sanctionref_no + "',";
                if ((lssanction_date == null) || (lssanction_date == ""))
                {
                    msSQL += "dn2sanction_date='" + Convert.ToDateTime(values.dn2sanction_date).AddDays(1).ToString("yyyy-MM-dd") + "',";

                }
                else
                {
                    msSQL += "dn2sanction_date='" + lssanction_date + "',";
                }

                msSQL +=" dn2sanction_amount='" + values.dn2sanction_amount.Replace(",", "") + "'," +
                       " dn2_flag='Y',"+
                       " updated_by='" + employee_gid + "'," +
                       " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where customer_urn='" + values.urn + "'  and status<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            if (mnResult != 0)
            {
                values.message = "Sanction Details Added Successfully";
                values.status = true;
                return true;
            }
            else
            {
                values.message = "Error Occured while adding";
                values.status = false;
                return false;
            }
        }

        public bool DaPostDN3sanctiondtl(sanctiondtl values, string employee_gid)
        {
            msSQL = "select customer_urn from lgl_trn_tsanctiondtl where customer_urn='" + values.urn + "'  and status<>'Closed'";
            lsurn = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select sanction_date from ocs_mst_tcustomer2sanction where sanction_refno='" + values.dn3sanctionref_no + "' and customer_urn='" + values.urn + "'";
            string lssanction_date = objdbconn.GetExecuteScalar(msSQL);
            if (lsurn == "")
            {
                msGetGid = objcmnfunctions.GetMasterGID("DNSA");
                msSQL = "insert into lgl_trn_tsanctiondtl(" +
                      " sanction_gid," +
                      " customer_urn," +
                      " dn3ref_no," +
                      " dn3sanctionref_no," +
                      " dn3sanction_date," +
                      " dn3sanction_amount," +
                      " dn3_flag," +
                      " created_by ," +
                      " created_date)" +
                      " values (" +
                      "'" + msGetGid + "'," +
                       "'" + values.urn + "'," +
                       "'" + values.dn3ref_no + "'," +
                       "'" + values.dn3sanctionref_no + "',";
                if ((lssanction_date == null) || (lssanction_date == ""))
                {
                    msSQL += "'" + Convert.ToDateTime(values.dn3sanction_date).AddDays(1).ToString("yyyy-MM-dd") + "',";

                }
                else
                {
                    msSQL += "'" + lssanction_date + "',";
                }

                msSQL += "'" + values.dn3sanction_amount.Replace(",", "") + "'," +
                       "'Y',"+
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = " update lgl_trn_tsanctiondtl set dn3ref_no='" + values.dn3ref_no + "'," +
                       " dn3sanctionref_no='" + values.dn3sanctionref_no + "',";
                if ((lssanction_date == null) || (lssanction_date == ""))
                {
                    msSQL += "dn3sanction_date='" + Convert.ToDateTime(values.dn3sanction_date).AddDays(1).ToString("yyyy-MM-dd") + "',";

                }
                else
                {
                    msSQL += "dn3sanction_date='" + lssanction_date + "',";
                }

                msSQL += " dn3_flag='Y'," +
                       " dn3sanction_amount='" + values.dn3sanction_amount.Replace(",", "") + "'," +
                       " updated_by='" + employee_gid + "'," +
                       " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where customer_urn='" + values.urn + "'  and status<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            if (mnResult != 0)
            {
                values.message = "Sanction Details Added Successfully";
                values.status = true;
                return true;
            }
            else
            {
                values.message = "Error Occured while adding";
                values.status = false;
                return false;
            }
        }

        public bool DaPostDNCBOsanctiondtl(sanctiondtl values, string employee_gid)
        {
            msSQL = "select customer_urn from lgl_trn_tsanctiondtl where customer_urn='" + values.urn + "'  and status<>'Closed'";
            lsurn = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select sanction_date from ocs_mst_tcustomer2sanction where sanction_refno='" + values.dnCBOsanctionref_no + "' and customer_urn='" + values.urn + "'";
            string lssanction_date = objdbconn.GetExecuteScalar(msSQL);
            if (lsurn == "")
            {
                msGetGid = objcmnfunctions.GetMasterGID("DNSA");
                msSQL = "insert into lgl_trn_tsanctiondtl(" +
                      " sanction_gid," +
                      " customer_urn," +
                      " dnCBOref_no," +
                      " dnCBOsanctionref_no," +
                      " dnCBOsanction_date," +
                      " dnCBOsanction_amount," +
                      " dnCBO_flag," +
                      " created_by ," +
                      " created_date)" +
                      " values (" +
                      "'" + msGetGid + "'," +
                       "'" + values.urn + "'," +
                       "'" + values.dnCBOref_no + "'," +
                       "'" + values.dnCBOsanctionref_no + "',";
                if ((lssanction_date == null) || (lssanction_date == ""))
                {
                    msSQL += "'" + Convert.ToDateTime(values.dnCBOsanction_date).AddDays(1).ToString("yyyy-MM-dd") + "',";

                }
                else
                {
                    msSQL += "'" + lssanction_date + "',";
                }

                msSQL += "'" + values.dnCBOsanction_amount.Replace(",", "") + "'," +
                           "'Y'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            else
            {
                msSQL = " update lgl_trn_tsanctiondtl set dnCBOref_no='" + values.dnCBOref_no + "'," +
                     " dnCBOsanctionref_no='" + values.dnCBOsanctionref_no + "',";
                if ((lssanction_date == null) || (lssanction_date == ""))
                {
                    msSQL += "dnCBOsanction_date='" + Convert.ToDateTime(values.dnCBOsanction_date).AddDays(1).ToString("yyyy-MM-dd") + "',";

                }
                else
                {
                    msSQL += "dnCBOsanction_date='" + lssanction_date + "',";
                }

                msSQL += " dnCBOsanction_amount='" + values.dnCBOsanction_amount.Replace(",", "") + "'," +
                       " dnCBO_flag='Y'," +
                       " updated_by='" + employee_gid + "'," +
                       " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where customer_urn='" + values.urn + "'  and status<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
                if (mnResult != 0)
                {
                    values.message = "Sanction Details Added Successfully";
                    values.status = true;
                    return true;
                }
                else
                {
                    values.message = "Error Occured while adding";
                    values.status = false;
                    return false;
                }
            }
        public bool DaGetSanctiondtl(string urn,sanctiondtl values)
        {
            msSQL = "select dn1ref_no,dn1sanctionref_no,dn1sanction_amount, date_format(dn1sanction_date,'%d-%m-%Y') as dn1sanction_date," +
                "  dn2ref_no,dn2sanctionref_no, dn2sanction_amount,date_format(dn2sanction_date,'%d-%m-%Y') as dn2sanction_date, " +
                "  dn3ref_no,dn3sanctionref_no, dn3sanction_amount,date_format(dn3sanction_date,'%d-%m-%Y') as dn3sanction_date, " +
                "  dnCBOref_no,dnCBOsanctionref_no,dnCBOsanction_amount, date_format(dnCBOsanction_date,'%d-%m-%Y') as dnCBOsanction_date, " +
                "  dncbo2ref_no,dncbo2sanctionref_no,dncbo2sanction_amount, date_format(dncbo2sanction_date,'%d-%m-%Y') as dncbo2sanction_date, " +
                "  dncbo3ref_no,dncbo3sanctionref_no,dncbo3sanction_amount, date_format(dncbo3sanction_date,'%d-%m-%Y') as dncbo3sanction_date, " +
                "  dn1_flag,dn2_flag,dn3_flag,dnCBO_flag,dncbo3_flag,dncbo2_flag,dn_type from lgl_trn_tsanctiondtl where customer_urn='" + urn + "'  and status<>'Closed'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader .HasRows ==true)
            {
                values.dn1ref_no = objODBCDatareader["dn1ref_no"].ToString();
                values.dn1sanctionref_no = objODBCDatareader["dn1sanctionref_no"].ToString();
                values.dn1sanction_amount = objODBCDatareader["dn1sanction_amount"].ToString();
                if (objODBCDatareader["dn1sanction_date"].ToString()  =="")
                {
                  
                }
                else
                {
                    values.dn1_date =objODBCDatareader["dn1sanction_date"].ToString ();
                }
                values.dn2ref_no = objODBCDatareader["dn2ref_no"].ToString();
                values.dn2sanctionref_no = objODBCDatareader["dn2sanctionref_no"].ToString();
                values.dn2sanction_amount = objODBCDatareader["dn2sanction_amount"].ToString();
                if (objODBCDatareader["dn2sanction_date"].ToString ()=="")
                {

                }
                else
                {
                    values.dn2_date = objODBCDatareader["dn2sanction_date"].ToString();
                }
               
                values.dn3ref_no = objODBCDatareader["dn3ref_no"].ToString();
                values.dn3sanctionref_no = objODBCDatareader["dn3sanctionref_no"].ToString();
                values.dn3sanction_amount = objODBCDatareader["dn3sanction_amount"].ToString();
                if (objODBCDatareader["dn3sanction_date"].ToString () == "")
                {

                }
                else
                {
                    values.dn3_date = objODBCDatareader["dn3sanction_date"].ToString();
                }
                values.dn1_flag = objODBCDatareader["dn1_flag"].ToString();
                values.dn2_flag = objODBCDatareader["dn2_flag"].ToString();
                values.dn3_flag = objODBCDatareader["dn3_flag"].ToString();
                values.dnCBO_flag = objODBCDatareader["dnCBO_flag"].ToString();
                values.dnCBOref_no = objODBCDatareader["dnCBOref_no"].ToString();
                values.dnCBOsanctionref_no = objODBCDatareader["dnCBOsanctionref_no"].ToString();
                values.dnCBOsanction_amount = objODBCDatareader["dnCBOsanction_amount"].ToString();
                if (objODBCDatareader["dnCBOsanction_date"].ToString () == "")
                {

                }
                else
                {
                    values.dnCBO_date = objODBCDatareader["dnCBOsanction_date"].ToString();
                }
                values.dnCBO_flag = objODBCDatareader["dnCBO_flag"].ToString();
                values.dnCBOref_no = objODBCDatareader["dnCBOref_no"].ToString();
                values.dnCBOsanctionref_no = objODBCDatareader["dnCBOsanctionref_no"].ToString();
                values.dnCBOsanction_amount = objODBCDatareader["dnCBOsanction_amount"].ToString();
                if (objODBCDatareader["dnCBOsanction_date"].ToString() == "")
                {

                }
                else
                {
                    values.dnCBO_date = objODBCDatareader["dnCBOsanction_date"].ToString();
                }
                values.dncbo2_flag = objODBCDatareader["dncbo2_flag"].ToString();
                values.dncbo2ref_no = objODBCDatareader["dncbo2ref_no"].ToString();
                values.dncbo2sanctionref_no = objODBCDatareader["dncbo2sanctionref_no"].ToString();
                values.dncbo2sanction_amount = objODBCDatareader["dncbo2sanction_amount"].ToString();
                if (objODBCDatareader["dncbo2sanction_date"].ToString() == "")
                {

                }
                else
                {
                    values.dncbo2_date = objODBCDatareader["dncbo2sanction_date"].ToString();
                }
                values.dncbo3_flag = objODBCDatareader["dncbo3_flag"].ToString();
                values.dncbo3ref_no = objODBCDatareader["dncbo3ref_no"].ToString();
                values.dncbo3sanctionref_no = objODBCDatareader["dncbo3sanctionref_no"].ToString();
                values.dncbo3sanction_amount = objODBCDatareader["dncbo3sanction_amount"].ToString();
                if (objODBCDatareader["dncbo3sanction_date"].ToString() == "")
                {

                }
                else
                {
                    values.dncbo3_date = objODBCDatareader["dncbo3sanction_date"].ToString();
                }
                values.dn_type = objODBCDatareader["dn_type"].ToString();
                values.dn_flag = "N";
                objODBCDatareader.Close();
            }
            else
            {
                values.dn_flag = "Y";
                objODBCDatareader.Close();
            }
            
            values.status = true;
            return true;

        }
        public bool DaGetsanctionloandetails(sanctionloan values, string urn)
        {
            msSQL = "select customer_gid from ocs_mst_tcustomer where customer_urn='" + urn + "'";
            string lscustomer_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select customer2sanction_gid,a.sanction_refno,if(a.sanction_date is null,'',date_format(a.sanction_date,'%Y-%m-%d')) as sanctiondate, " +
                    " format(sanction_amount, 2) as sanction_amount,facility_type,concat(sanction_limit,'.00') as sanction_limit" +
                    " from ocs_mst_tcustomer2sanction a " +
                    " where a.customer_gid = '" + lscustomer_gid + "' order by customer2sanction_gid desc limit 1 ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.sanction_amount = objODBCDatareader["sanction_amount"].ToString();
                if (objODBCDatareader["sanctiondate"].ToString () == "")
                {
                   
                }
                else
                {
                    values.sanction_date = Convert.ToDateTime(objODBCDatareader["sanctiondate"]);
                }
                values.sanction_refno = objODBCDatareader["sanction_refno"].ToString();
              
            }
            objODBCDatareader.Close();
            values.status = true;
            return true;
        }
    }
}