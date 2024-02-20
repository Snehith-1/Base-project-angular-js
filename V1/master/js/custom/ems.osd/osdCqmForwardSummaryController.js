(function () {
    'use strict';

    angular
        .module('angle')
        .controller('osdCqmForwardSummaryController', osdCqmForwardSummaryController);

        osdCqmForwardSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams', '$sce','cmnfunctionService'];

    function osdCqmForwardSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, $sce,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'osdCqmForwardSummaryController';

        activate();
        function activate() {
            var email_gid = localStorage.getItem('email_gid');
            $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };
            var url = 'api/OsdTrnCustomerQueryMgmt/CustomerAssignQueryForwardSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.QueryAssignedForwardList = resp.data.QueryAssignedForwardList;
                unlockUI();
            });

            var  params={
                email_gid: email_gid
            }
            var url = 'api/OsdTrnCustomerQueryMgmt/CustomerAssignedQuery360';
            SocketService.getparams(url,params).then(function (resp) {
                $scope.email_gid = resp.data.email_gid;
                $scope.ticketref_no = resp.data.ticketref_no;
                $scope.assigned_by = resp.data.assigned_by;
                $scope.assigned_date = resp.data.assigned_date;
                $scope.assigned_to = resp.data.assigned_to;
                $scope.email_from = resp.data.email_from;
                $scope.email_date = resp.data.email_date;
                $scope.status = resp.data.status;
                $scope.aging = resp.data.aging;
                $scope.email_subject = resp.data.email_subject;
                $scope.email_content = resp.data.email_content;
                $scope.email_to = resp.data.email_to;
                $scope.cc = resp.data.cc;
                $scope.bcc = resp.data.bcc;
                $scope.from_mailaddress = resp.data.from_mailaddress;
                $scope.Query360list= resp.data.Query360list;
                $scope.assigned_remarks= resp.data.assigned_remarks;
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

        $scope.ForwardView = function (email_gid) {
            $scope.email_gid = localStorage.setItem('email_gid', email_gid);
            $location.url('app/osdCqmForwardView');
           
        }
        $scope.transfer = function (email_gid, ticketref_no, from_mailaddress, email_subject, assigned_to) {

            var modalInstance = $modal.open({
                templateUrl: '/transferContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
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

        $scope.forward = function (email_gid) {
            
            var decision = "Forward";
            var lspage = "forward";
            var llspage = "forward";
            $location.url('app/osdComposeMail?hash=' + cmnfunctionService.encryptURL('email_gid=' + email_gid + '&toMail=' + $scope.from_mailaddress + '&ccMail=' + $scope.cc + '&bccMail=' + $scope.bcc + '&email_subject=' + $scope.email_subject + '&message_id=' + $scope.message_id + '&reference_id= ' + $scope.reference_id + '&rmemployee_gid=' + $scope.rmemployee_gid + '&rmemployee_name=' + $scope.rmemployee_name + '&decision=' + decision + '&lspage=' + lspage + '&llspage=' + llspage));
        }
    }
})();
