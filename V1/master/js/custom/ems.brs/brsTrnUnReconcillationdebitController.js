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
        .controller('brsTrnUnReconcillationdebitController', brsTrnUnReconcillationdebitController);

    brsTrnUnReconcillationdebitController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function brsTrnUnReconcillationdebitController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnUnReconcillationdebitController';
        // console.log('test');
        activate();

        function activate() {

            var url = 'api/RepaymentReconcillation/GetunreConcillationSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.uncocillationdebit_list = resp.data.uncocillationdebit_list;
                unlockUI();

            });
            var url = "api/RepaymentReconcillation/GetunreConcillationCount";
            SocketService.get(url).then(function (resp) {
                $scope.reconc_count = resp.data.reconc_count;
                $scope.unreconc_count = resp.data.unreconc_count;
                $scope.unreconcredit_count = resp.data.unreconcredit_count;
                $scope.unrecondebit_count = resp.data.unrecondebit_count;

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
