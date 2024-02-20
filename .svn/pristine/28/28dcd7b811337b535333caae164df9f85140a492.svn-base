(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCourierMgmtAckFormController', MstCourierMgmtAckFormController);

    MstCourierMgmtAckFormController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$timeout'];

    function MstCourierMgmtAckFormController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $timeout) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCourierMgmtAckFormController';
        var url = window.location.href;
        var relPath = url.split("?id=");
        var id = relPath[1];
        var relPath = id.split("&emp_gid=");
        var courierMgmt_gid = relPath[0];
        var employee_gid = relPath[1];


        activate();
        lockUI();
        function activate() {
            $scope.showack = true;
            $scope.hideack = true;
            $scope.hideexitack = true;


            var params = {
                courierMgmt_gid: courierMgmt_gid
            };
            var url = 'api/MstCourierAck/GetAcknowledgeForm';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.courier_details = resp.data;
                    if (resp.data.ack_status == "Pending") {
                        $scope.hideack = true;
                        $scope.showack = true;
                        $scope.hideexitack = true;
                    }
                    else {
                        $scope.hideack = true;
                        $scope.showack = false;
                        $scope.hideexitack = false;
                    }
                }
                else {

                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 4000
                    });
                    $scope.hideexitack = false;
                    $scope.showack = false;
                    $scope.hideack = true;
                }
            });
        }

        /* Feedback Add */

        $scope.feedback_submit = function () {
            var params = {
                courierMgmt_gid: courierMgmt_gid,
                employee_gid: employee_gid
            }
            var url = 'api/MstCourierAck/PostAckStatus';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 4000
                    });

                    $scope.hideack = false;
                    $scope.hideexitack = true;
                    $scope.showack = false;
                }
                else {

                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 4000
                    });

                    $scope.hideack = true;
                    $scope.hideexitack = true;
                    $scope.showack = true;
                }
            });

        }

    }
})();
