(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnConsolidatedWorkItem', iasnConsolidatedWorkItem);

    iasnConsolidatedWorkItem.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService'];

    function iasnConsolidatedWorkItem($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnConsolidatedWorkItem';

        activate();

        function activate() {
            lockUI();
            $scope.assignto_team = false;
            $scope.assignto_employee = false;
            $scope.total = 0;
            $scope.totalDisplayed = 100;
            if ($scope.page == undefined) {
                localStorage.setItem('page', 'workitemsummary')
            }
            $scope.page = localStorage.getItem('page');

            var url = 'api/IasnTrnWorkItem/ConsolidatedWorkItem';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.WorkItemPending_List = resp.data.MdlWorkItem;
                if ($scope.WorkItemPending_List == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.WorkItemPending_List.length;
                    if ($scope.WorkItemPending_List.length < 100) {
                        $scope.totalDisplayed = $scope.WorkItemPending_List.length;
                    }
                }
            });
          
        }

        
        $scope.loadMore = function (pagecount) {
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            // new code start
            if ($scope.total != 0) {

                if (pagecount < $scope.total) {
                    $scope.totalDisplayed += Number;
                    if ($scope.total < $scope.totalDisplayed) {
                        $scope.totalDisplayed = $scope.total;
                        Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            unlockUI();
        };
        // Action Work Item 360
        $scope.WorkItem360 = function (val) {
            localStorage.setItem('email_gid', val)
            var params = {
                email_gid: val
            }
            $state.go("app.isanconsolidatedview");
        }

        $scope.view = function (val)
        {
            $location.url('app/isanconsolidatedview?lsemail_gid=' + val)
        }

        $scope.recproof_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }


        $scope.export = function () {
            lockUI();
            var url = 'api/IasnTrnWorkItem/GetConsolidateExcel';

            SocketService.get(url).then(function (resp) {

                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.excel_name);
                    //var phyPath = resp.data.excel_path;
                    //var name = resp.data.excel_name.split('.');
                    //recproof_downloads(phyPath, name);

                  /*/  var phyPath = resp.data.excel_path;
                    var relPath = phyPath.split("EMS");
                    var relpath1 = relPath[1].replace("\\", "/");
                    var hosts = window.location.host;
                    var prefix = location.protocol + "//";
                    var str = prefix.concat(hosts, relpath1);
                    var link = document.createElement("a");
                    var name = resp.data.excel_name.split('.');
                    link.download = name[0];
                    var uri = str;
                    link.href = uri;
                    link.click();*/
               
            });
        }
       }
})();
