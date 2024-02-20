(function () {
    'use strict';

    angular
        .module('angle')
        .controller('dnTrackerReportcontroller', dnTrackerReportcontroller);

    dnTrackerReportcontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route','DownloaddocumentService'];

    function dnTrackerReportcontroller($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route,DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'dnTrackerReportcontroller';

        activate();
        function activate() {
            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened = true;
            };
            vm.close = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.closed = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];

            var date = new Date(),
             mnth = ("0" + (date.getMonth() + 1)).slice(-2),
             day = ("0" + date.getDate()).slice(-2);
            $scope.date = [date.getFullYear(), mnth, day].join("-");
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];

            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            var params = {
                date:$scope.date
            }

            var url = 'api/lglDashboard/GetdntrackerReport';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
 
                $scope.dn_list = resp.data.dn_list;
                $scope.dn1send_count = resp.data.dn1send_count;
                $scope.dn2send_count = resp.data.dn2send_count;
                $scope.dn3send_count = resp.data.dn3send_count;
                $scope.dn1skip_count = resp.data.dn1skip_count;
                $scope.dn2skip_count = resp.data.dn2skip_count;
                $scope.dn3skip_count = resp.data.dn3skip_count;
                unlockUI();
            });
         

            }
      
        $scope.export = function () {
            var params = {
                //customer_gid: $scope.customer_gid,
                //vertical_gid: $scope.vertical,
                //branch_gid: $scope.branch,
                //entity_gid: $scope.entity_gid,
                //relationshipMgmt: $scope.relationshipMgmt,
                //zonalHead: $scope.zonalHead,
                //businessHead: $scope.businessHead,
                //clustermanager: $scope.clustermanager,
                //creditmanager: $scope.creditmanager
            }
            lockUI();
            var url = 'api/lglDashboard/DNexport';
            SocketService.post(url, params).then(function (resp) {
                console.log(resp.data.status)
               if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(resp.data.lspath, resp.data.lsname);
                    unlockUI();
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

                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'success')
                    activate();

                }

            });
        }

        $scope.search = function () {
            if ($scope.date == undefined || $scope.date == "") {
                var date = 'null';
            }
            else {
                var date = $scope.date;

                var date = new Date(date.getTime() - (date.getTimezoneOffset() * 60000))
                                    .toISOString()
                                    .split("T")[0];
            }
            var params = {
                date: date
            }
            console.log(params)
            var url = 'api/lglDashboard/GetdntrackerReport_IST';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {           
                $scope.dn_list = resp.data.dn_list;
                $scope.dn1send_count = resp.data.dn1send_count;
                $scope.dn2send_count = resp.data.dn2send_count;
                $scope.dn3send_count = resp.data.dn3send_count;
                $scope.dn1skip_count = resp.data.dn1skip_count;
                $scope.dn2skip_count = resp.data.dn2skip_count;
                $scope.dn3skip_count = resp.data.dn3skip_count;
                unlockUI();
            });
        }
        

    }
})();
