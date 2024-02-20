(function () {
    'use strict';

    angular
        .module('angle')
        .controller('serviceRequestApprovalcontroller', serviceRequestApprovalcontroller);

    serviceRequestApprovalcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$timeout'];

    function serviceRequestApprovalcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $timeout) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'serviceRequestApprovalcontroller';
        var url = window.location.href;
        var relPath = url.split("?id=");
        var relpath1 = relPath[1];
        activate();

        function activate() {
           

            $scope.showapproval = true;
            $scope.hideapproval = true;
            var params = {
                approval_token: relpath1
            };
            var url = 'api/OsdTrnRequestApproval/GetRequestDtl';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.request_title = resp.data.request_title;
                $scope.request_refno = resp.data.request_refno;
                $scope.activity_name = resp.data.activity_name;
                $scope.assigned_dtl = resp.data.assigned_dtl;
                $scope.getapproval_remarks = resp.data.getapproval_remarks;
                $scope.hierary_level = resp.data.hierary_level;
                $scope.servicerequest_gid = resp.data.servicerequest_gid;
                $scope.approval_type = resp.data.approval_type;

                unlockUI();
                if (resp.data.status == true) {
                   
                    $scope.hideapproval = true;
                    $scope.showapproval = true;
                }
                else {

                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 4000
                    });
                    $scope.showapproval = false;
                    $scope.hideapproval = false;
                }
            });
        }

        $scope.approve_submit = function () {
            
            var hierarylevel = $scope.hierary_level;
            var level = ++hierarylevel;
            var params = {
                approval_remarks: $scope.txtremarks,
                approval_token: relpath1,
                hierary_level: level,
                servicerequest_gid: $scope.servicerequest_gid,
                approval_type: $scope.approval_type
            }
            lockUI();
            var url = "api/OsdTrnRequestApproval/PostRequestApproved";
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $scope.showapproval = false;
                    $scope.hideapproval = false;
                }
                else {
                    
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();

                }
            });
        }

        $scope.reject_submit = function () {
            var hierarylevel = $scope.hierary_level;
            var level = ++hierarylevel;

            var params = {
                approval_remarks: $scope.txtremarks,
                approval_token: relpath1,
                hierary_level: level,
                servicerequest_gid: $scope.servicerequest_gid,
                approval_type: $scope.approval_type
            }
            lockUI();
            var url = "api/OsdTrnRequestApproval/PostRequestRejected";
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                     
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $scope.showapproval = false;
                    $scope.hideapproval = false;
                }
                else {
                     
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                  
                }
            });
        }
    }
})();
