(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstShortClosingController', AgrMstShortClosingController);

    AgrMstShortClosingController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function AgrMstShortClosingController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstShortClosingController';       
        $scope.application_gid = $location.search().application_gid;

        lockUI();
        activate();       
        function activate() {          
            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstApplicationView/GetApplicationBasicView';
            SocketService.getparams(url, params).then(function (resp) {               
                $scope.lblapplication_no = resp.data.application_no;
                $scope.lblbasiccustomer_name = resp.data.customer_name;
                $scope.lblshortclosing_reason = resp.data.shortclosing_reason;
                unlockUI();
            });

            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstApplicationEdit/EditProceed';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.proceed_flag = resp.data.proceed_flag;
                $scope.level_zero = resp.data.level_zero;
                $scope.level_one = resp.data.level_one;
                $scope.clusterhead = resp.data.cluster_head;
                $scope.zonalhead = resp.data.zonal_head;
                $scope.regionhead = resp.data.regional_head;
                $scope.businesshead = resp.data.business_head;
                $scope.approveinitiated_flag = resp.data.approveinitiated_flag;
                unlockUI();

                var proceed_flag = $scope.proceed_flag;
                var approveinitiated_flag = $scope.approveinitiated_flag;
                var params = {
                    application_gid: $scope.application_gid
                }
                var url = 'api/AgrMstApplicationAdd/GetApprovalHierarchyFlag';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lshierarchychange_flag = resp.data.lshierarchychange_flag;
                    $scope.hierarchyupdated_flag = resp.data.hierarchyupdated_flag;
                    if ($scope.hierarchyupdated_flag == 'N' && $scope.approveinitiated_flag == 'N' && proceed_flag == 'Y') {
                        $scope.hierarchyshow = true;
                        $scope.done_disable = true;
                        $scope.done_enable = false;
                        $scope.resubmitshow = false;
                    }
                    else if ($scope.proceed_flag == 'Y' && $scope.approveinitiated_flag == 'N' && $scope.hierarchyupdated_flag == 'Y') {
                        $scope.hierarchyshow = false;
                        $scope.resubmitshow = false;
                        $scope.done_enable = true;
                        $scope.done_disable = false;
                    }
                    else if (proceed_flag == 'Y' && approveinitiated_flag == 'Y') {
                        $scope.hierarchyshow = false;
                        $scope.resubmitshow = true;
                        $scope.done_enable = false;
                        $scope.done_disable = false;
                    }
                    else if (proceed_flag == 'N' && approveinitiated_flag == 'N') {
                        $scope.hierarchyshow = false;
                        $scope.resubmitshow = false;
                        $scope.done_disable = true;
                        $scope.resubmitshow = false;
                    }
                    else {

                    }
                });    
            });

              
                       
        }

        $scope.overallsubmit_application = function () {
            lockUI();
            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstApplicationEdit/EditAppProceed';
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    activate();
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
                $state.go('app.AgrMstApplicationCreationSummary');
            });

        }       

        $scope.Back = function () {
            $location.url('app/AgrMstApplicationCreationSummary');
        }  

        $scope.hierarchy_change = function () {
            var modalInstance = $modal.open({
                templateUrl: '/HierarchyChange.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });  
            var application_gid = $scope.application_gid;
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {               
                var params = {
                    application_gid: application_gid
                }
                var url = 'api/AgrMstApplicationAdd/GetApprovalHierarchyChangeList';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.rm_name = resp.data.rm_name;
                    $scope.directreportingto_name = resp.data.directreportingto_name;
                    $scope.clustermanager_gid = resp.data.clustermanager_gid;
                    $scope.clustermanager_name = resp.data.clustermanager_name;
                    $scope.regionalhead_gid = resp.data.regionalhead_gid;
                    $scope.regionhead_name = resp.data.regionhead_name;
                    $scope.zonalhead_gid = resp.data.zonalhead_gid;
                    $scope.zonalhead_name = resp.data.zonalhead_name;
                    $scope.businesshead_gid = resp.data.businesshead_gid;
                    $scope.businesshead_name = resp.data.businesshead_name;
                });

                $scope.Update_hierarchy = function () {
                    var params = {
                        application_gid: application_gid,
                        clustermanager_gid: $scope.clustermanager_gid,
                        clustermanager_name: $scope.clustermanager_name,
                        regionalhead_gid: $scope.regionalhead_gid,
                        regionalhead_name: $scope.regionhead_name,
                        zonalhead_gid: $scope.zonalhead_gid,
                        zonalhead_name: $scope.zonalhead_name,
                        businesshead_gid: $scope.businesshead_gid,
                        businesshead_name: $scope.businesshead_name
                    }
                    var url = 'api/AgrMstApplicationAdd/UpdateApprovalHierarchyChange';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        $modalInstance.close('closed');
                    });
                    $modalInstance.close('closed');
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            }
        }

    }
})();
