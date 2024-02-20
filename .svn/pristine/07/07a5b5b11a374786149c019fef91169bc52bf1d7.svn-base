(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditOpsMappingController', MstCreditOpsMappingController);

    MstCreditOpsMappingController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCreditOpsMappingController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditOpsMappingController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            var url = 'api/MstApplication360/GetCreditOpsGroupSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.creditopsgroup_list = resp.data.creditopsgroup_list;
                unlockUI();
            });
        }


        $scope.addcreditops_group = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addcreditopsmapping.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var url = 'api/MstApplicationAdd/GetDropDown';
                SocketService.get(url).then(function (resp) {
                    $scope.vertical_list = resp.data.vertical_list;
                });

                var url = 'api/SystemMaster/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.maker_list = resp.data.employeelist;
                    unlockUI();
                });
                var url = 'api/SystemMaster/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.checker_list = resp.data.employeelist;
                    unlockUI();
                });

                $scope.submit = function () {
                    var lsvertical_gid = '';
                    var lsvertical_name = '';

                    if ($scope.cbovertical != undefined || $scope.cbovertical != null) {
                        lsvertical_gid = $scope.cbovertical.vertical_gid;
                        lsvertical_name = $scope.cbovertical.vertical_name;
                    }

                    var params = {
                        creditopsgroup_name: $scope.txtcreditops_groupname,
                        vertical_gid: lsvertical_gid,
                        vertical_name: lsvertical_name,
                        creditopsmaker: $scope.cbomaker,
                        creditopschecker: $scope.cbochecker,

                    }
                    var url = 'api/MstApplication360/PostCreditGroupAdd';
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
                    });

                    $modalInstance.close('closed');

                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            }
        }

        $scope.showPopover = function (creditopsgroupmapping_gid, creditopsgroup_name) {
            var modalInstance = $modal.open({
                templateUrl: '/showcrediopstheads.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    creditopsgroupmapping_gid: creditopsgroupmapping_gid
                }
                lockUI();
                var url = 'api/MstApplication360/GetCreditopsgroupHeads';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.creditopsmaker_name = resp.data.creditopsmaker_name;
                    $scope.creditopschecker_name = resp.data.creditopschecker_name;
                    $scope.creditopsgroup_name = creditopsgroup_name;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.edit_opscreditgroup = function (creditopsgroupmapping_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editcreditopsmapping.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var url = 'api/MstApplicationAdd/GetDropDown';
                SocketService.get(url).then(function (resp) {
                    $scope.vertical_list = resp.data.vertical_list;
                });

                lockUI();
                var params = {
                    creditopsgroupmapping_gid: creditopsgroupmapping_gid
                }
                var url = 'api/MstApplication360/GetCreditOpsGroupEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtcreditops_groupname = resp.data.creditopsgroup_name;
                    $scope.cboedit_vertical = resp.data.vertical_gid;
                    $scope.maker_editlist = resp.data.Creditmaker_list;
                    $scope.CreditOpsMakerList = resp.data.CreditOpsMakerList;
                    $scope.cboedit_maker = [];
                    if (resp.data.CreditOpsMakerList != null) {
                        var count = resp.data.CreditOpsMakerList.length;
                        for (var i = 0; i < count; i++) {
                            var indexs = $scope.maker_editlist.map(function (x) { return x.employee_gid; }).indexOf(resp.data.CreditOpsMakerList[i].employee_gid);
                            $scope.cboedit_maker.push($scope.maker_editlist[indexs]);
                            $scope.$parent.cboedit_maker = $scope.cboedit_maker;
                        }
                    }
                    $scope.checker_editlist = resp.data.Creditchecker_list;
                    $scope.CreditOpsCheckerList = resp.data.CreditOpsCheckerList;
                    $scope.cboedit_checker = [];
                    if (resp.data.CreditOpsCheckerList != null) {
                        var count = resp.data.CreditOpsCheckerList.length;
                        for (var i = 0; i < count; i++) {
                            var indexs = $scope.checker_editlist.map(function (x) { return x.employee_gid; }).indexOf(resp.data.CreditOpsCheckerList[i].employee_gid);
                            $scope.cboedit_checker.push($scope.checker_editlist[indexs]);
                            $scope.$parent.cboedit_checker = $scope.cboedit_checker;
                        }
                    }
                   
                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {
                    var vertical_Name = $('#EditVertical :selected').text();

                    lockUI();
                    var url = 'api/MstApplication360/PostCreditOpsGroupUpdate';
                    var params = {
                        creditopsgroupmapping_gid: creditopsgroupmapping_gid,
                        creditopsgroup_name: $scope.txtcreditops_groupname,
                        vertical_gid: $scope.cboedit_vertical,
                        vertical_name: vertical_Name,
                        creditopsmaker: $scope.cboedit_maker,
                        creditopschecker: $scope.cboedit_checker
                    }
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

                        }
                    });
                    $modalInstance.close('closed');

                }
            }
        }

        $scope.Status_update = function (creditopsgroupmapping_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuscreditopsgroup.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    creditopsgroupmapping_gid: creditopsgroupmapping_gid
                }
                var url = 'api/MstApplication360/GetCreditOpsGroupEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.creditopsgroupmapping_gid = resp.data.creditopsgroupmapping_gid,
                    $scope.lblcreditgroup_name = resp.data.creditopsgroup_name,
                    $scope.lblvertical_name = resp.data.vertical_name,
                    $scope.rbo_status = resp.data.creditopsgroup_status;
                });
               
                $scope.update_status = function () {
                    var params = {
                        creditopsgroupmapping_gid: creditopsgroupmapping_gid,
                        creditopsgroup_name: $scope.creditopsgroup_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status
                    }
                    var url = 'api/MstApplication360/PostCreditOpsgroupInactive';
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
                    creditopsgroupmapping_gid: creditopsgroupmapping_gid
                }

                var url = 'api/MstApplication360/GetCreditOpsgroupInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.CreditOpsStatuslog = resp.data.CreditOpsStatuslog;
                    unlockUI();
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            }
        }

    }
})();
