(function () {
    'use strict';

    angular
        .module('angle')
        .controller('lsaapprovalController', lsaapprovalController);

    lsaapprovalController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService'];

    function lsaapprovalController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        $scope.title = 'lsaapprovalController';

        activate();

        function activate() {
            $scope.tab = {};
            $scope.totalDisplayedpending = 100;
            $scope.totalDisplayedapproved = 100;
            var url = "api/IdasTrnLsaManagement/LSAapprovalpendinginfo";
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.pendinglist = resp.data.lsa_list;
                $scope.approvedlist = resp.data.approvedlsa_list;
                if ($scope.pendinglist == null) {
                    $scope.pendingCount = 0;
                }
                else {
                    $scope.pendingCount = $scope.pendinglist.length;
                }
                if ($scope.approvedlist == null) {
                    $scope.approvedCount = 0;
                }
                else {
                    $scope.approvedCount = $scope.approvedlist.length;
                }
                $scope.pendinglsa_count = resp.data.pendinglsa_count;
                $scope.approvedlsa_count = resp.data.approvedlsa_count;
            });
            var url = window.location.href;
            var relPath = url.split("lstab=");
            var relpath1 = relPath[1];
            if (relpath1 != undefined) {
                if (relpath1 == "Pending") {
                    $scope.tabpending = true;
                }
                else if (relpath1 == "Approved") {
                    $scope.tabapproved = true;
                }

            }
            else {
                if ($scope.tab.activeTabId == undefined) {
                    $scope.tabpending = true;
                }
                else if ($scope.tab.activeTabId == 'Pending') {
                    $scope.tabpending = true;

                }
                else if ($scope.tab.activeTabId == 'Approved') {
                    $scope.tabapproved = true;
                }

            }
        }
   
        $scope.loadMorepending = function (pagecountpending) {
            lockUI();
            var Number = parseInt(pagecountpending);

            $scope.totalDisplayedpending += Number;
            unlockUI();
        };
        $scope.loadMoreapproved = function (pagecountapproved) {
            lockUI();
            var Number = parseInt(pagecountapproved);

            $scope.totalDisplayedapproved += Number;
            unlockUI();
        };
        $scope.LSApdf = function (lsacreate_gid) {


            var params = {
                lsacreate_gid: lsacreate_gid

            };
            console.log(params);
            var url = 'api/IdasTrnLsaManagement/GetLSApdf';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
               
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(resp.data.file_path, "LSA Report.pdf");
                    Notify.alert('LSA Report Downloaded Successfully', 'success');
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export PDF !', 'warning');
                }
            });

        }
        $scope.loadMore = function (pagecount) {
            lockUI();
            var Number = parseInt(pagecount);

            $scope.totalDisplayed += Number;
            unlockUI();
        };
        $scope.View = function (lsacreate_gid)
        {
            $scope.lsacreate_gid = localStorage.setItem('lsacreate_gid', lsacreate_gid);

            $location.url('app/IdasTrnLSAapprovalview?lstab=Approved');

          
        }
        $scope.lsaapproval = function (lsacreate_gid) {
            $scope.lsacreate_gid = localStorage.setItem('lsacreate_gid', lsacreate_gid);

            $location.url('app/IdasTrnLSAapprovalview?lstab=Pending');

        }
    }
})();
