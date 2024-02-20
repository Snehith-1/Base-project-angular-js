(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstDeviationApprovalGroupController', MstDeviationApprovalGroupController);

        MstDeviationApprovalGroupController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstDeviationApprovalGroupController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstDeviationApprovalGroupController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();
        
        function activate() {
            var url = 'api/MstApplication360/GetDeviationApprovalGroupSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.deviationapprovalgroup = resp.data.DeviationApprovalGroup;
                unlockUI();
            });
        }


        $scope.adddeviationapprovalgroup = function () {
            var modalInstance = $modal.open({
                templateUrl: '/adddeviationapprovalgroup.html',
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
                    $scope.cbodeviationapprovalgroupmember_list = resp.data.employeelist;
                    $scope.cbodeviationapprovalgroupmanager_list = resp.data.employeelist;

                    unlockUI();
                });
                // var url = 'api/MstApplication360/GetProductsNameSummary';
                // lockUI();
                // SocketService.get(url).then(function (resp) {
                //     $scope.cboproducts_name = resp.data.Products_Name;
                //     unlockUI();
                // });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
               
              /*  var url = 'api/SystemMaster/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.cbodeviationapprovalgroupmanager_list = resp.data.employeelist;
                    unlockUI();
                }); */
                $scope.submit = function () {
                    var params = {
                        deviationapprovalgroup_name: $scope.txtdeviationapprovalgroup_name,
                        subgroup_name: $scope.txtsubgroup_name,
                        // products_gid: $scope.txtcboproducts_name.products_gid,
                        // products_name: $scope.txtcboproducts_name.products_name,
                        DeviationApprovalGroupMember: $scope.cbodeviationapprovalgroup_member,
                        DeviationApprovalGroupManager: $scope.cbodeviationapprovalgroup_manager,
                        deviationapprovalgroup_lms: $scope.txtdeviationapprovalgroup_lms,
                        deviationapprovalgroup_bureau: $scope.txtdeviationapprovalgroup_bureau

                    }
                    var url = 'api/MstApplication360/PostDeviationApprovalGroupAdd';
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
            }
        }

        $scope.editdeviationapprovalgroup = function (deviationapprovalgroup_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editdeviationapprovalgroup.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                    

                // var url = 'api/MstApplication360/GetProductsNameSummary';
                // lockUI();
                // SocketService.get(url).then(function (resp) {
                //     $scope.cboproducts_name = resp.data.Products_Name;
                //     unlockUI();
                // });
                
                var params = {
                    deviationapprovalgroup_gid: deviationapprovalgroup_gid
                }
                var url = 'api/MstApplication360/GetDeviationApprovalGroupEdit';
                    lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditdeviationapprovalgroup_id = resp.data.deviationapprovalgroup_id;
                    // $scope.cboeditproducts_gid = resp.data.products_gid;
                    $scope.txteditdeviationapprovalgroup_name = resp.data.deviationapprovalgroup_name;
                    $scope.txteditsubgroup_name = resp.data.subgroup_name;
                    $scope.txteditdeviationapprovalgroup_lms = resp.data.deviationapprovalgroup_lms;
                    $scope.txteditdeviationapprovalgroup_bureau = resp.data.deviationapprovalgroup_bureau;
                    $scope.cbodeviationapprovalgroupmember_editlist = resp.data.DeviationApprovalGroupMemberem_list;
                    $scope.DeviationApprovalGroupMember = resp.data.DeviationApprovalGroupMember;
                    $scope.cboeditdeviationapprovalgroup_member = [];
                    if (resp.data.DeviationApprovalGroupMember != null) {
                        var count = resp.data.DeviationApprovalGroupMember.length;
                        for (var i = 0; i < count; i++) {
                            var indexs = $scope.cbodeviationapprovalgroupmember_editlist.map(function (x) { return x.employee_gid; }).indexOf(resp.data.DeviationApprovalGroupMember[i].employee_gid);
                            $scope.cboeditdeviationapprovalgroup_member.push($scope.cbodeviationapprovalgroupmember_editlist[indexs]);
                        }
                    }
                    
                    $scope.cbodeviationapprovalgroupmanager_editlist = resp.data.DeviationApprovalGroupManagerem_list;
                    $scope.DeviationApprovalGroupManager = resp.data.DeviationApprovalGroupManager;
                    $scope.cboeditdeviationapprovalgroup_manager = [];
                    if (resp.data.DeviationApprovalGroupManager != null) {
                        var count = resp.data.DeviationApprovalGroupManager.length;
                        for (var i = 0; i < count; i++) {
                            var indexs = $scope.cbodeviationapprovalgroupmanager_editlist.map(function (x) { return x.employee_gid; }).indexOf(resp.data.DeviationApprovalGroupManager[i].employee_gid);
                            $scope.cboeditdeviationapprovalgroup_manager.push($scope.cbodeviationapprovalgroupmanager_editlist[indexs]);
                        }
                    }
                    unlockUI();
                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {
                    lockUI();
                    // var cboproducts_name = $('#cboproducts_name :selected').text();
                    var url = 'api/MstApplication360/PostDeviationApprovalGroupUpdate';
                    var params = {
                        deviationapprovalgroup_gid: deviationapprovalgroup_gid,
                        // products_gid: $scope.cboeditproducts_gid,
                        // products_name: cboproducts_name,
                        deviationapprovalgroup_name: $scope.txteditdeviationapprovalgroup_name,
                        subgroup_name: $scope.txteditsubgroup_name,
                        DeviationApprovalGroupMember: $scope.cboeditdeviationapprovalgroup_member,
                        DeviationApprovalGroupManager: $scope.cboeditdeviationapprovalgroup_manager,
                        deviationapprovalgroup_lms: $scope.txteditdeviationapprovalgroup_lms,
                        deviationapprovalgroup_bureau: $scope.txteditdeviationapprovalgroup_bureau
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

        //Delete
        
        $scope.delete = function (deviationapprovalgroup_gid) {
            var params = {
                deviationapprovalgroup_gid: deviationapprovalgroup_gid
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
                    var url = 'api/MstApplication360/DeleteDeviationApprovalGroup';
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


        $scope.showPopover = function (deviationapprovalgroup_gid, deviationapprovalgroup_name, subgroup_name) {
            var modalInstance = $modal.open({
                templateUrl: '/showdeviationapprovalgroup.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    deviationapprovalgroup_gid: deviationapprovalgroup_gid
                }
                lockUI();
                var url = 'api/MstApplication360/GetDeviationApprovalGroupDetails';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.deviationapprovalgroup_name = deviationapprovalgroup_name;
                    $scope.subgroup_name = subgroup_name;
                    $scope.DeviationApprovalGroupMember = resp.data.deviationapprovalgroup_member;
                    $scope.DeviationApprovalGroupManager = resp.data.deviationapprovalgroup_manager;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        
        $scope.Status_update = function (deviationapprovalgroup_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusdeviationapprovalgroup.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    deviationapprovalgroup_gid: deviationapprovalgroup_gid
                }
                var url = 'api/MstApplication360/GetDeviationApprovalGroupEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.deviationapprovalgroup_gid = resp.data.deviationapprovalgroup_gid
                    $scope.deviationapprovalgroup_name = resp.data.deviationapprovalgroup_name;
                    $scope.subgroup_name = resp.data.subgroup_name;
                    $scope.rbo_status = resp.data.deviationapprovalgroup_status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        deviationapprovalgroup_gid: deviationapprovalgroup_gid,
                        deviationapprovalgroup_name: $scope.deviationapprovalgroup_name,
                        subgroup_name: $scope.subgroup_name,
                        remarks: $scope.txtremarks,
                        deviationapprovalgroup_status: $scope.rbo_status

                    }
                    var url = 'api/MstApplication360/PostDeviationApprovalGroupInactive';
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
                    deviationapprovalgroup_gid: deviationapprovalgroup_gid
                }

                var url = 'api/MstApplication360/GetDeviationApprovalGroupInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.deviationapprovalgroupinactivelogview = resp.data.DeviationApprovalGrouplog;
                    unlockUI();
                });

            }
        }
    }
})();
