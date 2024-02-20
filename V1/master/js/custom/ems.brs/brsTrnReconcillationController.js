
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnReconcillationController', brsTrnReconcillationController);

        brsTrnReconcillationController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function brsTrnReconcillationController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnReconcillationController';
       // console.log('test');
        activate();

        function activate() {
          
            var url = 'api/RepaymentReconcillation/GetreConcillationSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.cocillationcredit_list = resp.data.cocillationcredit_list;
                unlockUI();

            });
            var url = "api/RepaymentReconcillation/GetunreConcillationCount";
            SocketService.get(url).then(function (resp) {
                $scope.reconc_count = resp.data.reconc_count;
                $scope.unreconc_count = resp.data.unreconc_count;
                $scope.reconcredit_count = resp.data.reconcredit_count;
                $scope.recondebit_count = resp.data.recondebit_count;

                unlockUI();
            });

            
        }
        $scope.recon = function () {
            $state.go('app.brsTrnReconcillation');
        }

        $scope.unrecon = function () {
            $state.go('app.brsTrnUnReconcillation');
        }
        $scope.recondebit = function () {
            $state.go('app.brsTrnReconcillationdebit');
        }
        
    }
})();
