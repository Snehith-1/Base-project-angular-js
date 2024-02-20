(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnCampaignApprovalController', FndTrnCampaignApprovalController);

    FndTrnCampaignApprovalController.$inject = ['$rootScope', '$scope', '$sce', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout','cmnfunctionService'];

    function FndTrnCampaignApprovalController($rootScope, $scope, $sce, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnCampaignApprovalController';

        activate();

        function activate() {
            var url = 'api/FndTrnCampaign/GetCampaignApprovalpending';
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
                                status: 'warning',
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
      


        $scope.edit = function (val) {

            $location.url('app/FndTrnCampaignApprovalEdit?hash=' + cmnfunctionService.encryptURL('lscampaign_gid=' + val));
        }
        $scope.viewcustomer = function (val) {

            $location.url('app/FndTrnCampaignApprovalView?hash=' + cmnfunctionService.encryptURL('lscampaign_gid=' + val));
        }


        $scope.deletecampaign = function (campaign_gid) {
            var params = {
                campaigntype_gid: campaigntype_gid
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
                    var url = 'api/FndMstCampaignTypeMaster/DeleteCampaignType';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Campaign Type !!!', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000

                            });
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };
    }
})();
