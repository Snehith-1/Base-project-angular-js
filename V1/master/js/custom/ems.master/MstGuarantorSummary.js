(function () {
    'use strict';

    angular
        .module('angle')
        .controller('mstguarantorsummarycontroller', mstguarantorsummarycontroller);

    mstguarantorsummarycontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function mstguarantorsummarycontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'mstguarantorsummarycontroller';

        activate();

        function activate() {

            $scope.totalDisplayed = 100;
            var url = 'api/MstGuarantor/GetGuarantorList';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.Guarantor_data = resp.data.guarantor_list;
                unlockUI();
                // new code start   
                if ($scope.Guarantor_data == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.Guarantor_data.length;
                    if ($scope.Guarantor_data.length < 100) {
                        $scope.totalDisplayed = $scope.Guarantor_data.length;
                    }
                }
              
            });          
        }
        $scope.showPopover = function (guarantor_id, name) {
            var modalInstance = $modal.open({
                templateUrl: '/taggedlist.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    guarantor_id: guarantor_id
                }
                
                console.log(name);
                $scope.name = name;
                var url = 'api/MstGuarantor/GetList';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.customer_list = resp.data.customer_list;
                         }
                    else {
                    }
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
     
        $scope.loadMore = function (pagecount) {
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            // new code start
            if ($scope.customer_data != null) {

                if (pagecount < $scope.customer_data.length) {
                    $scope.totalDisplayed += Number;
                    if ($scope.customer_data.length < $scope.totalDisplayed) {
                        $scope.totalDisplayed = $scope.customer_data.length;
                        Notify.alert(" Total Summary " + $scope.customer_data.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.customer_data.length + " Records Only", "warning");
                    return;
                }
            }
           
            unlockUI();
        };
        $scope.guarantorView = function (customer2usertype_gid, guarantor_id)
        {
            $scope.customer2usertype_gid = customer2usertype_gid;
            $scope.customer2usertype_gid = localStorage.setItem('customer2usertype_gid', customer2usertype_gid);
            $scope.guarantor_id = guarantor_id;
            $scope.guarantor_id = localStorage.setItem('guarantor_id', guarantor_id);
                $state.go('app.MstGuarantorView');
        }
    }
})();
