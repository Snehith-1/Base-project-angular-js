(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCADReassignApplicationController', MstCADReassignApplicationController);

    MstCADReassignApplicationController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCADReassignApplicationController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCADReassignApplicationController';
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        activate();

        function activate() {

            var params = {
                     application_gid: application_gid
            }
            var url = 'api/MstCAD/GetAssignmentView';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.assignment_list = resp.data.assignment_list;
            });

        }

        $scope.Back = function (val) {
            $location.url('app/MstCadAcceptedCustomers');
        }

        $scope.reassign_application = function (application_gid, cadgroup_gid, menu_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/ReassignApplication.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    cadgroup_gid: cadgroup_gid
                }
                var url = 'api/MstCAD/GetCADMembers';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.cadmembers_list = resp.data.cadmembers;
                    unlockUI();
                });

                var params =
                  {
                      application_gid: application_gid,
                      menu_gid: menu_gid
                  }
                var url = 'api/MstCAD/GetCADGroupDtl';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.application_gid = resp.data.application_gid;
                    $scope.cadgroup_gid = resp.data.cadgroup_gid;
                    $scope.txtcadgroup_name = resp.data.cadgroup_name;
                    $scope.menu_gid = resp.data.menu_gid;
                    $scope.txtmenu_name = resp.data.menu_name;
                    $scope.cbomaker_name = resp.data.maker_gid;
                    $scope.maker_name = resp.data.maker_name;
                    $scope.cbochecker_name = resp.data.checker_gid;
                    $scope.checker_name = resp.data.checker_name;
                    $scope.cboapprover_name = resp.data.approver_gid;
                    $scope.approver_name = resp.data.approver_name;
                });

                var params =
                  {
                      application_gid: application_gid,
                      menu_gid: menu_gid
                  }
                var url = 'api/MstCAD/GetReassignApplicationView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.Reassignedlog_list = resp.data.Reassignedlog_list;
                });

                $scope.reassign = function () {

                    var MakerName = $('#maker_name :selected').text();
                    var CheckerName = $('#checker_name :selected').text();
                    var ApproverName = $('#approver_name :selected').text();

                    var params = {
                        application_gid: $scope.application_gid,
                        cadgroup_gid: $scope.cadgroup_gid,
                        cadgroup_name: $scope.txtcadgroup_name,
                        menu_gid: menu_gid,
                        menu_name: $scope.txtmenu_name,
                        maker_gid: $scope.cbomaker_name,
                        maker_name: MakerName,
                        checker_gid: $scope.cbochecker_name,
                        checker_name: CheckerName,
                        approver_gid: $scope.cboapprover_name,
                        approver_name: ApproverName

                    }
                    var url = 'api/MstCAD/PostReassignCADApplication';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }                       
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
