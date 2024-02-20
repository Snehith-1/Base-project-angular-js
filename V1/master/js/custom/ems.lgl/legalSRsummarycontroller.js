(function () {
    'use strict';

    angular
        .module('angle')
        .controller('legalSRsummarycontroller', legalSRsummarycontroller);

    legalSRsummarycontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function legalSRsummarycontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'legalSRsummarycontroller';

        activate();


        function activate() {
            var url = 'api/raiseLegalSR/GetSR';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.mdlMisdataimport = resp.data.RaiselegalSR_list;
                console.log(resp.data);
            });

        }
      
        $scope.raisesr = function (templegalsr_gid, customer_gid)
        {
            $scope.templegalsr_gid = localStorage.setItem('templegalsr_gid', templegalsr_gid);
            $scope.customer_gid = localStorage.setItem('customer_gid', customer_gid);
            console.log(templegalsr_gid);
            $state.go('app.raiseSR2authentication');
        }
        $scope.view = function (legalsr_gid, legalsr_customergid) {
            $scope.templegalsr_gid = localStorage.setItem('templegalsr_gid', templegalsr_gid);
            $scope.customer_gid = localStorage.setItem('customer_gid', customer_gid);
            console.log(templegalsr_gid); //console.log(legalsr_gid);
            $state.go('app.viewlegalSR');
        }

        $scope.popuplegalSR = function () {

            $state.go('app.raiselegalSR');
        }

        //$scope.popuplegalSR = function () {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/LegalSRraise.html',
        //        controller: ModalInstanceCtrl,
        //        size: 'md'
        //    });
        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {
        //        var url = 'api/raiseLegalSR/Customer';
        //        SocketService.get(url).then(function (resp) {
        //            $scope.customer_list = resp.data.customer_list;

        //        });

        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };
        //        $scope.legalsrsubmit = function (customer) {
        //            var account_name = $('#customername :selected').text();

        //            var params = {
        //                customer_gid: customer,
        //                account_name: account_name,
        //                remarks: $scope.txtremarks
        //            }
        //            console.log(params);
        //            var url = 'api/raiseLegalSR/raiselegalsr';

        //            lockUI();
        //            SocketService.post(url, params).then(function (resp) {
        //                unlockUI();
        //                if (resp.data.status == true) {

        //                    $modalInstance.close('closed');
        //                    Notify.alert(resp.data.message, {
        //                        status: 'success',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                    activate();

        //                }
        //                else {
        //                    Notify.alert(resp.data.message, {
        //                        status: 'warning',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });

        //                    activate();

        //                }
        //            });
        //        }
        //    }

        //}


        $scope.edit = function () {

            $state.go('app.editLegalSR');
        }
    }
})();
