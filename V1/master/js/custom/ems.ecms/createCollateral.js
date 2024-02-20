(function () {
    'use strict';

    angular
        .module('angle')
        .controller('createCollateral', createCollateral);

    createCollateral.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function createCollateral($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'createCollateral';

        activate();

        function activate() {
            //var url = 'api/customer/customer';
            //SocketService.get(url).then(function (resp) {
            //    $scope.customer_list = resp.data.customer_list;
            //    $scope.customer_gid = resp.data.customer_gid;
            //});

            var url = 'api/security/getSecuritytype';
            SocketService.get(url).then(function (resp) {
                $scope.security_data = resp.data.securitytype_list;
            });
        }

        $scope.complete = function (string) {

            if (string.length >= 3) {
                $scope.message = "";
                var url = 'api/customer/ExploreCustomer';
                var params = {
                    customername: string
                }
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.message = "";
                        $scope.customer_list = resp.data.Customers;
                    }
                    else {
                        $scope.message = "No Records";
                    }


                });
            }
            else {
                $scope.customer_list = null;
                $scope.message = "Type atleast three character";
            }
        }

        $scope.fillTextbox = function (customer_gid, customer_name) {
            //console.log('string', customer_name, customer_gid);
            $scope.customer = customer_name;
            $scope.customer_gid = customer_gid;
            $scope.customer_list = null;




            var params = {
                customer_gid: customer_gid
            }
            var url = 'api/deferral/customer2loan';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.loan = resp.data.loan;

            });
       
        }


        $scope.onselectedchangecustomer = function (customer) {
            var params = {
                customer_gid: customer
            };
            var url = 'api/deferral/customer2loan';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.loan = resp.data.loan;
               
            });
        }

     
        $scope.checkall = function (selected) {
            angular.forEach($scope.loan, function (val) {
                val.checked = selected;
            });
        }

        $scope.collateralSubmit = function () {
           
            var loanGidList = [];
            angular.forEach($scope.loan, function (val) {

                if (val.checked == true) {
                    var loan_gid = val.value;
                 loanGidList.push(loan_gid);
                }

            });

            //var customer_name = $('#customer_name :selected').text();
            var security_type = $('#security_type :selected').text();
            var params = {
            customer_name: $scope.customer,
            customer_gid: $scope.customer_gid,
            security_type: security_type,
            securitytype_gid:$scope.security_type,
            security_description: $scope.security_description,
            account_status: $scope.account_status,
            loan_gid: loanGidList
            }
            //console.log(params);
            var url = 'api/collateral/createCollateral';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                   
                    unlockUI()

                   Notify.alert('Collateral Created Successfully..!!', 'success')

                   $state.go('app.collateralsummary');
                   
                }
                else {
                   
                    unlockUI()
                    Notify.alert('Select Atleast One Loan')
                    
                }
               
               });
        }
        $scope.back=function()
        {
            $state.go('app.collateralsummary');
        }

}
})();
