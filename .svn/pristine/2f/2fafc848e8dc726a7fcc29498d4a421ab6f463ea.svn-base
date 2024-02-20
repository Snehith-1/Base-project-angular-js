(function () {
    'use strict';

    angular
        .module('angle')
        .controller('customer2EscrowCreate', customer2EscrowCreate);

    customer2EscrowCreate.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout'];

    function customer2EscrowCreate($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout) {
        var vm = this;
        vm.title = 'customer2EscrowCreate';
        var customer_gid;
        activate();

        function activate() {
            customer_gid = localStorage.getItem('customer_gid');
            $scope.customer_name = localStorage.getItem('customer_name');
            $scope.urn = localStorage.getItem('urn');
            $scope.customer_code = localStorage.getItem('customer_code');
          
            vm.calenderDisbursement = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.opendisbursement = true;
            };
            vm.calenderTransaction = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.openTransaction = true;
            };
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];


            var params = {
                customer_gid:customer_gid
            }
            var url = 'api/loan/customer_getheads';

            SocketService.getparams(url, params).then(function (resp) {
              
                $scope.sanctiondtl = resp.data.sanctiondtl;
                console.log(resp.data.sanctiondtl);

            });
        }

        $scope.escrowSubmit = function () {
          
            var params = {
                sanction_gid: $scope.cbosanction.sanction_Gid,
                customer_gid: customer_gid,
                disbursement_date:$scope.txtdisbursementDate,
                transaction_date: $scope.transactionDate,
                transactionref_no: $scope.transactionRefNo,
                escrow_account_no: $scope.escrow_accountno,
                dealer_name: $scope.dealername,
                master_account_no: $scope.master_accountno,
                amount: $scope.amount,
                beneficiary_customer_account_name: $scope.beneficiarycustomer_accountname,
                sender_customer_account_name: $scope.sendercustomer_accountname,
                sender_customer_account_no: $scope.sendercustomer_accountno,
                remittance_info: $scope.remittance,
                sender_branch_IFSC: $scope.sendbranch_ifsc,
                reference: $scope.reference,
                credit_time: $scope.creditTime,
                remarks: $scope.remarks
            };

            var url = "api/customerManagement/escrowCreate";
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();

                    Notify.alert(resp.data.message, 'success')
                    $state.go('app.Customer2EscrowSummary');

                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message)
                }
                activate();
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
            var output = Number(str).toLocaleString('en-IN');
            $scope.amount = output;

        }
        $scope.sanctionrefnochange = function (sanction_Gid) {
            var params = {
                sanction_gid: $scope.cbosanction.sanction_Gid
            }
            var url = 'api/loan/GetSanctionDate';
            SocketService.getparams(url, params).then(function (resp) {
              
                $scope.sanctionDate = resp.data.sanctiondate;
                $scope.Sanction_Date = resp.data.Sanction_Date;
                $scope.facilitytype = resp.data.facility_type;
                $scope.facilitytype_gid = resp.data.facilitytype_gid;
            });
        }

        $scope.back=function()
        {
            $state.go('app.Customer2EscrowSummary');
        }
    }
})();
