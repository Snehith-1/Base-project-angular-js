(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnbankconfigurationeditController', brsTrnbankconfigurationeditController);

    brsTrnbankconfigurationeditController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','cmnfunctionService'];

    function brsTrnbankconfigurationeditController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
       /* var bankconfig_gid = $location.search().bankconfig_gid;*/
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var bankconfig_gid = searchObject.bankconfig_gid;

        vm.title = 'brsTrnbankconfigurationeditController';
        activate();


        function activate() {

            var url = 'api/MstAppCreditUnderWriting/BankNameList';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.bankdtl_list = resp.data.bankdtl_list;
            });
            var param = {
                bankconfig_gid: bankconfig_gid
            }
            var url = 'api/ConfigurationReconcillation/Editbankconfiguration';
            SocketService.getparams(url,param).then(function (resp) {
               
                $scope.txteditaccnorow = resp.data.acc_norow;
                $scope.txteditaccnocol = resp.data.acc_nocol;
                $scope.txteditdsrrow = resp.data.datastart_row;
                $scope.txtedittrndaterow = resp.data.trn_daterow;
                $scope.txtedittrndatecol = resp.data.trn_datecol;
                $scope.txteditvaldaterow = resp.data.value_daterow;
                $scope.txteditvaldatecol = resp.data.value_datecol;
                $scope.txteditpaydaterow = resp.data.payment_daterow;
                $scope.txteditpaydatecol = resp.data.payment_datecol;
                $scope.txtedittranscparticularrow = resp.data.transact_particularsrow;
                $scope.txtedittranscparticularcol = resp.data.transact_particularscol;
                $scope.txteditdebamtrow = resp.data.debit_amtrow;
                $scope.txteditdebamtcol = resp.data.debit_amtcol;
                $scope.txteditcreditamtrow = resp.data.credit_amtrow;
                $scope.txteditcreditamtcol = resp.data.credit_amtcol;
                $scope.txteditcrdrrow = resp.data.cr_drrow;
                $scope.txteditcrdrcol = resp.data.cr_drcol;
                $scope.txtedittrnvalrow = resp.data.transact_valrow;
                $scope.txtedittrnvalcol = resp.data.transact_valcol;
                $scope.txteditchqnorow = resp.data.chq_norow;
                $scope.txteditchqnocol = resp.data.chq_nocol;
                $scope.txteditbranchrow = resp.data.branch_namerow;
                $scope.txteditbranchcol = resp.data.branch_namecol;
                $scope.txteditcustrefnorow = resp.data.custref_norow;
                $scope.txteditcustrefnocol = resp.data.custref_nocol;
                $scope.txteditbalancerow = resp.data.balance_amtrow;
                $scope.txteditbalancecol = resp.data.balance_amtcol;
                $scope.txtedittrnidrow = resp.data.transc_idrow;
                $scope.txedittrnidcol = resp.data.transc_idcol;
                $scope.txtBankName = resp.data.brsbank_gid;
                $scope.txteditknockofffinrow = resp.data.knockoffby_financerow;
                $scope.txteditknockofffincol = resp.data.knockoffby_financecol;

            });
            unlockUI();


        }

        $scope.updateconfiguration = function () {
            var lsbankdtl_name = '';
            var lsacc_no = $scope.txteditaccnorow + ',' + $scope.txteditaccnocol;
            var lstrn_date = $scope.txtedittrndaterow + ',' + $scope.txtedittrndatecol;
            var lsvalue_date = $scope.txteditvaldaterow + ',' + $scope.txteditvaldatecol;
            var lspayment_date = $scope.txteditpaydaterow + ',' + $scope.txteditpaydatecol;
            var lstransact_particulars = $scope.txtedittranscparticularrow + ',' + $scope.txtedittranscparticularcol;
            var lsdebit_amt = $scope.txteditdebamtrow + ',' + $scope.txteditdebamtcol;
            var lscredit_amt = $scope.txteditcreditamtrow + ',' + $scope.txteditcreditamtcol;
            var lscr_dr = $scope.txteditcrdrrow + ',' + $scope.txteditcrdrcol;
            var lstransact_val = $scope.txtedittrnvalrow + ',' + $scope.txtedittrnvalcol;
            var lschq_no = $scope.txteditchqnorow + ',' + $scope.txteditchqnocol;
            var lsbranch_name = $scope.txteditbranchrow + ',' + $scope.txteditbranchcol;
            var lscustref_no = $scope.txteditcustrefnorow + ',' + $scope.txteditcustrefnocol;
            var lsbalance_amt = $scope.txteditbalancerow + ',' + $scope.txteditbalancecol;
            var lscr_dr = $scope.txteditcrdrrow + ',' + $scope.txteditcrdrcol;
            var lstransc_id = $scope.txtedittrnidrow + ',' + $scope.txedittrnidcol;
            var lsknockofffin = $scope.txteditknockofffinrow + ',' + $scope.txteditknockofffincol;
            lsbankdtl_name = $('#bankdetailname :selected').text();
            lockUI();
            var url = 'api/ConfigurationReconcillation/Updatebankconfiguration';

            var params = {
                bankconfig_gid: bankconfig_gid,
                brsbank_gid: $scope.txtBankName,
                bank_name: lsbankdtl_name,
                datastart_row: $scope.txteditdsrrow,
                acc_no: lsacc_no,
                trn_date: lstrn_date,
                value_date: lsvalue_date,
                payment_date: lspayment_date,
                transact_particulars: lstransact_particulars,
                debit_amt: lsdebit_amt,
                credit_amt: lscredit_amt,
                cr_dr: lscr_dr,
                transact_val: lstransact_val,
                chq_no: lschq_no,
                branch_name: lsbranch_name,
                custref_no: lscustref_no,
                balance_amt: lsbalance_amt,
                transc_id: lstransc_id,
                knockoffby_finance: lsknockofffin

            }           
           
            SocketService.post(url,params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    
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


        $scope.deletebank = function (bank_gid) {
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
                    var url = 'api/BankReconcillation/DeleteBank';
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