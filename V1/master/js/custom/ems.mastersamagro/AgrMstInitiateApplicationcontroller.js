(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstInitiateApplicationcontroller', AgrMstInitiateApplicationcontroller);

    AgrMstInitiateApplicationcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function AgrMstInitiateApplicationcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstInitiateApplicationcontroller';
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var application_gid = searchObject.application_gid;
        var FromBuyerApproval = searchObject.FromBuyerApproval;
        activate();

        function activate() {
            lockUI();
            var params = {
                buyeronboard_gid: application_gid
            }
            var url = 'api/AgrMstBuyerOnboard/GetonboardInitiateDetail';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.onboardtl = resp.data;
                    $scope.onboardapplicationList = resp.data.onboardapplicationList;
                    $scope.loanproductlist = resp.data.loanproductlist;
                    unlockUI();
                }
            });
        } 

        //$scope.validateproductdetails = function () {
        //    var params = {
        //        loanproduct_gid: $scope.cboProductTypelist.loanproduct_gid,
        //        buyeronboard_gid: application_gid
        //    }
        //    var url = 'api/AgrMstBuyerOnboard/GetValidateProductProgram';
        //    SocketService.getparams(url, params).then(function (resp) {
        //        $scope.loansubproductlist = resp.data.application_list;
        //    });
        //}

        $scope.validateproductdetails = function () {
            var params = {
                loanproduct_gid: $scope.cboProductTypelist.loanproduct_gid,
                buyeronboard_gid: application_gid
            }
            var url = 'api/AgrMstBuyerOnboard/GetValidateProductProgram';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.loansubproductlist = resp.data.application_list;
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $scope.cboProductTypelist = '';
                }
            });
        }


        $scope.back = function () {
            if (FromBuyerApproval == "Y") {
                $location.url('app/AgrMstBuyerApprovedSummary');
            }
            else {
                $location.url('app/AgrMstCustomerOnboardingSummary');
            } 
        }

        $scope.initiate_onboard = function () {
            var params = {
                application_gid: application_gid,
                approval_remarks: $scope.txtremarks,
                product_gid: $scope.cboProductTypelist.loanproduct_gid,
                product_name: $scope.cboProductTypelist.loanproduct_name,
                program_gid: $scope.cboProductSubTypelist.loansubproduct_gid,
                program_name: $scope.cboProductSubTypelist.loansubproduct_name,
                onboarding_status: $scope.rdbOnboarding
            }
            var url = 'api/AgrMstBuyerOnboard/PostInitiateBuyerApplication';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    if (FromBuyerApproval == "Y") {
                        $location.url('app/AgrMstBuyerApprovedSummary'); 
                    }
                    else {
                        $location.url('app/AgrMstCustomerOnboardingSummary');
                    } 
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
})();
