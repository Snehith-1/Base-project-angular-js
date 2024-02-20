(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstBusinessHierarchyUpdateHistoryController', AgrMstBusinessHierarchyUpdateHistoryController);

    AgrMstBusinessHierarchyUpdateHistoryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService','cmnfunctionService'];

    function AgrMstBusinessHierarchyUpdateHistoryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstBusinessHierarchyUpdateHistoryController';
        //$scope.application_gid = $location.search().application_gid;
        //var application_gid = $scope.application_gid;
        //$scope.lspage = $location.search().lspage;
        //var lspage = $scope.lspage;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.application_gid = searchObject.application_gid;
        var application_gid = $scope.application_gid;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.employee_gid = searchObject.employee_gid;
        var employee_gid = $scope.employee_gid;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.lspage = searchObject.lspage;
        var lspage = $scope.lspage;
        lockUI();
        activate();
        function activate() {
            var params = {
                application_gid: application_gid
            }
            var url = "api/AgrAdminApplication/GetHierarchyUpdateHistoryLog"
            SocketService.getparams(url, params).then(function (resp) {
                $scope.hierarchyupdatedhistory_list = resp.data.hierarchyupdatedhistory_list;
                angular.forEach($scope.hierarchyupdatedhistory_list, function (value, key) {
                    if (value.vertical_gid == "" || value.vertical_gid == null) {
                        value.showvertical_name = false;
                    }
                    else {
                        value.showvertical_name = true;
                    }
                    if (value.program_gid == "" || value.program_gid == null) {
                        value.showprogram_name = false;
                    }
                    else {
                        value.showprogram_name = true;
                    }
                    if (value.relationshipmanager_gid == "" || value.relationshipmanager_gid == null) {
                        value.showrelationshipmanager_name = false;
                    }
                    else {
                        value.showrelationshipmanager_name = true;
                    }
                    if (value.drm_gid == "" || value.drm_gid == null) {
                        value.showdrm_name = false;
                    }
                    else {
                        value.showdrm_name = true;
                    }
                    if (value.clustermanager_gid == "" || value.clustermanager_gid == null) {
                        value.showclustermanager_name = false;
                    }
                    else {
                        value.showclustermanager_name = true;
                    }
                    if (value.zonalhead_gid == "" || value.zonalhead_gid == null) {
                        value.showzonalhead_name = false;
                    }
                    else {
                        value.showzonalhead_name = true;
                    }
                    if (value.regionalhead_gid == "" || value.regionalhead_gid == null) {
                        value.showregionalhead_name = false;
                    }
                    else {
                        value.showregionalhead_name = true;
                    }
                    if (value.businesshead_gid == "" || value.businesshead_gid == null) {
                        value.showbusinesshead_name = false;
                    }
                    else {
                        value.showbusinesshead_name = true;
                    }
                    if (value.updatedvertical_gid == "" || value.updatedvertical_gid == null) {
                        value.showupdatedvertical_name = false;
                    }
                    else {
                        value.showupdatedvertical_name = true;
                    }
                    if (value.updatedprogram_gid == "" || value.updatedprogram_gid == null) {
                        value.showupdatedprogram_name = false;
                    }
                    else {
                        value.showupdatedprogram_name = true;
                    }
                    if (value.updatedregionalhead_gid == "" || value.updatedregionalhead_gid == null) {
                        value.showupdatedregionalhead_name = false;
                    }
                    else {
                        value.showupdatedregionalhead_name = true;
                    }
                    if (value.updateddrm_gid == "" || value.updateddrm_gid == null) {
                        value.showupdateddrm_name = false;
                    }
                    else {
                        value.showupdateddrm_name = true;
                    }
                    if (value.updatedclustermanager_gid == "" || value.updatedclustermanager_gid == null) {
                        value.showupdatedclustermanager_name = false;
                    }
                    else {
                        value.showupdatedclustermanager_name = true;
                    }
                    if (value.updatedregionalhead_gid == "" || value.updatedregionalhead_gid == null) {
                        value.showupdatedregionalhead_name = false;
                    }
                    else {
                        value.showupdatedregionalhead_name = true;
                    }
                    if (value.updatedzonalhead_gid == "" || value.updatedzonalhead_gid == null) {
                        value.showupdatedzonalhead_name = false;
                    }
                    else {
                        value.showupdatedzonalhead_name = true;
                    }
                    if (value.updatedbusinesshead_gid == "" || value.updatedbusinesshead_gid == null) {
                        value.showupdatedbusinesshead_name = false;
                    }
                    else {
                        value.showupdatedbusinesshead_name = true;
                    }
                });

                unlockUI();
            });
        }

        $scope.Back = function () {
            if (lspage == 'BusinessStage') {
                $state.go('app.AgrMstBusinessHierarchyUpdateSummary');
            }
            else if (lspage == 'CreditStage') {
                $state.go('app.AgrMstCreditStageSummary');
            }
            else if (lspage == 'CcStage') {
                $state.go('app.AgrMstCcStageSummary');
            }
            else if (lspage == 'CadPendingStage') {
                $state.go('app.AgrMstCadPendingStageSummary');
            }
            else if (lspage == 'CadAcceptedStage') {
                $state.go('app.AgrMstCadAcceptedStageSummary');
            }
            else if (lspage == 'IncompleteStage') {
                $state.go('app.AgrIncompleteStageSummary');
            }
            else if (lspage == 'ProductStage') {
                $state.go('app.AgrProductStageSummary');
            }
            else {

            }
        }

        $scope.hierarchy_remarks = function (businesshierarchyupdatelog_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/HierarchyUpdateRemarksView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                {
                    businesshierarchyupdatelog_gid: businesshierarchyupdatelog_gid
                }
                var url = 'api/AgrAdminApplication/GetHierarchyUpdateRemarks';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {                   
                    $scope.businesshierarchyupdatelog_gid = resp.data.businesshierarchyupdatelog_gid;
                    $scope.lblhierarchyupdate_remarks = resp.data.hierarchyupdate_remarks;
                    unlockUI();
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

    }
})();
