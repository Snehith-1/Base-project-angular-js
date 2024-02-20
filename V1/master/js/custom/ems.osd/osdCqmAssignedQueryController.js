(function () {
    'use strict';

    angular
        .module('angle')
        .controller('osdCqmAssignedQueryController', osdCqmAssignedQueryController);

        osdCqmAssignedQueryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams','$sce','cmnfunctionService'];

    function osdCqmAssignedQueryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams,$sce,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'osdCqmAssignedQueryController';

        activate();
        function activate() {
            var url = 'api/OsdTrnCustomerQueryMgmt/CustomerAssignedQuerySummary';
            SocketService.get(url).then(function (resp) {
                $scope.AssignedQueryList = resp.data.AssignedQueryList;
                unlockUI();
            });

            var url = "api/OsdTrnCustomerQueryMgmt/AssignedQueryCount";
            SocketService.get(url).then(function (resp) {
                $scope.assigned_count = resp.data.assigned_count;
                $scope.reply_count = resp.data.reply_count;
                $scope.forward_count = resp.data.forward_count;
                $scope.transfer_count = resp.data.transfer_count;
                $scope.close_count = resp.data.close_count;
                unlockUI();
            });
            

        }
        $scope.Assigned = function () {
            $state.go('app.osdCqmAssignedQuery');
        }

        $scope.Closed = function () {
            $state.go('app.osdCqmAssignCloseSummary');
        }

        $scope.Transfer = function () {
            $state.go('app.osdCqmTransferSummary');
        }

        $scope.Replay = function () {
            $state.go('app.osdCqmReplaySummary');
        }

        $scope.Forward = function () {
            $state.go('app.osdCqmForwardSummary');
        }

        $scope.Replay = function () {
            $state.go('app.osdCqmReplaySummary');
        }

        $scope.AssignedQuery360 = function (email_gid) {

          
            $location.url('app/osdCqmAssignedQuery360?hash=' + cmnfunctionService.encryptURL('email_gid=' + email_gid ));
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
                var url = 'api/OsdTrnCustomerQueryMgmt/TransferLog';

                    var params = {
                        lsemail_gid:email_gid
                    }
    
                    SocketService.getparams(url, params).then(function (resp) {
                            $scope.MdlTransferLog = resp.data.MdlTransferLog;
                    });
                $scope.close = function () {
                    modalInstance.close('closed');
                };
                //var url = "api/OsdTrnCustomerQueryMgmt/TransferLog";
                //lockUI();
                //SocketService.post(url, params).then(function (resp) {
                //    $scope.MdlTransferLog = resp.data.MdlTransferLog;
                //});
            }
        }

        
    }
})();
