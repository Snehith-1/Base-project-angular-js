(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstRMDisbursementRequestViewController', MstRMDisbursementRequestViewController);

    MstRMDisbursementRequestViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstRMDisbursementRequestViewController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstRMDisbursementRequestViewController';

        var application_gid = $location.search().application_gid;
        var application2sanction_gid = $location.search().application2sanction_gid;
        var application2loan_gid = $location.search().application2loan_gid;
        $scope.customer_urn = $location.search().customer_urn;
        var customer_urn = $scope.customer_urn;
        $scope.generatelsa_gid = $location.search().generatelsa_gid;
        var lsgeneratelsa_gid = $scope.generatelsa_gid;
        var rmdisbursementrequest_gid = $location.search().rmdisbursementrequest_gid;
        
        activate();

        lockUI(); 
        function activate() {

            var params = {
                application_gid: $location.search().application_gid,
                rmdisbursementrequest_gid: $location.search().rmdisbursementrequest_gid
            }
            var url = 'api/MstCreditOpsApplication/GetApplicantSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.amountapplication_gid = resp.data.application_gid;
                $scope.credit_gid = resp.data.credit_gid;
                $scope.lblcustomer_name = resp.data.customer_name;
                $scope.lblmobile_no = resp.data.mobile_no;
                $scope.lblemail_address = resp.data.email_address;
                $scope.lbldisbursement_amount = resp.data.disbursement_amount;
                $scope.lbldisbursementamount_gid = resp.data.disbursementamount_gid;
                $scope.lblmakerdisbursement_amount = resp.data.makerdisbursement_amount;
                $scope.lblcheckerdisbursement_amount = resp.data.checkerdisbursement_amount;
            });

            var params = {
                rmdisbursementrequest_gid: rmdisbursementrequest_gid
            }

            var url = 'api/MstCreditOpsApplication/GetDisbursementSupplierSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.disbsupplierdtl_list = resp.data.disbsupplierdtl_list;
            });

            var params = {
                rmdisbursementrequest_gid: rmdisbursementrequest_gid
            }

            var url = 'api/MstCreditOpsApplication/GetDisbFarmerIndividualCreditOps';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.farmerindividualsummary_list = resp.data.farmerindividualsummary_list;
            });
            var params = {
                application_gid: $location.search().application_gid
            }
            var url = 'api/MstCreditOpsApplication/GetDisbursementApplicantView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lblcustomer_urn = resp.data.customer_urn;
                $scope.customer_name = resp.data.customer_name;
                $scope.vertical_name = resp.data.vertical_name;
                $scope.program_name = resp.data.program_name;
                $scope.mobile_no = resp.data.mobile_no;
                $scope.email_address = resp.data.email_address;
            })
            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstCADApplication/GetLoanDetailsView';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.loandetails_list = resp.data.alldatamodified_List;
                unlockUI();
            });

            var params = {
                application_gid: application_gid
            }

            var url = 'api/MstCreditOpsApplication/GetDidbScannedDocList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtapplication_no = resp.data.application_no;
                $scope.txtbasiccustomer_name = resp.data.customer_name;
                $scope.ScannedDocumentlist = resp.data.ScannnedDocTaggedDocument;
                $scope.ScannnedCovenantDocumentlist = resp.data.ScannnedCovenantDocTaggedDocument;
            });

            var params = {
                application_gid: application_gid
            }

            var url = 'api/MstCreditOpsApplication/GetDidbPhysicalDocList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtapplication_no = resp.data.application_no;
                $scope.txtbasiccustomer_name = resp.data.customer_name;
                $scope.PhysicalDocumentlist = resp.data.PhysicalDocTaggedDocument;
                $scope.PhysicalCovenantDocumentlist = resp.data.PhysicalCovenantDocTaggedDocument;
            });

            var params = {
                rmdisbursementrequest_gid: rmdisbursementrequest_gid
            }
            var url = 'api/MstCreditOpsApplication/GetDisbursementApprovalDtlView';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.creditopsmaker_name = resp.data.creditopsmaker_name;
                $scope.creditopschecker_name = resp.data.creditopschecker_name;
                $scope.creditopsgroup_name = resp.data.creditopsgroup_name;
                $scope.maker_approveddate = resp.data.maker_approveddate;
                $scope.checker_approveddate = resp.data.checker_approveddate;
            });

            var params = {
                rmdisbursementrequest_gid: rmdisbursementrequest_gid
            }
            var url = 'api/MstCreditOpsApplication/GetDisbApplicantSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.disbapplicantbankacctdtl_list = resp.data.disbapplicantbankacctdtl_list;
            });

            var params = {
                rmdisbursementrequest_gid: rmdisbursementrequest_gid
            }
            var url = 'api/MstCreditOpsApplication/GetDisbursementCreditOpsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.cbosanctionref_no = resp.data.application2sanction_gid;
                $scope.sanction_refno = resp.data.sanction_refno;
                $scope.cboproductsubproduct_name = resp.data.application2loan_gid;
                $scope.lblproduct_type = resp.data.product_type;
                $scope.txtlblremarks = resp.data.remarks;
                $scope.amounttobe_disbursed = resp.data.amounttobe_disbursed;
                $scope.lblloandisbursement_date = resp.data.loandisbursement_date;
                $scope.editloandisbursement_date = resp.data.editloandisbursement_date;
                $scope.lsareference_number = resp.data.lsareference_number;
                $scope.txtchecker_remarks = resp.data.checker_remarks;
                $scope.txtmaker_remarks = resp.data.maker_remarks;
                $scope.disbursement_to = resp.data.disbursement_to;
                if ($scope.disbursement_to == "Supplier") {
                    $scope.supplierdtl_show = true;
                    $scope.farmerdtl_show = false;
                    $scope.Applicantdtl_show = false;
                }
                else if ($scope.disbursement_to == "Farmer(B2B2C)") {
                    $scope.supplierdtl_show = false;
                    $scope.farmerdtl_show = true;
                    $scope.Applicantdtl_show = false;
                }
                else if ($scope.disbursement_to == "Applicant(B2B)") {
                    $scope.supplierdtl_show = false;
                    $scope.farmerdtl_show = false;
                    $scope.Applicantdtl_show = true;
                }
                else {
                    $scope.supplierdtl_show = false;
                    $scope.farmerdtl_show = false;
                    $scope.Applicantdtl_show = false;
                }
            });

            var params = {
                rmdisbursementrequest_gid: rmdisbursementrequest_gid
            }
            var url = 'api/MstCreditOpsApplication/GetRmDisbursementDocumentDtl';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.disbursementuploaddocument_list = resp.data.disbursementuploadeddocument_list;
            });

            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstCADFlow/GetApplicationBasicView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lblapplication_no = resp.data.application_no;
                $scope.lblbasiccustomer_name = resp.data.customer_name;
                $scope.lblbasicdesignation = resp.data.designation_type;
                $scope.lblvertical = resp.data.vertical_name;
                $scope.lblconstitution = resp.data.constitution_name;
                $scope.lblcredit_group = resp.data.creditgroup_name;
                $scope.lblcustomer_urnno = resp.data.customer_urnno;
                $scope.lblbusinessapproved_date = resp.data.businessapproved_date;
                $scope.lblccapproved_date = resp.data.ccapproved_date;
                $scope.lblregion = resp.data.region;
            });
            //var url = 'api/MstCreditOpsApplication/GetDisbursementDocSummary';
            //SocketService.getparams(url, params).then(function (resp) {
            //    unlockUI();
            //    $scope.disbursementuploaddocument_list = resp.data.disbursementuploaddocument_list;
            //});
            // Get Overall Limit
            var url = 'api/MstCADFlow/GetProductChargesDtl';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lblSanctionAmount = resp.data.overalllimit_amount;
            });
            var url = 'api/MstCADFlow/BankAccountDetailsList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.bankaccount_list = resp.data.bankaccount_list;
            });
            var url = 'api/MstCAD/GetCADBasicView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lblentity_gid = resp.data.entity_gid;
                $scope.lblentity_name = resp.data.entity_name;
                $scope.lblcreditapproved_date = resp.data.creditapproved_date;
                $scope.lblvertical_code = resp.data.vertical_code;
            });
            //var url = 'api/MstCreditOpsApplication/GetSanctionRefnoDropDown';
            //SocketService.getparams(url, params).then(function (resp) {
            //    unlockUI();
            //    $scope.sanctionrefnolist = resp.data.sanctionrefnolist;
            //});

            var params = {
                application_gid: application_gid
            }
            var url = "api/MstCADFlow/GetlsaProductname";
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.loanproductlist = resp.data.LsaProductname;
            });

            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstCreditOpsApplication/GetCreditDisbursementBankDtls';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.creditbankaccount_list = resp.data.creditbankaccount_list;
            });

            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstCreditOpsApplication/GetLSADisbursementBankDtls';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lsabankaccount_list = resp.data.lsabankaccount_list;
            });

            var params = {
                application2sanction_gid: application2sanction_gid,
                application_gid: application_gid
            }
            var url = 'api/MstCreditOpsApplication/GetSanctionDtls';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.application2sanction_gid = resp.data.application2sanction_gid;               
                $scope.lblsanction_date = resp.data.sanction_date;
                $scope.lblsanctionfrom_date = resp.data.sanctionfrom_date;
                $scope.lblsanctiontill_date = resp.data.sanctiontill_date;               
            });

            var params = {
                application2loan_gid: application2loan_gid
            }
            var url = 'api/MstCreditOpsApplication/GetProductChargesDtl';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.application2sanction_gid = resp.data.application2sanction_gid;
                $scope.application_gid = resp.data.application_gid;
                $scope.lblfacility_purpose = resp.data.enduse_purpose;
                $scope.lblrevolving = resp.data.facility_mode;
                $scope.lblinterest_ratemargin = resp.data.rate_interest;
                $scope.lbltenure_days = resp.data.tenureoverall_limit;
               
            });

            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstCreditOpsApplication/GetRMDisbursementDtlView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.application2sanction_gid = resp.data.application2sanction_gid;
                $scope.application_gid = resp.data.application_gid;
                $scope.sanction_refno = resp.data.sanction_refno;
                $scope.product_type = resp.data.product_type;
                $scope.lblprocessing_fees = resp.data.processing_fees;
                $scope.lblgst = resp.data.gst;
                $scope.lbladditional_charges = resp.data.finance_charges;
                $scope.lblod_amount = resp.data.od_amount;
                $scope.lblpayment_escrow = resp.data.escrow_payment;
                $scope.lblnach_status = resp.data.nach_status;
                $scope.lblremarks = resp.data.remarks;
                $scope.lblamount_distursed = resp.data.amounttobe_disbursed;
                $scope.lblloan_date = resp.data.loandisbursement_date;
            });
            var params = {
                application2sanction_gid: application2sanction_gid,
                application_gid: application_gid
            }
            var url = 'api/MstCreditOpsApplication/GetSanctionDtls';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.application2sanction_gid = resp.data.application2sanction_gid;
                $scope.sanction_refno = resp.data.sanction_refno;
                $scope.lblsanction_date = resp.data.sanction_date;
                $scope.lblsanctionfrom_date = resp.data.sanctionfrom_date;
                $scope.lblsanctiontill_date = resp.data.sanctiontill_date;
            });           

           
            var params = {
                application2sanction_gid: application2sanction_gid
            };
            lockUI();
            var url = 'api/MstApplicationReport/CADSanctionDtls';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.sanction_refno = resp.data.sanction_refno;
                $scope.SanctionDateEdit = resp.data.sanctionDate;
                $scope.sanction_date = resp.data.sanction_date;
                $scope.sanction_amount = resp.data.sanction_amount;
                $scope.sanctionto_name = resp.data.sanctionto_name;
                $scope.customer_urn = resp.data.customer_urn;
                $scope.application_no = resp.data.application_no;
                $scope.application_type = resp.data.application_type;
                $scope.contactperson_address = resp.data.contactperson_address;
                $scope.contactperson_name = resp.data.contactperson_name;
                $scope.contactperson_number = resp.data.contactperson_number;
                $scope.contactpersonemail_address = resp.data.contactpersonemail_address;
                $scope.sanctionfrom_date = resp.data.sanctionfrom_date;
                $scope.sanctiontill_date = resp.data.sanctiontill_date;
                $scope.paycard = resp.data.paycard;
                $scope.sanction_type = resp.data.sanction_type;
                $scope.natureof_proposal = resp.data.natureof_proposal;
                $scope.branch_name = resp.data.branch_name;
                $scope.esdeclaration_status = resp.data.esdeclaration_status;

            });

            $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };

            var url = 'api/MstApplicationReport/GetTemplateDetails';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lspath = resp.data.makerfile_path;
                $scope.lsname = resp.data.makerfile_name;
                $scope.content = resp.data.template_content;
                $scope.checkerletter_flag = resp.data.checkerletter_flag;
                $scope.checkerapproval_flag = resp.data.checkerapproval_flag;
                $scope.sanctionletter_flag = resp.data.sanctionletter_flag;
                $scope.sanctionletter_status = resp.data.sanctionletter_status;
                $scope.digitalsignature_flag = resp.data.digitalsignature_flag;
                $scope.checkerupdated_by = resp.data.checkerupdated_by;
                $scope.checkerupdated_on = resp.data.checkerupdated_on;
                $scope.makersubmitted_by = resp.data.makersubmitted_by;
                $scope.makersubmitted_on = resp.data.makersubmitted_on;
                $scope.approved_by = resp.data.approved_by;
                $scope.approved_date = resp.data.approved_date;
                unlockUI();


                $scope.download_show = true;


            });

            var url = 'api/MstApplicationReport/CADSanctionLetterSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.sanctiontocheckerlist = resp.data.reportsanctiondetails_list;
            });

            var url = 'api/MstApplicationReport/Getesdocument';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.UploadCADES_DocumentList = resp.data.UploadCADES_DocumentList;
            });

            var url = 'api/MstApplicationReport/GetMaildocument';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.DeviationCADMail_DocumentList = resp.data.DeviationCADMail_DocumentList;
            });


            $scope.downloadsCAM = function (val1, val2) {
                DownloaddocumentService.Downloaddocument(val1, val2);
            }

            $scope.downloaddocument = function (val1, val2) {
                DownloaddocumentService.Downloaddocument(val1, val2);
            }


            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstCADApplication/GetProductChargesDtl';

            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtoveralllimit_amt = resp.data.overalllimit_amount;
                $scope.txtvalidity_year = resp.data.validityoveralllimit_year;
                $scope.txtvalidity_month = resp.data.validityoveralllimit_month;
                $scope.txtvalidity_days = resp.data.validityoveralllimit_days;
                $scope.txtcalculation_limitvalidity = resp.data.calculationoveralllimit_validity;
                $scope.loandtls_list = resp.data.mstLoan_list;
                $scope.txtpurposeof_loan = resp.data.enduse_purpose;
                $scope.txt_processingfee = resp.data.processing_fee;
                $scope.txtprocessing_collecttype = resp.data.processing_collectiontype;
                $scope.txtdoc_charges = resp.data.doc_charges;
                $scope.txtdoc_collecttype = resp.data.doccharge_collectiontype;
                $scope.txtfield_visitcharges = resp.data.fieldvisit_charge;
                $scope.txtfield_collecttype = resp.data.fieldvisit_collectiontype;
                $scope.txtadhoc_fee = resp.data.adhoc_fee;
                $scope.txtadhoccollection_type = resp.data.adhoc_collectiontype;
                $scope.txtlife_insurance = resp.data.life_insurance;
                $scope.txtlife_collectiontype = resp.data.lifeinsurance_collectiontype;
                $scope.txtaccident_insurance = resp.data.acct_insurance;
                $scope.txttotal_collectible = resp.data.total_collect;
                $scope.txttotal_deductible = resp.data.total_deduct;
                $scope.Collateral_list = resp.data.mstcollateral_list;
                $scope.txtproduct_type = resp.data.product_type;
                $scope.servicecharge_List = resp.data.servicecharge_List;
                $scope.txtsecurity_type = resp.data.security_type;
                $scope.txtsecurity_description = resp.data.security_description;
                $scope.txtsecurity_value = resp.data.security_value;
                $scope.txtsecurityassessed_date = resp.data.securityassessed_date;
                $scope.txtasset_id = resp.data.asset_id;
                $scope.txtroc_fillingid = resp.data.roc_fillingid;
                $scope.txtCERSAI_fillingid = resp.data.CERSAI_fillingid;
                $scope.txthypoobservation_summary = resp.data.hypoobservation_summary;
                $scope.txtprimary_security = resp.data.primary_security;
                $scope.application2hypothecation_gid = resp.data.application2hypothecation_gid;
            });

            var url = 'api/MstCadFlow/GetBuyerList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.rmbuyer_list = resp.data.rmbuyer_list;
                $scope.creditbuyer_list = resp.data.creditbuyer_list;
            });

            var params = {
                application_gid: application_gid,
                application2sanction_gid: application2sanction_gid
            }
            var url = 'api/MstCAD/GetApp2SanctionLimitInfoDtl';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.productdetails = resp.data.limitandproducts;
            });

            var params = {
                application_gid: application_gid
            };

            var url = 'api/MstCAD/GetSanction';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.sanctionuploaddocument_list = resp.data.sanctiondocument_list;
            });

            // Lsa Details

            //$scope.lslsfilledlimitproduct = lslsfilledlimitproduct;
            $scope.tobe_recovered = false;
            $scope.already_recovered = false;
            $scope.yes = false;
            $scope.no = false;
            $scope.customer_pnl = true;
            $scope.sanction_pnl = true;
            $scope.signmatching = false;
            $scope.nach_no = false;
            $scope.signmatch_kycprovide = false;
            $scope.escrow_no = false;
            $scope.stamp = false;
            $scope.roc_no = false;
            $scope.cersai_no = false;
            $scope.panel1 = false;
            $scope.panel2 = false;
            $scope.panel3 = false;
            $scope.hidephotodiv = true;
            $scope.showphotodiv = false;

            var params = {
                application_gid: application_gid
            }

            var url = 'api/MstCAD/GetCADBasicView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtentity_gid = resp.data.entity_gid;
                $scope.txtentity_name = resp.data.entity_name;
                $scope.txtcreditapproved_date = resp.data.creditapproved_date;
                $scope.txtvertical_code = resp.data.vertical_code;
            });

            //Sanction Type Drop Down
            var url = 'api/MstCAD/GetCADBasicView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.sanctiontype_list = resp.data.sanctiontype_list;
            });

            var url = 'api/MstApplicationView/GetApplicationBasicView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtapplication_no = resp.data.application_no;
                $scope.txtbasiccustomer_name = resp.data.customer_name;
                $scope.txtbasicdesignation = resp.data.designation_type;
                $scope.txtvertical = resp.data.vertical_name;
                $scope.txtconstitution = resp.data.constitution_name;
                $scope.txtcredit_group = resp.data.creditgroup_name;
                //$scope.txtcontactperson_name = resp.data.contactperson_name;
                $scope.txtbusinessapproved_date = resp.data.headapproval_date;
                $scope.txtccapproved_date = resp.data.ccapproved_date;
                $scope.txtregion = resp.data.region;
            });

            // Get Credit Approval Hirerichy
            var url = 'api/MstCreditApproval/Getcreditheadsview';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtcredit_head = resp.data.credithead_name;
                $scope.txtnational_manager = resp.data.nationalcredit_name;
                $scope.txtregional_manager = resp.data.regionalcredit_name;
                $scope.txtcredit_manager = resp.data.creditmanager_name;
                $scope.txtcredit_group = resp.data.creditgroup_name;
                $scope.remarks = resp.data.remarks;
            });


            var param = {
                application_gid: application_gid
            }
            var url = 'api/MstApplicationAdd/GetLoanDtl';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mstloan_list = resp.data.mstloan_list;
                $scope.servicecharges_list = resp.data.servicecharges_list;
            });
           

            if (lsgeneratelsa_gid != "" && lsgeneratelsa_gid != undefined) {
                var params = {
                    generatelsa_gid: lsgeneratelsa_gid
                }
                var url = 'api/MstLSA/GetLimitInfoDtl';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.productdetails = resp.data.limitandproducts;
                        $scope.lbltotal_existinglimit = resp.data.total_existinglimit;
                        $scope.lbltotal_limitreleased = resp.data.total_limitreleased;
                        $scope.lblmaker_signature = resp.data.maker_name;
                    }
                });

                var params = {
                    generatelsa_gid: lsgeneratelsa_gid,

                };
                var url = 'api/MstLSA/GetLSAGeneraldocument';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.filename_list = resp.data.UploadLSADocumentList;
                });

                var url = 'api/MstLSA/GetlsaFeeschargesDetail';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.lsaFeecharges_list = resp.data.lsaFeecharges_list;
                    }
                });
                var url = 'api/MstLSA/Getcompliancecheckinfo';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.rdbnachmandateform_held = resp.data.nachmandateform_held,
                        $scope.txtnachmandateform_heldremarks = resp.data.nachmandateform_heldremarks,
                        $scope.rdbsignmatching_nachform = resp.data.signmatching_nachform,
                        $scope.txtsignmatching_nachformremarks = resp.data.signmatching_nachform,
                        $scope.rdbnamesign_kycmatching = resp.data.namesign_kycmatching,
                        $scope.txtnamesign_kycmatchingremarks = resp.data.namesign_kycmatchingremarks,
                        $scope.rdbescrowaccount_opened = resp.data.escrowaccount_opened,
                        $scope.txtescrowaccount_openedremarks = resp.data.escrowaccount_openedremarks,
                        $scope.rdbappropriate_stamping = resp.data.appropriate_stamping,
                        $scope.txtappropriate_stampingremarks = resp.data.appropriate_stampingremarks,
                        $scope.rdbrocfiling_initiated = resp.data.rocfiling_initiated,
                        $scope.txtrocfiling_initiatedremarks = resp.data.rocfiling_initiatedremarks,
                        $scope.rdbcersai_initiated = resp.data.cersai_initiated,
                        $scope.txtcersai_initiatedremarks = resp.data.cersai_initiatedremarks,
                        $scope.rdballdeferralcovenant_captured = resp.data.alldeferralcovenant_captured,
                        $scope.rdballpredisbursement_stipulated = resp.data.allpredisbursement_stipulated,
                        $scope.lblmaker_signature = resp.data.maker_signaturename;
                });
            }

            var params = {
                generatelsa_gid: lsgeneratelsa_gid
            }
            var url = 'api/MstLSA/GetLSABankAccountSummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lsabankaccsummary_list = resp.data.MdlBankAccount;
            });

            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstLSA/GetCreditBankAccountSummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.creditbankacc_list = resp.data.MdlBankAccount;
            });

            refreshcreditbankaccountsummary();
            refreshbankaccountsummary();
        }

        $scope.supplierdtl_view = function (val) {
            var disbursementsupplier_gid = val;
            var modalInstance = $modal.open({
                templateUrl: '/SupplierDtlView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    disbursementsupplier_gid: disbursementsupplier_gid
                }
                var url = 'api/MstCreditOpsApplication/GetDisbSupplierDtlView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblsupplier_name = resp.data.supplier_name;
                    $scope.lblifsc_code = resp.data.ifsc_code;
                    $scope.lblmicr_code = resp.data.micr_code;
                    $scope.lblbranch_address = resp.data.branch_address;
                    $scope.lblbank_name = resp.data.bank_name;
                    $scope.lblbranch_name = resp.data.branch_name;
                    $scope.lblbankaccount_number = resp.data.bankaccount_number;
                    $scope.lblconfirmbankaccount_number = resp.data.confirmbankaccount_number;
                    $scope.lblaccountholder_name = resp.data.accountholder_name;
                    $scope.lbldisbursement_amount = resp.data.disbursement_amount;
                    $scope.lblcreated_by = resp.data.created_by;
                    $scope.lblcreated_date = resp.data.created_date;
                    $scope.lblaccount_type = resp.data.bankaccounttype_name;
                    $scope.lbljoint_account = resp.data.jointaccount_status;
                    $scope.lbljointacctholder_namr = resp.data.jointaccountholder_name;
                    $scope.lblchequebook_status = resp.data.chequebook_status;
                    $scope.lblacctopen_date = resp.data.accountopen_date;
                });

                var url = 'api/MstCreditOpsApplication/DisbsupplierdocumentView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.disbsupplieruploaddocument_list = resp.data.disbsupplieruploaddocument_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.supplierview_downloads = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }

                $scope.downloadallview_supplierdoc = function () {
                    for (var i = 0; i < $scope.disbsupplieruploaddocument_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.disbsupplieruploaddocument_list[i].document_path, $scope.disbsupplieruploaddocument_list[i].document_name);
                    }
                }

                $scope.supplierview_documentviewer = function (val1, val2) {
                    lockUI();
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                    if (IsValidExtension == false) {
                        Notify.alert("View is not supported for this format..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        return false;
                    }
                    DownloaddocumentService.DocumentViewer(val1, val2);
                }

            }

        }

        function defaultamountwordschange(input) {
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords = cmnfunctionService.fnConvertNumbertoWord(str);
            return lswords;
        }

        function lsabanklist() {
            var application_gid = application_gid;
            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstCreditOpsApplication/GetLSADisbursementBankDtls';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lsabankaccount_list = resp.data.lsabankaccount_list;
            });
        }
        function creditbanklist() {
            var application_gid = application_gid;
            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstCreditOpsApplication/GetCreditDisbursementBankDtls';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.creditbankaccount_list = resp.data.creditbankaccount_list;
            });
        }

        $scope.farmerindividual_view = function (farmercontact_gid) {
            localStorage.setItem('farmercontact_gid', farmercontact_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/MstDisbIndividualFarmerDtlView";
            window.open(URL, '_blank');
        }

        $scope.coapplicantdtl_view = function (contactcoapplicant_gid) {
            localStorage.setItem('contactcoapplicant_gid', contactcoapplicant_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/MstDisbCoApplicantContactDtlView";
            window.open(URL, '_blank');
        }

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.disbursementuploaddocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.disbursementuploaddocument_list[i].document_path, $scope.disbursementuploaddocument_list[i].document_name);
            }
        }

        $scope.downloadall_disgeneral = function () {
            for (var i = 0; i < $scope.disbursementuploaddocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.disbursementuploaddocument_list[i].document_path, $scope.disbursementuploaddocument_list[i].document_name);
            }
        }

        $scope.Back = function () {
            $location.url('app/MstRMDisbursementRequest?application_gid=' + application_gid + '&customer_urn=' + customer_urn);
        }

        $scope.uploadeddoc_bankacctdtl = function (creditbankdtl_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/Bankacctdocuments.html',
                controller: ModalInstanceCtrl,
                size: 'lg',
                backdrop: 'static',
                keyboard: false
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                  {
                      creditbankdtl_gid: creditbankdtl_gid
                  }
                var url = 'api/MstAppCreditUnderWriting/GetCreditBankDocumentUpload';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.chequeleaf_list = resp.data.credituploaddocument_list;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.download_chequeleafdoc = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }
                $scope.downloadallchequedoc = function () {
                    for (var i = 0; i < $scope.chequeleaf_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.chequeleaf_list[i].chequeleaf_path, $scope.chequeleaf_list[i].chequeleaf_name);
                    }
                }

                $scope.documentviewer = function (val1, val2) {
                    lockUI();
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                    if (IsValidExtension == false) {
                        Notify.alert("View is not supported for this format..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        return false;
                    }
                    DownloaddocumentService.DocumentViewer(val1, val2);
                }

            }

        }

        $scope.lsauploadeddoc_bankacctdtl = function (lsabankaccdtl_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/LSABankacctdocuments.html',
                controller: ModalInstanceCtrl,
                size: 'lg',
                backdrop: 'static',
                keyboard: false
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                  {
                      lsabankaccdtl_gid: lsabankaccdtl_gid
                  }
                var url = 'api/MstCreditOpsApplication/GetLSABankDocumentUpload';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lsauploadeddocument_list = resp.data.lsauploadeddocument_list;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.download_chequeleafdoc = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }
                $scope.downloadallfile = function () {
                    for (var i = 0; i < $scope.lsauploadeddocument_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.lsauploadeddocument_list[i].document_path, $scope.lsauploadeddocument_list[i].document_name);
                    }
                }
                $scope.documentviewer = function (val1, val2) {
                    lockUI();
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                    if (IsValidExtension == false) {
                        Notify.alert("View is not supported for this format..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        return false;
                    }
                    DownloaddocumentService.DocumentViewer(val1, val2);
                }

            }

        }

        $scope.documentviewer = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }

        $scope.creditdisbursement_acct = function (creditbankdtl_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/CreditBankacctconfirm.html',
                controller: ModalInstanceCtrl,
                size: 'lg',
                backdrop: 'static',
                keyboard: false
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var application_gid = $location.search().application_gid;

                $scope.confirm_Submit = function () {
                    var params = {
                        application_gid: application_gid,
                        bankdtl_gid: creditbankdtl_gid,
                        disbursement_status: $scope.rdbdisbursement_status
                    }
                    var url = 'api/MstCreditOpsApplication/PostConfirmDisbursementAcct';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            creditbanklist();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }

                    });

                }
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }

        }

        $scope.lsadisbursement_acct = function (lsabankaccdtl_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/LsaBankacctconfirm.html',
                controller: ModalInstanceCtrl,
                size: 'lg',
                backdrop: 'static',
                keyboard: false
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var application_gid = $location.search().application_gid;

                $scope.confirm_Submit = function () {
                    var params = {
                        application_gid: application_gid,
                        bankdtl_gid: lsabankaccdtl_gid,
                        disbursement_status: $scope.rdbdisbursement_status
                    }
                    var url = 'api/MstCreditOpsApplication/PostConfirmDisbursementAcct';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            lsabanklist();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }

                    });

                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }

        }

        $scope.documentviewer = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }
        $scope.PurposeofLoanOther_view = function (application2loan_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/PurposeOfLoanOther.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                {
                    application2loan_gid: application2loan_gid
                }
                var url = 'api/MstApplicationView/GetPurposeofLoan';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtpurposeof_loan = resp.data.enduse_purpose;

                });
                var url = 'api/MstApplicationView/GetLoanProgramValueChain';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.program = resp.data.program;
                    $scope.primaryvaluechain_name = resp.data.primaryvaluechain_name;
                    $scope.secondaryvaluechain_name = resp.data.secondaryvaluechain_name;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            }



        }


        $scope.downloadallsanction = function () {
            for (var i = 0; i < $scope.sanctionuploaddocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.sanctionuploaddocument_list[i].document_path, $scope.sanctionuploaddocument_list[i].document_name);
            }
        }

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.DeviationCADMail_DocumentList.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.DeviationCADMail_DocumentList[i].document_path, $scope.DeviationCADMail_DocumentList[i].document_name);
            }
        }
        $scope.downloadallESdoc = function () {
            for (var i = 0; i < $scope.UploadCADES_DocumentList.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.UploadCADES_DocumentList[i].document_path, $scope.UploadCADES_DocumentList[i].document_name);
            }
        }

        $scope.documentviewer = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }

        function refreshservicechargedetails() {
            var params = {
                generatelsa_gid: lsgeneratelsa_gid,

            };
            var url = 'api/MstLSA/GetlsaFeeschargesDetail';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.lsaFeecharges_list = resp.data.lsaFeecharges_list;
                }
            });
        }

        $scope.LSApdf = function () {
            var params = {
                generatelsa_gid: lsgeneratelsa_gid
            }
            var url = 'api/MstLSA/GetLSApdf';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {

                var phyPath = resp.data;
                //var relPath = phyPath.split("EMS");
                //var relpath1 = relPath[1].replace("\\", "/");
                //var hosts = window.location.host;
                //var prefix = location.protocol + "//";
                //var str = prefix.concat(hosts, relpath1);
                //var link = document.createElement("a");
                //link.download = "LSA Report";
                //var uri = str;
                //link.href = uri;
                //link.click();
                //DownloaddocumentService.Downloaddocument(val1, val2);


                var phyPath = phyPath.replace("\\", "/");;
                var relPath = phyPath.split("EMS/");
                var relpath1 = relPath[1].replace("\\", "/");
                var url1 = filename1;
                var filename = url1.substring(url1.lastIndexOf('/') + 1);



                var url = 'api/azurestorage/FileUploadDocument';
                var params = {
                    file_path: relpath1
                }
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        DownloaddocumentService.Downloaddocument(relpath1, filename);
                        Notify.alert('LSA Report Downloaded Successfully', 'success')
                        unlockUI();
                    }
                    else {
                        unlockUI();
                        Notify.alert('Error Occurred While Export PDF !', 'warning');
                    }
                });
            });

        }

        function refreshproductdetails() {
            if (lsgeneratelsa_gid != "" && lsgeneratelsa_gid != undefined) {
                var params = {
                    generatelsa_gid: lsgeneratelsa_gid
                }
                var url = 'api/MstLSA/GetLimitInfoDtl';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.productdetails = resp.data.limitandproducts;
                        $scope.lbltotal_existinglimit = resp.data.total_existinglimit;
                        $scope.lbltotal_limitreleased = resp.data.total_limitreleased;
                    }
                });
            }
            else {
                lockUI();
                var params = {
                    application_gid: application_gid,
                    application2sanction_gid: application2sanction_gid
                }
                var url = 'api/MstLSA/GetLSAApplicationLimitInfo';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.productdetails = resp.data.limitandproducts;
                        $scope.lbltotal_existinglimit = resp.data.total_existinglimit;
                        $scope.lbltotal_limitreleased = resp.data.total_limitreleased;
                    }
                });
            }
        }

        $scope.remarks_view = function (limitinfo_remarks) {
            var modalInstance = $modal.open({
                templateUrl: '/Remarksproduct.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.lbllimitinfo_remarks = limitinfo_remarks;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.PurposeofLoanOther_view = function (application2loan_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/PurposeOfLoanOther.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                {
                    application2loan_gid: application2loan_gid
                }
                var url = 'api/MstApplicationView/GetPurposeofLoan';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtpurposeof_loan = resp.data.enduse_purpose;

                });
                var url = 'api/MstApplicationView/GetLoanProgramValueChain';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.program = resp.data.program;
                    $scope.primaryvaluechain_name = resp.data.primaryvaluechain_name;
                    $scope.secondaryvaluechain_name = resp.data.secondaryvaluechain_name;
                });

                var url = 'api/MstApplicationView/GetLoanProgramValueChain';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.program = resp.data.program;
                    $scope.primaryvaluechain_name = resp.data.primaryvaluechain_name;
                    $scope.secondaryvaluechain_name = resp.data.secondaryvaluechain_name;
                    $scope.product_gid = resp.data.product_gid;
                    $scope.product_name = resp.data.product_name;
                    $scope.variety_gid = resp.data.variety_gid;
                    $scope.variety_name = resp.data.variety_name;
                    $scope.sector_name = resp.data.sector_name;
                    $scope.category_name = resp.data.category_name;
                    $scope.botanical_name = resp.data.botanical_name;
                    $scope.alternative_name = resp.data.alternative_name;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            }

        }


        function refreshbankaccountsummary() {
            lockUI();
            var params = {
                generatelsa_gid: lsgeneratelsa_gid
            }
            var url = 'api/MstLSA/GetLSABankAccountSummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lsabankaccsummary_list = resp.data.MdlBankAccount;
            });
        }

        function refreshcreditbankaccountsummary() {
            lockUI();
            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstLSA/GetCreditBankAccountSummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.creditbankacc_list = resp.data.MdlBankAccount;
            });
        }

        function refreshLSAchequeleafDocument(lsabankaccdtl_gid) {
            lockUI();
            var params = {
                lsabankaccdtl_gid: lsabankaccdtl_gid
            }
            var url = 'api/MstLSA/GetLSAchequeleafDocument';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.LSAchequeleafDocument_list = resp.data.lsauploaddocument_list;
            });
        }
        $scope.downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);

        }
        $scope.view_observationsummary = function (collateralobservation_summary) {
            var modalInstance = $modal.open({
                templateUrl: '/ObservationSummary.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.txtobservation_summary = collateralobservation_summary;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            }

        }

        $scope.viewservicechargesdetails = function (lsachargestype_gid, charge_typeid) {
            var modalInstance = $modal.open({
                templateUrl: '/DocumentChargesView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                //lockUI();
                //var url = "api/MstLSA/GetlsachargesDetail";
                //SocketService.get(url).then(function (resp) {
                //    unlockUI();
                //    $scope.BankNamelist = resp.data.MdlBankName;
                //});

                lockUI();
                var params = {
                    lsachargestype_gid: lsachargestype_gid,
                    charge_type: charge_typeid
                }
                var url = "api/MstLSA/GetlsachargesDetail";
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.chargesdtl = resp.data;
                });

                //$scope.calender8 = function ($event) {
                //    $event.preventDefault();
                //    $event.stopPropagation();
                //    $scope.open8 = true;
                //};

                //$scope.dateOptions = {
                //    formatYear: 'yy',
                //    startingDay: 1
                //};

                //$scope.formats = ['dd-MM-yyyy'];
                //$scope.format = $scope.formats[0];

                $scope.already_recovered = function () {
                    $scope.alreadyrecovered_show = true;
                    $scope.toberecovered_show = false;
                }

                $scope.tobe_recovered = function () {
                    $scope.alreadyrecovered_show = false;
                    $scope.toberecovered_show = true;
                }

                //$scope.recoveredamountchange = function () {
                //    var input = document.getElementById('RecoveredAmount').value;
                //    var str = input.replace(/,/g, '');
                //    var output = Number(str).toLocaleString('en-IN');
                //    var lsrecovered_amt = inWords8(str);
                //    if (output == "NaN") {
                //        Notify.alert('Accept Number Format Only..!', {
                //            status: 'danger',
                //            pos: 'top-center',
                //            timeout: 3000
                //        });
                //        $scope.txtrecovered_amount = "";
                //    }
                //    else {
                //        $scope.txtrecovered_amount = output;
                //        document.getElementById('recoveredamount_words').innerHTML = lsrecovered_amt;
                //    }
                //}

                //function inWords8(num) {
                //    var a = ['', 'One ', 'Two ', 'Three ', 'Four ', 'Five ', 'Six ', 'Seven ', 'Eight ', 'Nine ', 'Ten ', 'Eleven ', 'Twelve ', 'Thirteen ', 'Fourteen ', 'Fifteen ', 'Sixteen ', 'Seventeen ', 'Eighteen ', 'Nineteen '];
                //    var b = ['', '', 'Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];
                //    var s = num.toString();
                //    s = s.replace(/[\, ]/g, '');
                //    if (s != parseFloat(s)) return '';
                //    if ((num = num.toString()).length > 9) return 'Overflow';
                //    var n = ('000000000' + num).substr(-9).match(/^(\d{2})(\d{2})(\d{2})(\d{1})(\d{2})$/);
                //    if (!n) return; var str = '';
                //    str += (n[1] != 0) ? (a[Number(n[1])] || b[n[1][0]] + ' ' + a[n[1][1]]) + 'Crore ' : '';
                //    str += (n[2] != 0) ? (a[Number(n[2])] || b[n[2][0]] + ' ' + a[n[2][1]]) + 'Lakh ' : '';
                //    str += (n[3] != 0) ? (a[Number(n[3])] || b[n[3][0]] + ' ' + a[n[3][1]]) + 'Thousand ' : '';
                //    str += (n[4] != 0) ? (a[Number(n[4])] || b[n[4][0]] + ' ' + a[n[4][1]]) + 'Hundred ' : '';

                //    str += (n[5] != 0) ? ((str != '') ? 'and ' : '') + (a[Number(n[5])] || b[n[5][0]] + ' ' + a[n[5][1]]) + 'only ' : '';
                //    return str;
                //}

                //$scope.recoveredamountchange1 = function () {
                //    var input = document.getElementById('RecoveredAmount1').value;
                //    var str = input.replace(/,/g, '');
                //    var output = Number(str).toLocaleString('en-IN');
                //    var lsrecovered_amount1 = inWords9(str);
                //    if (output == "NaN") {
                //        Notify.alert('Accept Number Format Only..!', {
                //            status: 'danger',
                //            pos: 'top-center',
                //            timeout: 3000
                //        });
                //        $scope.txtrecovered_amount1 = "";
                //    }
                //    else {
                //        $scope.txtrecovered_amount1 = output;
                //        document.getElementById('recoveredamount1_words').innerHTML = lsrecovered_amount1;
                //    }
                //}

                //function inWords9(num) {
                //    var a = ['', 'One ', 'Two ', 'Three ', 'Four ', 'Five ', 'Six ', 'Seven ', 'Eight ', 'Nine ', 'Ten ', 'Eleven ', 'Twelve ', 'Thirteen ', 'Fourteen ', 'Fifteen ', 'Sixteen ', 'Seventeen ', 'Eighteen ', 'Nineteen '];
                //    var b = ['', '', 'Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];
                //    var s = num.toString();
                //    s = s.replace(/[\, ]/g, '');
                //    if (s != parseFloat(s)) return '';
                //    if ((num = num.toString()).length > 9) return 'Overflow';
                //    var n = ('000000000' + num).substr(-9).match(/^(\d{2})(\d{2})(\d{2})(\d{1})(\d{2})$/);
                //    if (!n) return; var str = '';
                //    str += (n[1] != 0) ? (a[Number(n[1])] || b[n[1][0]] + ' ' + a[n[1][1]]) + 'Crore ' : '';
                //    str += (n[2] != 0) ? (a[Number(n[2])] || b[n[2][0]] + ' ' + a[n[2][1]]) + 'Lakh ' : '';
                //    str += (n[3] != 0) ? (a[Number(n[3])] || b[n[3][0]] + ' ' + a[n[3][1]]) + 'Thousand ' : '';
                //    str += (n[4] != 0) ? (a[Number(n[4])] || b[n[4][0]] + ' ' + a[n[4][1]]) + 'Hundred ' : '';

                //    str += (n[5] != 0) ? ((str != '') ? 'and ' : '') + (a[Number(n[5])] || b[n[5][0]] + ' ' + a[n[5][1]]) + 'only ' : '';
                //    return str;
                //}

                //$scope.processingfees_submit = function () {
                //    var params = {
                //        application_gid: application_gid,
                //        recovered_status: $scope.rdbrecovered_type,
                //        recovered_amount: $scope.txtrecovered_amount,
                //        Chequeno_details: $scope.txtprocessingfeescheque_no,
                //        Cheque_date: $scope.txtprocessingfeescheque_date,
                //        processingfees_remarks: $scope.txtalprocessingfees_remarks,
                //        bank_namegid: $scope.cboprocessingfeeschequebank_name,
                //        bank_name: $scope.cboprocessingfeeschequebank_name,
                //    }

                //    lockUI();
                //    var url = 'api/MstLSA/PostProcessingFee';
                //    SocketService.post(url, params).then(function (resp) {
                //        if (resp.data.status == true) {
                //            Notify.alert(resp.data.message, {
                //                status: 'success',
                //                pos: 'top-center',
                //                timeout: 3000
                //            });
                //            unlockUI();
                //            $modalInstance.close('closed');
                //        }
                //        else {
                //            $modalInstance.close('closed');
                //            Notify.alert(resp.data.message, {
                //                status: 'danger',
                //                pos: 'top-center',
                //                timeout: 3000
                //            });
                //            unlockUI();
                //        }
                //    });
                //}

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }


        $scope.view_uploadeddoc = function (application2loan_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/DocumentDetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    application2loan_gid: application2loan_gid
                }
                var url = 'api/MstLSA/GetLSACollateraldocument';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.document_list = resp.data.UploadLSADocumentList;
                });

                $scope.downloadcollateraldoc = function (val1, val2, val3) {
                    //var phyPath = val1;
                    //var relPath = phyPath.split("StoryboardAPI");
                    //var relpath1 = relPath[1].replace("\\", "/");
                    //var hosts = window.location.host;
                    //var prefix = location.protocol + "//";
                    //var str = prefix.concat(hosts, relpath1);
                    //var link = document.createElement("a");
                    //link.download = val2;
                    //var uri = str;
                    //link.href = uri;
                    //link.click();
                    if (val3 == 'N')
                        DownloaddocumentService.Downloaddocument(val1, val2);
                    else
                        DownloaddocumentService.OtherDownloaddocument(val1, val2, val3);

                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.downloadall_collateral = function () {
                    for (var i = 0; i < $scope.document_list.length; i++) {
                        if ($scope.document_list[i].migration_flag == 'N')
                            DownloaddocumentService.Downloaddocument($scope.document_list[i].document_path, $scope.document_list[i].document_name);
                        else
                            DownloaddocumentService.OtherDownloaddocument($scope.document_list[i].document_path, $scope.document_list[i].document_name, $scope.document_list[i].migration_flag);
                    }
                }

            }

        }

        $scope.Approved_submit = function () {
            var params = {
                generatelsa_gid: lsgeneratelsa_gid,
            }
            lockUI();
            var url = 'api/MstLSA/PostLSAApproved';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $location.url('app/MstCadLSAApprovalSummary');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }
            });
        }
        $scope.bankacct_view = function (primarygid, LSABankaccount) {
            var modalInstance = $modal.open({
                templateUrl: '/bankaccount_view.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var lsabankaccdtl_gid = "", creditbankdtl_gid = "";
                if (LSABankaccount == 'Y') {
                    var params = {
                        lsabankaccdtl_gid: primarygid
                    }
                    var url = 'api/MstLSA/GetLSABankAccountdetail';
                }
                else {
                    var params = {
                        creditbankdtl_gid: primarygid
                    }
                    var url = 'api/MstLSA/GetCreditBankAccountdetail';
                }
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI();
                        $scope.viewinfo = resp.data;
                        $scope.uploaddocument_list = resp.data.credituploaddocument_list;
                    }
                    else {
                        unlockUI();
                    }

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.downloads = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }

                $scope.documentviewer = function (val1, val2) {
                    lockUI();
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                    if (IsValidExtension == false) {
                        Notify.alert("View is not supported for this format..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        return false;
                    }
                    DownloaddocumentService.DocumentViewer(val1, val2);
                }
                $scope.downloadallche = function () {
                    for (var i = 0; i < $scope.uploaddocument_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.uploaddocument_list[i].chequeleaf_path, $scope.uploaddocument_list[i].chequeleaf_name);
                    }
                }
            }
        }

        $scope.bankacctdtl_documentviewer = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }

        $scope.bankacctdtl_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.downloadall_bankacctdtl = function () {
            for (var i = 0; i < $scope.disbsupplieruploaddocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.disbsupplieruploaddocument_list[i].document_path, $scope.disbsupplieruploaddocument_list[i].document_name);
            }
        }

        $scope.downloadall_generaldoc = function () {
            for (var i = 0; i < $scope.filename_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.filename_list[i].document_path, $scope.filename_list[i].document_name);
            }
        }

    }
})();
