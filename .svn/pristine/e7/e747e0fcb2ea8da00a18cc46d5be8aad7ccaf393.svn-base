(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnTrnConsolidatedReport', iasnTrnConsolidatedReport);

    iasnTrnConsolidatedReport.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService'];

    function iasnTrnConsolidatedReport($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnTrnConsolidatedReport';

        activate();

        function activate() {
            // Calender Popup... //

            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            // Calender Popup... //

            vm.calender2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open2 = true;
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            lockUI();
            var url = 'api/IasnTrnWorkItem/GetConsolidatedReport';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.WorkItem_List = resp.data.MdlWorkItem;
            });
        }

        $scope.view = function (val) {
            $location.url('app/isanconsolidatedview?lsemail_gid=' + val + '&lstab=ConsolidatedReport')
        }

        $scope.export = function () {
            if ($scope.emailfrom_date == undefined || $scope.emailfrom_date == "") {
                Notify.alert("Kindly Select From and To date", 'warning');
            }
            else if ($scope.emailto_date == undefined || $scope.emailto_date == "") {
                Notify.alert("Kindly Select From and To date", 'warning');
            }
            else {
                lockUI();
                var emailfrom_date1 = $scope.emailfrom_date;

                var emailfrom_date = new Date(emailfrom_date1.getTime() - (emailfrom_date1.getTimezoneOffset() * 60000)).toISOString().split("T")[0];
                var emailto_date1 = $scope.emailto_date;

                var emailto_date = new Date(emailto_date1.getTime() - (emailto_date1.getTimezoneOffset() * 60000)).toISOString().split("T")[0];

                var url = 'api/IasnTrnWorkItem/GetConsolidatedReportExcel';
                var param = {
                    emailfrom_date: emailfrom_date,
                    emailto_date: emailto_date
                }
                SocketService.getparams(url, param).then(function (resp) {
///if (resp.data.status == true) {
                        unlockUI();
                        DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.excel_name);
                       /* var phyPath = resp.data.excel_path;
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
    }
})();