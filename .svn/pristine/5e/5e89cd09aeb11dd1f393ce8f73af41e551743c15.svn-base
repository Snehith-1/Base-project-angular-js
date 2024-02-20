(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCheckerApprovalSummaryController', MstCheckerApprovalSummaryController);

    MstCheckerApprovalSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService'];

    function MstCheckerApprovalSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCheckerApprovalSummaryController';

        activate();

        function activate() {
            lockUI();

            var url = "api/IdasMstSanction/CheckerApprovalSummary";
            SocketService.get(url).then(function (resp) {
                $scope.sanctionlist = resp.data.sanctiondetails;
                unlockUI();
            });
        }
        
        $scope.checkerapprovalview = function (customer2sanction_gid) {
            $location.url('app/idasMstSanctionLetterWordView?sanction_gid=' + customer2sanction_gid + '&lspage=checkerapprovalsummary');
        }

        $scope.SanctionletterPDF = function (customer2sanction_gid) {
            var params = {
                sanction_gid: customer2sanction_gid
            };
            var url = 'api/IdasMstSanction/GetPDFGenerate';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                var phyPath =resp.data.lspath1;
                var filename1 = resp.data.lsname1;
                var phyPath = phyPath.replace("\\", "/");
                var phyPath = phyPath.replace("//", "/");
                var relPath = phyPath.split("EMS/");
                var relpath1 = relPath[1].replace("\\", "/");
                var url1 = filename1;
                var filename = url1.substring(url1.lastIndexOf('/')+1);                                                                      
               var url = 'api/azurestorage/FileUploadDocument';
                var params = {
                    file_path : relpath1
                }
                SocketService.post(url,params).then(function (resp) {
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(relpath1,filename1);
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export PDF !', 'warning');
                }
                });
                // var phyPath = resp.data.lspath1;
                // var relPath = phyPath.split("EMS");
                // var relpath1 = relPath[1].replace("\\", "/");
                // var hosts = window.location.host;
                // var prefix = location.protocol + "//";
                // var str = prefix.concat(hosts, relpath1);
                // var link = document.createElement("a");
                // var name = resp.data.lsname1.split(".")
                // link.download = name[0];
                // var uri = str;
                // link.href = uri;
                // link.click();
                unlockUI();
            });
        }
    }
})();
