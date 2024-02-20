(function () {
    'use strict';

    angular
        .module('angle')
        .controller('OsdTrnApprovalMyTicketController', OsdTrnApprovalMyTicketController);

    OsdTrnApprovalMyTicketController.$inject = ['$rootScope', '$scope', '$sce', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout','cmnfunctionService'];

    function OsdTrnApprovalMyTicketController($rootScope, $scope, $sce, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout,cmnfunctionService) {

        /* jshint validthis:true */
        var vm = this;
        vm.title = 'OsdTrnApprovalMyTicketController';

        activate();

        function activate() {
            lockUI();
            var url = "api/OsdTrnMyTicket/GetActivityCount";
            SocketService.get(url).then(function (resp) {
                $scope.alloted_count = resp.data.lsallotedcount;
                $scope.workinprogress_count = resp.data.lsworkinprogress_count;
                $scope.completed_count = resp.data.completed_count;
                $scope.closed_count = resp.data.closed_count;
                $scope.transfer_count = resp.data.transfer_count;
                $scope.forward_count = resp.data.lsforward_count;
                $scope.approvalpending_count = resp.data.approvalpending_count;
                unlockUI();
            });
            lockUI();
            var url = "api/OsdTrnMyTicket/GetApprovalPendingSummary";

            SocketService.get(url).then(function (resp) {
                $scope.allotedactivity_list = resp.data.allotteddtl;
               
                unlockUI();
            });
        }

        $scope.Refresh = function () {
            activate();
        }

        $scope.Allotted = function () {
            $state.go('app.OsdTrnAllotedMyTicket');
        }
        $scope.Work = function () {
            $state.go('app.OsdTrnWorkMyTicket');
        }
        $scope.Approval = function () {
            $state.go('app.OsdTrnApprovalMyTicket');
        }
        $scope.External = function () {
            $state.go('app.OsdTrnExternalMyTicket');
        }
        $scope.Internal = function () {
            $state.go('app.OsdTrnInternalMyTicket');
        }
        $scope.Completed = function () {
            $state.go('app.OsdTrnCompletedMyTicket');
        }
        $scope.Closed = function () {
            $state.go('app.OsdTrnClosedMyTicket');
        }

        $scope.view360 = function (val, val2, val3, val4,val6,val7) {
            $scope.servicerequest_gid = val;
            $scope.bankalert_flag = val2;
            $scope.bankalert2allocated_gid = val3;
            $scope.customer_gid = val4;
            $scope.request_refno = val7;
            $scope.customer_urn = val6;

            var servicerequest_gid = val;
            var bankalert_flag = val2;
            var bankalert2allocated_gid = val3;
            var customer_gid = val4;
            var request_refno = val7;


            var param = {
                servicerequest_gid: servicerequest_gid,
                bankalert_flag: bankalert_flag,
                bankalert2allocated_gid: bankalert2allocated_gid,
                customer_gid: customer_gid
            }
            var url = 'api/OsdTrnMyTicket/GetServiceRequestForwardView360Update';
            lockUI()
            SocketService.getparams(url, param).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI()
                }
                else {
                    unlockUI();
                }
            });

            var val5 = "N";
            $location.url('app/osdTrnApprovalPending360?hash=' + cmnfunctionService.encryptURL('servicerequest_gid=' + val + '&bankalert_flag=' + val2 + '&bankalert2allocated_gid=' + val3 + '&customer_gid=' + val4 + '&RequestCompletedFlag=' + val5 + '&lspage=Alloted' + '&request_refno='+ val7));
        }

    }
})();
