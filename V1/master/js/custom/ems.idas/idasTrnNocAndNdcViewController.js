(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasTrnNocAndNdcViewController', idasTrnNocAndNdcViewController);

    idasTrnNocAndNdcViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function idasTrnNocAndNdcViewController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'idasTrnNocAndNdcViewController';
        $scope.nocandndc_gid = $location.search().lsnocandndc_gid;
        var nocandndc_gid = $scope.nocandndc_gid;
        activate();

        function activate() {
            //$scope.nocandndc_gid = localStorage.getItem('nocandndc_gid');
            
            var param = {
                nocandndc_gid: $scope.nocandndc_gid
            }
            
            lockUI();
            var url = 'api/IdasNocAndNdc/EditNoc';
            
            
            SocketService.getparams(url, param).then(function (resp) {
                $scope.cbomaker = resp.data.maker_name;
                $scope.cbochecker = resp.data.checker_name;
                $scope.lblnocrequestdate = resp.data.nocandndc_date;
                $scope.lblcustomer_name = resp.data.customer_name;
                $scope.cboVertical = resp.data.vertical_name;
                $scope.lblnocclosuredate = resp.data.noc_closure_date,
                $scope.lblsanction_ref_no = resp.data.sanction_ref_no;
                $scope.lblsanctiondate = resp.data.sanction_date;
                $scope.lblloan_account_no = resp.data.loan_account_no;
                $scope.lblnocissuancedate = resp.data.noc_issuance_date;
                $scope.lblloanclosuredate = resp.data.loan_closure_date;

                unlockUI();
            });

            var url = 'api/IdasNocAndNdc/GetNocDocumentEditList';
            SocketService.getparams(url, param).then(function (resp) {

                $scope.UploadDocumentList = resp.data.UploadNocDocumentList;
            });

        }

        $scope.downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();

            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.back = function () {
            $state.go('app.idasTrnNocAndNdc');
        }

    }
})();