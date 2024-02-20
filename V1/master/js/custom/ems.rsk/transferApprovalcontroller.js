(function () {
    'use strict';

    angular
        .module('angle')
        .controller('transferApprovalcontroller', transferApprovalcontroller);

    transferApprovalcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location','SweetAlert', '$route', 'ngTableParams'];

    function transferApprovalcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'transferApprovalcontroller';

        activate();

        function activate() {
            lockUI();
            $scope.totalDisplayedCrossZonal = 100;
            $scope.totalDisplayedWithinZonal = 100;
            $scope.totalDisplayedHistory = 100;
          
            var url = "api/allocationTransfer/gettransferApprovalSummary";
            SocketService.get(url).then(function (resp) {
                $scope.allocationtransferList = resp.data.allocationtransferdtl;
                if ($scope.allocationtransferList == null) {
                    $scope.totalCrossZonal = 0;
                    $scope.totalDisplayedCrossZonal = 0;
                }
                else {
                    $scope.totalCrossZonal = $scope.allocationtransferList.length;
                    if ($scope.allocationtransferList.length < 100) {
                        $scope.totalDisplayedCrossZonal = $scope.allocationtransferList.length;
                    }
                }
                $scope.zonalapproval = resp.data.zonalapproval;
                if ($scope.zonalapproval == null) {
                    $scope.totalWithinZonal = 0;
                    $scope.totalDisplayedWithinZonal = 0;
                }
                else {
                    $scope.totalWithinZonal = $scope.zonalapproval.length;
                    if ($scope.zonalapproval.length < 100) {
                        $scope.totalDisplayedWithinZonal = $scope.zonalapproval.length;
                    }
                }
                $scope.count_myapproval = resp.data.count_myapproval;
                $scope.count_mypendingApproval = resp.data.count_mypendingApproval;
                $scope.count_myApproved = resp.data.count_myApproved;
                $scope.count_myrejected = resp.data.count_myrejected;
                $scope.count_mywithinzonalApproval = resp.data.count_mywithinzonalApproval;
                $scope.count_mycrosszonalApproval = resp.data.count_mycrosszonalApproval;
            });
            unlockUI();
        }

        $scope.approvalhistoryclick = function () {
            lockUI();
            var url = "api/allocationTransfer/getApprovalHistorySummary";
            SocketService.get(url).then(function (resp) {
                $scope.allocationHistoryList = resp.data.allocationtransferdtl;
                if ($scope.allocationHistoryList == null) {
                    $scope.totalHistory = 0;
                    $scope.totalDisplayedHistory = 0;
                }
                else {
                    $scope.totalHistory = $scope.allocationHistoryList.length;
                    if ($scope.allocationHistoryList.length < 100) {
                        $scope.totalDisplayedHistory = $scope.allocationHistoryList.length;
                    }
                }
                unlockUI();
            });
        }

        $scope.loadMoreWithin = function (pageWithincount) {
            if (pageWithincount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pageWithincount);
            $scope.totalDisplayedWithinZonal += Number;
            unlockUI();
        };

        $scope.loadMoreCross = function (pageCrosscount) {
            if (pageCrosscount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pageCrosscount);
            $scope.totalDisplayedCrossZonal += Number;
            unlockUI();
        };

        $scope.loadMoreHistory = function (pagecount) {
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pagecount);
            $scope.totalDisplayedHistory += Number;
            unlockUI();
        };

        $scope.TransferFromAllocationView = function (allocationdtl_gid, customer_gid, transferapproval_gid, allocation_transfergid) {
            localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            localStorage.setItem('allocation_customer_gid', customer_gid);
            localStorage.setItem('transferapproval_gid', transferapproval_gid);
            localStorage.setItem('allocation_transfergid', allocation_transfergid);
            localStorage.setItem('TransferFromApproval', 'Y');
            localStorage.setItem('MyApprovalPage', 'Y');
            $state.go('app.transferApproval360');
        }

        $scope.TransferToAllocationView = function (allocationdtl_gid, customer_gid, transferapproval_gid, zonalapprovalto_Flag, allocation_transfergid) {
            localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            localStorage.setItem('allocation_customer_gid', customer_gid);
            localStorage.setItem('transferapproval_gid', transferapproval_gid);
            localStorage.setItem('ZonalApprovalToFlag', zonalapprovalto_Flag);
            localStorage.setItem('allocation_transfergid', allocation_transfergid);
            localStorage.setItem('TransferFromApproval', 'N');
            localStorage.setItem('MyApprovalPage', 'Y');
            $state.go('app.transferApproval360');
        }

        $scope.TransferApprovalHistory = function (allocationdtl_gid, customer_gid, allocation_transfergid) {
            localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            localStorage.setItem('allocation_customer_gid', customer_gid);
            localStorage.setItem('allocation_transfergid', allocation_transfergid);
            $state.go('app.transferApprovalHistory');
        }
    }
})();
