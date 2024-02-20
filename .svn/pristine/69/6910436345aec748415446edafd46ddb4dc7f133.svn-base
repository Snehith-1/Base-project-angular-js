(function () {
    'use strict';

    angular
        .module('angle')
        .controller('dasTrackercontroller', dasTrackercontroller);

    dasTrackercontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout', '$modal'];

    function dasTrackercontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout, $modal) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'dasTrackercontroller';

        activate();

        function activate() {
            var url = 'api/customer/customerdetail';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.customer_data = resp.data.customer_list;
                unlockUI();
            });
        }



        //  Acknowledged buyers .............//

        $scope.ackbuyers = function (val) {
            var params = {
                customer_gid: val,
            };
            var modalInstance = $modal.open({
                templateUrl: '/AcknowledgedBuyerContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.acknowledgedbuyerssummary = function () {
                    var url = "api/DASTracker/getacknowledgedbuyers";
                    SocketService.getparams(url, params).then(function (resp) {
                        console.log(resp);
                        $scope.acknowledgedbuyers = resp.data.acknowledgedbuyers;
                    });

                }
                $scope.acknowledgedbuyerssummary();

                // Add Acknowledged buyers  ....//

                $scope.addacknowledgedbuyers = function () {
                    var addack = {
                        customer_gid: val,
                        acknowledged_buyers: $scope.txtackbuyers
                    };
                    console.log(addack);
                    var url = "api/DASTracker/addacknowledgedbuyers";
                    lockUI();
                    SocketService.post(url, addack).then(function (resp) {
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $scope.txtackbuyers = "";
                            $scope.acknowledgedbuyerssummary();
                            unlockUI();
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

                // Delete Acknowledged buyers  ....//

                $scope.deleteAcknowledgedbuyer = function (acknowledgedbuyers_gid) {
                    lockUI();
                    var deleteackbuyers = {
                        acknowledgedbuyers_gid: acknowledgedbuyers_gid
                    }
                    console.log(deleteackbuyers);
                    var url = "api/DASTracker/deleteacknowledgedbuyers";
                    SocketService.getparams(url, deleteackbuyers).then(function (resp) {
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $scope.acknowledgedbuyerssummary();
                            unlockUI();
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

                // Edit Acknowledged buyers  ....//
                $scope.editAcknowledgedbuyer = function (acknowledgedbuyers_gid) {
                    lockUI();
                    var editackbuyers = {
                        acknowledgedbuyers_gid: acknowledgedbuyers_gid,
                        acknowledged_buyers: $scope.txteditackbuyers
                    }
                    var url = "api/DASTracker/updateacknowledgedbuyers";
                    SocketService.post(url, editackbuyers).then(function (resp) {
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $scope.acknowledgedbuyerssummary();
                            unlockUI();
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
        }


        //  Remitters .............//


        $scope.remitterbuyers = function (val, customername) {

            $state.go('app.dasRemitterBuyers');
        
        }
    }
})();
