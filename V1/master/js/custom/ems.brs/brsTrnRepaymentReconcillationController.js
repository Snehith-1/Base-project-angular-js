
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnRepaymentReconcillationController', brsTrnRepaymentReconcillationController);

    brsTrnRepaymentReconcillationController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function brsTrnRepaymentReconcillationController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnRepaymentReconcillationController';
        // console.log('test');
        activate();

        function activate() {

            var url = 'api/RepaymentReconcillation/GetRepaymentReconcillationSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.repayment_cocillation_list = resp.data.repayment_cocillation_list;
                unlockUI();

            });
            var url = "api/RepaymentReconcillation/GetRepaymentReconcillationCount";
            SocketService.get(url).then(function (resp) {
                $scope.repay_reconc_count = resp.data.repay_reconc_count;
                $scope.repay_unreconc_count = resp.data.repay_unreconc_count;

                unlockUI();
            });


        }
        $scope.repay_recon = function () {
            $state.go('app.brsTrnRepaymentReconcillation');
        }

        $scope.repay_unrecon = function () {
            $state.go('app.brsTrnRepaymentUnReconcillation');
        }
        $scope.view = function (banktransc_gid) {
            /* $location.url('app/brsTrnUnreconTagViewAssignedHistory?banktransc_gid=' + banktransc_gid);*/
            $location.url("app/BrsTrnUnreconRepaymentMatchedView?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid));
        }
    }
})();
