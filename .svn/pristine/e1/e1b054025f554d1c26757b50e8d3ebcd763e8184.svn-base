(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstColendingProgramsController', MstColendingProgramsController);

    MstColendingProgramsController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstColendingProgramsController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstColendingProgramsController';

        activate();

        function activate() {
            var url = 'api/MstApplication360/GetColendingProgram';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.colending_list = resp.data.colending_list;
                //$scope.colending_list = resp.data.colending_list;
                unlockUI();
            });
        }

        $scope.editRule = function (colendingprogram_gid) {
            $location.url('app/MstColendingRuleadd?lscolendingprogram_gid=' + colendingprogram_gid);
        }

        $scope.addcolending = function () {
            $state.go('app.MstColendingProgramAdd');
        }
        $scope.editcolending = function (colendingprogram_gid) {
            //$state.go('app.MstColendingProgramEdit?lscolendingprogram_gid=' + colendingprogram_gid);
            $location.url('app/MstColendingProgramEdit?lscolendingprogram_gid=' + colendingprogram_gid);
        }
        $scope.viewcolending = function (colendingprogram_gid) {
            $location.url('app/MstColendingProgramView?lscolendingprogram_gid=' + colendingprogram_gid);
            //$state.go('app.MstColendingProgramView');
        }
        //$scope.addprogram = function (colendingprogram_gid) {
        //    $location.url('app/MstColendingProgramAdd?lscolendingprogram_gid=' + colendingprogram_gid);
        //}
        //$scope.addprogram = function (program_gid) {
        //    $location.url('app/MstProgramAdd?lsprogram_gid=' + program_gid);
        //}
        $scope.Status_update = function (colendingprogram_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuscolending.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    colendingprogram_gid: colendingprogram_gid
                }
                lockUI();
                var url = 'api/MstApplication360/EditColendingProgram';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtcolendar_name = resp.data.colendar_name;
                    $scope.rbo_status = resp.data.Status;
                    unlockUI();
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        colendingprogram_gid: colendingprogram_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    lockUI();
                    var url = 'api/MstApplication360/InactiveColendingProgram';
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
                        } activate();
                    });

                    $modalInstance.close('closed');

                }
                var params = {
                    colendingprogram_gid: colendingprogram_gid
                }

                var url = 'api/MstApplication360/ColendingProgInactiveLogview';

                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.colendinginactivelog_list = resp.data.application_list;
                    unlockUI();
                });
            }
        }

        //Summary Delete

        $scope.delete = function (colendingprogram_gid) {
            var params = {
                colendingprogram_gid: colendingprogram_gid
            }
            var url = 'api/MstApplication360/DeleteColendingProgram';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    SweetAlert.swal({
                        title: 'Are you sure?',
                        text: 'Do You Want To Delete the Record ?',
                        showCancelButton: true,
                        confirmButtonColor: '#DD6B55',
                        confirmButtonText: 'Yes, delete it!',
                        closeOnConfirm: false
                    }, function (isConfirm) {
                        if (isConfirm) {
                            SweetAlert.swal('Deleted Successfully!');
                            unlockUI();
                            activate();
                        }

                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    activate();
                }
            });
        };
       
    }
})();
