(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnCampaignClosedViewController', FndTrnCampaignClosedViewController);

    FndTrnCampaignClosedViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$modal','cmnfunctionService'];

    function FndTrnCampaignClosedViewController($rootScope, $scope, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $modal,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnCampaignClosedViewController';
        $scope.campaign_gid = cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid;        
        var campaign_gid = cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid;

        activate();

        function activate() {

            var params = {
                campaign_gid: $scope.campaign_gid
            }

            var url = 'api/FndTrnCampaign/campaignDetailsView';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.campaign_code = resp.data.campaign_code;
                $scope.campaign_name = resp.data.campaign_name;
                $scope.cboCampaign_type = resp.data.campaigntype_gid;
                $scope.cboCampaign_type = resp.data.campaigntype_name;
                $scope.cboCustomer = resp.data.customer_gid;
                $scope.cboCustomer = resp.data.customer_name;
                $scope.contact_name = resp.data.contact_name;
                $scope.contact_mobile = resp.data.contact_mobile;
                $scope.contact_email = resp.data.contact_email;
                $scope.cboCampaign_apr = resp.data.campaign_approver,
                $scope.campaign_cost = resp.data.campaign_cost;
                $scope.start_date = resp.data.start_date;
                //$scope.cboCampaign_apr = resp.data.campaign_approver;
                $scope.end_date = resp.data.end_date;
                $scope.assesment_date = resp.data.assesment_date;
                $scope.os_assesment_date = resp.data.os_assesment_date;
                $scope.loan_availed = resp.data.loan_availed;
                $scope.cboCampaign_mgr = resp.data.employee_gid,
                 $scope.cboCampaign_mgr = resp.data.manager_name,


                unlockUI();
            });

            var url = 'api/FndTrnCampaign/GetCampaignRaiseQuery';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.campiagnraisequery_list = resp.data.campiagnraisequery_list;
            });

            var params = {
                campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid
            }
            var url = 'api/FndTrnCampaign/GetSingleformEdit';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.singleform_list = resp.data.singleform_list;

            });


            var params = {
                campaign_gid: cmnfunctionService.decryptURL($location.search().hash).lscampaign_gid
            }
            var url = 'api/FndTrnCampaign/GetMultipleformEdit';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.multipleform_list = resp.data.multipleform_list;

            });

        }

        $scope.view_querydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/queryDescriptionView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.lblquery_desc = query_description;
                $scope.lblqueryresponse_remarks = queryresponse_remarks;
                $scope.lblquery_responseby = query_responseby;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.Back = function () {
            $state.go('app.FndTrnCampaignClosed');
        }
    }

})();
