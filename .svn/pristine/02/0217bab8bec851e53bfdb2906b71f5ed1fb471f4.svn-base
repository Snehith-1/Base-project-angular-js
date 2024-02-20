(function () {
    'use strict';

    angular
        .module('angle')
        .controller('dasRemitterBuyerscontroller', dasRemitterBuyerscontroller);

    dasRemitterBuyerscontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function dasRemitterBuyerscontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'dasRemitterBuyerscontroller';

        activate();

        function activate() {
            var params = {
                customer_gid: val
            };
            $scope.remitterbuyersummary = function () {
                var url = "api/DASTracker/getremitterbuyers";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.remitterbuyerslist = resp.data.remitterbuyers;
                });
            }

        }

        $scope.selfbuyer = function () {
            $scope.showselfbuyer = true;
            $scope.showacknowledgedbuyer = false;
            $scope.showunacknowledgedbuyer = false;
            $scope.selfcustomername = customername;
        }

        $scope.acknowledgedbuyer = function () {
            $scope.showselfbuyer = false;
            $scope.showacknowledgedbuyer = true;
            $scope.showunacknowledgedbuyer = false;
            var url = "api/DASTracker/getacknowledgedbuyers";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.acknowledgedbuyers = resp.data.acknowledgedbuyers;
            });
        }

        $scope.unacknowledgedbuyer = function () {
            $scope.showselfbuyer = false;
            $scope.showunacknowledgedbuyer = true;
            $scope.showacknowledgedbuyer = false;
        }

        $scope.addremitterbuyer = function () {

            if ($scope.cboremitterbuyer == "Self") {
                var params = {
                    customer_gid: val,
                    remitter_status: $scope.cboremitterbuyer,
                    remitter_self: val
                }
            }
            else if ($scope.cboremitterbuyer == "Acknowledged buyer") {
                console.log($scope.acknowledgedbuyers_gid);
                if ($scope.acknowledgedbuyers_gid != undefined) {
                    var params = {
                        customer_gid: val,
                        remitter_status: $scope.cboremitterbuyer,
                        remitter_ackbuyersgid: $scope.acknowledgedbuyers_gid,
                        remitter_ackbuyers: $('#ackbuyersname :selected').text()
                    }
                }
                else {
                    alert('Select Acknowledged Buyer');
                    return;
                }

            }
            else {
                if ($scope.txtunacknowbuyers != "") {
                    var params = {
                        customer_gid: val,
                        remitter_status: $scope.cboremitterbuyer,
                        remitter_unackbuyers: $scope.txtunacknowbuyers,
                    }
                }
                else {
                    alert('Enter Unacknowledged Buyer');
                    return;
                }
            }

            var url = "api/DASTracker/addremitterbuyers";
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $('#ackbuyersname').val(0);
                    $scope.acknowledgedbuyers_gid = "";
                    $scope.txtunacknowbuyers = "";
                    console.log($scope.acknowledgedbuyers_gid = "");
                    $scope.remitterbuyersummary();
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

        $scope.deleteAcknowledgedbuyer = function (remitterbuyers_gid) {
            lockUI();
            var params = {
                remitterbuyers_gid: remitterbuyers_gid
            }
            console.log(params);
            var url = "api/DASTracker/deleteremitterbuyers"
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.remitterbuyersummary();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
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
})();
