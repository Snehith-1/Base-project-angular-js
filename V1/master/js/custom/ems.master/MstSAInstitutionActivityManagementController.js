(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSAInstitutionActivityManagementController', MstSAInstitutionActivityManagementController);

    MstSAInstitutionActivityManagementController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstSAInstitutionActivityManagementController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true **/
        var vm = this;
        vm.title = 'MstSAInstitutionActivityManagementController';

        activate();

        function activate() {
            $scope.eye = true
            $scope.eyeslash = false
            $scope.dateyes = true

            var url = 'api/MstSAOnboardingInstitution/GetInstitutionActivityManagementSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.saOnboardSummary_list = resp.data.saOnboardSummary_list;
                unlockUI();
            });
            var url = 'api/MstSAOnboardingInstitution/GetSacodecreationCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.institution_count = resp.data.institution_count;
                $scope.individual_count = resp.data.individual_count;
                $scope.institutioninitiated_count = resp.data.institutioninitiated_count;
                $scope.individualinitiated_count = resp.data.individualinitiated_count;

            });
        }
        $scope.employee_resetpwd = function (sa_autogeneratedid) {

            var modalInstance = $modal.open({
                templateUrl: '/empresetpassowrd.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.calender03 = function ($event) {
                    $event.preventDefault();
                    $event.stopPropagation();

                    $scope.open03 = true;
                };
                $scope.formats = ['dd-MM-yyyy'];
                $scope.format = $scope.formats[0];
                $scope.dateOptions = {
                    formatYear: 'yy',
                    startingDay: 1

                };
                var params = {
                    sa_autogeneratedid: sa_autogeneratedid,
                }

                var url = 'api/MstSAOnboardingBussDevtVerification/samcodesview';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI()
                    $scope.samfin_code = resp.data.samfin_code;
                    $scope.samagro_code = resp.data.samagro_code;

                });
                //var params = {
                //    employee_gid: employee_gid,
                //}
                //var url = 'api/ManageEmployee/ResetPswdEdit';
                //SocketService.getparams(url, params).then(function (resp) {

                //    $scope.employee_code = resp.data.user_code;
                //    $scope.employee_name = resp.data.employee_name;
                //    $scope.user_gid = resp.data.user_gid;
                //});

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.passwordSubmit = function () {
                    var params = {
                        sbauser_password: $scope.Password,
                        sa_autogeneratedid: sa_autogeneratedid
                    }
                    console.log(params);
                    var url = 'api/MstSAOnboardingBussDevtVerification/PasswordUpdate';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {

                            activate();
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success')

                        }
                        else {

                            Notify.alert(resp.data.message, 'warning')
                            activate();

                        }
                    });
                }
            }
        }


        $scope.user_deactive = function (sa_autogeneratedid) {
            var modalInstance = $modal.open({
                templateUrl: '/Loginstatus.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {


                var params = {
                    sa_autogeneratedid: sa_autogeneratedid
                }
                var url = 'api/MstSAOnboardingBussDevtVerification/samcodesview';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtcode = resp.data.samfin_code;
                    $scope.rbo_status = resp.data.active;
                });
                var url = 'api/MstSAOnboardingBussDevtVerification/GetLoginActiveLog';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.log_list = resp.data.log_list;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {
                    if ($scope.rbo_status == "N") {
                        Notify.alert('Already status is Inactive', {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        var params = {
                            sa_autogeneratedid: sa_autogeneratedid,
                            remarks: $scope.txtremarks
                        }
                        console.log(params);
                        var url = 'api/MstSAOnboardingBussDevtVerification/Activation';
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
        }

        $scope.user_active = function (sa_autogeneratedid) {
            var modalInstance = $modal.open({
                templateUrl: '/Loginstatus.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {


                var params = {
                    sa_autogeneratedid: sa_autogeneratedid
                }
                var url = 'api/MstSAOnboardingBussDevtVerification/samcodesview';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtcode = resp.data.samfin_code;
                    $scope.rbo_status = resp.data.active;
                });
                var url = 'api/MstSAOnboardingBussDevtVerification/GetLoginActiveLog';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.log_list = resp.data.log_list;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {
                    if ($scope.rbo_status == "Y") {
                        Notify.alert('Already status is Active', {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        var params = {
                            sa_autogeneratedid: sa_autogeneratedid,
                            remarks: $scope.txtremarks
                        }
                        console.log(params);
                        var url = 'api/MstSAOnboardingBussDevtVerification/Deactivation';
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
        }


        $scope.WebStatus_update = function (sa_autogeneratedid) {
            var modalInstance = $modal.open({
                templateUrl: '/webaccessstatus.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {


                var params = {
                    sa_autogeneratedid: sa_autogeneratedid
                }
                var url = 'api/MstSAOnboardingBussDevtVerification/samcodesview';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtcode = resp.data.samfin_code;
                    $scope.rbo_status = resp.data.web_active;
                });
                var url = 'api/MstSAOnboardingBussDevtVerification/GetWebAccessActiveLog';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.log_list = resp.data.log_list;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {
                    if ($scope.rbo_status == "Y") {
                        Notify.alert('Already status is active', {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        var params = {
                            sa_autogeneratedid: sa_autogeneratedid,
                            remarks: $scope.txtremarks
                        }

                        var url = 'api/MstSAOnboardingBussDevtVerification/WebDeActivation';
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
        }
        $scope.user_webaccess = function (sa_autogeneratedid) {
            var modalInstance = $modal.open({
                templateUrl: '/webaccessstatus.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {


                var params = {
                    sa_autogeneratedid: sa_autogeneratedid
                }
                var url = 'api/MstSAOnboardingBussDevtVerification/samcodesview';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtcode = resp.data.samfin_code;
                    $scope.rbo_status = resp.data.web_active;
                });
                var url = 'api/MstSAOnboardingBussDevtVerification/GetWebAccessActiveLog';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.log_list = resp.data.log_list;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {
                    if ($scope.rbo_status == "N") {
                        Notify.alert('Already status is Inactive', {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        var params = {
                            sa_autogeneratedid: sa_autogeneratedid,
                            remarks: $scope.txtremarks
                        }
                        var url = 'api/MstSAOnboardingBussDevtVerification/WebActivation';
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
        }


        $scope.viewIndividual = function (sacontactinstitution_gid) {
            $location.url('app/MstSAOnboardingInstitutionCodeCreateView?hash=' + cmnfunctionService.encryptURL('lssacontactinstitution_gid=' + sacontactinstitution_gid + '&lspage=Institutioncodecreate'));
        }

        $scope.institution_pending = function () {
            $state.go('app.MstSBACodeCreationSummary');///app.MstSAOnboardingCodeApprovalInsSummary
        }
        $scope.institution_completed = function () {
            $state.go('app.MstSAInstitutionActivityManagement');///app.MstSAOnboardingCodeApprovalInsSummary
        }
        $scope.individual_completed = function () {
            $location.url('app/MstSBAIndividualActivityManagement')
        }

        $scope.individual_pending = function () {
            $state.go('app.MstSAOnboardingCodeApprovalIndSummary');
        }
        $scope.management_view = function (sacontactinstitution_gid) {
            $location.url('app/MstSBAManagementInstitutionCodeCreateView?hash=' + cmnfunctionService.encryptURL('lssacontactinstitution_gid=' + sacontactinstitution_gid));
        }

        $scope.show = function () {

            if ($scope.inputType == 'password')
                $scope.inputType = 'text';
            $scope.eye = false
            $scope.eyeslash = true
            //else
            //    $scope.inputType = 'password';

        }

        $scope.noshow = function () {

            if ($scope.inputType == 'text')
                $scope.inputType = 'password';
            //else
            //    $scope.inputType = 'password';
            $scope.eye = true
            $scope.eyeslash = false

        }

        $scope.saonboardingverification = function (sacontactinstitution_gid) {
            $location.url('app/MstSAOnboardingInstitutionCodecreation?hash=' + cmnfunctionService.encryptURL('lssacontactinstitution_gid=' + sacontactinstitution_gid + '&lspage=InstituteInitiate'));
        }
        $scope.activitymanagement = function (sacontactinstitution_gid,samfin_code,new_code) {
            $location.url('app/MstSBAManagement?hash=' + cmnfunctionService.encryptURL('lssacontactinstitution_gid=' + sacontactinstitution_gid + '&samfin_code=' + samfin_code + '&new_code=' + new_code));
            //$location.url('app/AtmTrnSampleQueryAuditor?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + auditcreation_gid + '&checklistmaster_gid=' + checklistmaster_gid + '&sampleimport_gid=' + sampleimport_gid + '&lspage=' + lspage + '&lsobservationfill_flag=' + $scope.observationfill))

        }
    }
})();