(function () {
    'use strict';

    angular
        .module('angle')
        .controller('lsaManagementController', lsaManagementController);

    lsaManagementController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function lsaManagementController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        $scope.title = 'lsaManagementController';

        activate();

        function activate() {
            $scope.totalDisplayed = 100;
            lockUI();
            var url = "api/IdasTrnLsaManagement/lsadetails";
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.list = resp.data.lsa_list;
                $scope.total = $scope.list.length;
            });
          
        }
        document.getElementById('pagecount').onkeyup = function () {
            if ($scope.pagecount == null) {
                var el = document.getElementById('loadmore');
                el.style.backgroundColor = '#DCDCDC';
            }
            else {
                var el = document.getElementById('loadmore');
                el.style.backgroundColor = '#ffa';
            }
        };
        $scope.loadMore = function (pagecount) {
            lockUI();
            var Number = parseInt(pagecount);
            $scope.totalDisplayed += Number;
            unlockUI();
        };

        $scope.clickadd = function (lsacreate_gid)
        {
            $scope.lsacreate_gid = localStorage.setItem('lsacreate_gid', lsacreate_gid);
            $scope.lsastatus = localStorage.setItem('lsacreate_gid', lsacreate_gid);
            $state.go('app.lsaManagementadd');
        }
        $scope.lsaadd = function (lsacreate_gid) {
            $scope.lsacreate_gid = localStorage.setItem('lsacreate_gid', lsacreate_gid);
           
            $location.url('app/lsaManagementadd?lstab=approved');
        }
        $scope.createlsa=function()
        {
            $state.go('app.createLSA');
        }
        $scope.clickView = function (lsacreate_gid) {
            $scope.lsacreate_gid = localStorage.setItem('lsacreate_gid', lsacreate_gid);          
            $state.go('app.viewLSA');
        }
        $scope.LSApdf = function (lsacreate_gid) {
            var params = {
                lsacreate_gid: lsacreate_gid
            };
            var url = 'api/IdasTrnLsaManagement/GetLSApdf';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {   
                unlockUI();
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(resp.data.file_path, "LSA Report.pdf");
                    Notify.alert('LSA Report Downloaded Successfully', 'success');
                }
                else {
                   
                    Notify.alert('Error Occurred While Export PDF !', 'warning');
                }           
                // var phyPath = resp.data;
                // var relPath = phyPath.split("EMS");
                // var relpath1 = relPath[1].replace("\\", "/");
                // var hosts = window.location.host;
                // var prefix = location.protocol + "//";
                // var str = prefix.concat(hosts, relpath1);
                // var link = document.createElement("a");
                // link.download = "LSA Report";
                // var uri = str;
                // link.href = uri;
                // link.click();
                // Notify.alert('LSA Report Downloaded Successfully', 'success')
                unlockUI();
            //     var phyPath = resp.data;
            //     var filename1 = resp.data;
            //     var phyPath = phyPath.replace("\\", "/");
            //     var phyPath = phyPath.replace("//", "/");
            //     var relPath = phyPath.split("EMS/");
            //     var relpath1 = relPath[1].replace("\\", "/");
            //     var url1 = filename1;
            //     var filename = url1.substring(url1.lastIndexOf('/')+1);                                                                      
            //    var url = 'api/azurestorage/FileUploadDocument';
            //     var params = {
            //         file_path : relpath1
            //     }
            //     SocketService.post(url,params).then(function (resp) {
            //     if (resp.data.status == true) {
            //         DownloaddocumentService.Downloaddocument(relpath1, "LSA Report.pdf");
            //         Notify.alert('LSA Report Downloaded Successfully', 'success');
            //     }
            //     else {
            //         unlockUI();
            //         Notify.alert('Error Occurred While Export PDF !', 'warning');
            //     }
            //     });
            });
        }
      
    }
})();
