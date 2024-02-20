(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasCourierMgmtAckList', idasCourierMgmtAckList);

    idasCourierMgmtAckList.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$timeout', 'ngTableParams'];

    function idasCourierMgmtAckList($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $timeout, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'idasCourierMgmtAckList';

        activate();

        function activate() {
            var url = 'api/IdasCourierManagement/CourierAckList';
            SocketService.get(url).then(function (resp) {
                $scope.courierackpending_list = resp.data.CourierAckPending;
                $scope.courierack_list = resp.data.CourierMgmt;
            });
        }

        $scope.back = function () {
            $state.go('app.idasCourierMgmtsummary');
        }

        // Submit Acknowledgement Code Starts
        $scope.ack_submit = function (courierMgmt_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/sendack.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    modalInstance.close('closed');
                };

                var params = {
                    courierMgmt_gid: courierMgmt_gid
                }
              
                var url = 'api/IdasCourierManagement/CourierAckView';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.courierref_no = resp.data.courierref_no;
                    $scope.date_of_courier = resp.data.date_of_courier;
                    $scope.sanctionref_no = resp.data.sanctionref_no;
                    $scope.customer_name = resp.data.customer_name;
                    $scope.document_type = resp.data.document_type;
                    $scope.sender_name = resp.data.sender_name;
                    $scope.pod_no = resp.data.pod_no;
                    $scope.couriercompany_name = resp.data.couriercompany_name;
                    $scope.courierhandover_to = resp.data.courierhandover_to;
                    $scope.courier_type = resp.data.courier_type;
                    $scope.ack_status = resp.data.ack_status;
                    $scope.remarks = resp.data.remarks;
                    $scope.ack_date = resp.data.ack_date;
                    $scope.ackby_name = resp.data.ackby_name;
                });

                // Submit
                $scope.sendcourierack = function () {
                    var params = {
                        courierMgmt_gid: courierMgmt_gid,
                    }
                    lockUI();
                    var url = "api/IdasCourierManagement/AckStatus"
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            unlockUI();
                        }
                        else {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }
                    });
                }
                // Click Cancel Button
                $scope.ok = function () {
                    modalInstance.close('closed');
                };
            }
        }
    }
})();
