(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstApplicationCreationSummaryController', MstApplicationCreationSummaryController);

        MstApplicationCreationSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstApplicationCreationSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstApplicationCreationSummaryController';
        lockUI();
        activate();

        function activate() { 
            var url = 'api/MstApplicationAdd/GetApplicationNewSummary';
               SocketService.get(url).then(function (resp) {
                   unlockUI();
                   $scope.applicationadd_list = resp.data.applicationadd_list;
               });

               // For Application Gid Temp Clear

                var url = 'api/MstApplicationAdd/GetAppTempClear';
                SocketService.get(url).then(function (resp) {
                }); 

               var url = 'api/MstApplicationAdd/ApplicationCount';
               SocketService.get(url).then(function (resp) {
                   //unlockUI();
                   $scope.newapplication_count = resp.data.newapplication_count;
                   $scope.rejected_count = resp.data.rejected_count;
                   $scope.hold_count = resp.data.hold_count;
                   $scope.ccapproved_count = resp.data.ccapproved_count;
                   $scope.totalcount = resp.data.lstotalcount;
            });
        }

        $scope.addapp_creation = function () {
            $state.go('app.MstApplicationGeneralAdd');
        }

        $scope.GradingTool_add = function (val) {
            $location.url('app/MstGradingToolAdd?application_gid=' + val);          
        }

        $scope.VisitReport_add = function (val) {
            $location.url('app/MstVisitReportAdd?application_gid=' + val);          
        }

        $scope.applicationcreation_edit = function (val) {
            localStorage.setItem('application_gid', val);
            $state.go('app.MstApplicationGeneralEdit');
        }

        $scope.applcreation_view = function (application_gid) {
            $location.url('app/MstApplicationCreationView?application_gid=' + application_gid + '&lstab=applicationcreation');
        }
        
        $scope.rm_approval = function (application_gid , employee_gid) {
            $location.url('app/MstApplicationCreationRMApproval?application_gid=' + application_gid + '&employee_gid=' + employee_gid + '&lspage=MyApplications');
        }

        $scope.my_applications = function () {
            $state.go('app.MstApplicationCreationSummary');
        }
        $scope.rejected_applications = function () {
            $state.go('app.MstRejectedApplicationSummary');
        }
        $scope.hold_applications = function () {
            $state.go('app.MstHoldApplicationSummary');
        }
        $scope.approved_applications = function () {
            $state.go('app.MstApprovedApplicationSummary');
        }
        
        $scope.delete = function (application_gid) {
             var params = {
                 application_gid: application_gid
            } 
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = 'api/MstApplicationAdd/DeleteApplicationAdd';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Constitution!', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    }); 
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };
    }
})();

