(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnSuprCreditPSLDataFlaggingAddController', AgrTrnSuprCreditPSLDataFlaggingAddController);

    AgrTrnSuprCreditPSLDataFlaggingAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function AgrTrnSuprCreditPSLDataFlaggingAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnSuprCreditPSLDataFlaggingAddController';
        $scope.institution_gid = $location.search().institution_gid;
        var institution_gid = $scope.institution_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        lockUI();
        activate();
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
                applicant_type: 'Institution',
                credit_gid: institution_gid,
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
                $scope.institutionPSL_list = resp.data.institutionpsltagging_list;
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
                credit_gid: institution_gid,
                applicant_type: 'Institution'
            }

            var url = 'api/AgrTrnSuprAppCreditUnderWriting/GetCreditOperationsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtinstitution_name = resp.data.company_name;
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
                applicant_type: 'Institution',
                institution_gid: institution_gid,
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
            ($scope.cboTurnover == '') || ($scope.cboMSMEClassification == '') || ($scope.txtDateofEntity == '') || ($scope.rdbmetropolitan == '') || ($scope.cboClientDetails == '')
            ) {
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
                    applicant_type: 'Institution',
                    institution_gid: institution_gid,
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
                        $scope.show_PSlDataflagform = false;
                        $scope.hide_PSlDataflagSummary = false;
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.show_PSlDataflagform = true;
                        $scope.hide_PSlDataflagSummary = true;
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


        $scope.company_deferraldocchecklist = function () {
            $location.url('app/AgrTrnSuprDocumentCheckList?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_covenantdocchecklist = function () {
            $location.url('app/AgrTrnSuprCreditAddCovenantCheckList?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_economicadd = function () {
            $location.url('app/AgrTrnSuprCreditEconomicCapitalAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_geneticadd = function () {
            $location.url('app/AgrTrnSuprCreditCompanyDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_PSLdata = function () {
            $location.url('app/AgrTrnSuprCreditPSLDataFlaggingAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_supplier = function () {
            $location.url('app/AgrTrnSuprCreditSuppliersDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_buyer = function () {
            $location.url('app/AgrTrnSuprCreditBuyerDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_bankaccount = function () {
            $location.url('app/AgrTrnSuprCreditBankAccountDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_existingbankaccount = function () {
            $location.url('app/AgrTrnSuprCreditExistingBankDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_repayment = function () {
            $location.url('app/AgrTrnSuprCreditRepaymentDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_observation = function () {
            $location.url('app/AgrTrnSuprCreditObservationAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.Edit_psldataflag = function () {
            $location.url('app/AgrTrnSuprCreditPSLDataFlaggingEdit?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.tan_verification = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=TAN' + '&lspage=' + lspage);
        }
        $scope.companyllpno_vertification = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=LLPNO' + '&lspage=' + lspage);
        }
        $scope.mcasign_vertification = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=MCASIGNATURE' + '&lspage=' + lspage);
        }
        $scope.iecdetailed_vertification = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=IECDETAILED' + '&lspage=' + lspage);
        }
        $scope.fssai_vertification = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=FSSAI' + '&lspage=' + lspage);
        }
        $scope.fda_vertification = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=FDA' + '&lspage=' + lspage);
        }
        $scope.gst_verification = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=GST' + '&lspage=' + lspage);
        }
        $scope.lpgid_verification = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=LPGID' + '&lspage=' + lspage);
        }
        $scope.shop_vertification = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=SHOP' + '&lspage=' + lspage);
        }
        $scope.rcauthadv_vertification = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=RCAUTHADV' + '&lspage=' + lspage);
        }
        $scope.rcsearch_vertification = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=RCSEARCH' + '&lspage=' + lspage);
        }
        $scope.propertytax_vertification = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=PROPERTYTAX' + '&lspage=' + lspage);
        }
        $scope.company_bankstatement = function () {
            $location.url('app/AgrTrnSuprCreditBankStatementAnalysisAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.company_fsa = function () {
            $location.url('app/AgrTrnSuprCreditFsaDetailAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.institution_bureauadd = function () {
            $location.url('app/AgrTrnSuprCreditInstitutionDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.crimecheck_record = function () {
            $location.url('app/AgrTrnSuprCompanyCrimeCheckRecordAPI?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.crimecheckreport_realtime = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=CRIMECHECKREPORTREALTIME' + '&lspage=' + lspage);
        }
    }
})();
