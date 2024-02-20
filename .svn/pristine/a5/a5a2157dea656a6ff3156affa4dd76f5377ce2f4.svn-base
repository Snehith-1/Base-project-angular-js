(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstRMDisbursementRequestEditController', MstRMDisbursementRequestEditController);

    MstRMDisbursementRequestEditController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function MstRMDisbursementRequestEditController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstRMDisbursementRequestEditController';
        var rmdisbursementrequest_gid = $location.search().rmdisbursementrequest_gid;
        var customer_urn = $location.search().customer_urn; 
        var application_gid = $location.search().application_gid;
        var lsgeneratelsa_gid = $location.search().generatelsa_gid;
        var application2loan_gid = $location.search().application2loan_gid;
        var application2sanction_gid = $location.search().application2sanction_gid;
        var cbodisbursement_to = $location.search().disbursement_to;
        activate();
        function activate() {               
          
            var url = "api/MstCreditOpsApplication/GetSupplierNameDropDown";
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.dispsupplier_list = resp.data.dispsupplier_list;
            });
            /*var cbodisbursement_to = $location.search().disbursement_to;*/
            if (cbodisbursement_to == "Supplier") {
                $scope.supplierdtl_show = true;
                $scope.farmerdtl_show = false;
                $scope.applicant_show = false;               
            }
            else if (cbodisbursement_to == "Farmer(B2B2C)") {
                $scope.supplierdtl_show = false;
                $scope.farmer_show = true;
                $scope.applicant_show = false;

                var params = {
                    rmdisbursementrequest_gid: $location.search().rmdisbursementrequest_gid
                }

                var url = 'api/MstCreditOpsApplication/GetDisbFarmerIndividualCreditOps';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.farmerindividualsummary_list = resp.data.farmerindividualsummary_list;
                });

                var params = {
                    application_gid: application_gid
                }
                var url = 'api/MstCreditOpsApplication/GetDisbFarmerIndividualImportLog';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.individualimport_List = resp.data.individualimport_List;
                });

            }

            else if (cbodisbursement_to == "Applicant(B2B)") {
                $scope.supplierdtl_show = false;
                $scope.farmer_show = false;
                $scope.applicant_show = true;
            }
            else {
                $scope.supplierdtl_show = false;
                $scope.farmer_show = false;
                $scope.applicant_show = false;
            }
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

            var url = 'api/MstCreditOpsApplication/GetDisbApplicantDocTempClear';
            SocketService.get(url).then(function (resp) {
            });

            var params = {
                rmdisbursementrequest_gid:$location.search().rmdisbursementrequest_gid
            }
            var url = 'api/MstCreditOpsApplication/GetDisbApplicantSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.disbapplicantbankacctdtl_list = resp.data.disbapplicantbankacctdtl_list;
            });


            var params = {
                rmdisbursementrequest_gid:$location.search().rmdisbursementrequest_gid
            }
            var url = 'api/MstCreditOpsApplication/GetRmDisbursementDocumentDtl';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.disbursementuploaddocument_list = resp.data.disbursementuploadeddocument_list;
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

            var rmdisbursementrequest_gid = $location.search().rmdisbursementrequest_gid;
            var params = {
                generatelsa_gid: $location.search().generatelsa_gid,
                rmdisbursementrequest_gid: rmdisbursementrequest_gid
            }
            var url = 'api/MstLSA/GetLSABankAccountDisSummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lsabankaccsummary_list = resp.data.MdlBankAccount;
            });


            var rmdisbursementrequest_gid = $location.search().rmdisbursementrequest_gid;
            var params = {
                application_gid: application_gid,
                rmdisbursementrequest_gid: rmdisbursementrequest_gid
            }
            var url = 'api/MstLSA/GetCreditBankAccountDisSummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.creditbankacc_list = resp.data.MdlBankAccount;
            });


            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstApplicationView/GetApplicationBasicView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lblapplication_no = resp.data.application_no;
                $scope.lblbasiccustomer_name = resp.data.customer_name;
                $scope.lblbasicdesignation = resp.data.designation_type;
                $scope.lblvertical = resp.data.vertical_name;
                $scope.lblconstitution = resp.data.constitution_name;
                $scope.lblcredit_group = resp.data.creditgroup_name;
                $scope.lblbusinessapproved_date = resp.data.headapproval_date;
                $scope.lblccapproved_date = resp.data.ccapproved_date;
                $scope.lblregion = resp.data.region;
            });

            //// Get Overall Limit
            //var url = 'api/MstApplicationView/GetProductChargesDtl';
            //SocketService.getparams(url, params).then(function (resp) {
            //    unlockUI();
            //    $scope.lblSanctionAmount = resp.data.overalllimit_amount;
            //    $scope.lblsanction_amounteperator = (parseInt($scope.lblSanctionAmount.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
            //    $scope.lblsanctionamount_incomewords = defaultamountwordschange($scope.lblsanction_amounteperator);
            //});

            var url = 'api/MstApplicationView/BankAccountDetailsList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.bankaccount_list = resp.data.bankaccount_list;
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
                application_gid: application_gid
            }
            var url = "api/MstCADFlow/GetlsaProductname";
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.loanproductlist = resp.data.LsaProductname;
            });
            var url = 'api/MstCAD/GetCADBasicView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lblentity_gid = resp.data.entity_gid;
                $scope.lblentity_name = resp.data.entity_name;
                $scope.lblcreditapproved_date = resp.data.creditapproved_date;
                $scope.lblvertical_code = resp.data.vertical_code;
            });

            var params = {
                rmdisbursementrequest_gid: $location.search().rmdisbursementrequest_gid
            }
            var url = 'api/MstCreditOpsApplication/GetDisbursementRequestEdit';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.rmdisbursementrequest_gid = resp.data.rmdisbursementrequest_gid;
                $scope.application_gid = resp.data.application_gid;
                $scope.cbosanctionref_no = resp.data.application2sanction_gid;
                $scope.sanction_refno = resp.data.sanction_refno;
                $scope.cboproductsubproduct_name = resp.data.application2loan_gid
                var params = {
                    application2loan_gid: $scope.cboproductsubproduct_name
                }
                var url = 'api/MstCreditOpsApplication/GetProductChargesDtl';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.application2sanction_gid = resp.data.application2sanction_gid;
                    $scope.application_gid = resp.data.application_gid;
                    $scope.lblfacility_purpose = resp.data.enduse_purpose;
                    $scope.lblrevolving = resp.data.facility_mode;
                    $scope.lblinterest_ratemargin = resp.data.rate_interest;
                    $scope.lbltenure_days = resp.data.tenureoverall_limit;
                    $scope.lblloanfacility_amount = resp.data.loanfacility_amount;
                    $scope.lblloanfacility_amounteperator = (parseInt($scope.lblloanfacility_amount.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblloanfacility_amountwords = defaultamountwordschange($scope.lblloanfacility_amounteperator);
                });
                $scope.product_type = resp.data.product_type;
                $scope.txtremarks = resp.data.remarks;
                $scope.disbursementassign_status = resp.data.disbursementassign_status;
                $scope.updated_person = resp.data.updated_person;
                $scope.txtloandisbursement_date = resp.data.loandisbursement_date;
            }); 
            var params = {
                rmdisbursementrequest_gid: $location.search().rmdisbursementrequest_gid
            }
            var url = 'api/MstCreditOpsApplication/GetDisbursementCreditOpsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.cbosanctionref_no = resp.data.application2sanction_gid;
                $scope.sanction_refno = resp.data.sanction_refno;
                $scope.cboproductsubproduct_name = resp.data.application2loan_gid;
                $scope.lblproduct_type = resp.data.product_type;               
                $scope.lblloandisbursement_date = resp.data.loandisbursement_date;
                $scope.editloandisbursement_date = resp.data.editloandisbursement_date;
                $scope.lsareference_number = resp.data.lsareference_number;
                $scope.cbodisbursement_to = resp.data.disbursement_to;
                if ($scope.cbodisbursement_to == "Supplier") {
                    $scope.supplierdtl_show = true;
                    $scope.farmerdtl_show = false;
                    $scope.applicant_show = false;
                }
                else if ($scope.cbodisbursement_to == "Farmer(B2B2C)") {
                    $scope.supplierdtl_show = false;
                    $scope.farmer_show = true;
                    $scope.applicant_show = false;

                    var rmdisbursementrequest_gid = $location.search().rmdisbursementrequest_gid;
                    var params = {
                        rmdisbursementrequest_gid: rmdisbursementrequest_gid
                    }

                    var url = 'api/MstCreditOpsApplication/GetDisbFarmerIndividualCreditOps';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.farmerindividualsummary_list = resp.data.farmerindividualsummary_list;
                    });

                    var params = {
                        application_gid: application_gid
                    }
                    var url = 'api/MstCreditOpsApplication/GetDisbFarmerIndividualImportLog';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.individualimport_List = resp.data.individualimport_List;
                    });

                }
                else if ($scope.cbodisbursement_to == "Applicant(B2B)") {
                    $scope.supplierdtl_show = false;
                    $scope.farmer_show = false;
                    $scope.applicant_show = true;
                }
                else {
                    $scope.supplierdtl_show = false;
                    $scope.farmer_show = false;
                    $scope.applicant_show = false;
                }
            });

            

            //var params = {
            //    rmdisbursementrequest_gid: $location.search().rmdisbursementrequest_gid
            //}
            //var url = "api/MstCreditOpsApplication/GetDisbursementDocEditSummary";
            //SocketService.getparams(url, params).then(function (resp) {
            //    unlockUI();
            //    $scope.disbursementuploaddocument_list = resp.data.disbursementuploaddocument_list;
            //});

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
                rmdisbursementrequest_gid: rmdisbursementrequest_gid
            }

            var url = 'api/MstCreditOpsApplication/GetDisbursementSupplierSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.disbsupplierdtl_list = resp.data.disbsupplierdtl_list;
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
                $scope.lblapplicationurn_gid = resp.data.application_gid;
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

            var rmdisbursementrequest_gid = $location.search().rmdisbursementrequest_gid;

            var params = {
                rmdisbursementrequest_gid: rmdisbursementrequest_gid
            }

            var url = 'api/MstCreditOpsApplication/GetDisbFarmerIndividualCreditOps';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.farmerindividualsummary_list = resp.data.farmerindividualsummary_list;
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

            var lsgeneratelsa_gid = $location.search().lsgeneratelsa_gid;

            if (lsgeneratelsa_gid != "" && lsgeneratelsa_gid != undefined) {
                var params = {
                    generatelsa_gid: $location.search().lsgeneratelsa_gid
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
                    generatelsa_gid: $location.search().lsgeneratelsa_gid

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
                generatelsa_gid: $location.search().generatelsa_gid,
                rmdisbursementrequest_gid: rmdisbursementrequest_gid
            }
            var url = 'api/MstLSA/GetLSABankAccountDisSummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lsabankaccsummary_list = resp.data.MdlBankAccount;
            });
            var rmdisbursementrequest_gid = $location.search().rmdisbursementrequest_gid;
            var params = {
                application_gid: application_gid,
                rmdisbursementrequest_gid: rmdisbursementrequest_gid
            }
            var url = 'api/MstLSA/GetCreditBankAccountDisSummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.creditbankacc_list = resp.data.MdlBankAccount;
            });

            var rmdisbursementrequest_gid = $location.search().rmdisbursementrequest_gid;
            var params = {
                application_gid: application_gid,
                rmdisbursementrequest_gid: rmdisbursementrequest_gid
            }
            var url = 'api/MstLSA/GetBankAccountStatus';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.overalldisbursement_flag = resp.data.lsoveralldisbursement_flag;
                $scope.lsfinalbankaccount_status = resp.data.lsbankaccount_status;
            });

            var url = 'api/MstCreditOpsApplication/GetBankAccountTempClear';
            SocketService.get(url).then(function (resp) {
            });

            var url = 'api/MstCreditOpsApplication/GetRMDisbursementTempClear';
            SocketService.get(url).then(function (resp) {
            });

            var url = 'api/MstCreditOpsApplication/GetDisbFarmerSupplierTempClear';
            SocketService.get(url).then(function (resp) {
            });

            var url = 'api/MstCreditOpsApplication/GetDisbSupplierDocTempClear';
            SocketService.get(url).then(function (resp) {
            });

            var url = 'api/MstCreditOpsApplication/GetDisbursementDocTempClear';
            SocketService.get(url).then(function (resp) {
            });
        }

        $scope.productsubproductname_change = function (cboproductsubproduct_name) {
            var params = {
                application2loan_gid: $scope.cboproductsubproduct_name.application2loan_gid
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

        }

        $scope.additionalcharges_changes = function () {
            var input = document.getElementById('additionalcharge').value;
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
                $scope.txtadditional_charges = "";
            }
            else {
                //  $scope.txtvalue = output;
                document.getElementById('words_totalamount1').innerHTML = lswords_totalamount1;
                var txtadditional_charges = parseInt($scope.txtadditional_charges.replace(/[\s,]+/g, '').trim());
            }
            $scope.txtadditional_charges = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN')
        }

        $scope.odamount_changes = function () {
            var input = document.getElementById('odamount').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount2 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtod_amount = "";
            }
            else {
                //  $scope.txtvalue = output;
                document.getElementById('words_totalamount2').innerHTML = lswords_totalamount2;
                var txtod_amount = parseInt($scope.txtod_amount.replace(/[\s,]+/g, '').trim());
            }
            $scope.txtod_amount = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN')
        }

        function defaultamountwordschange(input) {
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords = cmnfunctionService.fnConvertNumbertoWord(str);
            return lswords;
        }


        $scope.amountdisbursed_changes = function () {
            var input = document.getElementById('amountdisbursed').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount3 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtamount_disbursed = "";
            }
            else {
                //  $scope.txtvalue = output;
                document.getElementById('words_totalamount3').innerHTML = lswords_totalamount3;
                var txtamount_disbursed = parseInt($scope.txtamount_disbursed.replace(/[\s,]+/g, '').trim());
            }
            $scope.txtamount_disbursed = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN')
        }

        

        //$scope.uploaddocumentcancel = function (rmdisbursementdocument_gid, application_gid) {
        //    lockUI();
        //    var params = {
        //        rmdisbursementdocument_gid: rmdisbursementdocument_gid,
        //        rmdisbursementrequest_gid: rmdisbursementrequest_gid
        //    }
        //    var url = 'api/MstCreditOpsApplication/DeleteRMDisbursementDocument';
        //    SocketService.getparams(url, params).then(function (resp) {
        //        if (resp.data.status == true) {
        //            $scope.disbursementuploaddocument_list = resp.data.disbursementuploaddocument_list;
        //            Notify.alert(resp.data.message, {
        //                status: 'success',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //        }
        //        else {
        //            Notify.alert(resp.data.message, {
        //                status: 'warning',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });

        //        }
        //        unlockUI();
        //    });
        //}

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

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.Back = function () {
            $location.url('app/MstRMDisbursementRequest?customer_urn=' + customer_urn);
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
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.download_chequeleafdoc = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }

            }

        }

        $scope.Update_disbursement = function () {

          

            var params = {         
                remarks: $scope.txtremarks,
                updated_person: 'RM',
                application_gid: application_gid,
                rmdisbursementrequest_gid: rmdisbursementrequest_gid,
                disbursement_to: $location.search().disbursement_to,
            }
            var url = 'api/MstCreditOpsApplication/PostDisbursementUpdate';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $location.url('app/MstRMDisbursementRequest?application_gid=' + application_gid + '&customer_urn=' + customer_urn);
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
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.download_chequeleafdoc = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }

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

        $scope.txtamountchange2 = function () {
            var input = document.getElementById('Additional_charges').value;
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
                $scope.txtadditional_charges = "";
            }
            else {
                //   $scope.txtprocessing_fee = output;
                document.getElementById('words_totalamount52').innerHTML = lswords_totalamount52;
                var txtadditional_charges = parseInt($scope.txtadditional_charges.replace(/[\s,]+/g, '').trim());
            }
            $scope.txtadditional_charges = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
        }

        $scope.txtamountchange3 = function () {
            var input = document.getElementById('Amount_Disbursed').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount53 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtamounttobe_disbursed = "";
            }
            else {
                //   $scope.txtprocessing_fee = output;
                document.getElementById('words_totalamount53').innerHTML = lswords_totalamount53;
                var txtamounttobe_disbursed = parseInt($scope.txtamounttobe_disbursed.replace(/[\s,]+/g, '').trim());
                var lslblSanctionAmount = parseInt($scope.lblSanctionAmount);
                var lsamounttobe_disbursed = parseInt($scope.txtamounttobe_disbursed);
                if ((txtamounttobe_disbursed) > (lslblSanctionAmount - lsamounttobe_disbursed)) {
                    $scope.amount_validation = false;
                }
                else {
                    $scope.amount_validation = true;
                }
            }
            $scope.txtamounttobe_disbursed = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
        }

        $scope.txtamountchange4 = function () {
            var input = document.getElementById('Od_amount').value;
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
                $scope.txtod_amount = "";
            }
            else {
                //   $scope.txtprocessing_fee = output;
                document.getElementById('words_totalamount54').innerHTML = words_totalamount54;
                var txtod_amount = parseInt($scope.txtamounttobe_disbursed.replace(/[\s,]+/g, '').trim());
            }
            $scope.txtod_amount = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
        }

        //$scope.txtamountchange5 = function () {
        //    var input = document.getElementById('disbursement_amount').value;
        //    var str1 = input.replace(/,/g, '');
        //    var str = Math.round(str1);
        //    var output = Number(str).toLocaleString('en-IN');
        //    var words_totalamount55 = cmnfunctionService.fnConvertNumbertoWord(str);
        //    if (output == "NaN") {
        //        Notify.alert('Accept Number Format Only..!', {
        //            status: 'danger',
        //            pos: 'top-center',
        //            timeout: 3000
        //        });
        //        $scope.txtdisbursement_amount = "";
        //    }
        //    else {
        //        //   $scope.txtprocessing_fee = output;
        //        document.getElementById('words_totalamount55').innerHTML = words_totalamount55;
        //        var txtdisbursement_amount = parseInt($scope.txtdisbursement_amount.replace(/[\s,]+/g, '').trim());
        //    }
        //    $scope.txtdisbursement_amount = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');

        //    $scope.lbldisbursementamount_status = 'T';
        //    var params = {
        //        application_gid: list_gid,
        //        rmdisbursementrequest_gid: $location.search().rmdisbursementrequest_gid,
        //        validation_amount: str
        //    }
        //    var url = 'api/MstCreditOpsApplication/DisbursementAmountCalValidation';

        //    SocketService.getparams(url, params).then(function (resp) {
        //        $scope.lbldisbursementamount_status = resp.data.disbursementamount_status;

        //    });

        //}

        $scope.documentUpload = function (val) {

            if (($scope.txtgeneraldocument_title == null) || ($scope.txtgeneraldocument_title == '') || ($scope.txtgeneraldocument_title == undefined)) {
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
                frm.append('document_title', $scope.txtgeneraldocument_title);
                frm.append('rmdisbursementrequest_gid', rmdisbursementrequest_gid);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    lockUI();
                    var url = 'api/MstCreditOpsApplication/RMDisbursementDocumentUpload';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        $scope.disbursementuploaddocument_list = resp.data.disbursementuploaddocument_list;
                        unlockUI();

                        $("#fileDocument").val('');
                        $scope.uploadfrm = undefined;

                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $scope.txtgeneraldocument_title = '';
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
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        //$scope.downloadall = function () {
        //    for (var i = 0; i < $scope.disbursementuploaddocument_list.length; i++) {
        //        DownloaddocumentService.Downloaddocument($scope.disbursementuploaddocument_list[i].document_path, $scope.disbursementuploaddocument_list[i].document_name);
        //    }
        //}

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
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.download_chequeleafdoc = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
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
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.download_chequeleafdoc = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }

            }

        }

        $scope.Submit = function () {
            var lssanctionrefno_gid = '';
            var lssanctionref_no = '';
            var lsapplication_gid = '';
            var lsproduct_gid = '';
            var lsproduct_name = '';
            var lsgeneratelsa_gid = '';
            var lslsa_refno = '';

            if ($scope.cbosanctionref_no != undefined || $scope.cbosanctionref_no != null) {
                lssanctionrefno_gid = $scope.cbosanctionref_no.application2sanction_gid;
                lssanctionref_no = $scope.cbosanctionref_no.sanction_refno;
                lsapplication_gid = $scope.cbosanctionref_no.application_gid;
            }
            if ($scope.cboproductsubproduct_name != undefined || $scope.cboproductsubproduct_name != null) {
                lsproduct_gid = $scope.cboproductsubproduct_name.application2loan_gid;
                lsproduct_name = $scope.cboproductsubproduct_name.product_type;
            }
            if ($scope.cbolsaref_no != undefined || $scope.cbolsaref_no != null) {
                lsgeneratelsa_gid = $scope.cbolsaref_no.generatelsa_gid;
                lslsa_refno = $scope.cbolsaref_no.lsa_refno;
            }

            var params = {
                application_gid: lsapplication_gid,
                application2sanction_gid: lssanctionrefno_gid,
                sanction_refno: lssanctionref_no,
                application2loan_gid: lsproduct_gid,
                product_type: lsproduct_name,
                processing_fees: $scope.txtprocessing_fees,
                gst: $scope.txtgst,
                finance_charges: $scope.txtadditional_charges,
                od_amount: $scope.txtod_amount,
                escrow_payment: $scope.rdbpayment_escrow,
                nach_status: $scope.rdbnach_status,
                remarks: $scope.txtremarks,
                updated_person: 'RM',
                amounttobe_disbursed: $scope.txtamounttobe_disbursed,
                disbursement_to: $scope.cbodisbursement_to,
                lsareference_gid: lsgeneratelsa_gid,
                lsareference_number: lslsa_refno,
            }
            var url = 'api/MstCreditOpsApplication/PostDisbursementRequestAdd';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $location.url('app/MstRMDisbursementRequest?customer_urn=' + customer_urn);
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

        $scope.downloadallCADMail = function () {
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

                $scope.downloadcollateraldoc = function (val1, val2,val3) {
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

                $scope.downloadalldocument_list = function () {
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

        $scope.downloadtemplate_individual = function () {
            var Templateurl = apiManage.GetCommonData['TemplatePath'].Path;
            var filename = "\ImportExcelDisbursementFarmerIndividual.xlsx";
            var phyPath = Templateurl + filename;
            var relPath = phyPath.split("EMS");
            var relpath1 = relPath[1].replace("\\", "/");
            var prefix = window.location.protocol + "//";
            var hosts = window.location.host;
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            var name = filename.split('.');
            link.download = name[0];
            link.href = str;
            link.click();
        }

        $scope.uploadIndividual = function (val, val1, name) {
            var application_gid = $location.search().application_gid;

            var fileInput = document.getElementById('fileimport');
            var filePath = fileInput.value;

            $scope.fileinputvalue = filePath;

            // Allowing file type
            var allowedExtensions = /(\.xls|\.xlsx|\.csv)$/i;

            if (!allowedExtensions.exec(filePath)) {
                Notify.alert('File Format Not Supported!', 'warning')
                $modalInstance.close('closed');
                //fileInput.value = '';
            }
            else if (filePath.includes("ImportExcelDisbursementFarmerIndividual") == false) {
                Notify.alert('File Name / Template Not Supported!', 'warning')
                $modalInstance.close('closed');
            }
            else {
                var item = {
                    name: val[0].name,
                    file: val[0]
                };
                var frm = new FormData();
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
                frm.append('application_gid', application_gid);
                $scope.uploadfrm = frm;
            }
        }


        $scope.uploadExcelIndividual = function () {

            if ($scope.fileinputvalue == '' || $scope.fileinputvalue == undefined || $scope.fileinputvalue == null) {
                Notify.alert('Kindly Select the Excel file', 'warning')
            }
            else {
                var url = 'api/MstCreditOpsApplication/ImportExcelDisbFarmerIndividual';
                lockUI();
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    if (resp.data.status == true) {
                        unlockUI();
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        
                        var params = {
                            application_gid: application_gid
                        }
                        var url = 'api/MstCreditOpsApplication/GetDisbFarmerIndividualImportLog';
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.individualimport_List = resp.data.individualimport_List;
                        });
                        var rmdisbursementrequest_gid = $location.search().rmdisbursementrequest_gid;
                        var params = {
                            rmdisbursementrequest_gid: rmdisbursementrequest_gid
                        }
                        var url = 'api/MstCreditOpsApplication/GetDisbFarmerIndividualSummary';
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.farmerindividualsummary_list = resp.data.farmerindividualsummary_list;
                        });
                    }
                    else {
                        unlockUI();
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                    $("#fileimport").val('');
                });
            }

        }

        $scope.uploadExcelCancel = function () {
            $("#fileimport").val('');
        };

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

        $scope.buyerdtl_view = function (buyer_gid) {
            localStorage.setItem('buyer_gid', buyer_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/MstDisbursementBuyerDtlView";
            window.open(URL, '_blank');
        }

        $scope.farmerindividual_view = function (farmercontact_gid) {
            localStorage.setItem('farmercontact_gid', farmercontact_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/MstDisbIndividualFarmerDtlView";
            window.open(URL, '_blank');
        }

        $scope.coapplicantdtl_add = function (farmercontact_gid) {
            var application_gid = $location.search().application_gid;
            var disbursement_to = $scope.cbodisbursement_to;
            var application2sanction_gid = $location.search().application2sanction_gid;
            var lsgeneratelsa_gid = $location.search().generatelsa_gid;
            var productsubproduct_gid = $location.search().application2loan_gid;
            var rmdisbursementrequest_gid = $location.search().rmdisbursementrequest_gid;
            $location.url('app/MstDisbCoApplicantDtlAdd?farmercontact_gid=' + farmercontact_gid + '&application_gid=' + application_gid + '&disbursement_to=' + disbursement_to + '&customer_urn=' + customer_urn + '&lsgeneratelsa_gid=' + lsgeneratelsa_gid + '&application2sanction_gid=' + application2sanction_gid + '&productsubproduct_gid=' + productsubproduct_gid + '&rmdisbursementrequest_gid=' + rmdisbursementrequest_gid + '&lspage=RMDisbursementEdit');
        }

        $scope.addsupplier_details = function () {
            $scope.addsupplierdetails_show = true;
        }

        $scope.supplier_close = function () {
            $scope.addsupplierdetails_show = false;
        }

      
        $scope.supplier_documentviewer = function (val1, val2) {
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

        $scope.supplier_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.downloadall_supplierdoc = function () {
            for (var i = 0; i < $scope.disbsupplieruploaddocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.disbsupplieruploaddocument_list[i].document_path, $scope.disbsupplieruploaddocument_list[i].document_name);
            }
        }

        $scope.supplier_uploaddocumentcancel = function (disbsupplierbankdocument_gid) {
            lockUI();
            var params = {
                disbsupplierbankdocument_gid: disbsupplierbankdocument_gid
            }
            var url = 'api/MstCreditOpsApplication/DeleteDisbsupplierdocument';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.disbsupplieruploaddocument_list = resp.data.disbsupplieruploaddocument_list;
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

        $scope.add_supplierdtl = function () {

            if (($scope.cbosupplier_name == undefined) || ($scope.cbosupplier_name == '') || ($scope.cbosupplier_name == undefined) ||
               ($scope.cbosupplierifsc_code == '') || ($scope.cbosupplierifsc_code == '') || ($scope.cbosupplierifsc_code == undefined) ||
               ($scope.txtdisbursement_amount == '') || ($scope.txtdisbursement_amount == '') || ($scope.txtdisbursement_amount == undefined)) {
                Notify.alert('Enter All Mandatory Fields', 'warning');
            }
            else {
                var application_gid = $location.search().application_gid;
                var supplier_gid = $scope.cbosupplier_name.supplier_gid;
                var supplier_name = $scope.cbosupplier_name.supplier_name;
                var supplier2bank_gid = $scope.cbosupplierifsc_code.supplier2bank_gid;
                var ifsc_code = $scope.cbosupplierifsc_code.ifsc_code;

                var params = {
                    application_gid: application_gid,
                    supplier_gid: supplier_gid,
                    supplier_name: supplier_name,
                    supplier2bank_gid: supplier2bank_gid,
                    ifsc_code: ifsc_code,
                    bank_name: $scope.txtsuplbank_name,
                    branch_name: $scope.txtsuplbranch_name,
                    branch_address: $scope.txtsuplbranch_address,
                    micr_code: $scope.txtsuplmicr_code,
                    bankaccount_number: $scope.txtsuplbankacct_number,
                    confirmbankaccount_number: $scope.txtsuplconfirmbankacct_number,
                    accountholder_name: $scope.txtsuplacctholder_name,
                    bankaccounttype_name: $scope.txtsuplacct_type,
                    jointaccount_status: $scope.txtsupljoint_acct,
                    jointaccountholder_name: $scope.txtsupljointacctholder_name,
                    chequebook_status: $scope.txtsuplchequebook_available,
                    accountopen_date: $scope.txtsuplacctopen_date,
                    disbursement_amount: $scope.txtdisbursement_amount
                }
                var url = 'api/MstCreditOpsApplication/PostDisbursementSupplier';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.disbsupplieruploaddocument_list = null;
                        var params = {
                            rmdisbursementrequest_gid: rmdisbursementrequest_gid
                        }
                        var url = 'api/MstCreditOpsApplication/GetDisbursementEditSupplierSummary';
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.disbsupplierdtl_list = resp.data.disbsupplierdtl_list;
                        });
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.addsupplierdetails_show = false;
                        $scope.cbosupplierifsc_code = '';
                        $scope.txtsuplbank_name = '';
                        $scope.txtsuplbranch_name = '';
                        $scope.txtsuplbranch_address = '';
                        $scope.txtsuplacctholder_name = '';
                        $scope.txtsuplmicr_code = '';
                        $scope.txtsuplbankacct_number = '';
                        $scope.txtsuplconfirmbankacct_number = '';
                        $scope.txtsuplacct_type = '';
                        $scope.txtsupljoint_acct = '';
                        $scope.txtsupljointacctholder_name = '';
                        $scope.txtsuplchequebook_available = '';
                        $scope.txtsuplacctopen_date = '';
                        $scope.cbosupplier_name = '';
                        $scope.txtdisbursement_amount = '';
                        document.getElementById('words_totalamount55').innerHTML = '';
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

        $scope.coapplicantdtl_view = function (contactcoapplicant_gid) {
            localStorage.setItem('contactcoapplicant_gid', contactcoapplicant_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/MstDisbCoApplicantContactDtlView";
            window.open(URL, '_blank');
        }

        $scope.lsadisbursement_acct = function (primarygid, tab, list_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/LsaBankacctconfirm1.html',
                controller: ModalInstanceCtrl,
                size: 'lg',
                backdrop: 'static',
                keyboard: false
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var application_gid = $location.search().application_gid;
                var lsabankaccdtl_gid = "", creditbankdtl_gid = "";

                //$scope.onselectedurn_yes = function () {
                //    if ($scope.rdbdisbursement_status == 'Yes') {
                //        $scope.disbursement_yes = true;
                //    }
                //    else {
                //        $scope.disbursement_yes = false;
                //        $scope.txtdisbursement_amount = '';
                //    }
                //}

                $scope.txtlastyear_turnoverchange = function () {
                    var input = document.getElementById('lastyear_turnover').value;
                    var str = input.replace(/,/g, '');
                    var output = Number(str).toLocaleString('en-IN');
                    var lswords_totalamount = cmnfunctionService.fnConvertNumbertoWord(str);
                    if (output == "NaN") {
                        Notify.alert('Accept Number Format Only..!', {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.txtlastyear_turnover = "";
                    }
                    else {
                        $scope.txtlastyear_turnover = output;
                        document.getElementById('words_totalamount').innerHTML = lswords_totalamount;
                        var txtlastyear_turnover = parseInt($scope.txtlastyear_turnover.replace(/[\s,]+/g, '').trim());
                    }
                    $scope.txtlastyear_turnover = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN')
                }
                if (tab == 'LSA')
                    lsabankaccdtl_gid = primarygid;
                else
                    creditbankdtl_gid = primarygid;

                if (tab == 'LSA')
                    lsgeneratelsa_gid = list_gid;
                else
                    application_gid = list_gid;

                $scope.confirm_Submit = function () {
                    var rmdisbursementrequest_gid = $location.search().rmdisbursementrequest_gid;
                    var params = {
                        application_gid: application_gid,
                        creditbankdtl_gid: creditbankdtl_gid,
                        lsabankaccdtl_gid: lsabankaccdtl_gid,
                        disbursementaccount_status: $scope.rdbdisbursement_status,
                        disbursement_amount: $scope.txtdisbursement_amount,
                        rmdisbursementrequest_gid: rmdisbursementrequest_gid,
                        initiated_by: 'Credit Ops RM'
                    }
                    var url = 'api/MstCreditOpsApplication/PostConfirmDisbursementRequest';
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
                            bank_details();
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

        $scope.disbursementamount = function (primarygid, list_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/DisbursementAmount.html',
                controller: ModalInstanceCtrl,
                size: 'lg',
                backdrop: 'static',
                keyboard: false
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    application_gid: list_gid,
                    rmdisbursementrequest_gid: $location.search().rmdisbursementrequest_gid,
                }
                var url = 'api/MstCreditOpsApplication/DisbursementAmountCal';

                SocketService.getparams(url, params).then(function (resp) {
                    $scope.lbldisbursementamount_status = resp.data.disbursementamount_status;

                });
                var application_gid = $location.search().application_gid;
                var lsabankaccdtl_gid = "", creditbankdtl_gid = "";

                //$scope.onselectedurn_yes = function () {
                //    if ($scope.rdbdisbursement_status == 'Yes') {
                //        $scope.disbursement_yes = true;
                //    }
                //    else {
                //        $scope.disbursement_yes = false;
                //        $scope.txtdisbursement_amount = '';
                //    }
                //}

                $scope.txtlastyear_turnoverchange = function () {
                    var input = document.getElementById('lastyear_turnover').value;
                    var str = input.replace(/,/g, '');
                    var output = Number(str).toLocaleString('en-IN');
                    var lswords_totalamount = cmnfunctionService.fnConvertNumbertoWord(str);
                    if (output == "NaN") {
                        Notify.alert('Accept Number Format Only..!', {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.txtlastyear_turnover = "";
                    }
                    else {
                        $scope.txtlastyear_turnover = output;
                        document.getElementById('words_totalamount').innerHTML = lswords_totalamount;
                        var txtlastyear_turnover = parseInt($scope.txtlastyear_turnover.replace(/[\s,]+/g, '').trim());
                    }
                    $scope.txtlastyear_turnover = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');

                    $scope.lbldisbursementamount_status = 'T';
                    var params = {
                        application_gid: list_gid,
                        rmdisbursementrequest_gid: $location.search().rmdisbursementrequest_gid,
                        validation_amount: str
                    }
                    var url = 'api/MstCreditOpsApplication/DisbursementAmountCalValidation';

                    SocketService.post(url, params).then(function (resp) {
                        $scope.lbldisbursementamount_status = resp.data.disbursementamount_status;

                    });
                }
                //if (tab == 'LSA')
                //    lsabankaccdtl_gid = primarygid;
                //else
                //    creditbankdtl_gid = primarygid;

                //if (tab == 'LSA')
                //    lsgeneratelsa_gid = list_gid;
                //else
                //    application_gid = list_gid;

                $scope.confirm_Submit = function () {
                    if ($scope.lbldisbursementamount_status == "F") {
                        $scope.bankaccvalidation = false;
                        Notify.alert('Disbursement Amount Greater then Sanction Amount..!', 'warning');
                        $scope.txtacctholder_name = '';
                    } else {
                    var rmdisbursementrequest_gid = $location.search().rmdisbursementrequest_gid;
                    var params = {
                        application_gid: list_gid,
                        credit_gid: primarygid,
                        disbursement_amount: $scope.txtdisbursement_amount,
                        rmdisbursementrequest_gid: rmdisbursementrequest_gid
                    }
                    var url = 'api/MstCreditOpsApplication/PostDisbursementAmount';
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
                            bank_details();

                            var params = {
                                application_gid: list_gid,
                                rmdisbursementrequest_gid: rmdisbursementrequest_gid,

                            }
                            var url = 'api/MstCreditOpsApplication/GetApplicantSummary';
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.amountapplication_gid = resp.data.application_gid;
                                $scope.credit_gid = resp.data.credit_gid;
                                $scope.lblcustomer_name = resp.data.customer_name;
                                $scope.lblmobile_no = resp.data.mobile_no;
                                $scope.lblemail_address = resp.data.email_address;
                                $scope.lbldisbursement_amount = resp.data.disbursement_amount;
                                /*   $scope.lbltenure_days = resp.data.tenureoverall_limit;*/
                            });
                            activate();
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

                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }

        }

        $scope.Updateapplicant_disbursement_amount = function (primarygid, list_gid, lbldisbursementamount_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/EditApplicantCreditOpsAmount.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    application_gid: list_gid,
                    rmdisbursementrequest_gid: $location.search().rmdisbursementrequest_gid,
                }
                var url = 'api/MstCreditOpsApplication/DisbursementAmountCal';

                SocketService.getparams(url, params).then(function (resp) {
                    $scope.lbldisbursementamount_status = resp.data.disbursementamount_status;

                });

                var params = {
                    disbursementamount_gid: lbldisbursementamount_gid
                }
                var url = 'api/MstCreditOpsApplication/GetCreditOpsApplicantDisbAmountView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtsampledisbursement_amount = resp.data.disbursement_amount;
                    $scope.txteditapplicantcreditopsdisbursement_amount = (parseInt($scope.txtsampledisbursement_amount.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountseperator = (parseInt($scope.txteditapplicantcreditopsdisbursement_amount.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                    document.getElementById('words_totalamount75').innerHTML = $scope.lblamountwords;
                });

                function defaultamountwordschange(input) {
                    var str1 = input.replace(/,/g, '');
                    var str = Math.round(str1);
                    var output = Number(str).toLocaleString('en-IN');
                    var lswords = cmnfunctionService.fnConvertNumbertoWord(str);
                    return lswords;
                }

                $scope.txtamountchange75 = function () {
                    var input = document.getElementById('EditApplicantDisbursementCreditOps_amount').value;
                    var str1 = input.replace(/,/g, '');
                    var str = Math.round(str1);
                    var output = Number(str).toLocaleString('en-IN');
                    var words_totalamount75 = cmnfunctionService.fnConvertNumbertoWord(str);
                    if (output == "NaN") {
                        Notify.alert('Accept Number Format Only..!', {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.txteditapplicantcreditopsdisbursement_amount = "";
                    }
                    else {
                        //   $scope.txtprocessing_fee = output;
                        document.getElementById('words_totalamount75').innerHTML = words_totalamount75;
                        var txteditapplicantcreditopsdisbursement_amount = parseInt($scope.txteditapplicantcreditopsdisbursement_amount.replace(/[\s,]+/g, '').trim());
                    }
                    $scope.txteditapplicantcreditopsdisbursement_amount = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lbldisbursementamount_status = 'T';
                    var params = {
                        application_gid: list_gid,
                        rmdisbursementrequest_gid: $location.search().rmdisbursementrequest_gid,
                        validation_amount: str
                    }
                    var url = 'api/MstCreditOpsApplication/DisbursementAmountCalValidation';

                    SocketService.post(url, params).then(function (resp) {
                        $scope.lbldisbursementamount_status = resp.data.disbursementamount_status;

                    });

                }


                $scope.updateapplicant_creditopsamt = function () {
                    if ($scope.lbldisbursementamount_status == "F") {
                        $scope.bankaccvalidation = false;
                        Notify.alert('Disbursement Amount Greater then Sanction Amount..!', 'warning');
                        $scope.txtacctholder_name = '';
                    } else {
                    var params = {
                        application_gid: list_gid,
                        credit_gid: primarygid,
                        rmdisbursementrequest_gid: $location.search().rmdisbursementrequest_gid,
                        disbursement_amount: $scope.txteditapplicantcreditopsdisbursement_amount,
                    }
                    var url = 'api/MstCreditOpsApplication/PostDisbursementAmount';
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
                            bank_details();
                          
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

                }
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            }

        }


        function bank_details() {
            var lsgeneratelsa_gid = $location.search().lsgeneratelsa_gid;
            var application_gid = $location.search().application_gid;
            var rmdisbursementrequest_gid= $location.search().rmdisbursementrequest_gid;

            var params = {
                application_gid: application_gid,
                rmdisbursementrequest_gid: rmdisbursementrequest_gid
            }
            var url = 'api/MstLSA/GetBankAccountStatus';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.overalldisbursement_flag = resp.data.lsoveralldisbursement_flag;
                $scope.lsfinalbankaccount_status = resp.data.lsbankaccount_status;
            });

            var rmdisbursementrequest_gid = $location.search().rmdisbursementrequest_gid;
            var params = {
                generatelsa_gid: $location.search().generatelsa_gid,
                rmdisbursementrequest_gid: rmdisbursementrequest_gid
            }
            var url = 'api/MstLSA/GetLSABankAccountDisSummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lsabankaccsummary_list = resp.data.MdlBankAccount;
            });


            var rmdisbursementrequest_gid = $location.search().rmdisbursementrequest_gid;
            var params = {
                application_gid: application_gid,
                rmdisbursementrequest_gid: rmdisbursementrequest_gid
            }
            var url = 'api/MstLSA/GetCreditBankAccountDisSummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.creditbankacc_list = resp.data.MdlBankAccount;
            });

            var params = {
                application_gid: application_gid,
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
                /*   $scope.lbltenure_days = resp.data.tenureoverall_limit;*/
            });


        }

        $scope.addbankaccount_details = function () {
            $scope.addbankaccountdetails_show = true;
        }

        $scope.bankdetail_close = function () {
            $scope.addbankaccountdetails_show = false;
        }

        $scope.bankeditdetail_close = function () {
            $scope.editbankaccountdetails_show = false;
        }



        $scope.add_bankaccountdtl = function () {

            if (($scope.txtIFSC_Code == undefined) ||
                ($scope.txtIFSC_Code == '') || ($scope.txtbankacct_no == undefined) || ($scope.txtbankacct_no == '') ||
                ($scope.txtconfirmbankacct_no == undefined) || ($scope.txtconfirmbankacct_no == '') || ($scope.txtacctholder_name == undefined) ||
                ($scope.txtacctholder_name == '') || ($scope.rdbbankacctdisbusement_status == undefined) ||
                ($scope.rdbbankacctdisbusement_status == '')) {
                Notify.alert('Enter All Mandatory Fields', 'warning');
            }
            else {
                var application_gid = $location.search().application_gid;
                var rmdisbursementrequest_gid = $location.search().rmdisbursementrequest_gid;
                var params = {
                    application_gid: application_gid,
                    applicant_name: "",
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
                    initiated_by: 'Credit Ops RM',
                    rmdisbursementrequest_gid: rmdisbursementrequest_gid
                }
                var url = 'api/MstCreditOpsApplication/PostDisbApplicantBankDtl';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.disbapplicantbankacctdtl_list = resp.data.disbapplicantbankacctdtl_list;
                        bank_details();
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


        $scope.edit_supplierdtl = function (disbapplicantbankdtl_gid) {
            lockUI();
            $scope.editbankaccountdetails_show = true;
            $scope.calenderbankedit = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                $scope.calenderbankeditopen = true;
            };
            $scope.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            $scope.formats = ['dd-MM-yyyy'];
            $scope.format = $scope.formats[0];


            var params = {
                disbapplicantbankdtl_gid: disbapplicantbankdtl_gid
            }
            var url = 'api/MstCreditOpsApplication/GetDisbAppilicantDtlView';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                //$scope.lblsupplier_name = resp.data.supplier_name;
                $scope.txteditIFSC_Code = resp.data.ifsc_code;
                $scope.txteditMICR_Code = resp.data.micr_code;
                $scope.txteditBank_Address = resp.data.branch_address;
                $scope.txteditBank_Name = resp.data.bank_name;
                $scope.txteditBranch_Name = resp.data.branch_name;
                $scope.txteditbankacct_no = resp.data.bankaccount_number;
                $scope.txteditconfirmbankacct_no = resp.data.confirmbankaccount_number;
                $scope.txteditacctholder_name = resp.data.accountholder_name;
                //$scope.lbldisbursement_amount = resp.data.disbursement_amount;
                $scope.lblcreated_by = resp.data.created_by;
                $scope.lblcreated_date = resp.data.created_date;
                $scope.rdbeditbankacctdisbusement_status = resp.data.disbursementaccount_status;
                $scope.disbapplicantbankdtl_gid = resp.data.disbapplicantbankdtl_gid;
                $scope.disbapplicantuploaddocumentedit_list = resp.data.disbapplicantuploaddocument_list;
            });

            //var url = 'api/MstCreditOpsApplication/DisbsupplierdocumentView';
            //lockUI();
            //SocketService.getparams(url, params).then(function (resp) {
            //    unlockUI();
            //    $scope.disbsupplieruploaddocumentedit_list = resp.data.disbsupplieruploaddocument_list;
            //});


            //$scope.changestakeholder = function (cboapplicationholder_gid) {
            //    var getinfo = $scope.applicationNameinfo.filter(function (el) { return el.credit_gid === cboapplicationholder_gid });
            //    if (getinfo != null) {
            //        $scope.lblstakeholder_type = getinfo[0].stakeholder_type;
            //    }
            //}
        }

        $scope.update_bankdtl = function (disbapplicantbankdtl_gid) {
            if (($scope.txteditIFSC_Code == undefined) ||
                ($scope.txteditIFSC_Code == '') || ($scope.txteditbankacct_no == undefined) || ($scope.txteditbankacct_no == '') ||
                ($scope.txteditconfirmbankacct_no == undefined) || ($scope.txteditconfirmbankacct_no == '') || ($scope.txteditacctholder_name == undefined) ||
                ($scope.txteditacctholder_name == '') || ($scope.rdbeditbankacctdisbusement_status == undefined) ||
                ($scope.rdbeditbankacctdisbusement_status == '')) {
                Notify.alert('Enter All Mandatory Fields', 'warning');
            }
            else {

              
                var params = {
                    application_gid: application_gid,
                    applicant_name: "",
                    ifsc_code: $scope.txteditIFSC_Code,
                    bank_name: $scope.txteditBank_Name,
                    branch_name: $scope.txteditBranch_Name,
                    Bank_Address: $scope.txteditBank_Address,
                    micr_code: $scope.txteditMICR_Code,
                    bankaccount_number: $scope.txteditbankacct_no,
                    confirmbankaccount_number: $scope.txteditconfirmbankacct_no,
                    accountholder_name: $scope.txteditacctholder_name,
                    disbapplicantbankdtl_gid: disbapplicantbankdtl_gid,
                    disbursementaccount_status: $scope.rdbeditbankacctdisbusement_status,
                    initiated_by: 'Credit Ops RM',
                    rmdisbursementrequest_gid: $location.search().rmdisbursementrequest_gid
                }
                var url = 'api/MstCreditOpsApplication/PostUpdateBankAccountDetails';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    $scope.disbapplicantbankacctdtl_list = resp.data.disbapplicantbankacctdtl_list;
                    bank_details();
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.editbankaccountdetails_show = false;
                        $scope.ifscvalidation = "";

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        refreshbankaccountsummary();
                        activate();
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
        }
        $scope.supplierchequeleafdocumentUpload = function (val) {
            if (($scope.txtdocument_title == null) || ($scope.txtdocument_title == '') || ($scope.txtdocument_title == undefined)) {
                $("#chequefilefile22").val('');
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
                frm.append('disbursementsupplier_gid', '');
                frm.append('document_title', $scope.txtdocument_title);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    lockUI();
                    var url = 'api/MstCreditOpsApplication/DisbsupplierdocumentUpload';
                    console.log(url)
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                        $scope.disbsupplieruploaddocument_list = resp.data.disbsupplieruploaddocument_list;
                        unlockUI();

                        $("#chequefilefile22").val('');
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

        $scope.chequeleafdocumentUploadedit = function (val) {
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
                frm.append('disbapplicantbankdtl_gid', $scope.disbapplicantbankdtl_gid);
                frm.append('document_title', $scope.txtdocument_title);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    lockUI();
                    var url = 'api/MstCreditOpsApplication/DisbApplicantdocumentUpload';
                    console.log(url)
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                        $scope.disbapplicantuploaddocumentedit_list = resp.data.disbapplicantuploaddocument_list;
                        unlockUI();

                        $("#chequefilefile1").val('');
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
            for (var i = 0; i < $scope.disbapplicantuploaddocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.disbapplicantuploaddocument_list[i].document_path, $scope.disbapplicantuploaddocument_list[i].document_name);
            }
        }

        $scope.bankacctdtl_uploaddocumentcancel = function (disbapplicantbankdocument_gid) {
            lockUI();
            var params = {
                disbapplicantbankdocument_gid: disbapplicantbankdocument_gid,
                disbapplicantbankdtl_gid: ''
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

            $scope.lbldisbursementamount_status = 'T';
            var params = {
                application_gid: $location.search().application_gid,
                rmdisbursementrequest_gid: $location.search().rmdisbursementrequest_gid,
                validation_amount: str
            }
            var url = 'api/MstCreditOpsApplication/DisbursementAmountCalValidation';

            SocketService.post(url, params).then(function (resp) {
                $scope.lbldisbursementamount_status = resp.data.disbursementamount_status;

            });

        }

        $scope.document_submission = function (lblapplicationurn_gid) {
            //var application_gid = $location.search().application_gid;
            $location.url('app/MstRMDeferralDtls?application_gid=' + lblapplicationurn_gid + '&customer_urn=' + customer_urn);
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


        $scope.editbankacctdtl_uploaddocumentcancel = function (disbapplicantbankdocument_gid, disbapplicantbankdtl_gid) {
            lockUI();
            var params = {
                disbapplicantbankdocument_gid: disbapplicantbankdocument_gid,
                disbapplicantbankdtl_gid: disbapplicantbankdtl_gid
            }
            var url = 'api/MstCreditOpsApplication/DeleteDisbApplicantdocument';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.disbapplicantuploaddocumentedit_list = resp.data.disbapplicantuploaddocument_list;
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

        $scope.downloadallGeneral = function () {
            for (var i = 0; i < $scope.filename_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.filename_list[i].document_path, $scope.filename_list[i].document_name);
            }
        }

        $scope.downloadalldisb = function () {
            for (var i = 0; i < $scope.disbursementuploaddocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.disbursementuploaddocument_list[i].document_path, $scope.disbursementuploaddocument_list[i].document_name);
            }
        }
        $scope.downloadallCheque = function () {
            for (var i = 0; i < $scope.chequeleaf_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.chequeleaf_list[i].chequeleaf_path, $scope.chequeleaf_list[i].chequeleaf_name);
            }
        }

        $scope.downloadall_editbankacctdtl = function () {
            for (var i = 0; i < $scope.disbapplicantuploaddocumentedit_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.disbapplicantuploaddocumentedit_list[i].document_path, $scope.disbapplicantuploaddocumentedit_list[i].document_name);
            }
        }

        $scope.downloadalllsauploaded = function () {
            for (var i = 0; i < $scope.lsauploadeddocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.lsauploadeddocument_list[i].document_path, $scope.lsauploadeddocument_list[i].document_name);
            }
        }
        $scope.downloadallchequelleaf = function () {
            for (var i = 0; i < $scope.uploaddocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.uploaddocument_list[i].chequeleaf_path, $scope.uploaddocument_list[i].chequeleaf_name);
            }
        }

        $scope.downloadalldocument_list = function () {
            for (var i = 0; i < $scope.document_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.document_list[i].chequeleaf_path, $scope.document_list[i].chequeleaf_name);
            }
        }

        $scope.farmer_delete = function (farmercontact_gid) {
            var farmercontact_gid = farmercontact_gid;
            var params = {
                farmercontact_gid: farmercontact_gid
            }

            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();
                    var url = 'api/MstCreditOpsApplication/DeleteDisbFarmerDtl';
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');
                            var params = {
                                rmdisbursementrequest_gid: $location.search().rmdisbursementrequest_gid
                            }

                            var url = 'api/MstCreditOpsApplication/GetDisbFarmerIndividualCreditOps';
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.farmerindividualsummary_list = resp.data.farmerindividualsummary_list;
                            });

                            var params = {
                                application_gid: application_gid
                            }
                            var url = 'api/MstCreditOpsApplication/GetDisbFarmerIndividualImportLog';
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.individualimport_List = resp.data.individualimport_List;
                            });

                        }
                        else {
                            alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            unlockUI();
                        }
                    });
                }
            });
        }

        $scope.suppliername_change = function (cbosupplier_name) {

            var supplier_gid = $scope.cbosupplier_name.supplier_gid;

            var params = {
                supplier_gid: supplier_gid
            }
            var url = 'api/MstCreditOpsApplication/GetSupplierIfscCodeDropDown';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.dispsupplierifsc_list = resp.data.dispsupplierifsc_list;
                unlockUI();
                $scope.cbosupplierifsc_code = '';
                $scope.txtsuplbank_name = '';
                $scope.txtsuplbranch_name = '';
                $scope.txtsuplbranch_address = '';
                $scope.txtsuplacctholder_name = '';
                $scope.txtsuplmicr_code = '';
                $scope.txtsuplbankacct_number = '';
                $scope.txtsuplconfirmbankacct_number = '';
                $scope.txtsuplacct_type = '';
                $scope.txtsupljoint_acct = '';
                $scope.txtsupljointacctholder_name = '';
                $scope.txtsuplchequebook_available = '';
                $scope.txtsuplacctopen_date = '';
            });

        }

        $scope.supplierifsc_change = function (cbosupplierifsc_code) {

            var supplier2bank_gid = $scope.cbosupplierifsc_code.supplier2bank_gid;

            var params = {
                supplier2bank_gid: supplier2bank_gid
            }
            var url = 'api/MstCreditOpsApplication/GetDispSuplBankAcctDtlView';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtsuplbank_name = resp.data.bank_name;
                $scope.txtsuplbranch_name = resp.data.branch_name;
                $scope.txtsuplbranch_address = resp.data.bank_address;
                $scope.txtsuplacctholder_name = resp.data.bankaccount_name;
                $scope.txtsuplmicr_code = resp.data.micr_code;
                $scope.txtsuplbankacct_number = resp.data.bankaccount_number;
                $scope.txtsuplconfirmbankacct_number = resp.data.confirmbankaccountnumber;
                $scope.txtsuplacct_type = resp.data.bankaccounttype_name;
                $scope.txtsupljoint_acct = resp.data.joinaccount_status;
                $scope.txtsupljointacctholder_name = resp.data.joinaccount_name;
                $scope.txtsuplchequebook_available = resp.data.chequebook_status;
                $scope.txtsuplacctopen_date = resp.data.accountopen_date;
                unlockUI();
            });
        }

        $scope.chequeleafsupplierdocumentUpload = function (val) {
            if (($scope.txtdocument_title == null) || ($scope.txtdocument_title == '') || ($scope.txtdocument_title == undefined)) {
                $("#chequefilefile22").val('');
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
                frm.append('disbursementsupplier_gid', '');
                frm.append('document_title', $scope.txtdocument_title);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    lockUI();
                    var url = 'api/MstCreditOpsApplication/DisbsupplierdocumentUpload';
                    console.log(url)
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                        $scope.disbsupplieruploaddocument_list = resp.data.disbsupplieruploaddocument_list;
                        unlockUI();

                        $("#chequefilefile22").val('');
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
        $scope.supplier_documentviewer = function (val1, val2) {
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

        $scope.supplier_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.downloadall_supplierdoc = function () {
            for (var i = 0; i < $scope.disbsupplieruploaddocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.disbsupplieruploaddocument_list[i].document_path, $scope.disbsupplieruploaddocument_list[i].document_name);
            }
        }

        $scope.supplier_uploaddocumentcancel = function (disbsupplierbankdocument_gid) {
            lockUI();
            var params = {
                disbsupplierbankdocument_gid: disbsupplierbankdocument_gid
            }
            var url = 'api/MstCreditOpsApplication/DeleteDisbsupplierdocument';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.disbsupplieruploaddocument_list = resp.data.disbsupplieruploaddocument_list;
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
        $scope.coapplicant_exportexcel = function () {
            var rmdisbursementrequest_gid = $location.search().rmdisbursementrequest_gid;
            var application_gid = $scope.cbosanctionref_no.application_gid;
            if ((application_gid == null) || (application_gid == '') || (application_gid == undefined)) {
                var application_gid = $location.search().application_gid;
            }
            lockUI();
            var params = {
                application_gid: application_gid,
                rmdisbursementrequest_gid: rmdisbursementrequest_gid
            }
            var url = 'api/MstCreditOpsApplication/CoApplicantEditExportExcel';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'success')

                }

            });
        }

        $scope.uploadCoApplicantDtl = function (val, val1, name) {
            var application_gid = $scope.cbosanctionref_no.application_gid;
            if ((application_gid == null) || (application_gid == '') || (application_gid == undefined)) {
                var application_gid = $location.search().application_gid;
            }

            var fileInput = document.getElementById('fileimportcoapplicant');
            var filePath = fileInput.value;

            $scope.fileinputvalue = filePath;

            // Allowing file type
            var allowedExtensions = /(\.xls|\.xlsx|\.csv)$/i;

            if (!allowedExtensions.exec(filePath)) {
                Notify.alert('File Format Not Supported!', 'warning')
                $modalInstance.close('closed');
                //fileInput.value = '';
            }
            else if (filePath.includes("Individual CoApplicant Details") == false) {
                Notify.alert('File Name / Template Not Supported!', 'warning')
                $modalInstance.close('closed');
            }
            else {
                var item = {
                    name: val[0].name,
                    file: val[0]
                };
                var frm = new FormData();
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
                frm.append('application_gid', application_gid);
                $scope.uploadfrm = frm;
            }
        }


        $scope.uploadExcelIndividual_CoApplicant = function () {

            if ($scope.fileinputvalue == '' || $scope.fileinputvalue == undefined || $scope.fileinputvalue == null) {
                Notify.alert('Kindly Select the Excel file', 'warning')
            }
            else {
                //var application_gid = $scope.cbosanctionref_no.application_gid;
                //frm.append('application_gid', application_gid);
                //$scope.uploadfrm = frm;
                var url = 'api/MstCreditOpsApplication/ImportExcelDisbCoApplicant';
                lockUI();
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    if (resp.data.status == true) {
                        unlockUI();
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });

                        var params = {
                            application_gid: application_gid
                        }
                        var url = 'api/MstCreditOpsApplication/GetDisbFarmerIndividualImportLog';
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.individualimport_List = resp.data.individualimport_List;
                        });

                        var url = 'api/MstCreditOpsApplication/GetDisbFarmerIndividualSummary';
                        SocketService.get(url).then(function (resp) {
                            $scope.farmerindividualsummary_list = resp.data.farmerindividualsummary_list;
                        });
                    }
                    else {
                        unlockUI();
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                    $("#fileimportcoapplicant").val('');
                });
            }

        }

        $scope.IndividualCoApplicantExcel_Cancel = function () {
            $("#fileimport").val('');
        };
    }
})();
