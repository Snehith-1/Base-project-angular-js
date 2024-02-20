(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditStatusApprovedBuyer', MstCreditStatusApprovedBuyer);

    MstCreditStatusApprovedBuyer.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCreditStatusApprovedBuyer($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditStatusApprovedBuyer';

        activate();

        function activate() {
            var url = 'api/MstCreditStatusAdd/GetCreditStatusCount';
            SocketService.get(url).then(function (resp) {
                $scope.count_pending = resp.data.count_pending;
                $scope.count_approved = resp.data.count_approved;
                $scope.count_nonapproved = resp.data.count_nonapproved;
            });

            var url = 'api/MstCreditStatusAdd/GetCreditStatusApprovedBuyer';
            SocketService.get(url).then(function (resp) {
                $scope.buyer_list = resp.data.creditstatuslist;
            });
        }
        $scope.Addcredit_status = function (val, val1) {
            localStorage.setItem('buyer_gid', val);
            localStorage.setItem('credit_status', val1);
            localStorage.setItem('lspage', 'CreditStatusApprovedBuyer');
            $state.go('app.MstCreditStatusAdd');
        }

        $scope.Viewcredit_status = function (val) {
            localStorage.setItem('buyer_gid', val);
            localStorage.setItem('lspage', 'CreditStatusApprovedBuyer');
            $state.go('app.MstBuyer2CreditStatusView');
        }

        $scope.Status_update = function (buyer_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusCreditStatus.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    buyer_gid: buyer_gid
                }
                var url = 'api/MstCreditStatusAdd/buyerDetailsEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.buyer_gid = resp.data.buyer_gid
                    $scope.txtbuyer_name = resp.data.buyer_name;
                    $scope.rbo_status = resp.data.creditActive_status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        buyer_gid: buyer_gid,
                        buyer_name: $scope.txtbuyer_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status
                    }

                    var url = 'api/MstCreditStatusAdd/InactiveCreditStatusbuyer';
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
                    buyer_gid: buyer_gid
                }

                var url = 'api/MstCreditStatusAdd/CreditStatusbuyerInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.buyerInactiveListlog_data = resp.data.buyerInactive_List;

                    unlockUI();
                });
            }
        }

        $scope.credit_statusApproval = function (buyer_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/creditstatusApproval.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    buyer_gid: buyer_gid
                }
                var url = 'api/MstCreditStatusAdd/buyerDetailsEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.buyer_gid = resp.data.buyer_gid
                    $scope.txtbuyer_name = resp.data.buyer_name;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.SubmitApproval = function () {

                    var params = {
                        buyer_gid: buyer_gid,
                        buyer_name: $scope.txtbuyer_name,
                        approval_remarks: $scope.txtapprovalremarks,
                        rbo_status: $scope.rbo_status
                    }

                    var url = 'api/MstCreditStatusAdd/CreditStatusApproval';
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
                    buyer_gid: buyer_gid
                }

                var url = 'api/MstCreditStatusAdd/GetCreditStatusApprovalLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.buyerApproval_data = resp.data.buyerApproval_List;

                    unlockUI();
                });
            }
        }

        $scope.Pending = function () {
            $state.go('app.MstCreditStatusSummary');
        }
        $scope.NonApprovedBuyer = function () {
            $state.go('app.MstCreditStatusNonApprovedBuyer');
        }
    }
})();
