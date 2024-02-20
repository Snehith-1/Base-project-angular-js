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
        .controller('brsTrnUnReconcillationPendingdebitManagementController', brsTrnUnReconcillationPendingdebitManagementController);

    brsTrnUnReconcillationPendingdebitManagementController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function brsTrnUnReconcillationPendingdebitManagementController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnUnReconcillationPendingdebitManagementController';
        // console.log('test');
        activate();

        function activate() {

            var url = 'api/UnreconciliationManagement/GetunreConcillationSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.unrecocillationpendingdebit_list = resp.data.unrecocillationpendingdebit_list;
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
        $scope.Unreconciliationtag = function (banktransc_gid) {
        //    $location.url('app/brsTrnUnreconcillationTag?banktransc_gid=' + banktransc_gid);
             $location.url("app/brsTrnUnreconcillationTag?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid));
        }
                   //}


        $scope.unreconpending = function () {
            $state.go('app.brsTrnUnReconcillationManagement');
        }

        $scope.compunrecon = function () {
            $state.go('app.brsTrnUnReconcillationCompletedManagement');
        }
        $scope.unreconpendingdebit = function () {
            $state.go('app.brsTrnUnReconcillationPendingdebitManagement');
        }
        $scope.Manualmatch = function (banktransc_gid) {

            var params =
            {
                banktransc_gid: banktransc_gid,
            }
            var url = 'api/UnreconciliationManagement/PostManualMatch';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();



                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                $state.go('app.brsTrnUnReconcillationPendingdebitManagement');

            });
            activate();


        }


    }
})();
