(function () {
    'use strict';

    angular
        .module('angle')
        .controller('editLoan', editLoan);

    editLoan.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function editLoan($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        var loanValues;
        vm.title = 'editLoan';
        activate();
        function activate() {
            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened = true;
            };

            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };


            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.clusterlist = resp.data.employee_list;
                $scope.businesshead_list = resp.data.employee_list;
                $scope.relationshiplist = resp.data.employee_list;
                $scope.zonallist = resp.data.employee_list;
                $scope.creditlist = resp.data.employee_list;
            });


            //var url = 'api/customer/customer';
            //SocketService.get(url).then(function (resp) {
            //    $scope.customer_list = resp.data.customer_list;
            //});



            lockUI();
            $scope.loan_gid = localStorage.getItem('loan_gid');
            var url = 'api/loan/getLoandetails';
            var param = {
                loan_gid: $scope.loan_gid
            };


            SocketService.getparams(url, param).then(function (resp) {
                $scope.customer_gid = resp.data.customer_gid;
                var customer = resp.data.customerName.split("/");
                $scope.customer = customer[1];
                $scope.vertical = resp.data.vertical_gid;
                $scope.loanRefNoedit = resp.data.loanRefNo;
                $scope.sanctionrefnoedit = resp.data.sanctionRefno;
                $scope.sanctionDateedit = resp.data.sanction_date;
                $scope.sanction_Date = resp.data.sanctionDate;
               
                //$scope.sanctionDateedit = Date.parse(resp.data.sanctionDate);
                //$scope.sanctionDateedit = resp.data.sanctionDate;
                $scope.loanmaster_gid = resp.data.loanmaster_gid;
                 
                $scope.zonalHead = resp.data.zonal_gid;
                $scope.businessHead = resp.data.businesshead_gid;
                $scope.clustermanager = resp.data.clustermanagerGid;
                $scope.relationshipMgmt = resp.data.relationshipmgmt_gid;
                $scope.creditmgmt_name = resp.data.creditmanager_gid;

                unlockUI();
                //.log(resp.data);

                var params = {
                    customer_gid: resp.data.customer_gid
                }
                var url = 'api/loan/customer_getheads';

                SocketService.getparams(url, params).then(function (resp) {
                    $scope.mdlheadsofcustomer = resp.data;
                    $scope.sanctiondtl = resp.data.sanctiondtl;

                });
                $scope.cbosanctionGid = resp.data.sanction_Gid;

                var params = {
                    customer2sanction_gid: $scope.cbosanctionGid
                }
                var url = 'api/loan/SanctionLoanFacility';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {

                        $scope.loan_list = resp.data.loanfacility;

                    }

                });

                //$scope.loan = $scope.loanmaster_gid;

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

            var url = 'api/loan/customer_getheads';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.mdlheadsofcustomer = resp.data;
                $scope.zonalHead = resp.data.zonalGid;
                $scope.businessHead = resp.data.businessHeadGid;
                $scope.clustermanager = resp.data.clustermanagerGid;
                $scope.relationshipMgmt = resp.data.relationshipMgmtGid;
                $scope.creditmgmt_gid = resp.data.creditmanager_gid;
                $scope.vertical_code = resp.data.vertical_code;
                $scope.creditmgmt_name = resp.data.creditmanager_gid;
                $scope.sanctiondtl = resp.data.sanctiondtl;
                $scope.loan_list = {};
                $scope.vertical = true;

            });
        }


        $scope.sanctionrefnochange = function (cbosanctionGid) {
            var params = {
                sanction_gid: cbosanctionGid
            }

            var url = 'api/loan/GetSanctionDate';
            SocketService.getparams(url, params).then(function (resp) {

                $scope.sanctionDateedit = resp.data.sanctiondate;
                $scope.sanction_Date = resp.data.Sanction_Date;

            });
            $scope.loan_list = {};

            var params = {
                customer2sanction_gid: cbosanctionGid
            }
            var url = 'api/loan/SanctionLoanFacility';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                   
                    if (resp.data.count_loan == '1') {
                       
                        $scope.loan_list = resp.data.loanfacility;
                        $scope.loanmaster_gid = resp.data.loanfacility[0].facility_gid;
                    }
                    else {
                        $scope.loan_list = resp.data.loanfacility;
                    }
                   
                }
                else {
                    $scope.loan = '-----Select Loan Facility Type----';
                }

            });
        }


        $scope.onselectedchangecustomer = function (customer) {
            var params = {
                customer_gid: $scope.cbocustomergid.customer_gid
            }
            var url = 'api/loan/customer_getheads';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.mdlheadsofcustomer = resp.data;
                $scope.zonalHead = resp.data.zonalGid;
                $scope.businessHead = resp.data.businessHeadGid;
                $scope.clustermanager = resp.data.clustermanagerGid;
                $scope.relationshipMgmt = resp.data.relationshipMgmtGid;
                $scope.creditmgmt_name = resp.data.creditmanager_gid;
                $scope.sanctiondtl = resp.data.sanctiondtl;
                $scope.loan_list = {};

            });
        }

        $scope.loanback = function () {
            $state.go('app.loanManagement');
        }

        $scope.loanUpdate = function (val) {
            //var customername = $('#customername :selected').text();
            var vertical_code = $('#vertical_code :selected').text();
            var loanTitle = $('#loanTitle :selected').text();
            var sanctionrefno = $('#sanctionrefno :selected').text();
            var zonal_name = $('#zonal_name :selected').text();
            var businesshead_name = $('#businesshead_name :selected').text();
            var cluster_manager_name = $('#cluster_manager_name :selected').text();
            var relationshipmgmt_name = $('#relationshipmgmt_name :selected').text();
            var creditmgmt_name = $('#creditmanager_name :selected').text();
           

            var params = {
                loan_gid: $scope.loan_gid,
                customer_gid: $scope.customer_gid,
                customer_name: $scope.customer,
                vertical_gid: $scope.vertical,
                sanctionGid: $scope.cbosanctionGid,
                loanRefNoedit: $scope.loanRefNoedit,
                sanctionrefnoedit: sanctionrefno,
                sanctionDateedit: $scope.sanction_Date,
                loanmaster_gid: $scope.loanmaster_gid,
                loanTitleedit: loanTitle,
                vertical_code: vertical_code,
                zonalGid: $scope.zonalHead,
                businessHeadGid: $scope.businessHead,
                clustermanagerGid: $scope.clustermanager,
                relationshipMgmtGid: $scope.relationshipMgmt,
                creditmanager_name: creditmgmt_name,
                zonal_name: zonal_name,
                businesshead_name: businesshead_name,
                cluster_manager_name: cluster_manager_name,
                relationshipmgmt_name: relationshipmgmt_name,
                creditmanager_gid: $scope.creditmgmt_name,
            }
            console.log(params);

            var url = 'api/loan/loanUpdate';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                   
                    $state.go('app.loanManagement');
                    Notify.alert('Loan Updated Successfully..!!', 'success')
                }

                else {
                    Notify.alert(resp.data.message, 'warning')
                }

            });
        }

    }



})();



