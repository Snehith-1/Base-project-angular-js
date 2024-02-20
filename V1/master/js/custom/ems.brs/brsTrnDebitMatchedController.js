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
        .controller('brsTrnDebitMatchedController', brsTrnDebitMatchedController);

    brsTrnDebitMatchedController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function brsTrnDebitMatchedController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnDebitMatchedController';
        // console.log('test');
        activate();

        function activate() {
            var url = 'api/RepaymentReconcillation/GetDebitMatchedSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.Debitmatched_list = resp.data.Debitmatched_list;
                unlockUI();

            });
            var url = "api/RepaymentReconcillation/GetCreditCount";
            SocketService.get(url).then(function (resp) {
                $scope.credit_count = resp.data.credit_count;
                unlockUI();
            });

            var url = "api/RepaymentReconcillation/GetDebitCount";
            SocketService.get(url).then(function (resp) {
                $scope.debit_count = resp.data.debit_count;
                $scope.debitmatch_count = resp.data.debitmatch_count;
                $scope.partialdebit_count = resp.data.partialdebit_count;
                $scope.unmatchunassign_count = resp.data.unmatchunassign_count;
                $scope.unmatchassign_count = resp.data.unmatchassign_count;
                $scope.debitclose_count = resp.data.debitclose_count;
                unlockUI();
            });


        }
        $scope.recon = function () {
            $state.go('app.brsTrnCreditMatched');
        }
        $scope.unrecon = function () {
            $state.go('app.brsTrnDebitMatched');

        }
        $scope.partialmatch = function () {
            $state.go('app.brsTrnDebitPartialMatched');
        }
        $scope.unmatch = function () {
            $state.go('app.brsTrnDebitUnMatchedUnAssigned');
        }
        $scope.assign = function () {
            $state.go('app.brsTrnDebitUnMatchedAssigned');
        }
        $scope.close = function () {
            $state.go('app.brsTrnDebitClosed');
        }
        $scope.view = function (banktransc_gid) {
            /* $location.url('app/brsTrnUnreconTagViewAssignedHistory?banktransc_gid=' + banktransc_gid);*/
            $location.url("app/BrsTrnUnreconDebitBankMatchedView?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid));
        }
    }
})();
