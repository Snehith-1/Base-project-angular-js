(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstRMSanctionSummaryController', MstRMSanctionSummaryController);

    MstRMSanctionSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstRMSanctionSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstRMSanctionSummaryController';
        var application_gid = $location.search().application_gid;

        activate();
        lockUI();
        function activate() {
            var params = {
                application_gid: application_gid
            }
            var url = "api/MstCAD/GetAppSanctionSummary";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.sanctionlist = resp.data.appsanction_list;
                unlockUI();
            }); 
        }

        $scope.getReApprovalRequest = function (application2sanction_gid, application_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/Acceptpopup.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    application_gid: application_gid
                }
                var url = 'api/MstCAD/SanctionPopup';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.application2sanction_gid = resp.data.application2sanction_gid
                    $scope.lblsanction_refno = resp.data.sanction_refno;
                    $scope.rbo_status = resp.data.accepted_status;
                    //$scope.disable_status = resp.data.accepted_status;
                    $scope.customeraccepted = true;
                    if (resp.data.accepted_status == 'Y') {
                        $scope.customeraccepted = true;
                        $scope.customernotaccepted = false;
                      
                    }
                    if (resp.data.accepted_status == 'N') {
                        $scope.customeraccepted = false;
                        $scope.customernotaccepted = true;                     
                    }
                });


                $scope.rdbcustomer = function () {
                    $scope.customeraccepted = true;
                    $scope.customernotaccepted = false;
                 
                }
                $scope.rdbcustomernot = function () {
                    $scope.customeraccepted = false;
                    $scope.customernotaccepted = true;
                   
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_accept = function () {
                    if ($scope.rbo_status == '' || $scope.rbo_status == null) {

                        Notify.alert('Kindly Select Customer Status', 'warning')
                    }
                      
                else
                    { 
                    if ($scope.rbo_status == 'N') {
                        if ($scope.txtremarks == '' || $scope.txtremarks == null) {
                            Notify.alert('Kindly Enter Remarks', 'warning')
                        }
                        else {
                            var params = {
                                application_gid: application_gid,
                                application2sanction_gid: application2sanction_gid,
                                sanction_refno: $scope.lblsanction_refno,
                                remarks: $scope.txtremarks,
                                rbo_status: $scope.rbo_status

                            }
                            var url = 'api/MstCAD/SanctionAccepte';
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
                        }
                       
                    }
                    else {
                        var params = {
                            application_gid: application_gid,
                            application2sanction_gid: application2sanction_gid,
                            sanction_refno: $scope.lblsanction_refno,
                            remarks: $scope.txtremarks,
                            rbo_status: $scope.rbo_status

                        }
                        var url = 'api/MstCAD/SanctionAccepte';
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

                    }

                   

                }

                }

                var param = {
                    application_gid: application_gid
                }

                var url = 'api/MstCAD/SanctionAcceptedLog';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.appsanctionlog_list = resp.data.appsanction_list;
                    unlockUI();
                });

            }
        }


        $scope.view = function (customer2sanction_gid) {
            $location.url('app/MstAppSanctionLetterWordView?sanction_gid=' + customer2sanction_gid + '&lspage=RMSanctionSummary&application_gid=' + application_gid);
        }

        $scope.Back = function () {
            $location.url('app/MstPostCcActivitiesRMView?application_gid=' + application_gid);
        }

    }
})();
