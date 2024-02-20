(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCustomerCreationRequestSummaryController', MstCustomerCreationRequestSummaryController);

    MstCustomerCreationRequestSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCustomerCreationRequestSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCustomerCreationRequestSummaryController';
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;

        //$scope.selectallmenu_show = true;

        lockUI();
        activate();

        function activate() {
            var url = 'api/MstCADApplication/GetCustomerInitiatedSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.initiated_list = resp.data.initiated_list;               
                unlockUI();
            });
            var url = 'api/MstCADApplication/Getcount';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.total_count = resp.data.total_count;
                unlockUI();
            });

        }

        $scope.pending = function () {
            $location.url('app/MstCustomerCreationRequestSummary');
        }
        $scope.urn_update = function () {
            $location.url('app/MstCustomerUpdatedRequestSummary');
        }
        $scope.rejected = function () {
            $location.url('app/MstCustomerRejectedRequestSummary');
        }

        //$scope.urn_updatelms = function (cuclms_gid, application_gid) {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/Urnupdation.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'md'
        //    });
        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {

        //        $scope.cuclms_gid = cuclms_gid;

        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };

        //        $scope.update_status = function () {

        //            var params =
        //            {
        //                cuclms_gid: cuclms_gid,
        //                application_gid: application_gid,
        //                customer_urn: $scope.txt_urn
        //            }
        //            var url = 'api/UnreconciliationManagement/PostManualMatch';
        //            lockUI();
        //            SocketService.post(url, params).then(function (resp) {
        //                if (resp.data.status == true) {
        //                    unlockUI();
        //                    Notify.alert(resp.data.message, {
        //                        status: 'success',
        //                        pos: 'top-center',
        //                        timeout: 3000

        //                    });
        //                    activate();
        //                    $modalInstance.close('closed');
        //                    $state.go('app.brsTrnUnreconCreditSummaryManagement');

        //                }
        //                else {
        //                    $modalInstance.close('closed');
        //                    Notify.alert(resp.data.message, {
        //                        status: 'warning',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                }
        //                unlockUI();

        //            });

        //        }


        //    }
        //}


        $scope.urn_updatelms = function (cuclms_gid, application_gid) {

            $location.url('app/MstCustomercreationlmsurnupdation?cuclms_gid=' + cuclms_gid + '&application_gid=' + application_gid + '&lspage=CADCustomerURNupdateLMS');
        }




    }
})();
