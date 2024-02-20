(function () {
    'use strict';

    angular
        .module('angle')
        .controller('allocationTransfercontroller', allocationTransfercontroller);

    allocationTransfercontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function allocationTransfercontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'allocationTransfercontroller';

        activate();

        function activate() {
            lockUI();
            $scope.totalDisplayed = 100;
            var url = "api/allocationTransfer/gettransferSummary";
            SocketService.get(url).then(function (resp) {
                $scope.allocationtransferlist = resp.data.allocationtransferdtl;
                $scope.count_OverallTransfer = resp.data.count_OverallTransfer;
                $scope.count_pendingApproval = resp.data.count_mypendingApproval;
                $scope.count_Approved = resp.data.count_myApproved;
                $scope.count_rejected = resp.data.count_myrejected;
                if ($scope.allocationtransferlist == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.allocationtransferlist.length;
                    if ($scope.allocationtransferlist.length < 100) {
                        $scope.totalDisplayed = $scope.allocationtransferlist.length;
                    }
                }
                unlockUI();
            });

        }

        document.getElementById('pagecount').onkeyup = function () {

            if ($scope.pagecount == null) {
                var el = document.getElementById('loadmore');
                el.style.backgroundColor = '#DCDCDC';
            }
            else {
                var el = document.getElementById('loadmore');
                el.style.backgroundColor = '#ffa';
            }
        };

        $scope.loadMore = function (pagecount) {
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pagecount);
            $scope.totalDisplayed += Number;
            unlockUI();
        };

        $scope.initiateTransfer = function () {
            $state.go('app.allocationTransferInitiate');
        }

        $scope.TransferAllocationView = function (allocationdtl_gid, customer_gid,allocation_transfergid){
            localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            localStorage.setItem('allocation_customer_gid', customer_gid);
            localStorage.setItem('allocation_transfergid',allocation_transfergid);
            localStorage.setItem('MyApprovalPage', 'N');
            $state.go('app.transferApproval360');
        }
    }
})();
