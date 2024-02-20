(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstPmgApprovalcontroller', AgrMstPmgApprovalcontroller);

    AgrMstPmgApprovalcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrMstPmgApprovalcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstPmgApprovalcontroller';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;


        activate();

        function activate() {
            var url = 'api/AgrMstProductPmgApproval/GetPmgApproval';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.pmgapproval_list = resp.data.PmgApprovaldtl;
                unlockUI();

            });
        }

        $scope.addpmg = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addPMGApproval.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var url = 'api/SystemMaster/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.cboApprovalname_list = resp.data.employeelist;
                    unlockUI();
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {

                    var params = {
                        pmgapproval_gid: $scope.cboapproval_name.employee_gid,
                        pmgapproval_name: $scope.cboapproval_name.employee_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    }
                    var url = 'api/AgrMstProductPmgApproval/CreatePmgApproval';
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

            }
        }

        $scope.editpmg = function (mstpmgapproval_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editPMGApproval.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                var url = 'api/SystemMaster/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.cboApprovalname_list = resp.data.employeelist;
                    unlockUI();
                });
                var params = {
                    mstpmgapproval_gid: mstpmgapproval_gid
                }
                var url = 'api/AgrMstProductPmgApproval/EditPmgApproval';
                SocketService.getparams(url, params).then(function (resp) {

                    //$scope.cboeditapproval_name = resp.data.pmgapproval_gid;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.cboeditapproval_name = resp.data.pmgapproval_name;
                });
                $scope.update = function () {
                    //var lsproductapproval_gid = "", lsproductapproval_name = "";
                    //if ($scope.cboeditapproval_name != undefined || $scope.cboeditapproval_name != null) {
                    //    lsproductapproval_gid = $scope.cboeditapproval_name;
                    //    lsproductapproval_name = $scope.cboApprovalname_list.find(function (x) { return x.employee_gid === $scope.cboeditapproval_name}).employee_name
                    //}
                    var params = {
                        mstpmgapproval_gid: mstpmgapproval_gid,
                        //pmgapproval_gid: lsproductapproval_gid,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                    //    pmgapproval_name: lsproductapproval_name
                    }
                    var url = 'api/AgrMstProductPmgApproval/UpdatePmgApproval';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
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
                            activate();
                        }
                    });

                }

            }
        }
         

        $scope.delete = function (mstpmgapproval_gid) {
            var params = {
                mstpmgapproval_gid: mstpmgapproval_gid
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
                    lockUI();
                    var url = 'api/AgrMstProductPmgApproval/DeletePmgApproval';
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');
                            activate();
                        }
                        else {
                            alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            unlockUI;
                        }
                    });
                }
            });
        }
    }
})();
