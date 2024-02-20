(function () {
    'use strict';

    angular
        .module('angle')
        .controller('osdMstBusinessUnit', osdMstBusinessUnit);

    osdMstBusinessUnit.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','cmnfunctionService'];

    function osdMstBusinessUnit($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'osdMstBusinessUnit';
        var lstab = $location.search().lstab;
        activate();

        function activate() {

            var url = 'api/OsdMstDepartmentManagement/GetBusinessUnitSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.businessunit_list = resp.data.businessunit_list;
                unlockUI();
            });

            var url = 'api/OsdMstDepartmentManagement/GetBusinessStatusTempClear';
            SocketService.get(url).then(function (resp) {
            });

        }
        $scope.addbusinessunit = function () {
            $location.url('app/osdMstBusinessUnitAdd');
        }

        // Add Code Starts


        $scope.businessunitSubmit = function () {
            lockUI(); 
            if ($scope.txtbusinessunit_emailaddress == undefined) {
                $scope.txtbusinessunit_emailaddress = "";
            }
                        var params = {
                            businessunit_prefix: $scope.txtbusinessunit_prefix,
                            businessunit_name: $scope.txtbusinessunit_name,
                            businessunit_emailaddress: $scope.txtbusinessunit_emailaddress,
                            //business_status: $scope.txtbusinessunit_status,
                        }
                        var url = 'api/OsdMstDepartmentManagement/PostBusinessUnit';
                        SocketService.post(url, params).then(function (resp) {
                            if (resp.data.status == true) {
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                              
                                unlockUI();
                                $state.go('app.osdMstBusinessUnit');
                            }
                            else {
                                Notify.alert(resp.data.message, {
                                    status: 'danger',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                unlockUI();
                                $modalInstance.close('closed');
                                activate();
                                unlockUI();
                            }
                        });
                    }
        //$scope.addbusinessunit = function () {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/addbusinessunitModalContent.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'md'
        //    });
        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {
                
        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };
                
        //        $scope.businessunitSubmit = function () {
        //            lockUI();
        //            var params = {
        //                businessunit_prefix: $scope.txtbusinessunit_prefix,
        //                businessunit_name: $scope.txtbusinessunit_name,
        //                businessunit_emailaddress: $scope.txtbusinessunit_emailaddress,
        //            }
        //            var url = 'api/OsdMstDepartmentManagement/PostBusinessUnit';
        //            SocketService.post(url, params).then(function (resp) {
        //                if (resp.data.status == true) {
        //                    Notify.alert(resp.data.message, {
        //                        status: 'success',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                    $modalInstance.close('closed');
        //                    activate();
        //                    unlockUI();
        //                }
        //                else {
        //                    alert(resp.data.message, {
        //                        status: 'danger',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                    $modalInstance.close('closed');
        //                    activate();
        //                    unlockUI();
        //                }
        //            });
        //        }

        //    }
        //}
        // Add Code Ends
        $scope.businessstatus_add = function () {

            if (($scope.txtbusinessunit_status == undefined) || ($scope.txtbusinessunit_status == '')) {            
                Notify.alert('Enter Business Unit Status', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
            }
            else {

            var params = {
                business_status: $scope.txtbusinessunit_status,
             
            }
            var url = 'api/OsdMstDepartmentManagement/PostBusinessUnitStatus';
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
                $scope.txtbusinessunit_status = '';
                businessstatus_list();
               
            });
        }
         
        }
        function businessstatus_list() {
            var url = 'api/OsdMstDepartmentManagement/GetBusinessstatusList';
            var params = {
                user_gid: $scope.user_gid,

            }
            SocketService.getparams(url,params).then(function (resp) {
                $scope.businessstatusunit_list = resp.data.businessstatusunit_list;

            });
        }

        // Edit Code Starts
        $scope.edit = function (businessunit_gid) {
            $location.url('app/osdMstBusinessUnitEdit?hash=' + cmnfunctionService.encryptURL('businessunit_gid=' + businessunit_gid));
          

        }
        //$scope.edit = function (businessunit_gid) {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/businessunitModaledit.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'md'
        //    });
        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {

        //        var params = {
        //            businessunit_gid: businessunit_gid
        //        }
        //        var url = 'api/OsdMstDepartmentManagement/BusinessUnitView';
        //        SocketService.getparams(url, params).then(function (resp) {
        //            $scope.txtbusinessunit_codeedit = resp.data.businessunit_code,
        //            $scope.txtbusinessunit_prefixedit = resp.data.businessunit_prefix;
        //            $scope.txtbusinessunit_nameedit = resp.data.businessunit_name,
        //            $scope.txtbusinessunit_emailaddressedit = resp.data.businessunit_emailaddress;
        //            if(resp.data.sequence_flag == 'Y')
        //            {
        //                $scope.label = true
        //                $scope.text = false
        //            }
        //            else {
        //                $scope.label = false
        //                $scope.text = true
        //            }
        //        });


        //        $scope.ok = function () {
        //            modalInstance.close('closed');
        //        };
        //        $scope.businessunitUpdate = function () {
                  
        //            var params = {
        //                businessunit_prefix: $scope.txtbusinessunit_prefixedit,
        //                businessunit_name: $scope.txtbusinessunit_nameedit,
        //                businessunit_emailaddress: $scope.txtbusinessunit_emailaddressedit,
        //                businessunit_gid: businessunit_gid,
        //            }

        //            var url = 'api/OsdMstDepartmentManagement/UpdateBusinessUnit';
        //            SocketService.post(url, params).then(function (resp) {
        //                if (resp.data.status == true) {
        //                    Notify.alert(resp.data.message, {
        //                        status: 'success',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                    $modalInstance.close('closed');
        //                    activate();
        //                }
        //                else {
        //                    alert(resp.data.message, {
        //                        status: 'danger',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                    $modalInstance.close('closed');
        //                    activate();
        //                }
        //            });
        //        }
        //    }
        //}
        // Edit Code Ends

        // Delete Code Starts
        $scope.delete = function (val) {
            var params = {
                businessunit_gid: val
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
                    var url = "api/OsdMstDepartmentManagement/DeleteBusinessUnit";
                    SocketService.getparams(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            activate();
                            SweetAlert.swal('Deleted Successfully!');
                            unlockUI();
                        }
                        else {
                            alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }
                    });
                }
            });
        }
        // Delete Code Ends
        $scope.businessstatus_delete = function (businessstatus_gid) {
            var params =
                {
                    businessstatus_gid: businessstatus_gid
                }
            console.log(params)
            var url = 'api/OsdMstDepartmentManagement/DeleteBusinessstatus';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
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

                businessstatus_list();
            });

        }
        $scope.Status_update = function (businessunit_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/businessunitstatus.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    businessunit_gid: businessunit_gid
                }
                var url = 'api/OsdMstDepartmentManagement/BusinessUnitView';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.rbo_status = resp.data.businessunit_status;
                    $scope.businessunit_name = resp.data.businessunit_name;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        businessunit_gid: businessunit_gid,
                        businessunit_name: $scope.businessunit_name,
                        remarks: $scope.txtremarks,
                        businessunitstatus: $scope.rbo_status

                    }
                    var url = 'api/OsdMstDepartmentManagement/Postbusinessunitstatusupdate';
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

                        }
                        else {
                            $modalInstance.close('closed');
                           Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        activate();
                    });
                }

                var param = {
                    businessunit_gid: businessunit_gid
                }

                var url = 'api/OsdMstDepartmentManagement/BusinessunitstatusHistory';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.businessunitstatusehistory = resp.data.businessunitstatusHistory_list;
                    unlockUI();
                });

            }
        }

        $scope.cancel = function () {
           
            $location.url('app/osdMstBusinessUnit');
            
        }
       
    }
})();