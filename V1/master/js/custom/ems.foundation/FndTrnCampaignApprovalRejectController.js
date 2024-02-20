(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnCampaignApprovalRejectController', FndTrnCampaignApprovalRejectController);

    FndTrnCampaignApprovalRejectController.$inject = ['$rootScope', '$scope', '$sce', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout','cmnfunctionService'];

    function FndTrnCampaignApprovalRejectController($rootScope, $scope, $sce, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnCampaignApprovalRejectController';

        activate();
       
        function activate() {
            var url = 'api/FndTrnCampaign/GetCampaignRejected';
            SocketService.get(url).then(function (resp) {
                $scope.campaign_list = resp.data.campaign_list;
            });

            var url = 'api/FndTrnCampaign/GetCampaignApprovalCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.campaignapprovalpending_count = resp.data.campaignapprovalpending_count;
                $scope.approvalrejected_count = resp.data.approvalrejected_count;
                $scope.approvalapproved_count = resp.data.approvalapproved_count;

            });

        }
        $scope.pendingcampaignapproval = function () {
            $state.go('app.FndTrnCampaignApproval');
        }
        $scope.Reject = function () {
            $state.go('app.FndTrnCampaignApprovalReject');
        }

        $scope.Approved = function () {
            $state.go('app.FndTrnCampaignApprovalWork');
        }
        $scope.viewrejected = function (val) {

            $location.url('app/FndTrnCampaignApprovalRejectedView?hash=' + cmnfunctionService.encryptURL('lscampaign_gid=' + val));
        }

    }
})();
