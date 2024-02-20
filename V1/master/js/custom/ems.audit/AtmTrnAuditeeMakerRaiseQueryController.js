(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmTrnAuditeeMakerRaiseQueryController', AtmTrnAuditeeMakerRaiseQueryController);

    AtmTrnAuditeeMakerRaiseQueryController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', '$sce', '$anchorScroll'];

    function AtmTrnAuditeeMakerRaiseQueryController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, $sce, $anchorScroll) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmTrnAuditeeMakerRaiseQueryController';
        $scope.auditcreation_gid = $location.search().auditcreation_gid;
        var auditcreation_gid = $scope.auditcreation_gid;

        activate();
        lockUI();
        function activate() {
            var params = {
                auditcreation_gid: auditcreation_gid,
            };

            var url = 'api/AtmTrnMyAuditTaskAuditee/GetQuerydetaillist';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Querydetaillist = resp.data.Querydetaillist;
                unlockUI();
            });

            var url = 'api/AtmTrnMyAuditTaskAuditee/GetTagUserAuditview';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.audittaguser_list = resp.data.audittaguser_list;
                unlockUI();
            });

            var url = 'api/AtmTrnMyAuditTaskAuditee/closequerysummary';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.closequery_list = resp.data.closequery_list;
                unlockUI();
            });
            var url = 'api/AtmTrnMyAuditTaskAuditee/GetclosequeryAudit';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.auditraisequery_status = resp.data.auditraisequery_status;
                unlockUI();
            });
        }

        $scope.refresh = function () {
            lockUI();
            activate();
        }

        $scope.replytoquery = function () {
            var params = {
                auditcreation_gid: auditcreation_gid,
                remarks: $scope.txtqueries
            }
            lockUI();
            var url = "api/AtmTrnMyAuditTaskAuditee/PostQuerydetail";
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    var url = "api/AtmTrnMyAuditTaskAuditee/GetQuerydetaillist";
                    var param = {
                        auditcreation_gid: auditcreation_gid
                    };
                    SocketService.getparams(url, param).then(function (resp) {
                        unlockUI();
                        $scope.Querydetaillist = resp.data.Querydetaillist;
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
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        unlockUI();
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                $scope.txtqueries = "";
            });
        }

        $scope.Back = function (val) {
            $state.go('app.AtmTrnMyAuditTaskAuditeeSummary');
        }

        $scope.back = function (val) {
            $state.go('app.AtmTrnAuditeeMakerSummary');
        }

        $scope.tagemployee = function () {
            var modalInstance = $modal.open({
                templateUrl: '/tagemployee.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    auditcreation_gid: auditcreation_gid,
                }

                var url = 'api/AtmTrnMyAuditTaskAuditee/EditMyAuditTaskAuditee';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.auditcreation_gid = resp.data.auditcreation_gid;
                    $scope.txtaudit_name = resp.data.audit_name;
                });

                $scope.ok = function () {
                    //$location.url('app/AtmTrnAuditRaiseQuery?auditcreation_gid=' + auditcreation_gid)
                    $modalInstance.close('closed');
                };



                var url = 'api/SystemMaster/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employeelist;
                    unlockUI();
                });

                $scope.btnconfirm = function () {

                    var params = {
                        tagemployelist: $scope.cboemployee_name,
                        audit_name: $scope.txtaudit_name,
                        auditcreation_gid: auditcreation_gid
                    }

                    var url = 'api/AtmTrnMyAuditTaskAuditee/PostTagUserAudit';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            //$location.url('app/AtmTrnSampling?auditcreation_gid=' + val1)
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

                //var param = {
                //    auditcreation_gid: auditcreation_gid
                //}

                //var url = 'api/AtmTrnMyAuditTaskAuditee/GetTagUserAuditview';
                //lockUI();
                //SocketService.getparams(url, param).then(function (resp) {
                //    $scope.audittaguser_list = resp.data.audittaguser_list;
                //    unlockUI();
                //});

            }
        }



        $scope.close_query = function () {

            var params = {
                auditcreation_gid: auditcreation_gid,
                closing_description: $scope.txtdescription

            }

            var url = 'api/AtmTrnMyAuditTaskAuditee/closequery';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.AtmTrnAuditorMakerSummary');
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
        }

        $scope.showPopover = function (auditcreation_gid) {
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
                    auditcreation_gid: auditcreation_gid
                }
                lockUI();
                var url = 'api/AtmTrnMyAuditTaskAuditee/GetEmployeeName';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.employee_name = resp.data.employee_name;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

    }
})();