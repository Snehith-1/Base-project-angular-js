(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndMstCampaignTypeMasterController', FndMstCampaignTypeMasterController);

    FndMstCampaignTypeMasterController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function FndMstCampaignTypeMasterController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndMstCampaignTypeMasterController';
        activate();


        function activate() {

            var url = 'api/FndMstCampaignTypeMaster/GetCampaignType';
            //var url = 'api/FndMstCampaignType/GetCampaignType';
            lockUI();
            SocketService.get(url).then(function (resp) {
                //console.log(url);
                $scope.campaigntype_data = resp.data.campaigntype_list;
                unlockUI();
            });
        }

       

        $scope.popupcampaigntype = function () {
            var modalInstance = $modal.open({
                templateUrl: '/myModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md',
                backdrop: 'static',
                keyboard: false,
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.campaigntypeSubmit = function () {
                    var params = {
                        campaigntype_gid: $scope.campaigntype_gid,
                        campaigntype_name: $scope.txtcampaign_type,
                        campaigntype_code: $scope.txtcampaigntype_code,
                        lms_code: $scope.txtlms_code,
                        remarks: $scope.txtaddremarks,
                        bureau_code: $scope.txtbureau_code
                    }

                    var url = 'api/FndMstCampaignTypeMaster/CreateCampaignType';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert('Campaign Type Added Successfully..!!', 'success')
                            activate();

                        }
                        else {
                            Notify.alert(resp.data.message, 'warning')
                            //Notify.alert('Error Occurred While Adding Campaign Type!', 'warning')

                        }
                    });
                    $modalInstance.close('closed');
                    activate();
                }

            }
        }

        $scope.editcampaigntype = function (campaigntype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editcampaigntype.html',
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
                    $scope.txteditcampaigntype_code = resp.data.campaigntype_code;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.txteditcampaign_type = resp.data.campaigntype_name;
                    $scope.txteditremarks = resp.data.remarks;
                    $scope.campaigntype_gid = resp.data.campaigntype_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.campaigntypeUpdate = function () {

                    var url = 'api/FndMstCampaignTypeMaster/UpdateCampaignType';
                    var params = {
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        campaigntype_code: $scope.txteditcampaigntype_code,
                        campaigntype_name: $scope.txteditcampaign_type,
                        remarks: $scope.txteditremarks,
                        campaigntype_gid: $scope.campaigntype_gid
                    }
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();

                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                    });
                    $modalInstance.close('closed');
                }

            }
        }






        $scope.showPopover = function (campaigntype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/showremarks.html',
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
                    $scope.txteditcampaigntype_code = resp.data.campaigntype_code;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.txteditcampaign_type = resp.data.campaigntype_name;
                    $scope.txteditremarks = resp.data.remarks;
                    $scope.campaigntype_gid = resp.data.campaigntype_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

               

            }
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

 
        $scope.deletecampaigntype = function (campaigntype_gid) {
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