(function () {
    'use strict';

    angular
        .module('angle')
        .controller('osdCqmAssignToQueryController', osdCqmAssignToQueryController);

        osdCqmAssignToQueryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams','$sce','cmnfunctionService'];

    function osdCqmAssignToQueryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams,$sce,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'osdCqmAssignToQueryController';
        var email_gid = $location.search().email_gid;
        activate();
        function activate() {
            
            var url = 'api/OsdTrnCustomerQueryMgmt/CustomerQueryAssignSummary';
            lockUI(); 
            SocketService.get(url).then(function (resp) {
                $scope.QueryAssignList = resp.data.QueryAssignList;
                unlockUI();
            });

            var url = "api/OsdTrnCustomerQueryMgmt/QueryAssignmentCount";
            SocketService.get(url).then(function (resp) {
                $scope.pending_count = resp.data.pending_count;
                $scope.assign_count = resp.data.assign_count;
                $scope.close_count = resp.data.close_count;
                unlockUI();
            });
        }

        $scope.AssignedQuery360 = function(email_gid,status) {
            
            $location.url('app/osdCqmQueryAssign360?hash=' + cmnfunctionService.encryptURL('email_gid=' + email_gid +'&status=' + status ));
        }
        $scope.Pending = function () {
            $state.go('app.osdCqmQueryAssignment');
        }

        $scope.Assigned = function (){
            $state.go('app.osdCqmAssignToQuery');
        }
        
        $scope.Close = function (){
            $state.go('app.osdCqmCloseSummary');
        }

        $scope.transfer = function (email_gid, ticketref_no, from_mailaddress, email_subject, assigned_to) {

            var modalInstance = $modal.open({
                templateUrl: '/transferContent.html',
                controller: ModalInstanceCtrl,
                size: 'md',
                backdrop: 'static',
                keyboard: false
            });

            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                var url = 'api/OsdTrnCustomerQueryMgmt/TransferEmployee';
                SocketService.get(url).then(function (resp) {
                    $scope.TransferEmployeeList = resp.data.TransferEmployeeList;
                    unlockUI();
                });

                $scope.email_gid = email_gid;
                $scope.ticketref_no = ticketref_no;
                $scope.email_subject = email_subject;
                $scope.from_mailaddress = from_mailaddress;
                $scope.assigned_to = assigned_to;

                $scope.transferWIUpdate = function () {

                    if ($scope.transfer_to == undefined) {
                        modalInstance.close('closed');
                        Notify.alert('Kindly Select the Assign to Person', 'warning');
                        return;
                    }

                    var params = {
                        email_gid: $scope.email_gid,
                        employee_gid: $scope.transfer_to,
                        employee_name: $('#transfer_to :selected').text(),
                        transfer_remarks: $scope.transfer_remarks
                    }

                    var url = "api/OsdTrnCustomerQueryMgmt/TicketTransfer";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success');
                            activate();
                        }
                        else {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'warning');
                            activate();
                        }
                    });

                }

                var params = {
                    lsemail_gid:email_gid
                }

                var url = 'api/OsdTrnCustomerQueryMgmt/TransferLog';
              
                SocketService.getparams(url, params).then(function (resp) {
                        $scope.MdlTransferLog = resp.data.MdlTransferLog;
                });

                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
            
        }

    }
})();
