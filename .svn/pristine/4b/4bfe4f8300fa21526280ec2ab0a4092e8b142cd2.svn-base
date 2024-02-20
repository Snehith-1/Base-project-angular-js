(function () {
    'use strict';

    angular
        .module('angle')
        .controller('IdasTrnDespatchDtlsView', IdasTrnDespatchDtlsView);

    IdasTrnDespatchDtlsView.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function IdasTrnDespatchDtlsView($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        $scope.title = 'IdasTrnDespatchDtlsView';
        var despatch_gid;
        activate();

        function activate() {

            despatch_gid = localStorage.getItem('despatch_gid');
           
            var url = 'api/IdasTrnFile2Despatch/DespatchDtls';
            var params = {
                despatch_gid: despatch_gid
            };

            SocketService.getparams(url, params).then(function (resp) {
              
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

            SocketService.getparams(url,params).then(function (resp) {

                $scope.uploaddocument = resp.data.uploaddocument;

            });

            var url = 'api/IdasTrnFile2Despatch/TaggedBoxDtls';
            var params = {
                despatch_gid:despatch_gid
            }

            SocketService.getparams(url,params).then(function (resp) {
                $scope.box_list = resp.data.MdlCartonBoxSummary;

            });

        }
        $scope.back=function()
        {
            $location.url('app/idasTrnFile2Despatch?lstab=despatch');
           // $state.go('app.idasTrnFile2Despatch');
        }
        $scope.gotoBox360=function(cartonbox_gid)
        {
            localStorage.setItem('cartonbox_gid', cartonbox_gid);
           
            $state.go('app.IdasTrnDespatchBoxDtlsView');
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
