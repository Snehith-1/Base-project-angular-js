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
        .controller('brsTrnDebitClosedController', brsTrnDebitClosedController);

        brsTrnDebitClosedController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function brsTrnDebitClosedController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnDebitClosedController';
        // console.log('test');
        activate();

        function activate() {
            var url = 'api/RepaymentReconcillation/GetDebitClosedSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.debitClosed_list = resp.data.debitClosed_list;
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
    }
})();
