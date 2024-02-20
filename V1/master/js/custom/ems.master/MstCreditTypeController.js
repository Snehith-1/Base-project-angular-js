(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditTypeController', MstCreditTypeController);

        MstCreditTypeController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCreditTypeController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditTypeController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() { 

            $scope.total = 0;
            $scope.totalDisplayed = 100;
            var url = 'api/MstApplication360/GetCreditTypeSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.credittype_data = resp.data.application_list;
                unlockUI();

                //�new�code�start   
                if ($scope.credittype_data == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.credittype_data.length;
                    if ($scope.credittype_data.length < 100) {
                        $scope.totalDisplayed = $scope.credittype_data.length;
                    }
                }
                //�new�code�endd
            });
        }

        $scope.addcredittype = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addcredittype.html',
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
                $scope.submit = function () {

                    var params = {
                        credittype_name: $scope.txtcredittype_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    
                    }
                    var url = 'api/MstApplication360/CreditTypeAdd';
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

        $scope.editcredittype = function (credittype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editcredittype.html',
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
                                              
                var params = {
                    credittype_gid: credittype_gid
                }
                console.log(params);
                var url = 'api/MstApplication360/CreditTypeEdit';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditcredittype_name = resp.data.credittype_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.credittype_gid = resp.data.credittype_gid;
                });
               
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/MstApplication360/CreditTypeUpdate';
                    var params = {
                        credittype_name: $scope.txteditcredittype_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        credittype_gid: $scope.credittype_gid
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
                            activate();
                        }
                    }); 

                }
                
            }
        }

        $scope.Status_update = function (credittype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuscredittype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                                
                                   
                var params = {
                    credittype_gid: credittype_gid
                }
                var url = 'api/MstApplication360/CreditTypeEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.credittype_gid = resp.data.credittype_gid
                    $scope.txteditcredittype_name = resp.data.credittype_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        credittype_name: $scope.txtcredittype_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status,
                        credittype_gid: credittype_gid
                    }
                    console.log(params);
                    var url = 'api/MstApplication360/CreditTypeInactive';
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
              
                var param = {
                    credittype_gid: credittype_gid
                }
                var url = 'api/MstApplication360/CreditTypeInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.credittypeinactivelog_data = resp.data.application_list;
                    unlockUI();
                });
            }
        }

        $scope.delete = function (credittype_gid) {
           var params = {
               credittype_gid: credittype_gid
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
                    var url = 'api/MstApplication360/CreditTypeDelete';
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

