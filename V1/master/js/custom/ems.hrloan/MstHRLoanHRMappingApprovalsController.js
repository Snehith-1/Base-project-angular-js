(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHRLoanHRMappingApprovalsController',MstHRLoanHRMappingApprovalsController );

        MstHRLoanHRMappingApprovalsController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstHRLoanHRMappingApprovalsController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHRLoanHRMappingApprovalsController';

        activate();

        function activate() {
            var url = 'api/MstHRLoanHRMappingApprovals/GetHRMapping';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.hrmapping_list = resp.data.hrmapping_list;
                unlockUI();
            });
            var url = 'api/MstHRLoanHRMappingApprovals/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employeelist;
                    unlockUI();
                });
           
        }

        $scope.hrmappingadd = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addhrmapping.html',
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
                var url = 'api/MstHRLoanHRMappingApprovals/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employeelist;
                    unlockUI();
                });               

                $scope.submit = function () {
                    if (($scope.hrmapping_name == 'Approver') && ($scope.cboemployee_apr == '' || $scope.cboemployee_apr == null) )
                {                 
                   Notify.alert('Kindly Fill Appover details', 'warning')
                       
            }
            else if (($scope.hrmapping_name == 'Manager') && ($scope.cboemployee == '' || $scope.cboemployee == null)) {
                Notify.alert('Kindly Fill Manager details', 'warning')
            }else{
                if($scope.hrmapping_name == 'Approver'){
                    var params = {
                       
                        employee_gid: $scope.cboemployee_apr.employee_gid,
                        employee_name: $scope.cboemployee_apr.employee_name,
                        hrmapping_name: $scope.hrmapping_name,
                        hrmapping_code: $scope.txthrmapping_code,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    }
                    console.log( $scope.cboemployee_apr.employee_gid);
                }else if($scope.hrmapping_name == 'Manager'){
                    var params = {
                       
                        employee: $scope.cboemployee,
                        hrmapping_name: $scope.hrmapping_name,
                        hrmapping_code: $scope.txthrmapping_code,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    }
                }
                    var url = 'api/MstHRLoanHRMappingApprovals/CreateHRMapping';
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
        }

        $scope.edithrmapping = function (hrmapping_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/edithrmapping.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                
                var url = 'api/MstHRLoanHRMappingApprovals/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employeelist;
                    unlockUI();
                });
               
                var params = {
                    hrmapping_gid: hrmapping_gid
                }
                var url = 'api/MstHRLoanHRMappingApprovals/EditHRMapping';
                SocketService.getparams(url, params).then(function (resp) {
                   
                   
                    $scope.txtedithrmapping_code = resp.data.hrmapping_code;                    
                    $scope.hrmapping_name = resp.data.hrmapping_name;
                    $scope.hrmapping_gid = resp.data.hrmapping_gid;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.cboemployee_editapr = resp.data.employee_name;
                    $scope.cboemployee_editlist = resp.data.employee;
                    $scope.employeeem_list = resp.data.employeeem_list;
                    $scope.cboemployee_edit = [];
                    if (resp.data.employeeem_list != null) {
                        var count = resp.data.employeeem_list.length;
                        for (var i = 0; i < count; i++) {
                            var Index = $scope.cboemployee_editlist.map(function (x) { return x.employee_gid; }).indexOf(resp.data.employeeem_list[i].employee_gid);                           
                            $scope.cboemployee_edit.push($scope.cboemployee_editlist[Index]);                           
                        }
                    }
               
                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {
                    
                    //var employee_name;
                    //var employee_index = $scope.employee_list.map(function (e) { return e.employee_gid }).indexOf($scope.cboemployee_editapr);
                    //if (employee_index == -1) { employee_name = ''; } else { employee_name = $scope.employee_list[employee_index].employee_name; };
                    if (($scope.hrmapping_name == 'Approver') && ($scope.cboemployee_editapr == '' || $scope.cboemployee_editapr == null) )
                    {                 
                       Notify.alert('Kindly Fill Appover details', 'warning')
                           
                }
                else if (($scope.hrmapping_name == 'Manager') && ($scope.cboemployee_edit == '' || $scope.cboemployee_edit == null)) {
                    Notify.alert('Kindly Fill Manager details', 'warning')
                }else{
                    if($scope.hrmapping_name == 'Approver'){
                    
                    var params = {
                        employee_gid: $scope.cboemployee_editapr.employee_gid, 
                        employee_name: $scope.cboemployee_editapr.employee_name,                       
                        hrmapping_name: $scope.hrmapping_name,
                        hrmapping_code: $scope.txtedithrmapping_code,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        hrmapping_gid: $scope.hrmapping_gid,                     
                       
                    }
                }else if($scope.hrmapping_name == 'Manager'){
                    var params = {
                        employee: $scope.cboemployee_edit,                        
                        hrmapping_name: $scope.hrmapping_name,
                        hrmapping_code: $scope.txtedithrmapping_code,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        hrmapping_gid: $scope.hrmapping_gid,            
                       
                    }
                }
                var url = 'api/MstHRLoanHRMappingApprovals/UpdateHRMapping';
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
        }
        $scope.showPopover = function (hrmapping_gid, hrmapping_name) {
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
                    hrmapping_gid: hrmapping_gid
                }
                lockUI();
                var url = 'api/MstHRLoanHRMappingApprovals/GetEmployeeName';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();                  
                    $scope.employee_name = resp.data.employee_name;
                    $scope.hrmapping_name = resp.data.hrmapping_name;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.Status_update = function (hrmapping_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statushrmapping.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    hrmapping_gid: hrmapping_gid
                }
                var url = 'api/MstHRLoanHRMappingApprovals/EditHRMapping';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.hrmapping_gid = resp.data.hrmapping_gid
                    $scope.hrmapping_name = resp.data.hrmapping_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        hrmapping_gid: hrmapping_gid,
                        hrmapping_name: $scope.hrmapping_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstHRLoanHRMappingApprovals/InactiveHRMapping';
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
                    hrmapping_gid: hrmapping_gid
                }

                var url = 'api/MstHRLoanHRMappingApprovals/HRMappingInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.hrmappinginactivelog_list = resp.data.hrmapping_list;
                    unlockUI();
                });

            }
        }

        $scope.deletehrmapping = function (hrmapping_gid,hrmapping_name) {
            var params = {
                hrmapping_gid: hrmapping_gid,
                hrmapping_name:hrmapping_name
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
                    var url = 'api/MstHRLoanHRMappingApprovals/DeleteHRMapping';
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            unlockUI();
                        }
                    });                    
                }

            });
        };

    }
})();