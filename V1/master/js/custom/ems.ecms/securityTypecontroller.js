(function () {
    'use strict';

    angular
        .module('angle')
        .controller('securityTypecontroller', securityTypecontroller);

    securityTypecontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function securityTypecontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'securityTypecontroller';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() {
            $scope.totoalDisplayed=100;
            var url = 'api/security/getSecuritytype';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.security_data = resp.data.securitytype_list;
                // new code start   
                unlockUI();
                if ($scope.security_data == null) {
                                        $scope.total = 0;
                                        $scope.totalDisplayed = 0;
                                    }
                                    else {
                                        $scope.total = $scope.security_data.length;
                                        if ($scope.security_data.length < 100) {
                                            $scope.totalDisplayed = $scope.security_data.length;
                                        }
                                    }
                    // new code end
                // $scope.total=$scope.security_data.length;
            });
        }
     
  $scope.loadMore= function (pagecount) {
            if(pagecount==undefined){
                Notify.alert("Enter the Total Summary Count","warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            // new code start
             if ($scope.security_data != null) {
       
                        if (pagecount < $scope.security_data.length) {
                            $scope.totalDisplayed += Number;
                            if($scope.security_data.length<$scope.totalDisplayed){
                                $scope.totalDisplayed =$scope.security_data.length;
                                Notify.alert(" Total Summary " + $scope.security_data.length + " Records Only", "warning");
                            }
                            unlockUI();
                        }
                        else {
                            unlockUI();
                            Notify.alert(" Total Summary " + $scope.security_data.length + " Records Only", "warning");
                            return;
                        }
                    }
                    // new code end
            // $scope.totalDisplayed += Number;
            // console.log(pagecount);
            unlockUI();
  };
  $scope.popupSecurity = function () {
            var modalInstance = $modal.open({
                templateUrl: '/myModalContent.html',
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
                
                $scope.SecuritySubmit = function () {
                    var params = {
                        security_type: $scope.txtsecurity_type,
                        bureau_code: $scope.txtbureau_code,
                        lms_code: $scope.txtlms_code,
                    }
                    //console.log(params);
                    var url = 'api/security/createSecurityType';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert('Security Type Added Successfully..!!', 'success')
                            activate();

                        }
                        else {
                            Notify.alert('Error Occurred While Adding Security Type!', 'warning')
                            
                        }
                    });
                    $modalInstance.close('closed');
                    activate();
                }
          

            }
        }

        $scope.edit = function (securitytype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/securityedit.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            //var doc = document.getElementById('edit');
            //doc.style.display = 'block';
            $scope.securitytype_gid = securitytype_gid;
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                //$scope.customer_gid = customer_gid;
                $scope.securitytype_gid = localStorage.setItem('securitytype_gid', securitytype_gid);
                var params = {
                    securitytype_gid: securitytype_gid
                }
                var url = 'api/security/GetSecurityTypeEdit';

                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditsecurity_type = resp.data.security_type;
                    $scope.securityTypegid = resp.data.securitytype_gid;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.txteditlms_code = resp.data.lms_code;
                });
                
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.cancel = function () {
                    $modalInstance.dismiss('cancel');
                };

                $scope.securityUpdate = function () {

                    var params = {
                        security_type: $scope.txteditsecurity_type,
                        securitytype_gid: securitytype_gid,
                        bureau_code: $scope.txteditbureau_code,
                        lms_code: $scope.txteditlms_code,
                    }
                    var url = 'api/security/securityTypeUpdate';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {

                            //$scope.close('edit');
                            $modalInstance.close('closed');
                            //SweetAlert.swal('Success!', 'Covenant Type Updated!', 'success');
                            Notify.alert('Security Type Updated Successfully..!!', {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                        else {
                            
                            Notify.alert('Error Occurred ', {
                                status: 'warning',
                                pos: 'top-right',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }

            }

        }
        $scope.delete = function (securitytype_gid) {
            var params = {
                securitytype_gid: securitytype_gid
            }
            var url = 'api/security/securityTypeDelete';
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
        $scope.Status_update = function (securitytype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusCompanyDocument.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    securitytype_gid: securitytype_gid
                }
                var url = 'api/security/GetSecurityTypeEdit';

                SocketService.getparams(url, params).then(function (resp) {
                    $scope.securityTypenameedit = resp.data.security_type;
                    $scope.securityTypegid = resp.data.securitytype_gid;
                    $scope.txtbureau_codeedit = resp.data.bureau_code;
                    $scope.txtlms_codeedit = resp.data.lms_code;
                    $scope.rbo_status = resp.data.status_log;
                });
                var url = 'api/security/GetActiveLog';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.securitytype_list = resp.data.securitytype_list;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        remarks: $scope.txtremarks,
                        status_log: $scope.rbo_status,
                        securitytype_gid: securitytype_gid
                    }
                    var url = 'api/security/securityTypeStatusUpdate';
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
                                status: 'info',
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
})();
