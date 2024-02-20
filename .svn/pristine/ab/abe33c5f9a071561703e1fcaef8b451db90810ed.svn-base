
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnRepaymentUnReconcillationController', brsTrnRepaymentUnReconcillationController);

    brsTrnRepaymentUnReconcillationController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function brsTrnRepaymentUnReconcillationController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnRepaymentUnReconcillationController';
        // console.log('test');
        activate();

        function activate() {

            var url = 'api/RepaymentReconcillation/GetRepaymentUnReconcillationSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.repayment_unrecocillation_list = resp.data.repayment_unrecocillation_list;
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
    }
})();
