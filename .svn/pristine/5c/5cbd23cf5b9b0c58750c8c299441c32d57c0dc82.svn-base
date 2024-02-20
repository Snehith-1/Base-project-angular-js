(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstAssessmentCriteriaController', MstAssessmentCriteriaController);

    MstAssessmentCriteriaController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstAssessmentCriteriaController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstAssessmentCriteriaController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() {

            var url = 'api/MstApplication360/GetAssessmentCriteria';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.assessmentcriteria_data = resp.data.application_list;
                unlockUI();
            });
        }

        $scope.addAssessmentCriteria = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addAssessmentCriteria.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {

                    var params = {
                        assessmentcriteria_name: $scope.txtassessmentcriteria_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/MstApplication360/CreateAssessmentCriteria';
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
                            activate();
                        }
                    });

                    $modalInstance.close('closed');

                }
            }
        }

        $scope.editAssessmentCriteria = function (assessmentcriteria_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editAssessmentCriteria.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                var params = {
                    assessmentcriteria_gid: assessmentcriteria_gid
                }
                var url = 'api/MstApplication360/EditAssessmentCriteria';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditassessmentcriteria_name = resp.data.assessmentcriteria_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.assessmentcriteria_gid = resp.data.assessmentcriteria_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update = function () {

                    var url = 'api/MstApplication360/UpdateAssessmentCriteria';
                    var params = {
                        assessmentcriteria_name: $scope.txteditassessmentcriteria_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        assessmentcriteria_gid: $scope.assessmentcriteria_gid
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

        $scope.Status_update = function (assessmentcriteria_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusAssessmentCriteria.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    assessmentcriteria_gid: assessmentcriteria_gid
                }
                var url = 'api/MstApplication360/EditAssessmentCriteria';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.assessmentcriteria_gid = resp.data.assessmentcriteria_gid
                    $scope.txtassessmentcriteria_name = resp.data.assessmentcriteria_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        assessmentcriteria_gid: assessmentcriteria_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstApplication360/InactiveAssessmentCriteria';
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
                    assessmentcriteria_gid: assessmentcriteria_gid
                }

                var url = 'api/MstApplication360/AssessmentCriteriaInactiveHistory';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.assessmentcriteriainactivelog_data = resp.data.inactivehistory_list;
                    unlockUI();
                });

            }
        }

        $scope.delete = function (assessmentcriteria_gid) {
            var params = {
                assessmentcriteria_gid: assessmentcriteria_gid
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
                            var url = 'api/MstApplication360/DeleteAssessmentCriteria';
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