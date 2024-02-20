(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnCreditGroupPSLDataFlagEditController', AgrTrnCreditGroupPSLDataFlagEditController);

    AgrTrnCreditGroupPSLDataFlagEditController.$inject = ['$rootScope', '$sce', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrTrnCreditGroupPSLDataFlagEditController($rootScope, $sce, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnCreditGroupPSLDataFlagEditController';
        $scope.group_gid = $location.search().group_gid;
        var group_gid = $scope.group_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;

        activate();
        function activate() {

            var url = 'api/AgrTrnAppCreditUnderWriting/GetPSLDropdownList';
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
            var url = 'api/AgrTrnAppCreditUnderWriting/ClientDetailsList';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.clientdetail_list = resp.data.clientdetail_list;
            });

            var param = {
                applicant_type: 'Group',
                credit_gid: group_gid,
            }

            var url = 'api/AgrTrnAppCreditUnderWriting/EditPSLDataFlagging';

            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.rdbsanctiondate = resp.data.startupasofloansanction_date;
                $scope.cboOccupation = resp.data.occupation_gid;
                $scope.cboLineofActivity = resp.data.lineofactivity_gid;
                $scope.cboBSRcode = resp.data.bsrcode_gid;
                $scope.cboPSLCategory = resp.data.pslcategory_gid;
                $scope.cboWeakersection = resp.data.weakersection_gid;
                $scope.cboPSLpurpose = resp.data.pslpurpose_gid;
                $scope.txtfinancialinstitutions = resp.data.totalsanction_financialinstitution;
                $scope.txtPSLSanctionLimit = resp.data.pslsanction_limit;
                $scope.cboNatureofEntity = resp.data.natureofentity_gid;
                $scope.rdbMarketingActivities = resp.data.indulgeinmarketing_activity;
                $scope.cboInvestment = resp.data.plantandmachineryinvestment_gid;
                $scope.cboTurnover = resp.data.turnover_gid;
                $scope.cboMSMEClassification = resp.data.msmeclassification_gid;
                $scope.txtDate_ofLoanSanction = resp.data.loansanction_date;
                $scope.txtDateofEntity = resp.data.entityincorporation_date;
                $scope.rdbmetropolitan = resp.data.hq_metropolitancity;
                $scope.cboClientDetails = resp.data.clientdtl_gid;
                unlockUI();
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
        }

        $scope.psldata_Back = function () {
            $location.url('app/AgrTrnCreditGroupPSLDataFlagAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

        $scope.update_psldata = function () {
            if (($scope.rdbsanctiondate == undefined) || ($scope.cboOccupation == undefined) || ($scope.cboLineofActivity == undefined) || ($scope.cboBSRcode == undefined) || ($scope.cboPSLCategory == undefined) || ($scope.cboWeakersection == undefined) ||
             ($scope.cboPSLpurpose == undefined) || ($scope.txtfinancialinstitutions == undefined) || ($scope.txtPSLSanctionLimit == undefined) || ($scope.cboNatureofEntity == undefined) || ($scope.rdbMarketingActivities == undefined) || ($scope.cboInvestment == undefined) ||
             ($scope.cboTurnover == undefined) || ($scope.cboMSMEClassification == undefined) || ($scope.txtDateofEntity == undefined) || ($scope.rdbmetropolitan == undefined) || ($scope.cboClientDetails == undefined)
             ) {
                Notify.alert('Enter All Mandatory Fields', 'warning');
            }
            else {
                var occupationname = $('#occupation_name :selected').text();
                var lineofactivity = $('#lineof_activity :selected').text();
                var bsrcode = $('#bsr_code :selected').text();
                var pslcategory = $('#psl_category :selected').text();
                var weakersection = $('#weaker_section :selected').text();
                var pslpurpose = $('#psl_purpose :selected').text();
                var natureofentityname = $('#natureofentity_name :selected').text();
                var investmentname = $('#investment_name :selected').text();
                var turnovername = $('#turnover_name :selected').text();
                var msmename = $('#msme_name :selected').text();
                var clientdtlname = $('#clientdtl_name :selected').text();

                var params = {
                    startupasofloansanction_date: $scope.rdbsanctiondate,
                    occupation_gid: $scope.cboOccupation,
                    occupation: occupationname,
                    lineofactivity_gid: $scope.cboLineofActivity,
                    lineofactivity: lineofactivity,
                    bsrcode_gid: $scope.cboBSRcode,
                    bsrcode: bsrcode,
                    pslcategory_gid: $scope.cboPSLCategory,
                    pslcategory: pslcategory,
                    weakersection_gid: $scope.cboWeakersection,
                    weakersection: weakersection,
                    pslpurpose_gid: $scope.cboPSLpurpose,
                    pslpurpose: pslpurpose,
                    totalsanction_financialinstitution: $scope.txtfinancialinstitutions,
                    pslsanction_limit: $scope.txtPSLSanctionLimit,
                    natureofentity_gid: $scope.cboNatureofEntity,
                    natureofentity: natureofentityname,
                    indulgeinmarketing_activity: $scope.rdbMarketingActivities,
                    plantandmachineryinvestment_gid: $scope.cboInvestment,
                    plantandmachineryinvestment: investmentname,
                    turnover_gid: $scope.cboTurnover,
                    turnover: turnovername,
                    msmeclassification_gid: $scope.cboMSMEClassification,
                    msmeclassification: msmename,
                    loansanctiondate: $scope.txtDate_ofLoanSanction,
                    entityincorporationdate: $scope.txtDateofEntity,
                    hq_metropolitancity: $scope.rdbmetropolitan,
                    clientdtl_gid: $scope.cboClientDetails,
                    clientdtl_name: clientdtlname,
                    applicant_type: 'Group',
                    group_gid: group_gid,
                }
                var url = 'api/AgrTrnAppCreditUnderWriting/PSLDataFlaggingUpdate';
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
                    $location.url('app/AgrTrnCreditGroupPSLDataFlagAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
                });
            }
        }

    }
})();

