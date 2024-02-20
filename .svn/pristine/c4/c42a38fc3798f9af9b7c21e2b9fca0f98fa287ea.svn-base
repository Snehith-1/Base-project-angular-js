(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCADGroupSummaryController', MstCADGroupSummaryController);

    MstCADGroupSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCADGroupSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCADGroupSummaryController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        
        activate();
        lockUI();
        function activate() {
            var url = 'api/MstCADGroup/GetCADGroup';
            SocketService.get(url).then(function (resp) {
                $scope.cadgroup_list = resp.data.cadgroup;
                unlockUI();
            });
        }

        //Add

        $scope.addcadgroup = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addcadgroup.html',
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
                    $scope.cbocadgroupmanager_list = resp.data.employeelist;
                    unlockUI();
                });
               
                var url = 'api/SystemMaster/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.cbocadgroupmembers_list = resp.data.employeelist;
                    unlockUI();
                });

                $scope.submit = function () {
                    var params = {
                        cadmanager: $scope.cbocadgroupmanager,
                        cadmembers: $scope.cbocadgroupmember,
                        cadgroup_name: $scope.txtcadgroupname
                    }

                    lockUI();
                    var url = 'api/MstCADGroup/PostCADGroup';
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

        $scope.editcadgroup = function (cadgroup_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editcadgroup.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {               
                var params = {
                    cadgroup_gid: cadgroup_gid
                }
                lockUI();
                var url = 'api/MstCADGroup/GetCADGroupEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();

                    $scope.txteditcadgroup_name = resp.data.cadgroup_name;
                    $scope.cbocadgroupmanager_editlist = resp.data.cadmanager_list;
                    $scope.cadmanager = resp.data.cadmanager;
                    $scope.cbocadmanager_edit = [];
                    if (resp.data.cadmanager != null) {
                        var count = resp.data.cadmanager.length;
                        for (var i = 0; i < count; i++) {
                            var indexs = $scope.cbocadgroupmanager_editlist.map(function (x) { return x.employee_gid; }).indexOf(resp.data.cadmanager[i].employee_gid);
                            $scope.cbocadmanager_edit.push($scope.cbocadgroupmanager_editlist[indexs]);
                            $scope.$parent.cboSecondaryValueChain = $scope.cboSecondaryValueChain;
                        }
                    }
                    $scope.cbocadgroupmembers_editlist = resp.data.cadmembers_list;
                    $scope.cadmembers = resp.data.cadmembers;
                    $scope.cbocadmember_edit = [];
                    if (resp.data.cadmembers != null) {
                        var count = resp.data.cadmembers.length;
                        for (var i = 0; i < count; i++) {
                            var indexs = $scope.cbocadgroupmembers_editlist.map(function (x) { return x.employee_gid; }).indexOf(resp.data.cadmembers[i].employee_gid);
                            $scope.cbocadmember_edit.push($scope.cbocadgroupmembers_editlist[indexs]);
                            $scope.$parent.cboSecondaryValueChain = $scope.cboSecondaryValueChain;
                        }
                    }                
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {
                    lockUI();
                    var url = 'api/MstCADGroup/CADGroupUpdate';
                    var params = {
                        cadgroup_gid: cadgroup_gid,
                        cadmanager: $scope.cbocadmanager_edit,
                        cadmembers: $scope.cbocadmember_edit,
                        cadgroup_name: $scope.txteditcadgroup_name
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

        //Showpopover

        $scope.cadgroupmanagerpopover = function (cadgroup_gid, cadgroup_name) {
            var modalInstance = $modal.open({
                templateUrl: '/showcadmanager.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    cadgroup_gid: cadgroup_gid
                }
                lockUI();
                var url = 'api/MstCADGroup/GetCADGroupEmployee';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.cadmanager = resp.data.cadgroupmanager;
                    $scope.cadgroup_name = cadgroup_name;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.cadgroupmemberpopover = function (cadgroup_gid, cadgroup_name) {
            var modalInstance = $modal.open({
                templateUrl: '/showcadmember.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    cadgroup_gid: cadgroup_gid
                }
                lockUI();
                var url = 'api/MstCADGroup/GetCADGroupEmployee';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.cadmembers = resp.data.cadgroupmember;
                    $scope.cadgroup_name = cadgroup_name;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        //Delete

        $scope.delete = function (cadgroup_gid) {
            var params = {
                cadgroup_gid: cadgroup_gid
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
                    var url = 'api/MstCADGroup/DeleteCADGroup';
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
