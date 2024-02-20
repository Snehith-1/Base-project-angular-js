// JavaScript source code
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnUnreconcillationTagController', brsTrnUnreconcillationTagController);

    brsTrnUnreconcillationTagController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function brsTrnUnreconcillationTagController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {

        var vm = this;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.lspage = searchObject.lspage;
        var lspage = $scope.lspage;
        vm.title = 'brsTrnUnreconcillationTagController';

        //var banktransc_gid = $location.search().banktransc_gid;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var banktransc_gid = searchObject.banktransc_gid;
        activate();
        function activate() {


            var url = 'api/UnreconciliationManagement/GetAllocatedDetail';
            var param = {
                banktransc_gid: banktransc_gid

            }

            SocketService.getparams(url, param).then(function (resp) {
                $scope.banktransc_gid = banktransc_gid;
                $scope.banktransc_refno = resp.data.banktransc_refno;
                $scope.lblbank_name = resp.data.bank_name;
                $scope.lblcustomer_refno = resp.data.custref_no;
                $scope.lblbranch_name = resp.data.branch_name;
                $scope.lblcr_dr = resp.data.cr_dr;
                $scope.lbltransc_value = resp.data.transact_val;
                $scope.lblremarks = resp.data.remarks;
                $scope.lbltransc_balance = resp.data.transc_balance;
                $scope.lblacc_no = resp.data.acc_no;
                $scope.lbltrn_date = resp.data.trn_date;
                $scope.lbltransact_particulars = resp.data.transact_particulars;
                $scope.lblchq_no = resp.data.chq_no;
                $scope.lblremainingamount = resp.data.remaining_amount;
                $scope.brstransactiondetails_flag = resp.data.brstransactiondetails_flag;
                $scope.brstransactiondetailsadvice_flag = resp.data.brstransactiondetailsadvice_flag;

                if ($scope.lblremainingamount == "0.00")
                {
                   
                    var modalInstance = $modal.open({
                        templateUrl: '/warningmessage.html',
                        controller: ModalInstanceCtrl,
                        backdrop: 'static',
                        keyboard: false,
                        size: 'lg'
                    });

                    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                    function ModalInstanceCtrl($scope, $modalInstance) {
                        $scope.ok = function () {
                            $modalInstance.close('closed');
                        };


                    }
                }

            });
            var url = 'api/employee/Employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });
            //var url = 'api/employee/Employee';
            //SocketService.get(url).then(function (resp) {
            //    $scope.employee_list1 = resp.data.employee_list;
            //});
            var url = 'api/BRSMaster/GetBRSActivityStatus';
            SocketService.get(url).then(function (resp) {
                $scope.BRSActivity_List = resp.data.BRSActivity_List;
            });
            var url = 'api/UnreconciliationManagement/GetAdjustAdviceEmployeeWiseShow';
            SocketService.get(url).then(function (resp) {
                $scope.adjustadvicelist = resp.data.adjustadvicelist;
            });
            var url = 'api/UnreconciliationManagement/GetUnreconTransactionList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.unrecontransactionlist = resp.data.unrecontransactionlist;

            });
            var url = 'api/UnreconciliationManagement/GetDepartmentName';
            SocketService.get(url).then(function (resp) {
                $scope.department_name = resp.data.department_name;
                $scope.employeename = resp.data.employee_name;

            });
            var url = 'api/UnreconciliationManagement/GetSamfinCustomerSummary';
            SocketService.get(url).then(function (resp) {
                $scope.assignedlist = resp.data.assignedlist;

            });

            
        }
        $scope.Back = function () {
            if (lspage == "Credit") {
                $location.url('app/brsTrnUnreconCreditSummaryManagement');
                //$location.url('app\brsTrnUnReconCreditSummaryManagement');
            }
            else if (lspage == "Debit") {
                /* $state.go('app.brsTrnUnReconDebitSummaryManagement');*/
                $location.url('app/brsTrnUnreconDebitSummaryManagement');
            }

        }
        $scope.Submit = function () {

            var params =
            {
                banktransc_gid: banktransc_gid,            
                assigned_to: $scope.cboemployee.employee_gid,
                assigned_toname: $scope.cboemployee.employee_name,
                assigned_remarks: $scope.txtremarks,
            }
            var url = 'api/UnreconciliationManagement/Post2Assign';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    if (lspage == "Credit") {
                        $state.go('app.brsTrnUnreconCreditSummaryManagement');
                    }
                    else if (lspage == "Debit") {
                        $state.go('app.brsTrnUnreconDebitSummaryManagement');
                    }
                   
                }
                else {
                   
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                unlockUI();
            });

        }
        $scope.addtransaction = function () {
            var lsbrsactivityname = '';
            var lsbrsactivitygid = '';
            var lssamfincustomer_name = '';
            var lssamfincustomer_gid = '';
            var lsadjustadvicename = '';
            var lsadjustadvicegid = '';
            if ($scope.cbosamfin != undefined || $scope.cbosamfin != null) {
                lssamfincustomer_gid = $scope.cbosamfin.samfincustomer_gid;
                lssamfincustomer_name = $scope.cbosamfin.samfincustomer_name;
            }

           
            lsbrsactivityname = $('#brsactivity :selected').text();
            lsbrsactivitygid = $scope.cbobrsactivity;
            lsadjustadvicename = $('#adjustadvice :selected').text();
            lsadjustadvicegid = $scope.cboaction;

            if ((($scope.cbobrsactivity == '' || $scope.cbobrsactivity == null) || ($scope.cboaction == '' || $scope.cboaction == null) || ($scope.txt_amount == '' || $scope.txt_amount == null) || ($scope.transactionremarks == '' || $scope.transactionremarks == null))) {
                Notify.alert('Kindly Fill Transaction Details', 'warning')
            }
            else {
                var params =
                {
                    banktransc_gid: banktransc_gid,
                    //assignby_gid: $scope.cboemployee1.employee_gid,
                    assignby_name: $scope.employeename,
                    activity_name: lsbrsactivityname,
                    activity_gid: lsbrsactivitygid,
                    samfincustomer_gid: lssamfincustomer_gid,
                    samfincustomer_name: lssamfincustomer_name,
                    action_name: lsadjustadvicename,
                    department_name: $scope.department_name,
                    amount: $scope.txt_amount,
                    transaction_remarks: $scope.transactionremarks,
                }
                var url = 'api/UnreconciliationManagement/PostUnreconTransactionDetails';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();

                    if (resp.data.status == true)
                    {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        //if (lspage == "Credit") {
                        //    $state.go('app.brsTrnUnreconCreditSummaryManagement');
                        //}
                        //else if (lspage == "Debit") {
                        //    $state.go('app.brsTrnUnreconDebitSummaryManagement');
                        //}
                        unrecontransaction_list();

                        activate();
                    }
                    else
                    {

                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    $scope.cbosamfin = '';
                    $scope.cbobrsactivity = '';
                    $scope.cboaction = '';
                    $scope.txt_amount = '';
                    $scope.transactionremarks = '';
                    activate();
                });
            }
        }
        function unrecontransaction_list()
        {
            var params = {
                banktransc_gid: banktransc_gid
            }
            var url = 'api/UnreconciliationManagement/GetUnreconTransactionList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.unrecontransactionlist = resp.data.unrecontransactionlist;

            });
        }

        
        $scope.remarks = function (transaction_remarks) {
            var modalInstance = $modal.open({
                templateUrl: '/remarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.transaction_remarks = transaction_remarks;

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.overallSubmit = function () {

            var params =
            {
                banktransc_gid: banktransc_gid,
                cbounreconciliation_status: $scope.cborm_status,
                /*brs_status: $scope.brs_status,*/
                updation_remarks: $scope.txtremarks

            }
            var url = 'api/UnreconciliationManagement/UnconPendingStatusUpdation';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();

                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $location.url('app/brsTrnUnreconCreditSummaryManagement');

                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

            });

        }
        $scope.deletetransaction = function (unrecontransactiondetails_gid, banktransc_gid) {
            var params = {
                unrecontransactiondetails_gid: unrecontransactiondetails_gid,
                banktransc_gid: banktransc_gid

            }
            var url = 'api/UnreconciliationManagement/GetUnreconTransactionDelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
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
                unrecontransaction_list();
                activate();
            });
        }


        
        $scope.limit_amountChange = function () {
            var input = document.getElementById('limit_amount').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_creditamount = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txt_amount = '';
            }
            else {
                $scope.txt_amount = output;
                document.getElementById('words_limitamt').innerHTML = lswords_creditamount;
            }
        } 
    }
    }) ();