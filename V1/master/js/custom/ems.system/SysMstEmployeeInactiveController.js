(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstEmployeeInactiveController', SysMstEmployeeInactiveController);

        SysMstEmployeeInactiveController.$inject = ['$rootScope', '$modal','$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route','ngDialog', 'SweetAlert','DownloaddocumentService'];

    function SysMstEmployeeInactiveController($rootScope, $modal, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngDialog, SweetAlert, DownloaddocumentService) {

        activate();
        var vm = this;
        vm.title = 'SysMstEmployeeInactiveController';
        var lstab='inactive';

        function activate() {

            var url = 'api/ManageEmployee/EmployeeInactiveSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.employeeinactive_list = resp.data.employee;
                unlockUI();
            });
        }

       

        $scope.employee_view = function (employee_gid) {
            $location.url('app/SysMstEmployeeView?employee_gid=' + employee_gid + '&lstab=' + lstab);
        }
        $scope.pendingSummary = function () {
            $location.url('app/SysMstEmployeePendingSummary');
        }
        $scope.ActiveSummary = function () {
            $location.url('app/SysMstEmployeeActiveUserSummary');
        }
        $scope.InactiveSummary = function () {
            $location.url('app/SysMstEmployeeInactiveSummary');
        }
        
        $scope.RelieveingSummary = function () {
            $location.url('app/SysMstEmployeeRelievingSummary');
        }

        $scope.employee_add = function () {
            $location.url('app/SysMstEmployeeAdd?lstab=' + lstab);
            };

        $scope.exportemployee = function () {
            lockUI();
            var url = 'api/ManageEmployee/EmployeeExport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lspath, resp.data.lsname); 
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
                    Notify.alert('Error Occurred While Export !')

                }

            });
        }

    }
    
    
})();
