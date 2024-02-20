(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmTrnReplyToQueryController', AtmTrnReplyToQueryController);

    AtmTrnReplyToQueryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams', '$sce'];

    function AtmTrnReplyToQueryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmTrnReplyToQueryController';
        $scope.auditcreation_gid = $location.search().auditcreation_gid;
        var auditcreation_gid = $scope.auditcreation_gid;
        activate();
        function activate() {

            var params = {
                auditcreation_gid: auditcreation_gid,

            };

            var url = 'api/AtmTrnMyAuditTask/GetAssignedQuerySummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.AssignedQueryList = resp.data.AssignedQueryList;
                unlockUI();
            });

            var url = 'api/AtmTrnMyAuditTask/GetRepliedQuerySummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.ReplyToQueryList = resp.data.ReplyToQueryList;
                unlockUI();
            });

            
            //var url = "api//";
            //SocketService.get(url).then(function (resp) {
            //    $scope.assigned_count = resp.data.assigned_count;
            //    $scope.reply_count = resp.data.reply_count;
            //    $scope.close_count = resp.data.close_count;
            //    unlockUI();
            //});


        }     
        $scope.Assigned = function () {
            var auditcreation_gid = $scope.auditcreation_gid;
            $location.url('app/AtmTrnAssignedQuery?auditcreation_gid=' + auditcreation_gid)

        }

  
        $scope.Reply = function () {
            $state.go('app.AtmTrnReplyToQuery');
        }

        $scope.back = function (val) {
            $state.go('app.AtmTrnMyAuditTaskSummary');
        }

    }
})();
