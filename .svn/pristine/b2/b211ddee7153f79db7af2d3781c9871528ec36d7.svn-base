(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasMstSanctionSummary', idasMstSanctionSummary);

    idasMstSanctionSummary.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function idasMstSanctionSummary($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        $scope.title = 'idasMstSanctionSummary';

        activate();

        function activate() {

            $scope.totalDisplayedpending = 100;
            $scope.totalDisplayedapproved = 100;
            $scope.tab = {};
            lockUI();
            var url = "api/IdasMstSanction/PendingSanctionDtl";
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.sanctionlist = resp.data.sanctiondetails;
                 $scope.pending_count = resp.data.pending_count;
                $scope.completed_count = resp.data.completed_count;
                $scope.pendingCount = $scope.sanctionlist.length;
              });
            var url = window.location.href;
            var relPath = url.split("lstab=");
            var relpath1 = relPath[1];
            if (relpath1 != undefined) {
                if (relpath1 == "pending") {
                    $scope.tabpending = true;
                }
                else if (relpath1 == "completed") {
                    $scope.tabcompleted = true;
                }
              
            }
            else {
                if ($scope.tab.activeTabId == undefined) {
                    $scope.tabpending = true;
                }
                else if ($scope.tab.activeTabId == 'pending') {
                    $scope.tabpending = true;

                }
                else if ($scope.tab.activeTabId == 'completed') {
                    $scope.tabcompleted = true;
                }
               
            }
            if(relpath1=='completed')
            {
                $scope.totalDisplayedcompleted = 100;
                lockUI();
                var url = "api/IdasMstSanction/CompletedSanctionDtl";
                SocketService.get(url).then(function (resp) {

                    $scope.completed_sanctiondetails = resp.data.completed_sanctiondetails;
                    $scope.pending_count = resp.data.pending_count;
                    $scope.completed_count = resp.data.completed_count;

                    $scope.completedCount = $scope.completed_sanctiondetails.length;
                });
                unlockUI();
            }
        }
        $scope.completed=function()
        {
            $scope.totalDisplayedcompleted = 100;
            lockUI();
            var url = "api/IdasMstSanction/CompletedSanctionDtl";
            SocketService.get(url).then(function (resp) {
              
                $scope.completed_sanctiondetails = resp.data.completed_sanctiondetails;
                $scope.pending_count = resp.data.pending_count;
                $scope.completed_count = resp.data.completed_count;

                $scope.completedCount = $scope.completed_sanctiondetails.length;
            });
            unlockUI();
        }
        $scope.pending = function () {
            lockUI();
            var url = "api/IdasMstSanction/PendingSanctionDtl";
            SocketService.get(url).then(function (resp) {
                $scope.sanctionlist = resp.data.sanctiondetails;
                $scope.pending_count = resp.data.pending_count;
                $scope.completed_count = resp.data.completed_count;
                $scope.pendingCount = $scope.sanctionlist.length;
            
            });
            unlockUI();
        }
        $scope.createSanction = function () {

            $state.go('app.idasMstCreateSanction');
        }
      
        $scope.EditSanction_completed = function (sanction_gid) {
            localStorage.setItem('sanction_gid', sanction_gid);
            
            $location.url('app/idasMstSanctionEdit?lstab=completed');
           
        }

        $scope.EditSanction_pending = function (sanction_gid) {
            localStorage.setItem('sanction_gid', sanction_gid);
          
            $location.url('app/idasMstSanctionEdit?lstab=pending');

        }

        $scope.editgenerate = function (sanction_gid) {
            $location.url('app/idasMstSanctionLetterGeneration?sanction_gid=' + sanction_gid + '&lstab=pending');
        }
        $scope.generate = function (sanction_gid) {
            $location.url('app/idasMstSanctionLetterGeneration?sanction_gid=' + sanction_gid + '&lstab=completed');
        }

        $scope.loadMorepending = function (pagecountpending) {
            lockUI();
            var Number = parseInt(pagecountpending);

            $scope.totalDisplayedpending += Number;
            unlockUI();
        };
        $scope.loadMorecompleted = function (pagecountcompleted) {
            lockUI();
            var Number = parseInt(pagecountcompleted);

            $scope.totalDisplayedcompleted += Number;
            unlockUI();
        };
      
        $scope.WordGenerate = function (customer2sanction_gid) {
            var params = {
                sanction_gid: customer2sanction_gid
            };
            var url = 'api/IdasMstSanction/GetWordGenerate';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    var phyPath = resp.data.lspath;
                var filename1 = resp.data;
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
                    DownloaddocumentService.Downloaddocument(relpath1, filename);
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export PDF !', 'warning');
                }
                });
                    // var phyPath = resp.data.lspath;
                    // var relPath = phyPath.split("EMS");
                    // var relpath1 = relPath[1].replace("\\", "/");
                    // var hosts = window.location.host;
                    // var prefix = location.protocol + "//";
                    // var str = prefix.concat(hosts, relpath1);
                    // var link = document.createElement("a");
                    // var name = resp.data.lsname.split('.');
                    // link.download = name[0];
                    // var uri = str;
                    // link.href = uri;
                    // link.click();
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Downloading !', 'warning')
                    activate();

                }
            });
        }
 
    }
})();
