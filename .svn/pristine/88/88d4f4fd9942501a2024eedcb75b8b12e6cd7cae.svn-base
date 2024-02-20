(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstSupplierApprovedSummaryController', AgrMstSupplierApprovedSummaryController);

    AgrMstSupplierApprovedSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams', 'cmnfunctionService'];

    function AgrMstSupplierApprovedSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstSupplierApprovedSummaryController';

        activate();

        function activate() { }


        //$scope.GetBuyeronboardingApprovedlist = function () {
        //    getApprovalCount();
        //    lockUI();
        //    var url = 'api/AgrMstBuyerOnboard/GetBuyerOnboardApprovedSummary';
        //    SocketService.get(url).then(function (resp) {
        //        if (resp.data.status == true) {
        //            unlockUI();
        //            $scope.buyerapplicationlist = resp.data.onboardapplicationdtl;
        //        }
        //        else unlockUI();
        //    });
        //}

        $scope.GetsupplieronboardingApprovedlist = function () {
            getApprovalCount();
            lockUI();
            var url = 'api/AgrMstBuyerOnboard/GetSupplierOnboardApprovedSummary';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.supplierapplicationlist = resp.data.onboardapplicationdtl;
                }
                else unlockUI();
            });
        }

        function getApprovalCount() {
            lockUI();
            var url = 'api/AgrMstBuyerOnboard/GetApproverApprovedCountDetail';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.approvalcount = resp.data;
                }
                else unlockUI();
            });
        }

        $scope.initiate_supplieronboard = function (application_gid, application_no, customer_name) {
            var modalInstance = $modal.open({
                templateUrl: '/InitiateOnboard.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.application_no = application_no;
                $scope.Popupdtl = "Supplier";
                $scope.customer_name = customer_name;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.initiate_onboard = function () {
                    var params = {
                        application_gid: application_gid,
                        approval_remarks: $scope.txtremarks
                    }
                    var url = 'api/AgrMstSupplierOnboard/PostInitiateSupplierApplication';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $scope.current = $state.current.name;
                            ScopeValueService.store("dataldCtrl", $scope);
                            $state.go('app.pageredirect');
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

                    $modalInstance.close('closed');
                }
            }
        }

        $scope.applcreation_view = function (application_gid) {
            $location.url('app/AgrMstCustomerOnboardingApproval?hash=' + cmnfunctionService.encryptURL('application_gid=' + application_gid + '&selectedIndex=' + '&lsapproved=byr' ));
        }

        $scope.suprapplcreation_view = function (application_gid) {
            $location.url('app/AgrMstSupplierOnboardingApproval?hash=' + cmnfunctionService.encryptURL('application_gid=' + application_gid + '&selectedIndex=' + '&lsapproved=supr' ));
        }
    }
})();
