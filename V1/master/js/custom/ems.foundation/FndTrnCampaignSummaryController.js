(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnCampaignSummaryController', FndTrnCampaignSummaryController);

    FndTrnCampaignSummaryController.$inject = ['$rootScope', '$scope', '$sce', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout', 'SweetAlert','cmnfunctionService'];

    function FndTrnCampaignSummaryController($rootScope, $scope, $sce, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout,SweetAlert,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnCampaignSummaryController';

        activate();

        function activate() {
            var url = 'api/FndTrnCampaign/GetCampaignSummary';
            SocketService.get(url).then(function (resp) {
                $scope.campaign_list = resp.data.campaign_list;
            });

            var url = 'api/FndTrnCampaign/GetCampaignCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.campaignpending_count = resp.data.campaignpending_count;
                $scope.rejected_count = resp.data.rejected_count;
                $scope.approved_count = resp.data.approved_count;
                $scope.closed_count = resp.data.closed_count;
               
            });

        }
        $scope.Create = function () {
            $state.go('app.FndTrnCampaignAdd');
        }
        $scope.Approved = function () {
            $state.go('app.FndTrnCampaignWork');
        }
        $scope.Closed = function () {
            $state.go('app.FndTrnCampaignClosed');
        }
        $scope.Reject = function () {
            $state.go('app.FndTrnCampaignReject');
        }
        $scope.Pending = function () {
            $state.go('app.FndTrnCampaignPendingSummary');
        }
        $scope.edit = function (val) {
           
            $location.url('app/FndTrnCampaignEdit?hash=' + cmnfunctionService.encryptURL('lscampaign_gid=' + val));
        }
        $scope.view = function (val) {

            $location.url('app/FndTrnCampaignView?hash=' + cmnfunctionService.encryptURL('lscampaign_gid=' + val));
        }
        $scope.delete = function (val) {
            var params = {
                campaign_gid: val
            }

            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {

                    var url = 'api/FndTrnCampaign/DeleteCampaign';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                           
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        activate();
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };
      
        $scope.Status_update = function (campaigntype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuscampaigntype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    campaigntype_gid: campaigntype_gid
                }
                var url = 'api/FndMstCampaignTypeMaster/EditCampaignType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.campaigntype_gid = resp.data.campaigntype_gid
                    $scope.txtcampaign_type = resp.data.campaigntype_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        campaigntype_name: $scope.txtcampaign_type,
                        campaigntype_gid: $scope.campaigntype_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/FndMstCampaignTypeMaster/InactiveCampaignType';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        activate();
                    });

                    $modalInstance.close('closed');

                }
                var param = {
                    campaigntype_gid: campaigntype_gid
                }

                var url = 'api/FndMstCampaignTypeMaster/CampaignTypeInactiveLogview';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.campaigntypeinactivelog_data = resp.data.campaigntype_list;
                    unlockUI();
                });
            }
        }
     

      
    }
})();
