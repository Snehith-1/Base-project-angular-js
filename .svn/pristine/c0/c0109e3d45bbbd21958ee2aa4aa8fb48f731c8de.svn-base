(function () {
    'use strict';

    angular
        .module('angle')
        .controller('IdasTrnBoxDtlsView', IdasTrnBoxDtlsView);

    IdasTrnBoxDtlsView.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function IdasTrnBoxDtlsView($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        $scope.title = 'IdasTrnBoxDtlsView';
        var cartonbox_gid;
        activate();

        function activate() {

            cartonbox_gid = localStorage.getItem('cartonbox_gid');

            var url = 'api/IdasTrnFile2Despatch/BoxDtls';
            var params = {
                cartonbox_gid: cartonbox_gid
            };

            SocketService.getparams(url, params).then(function (resp) {
                console.log(resp.data);
                $scope.boxref_no = resp.data.boxref_no;
                $scope.stampref_no = resp.data.stampref_no;
                $scope.cartonbox_date = resp.data.cartonbox_date;
                $scope.remarks = resp.data.remarks;
                $scope.boxbarcoderef_no = resp.data.boxbarcoderef_no;
            });

            var url = 'api/IdasTrnFile2Despatch/TaggedBatchDtls';
            var params = {
                cartonbox_gid: cartonbox_gid
            };

            SocketService.getparams(url, params).then(function (resp) {
                $scope.batch_list = resp.data.MdlbatchSummary;
             
            });


        }

        $scope.back=function()
        {
            // $state.go('app.idasTrnFile2Despatch');
            $location.url('app/idasTrnFile2Despatch?lstab=box');
        }

        $scope.gotoBatch360=function(sanction_gid)
        {
            localStorage.setItem('page', 'box');
            localStorage.setItem('sanction_gid', sanction_gid);
            $state.go('app.IdasTrnBatchView');
        }
    }
})();
