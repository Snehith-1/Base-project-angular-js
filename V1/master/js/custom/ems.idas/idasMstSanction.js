(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasMstSanction', idasMstSanction);

    idasMstSanction.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function idasMstSanction($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        var vm = this;
        vm.title = 'idasMstSanction';
        var vertical_gid;
        var vertical_code;

        activate();

        function activate() {
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            vm.calenderEdit = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.openEdit = true;
            };
            vm.dateOptionsEdit = {
                formatYear: 'yy',
                startingDay: 1
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];

            var url = 'api/customer/customer';
            SocketService.get(url).then(function (resp) {
                $scope.customer_list = resp.data.customer_list;
            });
            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });

            var url = 'api/loan/loan_list';
            SocketService.get(url).then(function (resp) {
                $scope.loan_list = resp.data.loanmasterdtls;
            });

        
        }
     
        $scope.onselectedchangecustomer = function (customer) {
            var params = {
                customer_gid: customer
            }
            var url = 'api/loan/customer_getheads';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.mdlheadsofcustomer = resp.data;
                $scope.zonalHead = resp.data.zonalGid;
                $scope.businessHead = resp.data.businessHeadGid;
                $scope.clustermanager = resp.data.clustermanagerGid;
                $scope.relationshipMgmt = resp.data.relationshipMgmtGid;
                $scope.creditmgmt_name = resp.data.creditmanager_gid;
                $scope.vertical_code = resp.data.vertical_code;
                vertical_gid = resp.data.vertical_gid;
                vertical_code = resp.data.vertical_code;
            });

        }

        $scope.amountschange = function () {
            var input = document.getElementById('txtInput').value;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {

                var str = input.replace(',', '');
                input = str;
            }
            var output = Number(str).toLocaleString('en-US');
            $scope.txtSanctionAmount = output;
            //console.log(output);
        }

     
      
        $scope.sanctionSubmit = function () {
            var input = $scope.txtSanctionAmount;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {
                var str = input.replace(',', '');
                input = str;
            }
          
            var loan_title = $('#facility_type :selected').text();
            var customer_name = $('#customername :selected').text();
            var zonal_name = $('#zonal_name :selected').text();
            var businesshead_name = $('#businesshead_name :selected').text();
            var relationshipmgmt_name = $('#relationshipmgmt_name :selected').text();
            var cluster_manager_name = $('#cluster_manager_name :selected').text();
            var creditmgmt_name = $('#creditmanager_name :selected').text();
            var facility_type = $('#facility_type:selected').text();

            var params = {
                sanction_refno: $scope.sanctionrefno,
                sanction_amount: input,
                sanction_date: $scope.txtSanctionDate,
                customer_gid: $scope.customer,
                customername: customer_name,
                vertical_gid: vertical_gid,
                vertical_code: vertical_code,
                zonal_name: zonal_name,
                businesshead_name: businesshead_name,
                relationshipmgmt_name: relationshipmgmt_name,
                cluster_manager_name: cluster_manager_name,
                creditmanager_name: creditmgmt_name,
                zonalGid: $scope.zonalHead,
                businessHeadGid: $scope.businessHead,
                relationshipMgmtGid: $scope.relationshipMgmt,
                clustermanagerGid: $scope.clustermanager,
                creditmanagerGid: $scope.creditmgmt_name,
                facility_type: loan_title,
                facilitytype_gid: $scope.facility_type,
                collateral: $scope.collateral,
            }
            var url = 'api/IdasMstSanction/CreateSanction';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();

                    Notify.alert(resp.data.message, 'success')
                    $state.go('app.idasMstSanctionSummary');

                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message)
                }
                activate();
            });

        }

        $scope.sanctionback = function (val) {
            $state.go('app.idasMstSanctionSummary');
        }
    }
})();
