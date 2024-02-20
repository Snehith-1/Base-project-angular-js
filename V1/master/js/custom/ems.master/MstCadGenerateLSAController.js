(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCadGenerateLSAController', MstCadGenerateLSAController);

    MstCadGenerateLSAController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function MstCadGenerateLSAController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCadGenerateLSAController';
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        $scope.employee_gid = $location.search().employee_gid;
        var employee_gid = $scope.employee_gid;
        var lsgeneratelsa_gid = $location.search().generatelsa_gid;
        var lslsfilledlimitproduct = $location.search().lsfilledlimitproduct;
        var application2sanction_gid = $location.search().application2sanction_gid;
        var lsfollowup = $location.search().lsfollowup;

        var lspage = $scope.lspage;
        var vertical_gid;
        var vertical_code;
        activate();

        function activate() {
            
            if (lsfollowup == 'N') {
                if (lspage == 'CadLsaMaker')
                    $scope.makerapproval_pending = true;
                else
                    $scope.makercheckerapproval_pending = true;
            }
            else {
                $scope.makerapproval_pending = false;
                $scope.makercheckerapproval_pending = false;
            }
            $scope.lslsfilledlimitproduct = lslsfilledlimitproduct;
            $scope.tobe_recovered = false;
            $scope.already_recovered = false;
            $scope.yes = false;
            $scope.no = false;
            $scope.customer_pnl = true;
            $scope.showaddfeecharges = false;
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

            $scope.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                $scope.open1 = true;
            };

            vm.calender2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open2 = true;
            };
            vm.calender200 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open200 = true;
            };


            vm.calender3 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open3 = true;
            };

            vm.calender4 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open4 = true;
            };

            vm.calender5 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open5 = true;
            };

            vm.calender6 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open6 = true;
            };
            vm.calender10 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open10 = true;
            };
            vm.calender11 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open11 = true;
            };
            vm.calender12 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open12 = true;
            };
            vm.calender13 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open13 = true;
            };
            vm.calender14 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open14 = true;
            };
            vm.calender15 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open15 = true;
            };

            //$scope.dateOptions = {
            //    formatYear: 'yy',
            //    startingDay: 1
            //};

            //$scope.formats = ['dd-MM-yyyy'];
            //$scope.format = $scope.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
          


            var url = 'api/MstLSA/GetBankAccountTempClear';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
            });
            var params = {
                application_gid: application_gid
            }

            var url = 'api/MstCAD/GetCADBasicView';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtentity_gid = resp.data.entity_gid;
                $scope.txtentity_name = resp.data.entity_name;
                $scope.txtcreditapproved_date = resp.data.creditapproved_date;
                $scope.txtvertical_code = resp.data.vertical_code;
            });

            //Sanction Type Drop Down
            var url = 'api/MstCAD/GetCADBasicView';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.sanctiontype_list = resp.data.sanctiontype_list;
            });

            var url = 'api/MstCADApplication/GetApplicationBasicView';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtapplication_no = resp.data.application_no;
                $scope.txtbasiccustomer_name = resp.data.customer_name;
                $scope.txtbasicdesignation = resp.data.designation_type;
                $scope.txtvertical = resp.data.vertical_name;
                $scope.txtconstitution = resp.data.constitution_name;
                $scope.txtcredit_group = resp.data.creditgroup_name;
                //$scope.txtcontactperson_name = resp.data.contactperson_name;
                $scope.txtbusinessapproved_date = resp.data.businessapproved_date;
                $scope.txtccapproved_date = resp.data.ccapproved_date;
                $scope.txtregion = resp.data.region;
            });

            // Get Credit Approval Hirerichy
            var url = 'api/MstCreditApproval/Getcreditheadsview';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtcredit_head = resp.data.credithead_name;
                $scope.txtnational_manager = resp.data.nationalcredit_name;
                $scope.txtregional_manager = resp.data.regionalcredit_name;
                $scope.txtcredit_manager = resp.data.creditmanager_name;
                $scope.txtcredit_group = resp.data.creditgroup_name;
                $scope.remarks = resp.data.remarks;
            });

            var param = {
                application_gid: application_gid,
                employee_gid: employee_gid,
            }
            // Get RM Approval Hirerichy
            var url = 'api/MstApplicationApproval/Getapplicationhierarchylist';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.level_zero = resp.data.level_zero;
                $scope.level_one = resp.data.level_one;
                $scope.clusterhead = resp.data.clusterhead;
                $scope.zonalhead = resp.data.zonalhead;
                $scope.regionhead = resp.data.regionhead;
                $scope.businesshead = resp.data.businesshead;
            });

            var params = {
                application_gid: $scope.application_gid
            }
           
            var url = 'api/MstCADApplication/GetCadProductChargesDtl';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtoveralllimit_amt = resp.data.overalllimit_amount;             
                $scope.txtvalidity_year = resp.data.validityoveralllimit_year;
                $scope.txtvalidity_month = resp.data.validityoveralllimit_month;
                $scope.txtvalidity_days = resp.data.validityoveralllimit_days;
                $scope.txtcalculation_limitvalidity = resp.data.calculationoveralllimit_validity;
                $scope.loandtls_list = resp.data.mstLoan_list;           
                //$scope.buyer_list = resp.data.mstbuyer_list;
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

            var param = {
                application_gid: application_gid
            }
            var url = 'api/MstCADApplication/GetEditLoanDtl';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.mstloan_list = resp.data.mstloan_list;
                $scope.servicecharges_list = resp.data.servicecharges_list;
            });

            var url = 'api/MstCADApplication/GetProductChargesDtl';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.servicecharge_List = resp.data.servicecharge_List;
            });
            var param = {
                application_gid: application_gid
            }
            var url = 'api/MstCADFlow/GetProductChargesDtl';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.Collateral_list = resp.data.mstcollateral_list;
            });

            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstCC/GetScheduleMeeting';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.ccmember_list = resp.data.ccmember_list;
                $scope.otheruser_list = resp.data.otheruser_list;
            });

            var lsgeneratelsa_gid = $location.search().generatelsa_gid;
            if (lsgeneratelsa_gid != "" && lsgeneratelsa_gid != undefined) {
                var params = {
                    generatelsa_gid: lsgeneratelsa_gid
                }
                var url = 'api/MstLSA/GetLimitInfoDtl';
                lockUI();
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
                    generatelsa_gid: $location.search().generatelsa_gid,

                };
                var url = 'api/MstLSA/GetLSAGeneraldocument';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.filename_list = resp.data.UploadLSADocumentList;
                });

                var url = 'api/MstLSA/GetlsaFeeschargesDetail';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.lsaFeecharges_list = resp.data.lsaFeecharges_list;
                    }
                });

                var url = 'api/MstLSA/Getcompliancecheckinfo';
               lockUI();
               SocketService.getparams(url, params).then(function (resp) {
                   unlockUI();
                    if (resp.data.status == true) {
                        $scope.lsacompliancecheckdetail_gid = resp.data.lsacompliancecheckdetail_gid,
                    $scope.rdbnachmandateform_held = resp.data.nachmandateform_held,
                    $scope.txtnachmandateform_heldremarks = resp.data.nachmandateform_heldremarks;
                        if ($scope.txtnachmandateform_heldremarks == "")
                            $scope.txtnachmandateform_heldremarks = null;
                        $scope.rdbsignmatching_nachform = resp.data.signmatching_nachform,
                        $scope.txtsignmatching_nachformremarks = resp.data.signmatching_nachform;
                        if ($scope.txtsignmatching_nachformremarks == "")
                            $scope.txtsignmatching_nachformremarks = null;
                        $scope.rdbnamesign_kycmatching = resp.data.namesign_kycmatching,
                        $scope.txtnamesign_kycmatchingremarks = resp.data.namesign_kycmatchingremarks;
                        if ($scope.txtnamesign_kycmatchingremarks == "")
                            $scope.txtnamesign_kycmatchingremarks = null;
                        $scope.rdbescrowaccount_opened = resp.data.escrowaccount_opened,
                        $scope.txtescrowaccount_openedremarks = resp.data.escrowaccount_openedremarks;
                        if ($scope.txtescrowaccount_openedremarks == "")
                            $scope.txtescrowaccount_openedremarks = null;
                        $scope.rdbappropriate_stamping = resp.data.appropriate_stamping,
                        $scope.txtappropriate_stampingremarks = resp.data.appropriate_stampingremarks;
                        if ($scope.txtappropriate_stampingremarks == "")
                            $scope.txtappropriate_stampingremarks = null;
                        $scope.rdbrocfiling_initiated = resp.data.rocfiling_initiated,
                        $scope.txtrocfiling_initiatedremarks = resp.data.rocfiling_initiatedremarks;
                        if ($scope.txtrocfiling_initiatedremarks == "")
                            $scope.txtrocfiling_initiatedremarks = null;
                        $scope.rdbcersai_initiated = resp.data.cersai_initiated,
                        $scope.txtcersai_initiatedremarks = resp.data.cersai_initiatedremarks;
                        if ($scope.txtcersai_initiatedremarks == "")
                            $scope.txtcersai_initiatedremarks = null;
                        $scope.rdballdeferralcovenant_captured = resp.data.alldeferralcovenant_captured,
                        $scope.rdballpredisbursement_stipulated = resp.data.allpredisbursement_stipulated,
                        $scope.lblmaker_signature = resp.data.maker_signaturename;
                    }
                });
            }
            else {
                lockUI();
                var params = {
                    application_gid: $scope.application_gid,
                    application2sanction_gid: application2sanction_gid
                }
                var url = 'api/MstLSA/GetLSAApplicationLimitInfo';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.productdetails = resp.data.limitandproducts;
                        $scope.lbltotal_existinglimit = resp.data.total_existinglimit;
                        $scope.lbltotal_limitreleased = resp.data.total_limitreleased;
                    }
                });

            }

           // refreshcreditbankaccountsummary();
            var params = {
                generatelsa_gid: $location.search().generatelsa_gid,

            };
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

        }

        function refreshservicechargedetails() {
            var params = {
                generatelsa_gid: $location.search().generatelsa_gid
            }
            var url = 'api/MstLSA/GetlsaFeeschargesDetail';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.lsaFeecharges_list = resp.data.lsaFeecharges_list;
                }
            });
        }

        $scope.documentchargestatus_yes = function () {
            if ($scope.rdbdocumentcharge == 'Yes') {
                $scope.documentcharge_yes = true;
                $scope.documentcharge_no = false;
            }
        }
        $scope.documentchargestatus_no = function () {
            if ($scope.rdbdocumentcharge == 'No') {
                $scope.documentcharge_yes = false;
                $scope.documentcharge_no = true;
            }
        }


        $scope.nachform_no = function () {

            $scope.nach_no = true;

        }
        $scope.signmatch_no = function () {

            $scope.signmatching = true;

        }
        $scope.signmatch_kyc_no = function () {

            $scope.signmatch_kycprovide = true;

        }
        $scope.escrowac_no = function () {

            $scope.escrow_no = true;

        }
        $scope.stamping_no = function () {

            $scope.stamp_no = true;

        }
        $scope.roc_filling_no = function () {

            $scope.roc_no = true;

        }
        $scope.nachform_yes = function () {

            $scope.nach_no = false;

        }
        $scope.signmatch_yes = function () {

            $scope.signmatching = false;

        }
        $scope.signmatch_kyc_yes = function () {

            $scope.signmatch_kycprovide = false;

        }
        $scope.escrowac_yes = function () {

            $scope.escrow_no = false;

        }
        $scope.stamping_yes = function () {

            $scope.stamp_no = false;

        }
        $scope.roc_filling_yes = function () {

            $scope.roc_no = false;

        }
        $scope.rdbcersai_no = function () {

            $scope.cersai_no = true;

        }
        $scope.rdbcersai_yes = function () {

            $scope.cersai_no = false;

        }


        $scope.Back = function () {
            $location.url('app/MstCadLSADtlSummary?application_gid=' + application_gid + '&generatelsa_gid=' + lsgeneratelsa_gid + '&application2sanction_gid=' + application2sanction_gid + '&lsfilledlimitproduct=' + lslsfilledlimitproduct + '&lspage=' + lspage + '&lsfollowup=' + lsfollowup);
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


        $scope.PurposeofLoan_view = function (application2loan_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/PurposeOfLoan.html',
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
                var url = 'api/MstCADApplication/GetCadPurposeofLoan';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtpurposeof_loan = resp.data.enduse_purpose;

                });

                var params =
                {
                    application2loan_gid: application2loan_gid
                }
                var url = 'api/MstCADApplication/GetCadLoantoBuyerList';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.buyer_list = resp.data.mstbuyer_list;
                });
                var url = 'api/MstCADApplication/GetLoanProgramValueChain';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.program = resp.data.program;
                    //$scope.primaryvaluechain_name = resp.data.primaryvaluechain_name;
                    //$scope.secondaryvaluechain_name = resp.data.secondaryvaluechain_name;
                    $scope.product_gid = resp.data.product_gid;
                    $scope.product_name = resp.data.product_name;
                    $scope.variety_gid = resp.data.variety_gid;
                    $scope.variety_name = resp.data.variety_name;
                    $scope.sector_name = resp.data.sector_name;
                    $scope.category_name = resp.data.category_name;
                    $scope.botanical_name = resp.data.botanical_name;
                    $scope.alternative_name = resp.data.alternative_name;
                    $scope.mstproductdtl_list = resp.data.mstproductdtl_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.add_product = function (application2loan_gid, txtoveralllimit_amt) {
            var modalInstance = $modal.open({
                templateUrl: '/productadd.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.interchangeability_yes = function () {
                    $scope.interchangeabilityno = false;
                    $scope.interchangeabilityyes = true;
                    $scope.mandatoryfields = false;
                }

                $scope.interchangeability_no = function () {
                    $scope.interchangeabilityno = true;
                    $scope.interchangeabilityyes = false;
                    $scope.mandatoryfields = false;
                }

                //var param = {
                //    application2loan_gid: application2loan_gid,
                //}
                //var url = 'api/MstCAD/GetLoanDetail';
                //SocketService.getparams(url, param).then(function (resp) {
                //    unlockUI();
                //    $scope.rdbinterchangeability = resp.data.interchangeability;
                //    $scope.txtdocument_limit = resp.data.document_limit;
                //    $scope.txtexpiry_date = new Date(resp.data.expiry_date);
                //    if (resp.data.interchangeability == 'Yes') {
                //        $scope.interchangeabilityno = false;
                //        $scope.interchangeabilityyes = true;
                //        $scope.mandatoryfields = false;
                //        $scope.cboreport_structure = resp.data.report_structure;
                //    } else {
                //        $scope.interchangeabilityno = true;
                //        $scope.interchangeabilityyes = false;
                //        $scope.mandatoryfields = false;
                //        $scope.cboapplicable_condition = resp.data.report_structure;
                //    }
                //}); 

                $scope.Submit_productadditional = function () {
                    var report_structuregid = "", report_structure = "";
                    if ($scope.rdbinterchangeability == "Yes") {
                        report_structuregid = $scope.cboreport_structure.sanction2loanfacilitytype_gid;
                        report_structure = $scope.cboreport_structure;
                        $scope.cboapplicable_condition = "";
                    }
                    var params = {
                        application_gid: application_gid,
                        application2loan_gid: application2loan_gid,
                        interchangeability: $scope.rdbinterchangeability,
                        report_structuregid: report_structuregid,
                        report_structure: report_structure,
                        odlim_condition: $scope.cboapplicable_condition,
                        odlim_amount: txtoveralllimit_amt,
                        existing_limit: $scope.txtexisting_limit,
                        limit_released: $scope.txtrelease_limit,
                        limitinfo_remarks: $scope.txtremarks,
                    }
                    
                    var url = 'api/MstLSA/PostLimitInfo';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            var params = {
                                generatelsa_gid: resp.data.generatelsa_gid
                            }
                            var url = 'api/MstLSA/GetLimitInfoDtl';
                            lockUI();
                            SocketService.getparams(url, params).then(function (resp) {
                                unlockUI();
                                $scope.productdetails = resp.data.limitandproducts;
                                $scope.lbltotal_existinglimit = resp.data.total_existinglimit;
                                $scope.lbltotal_limitreleased = resp.data.total_limitreleased;
                            });
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                          

                            $modalInstance.close('closed');
                        }
                        else {
                            var params = {
                                generatelsa_gid: resp.data.generatelsa_gid
                            }
                            var url = 'api/MstLSA/GetLimitInfoDtl';
                            lockUI();
                            SocketService.getparams(url, params).then(function (resp) {
                                unlockUI();
                                $scope.productdetails = resp.data.limitandproducts;
                                $scope.lbltotal_existinglimit = resp.data.total_existinglimit;
                                $scope.lbltotal_limitreleased = resp.data.total_limitreleased;
                            });
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }
                    });
                }

                $scope.existinglimitchange = function () {
                    var input = document.getElementById('txtInput1').value;
                    var arr = input.split(',');
                    var i;
                    for (i = 0; i < arr.length; i++) {

                        var str = input.replace(',', '');
                        input = str;
                    }
                    var output = Number(str).toLocaleString('en-US');
                    var lswords_existinglimit = cmnfunctionService.fnConvertNumbertoWord(str);
                    var amount = new Intl.NumberFormat('en-IN').format(Number(str));
                    if (output == 'NaN') {
                        Notify.alert('Accept Numeric Only..!', {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.txtdocument_limit = "";

                    }
                    else {
                        document.getElementById('existinglimitamount_words').innerHTML = lswords_existinglimit;
                        $scope.txtdocument_limit = amount;

                        //if ((($scope.txtSanctionAmount.replace(/[\s,]+/g, '').trim()) - ($scope.totaldocumentlimitamount.replace(/[\s,]+/g, '').trim())) < ($scope.txtdocument_limit.replace(/[\s,]+/g, '').trim())) {
                        //    $scope.panel1 = false;
                        //}
                        //else {
                        //    $scope.panel1 = true;
                        //}
                    }
                    $scope.mandatoryfields = false;
                }

                $scope.releaselimitchange = function () {
                    var input = document.getElementById('txtInput2').value;
                    var arr = input.split(',');
                    var i;
                    for (i = 0; i < arr.length; i++) {

                        var str = input.replace(',', '');
                        input = str;
                    }
                    var output = Number(str).toLocaleString('en-US');
                    var lswords_releaselimit = cmnfunctionService.fnConvertNumbertoWord(str);
                    var amount = new Intl.NumberFormat('en-IN').format(Number(str));
                    if (output == 'NaN') {
                        Notify.alert('Accept Numeric Only..!', {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.txtdocument_limit = "";

                    }
                    else {
                        document.getElementById('releaselimitamount_words').innerHTML = lswords_releaselimit;
                        $scope.txtdocument_limit = amount;

                        //if ((($scope.txtSanctionAmount.replace(/[\s,]+/g, '').trim()) - ($scope.totaldocumentlimitamount.replace(/[\s,]+/g, '').trim())) < ($scope.txtdocument_limit.replace(/[\s,]+/g, '').trim())) {
                        //    $scope.panel1 = false;
                        //}
                        //else {
                        //    $scope.panel1 = true;
                        //}
                    }
                    $scope.mandatoryfields = false;
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.edit_product = function (limitproductinfodtl_gid, interchangeability, report_structure, odlim_condition, documented_limit, lsatotalreleased_approved) {
            var modalInstance = $modal.open({
                templateUrl: '/productedit.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.lblinterchangeability = interchangeability;
                $scope.lblreport_structure = report_structure;
                $scope.lblodlim_condition = odlim_condition;
                $scope.lbldocumented_limit = documented_limit;
                $scope.lsatotalreleased_approved = lsatotalreleased_approved;
                $scope.panel1 = true;
                if (interchangeability == "Yes") {
                    $scope.interchangeabilityno = false;
                    $scope.interchangeabilityyes = true;
                }

                if (interchangeability == "No") {
                    $scope.interchangeabilityno = true;
                    $scope.interchangeabilityyes = false;
                    $scope.mandatoryfields = false;
                }

                var param = {
                    limitproductinfodtl_gid: limitproductinfodtl_gid,
                }
                var url = 'api/MstLSA/GetLimitProductInfoDtl';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    unlockUI();
                    $scope.rdbinterchangeability = resp.data.interchangeability;
                    $scope.txtremarks = resp.data.limitinfo_remarks;
                    $scope.txtexisting_limit = resp.data.existing_limit;
                    $scope.txtrelease_limit = resp.data.limit_released;
                    if ($scope.txtexisting_limit != null && $scope.txtexisting_limit != undefined && $scope.txtexisting_limit != "") {
                        var str = $scope.txtexisting_limit.replace(/,/g, '');
                        var str = str.split('.')[0];
                        $scope.txtexisting_limit = Number(str).toLocaleString('en-IN');
                        document.getElementById('existinglimitamount_words').innerHTML = cmnfunctionService.fnConvertNumbertoWord(str);
                    }
                    if ($scope.txtrelease_limit != null && $scope.txtrelease_limit != undefined && $scope.txtrelease_limit != "") {
                        var str = $scope.txtrelease_limit.replace(/,/g, '');
                        var str = str.split('.')[0];
                        $scope.txtrelease_limit = Number(str).toLocaleString('en-IN');
                        document.getElementById('releaselimitamount_words').innerHTML = cmnfunctionService.fnConvertNumbertoWord(str);
                    }

                    if (resp.data.interchangeability == 'Yes') {
                        $scope.interchangeabilityno = false;
                        $scope.interchangeabilityyes = true;
                        $scope.mandatoryfields = false;
                        $scope.$parent.cboreport_structure = resp.data.report_structure;
                    } else {
                        $scope.interchangeabilityno = true;
                        $scope.interchangeabilityyes = false;
                        $scope.mandatoryfields = false;
                        $scope.$parent.cboapplicable_condition = resp.data.odlim_condition;
                    }
                });

                $scope.Submit_productupdate = function () {
                    var report_structuregid = "", report_structure = "";
                    if ($scope.rdbinterchangeability == "Yes") {
                        report_structuregid = $scope.cboreport_structure.sanction2loanfacilitytype_gid;
                        report_structure = $scope.cboreport_structure;
                        $scope.cboapplicable_condition = "";
                    }
                    var params = {
                        limitproductinfodtl_gid: limitproductinfodtl_gid,
                        interchangeability: $scope.rdbinterchangeability,
                        report_structuregid: report_structuregid,
                        report_structure: report_structure,
                        odlim_condition: $scope.cboapplicable_condition,
                        existing_limit: $scope.txtexisting_limit,
                        limit_released: $scope.txtrelease_limit,
                        limitinfo_remarks: $scope.txtremarks,
                        application_gid: application_gid
                    }
                    lockUI();
                    var url = 'api/MstLSA/Updatelimitproduct';
                    SocketService.post(url, params).then(function (resp) {                       
                        if (resp.data.status == true) {
                            $scope.lsgeneratelsa_gid = resp.data.generatelsa_gid;
                            var params = {
                                generatelsa_gid: $scope.lsgeneratelsa_gid
                            }
                            var url = 'api/MstLSA/GetlsaFeeschargesDetail';
                            lockUI();
                            SocketService.getparams(url, params).then(function (resp) {
                                unlockUI();
                                if (resp.data.status == true) {
                                    $scope.lsaFeecharges_list = resp.data.lsaFeecharges_list;
                                }
                            });
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            if (resp.data.lsfilledlimitproduct == "Y") {
                                $scope.lslsfilledlimitproduct = 'Y';
                                var lsgeneratelsa_gid = $scope.lsgeneratelsa_gid;
                                $location.url('app/MstCadGenerateLSA?application_gid=' + application_gid + '&generatelsa_gid=' + lsgeneratelsa_gid + '&application2sanction_gid=' + application2sanction_gid + '&lsfilledlimitproduct=Y' + '&lspage=' + lspage + '&lsfollowup=' + lsfollowup);
                            }
                            refreshproductdetails();    
                            $modalInstance.close('closed');
                        }
                        else {
                            refreshproductdetails();
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }
                    });
                }

                $scope.existinglimitchange = function () {
                    var input = document.getElementById('txtInput1').value;
                    var arr = input.split(',');
                    var i;
                    for (i = 0; i < arr.length; i++) {

                        var str = input.replace(',', '');
                        input = str;
                    }
                    var output = Number(str).toLocaleString('en-US');
                    var lswords_existinglimit = cmnfunctionService.fnConvertNumbertoWord(str);
                    var amount = new Intl.NumberFormat('en-IN').format(Number(str));
                    if (output == 'NaN') {
                        Notify.alert('Accept Numeric Only..!', {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.txtexisting_limit = "";

                    }
                    else {
                        document.getElementById('existinglimitamount_words').innerHTML = lswords_existinglimit;
                        $scope.txtexisting_limit = amount;
                    }
                    $scope.mandatoryfields = false;
                }

                $scope.releaselimitchange = function () {
                    var input = document.getElementById('txtInput2').value;
                    var arr = input.split(',');
                    var i;
                    for (i = 0; i < arr.length; i++) {

                        var str = input.replace(',', '');
                        input = str;
                    }
                    var output = Number(str).toLocaleString('en-US');
                    var lswords_releaselimit = cmnfunctionService.fnConvertNumbertoWord(str);
                    var amount = new Intl.NumberFormat('en-IN').format(Number(str));
                    if (output == 'NaN') {
                        Notify.alert('Accept Numeric Only..!', {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.txtrelease_limit = "";

                    }
                    else {
                        document.getElementById('releaselimitamount_words').innerHTML = lswords_releaselimit;
                        $scope.txtrelease_limit = amount;
                    }
                    //if (($scope.txtrelease_limit.replace(/[\s,]+/g, '').trim()) > (documented_limit.replace(/[\s,]+/g, '').trim() - $scope.txtexisting_limit.replace(/[\s,]+/g, '').trim())) {
                    //    $scope.panel1 = false;
                    //}
                    if (($scope.txtrelease_limit.replace(/[\s,]+/g, '').trim()) > (documented_limit.replace(/[\s,]+/g, '').trim() - lsatotalreleased_approved.replace(/[\s,]+/g, '').trim())) {
                        $scope.panel1 = false;
                    }
                    else {
                        $scope.panel1 = true;
                    }
                    $scope.mandatoryfields = false;
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.tabBankaccountpage = function () {
            var lslsfilledlimitproduct = $location.search().lsfilledlimitproduct;
            if (lslsfilledlimitproduct == "Y") {
                $scope.lslsfilledlimitproduct = 'Y';

                //var params = {
                //    generatelsa_gid: lsgeneratelsa_gid,

                //};
                //var url = 'api/MstLSA/GetLSABankAccountSummary';
                //SocketService.getparams(url, params).then(function (resp) {
                //    unlockUI();
                //    $scope.lsabankaccsummary_list = resp.data.MdlBankAccount;
                //});
                //var params = {
                //    application_gid: application_gid
                //}
                //var url = 'api/MstLSA/GetCreditBankAccountSummary';
                //SocketService.getparams(url, params).then(function (resp) {
                //    unlockUI();
                //    $scope.creditbankacc_list = resp.data.MdlBankAccount;
                //});
            }
        }

        $scope.tabfeesandcharges = function () {
            var lslsfilledlimitproduct = $location.search().lsfilledlimitproduct;
            if (lslsfilledlimitproduct == "Y") {
                $scope.lslsfilledlimitproduct = 'Y';
            }

            var params = {
                generatelsa_gid: $location.search().generatelsa_gid
            }
            var url = 'api/MstLSA/GetlsaFeeschargesDetail';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.lsaFeecharges_list = resp.data.lsaFeecharges_list;
                }
            });

        }
        $scope.tabcollateraldetails = function () {
            var lslsfilledlimitproduct = $location.search().lsfilledlimitproduct;
            if (lslsfilledlimitproduct == "Y") {
                $scope.lslsfilledlimitproduct = 'Y';
            }
        }

        $scope.tabgeneraldocuments = function () {
            var lslsfilledlimitproduct = $location.search().lsfilledlimitproduct;
            if (lslsfilledlimitproduct == "Y") {
                $scope.lslsfilledlimitproduct = 'Y';
            }
        }

        $scope.tabcompliancechecks = function () {
            var lslsfilledlimitproduct = $location.search().lsfilledlimitproduct;
            if (lslsfilledlimitproduct == "Y") {
                $scope.lslsfilledlimitproduct = 'Y';
            }
        }

        function refreshproductdetails() {
            var lsgeneratelsa_gid =  $location.search().generatelsa_gid;
            if (lsgeneratelsa_gid != "" && lsgeneratelsa_gid != undefined) {
                var params = {
                    generatelsa_gid: lsgeneratelsa_gid
                }
                var url = 'api/MstLSA/GetLimitInfoDtl';
                lockUI();
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
                
                var params = {
                    application_gid: $scope.application_gid,
                    application2sanction_gid: application2sanction_gid
                }
                var url = 'api/MstLSA/GetLSAApplicationLimitInfo';
                lockUI();
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
       

        function refreshcollaturalsummary() {
           
            var params = {
                application_gid: $scope.application_gid
            }

            var url = 'api/MstCADApplication/GetCadProductChargesDtl';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();              
                $scope.Collateral_list = resp.data.mstcollateral_list;
            });

        }
        function refreshbankaccountsummary() {
            lockUI();
            var params = {
                generatelsa_gid: $location.search().generatelsa_gid
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

        $scope.BankAccValidationEdit = function () {
            if ($scope.txtbankacct_noedit == $scope.txtconfirmbankacct_noedit) {
                var params = {
                    ifsc: $scope.txtIFSC_Code,
                    accountNumber: $scope.txtconfirmbankacct_noedit
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


        $scope.submit_bankdtl = function () {
            if (($scope.txtIFSC_Code == undefined) || ($scope.txtIFSC_Code == '') || ($scope.txtbankacct_no == undefined) || ($scope.txtbankacct_no == '') ||
               ($scope.txtconfirmbankacct_no == undefined) || ($scope.txtconfirmbankacct_no == '') || ($scope.txtacctholder_name == undefined) || ($scope.txtacctholder_name == '') ||
               ($scope.cboAccountType == undefined) || ($scope.cboAccountType == '') || ($scope.rdbJoint_Account == undefined) || ($scope.rdbJoint_Account == '') ||
               ($scope.rdbCheque_Book == undefined) || ($scope.rdbCheque_Book == '')) {
                Notify.alert('Enter All Mandatory Fields', 'warning');
            }
            else {
                if (($scope.rdbJoint_Account == 'Yes') && ($scope.txtjointacctholder_name == undefined) || ($scope.txtjointacctholder_name == '') && ($scope.rdbJoint_Account != 'No')) {
                    Notify.alert('Enter Joint Account Holder Name', 'warning');
                }
                else {
                    if ($scope.rdbJoint_Account == 'No') {
                        $scope.txtjointacctholder_name = "";
                    }
                    var params = {
                        application_gid: application_gid,
                        generatelsa_gid: $location.search().generatelsa_gid,
                        credit_gid: $scope.cboapplicationholder_name.credit_gid,
                        name: $scope.cboapplicationholder_name.holder_name,
                        stakeholder_type: $scope.cboapplicationholder_name.stakeholder_type,
                        ifsc_code: $scope.txtIFSC_Code,
                        bank_name: $scope.txtBank_Name,
                        branch_name: $scope.txtBranch_Name,
                        bank_address: $scope.txtBank_Address,
                        micr_code: $scope.txtMICR_Code,
                        bankaccount_number: $scope.txtbankacct_no,
                        confirmbankaccountnumber: $scope.txtconfirmbankacct_no,
                        accountholder_name: $scope.txtacctholder_name,
                        bankaccounttype_gid: $scope.cboAccountType.bankaccounttype_gid,
                        bankaccounttype_name: $scope.cboAccountType.bankaccounttype_name,
                        joint_account: $scope.rdbJoint_Account,
                        jointaccountholder_name: $scope.txtjointacctholder_name,
                        chequebookfacility_available: $scope.rdbCheque_Book,
                        accountopen_date1: $scope.txtAccountOpen_Add,
                        disbursement_accountstatus: $scope.rdbdisbusement_status,
                    }
                    var url = 'api/MstLSA/PostBankAccountDetails';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            $scope.bankdtladd_show = false;
                            $scope.ifscvalidation = "";
                            $scope.LSAchequeleafDocument_list = "";
                            document.getElementById("bankaccForm").reset();

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            refreshbankaccountsummary();
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
                frm.append('document_title', $scope.txtdocument_title);
                frm.append('generatelsa_gid', $location.search().generatelsa_gid);
                frm.append('lsabankaccdtl_gid', $scope.lsabankaccdtl_gid)
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    lockUI();
                    var url = 'api/MstLSA/lsachequeleafdocumentUpload';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                        $scope.LSAchequeleafDocument_list = resp.data.lsauploaddocument_list;
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


        $scope.chequeleafdocumentUploadEdit = function (val) {
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
                frm.append('document_title', $scope.txtdocument_title);
                frm.append('generatelsa_gid', $location.search().generatelsa_gid);
                frm.append('lsabankaccdtl_gid', $scope.lsabankaccdtl_gid)
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    lockUI();
                    var url = 'api/MstLSA/lsachequeleafdocumentUpload';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                        $scope.LSAchequeleafDocument_listEdit = resp.data.lsauploaddocument_list;
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

        $scope.documentviewerbank = function (val1, val2) {
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
        $scope.downloadsgen = function (val1, val2) {
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


        $scope.downloadsbank = function (val1, val2) {
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
        $scope.downloads = function (val1, val2, val3) {
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

        $scope.update_bankdtl = function (lsabankaccdtl_gid) {
            if (($scope.txtIFSC_Codeedit == undefined) || ($scope.txtIFSC_Codeedit == '') || ($scope.txtbankacct_noedit == undefined) || ($scope.txtbankacct_noedit == '') ||
                ($scope.txtconfirmbankacct_noedit == undefined) || ($scope.txtconfirmbankacct_noedit == '') || ($scope.txtacctholder_nameedit == undefined) || ($scope.txtacctholder_nameedit == '') ||
                ($scope.cboAccountTypeedit == undefined) || ($scope.cboAccountTypeedit == '') || ($scope.rdbJoint_Accountedit == undefined) || ($scope.rdbJoint_Accountedit == '') ||
                ($scope.rdbCheque_Bookedit == undefined) || ($scope.rdbCheque_Bookedit == '')) {
                Notify.alert('Enter All Mandatory Fields', 'warning');
            }
            else {
                if (($scope.rdbJoint_Accountedit == 'Yes') && ($scope.txtjointacctholder_nameedit == undefined) || ($scope.txtjointacctholder_nameedit == '') && ($scope.rdbJoint_Accountedit != 'No')) {
                    Notify.alert('Enter Joint Account Holder Name', 'warning');
                }
                else {
                    if ($scope.rdbJoint_Account == 'No') {
                        $scope.txtjointacctholder_nameedit = "";
                    }
                    var AccountTypeEdit = $('#AccountTypeEdit :selected').text();

                    if (AccountTypeEdit == '----- Select From Dropdown -----') {
                        AccountTypeEdit = '';
                    }
                    $scope.holder_name = "";
                    $scope.bankaccounttype_name = "";
                    var getinfo = $scope.applicationNameinfo.filter(function (el) { return el.credit_gid === $scope.cboapplicationholder_nameedit });
                    if (getinfo != null) {
                        $scope.holder_name = getinfo[0].holder_name;
                    }
                    var getbankname = $scope.accounttype_list.filter(function (el) { return el.bankaccounttype_gid === $scope.cboAccountTypeedit });
                    if (getbankname != null) {
                        $scope.bankaccounttype_name = getbankname[0].bankaccounttype_name;
                    }
                    var params = {
                        application_gid: application_gid,
                        generatelsa_gid: $location.search().generatelsa_gid,
                        lsabankaccdtl_gid: lsabankaccdtl_gid,
                        credit_gid: $scope.cboapplicationholder_nameedit,
                        name: $scope.holder_name,
                        stakeholder_type: $scope.lblstakeholder_type,
                        ifsc_code: $scope.txtIFSC_Codeedit,
                        bank_name: $scope.txtBank_Nameedit,
                        branch_name: $scope.txtBranch_Nameedit,
                        branch_address: $scope.txtBank_Addressedit,
                        micr_code: $scope.txtMICR_Codeedit,
                        bankaccount_number: $scope.txtbankacct_noedit,
                        confirmbankaccountnumber: $scope.txtconfirmbankacct_noedit,
                        accountholder_name: $scope.txtacctholder_nameedit,
                        bankaccounttype_gid: $scope.cboAccountTypeedit,
                        bankaccounttype_name: AccountTypeEdit,
                        joint_account: $scope.rdbJoint_Accountedit,
                        jointaccountholder_name: $scope.txtjointacctholder_nameedit,
                        chequebookfacility_available: $scope.rdbCheque_Bookedit,
                        accountopen_date: $scope.txtAccountOpen_Dateedit,
                        disbursement_accountstatus: $scope.rdbdisbusement_statusedit,
                    }
                    var url = 'api/MstLSA/PostUpdateBankAccountDetails';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            $scope.bankdtledit_show = false;
                            $scope.ifscvalidation = "";
                            $scope.LSAchequeleafDocument_list = "";
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            refreshbankaccountsummary();
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
        }

        $scope.bankacct_add = function () {
            $scope.bankdtladd_show = true;
            
            $scope.calenderbankadd = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                $scope.calenderbankaddopen = true;
            };
            vm.calender200 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open200 = true;
            };
            //vm.calenderbankedit = function ($event) {
            //    $event.preventDefault();
            //    $event.stopPropagation();
            //    vm.open6 = true;
            //};
            //vm.calenderbankadd = function ($event) {
            //    $event.preventDefault();
            //    $event.stopPropagation();
            //    vm.open6 = true;
            //};
            $scope.formats = ['dd-MM-yyyy'];
            $scope.format = $scope.formats[0];
            $scope.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

           
            var url = 'api/MstAppCreditUnderWriting/GetCreditAccountType';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.accounttype_list = resp.data.creditbankacc_list;
            });

            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstLSA/GetApplicationNameDetails';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.applicationNameinfo = resp.data.bankapplicationNameinfo;
            });
            $scope.lsabankaccdtl_gid = "";
            tmpdocumentclear();
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

        $scope.IFSCValidationedit = function () {

            if ($scope.txtIFSC_Codeedit.length == 11) {
                var params = {
                    ifsc: $scope.txtIFSC_Codeedit
                }
                lockUI();
                var url = 'api/Kyc/IfscVerification';
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.result.bank != "" && resp.data.result.bank != null) {
                        $scope.ifscvalidation = true;
                        $scope.txtBank_Nameedit = resp.data.result.bank;
                        $scope.txtBranch_Nameedit = resp.data.result.branch;
                        $scope.txtBank_Addressedit = resp.data.result.address;
                        $scope.txtMICR_Codeedit = resp.data.result.micr;

                        if (resp.data.result.micr != "" && resp.data.result.micr != null) {
                            $scope.micrempty = true;
                        }

                    } else if (resp.data.result.bank == "" || resp.data.result.bank == null) {
                        $scope.ifscvalidation = false;
                        Notify.alert('IFSC is not verified..!', 'warning');
                        $scope.txtBank_Nameedit = '';
                        $scope.txtBranch_Nameedit = '';
                        $scope.txtBank_Addressedit = '';
                        $scope.txtMICR_Codeedit = '';
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }

                });
            }
        }

        $scope.close = function () {
            $scope.bankdtladd_show = false;
            tmpdocumentclear();
            $scope.LSAchequeleafDocument_list = '';
        }

        function tmpdocumentclear() {
            var url = 'api/MstLSA/GetTmpClearchequeleafDocument';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
            });
        }

        $scope.downloadalllsa_add = function () {
            for (var i = 0; i < $scope.LSAchequeleafDocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.LSAchequeleafDocument_list[i].document_path, $scope.LSAchequeleafDocument_list[i].document_name);
            }
        }

        $scope.downloadalllsa = function () {
            for (var i = 0; i < $scope.LSAchequeleafDocument_listEdit.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.LSAchequeleafDocument_listEdit[i].document_path, $scope.LSAchequeleafDocument_listEdit[i].document_name);
            }
        }

        $scope.uploaddocumentcancel = function (lsachequeleafdocument_gid) {
            var params = {
                lsachequeleafdocument_gid: lsachequeleafdocument_gid
            }
            var url = 'api/MstLSA/GetDeleteLSAchequeleafDocument';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    var url = 'api/MstLSA/GetLSAChequeLeafTmpdoc';
                    lockUI();
                    SocketService.get(url).then(function (resp) {
                        unlockUI();
                        $scope.LSAchequeleafDocument_list = resp.data.lsauploaddocument_list;
                    });
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
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
            });
        }
        $scope.uploaddocumentcanceledit = function (lsachequeleafdocument_gid, lsabankaccdtl_gid) {
            var params = {
                lsachequeleafdocument_gid: lsachequeleafdocument_gid
            }
            var url = 'api/MstLSA/GetDeleteLSAchequeleafDocument';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    var params = {
                        lsabankaccdtl_gid: lsabankaccdtl_gid
                    }
                    var url = 'api/MstLSA/GetLSAChequeLeafTmpdocedit';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.LSAchequeleafDocument_listEdit = resp.data.lsauploaddocument_list;
                    });
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
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
            });
        }


        $scope.bankacct_delete = function (lsabankaccdtl_gid) {
            var params = {
                lsabankaccdtl_gid: lsabankaccdtl_gid
            }
            var url = 'api/MstLSA/GetDeleteLSABankAccountdtl';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    refreshbankaccountsummary();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
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

        $scope.bankacct_edit = function (lsabankaccdtl_gid) {
            lockUI();
            $scope.bankdtledit_show = true;
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
            var url = 'api/MstAppCreditUnderWriting/GetCreditAccountType';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.accounttype_list = resp.data.creditbankacc_list;
            });
            $scope.lsabankaccdtl_gid = lsabankaccdtl_gid;
            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstLSA/GetApplicationNameDetails';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.applicationNameinfo = resp.data.bankapplicationNameinfo;
            });
            tmpdocumentclear();

            var params = {
                lsabankaccdtl_gid: lsabankaccdtl_gid
            }
            var url = 'api/MstLSA/GetLSABankAccountdetail';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.cboapplicationholder_nameedit = resp.data.credit_gid,
                    //$scope.cboapplicationholder_name = resp.data.name,
                    $scope.lblstakeholder_type = resp.data.stakeholder_type,
                    $scope.txtIFSC_Codeedit = resp.data.ifsc_code,
                        $scope.txtBank_Nameedit = resp.data.bank_name,
                        $scope.txtBranch_Nameedit = resp.data.branch_name, 
                        $scope.txtBank_Addressedit = resp.data.bank_address,
                        $scope.txtMICR_Codeedit = resp.data.micr_code,
                    $scope.txtbankacct_noedit = resp.data.bankaccount_number,
                    $scope.txtconfirmbankacct_noedit = resp.data.confirmbankaccountnumber,
                    $scope.txtacctholder_nameedit = resp.data.accountholder_name,
                    $scope.cboAccountTypeedit = resp.data.bankaccounttype_gid,
                    $scope.rdbJoint_Accountedit = resp.data.joint_account,
                    $scope.txtjointacctholder_nameedit = resp.data.jointaccountholder_name,
                    $scope.rdbCheque_Bookedit = resp.data.chequebookfacility_available,
                    $scope.txtAccountOpen_Dateedit = resp.data.accountopendate,
                    $scope.rdbdisbusement_statusedit = resp.data.disbursement_accountstatus,
                    unlockUI();
                    //refreshLSAchequeleafDocument(lsabankaccdtl_gid);

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

            });

            //var params = {
            //    lsabankaccdtl_gid: lsabankaccdtl_gid
            //}
            var url = 'api/MstLSA/GetLSAChequeLeafTmpdocedit';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.LSAchequeleafDocument_listEdit = resp.data.lsauploaddocument_list;
            });

            $scope.changestakeholder = function (cboapplicationholder_gid) {
                var getinfo = $scope.applicationNameinfo.filter(function (el) { return el.credit_gid === cboapplicationholder_gid });
                if (getinfo != null) {
                    $scope.lblstakeholder_type = getinfo[0].stakeholder_type;
                }
            }
        }

        $scope.edit_close = function () {
            $scope.bankdtledit_show = false;
        }

        $scope.collateraldtl_add = function () {
            lockUI();
            var params = {
                application_gid: application_gid
            }
            var url = "api/MstLSA/GetlsaProductname";
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.loanproductlist = resp.data.LsaProductname;
            });
            $scope.calenderGuideline = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                $scope.opencalenderGuideline = true;
            };
            $scope.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            $scope.formats = ['dd-MM-yyyy'];
            $scope.format = $scope.formats[0];
            var params = {
                generatelsa_gid: $location.search().generatelsa_gid
            }
            var url = 'api/MstLSA/GetCollateralTmpClear';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
            });
            $scope.CollateralDocumentList = "";
            $scope.addcollateraldtl_show = true;
        }

        $scope.close_collateral = function () {
            $scope.addcollateraldtl_show = false;
            $scope.CollateralDocumentList = '';
        }

        $scope.collatural_edit = function (application2loan_gid) {
            //activate();
            $scope.application2loan_gid = application2loan_gid;
            var params = {
                generatelsa_gid: $location.search().generatelsa_gid
            }
            var url = 'api/MstLSA/GetCollateralTmpClear';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
            });
            $scope.calenderGuideline = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                $scope.opencalenderGuideline = true;
            };
            $scope.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            $scope.formats = ['dd-MM-yyyy'];
            $scope.format = $scope.formats[0];
            var params = {
                application2loan_gid: application2loan_gid
            }
            var url = 'api/MstLSA/GetlsaCollateralDetail';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                
                $scope.lblproduct_type = resp.data.product_type;
                $scope.cboSourceTypeedit = resp.data.source_type;
                $scope.txtguidelinevalueedit = resp.data.guideline_value,
                $scope.txtguideline_assessedonedit = resp.data.guideline_date,
                $scope.txtmarketValueedit = resp.data.market_value,
                $scope.txtmarketvalue_assessedonedit = resp.data.marketvalue_date,
                $scope.txtforcedsource_valueedit = resp.data.forcedsource_value,
                $scope.txtcollateralFSV_valueedit = resp.data.collateralFSV_value,
                $scope.txtforcedvalueassessed_onedit = resp.data.forcedvalue_date,
                $scope.txtcollateralobservation_summaryedit = resp.data.collateralobservation_summary;
                unlockUI();
            });
            var params = {
                application2loan_gid: application2loan_gid
            }
            var url = 'api/MstLSA/GetLSACollateraldocument';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.CollateralDocumentListEdit = resp.data.UploadLSADocumentList;
            });

            $scope.editcollateraldtl_show = true
        }

        $scope.close_collateraledit = function () {
            $scope.editcollateraldtl_show = false;
        }

        $scope.disbursement_account = function (primarygid, disbursement_accountstatus, LSABankaccount) {
            var modalInstance = $modal.open({
                templateUrl: '/DisbursementAccount.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var lsabankaccdtl_gid = "", creditbankdtl_gid = "";
                var lslsfilledlimitproduct = $location.search().lsfilledlimitproduct;
                if (lslsfilledlimitproduct == "Y") {
                    $scope.lslsfilledlimitproduct = 'Y';

                   
                }

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
                       /* $scope.viewinfo = resp.data;*/
                        $scope.rdbDisbursement_Account = disbursement_accountstatus;
                    }
                    else {
                        unlockUI();
                    }

                });
                if (LSABankaccount == 'Y')
                    lsabankaccdtl_gid = primarygid;
                else
                    creditbankdtl_gid = primarygid;

                $scope.Submit_Disbursement = function () {
                    var params = {
                        disbursement_accountstatus: $scope.rdbDisbursement_Account,
                        creditbankdtl_gid: creditbankdtl_gid,
                        lsabankaccdtl_gid: lsabankaccdtl_gid,
                    }
                    lockUI();
                    var url = 'api/MstLSA/updateDisbursementStatus';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            //activate();
                            $modalInstance.close('closed');
                            refreshcreditbankaccountsummary();
                            refreshbankaccountsummary();
                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }
                    });
                }


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.txtguidelinevaluechange = function () {
            var input = document.getElementById('GuidelineValue').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lsguideline_value = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtguidelinevalue = "";
            }
            else {
                $scope.txtguidelinevalue = output;
                document.getElementById('words_guidelinevalue').innerHTML = lsguideline_value;
            }
        }

      

        $scope.txtMarketValuechange = function () {
            var input = document.getElementById('MarketValue').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lsmarket_value = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtmarketValue = "";
            }
            else {
                $scope.txtmarketValue = output;
                document.getElementById('words_marketvalue').innerHTML = lsmarket_value;
            }
        }


        $scope.txtForcedSourceValuechange = function () {
            var input = document.getElementById('ForcedSourceValue').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lsforcedsource_value = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtforcedsource_value = "";
            }
            else {
                $scope.txtforcedsource_value = output;
                document.getElementById('words_forcedsourcevalue').innerHTML = lsforcedsource_value;
            }
        }


        $scope.txtCollateralFSVvaluechange = function () {
            var input = document.getElementById('CollateralFSVvalue').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_collateralfsv = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtcollateralFSV_value = "";
            }
            else {
                $scope.txtcollateralFSV_value = output;
                document.getElementById('words_collateralfsv').innerHTML = lswords_collateralfsv;
            }
        }



        $scope.txteditguidelinevaluechange = function () {
            var input = document.getElementById('EditGuidelineValue').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lseditguideline_value = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtguidelinevalueedit = "";
            }
            else {
                $scope.txtguidelinevalueedit = output;
                document.getElementById('words_editguidelinevalue').innerHTML = lseditguideline_value;
            }
        }



        $scope.txteditMarketValuechange = function () {
            var input = document.getElementById('EditMarketValue').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lseditmarket_value = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtmarketValueedit = "";
            }
            else {
                $scope.txtmarketValueedit = output;
                document.getElementById('words_editmarketvalue').innerHTML = lseditmarket_value;
            }
        }


        $scope.txteditForcedSourceValuechange = function () {
            var input = document.getElementById('EditForcedSourceValue').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lseditforcedsource_value = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtforcedsource_valueedit = "";
            }
            else {
                $scope.txtforcedsource_valueedit = output;
                document.getElementById('words_editforcedsourcevalue').innerHTML = lseditforcedsource_value;
            }
        }


        $scope.txteditCollateralFSVvaluechange = function () {
            var input = document.getElementById('txteditcollateralFSV_value').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lseditwords_collateralfsv = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtcollateralFSV_valueedit = "";
            }
            else {
                $scope.txtcollateralFSV_valueedit = output;
                document.getElementById('words_editcollateralfsv').innerHTML = lseditwords_collateralfsv;
            }
        }


        $scope.submit_collateral = function () {
            if (($scope.cboSourceType == 'Fixed' || $scope.cboSourceType == 'Moving' || $scope.cboSourceType == 'Property' ||
                $scope.cboSourceType == 'Deposit') && ($scope.txtcollateralobservation_summary == '' ||
                $scope.txtcollateralobservation_summary == null)) {
                Notify.alert('Kindly Fill Observation Summary Detail', 'warning')
            }
            else {
                var params = {
                    application_gid: application_gid,
                    generatelsa_gid: $location.search().generatelsa_gid,
                    application2loan_gid: $scope.cboProductTypelist.application2loan_gid,
                    source_type: $scope.cboSourceType,
                    guideline_value: $scope.txtguidelinevalue,
                    guideline_assessedon: $scope.txtguideline_assessedon,
                    market_value: $scope.txtmarketValue,
                    marketvalue_assessedon: $scope.txtmarketvalue_assessedon,
                    forcedsource_value: $scope.txtforcedsource_value,
                    collateralFSV_value: $scope.txtcollateralFSV_value,
                    forcedvalue_assessedon: $scope.txtforcedvalueassessed_on,
                    collateralobservation_summary: $scope.txtcollateralobservation_summary
                }
                var url = 'api/MstLSA/Postcollateraldetails';
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
                        $("#collateraladdform")[0].reset();
                        $scope.CollateralDocumentList = "";
                        document.getElementById('words_guidelinevalue').innerHTML = '';
                        document.getElementById('words_marketvalue').innerHTML = '';
                        document.getElementById('words_forcedsourcevalue').innerHTML = '';
                        document.getElementById('words_collateralfsv').innerHTML = '';
                        $scope.addcollateraldtl_show = false;
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

        $scope.update_collateral = function (application2loan_gid) {
            if (($scope.cboSourceTypeedit == 'Fixed' || $scope.cboSourceTypeedit == 'Moving' || $scope.cboSourceTypeedit == 'Property' ||
                $scope.cboSourceTypeedit == 'Deposit') && ($scope.txtcollateralobservation_summaryedit == '' ||
                $scope.txtcollateralobservation_summaryedit == null)) {
                Notify.alert('Kindly Fill Observation Summary Detail', 'warning')
            }
            else {
                var params = {
                    application_gid: application_gid,
                    generatelsa_gid: $location.search().generatelsa_gid,
                    application2loan_gid: application2loan_gid,
                    source_type: $scope.cboSourceTypeedit,
                    guideline_value: $scope.txtguidelinevalueedit,
                    guideline_assessedon: $scope.txtguideline_assessedonedit,
                    market_value: $scope.txtmarketValueedit,
                    marketvalue_assessedon: $scope.txtmarketvalue_assessedonedit,
                    forcedsource_value: $scope.txtforcedsource_valueedit,
                    collateralFSV_value: $scope.txtcollateralFSV_valueedit,
                    forcedvalue_assessedon: $scope.txtforcedvalueassessed_onedit,
                    collateralobservation_summary: $scope.txtcollateralobservation_summaryedit
                }
                var url = 'api/MstLSA/Postcollateraldetails';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                       
                        $scope.editcollateraldtl_show = false;
                        //$("#collateraleditform")[0].reset();
                        $scope.CollateralDocumentList = "";
                        //document.getElementById('words_guidelinevalue').innerHTML = '';
                        //document.getElementById('words_marketvalue').innerHTML = '';
                        //document.getElementById('words_forcedsourcevalue').innerHTML = '';
                        //document.getElementById('words_collateralfsv').innerHTML = '';
                        $scope.addcollateraldtl_show = false;
                        refreshcollaturalsummary();
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

                $scope.downloadcollateraldoc = function (val1, val2) {
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

                $scope.downloadall = function () {
                    for (var i = 0; i < $scope.document_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.document_list[i].document_path, $scope.document_list[i].document_name);
                    }
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            }

        }
        /// Start - Processing Fee charges Add ...//
        $scope.doc_charges = function (lsafeescharge_gid, product_type) {
            $scope.showaddfeecharges = true;
            lockUI();
            $scope.panelalreadycollected = true;
            $scope.showtobecollecteddiv = false;
          
            $scope.lblchargeproduct_type = product_type;
            $scope.hdnlsafeescharge_gid = lsafeescharge_gid;
            var url = "api/MstLSA/GetBankName";
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.BankNamelist = resp.data.MdlBankName;
            });
            $scope.checklist = [
               { id: 1, typename: 'Processing Fees' },
               { id: 2, typename: 'Document Charges' },
               { id: 3, typename: 'Field Visit Charges' },
               { id: 4, typename: 'Adhoc Fee' },
               { id: 5, typename: 'Term Life Insurance' },
               { id: 6, typename: 'Personal Accident Insurance' }
            ]
            var getalreadyaddedcharges = $scope.lsaFeecharges_list.filter(function (el) { return el.lsafeescharge_gid === $scope.hdnlsafeescharge_gid });

            angular.forEach($scope.checklist, function (value, key) {
                if (getalreadyaddedcharges && getalreadyaddedcharges.length > 0) {
                    if (getalreadyaddedcharges[0].processingfees_flag == "Y" && value.id == 1)
                        value.alreadyaddedcharges = "Y";
                    else if (getalreadyaddedcharges[0].documentcharges_flag == "Y" && value.id == 2)
                        value.alreadyaddedcharges = "Y";
                    else if (getalreadyaddedcharges[0].fieldvisitcharges_flag == "Y" && value.id == 3)
                        value.alreadyaddedcharges = "Y";
                    else if (getalreadyaddedcharges[0].adhocfee_flag == "Y" && value.id == 4)
                        value.alreadyaddedcharges = "Y";
                    else if (getalreadyaddedcharges[0].termlifeinsurance_flag == "Y" && value.id == 5)
                        value.alreadyaddedcharges = "Y";
                    else if (getalreadyaddedcharges[0].personalaccidentinsurance == "Y" && value.id == 6)
                        value.alreadyaddedcharges = "Y";
                    else
                        value.alreadyaddedcharges = "N";
                }
                else {
                    value.alreadyaddedcharges = "N";
                }
            });

            $scope.typelist = $scope.checklist.filter(function (el) { return el.alreadyaddedcharges === "N" });

            $scope.calender7 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                $scope.open7 = true;
            };
            $scope.calenderneft = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                $scope.openneft = true;
            };

            $scope.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            $scope.formats = ['dd-MM-yyyy'];
            $scope.format = $scope.formats[0];
            //var modalInstance = $modal.open({
            //    templateUrl: '/DocumentCharges.html',
            //    controller: ModalInstanceCtrl,
            //    backdrop: 'static',
            //    keyboard: false,
            //    size: 'lg'
            //});
            //ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            //function ModalInstanceCtrl($scope, $modalInstance) {

            //    $scope.ok = function () {
            //        $modalInstance.close('closed');
            //    };
            //}
        }

        $scope.Closechargepanel = function () {
            $scope.showaddfeecharges = false;
            documentcharge_clear();
        }

        //$scope.documentchargestatus_yes = function () {
        //    if ($scope.rdbdocumentcharge == "Yes") {
        //        $scope.Yes = false;
        //        $scope.No = true;
        //    }
        //}
        //$scope.documentchargestatus_no = function () {
        //    if ($scope.rdbdocumentcharge == "No") {
        //        $scope.Yes = true;
        //        $scope.No = false;
        //    }
        //}

        $scope.documentchargestatus_yes = function () {
            if ($scope.rdbdocumentcharge == 'Yes') {
                $scope.documentcharge_yes = true;
                $scope.documentcharge_no = false;
            }
        }
        $scope.documentchargestatus_no = function () {
            if ($scope.rdbdocumentcharge == 'No') {
                $scope.documentcharge_yes = false;
                $scope.documentcharge_no = true;
            }
        }

        function documentcharge_clear() {
            $scope.cbotypename = "",
            $scope.application_totalamount = "",
            $scope.rdbdocumentcharge = "",
            $scope.txtdocumentcharge_remarks = "",
           $scope.txtdoc_alreadyrecoveredamount = "",
            $scope.txtalreadycollectgst_inpercentage = "",
            $scope.rdbgst_alreadycollected = "",
            $scope.txtalreadycollecttotal_amount = "",
           $scope.rdbalreadycollected_amount = "",
            $scope.txtalreadycollectdoc_cheque_no = "",
            $scope.txtalreadycollectdoc_cheque_date = "",
            $scope.txtalreadycollectaccount_number = "",
            $scope.txtcollectedrecovered_amt = "",
           $scope.txtcollectedgst_inpercentage = "",
            $scope.rdbcollectedgst = "",
            $scope.txtcollectedtotal_amount = "",
            $scope.rdbcollectedcollected_amount = "",
            $scope.txtcollecteddoc_cheque_no = "",
            $scope.txtcollecteddoc_cheque_date = "",
            $scope.cboalreadycollectbank_name = "",
            $scope.cbocollectedbank_name = "",
            $scope.txtcollectedaccount_number = "";
        }
        $scope.alreadycolamount_deduct = function () {
            $scope.alreadyamount_collect = false;
            $scope.alreadyamount_deduct = true;
        }

        $scope.alreadycolamount_collect = function () {
            $scope.alreadyamount_collect = true;
            $scope.alreadyamount_deduct = false;
        }

        $scope.remainingamount_collect = function () {
            $scope.remainingamt_collect = true;
            $scope.remainingamt_deduct = false;
        }

        $scope.remainingamount_deduct = function () {
            $scope.remainingamt_collect = false;
            $scope.remainingamt_deduct = true;
        }

        $scope.collectedrecoveredamtchange = function () {
            var input = document.getElementById('CollectedRecoverdAmount').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lscollectedrecovered_amt = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtcollectedrecovered_amt = "";
            }
            else {
                $scope.txtcollectedrecovered_amt = output;
                document.getElementById('words_collectedrecoveredamt').innerHTML = lscollectedrecovered_amt;
            }
        }

        

        $scope.alreadyrecoveredamountchange = function () {
            var input = document.getElementById('RecoveredAmount').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lsrecovered_amount = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtdoc_alreadyrecoveredamount = "";
            }
            else {
                $scope.txtdoc_alreadyrecoveredamount = output;
                document.getElementById('words_alreadyrecoverdamount').innerHTML = lsrecovered_amount;
                var total_amount = ($scope.application_totalamount.replace(/[\s,]+/g, '').trim());
                var already_recoveramount = $scope.txtdoc_alreadyrecoveredamount.replace(/[\s,]+/g, '').trim();
                if (parseInt(total_amount) >= parseInt(already_recoveramount)) {
                    $scope.panelalreadycollected = true;
                }
                else {
                    $scope.panelalreadycollected = false;
                }
                $scope.txtcollectedrecovered_amt = (parseInt(total_amount)) - (parseInt(already_recoveramount));
                if ($scope.txtcollectedrecovered_amt <= 0) {
                    $scope.showtobecollecteddiv = false;
                }
                else {
                    $scope.showtobecollecteddiv = true;
                }
            }
        }

        
        //$scope.changealreadyexclusivegst = function () {
        //    $scope.txtalreadycollectgst_inpercentage = "0";
        //    $scope.txtalreadycollecttotal_amount = "";
        //}

        $scope.changealreadyinclusivegst = function () {
            if ($scope.txtalreadycollectgst_inpercentage != undefined && $scope.txtalreadycollectgst_inpercentage != "") {
                var amount = ($scope.txtdoc_alreadyrecoveredamount.replace(/[\s,]+/g, '').trim() * $scope.txtalreadycollectgst_inpercentage) / 100;
                var doc = ($scope.txtdoc_alreadyrecoveredamount.replace(/[\s,]+/g, '').trim());
                if ($scope.rdbgst_alreadycollected === "Inclusive of GST") { 
                    var output = parseFloat(parseInt(doc) - amount).toFixed(2);
                    $scope.txtalreadycollecttotal_amount = new Intl.NumberFormat('en-IN').format(Number(output));
                }
                else { 
                    var output = parseFloat(parseInt(doc) + amount).toFixed(2);
                    $scope.txtalreadycollecttotal_amount = new Intl.NumberFormat('en-IN').format(Number(output));
                }
            }
        }

        //$scope.changecollectedexclusivegst = function () {
        //    $scope.txtcollectedgst_inpercentage = "0";
        //    $scope.txtcollectedtotal_amount = "";
        //}

        $scope.changecollectedinclusivegst = function () {
            var amount = "0"; var doc = "";
            if ($scope.txtcollectedgst_inpercentage != undefined && $scope.txtcollectedgst_inpercentage != "") {
                try {
                    amount = ($scope.txtcollectedrecovered_amt.replace(/[\s,]+/g, '').trim() * $scope.txtcollectedgst_inpercentage) / 100;
                    doc = ($scope.txtcollectedrecovered_amt.replace(/[\s,]+/g, '').trim());
                }
                catch (err) {
                    amount = ($scope.txtcollectedrecovered_amt * $scope.txtcollectedgst_inpercentage) / 100;
                    doc = ($scope.txtcollectedrecovered_amt);
                }
                if ($scope.rdbcollectedgst == "Inclusive of GST") { 
                    var output = parseFloat(parseInt(doc) - amount).toFixed(2);
                    $scope.txtcollectedtotal_amount = new Intl.NumberFormat('en-IN').format(Number(output));
                }
                else { 
                    var output = parseFloat(parseInt(doc) + amount).toFixed(2);
                    $scope.txtcollectedtotal_amount = new Intl.NumberFormat('en-IN').format(Number(output));
                }
            }
        }

        $scope.changechargetype = function (cbotypename) {
            var getlsafeecharges = $scope.lsaFeecharges_list.filter(function (el) { return el.lsafeescharge_gid === $scope.hdnlsafeescharge_gid });
            if (getlsafeecharges && getlsafeecharges.length > 0) {
                $scope.lblcharge_name = cbotypename.typename;
                switch (cbotypename.id) {
                    case 1:
                        $scope.application_totalamount = getlsafeecharges[0].processing_fee;
                        break;
                    case 2:
                        $scope.application_totalamount = getlsafeecharges[0].doc_charges;
                        break;
                    case 3:
                        $scope.application_totalamount = getlsafeecharges[0].fieldvisit_charge;
                        break;
                    case 4:
                        $scope.application_totalamount = getlsafeecharges[0].adhoc_fee;
                        break;
                    case 5:
                        $scope.application_totalamount = getlsafeecharges[0].life_insurance;
                        break;
                    case 6:
                        $scope.application_totalamount = getlsafeecharges[0].acct_insurance;
                        break;
                    default:
                        $scope.application_totalamount = "0";
                }
            }
        }

        $scope.documentcharge_submit = function () {
            if ($scope.showtobecollecteddiv == true) {
                if (($scope.txtcollectedrecovered_amt == undefined) || ($scope.txtcollectedrecovered_amt == '') || ($scope.txtcollectedrecovered_amt == null) ||
                    ($scope.rdbcollectedgst == undefined) || ($scope.rdbcollectedgst == '') || ($scope.rdbcollectedgst == null)
                    || ($scope.txtcollectedgst_inpercentage == undefined) || ($scope.txtcollectedgst_inpercentage == '') || ($scope.txtcollectedgst_inpercentage == null) ||
                    ($scope.txtcollectedtotal_amount == undefined) || ($scope.txtcollectedtotal_amount == '') || ($scope.txtcollectedtotal_amount == null) ||
                    ($scope.rdbcollectedcollected_amount == undefined) || ($scope.rdbcollectedcollected_amount == '') || ($scope.rdbcollectedcollected_amount == null)) {

                    Notify.alert('Add all mandatory fields ', 'warning');
                }
                else { 
                var lsalreadybank_namegid = "", lsalreadybank_name = "";
                if ($scope.cboalreadycollectbank_name != undefined && $scope.cboalreadycollectbank_name != "") {
                    lsalreadybank_namegid = $scope.cboalreadycollectbank_name.bankname_gid;
                    lsalreadybank_name = $scope.cboalreadycollectbank_name.bank_name;
                }
                var lsbank_namegid = "", lsbank_name = "";
                if ($scope.cbocollectedbank_name != undefined && $scope.cbocollectedbank_name != "") {
                    lsbank_namegid = $scope.cbocollectedbank_name.bankname_gid;
                    lsbank_name = $scope.cbocollectedbank_name.bank_name;
                }
                if ($scope.txtdoc_recovered_amount != '' || $scope.txtdoc_recovered_amount != null) {
                    var lsalreadycol_recoveredamount = $scope.txtdoc_recovered_amount
                }
                else {
                    var lsalreadycol_recoveredamount = '0.00';
                }
                if ($scope.txtalreadycollecttotal_amount != '' || $scope.txtalreadycollecttotal_amount != null) {
                    var lsalreadycol_totalamount = $scope.txtalreadycollecttotal_amount
                }
                else {
                    var lsalreadycol_totalamount = '0.00';
                }

                var params = {
                    lsafeescharge_gid: $scope.hdnlsafeescharge_gid,
                    generatelsa_gid: $location.search().generatelsa_gid,
                    charge_typeid: $scope.cbotypename.id,
                    charge_typename: $scope.cbotypename.typename,
                    charge_amount: $scope.application_totalamount,
                    charge_applicable: $scope.rdbdocumentcharge,
                    charge_remarks: $scope.txtdocumentcharge_remarks,
                    alreadycol_recoveredamount: lsalreadycol_recoveredamount,
                    alreadycol_gstinpercentage: $scope.txtalreadycollectgst_inpercentage,
                    alreadycol_gst: $scope.rdbgst_alreadycollected,
                    alreadycol_totalamount: lsalreadycol_totalamount,
                    //alreadycol_remainingamountcollected: $scope.rdbalreadycollected_amount,
                    //alreadycol_Chequenodetails: $scope.txtalreadycollectdoc_cheque_no,
                    //alreadycol_Cheque_date: $scope.txtalreadycollectdoc_cheque_date,
                    //alreadycol_banknamegid: lsalreadybank_namegid,
                    //alreadycol_bankname: lsalreadybank_name,
                    /*   alreadycol_accountnumber: $scope.txtalreadycollectaccount_number,*/
                    tobecol_recoveredamount: $scope.txtcollectedrecovered_amt,
                    tobecol_gstinpercentage: $scope.txtcollectedgst_inpercentage,
                    tobecol_gst: $scope.rdbcollectedgst,
                    tobecol_totalamount: $scope.txtcollectedtotal_amount,
                    tobecol_remainingamountcollected: $scope.rdbcollectedcollected_amount,
                    tobecol_Chequenodetails: $scope.txtcollecteddoc_cheque_no,
                    tobecol_Cheque_date: $scope.txtcollecteddoc_cheque_date,
                    tobecol_banknamegid: lsbank_namegid,
                    tobecol_bankname: lsbank_name,
                    tobecol_accountnumber: $scope.txtcollectedaccount_number,
                }
                lockUI();
                var url = 'api/MstLSA/PostDocumentCharge';
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        refreshservicechargedetails();
                        documentcharge_clear();
                        $scope.showaddfeecharges = false;
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
            }
            else
                {
                var lsalreadybank_namegid = "", lsalreadybank_name = "";
                if ($scope.cboalreadycollectbank_name != undefined && $scope.cboalreadycollectbank_name != "") {
                    lsalreadybank_namegid = $scope.cboalreadycollectbank_name.bankname_gid;
                    lsalreadybank_name = $scope.cboalreadycollectbank_name.bank_name;
                }
                var lsbank_namegid = "", lsbank_name = "";
                if ($scope.cbocollectedbank_name != undefined && $scope.cbocollectedbank_name != "") {
                    lsbank_namegid = $scope.cbocollectedbank_name.bankname_gid;
                    lsbank_name = $scope.cbocollectedbank_name.bank_name;
                }
                if ($scope.txtdoc_recovered_amount != '' || $scope.txtdoc_recovered_amount != null) {
                    var lsalreadycol_recoveredamount = $scope.txtdoc_recovered_amount
                }
                else {
                    var lsalreadycol_recoveredamount = '0.00';
                }
                if ($scope.txtalreadycollecttotal_amount != '' || $scope.txtalreadycollecttotal_amount != null) {
                    var lsalreadycol_totalamount = $scope.txtalreadycollecttotal_amount
                }
                else {
                    var lsalreadycol_totalamount = '0.00';
                }

                var params = {
                    lsafeescharge_gid: $scope.hdnlsafeescharge_gid,
                    generatelsa_gid: $location.search().generatelsa_gid,
                    charge_typeid: $scope.cbotypename.id,
                    charge_typename: $scope.cbotypename.typename,
                    charge_amount: $scope.application_totalamount,
                    charge_applicable: $scope.rdbdocumentcharge,
                    charge_remarks: $scope.txtdocumentcharge_remarks,
                    alreadycol_recoveredamount: lsalreadycol_recoveredamount,
                    alreadycol_gstinpercentage: $scope.txtalreadycollectgst_inpercentage,
                    alreadycol_gst: $scope.rdbgst_alreadycollected,
                    alreadycol_totalamount: lsalreadycol_totalamount,
                    //alreadycol_remainingamountcollected: $scope.rdbalreadycollected_amount,
                    //alreadycol_Chequenodetails: $scope.txtalreadycollectdoc_cheque_no,
                    //alreadycol_Cheque_date: $scope.txtalreadycollectdoc_cheque_date,
                    //alreadycol_banknamegid: lsalreadybank_namegid,
                    //alreadycol_bankname: lsalreadybank_name,
                    /*   alreadycol_accountnumber: $scope.txtalreadycollectaccount_number,*/
                    tobecol_recoveredamount: $scope.txtcollectedrecovered_amt,
                    tobecol_gstinpercentage: $scope.txtcollectedgst_inpercentage,
                    tobecol_gst: $scope.rdbcollectedgst,
                    tobecol_totalamount: $scope.txtcollectedtotal_amount,
                    tobecol_remainingamountcollected: $scope.rdbcollectedcollected_amount,
                    tobecol_Chequenodetails: $scope.txtcollecteddoc_cheque_no,
                    tobecol_Cheque_date: $scope.txtcollecteddoc_cheque_date,
                    tobecol_banknamegid: lsbank_namegid,
                    tobecol_bankname: lsbank_name,
                    tobecol_accountnumber: $scope.txtcollectedaccount_number,
                }
                lockUI();
                var url = 'api/MstLSA/PostDocumentCharge';
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        refreshservicechargedetails();
                        documentcharge_clear();
                        $scope.showaddfeecharges = false;
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
        }

        /// End - Processing Fee charges Add ...//

        $scope.docedit_charges = function (lsafeescharge_gid, chargetype) {
            var modalInstance = $modal.open({
                templateUrl: '/EditDocumentCharges.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                lockUI();
                var url = "api/MstLSA/GetBankName";
                SocketService.get(url).then(function (resp) {
                    unlockUI();
                    $scope.BankNamelist = resp.data.MdlBankName;
                });
                //$scope.typelist = [
                //    { id: 1, typename: 'Processing Fees' },
                //    { id: 2, typename: 'Document Charges' },
                //    { id: 3, typename: 'Field Visit Charges' },
                //    { id: 4, typename: 'Adhoc Fee' },
                //    { id: 5, typename: 'Term Life Insurance' },
                //    { id: 6, typename: 'Personal Accident Insurance' },
                //    { id: 0, typename: '' }
                //]
                lockUI();
                var params = {
                    lsafeescharge_gid: lsafeescharge_gid,
                    charge_type: chargetype
                }
                var url = "api/MstLSA/GetlsachargesDetail";
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lsachargestype_gid = resp.data.lsachargestype_gid;
                    $scope.lsafeescharge_gid = resp.data.lsafeescharge_gid;
                    $scope.cbotypename = resp.data.charge_typeid;
                    $scope.cbocharge_typename = resp.data.charge_typename;
                    $scope.rdbdocumentcharge = resp.data.charge_applicable;
                    if ($scope.rdbdocumentcharge == 'Yes') {
                        $scope.documentcharge_yes = true;
                        $scope.documentcharge_no = false;
                    }
                    if ($scope.rdbdocumentcharge == 'No') {
                        $scope.documentcharge_yes = false;
                        $scope.documentcharge_no = true;
                    }

                    $scope.txtdocumentcharge_remarks = resp.data.charge_remarks;
                    $scope.txtsampledocrecovered_amount = resp.data.alreadycol_recoveredamount;
                    $scope.txtdoc_recovered_amount = (parseInt($scope.txtsampledocrecovered_amount.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountseperator = (parseInt($scope.txtdoc_recovered_amount.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                    document.getElementById('words_recoverdamount').innerHTML = $scope.lblamountwords;
                    $scope.txtgst_inpercentage = resp.data.alreadycol_gstinpercentage;
                    $scope.rdbgst = resp.data.alreadycol_gst;
                    $scope.txttotal_amount = resp.data.tobecol_totalamount;
                    $scope.txtsamplecollectedrecovered_amt = resp.data.tobecol_recoveredamount;
                    $scope.txtcollectedrecovered_amt = (parseInt($scope.txtsamplecollectedrecovered_amt.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountseperator = (parseInt($scope.txtcollectedrecovered_amt.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                    document.getElementById('words_collectedrecoveredamtedit').innerHTML = $scope.lblamountwords;
                    $scope.rdbcollected_amount = resp.data.tobecol_remainingamountcollected;
                    if ($scope.rdbcollected_amount == 'Collect') {
                        $scope.remainingamt_collect = true;
                    }
                    else if ($scope.rdbcollected_amount == 'Deduct') {
                        $scope.remainingamt_collect = false;
                    }
                    $scope.txtdoc_cheque_no = resp.data.alreadycol_Chequenodetails;
                    $scope.txtdoc_cheque_date = resp.data.alreadycol_ChequeDate;
                    $scope.cbobank_name = resp.data.tobecol_banknamegid;
                });

                function defaultamountwordschange(input) {
                    var str1 = input.replace(/,/g, '');
                    var str = Math.round(str1);
                    var output = Number(str).toLocaleString('en-IN');
                    var lswords = cmnfunctionService.fnConvertNumbertoWord(str);
                    return lswords;
                }

                $scope.calender7 = function ($event) {
                    $event.preventDefault();
                    $event.stopPropagation();
                    $scope.open7 = true;
                };

                $scope.dateOptions = {
                    formatYear: 'yy',
                    startingDay: 1
                };

                $scope.formats = ['dd-MM-yyyy'];
                $scope.format = $scope.formats[0];
                $scope.documentchargestatus_yes = function () {
                    if ($scope.rdbdocumentcharge == "Yes") {
                        $scope.documentcharge_yes = true;
                        $scope.documentcharge_no = false;
                    }
                }
                $scope.documentchargestatus_no = function () {
                    if ($scope.rdbdocumentcharge == "No") {
                        $scope.documentcharge_yes = false;
                        $scope.documentcharge_no = true;
                    }
                }

                $scope.documentcharge_clear = function () {
                    $scope.txtdocument_charges = '';
                    $scope.rdbgst = '';
                    $scope.txttotal_gst = '';
                    $scope.txtcollectedrecovered_amt = '';
                    $scope.rdbcollected_amount = '';
                    $scope.txtdoc_cheque_no = '';
                    $scope.txtdoc_cheque_date = '';
                    $scope.cbobank_name = '';
                    $scope.txtdocumentcharge_remarks = '';
                }

                $scope.remainingamount_collect = function () {
                    $scope.remainingamt_collect = true;
                    $scope.remainingamt_deduct = false;
                }

                $scope.remainingamount_deduct = function () {
                    $scope.remainingamt_collect = false;
                    $scope.remainingamt_deduct = true;
                }

                $scope.collectedrecoveredamtchange = function () {
                    var input = document.getElementById('CollectedRecoverdAmountEdit').value;
                    var str = input.replace(/,/g, '');
                    var output = Number(str).toLocaleString('en-IN');
                    var lscollectedrecovered_amt = cmnfunctionService.fnConvertNumbertoWord(str);
                    if (output == "NaN") {
                        Notify.alert('Accept Number Format Only..!', {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.txtcollectedrecovered_amt = "";
                    }
                    else {
                        $scope.txtcollectedrecovered_amt = output;
                        document.getElementById('words_collectedrecoveredamtedit').innerHTML = lscollectedrecovered_amt;
                    }
                }

                $scope.recoveredamountchange = function () {
                    var input = document.getElementById('RecoveredAmountEdit').value;
                    var str = input.replace(/,/g, '');
                    var output = Number(str).toLocaleString('en-IN');
                    var lsrecovered_amount = cmnfunctionService.fnConvertNumbertoWord(str);
                    if (output == "NaN") {
                        Notify.alert('Accept Number Format Only..!', {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.txtdoc_recovered_amount = "";
                    }
                    else {
                        $scope.txtdoc_recovered_amount = output;
                        document.getElementById('words_recoverdamount').innerHTML = lsrecovered_amount;
                        if ($scope.application_totalamount != null && $scope.txtdoc_recovered_amount != null) {
                            if (($scope.application_totalamount.replace(/[\s,]+/g, '').trim()) > ($scope.txtdoc_recovered_amount.replace(/[\s,]+/g, '').trim())) {
                                $scope.panelalreadycollected = false;
                            }
                        }
                        else {
                            $scope.panelalreadycollected = true;
                        }
                        if ($scope.application_totalamount != null && $scope.txtdoc_recovered_amount != null) {
                            $scope.txtcollectedrecovered_amt = ($scope.application_totalamount.replace(/[\s,]+/g, '').trim()) - ($scope.txtdoc_recovered_amount.replace(/[\s,]+/g, '').trim());
                        }
                        if ($scope.txtcollectedrecovered_amt != 0) {
                            $scope.showtobecollecteddiv = true;
                        }
                        else {
                            $scope.showtobecollecteddiv = false;
                        }
                    }
                }

                $scope.changeexclusivegst = function () {
                    $scope.txtgst_inpercentage = "0";
                }

                $scope.documentcharge_update = function () {
                    if (($scope.rdbdocumentcharge == 'Yes') && ($scope.txtdoc_recovered_amount == undefined || $scope.txtdoc_recovered_amount == null || $scope.txtdoc_recovered_amount == '' ||
                        $scope.rdbgst == undefined || $scope.rdbgst == null || $scope.rdbgst == '' ||
                        $scope.txtgst_inpercentage == undefined || $scope.txtgst_inpercentage == null || $scope.txtgst_inpercentage == '' ||
                        $scope.txttotal_amount == undefined || $scope.txttotal_amount == null || $scope.txttotal_amount == '' ||
                        $scope.txtcollectedrecovered_amt == undefined || $scope.txtcollectedrecovered_amt == null || $scope.txtcollectedrecovered_amt == '' ||
                        $scope.rdbcollected_amount == undefined || $scope.rdbcollected_amount == null || $scope.rdbcollected_amount == '')) {
                        alert('Kindly Enter All Mandatory Fields');
                    }
                    else if (($scope.rdbcollected_amount == 'Collect') && ($scope.txtdoc_cheque_no == undefined || $scope.txtdoc_cheque_no == null || $scope.txtdoc_cheque_no == '' ||
                        $scope.txtdoc_cheque_date == undefined || $scope.txtdoc_cheque_date == null || $scope.txtdoc_cheque_date == '' ||
                        $scope.cbobank_name == undefined || $scope.cbobank_name == null || $scope.cbobank_name == '')) {
                        alert('Kindly Enter Cheque Details');
                    }
                    else {
                        var lsbank_namegid = "", lsbank_name = "";
                        if ($scope.cbobank_name != undefined && $scope.cbobank_name != "") {
                            lsbank_namegid = $scope.cbobank_name;
                            var getbankname = $scope.BankNamelist.filter(function (el) { return el.bankname_gid === $scope.cbobank_name });
                            if (getbankname != null) {
                                lsbank_name = getbankname[0].bank_name;
                            }
                        }
                        if ($scope.txtdoc_recovered_amount != '' || $scope.txtdoc_recovered_amount != null) {
                            var lsalreadycol_recoveredamount = $scope.txtdoc_recovered_amount
                        }
                        else {
                            var lsalreadycol_recoveredamount = '0.00';
                        }
                        if ($scope.txtdoc_recovered_amount != '' || $scope.txtdoc_recovered_amount != null) {
                            var lsalreadycol_totalamount = $scope.txtdoc_recovered_amount
                        }
                        else {
                            var lsalreadycol_totalamount = '0.00';
                        }
                        if ($scope.txtcollectedrecovered_amt != '' || $scope.txtcollectedrecovered_amt != null) {
                            var lstobecol_recoveredamount = $scope.txtcollectedrecovered_amt
                        }
                        else {
                            var lstobecol_recoveredamount = '0.00';
                        }
                        if ($scope.txttotal_amount != '' || $scope.txttotal_amount != null) {
                            var lstxttotal_amount = $scope.txttotal_amount
                        }
                        else {
                            var lstxttotal_amount = '0.00';
                        }

                        var params = {
                            lsafeescharge_gid: lsafeescharge_gid,
                            lsachargestype_gid: $scope.lsachargestype_gid,
                            charge_applicable: $scope.rdbdocumentcharge,
                            charge_remarks: $scope.txtdocumentcharge_remarks,
                            alreadycol_recoveredamount: lsalreadycol_recoveredamount,
                            alreadycol_gstinpercentage: $scope.txtgst_inpercentage,
                            alreadycol_gst: $scope.rdbgst,
                            alreadycol_totalamount: lsalreadycol_totalamount,
                            tobecol_recoveredamount: lstobecol_recoveredamount,
                            tobecol_remainingamountcollected: $scope.rdbcollected_amount,
                            alreadycol_Chequenodetails: $scope.txtdoc_cheque_no,
                            alreadycol_Cheque_date: $scope.txtdoc_cheque_date,
                            tobecol_banknamegid: lsbank_namegid,
                            tobecol_bankname: lsbank_name,
                            tobecol_totalamount: lstxttotal_amount
                        }
                        lockUI();
                        var url = 'api/MstLSA/PostUpdateChargeDetail';
                        SocketService.post(url, params).then(function (resp) {
                            if (resp.data.status == true) {
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                unlockUI();
                                $modalInstance.close('closed');
                                activate();
                            }
                            else {
                                $modalInstance.close('closed');
                                Notify.alert(resp.data.message, {
                                    status: 'danger',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                unlockUI();
                            }
                        });
                    }

                  
                }
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
                //    var lsrecovered_amt = cmnfunctionService.fnConvertNumbertoWord(str);
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

                //function cmnfunctionService.fnConvertNumbertoWord(num) {
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
                //    var lsrecovered_amount1 = cmnfunctionService.fnConvertNumbertoWord(str);
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

                //function cmnfunctionService.fnConvertNumbertoWord(num) {
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


        $scope.downloadallfile = function () {
            for (var i = 0; i < $scope.filename_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.filename_list[i].document_path, $scope.filename_list[i].document_name);
            }
        }

        $scope.document_cancelclick = function (lsauploaddocument_gid) {
            lockUI();
            var params = {
                lsauploaddocument_gid: lsauploaddocument_gid
            }
            var url = 'api/MstLSA/GetDeleteLSAuploadeddocument';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    var params = {
                        generatelsa_gid: $location.search().generatelsa_gid,

                    };
                    var url = 'api/MstLSA/GetLSAGeneraldocument';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.filename_list = resp.data.UploadLSADocumentList;
                    });
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $modalInstance.close('closed');
                }
                else {
                    $modalInstance.close('closed');
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }
            });
        }

        $scope.upload = function (val) {
            if (($scope.txtdocument_type == null) || ($scope.txtdocument_type == '') || ($scope.txtdocument_type == undefined)) {
                $("#addupload").val('');
                Notify.alert('Kindly Enter the Document Type', 'warning');
            } else {
                var frm = new FormData();
                for (var i = 0; i < val.length; i++) {
                    var item = {
                        name: val[i].name,
                        file: val[i]
                    };
                    frm.append('fileupload', item.file);
                    frm.append('file_name', item.name);
                    frm.append('document_name', $scope.documentname);
                 

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
                frm.append('document_type', $scope.txtdocument_type);
                frm.append('generatelsa_gid', $location.search().generatelsa_gid);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    lockUI();
                    var url = 'api/MstLSA/PostlsaGeneralUploaddocument';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        $scope.filename_list = resp.data.UploadLSADocumentList;
                        $("#addupload").val('');
                        if (resp.data.status == true) {
                            unlockUI();
                            $scope.txtdocument_type = '';
                            $scope.showdiv = true;
                            $scope.hidediv = false;
                            Notify.alert(resp.data.message, 'success')
                        }
                        else {
                            unlockUI();
                            Notify.alert(resp.data.message, 'warning')
                        }
                    });
                }
                else {
                    alert('Document is not Available..!');
                    return;
                }
            }
        }

        $scope.proceedtochecker = function () {
            var getlsabankinfo = null, getcreditbankinfo = null;
            if ($scope.lsabankaccsummary_list && $scope.lsabankaccsummary_list.length > 0) {
                getlsabankinfo = $scope.lsabankaccsummary_list.filter(function (el) { return el.disbursement_accountstatus === "Yes" });
                if (getlsabankinfo && getlsabankinfo.length == 0)
                    getlsabankinfo = null;
            }
            if ($scope.creditbankacc_list && $scope.creditbankacc_list.length > 0) {
                getcreditbankinfo = $scope.creditbankacc_list.filter(function (el) { return el.disbursement_accountstatus === "Yes" });
                if (getcreditbankinfo && getcreditbankinfo.length == 0)
                    getcreditbankinfo = null;
            }
            if ((getlsabankinfo == null && getcreditbankinfo == null)) {
                Notify.alert('Atleast One Bank account status should be marked as Disbursement "Yes"..!', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
            }
            else {
                var params = {
                    generatelsa_gid: $location.search().generatelsa_gid,
                    lsacompliancecheckdetail_gid: $scope.lsacompliancecheckdetail_gid,
                    nachmandateform_held: $scope.rdbnachmandateform_held,
                    nachmandateform_heldremarks: $scope.txtnachmandateform_heldremarks,
                    signmatching_nachform: $scope.rdbsignmatching_nachform,
                    signmatching_nachformremarks: $scope.txtsignmatching_nachformremarks,
                    namesign_kycmatching: $scope.rdbnamesign_kycmatching,
                    namesign_kycmatchingremarks: $scope.txtnamesign_kycmatchingremarks,
                    escrowaccount_opened: $scope.rdbescrowaccount_opened,
                    escrowaccount_openedremarks: $scope.txtescrowaccount_openedremarks,
                    appropriate_stamping: $scope.rdbappropriate_stamping,
                    appropriate_stampingremarks: $scope.txtappropriate_stampingremarks,
                    rocfiling_initiated: $scope.rdbrocfiling_initiated,
                    rocfiling_initiatedremarks: $scope.txtrocfiling_initiatedremarks,
                    cersai_initiated: $scope.rdbcersai_initiated,
                    cersai_initiatedremarks: $scope.txtcersai_initiatedremarks,
                    alldeferralcovenant_captured: $scope.rdballdeferralcovenant_captured,
                    allpredisbursement_stipulated: $scope.rdballpredisbursement_stipulated,
                    maker_signaturename: $scope.lblmaker_signature
                }
                lockUI();
                var url = 'api/MstLSA/PostProceedtoChecker';
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        $location.url('app/MstCadLSASummary');
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
        }

        $scope.proceedtoapprover = function (lsacompliancecheckdetail_gid) {


            var getlsabankinfo = null, getcreditbankinfo = null;
            if ($scope.lsabankaccsummary_list && $scope.lsabankaccsummary_list.length > 0) {
                getlsabankinfo = $scope.lsabankaccsummary_list.filter(function (el) { return el.disbursement_accountstatus === "Yes" });
                if (getlsabankinfo && getlsabankinfo.length == 0)
                    getlsabankinfo = null;
            }
            if ($scope.creditbankacc_list && $scope.creditbankacc_list.length > 0) {
                getcreditbankinfo = $scope.creditbankacc_list.filter(function (el) { return el.disbursement_accountstatus === "Yes" });
                if (getcreditbankinfo && getcreditbankinfo.length == 0)
                    getcreditbankinfo = null;
            }
            if ((getlsabankinfo == null && getcreditbankinfo == null)) {
                Notify.alert('Atleast One Bank account status should be marked as Disbursement "Yes"..!', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
            }
            else {
                var params = {
                    generatelsa_gid: $location.search().generatelsa_gid,
                    lsacompliancecheckdetail_gid: lsacompliancecheckdetail_gid,
                    nachmandateform_held: $scope.rdbnachmandateform_held,
                    nachmandateform_heldremarks: $scope.txtnachmandateform_heldremarks,
                    signmatching_nachform: $scope.rdbsignmatching_nachform,
                    signmatching_nachformremarks: $scope.txtsignmatching_nachformremarks,
                    namesign_kycmatching: $scope.rdbnamesign_kycmatching,
                    namesign_kycmatchingremarks: $scope.txtnamesign_kycmatchingremarks,
                    escrowaccount_opened: $scope.rdbescrowaccount_opened,
                    escrowaccount_openedremarks: $scope.txtescrowaccount_openedremarks,
                    appropriate_stamping: $scope.rdbappropriate_stamping,
                    appropriate_stampingremarks: $scope.txtappropriate_stampingremarks,
                    rocfiling_initiated: $scope.rdbrocfiling_initiated,
                    rocfiling_initiatedremarks: $scope.txtrocfiling_initiatedremarks,
                    cersai_initiated: $scope.rdbcersai_initiated,
                    cersai_initiatedremarks: $scope.txtcersai_initiatedremarks,
                    alldeferralcovenant_captured: $scope.rdballdeferralcovenant_captured,
                    allpredisbursement_stipulated: $scope.rdballpredisbursement_stipulated,
                    maker_signaturename: $scope.lblmaker_signature
                }
                lockUI();
                var url = 'api/MstLSA/PostProceedtoApprover';
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        $location.url('app/MstCadLSACheckerSummary');
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
        }

        $scope.upload_doc = function (val, val1, name) {
            if (($scope.cboDocumentTitle == null) || ($scope.cboDocumentTitle == '') || ($scope.cboDocumentTitle == undefined)) {
                $("#file").val('');
                Notify.alert('Kindly Select the Document Title', 'warning');
            }
            else {
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

                frm.append('document_name', $scope.documentname);
                frm.append('document_title', $scope.cboDocumentTitle);
                frm.append('generatelsa_gid', $location.search().generatelsa_gid);
                frm.append('application2loan_gid', $scope.application2loan_gid);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                var url = 'api/MstLSA/postLSAcollateraldocumentAdd';
                lockUI();
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    $("#file").val('');
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.CollateralDocumentList = resp.data.DocumentList;
                        $scope.cboDocumentTitle = '';

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        Notify.alert('File Format Not Supported!', {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                });
            }
        }

        $scope.upload_docedit = function (val, val1, name) {
            if (($scope.cboDocumentTitle == null) || ($scope.cboDocumentTitle == '') || ($scope.cboDocumentTitle == undefined)) {
                $("#file").val('');
                Notify.alert('Kindly Select the Document Title', 'warning');
            }
            else {
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

                frm.append('document_name', $scope.documentname);
                frm.append('document_title', $scope.cboDocumentTitle);
                frm.append('generatelsa_gid', $location.search().generatelsa_gid);
                frm.append('application2loan_gid', $scope.application2loan_gid);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                var url = 'api/MstLSA/postLSAcollateraldocument';
                lockUI();
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    $("#file").val('');
                    unlockUI();
                    if (resp.data.status == true) {
                       
                        $scope.CollateralDocumentListEdit = resp.data.DocumentList;
                       

                        $scope.cboDocumentTitle = '';

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        Notify.alert('File Format Not Supported!', {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                });
            }
        }


        $scope.downloadallcoll = function () {
            for (var i = 0; i < $scope.CollateralDocumentList.length; i++) {
                if ($scope.CollateralDocumentList[i].migration_flag == 'N')
                    DownloaddocumentService.Downloaddocument($scope.CollateralDocumentList[i].document_path, $scope.CollateralDocumentList[i].document_name);
                else
                    DownloaddocumentService.OtherDownloaddocument($scope.CollateralDocumentList[i].document_path, $scope.CollateralDocumentList[i].document_name);
            }
        }
        $scope.downloadallcolledit = function () {
            for (var i = 0; i < $scope.CollateralDocumentListEdit.length; i++) {
                if ($scope.CollateralDocumentListEdit[i].migration_flag == 'N')
                    DownloaddocumentService.Downloaddocument($scope.CollateralDocumentListEdit[i].document_path, $scope.CollateralDocumentListEdit[i].document_name);
                else
                    DownloaddocumentService.OtherDownloaddocument($scope.CollateralDocumentListEdit[i].document_path, $scope.CollateralDocumentListEdit[i].document_name, $scope.CollateralDocumentListEdit[i].migration_flag);
            }
        }
        $scope.uploadcollateraldocumentcanceledit = function (val,val1,val2, data) {
            var params = {
                document_gid: val
            };

            var url = 'api/MstLSA/deletelsacollateraldoc';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    //angular.forEach($scope.CollateralDocumentList, function (value, key) {
                    //    if (value.document_gid == val) {
                    //        $scope.CollateralDocumentList.splice(key, 1);
                    //    }
                    //});


                    var params = {
                        /*document_gid: val,*/
                        application2loan_gid: val1,
                        generatelsa_gid: val2
                    }
                    var url = 'api/MstLSA/GetLSACollateraldocumentEdit';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.CollateralDocumentListEdit = resp.data.UploadLSADocumentList;
                    });

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'Warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }

        $scope.uploadcollateraldocumentcancel = function (val, val1, data) {
            var params = {
                document_gid: val
            };

            var url = 'api/MstLSA/deletelsacollateraldoc';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    angular.forEach($scope.CollateralDocumentList, function (value, key) {
                        if (value.document_gid == val) {
                            $scope.CollateralDocumentList.splice(key, 1);
                        }
                    });


                  

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'Warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }

        $scope.SubmitServiceCharges = function () {
            var params = {
                processing_fee: $scope.txtprocessing_fee,
                processing_collectiontype: $scope.rdbprocessing_collectiontype,
                doc_charges: $scope.txtdoc_charges,
                doccharge_collectiontype: $scope.rdbdoccharge_collectiontype,
                fieldvisit_charge: $scope.txtfieldvisit_charges,
                fieldvisit_collectiontype: $scope.rdbfieldvisit_collectiontype,
                adhoc_fee: $scope.txtadhoc_fee,
                adhoc_collectiontype: $scope.rdbadhoc_collectiontype,
                life_insurance: $scope.txtlife_insurance,
                lifeinsurance_collectiontype: $scope.rdblifeinsurance_collectiontype,
                acct_insurance: $scope.txtacct_insurance,
                acctinsurance_collectiontype: $scope.rdbpersonalaccident_collectiontype,
                total_collect: document.getElementById("total_collect").value,
                total_deduct: document.getElementById("total_deduct").value,
                productTypelist: $scope.cboProductTypelist,
                application_gid: $scope.application_gid
            }
            var url = 'api/MstLSA/PostServiceCharges';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $scope.txtprocessing_fee = '';
                    $scope.rdbprocessing_collectiontype = '';
                    $scope.txtdoc_charges = '';
                    $scope.rdbdoccharge_collectiontype = '';
                    $scope.txtfieldvisit_charges = '';
                    $scope.rdbfieldvisit_collectiontype = '';
                    $scope.txtadhoc_fee = '';
                    $scope.rdbadhoc_collectiontype = '';
                    $scope.txtlife_insurance = '';
                    $scope.rdblifeinsurance_collectiontype = '';
                    $scope.txtacct_insurance = '';
                    $scope.rdbpersonalaccident_collectiontype = '';
                    $scope.cboProductTypelist = '';
                    document.getElementById('words_totalamount51').innerHTML = '';
                    document.getElementById('words_totalamount52').innerHTML = '';
                    document.getElementById('words_totalamount53').innerHTML = '';
                    document.getElementById('words_totalamount54').innerHTML = '';
                    document.getElementById('words_totalamount55').innerHTML = '';
                    document.getElementById('words_totalamount56').innerHTML = '';
                    document.getElementById("total_collect").value = '';
                    document.getElementById("total_deduct").value = '';
                    document.getElementById("total_collect").value = '';
                    document.getElementById("total_deduct").value = '';
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

        $scope.documentviewer = function (val1, val2, val3) {
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
            if (val3=='N')
                DownloaddocumentService.DocumentViewer(val1, val2);
            else
                DownloaddocumentService.OtherDocumentViewer(val1, val2, val3);
        }

        $scope.documentviewergen = function (val1, val2) {
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
}) ();