(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnbankconfigurationaddController', brsTrnbankconfigurationaddController);

    brsTrnbankconfigurationaddController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function brsTrnbankconfigurationaddController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnbankconfigurationaddController';
        activate();


        function activate() {

             var url = 'api/MstAppCreditUnderWriting/BankNameList';
                SocketService.get(url).then(function (resp) {
                    unlockUI();
                    $scope.bankdtl_list = resp.data.bankdtl_list;
                });
        }
      
        $scope.submitconfiguration = function () {
            var lsacc_no = $scope.txtaccnorow + ',' + $scope.txtaccnocol;
            var lstrn_date = $scope.txttrndaterow + ',' + $scope.txttrndatecol;
            var lsvalue_date = $scope.txtvaldaterow + ',' + $scope.txtvaldatecol;
            var lspayment_date = $scope.txtpaydaterow + ',' + $scope.txtpaydatecol;
            var lstransact_particulars = $scope.txttranscparticularrow + ',' + $scope.txttranscparticularcol;
            var lsdebit_amt = $scope.txtdebamtrow + ',' + $scope.txtdebamtcol;
            var lscredit_amt = $scope.txtcreditamtrow + ',' + $scope.txtcreditamtcol;
            var lscr_dr = $scope.txtcrdrrow + ',' + $scope.txtcrdrcol;
            var lstransact_val = $scope.txttrnvalrow + ',' + $scope.txttrnvalcol;
            var lschq_no = $scope.txtchqnorow + ',' + $scope.txtchqnocol;
            var lsbranch_name = $scope.txtbranchrow + ',' + $scope.txtbranchcol;
            var lscustref_no = $scope.txtcustrefnorow + ',' + $scope.txtcustrefnocol;
            var lsbalance_amt = $scope.txtbalancerow + ',' + $scope.txtbalancecol;
            var lscr_dr = $scope.txtcrdrrow + ',' + $scope.txtcrdrcol;
            var lstransc_id = $scope.txttrnidrow + ',' + $scope.txttrnidcol;
            var lsknockoffby_fin = $scope.txtknockofffinrow + ',' + $scope.txttknockofffincol;

            var params = {
                brsbank_gid: $scope.txtBankName.bankdtl_gid,               
                datastart_row: $scope.txtdsrrow,
                acc_no: lsacc_no,                
                trn_date: lstrn_date ,              
                value_date: lsvalue_date,               
                payment_date: lspayment_date,                
                transact_particulars: lstransact_particulars,              
                debit_amt: lsdebit_amt,                
                credit_amt: lscredit_amt,                
                cr_dr: lscr_dr,                
                transact_val:lstransact_val,               
                chq_no: lschq_no,               
                branch_name:lsbranch_name,             
                custref_no: lscustref_no,               
                balance_amt: lsbalance_amt,
                transc_id: lstransc_id,
                knockoffby_finance:lsknockoffby_fin
               
            }


            var url = "api/ConfigurationReconcillation/Addbankconfiguration"
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.brsTrnbankconfiguration');
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
        $scope.back = function () {
            $state.go('app.brsTrnbankconfiguration');
        }


        $scope.editbankfield = function (bank_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editbank.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    bank_gid: bank_gid
                }
                var url = 'api/BankReconcillation/EditBank';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditbank_name = resp.data.bank_name;
                    $scope.txteditacc_no = resp.data.acc_no;
                    $scope.txteditcustref_no = resp.data.custref_no;
                    $scope.txteditbranch = resp.data.branch_name;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.Updatebank = function () {

                    var url = 'api/BankReconcillation/UpdateBank';
                    var params = {
                        bank_name: $scope.txteditbank_name,
                        acc_no: $scope.txteditacc_no,
                        custref_no: $scope.txteditcustref_no,
                        branch_name: $scope.txteditbranch,
                        bank_gid: bank_gid
                    }
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });


                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }

                        activate();

                    });
                    $modalInstance.close('closed');
                }

            }
        }


        $scope.deleteconfiguration = function (bank_gid) {
            var params = {
                bank_gid: bank_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = 'api/ConfigurationReconcillation/DeleteConfigurationdata';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');

                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }

            });
        };
    }

})();