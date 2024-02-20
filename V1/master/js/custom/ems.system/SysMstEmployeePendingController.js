(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstEmployeePendingController', SysMstEmployeePendingController);

        SysMstEmployeePendingController.$inject = ['$rootScope', '$modal','$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route','ngDialog', 'SweetAlert','DownloaddocumentService'];

    function SysMstEmployeePendingController($rootScope, $modal, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngDialog, SweetAlert, DownloaddocumentService) {

        activate();
        var vm = this;
        vm.title = 'SysMstEmployeePendingController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        var lstab='pending';

        function activate() {

            var url = 'api/ManageEmployee/EmployeePendingSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.employeepending_list = resp.data.employee;
                unlockUI();
            });

            $scope.today = new Date();
            var today = $scope.today;
            var checktoday = new Date();
            checktoday = ("0" + today.getDate()+1).slice(-2) + '-' + ("0" + (today.getMonth() + 1)).slice(-2) + '-' + today.getFullYear();
            $scope.checktoday = checktoday;

            var today = new Date();
            var dd = String(today.getDate()).padStart(2, '0');
            var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
            var yyyy = today.getFullYear();

            today = mm + '-' + dd + '-' + yyyy;
            $scope.checktoday = today;
        }

        $scope.employee_edit = function (employee_gid) {
            $location.url('app/SysMstEmployeeEdit?employee_gid=' + employee_gid + '&lstab=' + lstab);
        };

        $scope.assigntask = function (employee_gid) {
            $location.url('app/SysMstTaskInitiate?employee_gid=' + employee_gid + '&lstab=' + lstab);
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

        $scope.employee_deactive = function (employee_gid) {
            $location.url('app/SysMstEmployeeDeactivate?employee_gid=' + employee_gid + '&lstab=' + lstab);
        };
        /* Employee Active */
        $scope.employee_onboard = function (employee_gid) {
            $scope.employee_gid = employee_gid;
            $scope.employee_gid = localStorage.setItem('employee_gid', employee_gid);
            SweetAlert.swal({
                title: 'Are you sure ?',
                text: 'Do You Want To Activate ?',
                showCancelButton: true, 
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, Activate it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = 'api/ManageEmployee/EmployeePendingApproval';
                    $scope.employee_gid = localStorage.getItem('employee_gid');
                    var param = {
                        employee_gid: $scope.employee_gid
                    };
                    lockUI();
                    SocketService.getparams(url, param).then(function (resp) {
                        if (resp.data.status == true) {
                            SweetAlert.swal('Activated Successfully!');
                            activate();
                            $location.url('app/SysMstEmployeeActiveUserSummary');
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 4000
                            });
                            activate();
                            unlockUI();
                        }
                    });
                }
            });
        };

        //Export Excel of Employee
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
