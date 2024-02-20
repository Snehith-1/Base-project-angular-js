// JavaScript source code
// (function () {
//     'use strict';

//     angular
//         .module('angle')
//         .controller('brsTrnReconcillationController', brsTrnReconcillationController);
//     brsTrnReconcillationController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService', 'cmnfunctionService'];

//     function brsTrnReconcillationController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService, cmnfunctionService) {
//         var vm = this;
//         vm.title = 'brsTrnReconcillationController';

//         activate();
//         lockUI();
//         function activate() {


//         }

//     }
// })();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnUnReconcillationCompleteddebitManagementController', brsTrnUnReconcillationCompleteddebitManagementController);

    brsTrnUnReconcillationCompleteddebitManagementController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function brsTrnUnReconcillationCompleteddebitManagementController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnUnReconcillationCompleteddebitManagementController';
        // console.log('test');
        activate();

        function activate() {

            //var url = 'api/KotakReconcillation/GetTransactionSummary';
            //lockUI();
            //SocketService.get(url).then(function (resp) {
            //    $scope.transaction_list = resp.data.transaction_list;
            //    unlockUI();

            //});
            var url = 'api/UnreconciliationManagement/GetUnReconciliationAssigned';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.banktransc_gid = resp.data.banktransc_gid;
                $scope.BankAlertUnreconciliationdebit_list = resp.data.BankAlertUnreconciliationdebit_list;
                unlockUI();
            });
            var url = "api/UnreconciliationManagement/GetunreConcillationCount";
            SocketService.get(url).then(function (resp) {
                $scope.unreconpendingdebit_count = resp.data.unreconpendingdebit_count;
                $scope.unreconpendingcredit_count = resp.data.unreconpendingcredit_count;
                $scope.unreconpending_count = resp.data.unreconpending_count;
                $scope.unreconcompdebit_count = resp.data.unreconcompdebit_count;
                $scope.unreconcompcredit_count = resp.data.unreconcompcredit_count;
                $scope.unreconcomp_count = resp.data.unreconcomp_count;

                unlockUI();
            });
        }
        $scope.unreconpending = function () {
            $state.go('app.brsTrnUnReconcillationManagement');
        }
        $scope.view = function (banktransc_gid) {
           /* $location.url('app/brsTrnUnreconTagViewAssignedHistory?banktransc_gid=' + banktransc_gid);*/
            $location.url("app/brsTrnUnreconTagViewAssignedHistory?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid));
        }

        $scope.compunrecon = function () {
            $state.go('app.brsTrnUnReconcillationCompletedManagement');
        }
        $scope.unreconcompdebit = function () {
            $state.go('app.brsTrnUnReconcillationCompleteddebitManagement');
        }

        $scope.Transfer = function (banktransc_gid) {
        /*//    $location.url('app/brsTrnUnreconcillationTransfer?banktransc_gid=' + banktransc_gid);*/
            $location.url("app/brsTrnUnreconcillationTransfer?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid));
        }
    }
})();
