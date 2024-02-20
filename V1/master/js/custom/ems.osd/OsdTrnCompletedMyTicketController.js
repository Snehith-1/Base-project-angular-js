(function () {
    'use strict';

    angular
        .module('angle')
        .controller('OsdTrnCompletedMyTicketController', OsdTrnCompletedMyTicketController);

    OsdTrnCompletedMyTicketController.$inject = ['$rootScope', '$scope', '$sce', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout','cmnfunctionService'];

    function OsdTrnCompletedMyTicketController($rootScope, $scope, $sce, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout,cmnfunctionService) {

        /* jshint validthis:true */
        var vm = this;
        vm.title = 'OsdTrnCompletedMyTicketController';

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
        
            var url = "api/OsdTrnMyTicket/GetCompletedSummary";
            SocketService.get(url).then(function (resp) {

                $scope.completedticket_list = resp.data.completeddtl;
              
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

        $scope.completedview360 = function (val, val2, val3, val4,val7) {
            $scope.servicerequest_gid = val;
            $scope.bankalert_flag = val2;
            $scope.bankalert2allocated_gid = val3;
            $scope.customer_gid = val4;
            $scope.request_refno = val7;

            var servicerequest_gid = val;
            var bankalert_flag = val2;
            var bankalert2allocated_gid = val3;
            var customer_gid = val4;
            var request_refno = val7;


            var val5 = "Y";
            $location.url('app/osdTrnMyActivityComplete?hash=' + cmnfunctionService.encryptURL('servicerequest_gid=' + val + '&bankalert_flag=' + val2 + '&bankalert2allocated_gid=' + val3 + '&customer_gid=' + val4 + '&RequestCompletedFlag=' + val5 + '&lspage=Completed' + '&request_refno=' + val7 ));
        }

    }
})();
