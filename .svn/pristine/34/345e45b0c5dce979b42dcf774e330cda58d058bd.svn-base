(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndMstCustomerApprovingMasterController', FndMstCustomerApprovingMasterController);

    FndMstCustomerApprovingMasterController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function FndMstCustomerApprovingMasterController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndMstCustomerApprovingMasterController';

        activate();

        function activate() {
            var url = 'api/FndMstCustomerApprovingMaster/GetCustomerApproving';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.customerapproving_list = resp.data.customerapproving_list;
                unlockUI();
            });


        }

        $scope.customerapprovingadd = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addcustomerapproving.html',
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

                var url = 'api/FndMstCustomerMasterAdd/GetPendingCustomer';
                SocketService.get(url).then(function (resp) {
                    $scope.customerlist = resp.data.customerpending_list;

                });

                var url = 'api/SystemMaster/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employeelist;
                    unlockUI();
                });

                
                $scope.submit = function () {

                    var lscustomer_gid = '';
                    var lscustomer_name = '';
                    if ($scope.cboCustomer != undefined || $scope.cboCustomer != null) {
                        lscustomer_gid = $scope.cboCustomer.customer_gid,
                        lscustomer_name = $scope.cboCustomer.customer_name;
                    }

                    var lsapprover_gid = '';
                    var lsapprover_name = '';
                    if ($scope.cboemployee != undefined || $scope.cboemployee != null) {
                        lsapprover_gid = $scope.cboemployee.employee_gid,
                        lsapprover_name = $scope.cboemployee.employee_name;
                    }


                    var params = {
                        approver_gid: lsapprover_gid,
                        approver_name: lsapprover_name,
                        customer_gid: lscustomer_gid,
                        customer_name: lscustomer_name,
                        customerapproving_code: $scope.txtcustomerapproving_code,
                        lms_code: $scope.txtlms_code,
                        remarks: $scope.txtaddremarks,
                        bureau_code: $scope.txtbureau_code
                    }

                    var url = 'api/FndMstCustomerApprovingMaster/CreateCustomerApproving';
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
        $scope.editcustomerapproving = function (customerapproving_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editcustomerapproving.html',
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
                    $scope.employee_list = resp.data.employeelist;
                    unlockUI();
                });


                var params = {
                    customerapproving_gid: customerapproving_gid
                }
                var url = 'api/FndMstCustomerApprovingMaster/EditCustomerApproving';
                SocketService.getparams(url, params).then(function (resp) {


                    $scope.txteditcustomerapproving_code = resp.data.customerapproving_code;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    //$scope.txtcustomer_name = resp.data.customer_gid;
                    $scope.txtcustomer_name = resp.data.customer_name;
                    $scope.txteditremarks = resp.data.remarks;
                    $scope.customerapproving_gid = resp.data.customerapproving_gid;
                    $scope.cboemployee_editlist = resp.data.employeeem_list;
                    //$scope.employee = resp.data.approver_name;
                    $scope.cboemployee_edit = resp.data.approver_gid;
                    //$scope.cboemployee_edit = [];
                    //if (resp.data.employee != null) {
                    //    var count = resp.data.employee.length;
                    //    for (var i = 0; i < count; i++) {
                    //        var indexs = $scope.cboemployee_editlist.findIndex(x => x.employee_gid === resp.data.employee[i].employee_gid);
                    //        $scope.cboemployee_edit.push($scope.cboemployee_editlist[indexs]);
                    //        $scope.$parent.cboSecondaryValueChain = $scope.cboSecondaryValueChain;
                    //    }
                    //}

                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

              

                //var url = 'api/FndMstCustomerMasterAdd/GetPendingCustomer';
                //SocketService.get(url).then(function (resp) {
                //    $scope.customerlist = resp.data.customerpending_list;

                //});

                $scope.update = function () {

                    var customer_name;
                    //var customername_index = $scope.customerlist.map(function (e) { return e.customer_gid }).indexOf($scope.txtcustomer_name);
                    //if (customername_index == -1) { customer_name = ''; } else { customer_name = $scope.customerlist[customername_index].customer_name; };

                    var approver_name;
                    //var approver_index = $scope.employee_list.map(function (e) { return e.employee_gid }).indexOf($scope.cboemployee_edit);
                    //if (approver_index == -1) { approver_name = ''; } else { approver_name = $scope.employee_list[approver_index].employee_name; };

                    var url = 'api/FndMstCustomerApprovingMaster/UpdateCustomerApproving';
                    var params = {
                        customerapproving_code: $scope.txteditcustomerapproving_code,
                        customer_name: $scope.txtcustomer_name,
                        //customer_name: customer_name,
                        approver_gid: $scope.cboemployee_edit,
                        ////approver_name: employee_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        remarks: $scope.txteditremarks,
                        customerapproving_gid: $scope.customerapproving_gid,
                        //employee: $scope.cboemployee_edit,


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
        $scope.showPopover = function (customerapproving_gid, customerapproving_name) {
            var modalInstance = $modal.open({
                templateUrl: '/showemployee.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    customerapproving_gid: customerapproving_gid
                }
                lockUI();
                var url = 'api/FndMstCustomerApprovingMaster/GetEmployeeName';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.employee_name = resp.data.employee_name;
                    $scope.customerapproving_name = resp.data.customerapproving_name;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }


        $scope.showsPopover = function (customerapproving_gid) {
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
                    customerapproving_gid: customerapproving_gid
                }
                var url = 'api/FndMstCustomerApprovingMaster/EditCustomerApproving';
                SocketService.getparams(url, params).then(function (resp) {


                    $scope.txteditcustomerapproving_code = resp.data.customerapproving_code;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.customer_approving = resp.data.customerapproving_name;
                    $scope.txteditremarks = resp.data.remarks;
                    $scope.customerapproving_gid = resp.data.customerapproving_gid;
                    $scope.cboemployee_editlist = resp.data.employeeem_list;
                    $scope.employee = resp.data.employee;
                   

                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

             
            }
        }
        $scope.Status_update = function (customerapproving_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuscustomerapproving.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    customerapproving_gid: customerapproving_gid
                }
                var url = 'api/FndMstCustomerApprovingMaster/EditCustomerApproving';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.customerapproving_gid = resp.data.customerapproving_gid
                    $scope.customer_approving = resp.data.customerapproving_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        customerapproving_gid: customerapproving_gid,
                        customerapproving_name: $scope.txtcustomerapproving_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/FndMstCustomerApprovingMaster/InactiveCustomerApproving';
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
                    customerapproving_gid: customerapproving_gid
                }

                var url = 'api/FndMstCustomerApprovingMaster/CustomerApprovingInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.customerapprovinginactivelog_list = resp.data.customerapproving_list;
                    unlockUI();
                });

            }
        }

        $scope.deletecustomerapproving = function (customerapproving_gid) {
            var params = {
                customerapproving_gid: customerapproving_gid
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

                    var url = 'api/FndMstCustomerApprovingMaster/DeleteCustomerApproving';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Customer Approving !!!', {
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