(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstRMCadUrnAcceptedCustomerDtlsController', MstRMCadUrnAcceptedCustomerDtlsController);

    MstRMCadUrnAcceptedCustomerDtlsController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstRMCadUrnAcceptedCustomerDtlsController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstRMCadUrnAcceptedCustomerDtlsController';

        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.customer_urn = $location.search().customer_urn;
        var customer_urn = $scope.customer_urn;

        activate();
        lockUI();
        function activate() {
            var params = {
                customer_urn: customer_urn
            }

            var url = 'api/MstCAD/GetCADUrnGroupingDtlsSummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.urngrouping_list = resp.data.cadapplicationlist;
            });

            var url = 'api/MstCAD/GetRMRnewalApplicationSummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.rmrenewalapplication_list = resp.data.cadapplicationlist;
            });
        }

        $scope.view = function (val, val1) {
            $location.url('app/MstCadApplicationView?application_gid=' + val + '&customer_urn=' + val1 + '&lspage=RMCADUrnGrouping');
        }

        $scope.process = function (application_gid, val1) {
            $location.url('app/MstPostCcActivitiesRMView?application_gid=' + application_gid + '&customer_urn=' + val1 + '&lspage=RMCADUrnGrouping');
        }

        $scope.Back = function (application_gid, val1) {
            $location.url('app/MstRMCustomerSummary?application_gid=' + application_gid + '&customer_urn=' + val1 + '&lspage=RMCADUrnGrouping');
        }

        $scope.enhancement = function (application_gid, customer_urn) {
            var modalInstance = $modal.open({
                templateUrl: '/Enhancementpopup.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.enhancementedshow = false;
                $scope.enhancementshow = true;
                $scope.enhancement_confirm = function () {
                    var params = {
                        customer_urn: customer_urn
                    }
                    lockUI();
                    var url = 'api/MstApplicationAdd/PostEnhancementAdd';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            $scope.enhancementapplication_no = resp.data.application_no;
                            $scope.enhancementedshow = true;
                            $scope.enhancementshow = false;
                        }
                        else {
                            alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                            unlockUI;
                        }
                    });
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                    activate();
                };


            }

        }

    }
})();
