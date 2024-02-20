(function () {
    'use strict';

    angular
        .module('angle')
        .controller('IdasTrnDespatchBoxDtlsView', IdasTrnDespatchBoxDtlsView);

    IdasTrnDespatchBoxDtlsView.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout', 'DownloaddocumentService'];

    function IdasTrnDespatchBoxDtlsView($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout, DownloaddocumentService) {
        $scope.title = 'IdasTrnDespatchBoxDtlsView';
        var despatch_gid;
        var cartonbox_gid;
        activate();

        function activate() {
            despatch_gid = localStorage.getItem('despatch_gid');

            var url = 'api/IdasTrnFile2Despatch/DespatchDtls';
            var params = {
                despatch_gid: despatch_gid
            };

            SocketService.getparams(url, params).then(function (resp) {
                console.log(resp.data);
                $scope.despatchref_no = resp.data.despatchref_no;
                $scope.despatch_date = resp.data.despatch_date;
                $scope.vendor_name = resp.data.vendor_name;
                $scope.remarks = resp.data.remarks;
                $scope.contact_person = resp.data.contact_person;
                $scope.mobile_no = resp.data.mobile_no;
                $scope.desptached_by = resp.data.desptached_by;
                $scope.stampref_no = resp.data.stampref_no;

            });

            var url = 'api/IdasTrnFile2Despatch/DespatchDocument';
            var params = {
                despatch_gid: despatch_gid
            };

            SocketService.getparams(url, params).then(function (resp) {

                $scope.uploaddocument = resp.data.uploaddocument;

            });

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
            $state.go('app.IdasTrnDespatchDtlsView')
        }
        $scope.gotoBatch360=function(sanction_gid)
        {
            localStorage.setItem('sanction_gid', sanction_gid);
            localStorage.setItem('page', 'Despatch');
            $state.go('app.IdasTrnBatchView');
        }
        $scope.downloadsdocument = function (val1, val2) {

            //var phyPath = val1;

            //var relPath = phyPath.split("EMS");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = "http://"
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //var name = val2.split(".")
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
    }
})();
