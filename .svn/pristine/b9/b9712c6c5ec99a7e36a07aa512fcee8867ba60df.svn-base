(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstGroupWaiverSummaryController', MstGroupWaiverSummaryController);

    MstGroupWaiverSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstGroupWaiverSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstGroupWaiverSummaryController';

        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();
        lockUI();
        function activate() {
            var url = 'api/MstGroupWaiver/GetGroupWaiver';
            SocketService.get(url).then(function (resp) {
                $scope.groupwaiver_list = resp.data.groupwaiver;
                unlockUI();
            });
        }

        //Add

        $scope.addgroupwaiver = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addgroupwaiver.html',
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
                    $scope.cboassignmember_list = resp.data.employeelist;
                    unlockUI();
                });

                $scope.submit = function () {
                    var params = {
                        assignmember: $scope.cboassignmember,
                        groupwaiver_code: $scope.txtgroupwaiver_code,
                        groupwaiver_name: $scope.txtgroupwaiver_name,
                        description: $scope.txtdescription
                    }
                    lockUI();
                    var url = 'api/MstGroupWaiver/PostGroupWaiver';
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
                            $modalInstance.close('closed');
                        }
                    });
                }
            }
        }

        //Edit

        $scope.editgroupwaiver = function (groupwaiver_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editgroupwaiver.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    groupwaiver_gid: groupwaiver_gid
                }
                lockUI();
                var url = 'api/MstGroupWaiver/GetGroupWaiverEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.groupwaivercodeedit = resp.data.groupwaiver_code;
                    $scope.txteditgroupwaiver_name = resp.data.groupwaiver_name;
                    $scope.txteditdescription = resp.data.description;

                    $scope.cboassignmember_editlist = resp.data.assignmember_list;
                    $scope.assignmember = resp.data.assignmember;
                    $scope.cboassignmember_edit = [];
                    if (resp.data.assignmember != null) {
                        var count = resp.data.assignmember.length;
                        for (var i = 0; i < count; i++) {
                            var indexs = $scope.cboassignmember_editlist.map(function (x) { return x.employee_gid; }).indexOf(resp.data.assignmember[i].employee_gid);
                            $scope.cboassignmember_edit.push($scope.cboassignmember_editlist[indexs]);
                            $scope.$parent.cboSecondaryValueChain = $scope.cboSecondaryValueChain;
                        }
                    }

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {
                    lockUI();
                    var url = 'api/MstGroupWaiver/UpdateGroupWaiver';
                    var params = {
                        groupwaiver_gid: groupwaiver_gid,
                        assignmember: $scope.cboassignmember_edit,
                        groupwaiver_code: $scope.groupwaivercodeedit,
                        groupwaiver_name: $scope.txteditgroupwaiver_name,
                        description: $scope.txteditdescription,
                    }
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
                            $modalInstance.close('closed');
                        }
                    });
                }
            }
        }

        // Showoverpopup

        $scope.description = function (description) {
            var modalInstance = $modal.open({
                templateUrl: '/description.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.description = description;
                $scope.back = function () {
                    $modalInstance.close('closed');
                };
            }
        }


        $scope.assignmemberpopover = function (groupwaiver_gid, groupwaiver_name) {
            var modalInstance = $modal.open({
                templateUrl: '/showassignmember.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    groupwaiver_gid: groupwaiver_gid
                }
                lockUI();
                var url = 'api/MstGroupWaiver/GetAssignMember';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.assignmember = resp.data.groupassignmembers;
                    $scope.groupwaiver_name = groupwaiver_name;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        //Status

        $scope.Status_update = function (groupwaiver_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusgroupwaiver.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    groupwaiver_gid: groupwaiver_gid
                }
                lockUI();
                var url = 'api/MstGroupWaiver/GetGroupWaiverEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.groupwaivercodeedit = resp.data.groupwaiver_code;
                    $scope.txteditgroupwaiver_name = resp.data.groupwaiver_name;
                    $scope.txteditdescription = resp.data.description;
                    $scope.rbo_status = resp.data.Status;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        groupwaiver_gid: groupwaiver_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status
                    }
                    lockUI();
                    var url = 'api/MstGroupWaiver/InactiveGroupWaiver';
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
                    groupwaiver_gid: groupwaiver_gid
                }
                lockUI();
                var url = 'api/MstGroupWaiver/InactiveGroupWaiverHistory';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.groupwaiverinactivelog_data = resp.data.groupwaiverinactivehistory_list;
                    unlockUI();
                });
            }
        }

        //Delete

        $scope.delete = function (groupwaiver_gid) {
            var params = {
                groupwaiver_gid: groupwaiver_gid
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
                    var url = 'api/MstGroupWaiver/DeleteGroupWaiver';
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
                        }
                    });
                }
            });
        }
    }
})();