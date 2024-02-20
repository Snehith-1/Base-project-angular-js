(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnSuprCreditIndividualPSLDataFlagAddController', AgrTrnSuprCreditIndividualPSLDataFlagAddController);

    AgrTrnSuprCreditIndividualPSLDataFlagAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function AgrTrnSuprCreditIndividualPSLDataFlagAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnSuprCreditIndividualPSLDataFlagAddController';
        $scope.contact_gid = $location.search().contact_gid;
        var contact_gid = $scope.contact_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;

        activate();
        lockUI();
        function activate() {

            $scope.show_PSlDataflagform = true;
            $scope.hide_PSlDataflagSummary = true;

            var url = 'api/AgrTrnSuprAppCreditUnderWriting/GetPSLDropdownList';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.occupation_list = resp.data.occupation_list;
                $scope.lineofactivity_list = resp.data.lineofactivity_list;
                $scope.bsrcode_list = resp.data.bsrcode_list;
                $scope.pslcategorylist = resp.data.pslcategorylist;
                $scope.weakersectionlist = resp.data.weakersectionlist;
                $scope.pslpurpose_list = resp.data.pslpurpose_list;
                $scope.natureofentitylist = resp.data.natureofentitylist;
                $scope.turnoverlist = resp.data.turnoverlist;
                $scope.msmelist = resp.data.msmelist;
                $scope.investmentlist = resp.data.investmentlist;
            });

            var url = 'api/AgrTrnSuprAppCreditUnderWriting/ClientDetailsList';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.clientdetail_list = resp.data.clientdetail_list;
            });

            var params = {
                applicant_type: 'Individual',
                credit_gid: contact_gid,
            }
            var url = 'api/AgrTrnSuprAppCreditUnderWriting/EditPSLDataFlagging';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.psltagging_flag = resp.data.psltagging_flag;

                if ($scope.psltagging_flag == 'N') {
                    $scope.show_PSlDataflagform = true;
                    $scope.hide_PSlDataflagSummary = true;
                }
                else {
                    $scope.show_PSlDataflagform = false;
                    $scope.hide_PSlDataflagSummary = false;
                }
            });

            var url = 'api/AgrTrnSuprAppCreditUnderWriting/GetPSLDataFlagging';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.individualPSL_list = resp.data.individualpsltagging_list;
            });

            vm.submitted = false;
            vm.validateInput = function (name, type) {
                var input = vm.formValidate[name];
                return (input.$dirty || vm.submitted) && input.$error[type];
            };

            // Submit form
            vm.submitForm = function () {
                vm.submitted = true;
                if (vm.formValidate.$valid) {
                } else {
                    return false;
                }
            };

            // Calender Popup... //

            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open1 = true;
            };

            vm.calender2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open2 = true;
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            var params = {
                credit_gid: contact_gid,
                applicant_type: 'Individual'
            }

            var url = 'api/AgrTrnSuprAppCreditUnderWriting/GetCreditOperationsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtindividual_name = resp.data.individual_name;
                $scope.txtstakeholder_type = resp.data.stakeholder_type;
            });

        }

        $scope.save_creditPSLData = function () {
            var params = {
                startupasofloansanction_date: $scope.rdbsanctiondate,
                occupation_gid: $scope.cboOccupation.occupation_gid,
                occupation: $scope.cboOccupation.occupation_name,
                lineofactivity_gid: $scope.cboLineofActivity.lineofactivity_gid,
                lineofactivity: $scope.cboLineofActivity.lineof_activity,
                bsrcode_gid: $scope.cboBSRcode.bsrcode_gid,
                bsrcode: $scope.cboBSRcode.bsr_code,
                pslcategory_gid: $scope.cboPSLCategory.pslcategory_gid,
                pslcategory: $scope.cboPSLCategory.psl_category,
                weakersection_gid: $scope.cboWeakersection.weakersection_gid,
                weakersection: $scope.cboWeakersection.weaker_section,
                pslpurpose_gid: $scope.cboPSLpurpose.pslpurpose_gid,
                pslpurpose: $scope.cboPSLpurpose.psl_purpose,
                totalsanction_financialinstitution: $scope.txtfinancialinstitutions,
                pslsanction_limit: $scope.txtPSLSanctionLimit,
                natureofentity_gid: $scope.cboNatureofEntity.natureofentity_gid,
                natureofentity: $scope.cboNatureofEntity.natureofentity_name,
                indulgeinmarketing_activity: $scope.rdbMarketingActivities,
                plantandmachineryinvestment_gid: $scope.cboInvestment.investment_gid,
                plantandmachineryinvestment: $scope.cboInvestment.investment_name,
                turnover_gid: $scope.cboTurnover.turnover_gid,
                turnover: $scope.cboTurnover.turnover_name,
                msmeclassification_gid: $scope.cboMSMEClassification.msme_gid,
                msmeclassification: $scope.cboMSMEClassification.msme_name,
                loansanction_date: $scope.txtDate_ofLoanSanction,
                entityincorporation_date: $scope.txtDateofEntity,
                hq_metropolitancity: $scope.rdbmetropolitan,
                clientdtl_gid: $scope.cboClientDetails.clientdtl_gid,
                clientdtl_name: $scope.cboClientDetails.clientdtl_name,
                applicant_type: 'Individual',
                contact_gid: contact_gid,
            }
            var url = 'api/AgrTrnSuprAppCreditUnderWriting/PSLDataFlaggingSave';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                    $scope.show_PSlDataflagform = false;
                    $scope.hide_PSlDataflagSummary = false;
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                    $scope.show_PSlDataflagform = true;
                    $scope.hide_PSlDataflagSummary = true;
                }

            });

        }

        $scope.submit_creditPSLData = function () {
            if (($scope.rdbsanctiondate == undefined) || ($scope.cboOccupation == undefined) || ($scope.cboLineofActivity == undefined) || ($scope.cboBSRcode == undefined) || ($scope.cboPSLCategory == undefined) || ($scope.cboWeakersection == undefined) ||
              ($scope.cboPSLpurpose == undefined) || ($scope.txtfinancialinstitutions == undefined) || ($scope.txtPSLSanctionLimit == undefined) || ($scope.cboNatureofEntity == undefined) || ($scope.rdbMarketingActivities == undefined) || ($scope.cboInvestment == undefined) ||
              ($scope.cboTurnover == undefined) || ($scope.cboMSMEClassification == undefined) || ($scope.txtDateofEntity == undefined) || ($scope.rdbmetropolitan == undefined) || ($scope.cboClientDetails == undefined) ||
              ($scope.rdbsanctiondate == '') || ($scope.cboOccupation == '') || ($scope.cboLineofActivity == '') || ($scope.cboBSRcode == '') || ($scope.cboPSLCategory == '') || ($scope.cboWeakersection == '') ||
              ($scope.cboPSLpurpose == '') || ($scope.txtfinancialinstitutions == '') || ($scope.txtPSLSanctionLimit == '') || ($scope.cboNatureofEntity == '') || ($scope.rdbMarketingActivities == '') || ($scope.cboInvestment == '') ||
              ($scope.cboTurnover == '') || ($scope.cboMSMEClassification == '') || ($scope.txtDateofEntity == '') || ($scope.rdbmetropolitan == '') || ($scope.cboClientDetails == '')) {
                Notify.alert('Enter All Mandatory Fields', 'warning');
            }
            else {
                var params = {
                    startupasofloansanction_date: $scope.rdbsanctiondate,
                    occupation_gid: $scope.cboOccupation.occupation_gid,
                    occupation: $scope.cboOccupation.occupation_name,
                    lineofactivity_gid: $scope.cboLineofActivity.lineofactivity_gid,
                    lineofactivity: $scope.cboLineofActivity.lineof_activity,
                    bsrcode_gid: $scope.cboBSRcode.bsrcode_gid,
                    bsrcode: $scope.cboBSRcode.bsr_code,
                    pslcategory_gid: $scope.cboPSLCategory.pslcategory_gid,
                    pslcategory: $scope.cboPSLCategory.psl_category,
                    weakersection_gid: $scope.cboWeakersection.weakersection_gid,
                    weakersection: $scope.cboWeakersection.weaker_section,
                    pslpurpose_gid: $scope.cboPSLpurpose.pslpurpose_gid,
                    pslpurpose: $scope.cboPSLpurpose.psl_purpose,
                    totalsanction_financialinstitution: $scope.txtfinancialinstitutions,
                    pslsanction_limit: $scope.txtPSLSanctionLimit,
                    natureofentity_gid: $scope.cboNatureofEntity.natureofentity_gid,
                    natureofentity: $scope.cboNatureofEntity.natureofentity_name,
                    indulgeinmarketing_activity: $scope.rdbMarketingActivities,
                    plantandmachineryinvestment_gid: $scope.cboInvestment.investment_gid,
                    plantandmachineryinvestment: $scope.cboInvestment.investment_name,
                    turnover_gid: $scope.cboTurnover.turnover_gid,
                    turnover: $scope.cboTurnover.turnover_name,
                    msmeclassification_gid: $scope.cboMSMEClassification.msme_gid,
                    msmeclassification: $scope.cboMSMEClassification.msme_name,
                    loansanction_date: $scope.txtDate_ofLoanSanction,
                    entityincorporation_date: $scope.txtDateofEntity,
                    hq_metropolitancity: $scope.rdbmetropolitan,
                    clientdtl_gid: $scope.cboClientDetails.clientdtl_gid,
                    clientdtl_name: $scope.cboClientDetails.clientdtl_name,
                    applicant_type: 'Individual',
                    contact_gid: contact_gid,
                }
                var url = 'api/AgrTrnSuprAppCreditUnderWriting/PSLDataFlaggingSubmit';
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        activate();
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        activate();
                    }

                });
            }
        }

        $scope.Back = function () {
            if (lspage == "StartCreditUnderwriting") {
                $location.url('app/AgrTrnSuprStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "PendingCADReview") {
                $location.url('app/AgrTrnSuprCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CADApplicationEdit") {
                $location.url('app/AgrTrnSuprCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CADAcceptanceCustomers") {
                $location.url('app/AgrTrnSuprCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else {

            }
        }

        $scope.individual_docchecklist = function () {
            $location.url('app/AgrTrnSuprIndividualDocCheckList?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_covenantdocchecklist = function () {
            $location.url('app/AgrTrnSuprIndividualCovenantDocChecklist?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_bureauadd = function () {
            $location.url('app/AgrTrnSuprCreditIndividualDtlAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_bankaccount = function () {
            $location.url('app/AgrTrnSuprCreditIndividualBankAcctAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_existingbankaccount = function () {
            $location.url('app/AgrTrnSuprCreditIndividualExistingBankAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_PSLdata = function () {
            $location.url('app/AgrTrnSuprCreditIndividualPSLDataFlagAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_repayment = function () {
            $location.url('app/AgrTrnSuprCreditIndividualRepaymentAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_observation = function () {
            $location.url('app/AgrTrnSuprCreditIndividualObservationAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.individual_observation = function () {
            $location.url('app/AgrTrnSuprCreditIndividualObservationAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.Edit_psldataflag = function () {
            $location.url('app/AgrTrnSuprCreditIndividualPSLDataFlagEdit?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }
        //KYC API
        $scope.iecdetailed_vertification = function () {
            $location.url('app/AgrTrnSuprCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=IECDETAILED' + '&lspage=' + lspage);
        }
        $scope.fssai_vertification = function () {
            $location.url('app/AgrTrnSuprCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=FSSAI' + '&lspage=' + lspage);
        }
        $scope.fda_vertification = function () {
            $location.url('app/AgrTrnSuprCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=FDA' + '&lspage=' + lspage);
        }
        $scope.lpgid_vertification = function () {
            $location.url('app/AgrTrnSuprCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=LPGID' + '&lspage=' + lspage);
        }
        $scope.shop_vertification = function () {
            $location.url('app/AgrTrnSuprCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=SHOP' + '&lspage=' + lspage);
        }
        $scope.rcauthadv_vertification = function () {
            $location.url('app/AgrTrnSuprCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=RCAUTHADV' + '&lspage=' + lspage);
        }
        $scope.rcsearch_vertification = function () {
            $location.url('app/AgrTrnSuprCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=RCSEARCH' + '&lspage=' + lspage);
        }
        $scope.propertytax_vertification = function () {
            $location.url('app/AgrTrnSuprCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=PROPERTYTAX' + '&lspage=' + lspage);
        }
        $scope.company_bankstatement = function () {
            $location.url('app/AgrTrnSuprCreditIndividualBankStatementAnalysisAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }
        $scope.crimecheck_record = function () {
            $location.url('app/AgrTrnSuprCreditCrimeCheckRecordAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=PROPERTYTAX' + '&lspage=' + lspage);
        }
        $scope.crimecheckreport_realtime = function () {
            $location.url('app/AgrTrnSuprCreditIndividualAPI?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lsapi_name=CRIMECHECKREPORTREALTIME' + '&lspage=' + lspage);
        }
    }
})();
