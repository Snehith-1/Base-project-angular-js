(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditUnderwritingFacilityTypeController', MstCreditUnderwritingFacilityTypeController);

    MstCreditUnderwritingFacilityTypeController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCreditUnderwritingFacilityTypeController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditUnderwritingFacilityTypeController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            var url = 'api/MstApplication360/GetCreditUnderwritingFacilityType';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.creditunderwritingfacilitytype_data = resp.data.application_list;
                unlockUI();
            });
        }


        $scope.addCreditUnderwritingFacilityType = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addCreditUnderwritingFacilityType.html',
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
                        credit_underwriting_facility_type: $scope.txtcredit_underwriting_facility_type,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/MstApplication360/CreateCreditUnderwritingFacilityType';
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
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                    });

                    //  $modalInstance.close('closed');

                }
               
                
            }
        }

        $scope.editCreditUnderwritingFacilityType = function (creditunderwritingfacilitytype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editCreditUnderwritingFacilityType.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    creditunderwritingfacilitytype_gid: creditunderwritingfacilitytype_gid
                }
                var url = 'api/MstApplication360/EditCreditUnderwritingFacilityType';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditcredit_underwriting_facility_type = resp.data.credit_underwriting_facility_type;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.creditunderwritingfacilitytype_gid = resp.data.creditunderwritingfacilitytype_gid;
                });

               
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/MstApplication360/UpdateCreditUnderwritingFacilityType';
                    var params = {
                        credit_underwriting_facility_type: $scope.txteditcredit_underwriting_facility_type,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        creditunderwritingfacilitytype_gid: $scope.creditunderwritingfacilitytype_gid
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

        $scope.Status_update = function (creditunderwritingfacilitytype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusCreditUnderwritingFacilityType.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    creditunderwritingfacilitytype_gid: creditunderwritingfacilitytype_gid
                }
                var url = 'api/MstApplication360/EditCreditUnderwritingFacilityType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.creditunderwritingfacilitytype_gid = resp.data.creditunderwritingfacilitytype_gid
                    $scope.txtcredit_underwriting_facility_type = resp.data.credit_underwriting_facility_type;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        creditunderwritingfacilitytype_gid: creditunderwritingfacilitytype_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstApplication360/InactiveCreditUnderwritingFacilityType';
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
                    creditunderwritingfacilitytype_gid: creditunderwritingfacilitytype_gid
                }

                var url = 'api/MstApplication360/CreditUnderwritingFacilityTypeInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.creditunderwritingfacilitytypeinactivelog_data = resp.data.application_list;
                    unlockUI();
                });

            }
        }

        $scope.delete = function (creditunderwritingfacilitytype_gid) {
            var params = {
                creditunderwritingfacilitytype_gid: creditunderwritingfacilitytype_gid
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
                    var url = 'api/MstApplication360/DeleteCreditUnderwritingFacilityType';
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

