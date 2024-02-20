(function () {
    'use strict';

    angular
        .module('angle')
        .controller('osdCqmTransferSummaryController', osdCqmTransferSummaryController);

        osdCqmTransferSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams', '$sce','cmnfunctionService'];

    function osdCqmTransferSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, $sce,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'osdCqmTransferSummaryController';
        var email_gid = $location.search().email_gid;
        activate();
        function activate() {
         
            var url = 'api/OsdTrnCustomerQueryMgmt/CustomerAssignQueryTransferSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.QueryAssignedTransferList = resp.data.QueryAssignedTransferList;
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

            var url = 'api/OsdTrnCustomerQueryMgmt/CustomerQueryAttachments';
            SocketService.getparams(url,params).then(function (resp) {
                $scope.QueryAttachmentsList = resp.data.QueryAttachmentsList;
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


        $scope.TransferView = function (email_gid) {
     
            $location.url('app/osdCqmTransferView?hash=' + cmnfunctionService.encryptURL('email_gid=' + email_gid));
        }
     
    }
})();
