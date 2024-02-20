(function () {
    'use strict';

    angular
        .module('angle')
        .controller('applyLeavecontroller', applyLeavecontroller);

    applyLeavecontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function applyLeavecontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'applyLeavecontroller';

        activate();

        function activate() {

            var url = "api/applyLeave/leavetype"
            SocketService.get(url).then(function (resp) {
                $scope.leavetype_list = resp.data.leavetype_list;
                
            });
        }

        $scope.applyleave = function (leavetype_gid) {
            var params = {
                leavetype_gid: leavetype_gid
            }
            
            var url = 'api/applyLeave/leavetype';

        SocketService.post(url, params).then(function (resp) {
            leavetype_gid = leavetype_gid;
            console.log(leavetype_gid);
            if (resp.data.status == true) {
                $modalInstance.close('closed');
                Notify.alert('Leave Applied Successfully!!', 'success')
                activate();

            }
            else {
                Notify.alert('Error Occurred!', 'warning')
                activate();
            }
        });
   
            
            var modalInstance = $modal.open({
                templateUrl: '/myModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.fullclick = function () {
                    $scope.leavefull = true;
                    $scope.leavehalf = false;
                   
                }
                $scope.halfclick = function () {
                    $scope.leavefull = false;
                    $scope.leavehalf = true;
                }
                $scope.fullleavesubmit = function () {
                    var params = {
                        leave_from: $scope.fromdate,
                        leave_to: $scope.todate,
                        leave_reason: $scope.leave_reason,
                        leavetype_name: $scope.leavetypeName
                        
                    }
                    var url = 'api/applyLeave/applyleavesummary';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert('Leave Applied Successfully!!', 'success')
                            activate();

                        }
                        else {
                            Notify.alert('Error Occurred!', 'warning')
                            activate();
                        }
                       
                        
                    });
                    $state.go('app.applyLeave');
                }
               
               
            }
        }

    }
})();
