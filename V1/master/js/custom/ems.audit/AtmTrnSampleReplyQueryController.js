(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmTrnSampleReplyQueryController', AtmTrnSampleReplyQueryController);

    AtmTrnSampleReplyQueryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams', '$sce'];

    function AtmTrnSampleReplyQueryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmTrnSampleReplyQueryController';
        $scope.auditcreation_gid = $location.search().auditcreation_gid;
        var auditcreation_gid = $scope.auditcreation_gid;
        $scope.sampleimport_gid = $location.search().sampleimport_gid;
        var sampleimport_gid = $scope.sampleimport_gid;
        activate();
        function activate() {

            var params = {
                auditcreation_gid: auditcreation_gid,
                sampleimport_gid: sampleimport_gid
            }

            var url = 'api/AtmTrnSampling/GetRepliedQuerySummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.SampleReplyQueryList = resp.data.SampleReplyQueryList;
                unlockUI();
            });

            //var url = 'api/AtmTrnSampling/GetAssignedQuerySummary';
            //SocketService.getparams(url, params).then(function (resp) {
            //    $scope.SampleAssignedQueryList = resp.data.SampleAssignedQueryList;
            //    unlockUI();
            //});




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
            var sampleimport_gid = $scope.sampleimport_gid;
            $location.url('app/AtmTrnSampleAssignedQuery?auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + sampleimport_gid);
        }


        $scope.back = function () {
            var auditcreation_gid = $scope.auditcreation_gid;

            $location.url('app/AtmTrnCheckpointObservation?auditcreation_gid=' + auditcreation_gid)

        }



    }
})();
