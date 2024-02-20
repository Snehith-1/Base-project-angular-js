(function () {
    'use strict';

    angular
        .module('angle')
        .controller('customer2EscrowSummary', customer2EscrowSummary);

    customer2EscrowSummary.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout', 'SweetAlert'];

    function customer2EscrowSummary($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout, SweetAlert) {
        var vm = this;
        vm.title = 'customer2EscrowSummary';
        var customer_gid;
       
        activate();

        function activate() {
            $scope.IsCreate = false;
            $scope.IsView = false;
            customer_gid = localStorage.getItem('customer_gid');
            var params = {
                customer_gid: localStorage.getItem('customer_gid')
            }

            var url = "api/customerManagement/EscrowSummary";
            SocketService.getparams(url, params).then(function (resp) {
                if(resp.data.status==true)
                {
                    $scope.escrowlist = resp.data.escrowSummary;
                   
                }

            });

            var url = 'api/customer/Getcustomerdetails';

            lockUI();
            SocketService.getparams(url,params).then(function (resp) {

                if(resp.data.status==true)
                {
                    $scope.customerdetails = resp.data;
                    $scope.urn=resp.data.customer_urnedit;
                    $scope.customername = resp.data.customerNameedit;
                    $scope.customercode = resp.data. customerCodeedit;
                }
               
               
            });
            unlockUI();



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
                customer_gid: customer_gid
            }
            var url = 'api/loan/customer_getheads';

            SocketService.getparams(url, params).then(function (resp) {

                $scope.sanctiondtl = resp.data.sanctiondtl;
               
            });
        }

        $scope.sanctionrefnochange = function (sanction_Gid) {
            if ($scope.cbosanction.sanction_Gid == undefined)
            {

            }
            else {
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
             
           
           
        }

        $scope.escrowCreate=function()
        {
           $scope. IsCreate = true;
            //localStorage.setItem('customer_gid', customer_gid);
            //localStorage.setItem('urn', $scope.urn);
            //localStorage.setItem('customer_name', $scope.customername);
            //localStorage.setItem('customer_code', $scope.customercode);

            //$state.go('app.Customer2EscrowCreate');

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

        $scope.escrowSubmit = function () {

            var params = {
                sanction_gid: $scope.cbosanction.sanction_Gid,
                customer_gid: customer_gid,
                disbursement_date: $scope.txtdisbursementDate,
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
                    $scope.remarks = null;
                    $scope.cbosanction = '---Select---';
                    $scope.Sanction_Date = null;
                    $scope.facilitytype = null;
                    $scope.txtdisbursementDate = null;
                    $scope.transactionRefNo = null;
                    $scope.transactionDate = null;
                    $scope.dealername = '';
                    $scope.escrow_accountno = '';
                    $scope.master_accountno = '';
                    $scope.sendercustomer_accountname = '';
                    $scope.sendercustomer_accountno = '';
                    $scope.sendbranch_ifsc = '';
                    $scope.beneficiarycustomer_accountname = '';
                    $scope.reference = null;
                    $scope.remittance = '';
                    $scope.amount = '';
                    $scope.creditTime = '';
                    
                    Notify.alert(resp.data.message, 'success');
                    activate();
                   
                    //$state.go('app.Customer2EscrowSummary');
                   
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message)
                }
                activate();
            });

        }
        $scope.close=function()
        {
            $scope.IsCreate = false;
            this.remarks = null;
            this.cbosanction ='---Select---';
            this.Sanction_Date = null;
            this.facilitytype = null;
            this.txtdisbursementDate = null;
            this.transactionRefNo = null;
            this.transactionDate = null;
            this.dealername = '';
            this.escrow_accountno = '';
            this.master_accountno = '';
            this.sendercustomer_accountname = '';
            this.sendercustomer_accountno = '';
            this.sendbranch_ifsc = '';
            this.beneficiarycustomer_accountname = '';
            this.reference = null;
            this.remittance = '';
            this.amount = '';
            this.creditTime = '';
        }

        $scope.escrowView = function (escrow_gid)
        {
           
            $scope.IsView = true;
            var params = {
                escrow_gid: escrow_gid
            };
            var url = "api/customerManagement/EscrowView";
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.escrowview = resp.data;

                }


            });
           // localStorage.setItem('escrow_gid', escrow_gid);
            //$state.go('app.customer2EscrowView');
        }

        $scope.ViewClose=function()
        {
            $scope.IsView = false;
        }
        $scope.escrowDelete = function (val) {
            var params = {
                escrow_gid: val
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Escrow ?',

                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();
                    var url = "api/customerManagement/EscrowDelete";
                    SocketService.getparams(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            
                            SweetAlert.swal('Deleted Successfully!');
                            unlockUI();
                            activate();
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

            });
        }
    }
})();
