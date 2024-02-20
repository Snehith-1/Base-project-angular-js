(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndMstCustomerChequeViewController', FndMstCustomerChequeViewController);

    FndMstCustomerChequeViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','cmnfunctionService'];

    function FndMstCustomerChequeViewController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndMstCustomerChequeViewController';
        
        $scope.customer_gid = cmnfunctionService.decryptURL($location.search().hash).lscustomer_gid;
        $scope.fndmanagement2cheque_gid = cmnfunctionService.decryptURL($location.search().hash).lsfndmanagement2cheque_gid;
        $scope.tab = cmnfunctionService.decryptURL($location.search().hash).lstab;
        activate();
        function activate() {

            var params = {
                fndmanagement2cheque_gid: $scope.fndmanagement2cheque_gid
            }
           var url = 'api/FndMstCustomerMasterAdd/GetChequeView';
            SocketService.getparams(url, params).then(function (resp) {
                //$scope.cheque_list = resp.data.cheque_list;
                $scope.accountholder_name = resp.data.accountholder_name;
                $scope.account_number = resp.data.account_number;
                $scope.bank_name = resp.data.bank_name;
                $scope.cheque_no = resp.data.cheque_no;
                $scope.ifsc_code = resp.data.ifsc_code;
                $scope.micr = resp.data.micr;
                $scope.branch_address = resp.data.branch_address;
                $scope.branch_name = resp.data.branch_name;
                $scope.city = resp.data.city;
                $scope.district = resp.data.district;
                $scope.state = resp.data.state;
                $scope.cbomergedbankingentity_name = resp.data.mergedbankingentity_name;
                $scope.cheque_type = resp.data.cheque_type;
                $scope.date_chequetype = resp.data.date_chequetype;
                $scope.cts_enabled = resp.data.cts_enabled;
                $scope.date_chequepresentation = resp.data.date_chequepresentation;
                $scope.status_chequepresentation = resp.data.status_chequepresentation;
                $scope.date_chequeclearance = resp.data.date_chequeclearance;
                $scope.status_chequeclearance = resp.data.status_chequeclearance;
                $scope.special_condition = resp.data.special_condition;
                $scope.general_remarks = resp.data.general_remarks;
            });
        }

        $scope.back = function () {
            if ($scope.tab == "edit") {
                $location.url('app/FndMstCustomerMasterEdit?hash=' + cmnfunctionService.encryptURL('lscustomer_gid=' + $scope.customer_gid + '&lstab=edit'));

                //$location.url('app/FndMstCustomerMasterEdit?lscustomer_gid=' + $scope.customer_gid + '&lsfndmanagement2cheque_gid=' + $scope.fndmanagement2cheque_gid + '&lstab=edit');
            }
            else if ($scope.tab == "add") {
                $location.url('app/FndMstCustomerMasterAdd');
            }
        }
    }
})();