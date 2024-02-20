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
        .controller('brsTrnReconcillationdebitController', brsTrnReconcillationdebitController);

    brsTrnReconcillationdebitController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function brsTrnReconcillationdebitController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnReconcillationdebitController';
        // console.log('test');
        activate();

        function activate() {

            var url = 'api/RepaymentReconcillation/GetreConcillationSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.cocillationdebit_list = resp.data.cocillationdebit_list;
                unlockUI();

            });
            var url = "api/RepaymentReconcillation/GetunreConcillationCount";
            SocketService.get(url).then(function (resp) {
                $scope.reconc_count = resp.data.reconc_count;
                $scope.unreconc_count = resp.data.unreconc_count;

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
