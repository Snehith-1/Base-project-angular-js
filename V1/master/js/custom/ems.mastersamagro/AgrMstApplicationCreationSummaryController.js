(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstApplicationCreationSummaryController', AgrMstApplicationCreationSummaryController);

    AgrMstApplicationCreationSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrMstApplicationCreationSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstApplicationCreationSummaryController';
        lockUI();
        activate();

        function activate() { 
            var url = 'api/AgrMstApplicationAdd/GetApplicationNewSummary';
               SocketService.get(url).then(function (resp) {
                   unlockUI();
                   $scope.applicationadd_list = resp.data.applicationadd_list;
               });

               // For Application Gid Temp Clear
            // For Application Gid Temp Clear

               var url = 'api/AgrMstApplicationAdd/GetAppTempClear';
                SocketService.get(url).then(function (resp) {
                }); 

                var url = 'api/AgrMstApplicationAdd/ApplicationCount';
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
            $state.go('app.AgrMstApplicationGeneralAdd');
        }

        $scope.GradingTool_add = function (val) {
            $location.url('app/AgrGradingToolAdd?application_gid=' + val);          
        }

        $scope.VisitReport_add = function (val) {
            $location.url('app/AgrMstVisitReportAdd?application_gid=' + val);          
        }

        $scope.applicationcreation_edit = function (val) {
            localStorage.setItem('application_gid', val);
            $state.go('app.AgrMstApplicationGeneralEdit');
        }

        $scope.applcreation_view = function (application_gid) {
            $location.url('app/AgrApplicationCreationView?application_gid=' + application_gid + '&lstab=applicationcreation');
        }
        
        $scope.rm_approval = function (application_gid , employee_gid) {
            $location.url('app/AgrMstApplicationCreationRMApproval?application_gid=' + application_gid + '&employee_gid=' + employee_gid + '&lspage=MyApplications');
        }

        $scope.my_applications = function () {
            $state.go('app.AgrMstApplicationCreationSummary');
        }
        $scope.rejected_applications = function () {
            $state.go('app.AgrMstRejectedApplicationSummary');
        }
        $scope.hold_applications = function () {
            $state.go('app.AgrMstHoldApplicationSummary');
        }
        $scope.approved_applications = function () {
            $state.go('app.AgrMstApprovedApplicationSummary');
        }

        $scope.buyer_shortclosing = function (application_gid) {
            $location.url('app/AgrMstShortClosing?application_gid=' + application_gid);
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
                    var url = 'api/AgrMstApplicationAdd/DeleteApplicationAdd';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            /*activate();*/
                            applicantion_list();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Constitution!', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            /*activate();*/
                            applicantion_list();
                        }
                    }); 
                    
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };

        function applicantion_list() {
           
            var url = 'api/AgrMstApplicationAdd/GetApplicationNewSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.applicationadd_list = resp.data.applicationadd_list;
                unlockUI();
            });

            var url = 'api/AgrMstApplicationAdd/ApplicationCount';
            SocketService.get(url).then(function (resp) {

                $scope.newapplication_count = resp.data.newapplication_count;
                $scope.rejected_count = resp.data.rejected_count;
                $scope.hold_count = resp.data.hold_count;
                $scope.ccapproved_count = resp.data.ccapproved_count;
                $scope.totalcount = resp.data.lstotalcount;
            });

        }


    }
})();

