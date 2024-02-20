(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstApprovedDisbursementViewController', MstApprovedDisbursementViewController);

    MstApprovedDisbursementViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','cmnfunctionService','DownloaddocumentService'];

    function MstApprovedDisbursementViewController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService,DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstApprovedDisbursementViewController';

        var application_gid = $location.search().application_gid;
        var application2sanction_gid = $location.search().application2sanction_gid;
        var application2loan_gid = $location.search().application2loan_gid;
        var lspage = $location.search().lspage;
        //var customer_urn = $location.search().customer_urn;
        $scope.customer_urn = $location.search().customer_urn;
        var customer_urn = $scope.customer_urn;
        var rmdisbursementrequest_gid = $location.search().rmdisbursementrequest_gid;
        var lsgeneratelsa_gid = $location.search().lsareference_gid;
        $scope.application_gid = $location.search().application_gid;
        $scope.rmdisbursementrequest_gid = $location.search().rmdisbursementrequest_gid;

        activate();

        lockUI();
        function activate() {

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

                $scope.txtsampledispprocessing_fees = resp.data.processing_fees;
                $scope.txtdispprocessing_fees = (parseInt($scope.txtsampledispprocessing_fees.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountseperator = (parseInt($scope.txtdispprocessing_fees.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                document.getElementById('words_totalamount56').innerHTML = $scope.lblamountwords;

                $scope.txtdispprocessinggst = resp.data.gst;

                $scope.txtsampledispgstprocessing_fees = resp.data.dispgstprocessing_fees;
                $scope.txtdispgstprocessing_fees = (parseInt($scope.txtsampledispgstprocessing_fees.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountseperator = (parseInt($scope.txtdispgstprocessing_fees.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                document.getElementById('documentchargerecoverd_words').innerHTML = $scope.lblamountwords;

                $scope.txtsampledisbadditional_charges = resp.data.finance_charges;
                $scope.txtdisbadditional_charges = (parseInt($scope.txtsampledisbadditional_charges.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountseperator = (parseInt($scope.txtdisbadditional_charges.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                document.getElementById('words_totalamount52').innerHTML = $scope.lblamountwords;

                $scope.txtdisbadditionalgst = resp.data.additionalcharges_gst;

                $scope.txtsamplegstadditionfees_charges = resp.data.dispgstadditionfees_charges;
                $scope.txtgstadditionfees_charges = (parseInt($scope.txtsamplegstadditionfees_charges.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountseperator = (parseInt($scope.txtgstadditionfees_charges.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                document.getElementById('additionalfinancecharges_words').innerHTML = $scope.lblamountwords;

                $scope.txtsampledispod_amount = resp.data.od_amount;
                $scope.txtdispod_amount = (parseInt($scope.txtsampledispod_amount.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountseperator = (parseInt($scope.txtdispod_amount.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                document.getElementById('words_totalamount54').innerHTML = $scope.lblamountwords;

                $scope.rdbdisbpayment_escrow = resp.data.escrow_payment;
                $scope.rdbdisbnach_status = resp.data.nach_status;
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


            // Get Overall Limit
            var url = 'api/MstCADFlow/GetProductChargesDtl';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lblSanctionAmount = resp.data.overalllimit_amount;
            });
            //var url = 'api/MstCADFlow/BankAccountDetailsList';
            //SocketService.getparams(url, params).then(function (resp) {
            //    unlockUI();
            //    $scope.bankaccount_list = resp.data.bankaccount_list;
            //});
            var url = 'api/MstCADFlow/GetCADBasicView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lblentity_gid = resp.data.entity_gid;
                $scope.lblentity_name = resp.data.entity_name;
                $scope.lblcreditapproved_date = resp.data.creditapproved_date;
                $scope.lblvertical_code = resp.data.vertical_code;
            });

            var params = {
                customer_urn: customer_urn
            }
            var url = 'api/MstCreditOpsApplication/GetSanctionRefnoDropDown';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.sanctionrefnolist = resp.data.sanctionrefnolist;
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
            var url = "api/MstCADFlow/GetlsaProductname";
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.loanproductlist = resp.data.LsaProductname;
            });

            var params = {
                rmdisbursementrequest_gid: rmdisbursementrequest_gid
            }

            var url = 'api/MstCreditOpsApplication/GetDisbursementSupplierSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.disbsupplierdtl_list = resp.data.disbsupplierdtl_list;
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

            // Sanction Details

            var application_gid = $location.search().application_gid;
            var application2sanction_gid = $location.search().application2sanction_gid;

            var params = {
                application2sanction_gid: application2sanction_gid
            }
            lockUI();
            var url = 'api/MstCreditOpsApplication/GetLSARefNoDropDown';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.LSARefNoDropDown_list = resp.data.LSARefNoDropDown_list;
            });

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
            // Get Overall Limit
            var url = 'api/MstCADFlow/GetProductChargesDtl';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lblSanctionAmount = resp.data.overalllimit_amount;
                $scope.lblsanction_amounteperator = (parseInt($scope.lblSanctionAmount.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblsanctionamount_incomewords = defaultamountwordschange($scope.lblsanction_amounteperator);
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


            var url = 'api/MstCreditOpsApplication/GetRMDisbursementTempClear';
            SocketService.get(url).then(function (resp) {
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
                $scope.sanction_refno = resp.data.sanction_refno;
                $scope.lblsanction_date = resp.data.sanction_date;
                $scope.lblsanctionfrom_date = resp.data.sanctionfrom_date;
                $scope.lblsanctiontill_date = resp.data.sanctiontill_date;
            });

            var application2sanction_gid = application2sanction_gid;

            var url = 'api/MstApplicationReport/CADSanctionDtls';
            var params = {
                application2sanction_gid: application2sanction_gid
            };
            lockUI();
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

            var params = {
                rmdisbursementrequest_gid: rmdisbursementrequest_gid
            }

            var url = 'api/MstCreditOpsApplication/GetDisbFarmerIndividualCreditOps';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.farmerindividualsummary_list = resp.data.farmerindividualsummary_list;
                $scope.batchencorefindcust_status = resp.data.batchencorefindcust_status;

            });


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
                $scope.lblencore_accountid = resp.data.encore_accountid;
                $scope.lblencoreintegration_status = resp.data.encoreintegration_status;
                $scope.lbldisbursementbookingencore_status = resp.data.disbursementbookingencore_status;

                
            });
        }

        $scope.downloads = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("StoryboardAPI");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            link.download = val2;
            var uri = str;
            link.href = uri;
            link.click();
        }

        $scope.Back = function () {
            if (lspage == 'followupmaker') {
                $location.url('app/MstMyDisbursementSummary');
            }
            else if (lspage == 'MyDisbursementCompleted') {
                $location.url('app/MstMyDisbursementCompletedSummary');
            }
            else if (lspage == 'ApprovedDisbursement') {
                $location.url('app/MstApprovedDisbursementSummary');
            }
            else if (lspage == 'MyDisbursementRejected') {
                $location.url('app/MstCreditOpsDisbursementRejectedSummary');
            }
            else if (lspage == 'MyDisbursementRM') {
                $location.url('app/MstRMDisbursementRequest?customer_urn=' + $location.search().customer_urn);
            }
            else {

            }

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
                    var phyPath = val1;
                    var relPath = phyPath.split("EMS");
                    var relpath1 = relPath[1].replace("\\", "/");
                    var hosts = window.location.host;
                    var prefix = location.protocol + "//";
                    var str = prefix.concat(hosts, relpath1);
                    var link = document.createElement("a");
                    link.download = val2;
                    var uri = str;
                    link.href = uri;
                    link.click();
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

        $scope.documentUpload = function (val) {
            var application_gid = $location.search().application_gid;

            if (($scope.txtdocument_title == null) || ($scope.txtdocument_title == '') || ($scope.txtdocument_title == undefined)) {
                $("#fileDocument").val('');
                Notify.alert('Kindly Enter the Document Title', 'warning');
            } else {
                var frm = new FormData();
                for (var i = 0; i < val.length; i++) {
                    var item = {
                        name: val[i].name,
                        file: val[i]
                    };
                    frm.append('fileupload', item.file);
                    frm.append('file_name', item.name);
                    frm.append('document_title', $scope.txtdocument_title);
                    frm.append('application_gid', application_gid);
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[i].name, "documentformatonly");
                    if (IsValidExtension == false) {
                        Notify.alert("File format is not supported..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });

                        return false;
                    }
                }

                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    lockUI();
                    var url = 'api/MstCreditOpsApplication/RMDisbursementDocumentUpload';
                    console.log(url)
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        $scope.disbursementuploaddocument_list = resp.data.disbursementuploaddocument_list;
                        unlockUI();

                        $("#chequefilefile").val('');
                        $scope.uploadfrm = undefined;

                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $scope.txtdocument_title = '';
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        unlockUI();
                    });
                }
                else {
                    alert('Document is not Available..!');
                    return;
                }
            }
        }
        $scope.downloads = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("StoryboardAPI");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            link.download = val2;
            var uri = str;
            link.href = uri;
            link.click();
        }
       $scope.downloadsfollow = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
      $scope.downloadall = function () {
            for (var i = 0; i < $scope.disbursementuploaddocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.disbursementuploaddocument_list[i].document_path, $scope.disbursementuploaddocument_list[i].document_name);
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

        $scope.uploaddocumentcancel = function (rmdisbursementdocument_gid, application_gid) {
            lockUI();
            var params = {
                rmdisbursementdocument_gid: rmdisbursementdocument_gid,
                application_gid: application_gid
            }
            var url = 'api/MstCreditOpsApplication/DeleteRMDisbursementDocument';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.disbursementuploaddocument_list = resp.data.disbursementuploaddocument_list;
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                unlockUI();
            });
        }

        $scope.approve = function () {

            var sanctionrefno_Name = $('#sanctionrefnoid :selected').text();
            var product_Name = $('#productsubproduct_name :selected').text();
            var application_gid = $location.search().application_gid;

            var params = {
                application_gid: application_gid,
                application2sanction_gid: $scope.cbosanctionref_no,
                sanction_refno: sanctionrefno_Name,
                application2loan_gid: $scope.cboproductsubproduct_name,
                product_type: product_Name,
                processing_fees: $scope.txtprocessing_fees,
                gst: $scope.txtgst,
                finance_charges: $scope.txtadditional_charges,
                od_amount: $scope.txtod_amount,
                escrow_payment: $scope.rdbpayment_escrow,
                nach_status: $scope.rdbnach_status,
                remarks: $scope.txtremarks,
                updated_person: 'Checker'
            }

            var url = "api/MstCreditOpsApplication/PostDisbursementApprove";
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status = true) {
                    Notify.alert(resp.data.message, 'success');
                    $location.url('app/MstMyDisbursementCheckerSummary');
                }
                else {
                    Notify.alert(resp.data.message, 'warning');
                    activate();
                }
            });
        }

        $scope.completed_process = function (application_gid, application2sanction_gid, application2loan_gid) {
            $location.url('app/MstApprovedDisbursementView?application_gid=' + application_gid + '&application2sanction_gid=' + application2sanction_gid + '&application2loan_gid=' + application2loan_gid);
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
                $scope.download_chequeleafdoc = function (val1, val2) {
                    var phyPath = val1;
                    var relPath = phyPath.split("EMS");
                    var relpath1 = relPath[1].replace("\\", "/");
                    var hosts = window.location.host;
                    var prefix = location.protocol + "//";
                    var str = prefix.concat(hosts, relpath1);
                    var link = document.createElement("a");
                    link.download = val2;
                    var uri = str;
                    link.href = uri;
                    link.click();
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
                $scope.download_chequeleafdoc = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }

            }

        }

        $scope.documentUpload = function (val) {
            var application_gid = $location.search().application_gid;

            if (($scope.txtdocument_title == null) || ($scope.txtdocument_title == '') || ($scope.txtdocument_title == undefined)) {
                $("#fileDocument").val('');
                Notify.alert('Kindly Enter the Document Title', 'warning');
            } else {
                var frm = new FormData();
                for (var i = 0; i < val.length; i++) {
                    var item = {
                        name: val[i].name,
                        file: val[i]
                    };
                    frm.append('fileupload', item.file);
                    frm.append('file_name', item.name);
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[i].name, "documentformatonly");
                    if (IsValidExtension == false) {
                        Notify.alert("File format is not supported..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });

                        return false;
                    }
                }
                frm.append('document_title', $scope.txtdocument_title);
                frm.append('application_gid', application_gid);
                frm.append('project_flag', "Default");
                frm.append('rmdisbursementrequest_gid', rmdisbursementrequest_gid);
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    lockUI();
                    var url = 'api/MstCreditOpsApplication/RMDisbursementDocumentUpload';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        $scope.disbursementuploaddocument_list = resp.data.disbursementuploaddocument_list;
                        unlockUI();

                        $("#chequefilefile").val('');
                        $scope.uploadfrm = undefined;

                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $scope.txtdocument_title = '';
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        unlockUI();
                    });
                }
                else {
                    alert('Document is not Available..!');
                    return;
                }
            }
        }
        $scope.downloads = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("StoryboardAPI");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            link.download = val2;
            var uri = str;
            link.href = uri;
            link.click();
        }
        $scope.downloadsmaker = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.downloadall = function () {
            for (var i = 0; i < $scope.disbursementuploaddocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.disbursementuploaddocument_list[i].document_path, $scope.disbursementuploaddocument_list[i].document_name);
            }
        }
        $scope.uploaddocumentcancel = function (rmdisbursementdocument_gid, application_gid) {
            lockUI();
            var params = {
                rmdisbursementdocument_gid: rmdisbursementdocument_gid,
                rmdisbursementrequest_gid: rmdisbursementrequest_gid
            }
            var url = 'api/MstCreditOpsApplication/DeleteRMDisbursementDocument';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.disbursementuploaddocument_list = resp.data.disbursementuploaddocument_list;
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                unlockUI();
            });
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

        $scope.txtamountchange4 = function () {
            var input = document.getElementById('Disbursement_Od_amount').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var words_totalamount54 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtdispod_amount = "";
            }
            else {
                //   $scope.txtprocessing_fee = output;
                document.getElementById('words_totalamount54').innerHTML = words_totalamount54;
                var txtdispod_amount = parseInt($scope.txtdispod_amount.replace(/[\s,]+/g, '').trim());
            }
            $scope.txtdispod_amount = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
        }

        $scope.txtamountchange2 = function () {
            var input = document.getElementById('Disbursement_Additional_charges').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount52 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtdisbadditional_charges = "";
            }
            else {
                //   $scope.txtprocessing_fee = output;
                document.getElementById('words_totalamount52').innerHTML = lswords_totalamount52;
                var txtdisbadditional_charges = parseInt($scope.txtdisbadditional_charges.replace(/[\s,]+/g, '').trim());
            }
            $scope.txtdisbadditional_charges = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
        }

        $scope.txtamountchange6 = function () {
            var input = document.getElementById('Processing_Fees_amount').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount52 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtdispprocessing_fees = "";
            }
            else {
                //   $scope.txtprocessing_fee = output;
                document.getElementById('words_totalamount56').innerHTML = lswords_totalamount52;
                var txtdispprocessing_fees = parseInt($scope.txtdispprocessing_fees.replace(/[\s,]+/g, '').trim());
            }
            $scope.txtdispprocessing_fees = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
        }

        $scope.amountschange10 = function () {
            var amount = (($scope.txtdispprocessing_fees.replace(/[\s,]+/g, '').trim()) * ($scope.txtdispprocessinggst) / 100);
            var doc = ($scope.txtdispprocessing_fees.replace(/[\s,]+/g, '').trim());
            var output = (parseInt(amount) + parseInt(doc));
            var lsdocumentchargerecoverd_words = inWords(output);
            document.getElementById('documentchargerecoverd_words').innerHTML = lsdocumentchargerecoverd_words;
            $scope.txtdispgstprocessing_fees = new Intl.NumberFormat('en-IN').format(Number(output));

        }

        function inWords(num) {
            var a = ['', 'One ', 'Two ', 'Three ', 'Four ', 'Five ', 'Six ', 'Seven ', 'Eight ', 'Nine ', 'Ten ', 'Eleven ', 'Twelve ', 'Thirteen ', 'Fourteen ', 'Fifteen ', 'Sixteen ', 'Seventeen ', 'Eighteen ', 'Nineteen '];
            var b = ['', '', 'Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];
            var s = num.toString();
            s = s.replace(/[\, ]/g, '');
            if (s != parseFloat(s)) return '';
            if ((num = num.toString()).length > 9) return 'Overflow';
            var n = ('000000000' + num).substr(-9).match(/^(\d{2})(\d{2})(\d{2})(\d{1})(\d{2})$/);
            if (!n) return; var str = '';
            str += (n[1] != 0) ? (a[Number(n[1])] || b[n[1][0]] + ' ' + a[n[1][1]]) + 'Crore ' : '';
            str += (n[2] != 0) ? (a[Number(n[2])] || b[n[2][0]] + ' ' + a[n[2][1]]) + 'Lakh ' : '';
            str += (n[3] != 0) ? (a[Number(n[3])] || b[n[3][0]] + ' ' + a[n[3][1]]) + 'Thousand ' : '';
            str += (n[4] != 0) ? (a[Number(n[4])] || b[n[4][0]] + ' ' + a[n[4][1]]) + 'Hundred ' : '';

            str += (n[5] != 0) ? ((str != '') ? 'and ' : '') + (a[Number(n[5])] || b[n[5][0]] + ' ' + a[n[5][1]]) + 'only ' : '';
            return str;
        }

        $scope.amountschange14 = function () {
            var input = document.getElementById('txtInput4').value;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {

                var str = input.replace(',', '');
                input = str;
            }
            var output = Number(str).toLocaleString('en-US');
            var lsdocumentchargerecoverd_words = inWords(str);
            var amount = new Intl.NumberFormat('en-IN').format(Number(str));
            if (amount == 'NaN') {
                Notify.alert('Accept Numeric Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtdispgstprocessing_fees = "";

            }
            else {
                document.getElementById('documentchargerecoverd_words').innerHTML = lsdocumentchargerecoverd_words;
                $scope.txtdispgstprocessing_fees = amount;

            }

        }

        $scope.amountschange11 = function () {
            var amount = (($scope.txtdisbadditional_charges.replace(/[\s,]+/g, '').trim()) * ($scope.txtdisbadditionalgst) / 100);
            var doc = ($scope.txtdisbadditional_charges.replace(/[\s,]+/g, '').trim());
            var output = (parseInt(amount) + parseInt(doc));
            var lsadditionalfinancecharges_words = inWords1(output);
            document.getElementById('additionalfinancecharges_words').innerHTML = lsadditionalfinancecharges_words;
            $scope.txtgstadditionfees_charges = new Intl.NumberFormat('en-IN').format(Number(output));

        }

        function inWords1(num) {
            var a = ['', 'One ', 'Two ', 'Three ', 'Four ', 'Five ', 'Six ', 'Seven ', 'Eight ', 'Nine ', 'Ten ', 'Eleven ', 'Twelve ', 'Thirteen ', 'Fourteen ', 'Fifteen ', 'Sixteen ', 'Seventeen ', 'Eighteen ', 'Nineteen '];
            var b = ['', '', 'Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];
            var s = num.toString();
            s = s.replace(/[\, ]/g, '');
            if (s != parseFloat(s)) return '';
            if ((num = num.toString()).length > 9) return 'Overflow';
            var n = ('000000000' + num).substr(-9).match(/^(\d{2})(\d{2})(\d{2})(\d{1})(\d{2})$/);
            if (!n) return; var str = '';
            str += (n[1] != 0) ? (a[Number(n[1])] || b[n[1][0]] + ' ' + a[n[1][1]]) + 'Crore ' : '';
            str += (n[2] != 0) ? (a[Number(n[2])] || b[n[2][0]] + ' ' + a[n[2][1]]) + 'Lakh ' : '';
            str += (n[3] != 0) ? (a[Number(n[3])] || b[n[3][0]] + ' ' + a[n[3][1]]) + 'Thousand ' : '';
            str += (n[4] != 0) ? (a[Number(n[4])] || b[n[4][0]] + ' ' + a[n[4][1]]) + 'Hundred ' : '';

            str += (n[5] != 0) ? ((str != '') ? 'and ' : '') + (a[Number(n[5])] || b[n[5][0]] + ' ' + a[n[5][1]]) + 'only ' : '';
            return str;
        }

        $scope.amountschange18 = function () {
            var input = document.getElementById('txtInput6').value;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {

                var str = input.replace(',', '');
                input = str;
            }
            var output = Number(str).toLocaleString('en-US');
            var lsadditionalfinancecharges_words = inWords(str);
            var amount = new Intl.NumberFormat('en-IN').format(Number(str));
            if (amount == 'NaN') {
                Notify.alert('Accept Numeric Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtgstadditionfees_charges = "";

            }
            else {
                document.getElementById('additionalfinancecharges_words').innerHTML = lsadditionalfinancecharges_words;
                $scope.txtgstadditionfees_charges = amount;

            }

        }

        // Lsa Details


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

        var application_gid = $location.search().application_gid;
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

        var application_gid = $location.search().application_gid;
        var param = {
            application_gid: application_gid
        }
        var url = 'api/MstApplicationAdd/GetLoanDtl';
        SocketService.getparams(url, param).then(function (resp) {
            $scope.mstloan_list = resp.data.mstloan_list;
            $scope.servicecharges_list = resp.data.servicecharges_list;
        });

        var params = {
            application_gid: application_gid
        }
        var url = 'api/MstCC/GetScheduleMeeting';
        SocketService.getparams(url, params).then(function (resp) {
            unlockUI();
            $scope.ccmember_list = resp.data.ccmember_list;
            $scope.otheruser_list = resp.data.otheruser_list;
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

        var rmdisbursementrequest_gid = $location.search().rmdisbursementrequest_gid;
        var params = {
            generatelsa_gid: $location.search().lsareference_gid,
            rmdisbursementrequest_gid: rmdisbursementrequest_gid
        }
        var url = 'api/MstLSA/GetLSABankAccountSummary';
        SocketService.getparams(url, params).then(function (resp) {
            unlockUI();
            $scope.lsabankaccsummary_list = resp.data.MdlBankAccount;
        });

        var rmdisbursementrequest_gid = $location.search().rmdisbursementrequest_gid;
        var params = {
            application_gid: application_gid,
            rmdisbursementrequest_gid: rmdisbursementrequest_gid
        }
        var url = 'api/MstLSA/GetCreditBankAccountSummary';
        SocketService.getparams(url, params).then(function (resp) {
            unlockUI();
            $scope.creditbankacc_list = resp.data.MdlBankAccount;
        });


        //refreshcreditbankaccountsummary();
        //refreshbankaccountsummary();
        //function refreshbankaccountsummary() {
        //    lockUI();
        //    var params = {
        //        generatelsa_gid: lsgeneratelsa_gid
        //    }
        //    var url = 'api/MstLSA/GetLSABankAccountSummary';
        //    SocketService.getparams(url, params).then(function (resp) {
        //        unlockUI();
        //        $scope.lsabankaccsummary_list = resp.data.MdlBankAccount;
        //    });
        //}

        //function refreshcreditbankaccountsummary() {
        //    lockUI();
        //    var params = {
        //        application_gid: application_gid
        //    }
        //    var url = 'api/MstLSA/GetCreditBankAccountSummary';
        //    SocketService.getparams(url, params).then(function (resp) {
        //        unlockUI();
        //        $scope.creditbankacc_list = resp.data.MdlBankAccount;
        //    });
        //}

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

        $scope.viewservicechargesdetails = function (lsafeescharge_gid, chargetype) {
            var modalInstance = $modal.open({
                templateUrl: '/DocumentChargesView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                lockUI();
                var params = {
                    lsafeescharge_gid: lsafeescharge_gid,
                    charge_type: chargetype
                }
                var url = "api/MstLSA/GetlsachargesDetail";
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.chargesdtl = resp.data;
                });

                $scope.already_recovered = function () {
                    $scope.alreadyrecovered_show = true;
                    $scope.toberecovered_show = false;
                }

                $scope.tobe_recovered = function () {
                    $scope.alreadyrecovered_show = false;
                    $scope.toberecovered_show = true;
                }

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
                    if (val3=='N')
                        DownloaddocumentService.Downloaddocument(val1, val2);
                    else
                        DownloaddocumentService.OtherDownloaddocument(val1, val2, val3);

                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.downloadcollateraldoc = function () {
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

        $scope.txtfacility = function () {

            var input = document.getElementById('valueamount').value;
            var amountwithdot = input.includes(".");
            if (amountwithdot == false) {
                var str1 = input.replace(/,/g, '');

                var str = Math.round(str1);
                var output = Number(str).toLocaleString('en-IN');
                var lswords_totalamount1 = cmnfunctionService.fnConvertNumbertoWord(str);
                if (output == "NaN") {
                    Notify.alert('Accept Number Format Only..!', {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    // $scope.txtloanfaility_amount = "";
                }
                else {
                    //$scope.txtloanfaility_amount = output;
                    document.getElementById('words_totalamount1').innerHTML = lswords_totalamount1;

                    var txtloanfaility_amount = parseInt($scope.txtloanfaility_amount.replace(/[\s,]+/g, '').trim());
                    var lsoveralllimit_amount = parseInt($scope.overalllimit_amount);
                    var lsloanfacility_amount = parseInt($scope.lsloanfacility_amount);
                    if ((txtloanfaility_amount) > (lsoveralllimit_amount - lsloanfacility_amount)) {
                        $scope.amount_validation = false;
                    }
                    else {
                        $scope.amount_validation = true;
                    }
                    var lsamount = (lsoveralllimit_amount - lsloanfacility_amount);
                    $scope.txtremaining = (lsamount - txtloanfaility_amount);
                }
                $scope.txtloanfaility_amount = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN')
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

        $scope.total = function () {
            var total = 0;
            angular.forEach($scope.disbsupplierdtl_list, function (data) {
                if (data.creditopsdisbursement_amount) {
                    total += parseFloat(data.creditopsdisbursement_amount.replaceAll(",", ""));
                }
            })
            total = total.toLocaleString('en-IN');
            return total;
        }

        $scope.totalrm = function () {
            var total = 0;
            angular.forEach($scope.disbsupplierdtl_list, function (data) {
                total += parseFloat(data.rmdisbursement_amount.replaceAll(",", ""));
            })
            total = total.toLocaleString('en-IN');
            return total;
        }

        $scope.totalchecker = function () {
            var total = 0;
            angular.forEach($scope.disbsupplierdtl_list, function (data) {
                if (data.creditopscheckerdisbursement_amount) {
                    total += parseFloat(data.creditopscheckerdisbursement_amount.replaceAll(",", ""));
                }
            })
            total = total.toLocaleString('en-IN');
            return total;
        }

        $scope.totalfarmermaker = function () {
            var total = 0;
            angular.forEach($scope.farmerindividualsummary_list, function (data) {
                if (data.creditopsdisbursement_amount) {
                    total += parseFloat(data.creditopsdisbursement_amount.replaceAll(",", ""));
                }
            })
            total = total.toLocaleString('en-IN');
            return total;
        }

        $scope.totalfarmerrm = function () {
            var total = 0;
            angular.forEach($scope.farmerindividualsummary_list, function (data) {
                total += parseFloat(data.rmfarmerdisbursement_amount.replaceAll(",", ""));
            })
            total = total.toLocaleString('en-IN');
            return total;
        }

        $scope.totalfarmerchecker = function () {
            var total = 0;
            angular.forEach($scope.farmerindividualsummary_list, function (data) {
                if (data.creditopscheckerdisbursement_amount) {
                    total += parseFloat(data.creditopscheckerdisbursement_amount.replaceAll(",", ""));
                }
            })
            total = total.toLocaleString('en-IN');
            return total;
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

        $scope.farmerindividual_postcreatecustomertoencore = function (farmercontact_gid) {
            var params = {
                farmercontact_gid: farmercontact_gid
            }
            var url = 'api/SamFinEncoreCustomer/PostCreateCustomerEncore';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    getEncoreFarmerDetailsSummary()
                }
                else {

                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                unlockUI();
                $modalInstance.close('closed');
            });
        }

        $scope.farmerindividual_postfindcustomerencore = function (farmercontact_gid) {
            var params = {
                farmercontact_gid: farmercontact_gid
            }
            var url = 'api/SamFinEncoreCustomer/PostFindCustomerEncoreFarmer';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    getEncoreFarmerDetailsSummary()
                }
                else {

                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    getEncoreFarmerDetailsSummary()
                }
                unlockUI();
                $modalInstance.close('closed');
            });
        }

        //$scope.farmerindividual_postdisbursementtoencore = function (farmercontact_gid) {
        //    var params = {
        //        farmercontact_gid: farmercontact_gid
        //    }
        //    var url = 'api/SamFinEncoreDisbursement/PostEncoreDisbursementFarmer';
        //    lockUI();
        //    SocketService.post(url, params).then(function (resp) {
        //        if (resp.data.status == true) {
        //            unlockUI();
        //            Notify.alert(resp.data.message, {
        //                status: 'success',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //            getEncoreFarmerDetailsSummary()
        //        }
        //        else {

        //            Notify.alert(resp.data.message, {
        //                status: 'warning',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //        }
        //        unlockUI();
        //        $modalInstance.close('closed');
        //    });
        //}

        $scope.farmerindividual_postdisbursementtoencorepop = function (farmercontact_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/addinstrument.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                var url = 'api/MstCreditOpsApplication/Getinstrumentlist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.instrument_list = resp.data.instrumentlist;
                    unlockUI();
                });

                $scope.submit = function () {
                    var lsinstrument_gid = '';
                    var lsinstrument = '';
                    if ($scope.cboinstrument != undefined || $scope.cboinstrument != null) {
                        lsinstrument_gid = $scope.cboinstrument.instrument_gid,
                            lsinstrument = $scope.cboinstrument.instrument;
                    }

                    var params = {

                        instrument_gid: lsinstrument_gid,
                        instrument: lsinstrument,
                        farmercontact_gid: farmercontact_gid

                    }

                    var url = 'api/SamFinEncoreDisbursement/PostEncoreDisbursementFarmer';
                    lockUI();

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            getEncoreFarmerDetailsSummary()
                        }
                        else {

                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        unlockUI();
                        $modalInstance.close('closed');
                    });
                }
            }
        }

        $scope.farmerindividual_postdisbursementtoencorepopbatch = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addinstrument.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                var url = 'api/MstCreditOpsApplication/Getinstrumentlist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.instrument_list = resp.data.instrumentlist;
                    unlockUI();
                });

                $scope.submit = function () {
                    var lsinstrument_gid = '';
                    var lsinstrument = '';
                    if ($scope.cboinstrument != undefined || $scope.cboinstrument != null) {
                        lsinstrument_gid = $scope.cboinstrument.instrument_gid,
                            lsinstrument = $scope.cboinstrument.instrument;
                    }

                    var params = {

                        instrument_gid: lsinstrument_gid,
                        instrument: lsinstrument,
                        /*farmercontact_gid: farmercontact_gid*/
                        application_gid: application_gid,
                        application2sanction_gid: application2sanction_gid,
                        application2loan_gid: application2loan_gid,
                        customer_urn: customer_urn,
                        rmdisbursementrequest_gid: rmdisbursementrequest_gid

                    }

                    var url = 'api/SamFinEncoreDisbursement/BatchEncoreDisbursementFarmer';
                    lockUI();

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });                            
                        }
                        else {

                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        getEncoreFarmerDetailsSummary()
                        unlockUI();
                        $modalInstance.close('closed');
                    });
                }
            }
        }

        //$scope.supplier_postdisbursementtoencore = function (disbursementsupplier_gid) {
        //    var params = {
        //        disbursementsupplier_gid: disbursementsupplier_gid
        //    }
        //    var url = 'api/SamFinEncoreDisbursement/PostEncoreDisbursementSupplier';
        //    lockUI();
        //    SocketService.post(url, params).then(function (resp) {
        //        if (resp.data.status == true) {
        //            unlockUI();
        //            Notify.alert(resp.data.message, {
        //                status: 'success',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //            getEncoreFarmerDetailsSummary()
        //        }
        //        else {

        //            Notify.alert(resp.data.message, {
        //                status: 'warning',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //        }
        //        unlockUI();
        //        $modalInstance.close('closed');
        //    });
        //}
        
        $scope.supplier_postdisbursementtoencorepop = function (disbursementsupplier_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/addinstrument.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                var url = 'api/MstCreditOpsApplication/Getinstrumentlist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.instrument_list = resp.data.instrumentlist;
                    unlockUI();
                });

                $scope.submit = function () {
                    var lsinstrument_gid = '';
                    var lsinstrument = '';
                    if ($scope.cboinstrument != undefined || $scope.cboinstrument != null) {
                        lsinstrument_gid = $scope.cboinstrument.instrument_gid,
                        lsinstrument = $scope.cboinstrument.instrument;
                    }

                    var params = {

                        instrument_gid: lsinstrument_gid,
                        instrument: lsinstrument,
                        disbursementsupplier_gid: disbursementsupplier_gid

                    }

                    var url = 'api/SamFinEncoreDisbursement/PostEncoreDisbursementSupplier';
                    lockUI();
                    
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            getEncoreApplicantSummary()()
                        }
                        else {

                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        unlockUI();
                        $modalInstance.close('closed');
                    });
                }
            }
        }

        $scope.supplier_postdisbursementtoencorepopbatch = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addinstrument.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                var url = 'api/MstCreditOpsApplication/Getinstrumentlist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.instrument_list = resp.data.instrumentlist;
                    unlockUI();
                });

                $scope.submit = function () {
                    var lsinstrument_gid = '';
                    var lsinstrument = '';
                    if ($scope.cboinstrument != undefined || $scope.cboinstrument != null) {
                        lsinstrument_gid = $scope.cboinstrument.instrument_gid,
                            lsinstrument = $scope.cboinstrument.instrument;
                    }

                    var params = {

                        instrument_gid: lsinstrument_gid,
                        instrument: lsinstrument,
                        /*disbursementsupplier_gid: disbursementsupplier_gid*/
                        application_gid: application_gid,
                        application2sanction_gid: application2sanction_gid,
                        application2loan_gid: application2loan_gid,
                        customer_urn: customer_urn,
                        rmdisbursementrequest_gid: rmdisbursementrequest_gid

                    }

                    var url = 'api/SamFinEncoreDisbursement/BatchEncoreDisbursementSupplier';
                    lockUI();

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });                            
                        }
                        else {

                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        getEncoreApplicantSummary()
                        unlockUI();
                        $modalInstance.close('closed');
                    });
                }
            }
        }
        
        $scope.Updatesupplier_creditopsamt = function (disbursementsupplier_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/EditSupplierCreditOpsAmount.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    disbursementsupplier_gid: disbursementsupplier_gid
                }
                var url = 'api/MstCreditOpsApplication/GetCreditOpsCheckerSupplierDisbAmountView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtsampleeditsupplierdisbursement_amount = resp.data.creditopscheckerdisbursement_amount;
                    $scope.txteditsupplierdisbursement_amount = (parseInt($scope.txtsampleeditsupplierdisbursement_amount.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountseperator = (parseInt($scope.txteditsupplierdisbursement_amount.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                    document.getElementById('words_totalamount68').innerHTML = $scope.lblamountwords;
                });

                function defaultamountwordschange(input) {
                    var str1 = input.replace(/,/g, '');
                    var str = Math.round(str1);
                    var output = Number(str).toLocaleString('en-IN');
                    var lswords = cmnfunctionService.fnConvertNumbertoWord(str);
                    return lswords;
                }

                $scope.txtamountchange68 = function () {
                    var input = document.getElementById('EditDisbursementCreditOps_amount').value;
                    var str1 = input.replace(/,/g, '');
                    var str = Math.round(str1);
                    var output = Number(str).toLocaleString('en-IN');
                    var words_totalamount68 = cmnfunctionService.fnConvertNumbertoWord(str);
                    if (output == "NaN") {
                        Notify.alert('Accept Number Format Only..!', {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.txteditsupplierdisbursement_amount = "";
                    }
                    else {
                        //   $scope.txtprocessing_fee = output;
                        document.getElementById('words_totalamount68').innerHTML = words_totalamount68;
                        var txteditsupplierdisbursement_amount = parseInt($scope.txteditsupplierdisbursement_amount.replace(/[\s,]+/g, '').trim());
                    }
                    $scope.txteditsupplierdisbursement_amount = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                }

                $scope.updatesupplier_creditopsamt = function () {
                    var params = {
                        disbursementsupplier_gid: disbursementsupplier_gid,
                        creditopscheckerdisbursement_amount: $scope.txteditsupplierdisbursement_amount
                    }
                    var url = 'api/MstCreditOpsApplication/CreditOpsCheckerSupplierDisbAmountUpdate';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            $modalInstance.close('closed');
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

        $scope.addsupplier_creditopsamt = function (disbursementsupplier_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/AddSupplierCreditOpsAmount.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.txtamountchange64 = function () {
                    var input = document.getElementById('DisbursementCreditOps_amount').value;
                    var str1 = input.replace(/,/g, '');
                    var str = Math.round(str1);
                    var output = Number(str).toLocaleString('en-IN');
                    var words_totalamount64 = cmnfunctionService.fnConvertNumbertoWord(str);
                    if (output == "NaN") {
                        Notify.alert('Accept Number Format Only..!', {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.txtsupplierdisbursement_amount = "";
                    }
                    else {
                        //   $scope.txtprocessing_fee = output;
                        document.getElementById('words_totalamount64').innerHTML = words_totalamount64;
                        var txtsupplierdisbursement_amount = parseInt($scope.txtsupplierdisbursement_amount.replace(/[\s,]+/g, '').trim());
                    }
                    $scope.txtsupplierdisbursement_amount = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                }

                $scope.submitsupplier_creditopsamt = function () {
                    var params = {
                        disbursementsupplier_gid: disbursementsupplier_gid,
                        creditopscheckerdisbursement_amount: $scope.txtsupplierdisbursement_amount
                    }
                    var url = 'api/MstCreditOpsApplication/CreditOpsCheckerSupplierDisbAmountUpdate';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            $modalInstance.close('closed');
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

        $scope.addfarmer_creditopsamt = function (farmercontact_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/AddFarmerCreditOpsAmount.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.txtamountchange84 = function () {
                    var input = document.getElementById('DisbursementFarmerCreditOps_amount').value;
                    var str1 = input.replace(/,/g, '');
                    var str = Math.round(str1);
                    var output = Number(str).toLocaleString('en-IN');
                    var words_totalamount84 = cmnfunctionService.fnConvertNumbertoWord(str);
                    if (output == "NaN") {
                        Notify.alert('Accept Number Format Only..!', {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.txtfarmerdisbursement_amount = "";
                    }
                    else {
                        //   $scope.txtprocessing_fee = output;
                        document.getElementById('words_totalamount84').innerHTML = words_totalamount84;
                        var txtfarmerdisbursement_amount = parseInt($scope.txtfarmerdisbursement_amount.replace(/[\s,]+/g, '').trim());
                    }
                    $scope.txtfarmerdisbursement_amount = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                }

                $scope.submitfarmer_creditopsamt = function () {
                    var params = {
                        farmercontact_gid: farmercontact_gid,
                        creditopscheckerdisbursement_amount: $scope.txtfarmerdisbursement_amount
                    }
                    var url = 'api/MstCreditOpsApplication/CreditOpsCheckerFarmerDisbAmountUpdate';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            $modalInstance.close('closed');
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

        $scope.Updatefarmer_creditopsamt = function (farmercontact_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/EditFarmerCreditOpsAmount.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    farmercontact_gid: farmercontact_gid
                }
                var url = 'api/MstCreditOpsApplication/GetCreditOpsCheckerFarmerDisbAmountView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtsampleeditfarmerdisbursement_amount = resp.data.creditopscheckerdisbursement_amount;
                    $scope.txteditFarmerdisbursement_amount = (parseInt($scope.txtsampleeditfarmerdisbursement_amount.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountseperator = (parseInt($scope.txteditFarmerdisbursement_amount.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                    document.getElementById('words_totalamount33').innerHTML = $scope.lblamountwords;
                });

                function defaultamountwordschange(input) {
                    var str1 = input.replace(/,/g, '');
                    var str = Math.round(str1);
                    var output = Number(str).toLocaleString('en-IN');
                    var lswords = cmnfunctionService.fnConvertNumbertoWord(str);
                    return lswords;
                }

                $scope.txtamountchange33 = function () {
                    var input = document.getElementById('EditDisbursementFarmerCreditOps_amount').value;
                    var str1 = input.replace(/,/g, '');
                    var str = Math.round(str1);
                    var output = Number(str).toLocaleString('en-IN');
                    var words_totalamount68 = cmnfunctionService.fnConvertNumbertoWord(str);
                    if (output == "NaN") {
                        Notify.alert('Accept Number Format Only..!', {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.txteditFarmerdisbursement_amount = "";
                    }
                    else {
                        //   $scope.txtprocessing_fee = output;
                        document.getElementById('words_totalamount33').innerHTML = words_totalamount68;
                        var txteditFarmerdisbursement_amount = parseInt($scope.txteditFarmerdisbursement_amount.replace(/[\s,]+/g, '').trim());
                    }
                    $scope.txteditFarmerdisbursement_amount = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                }

                $scope.updatefarmer_creditopsamt = function () {
                    var params = {
                        farmercontact_gid: farmercontact_gid,
                        creditopscheckerdisbursement_amount: $scope.txteditFarmerdisbursement_amount
                    }
                    var url = 'api/MstCreditOpsApplication/CreditOpsCheckerFarmerDisbAmountUpdate';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            $modalInstance.close('closed');
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

        $scope.disbursement_reject = function () {
            var modalInstance = $modal.open({
                templateUrl: '/DisbursementReject.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.reject_submit = function () {
                    var url = 'api/MstCreditOpsApplication/PostDisbursementRequestReject';
                    var params = {
                        rmdisbursementrequest_gid: rmdisbursementrequest_gid,
                        rejected_remarks: $scope.txtrejectremarks,
                        approval_status: 'Rejected',
                    }
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            $state.go('app.MstMyDisbursementSummary');
                            //activate();
                        }
                        else {
                            unlockUI();
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                        }
                    });
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            }
        }

        $scope.approve = function () {

            var params = {
                processing_fees: $scope.txtdispprocessing_fees,
                processing_gst: $scope.txtdispprocessinggst,
                dispgstprocessing_fees: $scope.txtdispgstprocessing_fees,
                od_amount: $scope.txtdispod_amount,
                finance_charges: $scope.txtdisbadditional_charges,
                additionalcharges_gst: $scope.txtdisbadditionalgst,
                dispgstadditionfees_charges: $scope.txtgstadditionfees_charges,
                escrow_payment: $scope.rdbdisbpayment_escrow,
                nach_status: $scope.rdbdisbnach_status,
                checker_remarks: $scope.txtchecker_remarks,
                updated_person: 'Checker',
                rmdisbursementrequest_gid: rmdisbursementrequest_gid
            }

            var url = "api/MstCreditOpsApplication/PostDisbursementApprove";
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status = true) {
                    Notify.alert(resp.data.message, 'success');
                    $location.url('app/MstMyDisbursementCheckerSummary');
                }
                else {
                    Notify.alert(resp.data.message, 'warning');
                    activate();
                }
            });
        }

        function defaultamountwordschange(input) {
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords = cmnfunctionService.fnConvertNumbertoWord(str);
            return lswords;
        }

        $scope.IFSCValidation = function () {

            if ($scope.txtIFSC_Code.length == 11) {
                var params = {
                    ifsc: $scope.txtIFSC_Code
                }
                lockUI();
                var url = 'api/Kyc/IfscVerification';
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.result.bank != "" && resp.data.result.bank != null) {
                        $scope.ifscvalidation = true;
                        $scope.txtBank_Name = resp.data.result.bank;
                        $scope.txtBranch_Name = resp.data.result.branch;
                        $scope.txtBank_Address = resp.data.result.address;
                        $scope.txtMICR_Code = resp.data.result.micr;

                        if (resp.data.result.micr != "" && resp.data.result.micr != null) {
                            $scope.micrempty = true;
                        }

                    } else if (resp.data.result.bank == "" || resp.data.result.bank == null) {
                        $scope.ifscvalidation = false;
                        Notify.alert('IFSC is not verified..!', 'warning');
                        $scope.txtBank_Name = '';
                        $scope.txtBranch_Name = '';
                        $scope.txtBank_Address = '';
                        $scope.txtMICR_Code = '';
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }

                });
            }
        }
        $scope.BankAccValidation = function () {
            if ($scope.txtbankacct_no == $scope.txtconfirmbankacct_no) {
                var params = {
                    ifsc: $scope.txtIFSC_Code,
                    accountNumber: $scope.txtconfirmbankacct_no
                }
                var url = 'api/Kyc/BankAccVerification';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.result.bankTxnStatus != "" && resp.data.result.bankTxnStatus != null) {
                        $scope.bankaccvalidation = true;
                        $scope.txtacctholder_name = resp.data.result.accountName;

                    } else if (resp.data.result.bankTxnStatus == "" || resp.data.result.bankTxnStatus == null) {
                        $scope.bankaccvalidation = false;
                        Notify.alert('Bank Account is not verified..!', 'warning');
                        $scope.txtacctholder_name = '';
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
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

        $scope.addbankaccount_details = function () {
            $scope.addbankaccountdetails_show = true;
        }

        $scope.bankdetail_close = function () {
            $scope.addbankaccountdetails_show = false;
        }

        $scope.add_bankaccountdtl = function () {

            if (($scope.txtapplicant_name == undefined) || ($scope.txtapplicant_name == '') || ($scope.txtIFSC_Code == undefined) ||
                ($scope.txtIFSC_Code == '') || ($scope.txtbankacct_no == undefined) || ($scope.txtbankacct_no == '') ||
                ($scope.txtconfirmbankacct_no == undefined) || ($scope.txtconfirmbankacct_no == '') || ($scope.txtacctholder_name == undefined) ||
                ($scope.txtacctholder_name == '') || ($scope.rdbbankacctdisbusement_status == undefined) ||
                ($scope.rdbbankacctdisbusement_status == '')) {
                Notify.alert('Enter All Mandatory Fields', 'warning');
            }
            else {
                var application_gid = $location.search().application_gid;
                var params = {
                    application_gid: application_gid,
                    applicant_name: $scope.txtapplicant_name,
                    ifsc_code: $scope.txtIFSC_Code,
                    bank_name: $scope.txtBank_Name,
                    branch_name: $scope.txtBranch_Name,
                    Bank_Address: $scope.txtBank_Address,
                    micr_code: $scope.txtMICR_Code,
                    bankaccount_number: $scope.txtbankacct_no,
                    confirmbankaccount_number: $scope.txtconfirmbankacct_no,
                    accountholder_name: $scope.txtacctholder_name,
                    disbursement_amount: $scope.txtdisbursement_amount,
                    disbursementaccount_status: $scope.rdbbankacctdisbusement_status,
                    rmdisbursementrequest_gid: rmdisbursementrequest_gid,
                    initiated_by: 'Credit Ops Checker'
                }
                var url = 'api/MstCreditOpsApplication/PostDisbApplicantBankDtl';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.disbapplicantbankacctdtl_list = resp.data.disbapplicantbankacctdtl_list;
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.addbankaccountdetails_show = false;
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    $scope.txtIFSC_Code = '';
                    $scope.txtBank_Name = '';
                    $scope.txtBranch_Name = '';
                    $scope.txtBank_Address = '';
                    $scope.txtMICR_Code = '';
                    $scope.txtbankacct_no = '';
                    $scope.txtconfirmbankacct_no = '';
                    $scope.txtacctholder_name = '';
                    $scope.txtapplicant_name = '';
                    $scope.txtdisbursement_amount = '';
                });
            }

        }

        $scope.chequeleafdocumentUpload = function (val) {
            if (($scope.txtdocument_title == null) || ($scope.txtdocument_title == '') || ($scope.txtdocument_title == undefined)) {
                $("#chequefilefile").val('');
                Notify.alert('Kindly Enter the Document Title', 'warning');
            } else {
                var frm = new FormData();
                for (var i = 0; i < val.length; i++) {
                    var item = {
                        name: val[i].name,
                        file: val[i]
                    };
                    frm.append('fileupload', item.file);
                    frm.append('file_name', item.name);
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[i].name, "documentformatonly");
                    if (IsValidExtension == false) {
                        Notify.alert("File format is not supported..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });

                        return false;
                    }

                }
                frm.append('disbapplicantbankdtl_gid', '');
                frm.append('document_title', $scope.txtdocument_title);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    lockUI();
                    var url = 'api/MstCreditOpsApplication/DisbApplicantdocumentUpload';
                    console.log(url)
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                        $scope.disbapplicantuploaddocument_list = resp.data.disbapplicantuploaddocument_list;
                        unlockUI();

                        $("#chequefilefile").val('');
                        $scope.uploadfrm = undefined;

                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $scope.txtdocument_title = '';
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
                else {
                    alert('Document is not Available..!');
                    return;
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

        $scope.bankacctdtl_uploaddocumentcancel = function (disbapplicantbankdocument_gid) {
            lockUI();
            var params = {
                disbapplicantbankdocument_gid: disbapplicantbankdocument_gid
            }
            var url = 'api/MstCreditOpsApplication/DeleteDisbApplicantdocument';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.disbapplicantuploaddocument_list = resp.data.disbapplicantuploaddocument_list;
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                unlockUI();
            });
        }

        $scope.txtamountchange5 = function () {
            var input = document.getElementById('disbursement_amount').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var words_totalamount55 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtdisbursement_amount = "";
            }
            else {
                //   $scope.txtprocessing_fee = output;
                document.getElementById('words_totalamount55').innerHTML = words_totalamount55;
                var txtdisbursement_amount = parseInt($scope.txtdisbursement_amount.replace(/[\s,]+/g, '').trim());
            }
            $scope.txtdisbursement_amount = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
        }

        $scope.applicantbankdtl_view = function (val) {
            var disbapplicantbankdtl_gid = val;
            var modalInstance = $modal.open({
                templateUrl: '/ApplicantBankDtlView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    disbapplicantbankdtl_gid: disbapplicantbankdtl_gid
                }
                var url = 'api/MstCreditOpsApplication/GetDisbApplicantBankDtlView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblapplicant_name = resp.data.applicant_name;
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
                    $scope.lbldisbursementaccount_status = resp.data.disbursementaccount_status;
                    $scope.lblinitiated_by = resp.data.initiated_by;
                });

                var url = 'api/MstCreditOpsApplication/DisbApplicantBankAcctDocView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.disbapplicantuploaddocument_list = resp.data.disbapplicantuploaddocument_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.applicantview_downloads = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }

                $scope.downloadallview_applicantdoc = function () {
                    for (var i = 0; i < $scope.disbapplicantuploaddocument_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.disbapplicantuploaddocument_list[i].document_path, $scope.disbapplicantuploaddocument_list[i].document_name);
                    }
                }

                $scope.applicantview_documentviewer = function (val1, val2) {
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

        //Export Excel

        $scope.ExportexcelLoanODDispersement = function () {
            lockUI();
            
                var params = {
                    rmdisbursementrequest_gid: rmdisbursementrequest_gid
                            }
            var url = 'api/MstApplicationReport/LoanODDispersement';
            SocketService.getparams(url,params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'warning')
                    
                }

            });
        }
        //

        $scope.ExportexcelDispersementNEFT = function () {
            lockUI();
            var params = {
                rmdisbursementrequest_gid: rmdisbursementrequest_gid
            }
            var url = 'api/MstApplicationReport/DispersementNEFT';
            SocketService.getparams(url,params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                    
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'warning')
                    
                }

            });
        }
         $scope.ExportexcelODAccountopen = function () {
            lockUI();
            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstApplicationReport/ODAccountopen';
            SocketService.getparams(url,params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                                 
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'warning')
                    
                }

            });
        }

        $scope.createloanaccount_encore = function () {
            var params = {
                application_gid: application_gid,
                application2sanction_gid: application2sanction_gid,
                application2loan_gid: application2loan_gid,
                customer_urn: customer_urn,
                rmdisbursementrequest_gid: rmdisbursementrequest_gid
            }
            var url = 'api/SamFinEncoreLoanAccount/CreateLoanAccount';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {                   
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    getEncoreApplicantSummary();
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

        //$scope.applicant_postdisbursementtoencore = function () {
        //    var params = {
        //        application_gid: application_gid,
        //        application2sanction_gid: application2sanction_gid,
        //        application2loan_gid: application2loan_gid,
        //        customer_urn: customer_urn,
        //        rmdisbursementrequest_gid: rmdisbursementrequest_gid
        //    }
        //    var url = 'api/SamFinEncoreDisbursement/PostEncoreDisbursementB2B';
        //    lockUI();
        //    SocketService.post(url, params).then(function (resp) {
        //        unlockUI();
        //        if (resp.data.status == true) {
        //            Notify.alert(resp.data.message, {
        //                status: 'success',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //            getEncoreApplicantSummary();
        //        }
        //        else {
        //            Notify.alert(resp.data.message, {
        //                status: 'warning',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //        }


        //    });
        //}

        $scope.applicant_postdisbursementtoencorepop = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addinstrument.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                var url = 'api/MstCreditOpsApplication/Getinstrumentlist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.instrument_list = resp.data.instrumentlist;
                    unlockUI();
                });

                $scope.submit = function () {
                    var lsinstrument_gid = '';
                    var lsinstrument = '';
                    if ($scope.cboinstrument != undefined || $scope.cboinstrument != null) {
                        lsinstrument_gid = $scope.cboinstrument.instrument_gid,
                            lsinstrument = $scope.cboinstrument.instrument;
                    }

                    var params = {

                        instrument_gid: lsinstrument_gid,
                        instrument: lsinstrument,
                        application_gid: application_gid,
                        application2sanction_gid: application2sanction_gid,
                        application2loan_gid: application2loan_gid,
                        customer_urn: customer_urn,
                        rmdisbursementrequest_gid: rmdisbursementrequest_gid

                    }

                    var url = 'api/SamFinEncoreDisbursement/PostEncoreDisbursementB2B';
                    lockUI();

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            getEncoreFarmerDetailsSummary()
                        }
                        else {

                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        unlockUI();
                        $modalInstance.close('closed');
                    });
                }
            }
        }

        function getEncoreApplicantSummary() {
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
                $scope.lblencore_accountid = resp.data.encore_accountid;
                $scope.lblencoreintegration_status = resp.data.encoreintegration_status;
            });
        }

        $scope.createloanaccountfarmer_encore = function (farmercontact_gid) {
            var params = {
                application_gid: application_gid,
                application2sanction_gid: application2sanction_gid,
                application2loan_gid: application2loan_gid,
                customer_urn: customer_urn,
                rmdisbursementrequest_gid: rmdisbursementrequest_gid,
                farmercontact_gid: farmercontact_gid
            }
            var url = 'api/SamFinEncoreLoanAccount/CreateLoanAccountFarmer';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {                   
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    getEncoreFarmerDetailsSummary();
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

        function getEncoreFarmerDetailsSummary() {
            var params = {
                rmdisbursementrequest_gid: rmdisbursementrequest_gid
            }

            var url = 'api/MstCreditOpsApplication/GetDisbFarmerIndividualCreditOps';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.farmerindividualsummary_list = resp.data.farmerindividualsummary_list;
                $scope.batchencorefindcust_status = resp.data.batchencorefindcust_status;
            });

        }

        $scope.encoreaccountid_view = function (application_gid, rmdisbursementrequest_gid) {
            
            var modalInstance = $modal.open({
                templateUrl: '/EncoreAccountIDView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

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
                    $scope.lblencore_accountid = resp.data.encore_accountid;
                    $scope.lblencoreintegration_status = resp.data.encoreintegration_status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

  
        
            }

        }

        $scope.createloanaccountsupplier_encore = function () {
            var params = {
                application_gid: application_gid,
                application2sanction_gid: application2sanction_gid,
                application2loan_gid: application2loan_gid,
                customer_urn: customer_urn,
                rmdisbursementrequest_gid: rmdisbursementrequest_gid
            }
            var url = 'api/SamFinEncoreLoanAccount/CreateLoanAccountSupplier';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {                   
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    getEncoreApplicantSummary();
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

        $scope.batchdisbursementbookingforsupplierlist_encore = function () {
            var params = {
                application_gid: application_gid,
                application2sanction_gid: application2sanction_gid,
                application2loan_gid: application2loan_gid,
                customer_urn: customer_urn,
                rmdisbursementrequest_gid: rmdisbursementrequest_gid
            }
            var url = 'api/SamFinEncoreDisbursement/BatchEncoreDisbursementSupplier';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    getEncoreApplicantSummary();
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

        $scope.batchcustomercreationforfarmerlist_encore = function () {
            var params = {
                application_gid: application_gid,
                application2sanction_gid: application2sanction_gid,
                application2loan_gid: application2loan_gid,
                customer_urn: customer_urn,
                rmdisbursementrequest_gid: rmdisbursementrequest_gid
            }
            var url = 'api/SamFinEncoreCustomer/BatchCustomerCreationforFarmer';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    getEncoreFarmerDetailsSummary()
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

        $scope.batchfindcustomerforfarmerlist_encore = function () {
            var params = {
                application_gid: application_gid,
                application2sanction_gid: application2sanction_gid,
                application2loan_gid: application2loan_gid,
                customer_urn: customer_urn,
                rmdisbursementrequest_gid: rmdisbursementrequest_gid
            }
            var url = 'api/SamFinEncoreCustomer/BatchFindCustomerforFarmer';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });                    
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                getEncoreFarmerDetailsSummary()
            });
        }

        $scope.batchloanaccountcreationforfarmerlist_encore = function () {
            var params = {
                application_gid: application_gid,
                application2sanction_gid: application2sanction_gid,
                application2loan_gid: application2loan_gid,
                customer_urn: customer_urn,
                rmdisbursementrequest_gid: rmdisbursementrequest_gid
            }
            var url = 'api/SamFinEncoreLoanAccount/BatchCreateLoanAccountFarmer';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    getEncoreFarmerDetailsSummary()
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

        //$scope.batchdisbursementbookingforfarmerlist_encore = function () {
        //    var params = {
        //        application_gid: application_gid,
        //        application2sanction_gid: application2sanction_gid,
        //        application2loan_gid: application2loan_gid,
        //        customer_urn: customer_urn,
        //        rmdisbursementrequest_gid: rmdisbursementrequest_gid
        //    }
        //    var url = 'api/SamFinEncoreDisbursement/BatchEncoreDisbursementFarmer';
        //    lockUI();
        //    SocketService.post(url, params).then(function (resp) {
        //        unlockUI();
        //        if (resp.data.status == true) {
        //            Notify.alert(resp.data.message, {
        //                status: 'success',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //            getEncoreFarmerDetailsSummary()
        //        }
        //        else {
        //            Notify.alert(resp.data.message, {
        //                status: 'warning',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //        }


        //    });
        //}

        $scope.downloadallESdoc = function () {
            for (var i = 0; i < $scope.UploadCADES_DocumentList.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.UploadCADES_DocumentList[i].document_path, $scope.UploadCADES_DocumentList[i].document_name);
            }
        }

        $scope.downloadallsanction = function () {
            for (var i = 0; i < $scope.sanctionuploaddocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.sanctionuploaddocument_list[i].document_path, $scope.sanctionuploaddocument_list[i].document_name);
            }
        }

        $scope.downloadallGeneral = function () {
            for (var i = 0; i < $scope.filename_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.filename_list[i].document_path, $scope.filename_list[i].document_name);
            }
        }

    }
})();
