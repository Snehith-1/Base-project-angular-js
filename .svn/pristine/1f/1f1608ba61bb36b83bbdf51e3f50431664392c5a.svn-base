(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCADCustomerCreationLMSController', MstCADCustomerCreationLMSController);

    MstCADCustomerCreationLMSController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCADCustomerCreationLMSController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCADCustomerCreationLMSController';
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;

        //$scope.selectallmenu_show = true;

        lockUI();
        activate();

        function activate() {

            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/MstCADApplication/GetCustomerCreateLMSView';
            lockUI();
            SocketService.getparams(url,params).then(function (resp) {
                unlockUI();
                $scope.customername = resp.data.customername;
                $scope.address1 = resp.data.address1;
                $scope.address2 = resp.data.address2;
                $scope.state = resp.data.state;
                $scope.city = resp.data.city;
                $scope.country = resp.data.country;
                $scope.sector = resp.data.sector;
                $scope.RMName = resp.data.RMName;
                $scope.RMmailID = resp.data.RMmailID;
                $scope.mobilenumber = resp.data.mobilenumber;
                $scope.emailaddress = resp.data.emailaddress;
                $scope.Firstname = resp.data.Firstname;
                $scope.middlename = resp.data.middlename;
                $scope.lastname = resp.data.lastname;
                $scope.userid = resp.data.userid;
                $scope.pincode = resp.data.pincode;
                $scope.idproof_name = resp.data.idproof_name;
                $scope.idproof_no = resp.data.idproof_no;
                $scope.txtapplication_no = resp.data.application_no;

            });   
            var url = 'api/MstCADApplication/Getgstbankaccnodetails';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.reject_flag = resp.data.reject_flag;
                $scope.update_flag = resp.data.update_flag;
            });
            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/MstCADApplication/GetCustomerRejectingSummary';
            lockUI();
            SocketService.getparams(url,params).then(function (resp) {
                $scope.rejected_list = resp.data.rejected_list;
                unlockUI();
            });

            var url = 'api/MstCADApplication/Getgstno';
            lockUI();
            SocketService.getparams(url,params).then(function (resp) {
                $scope.gst_list = resp.data.gst_list;
                if ($scope.gst_list == '' || $scope.gst_list == null || $scope.gst_list == undefined) {
                    $scope.gst = true;
                }
                unlockUI();
            });
            var url = 'api/MstCADApplication/Getbankaccno';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.bankaccnoinstitution_list = resp.data.bankaccnoinstitution_list;
                $scope.bankaccnocontact_list = resp.data.bankaccnocontact_list;

                //if ($scope.bankaccnoinstitution_list != null || $scope.bankaccnoinstitution_list != '' || $scope.bankaccnoinstitution_list != undefined) {
                //    $scope.bankaccnoinstitution = true;
                //  $scope.bankaccnocontact = false;
                //}
                //else if ($scope.bankaccnocontact_list != null || $scope.bankaccnocontact_list != '' || $scope.bankaccnocontact_list != undefined) {
                //    $scope.bankaccnocontact = true;
                //    $scope.bankaccnoinstitution = false;
                //}
                ////if (($scope.bankaccnoinstitution_list != '' || $scope.bankaccnoinstitution_list != null || $scope.bankaccnoinstitution_list != undefined)) {
                //    $scope.bankaccnoinstitution = true;
                //    $scope.bankaccnocontact = false;
                //}
                //else if ($scope.bankaccnocontact_list != '' || $scope.bankaccnocontact_list != null || $scope.bankaccnocontact_list != undefined) {
                //    $scope.bankaccnocontact = true;
                //    $scope.bankaccnoinstitution = false;
                //}
                //else {
                //    $scope.bankaccno = false;
                //}

                             
                unlockUI();
            });
          

        }
        $scope.customerbankaccins_change = function (customer_bankacc) {
            for (var i = 0; i < $scope.bankaccnoinstitution_list.length; i++) {
                if (customer_bankacc == $scope.bankaccnoinstitution_list[i].application_gid)
                    $scope.selectinterest = $scope.bankaccnoinstitution_list[i].bankaccount_number.replace(" ", "").toLowerCase();
                var params = {
                    bankaccount_number: $scope.selectinterest
                }
                var url = 'api/MstCADApplication/GetCustomerCreatebankdetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.ifsc_code = resp.data.ifsc_code;
                    $scope.bank_name = resp.data.bank_name;
                    $scope.branch_name = resp.data.branch_name;
                    $scope.accountholder_name = resp.data.accountholder_name;                  
                });
                var params = {
                    application_gid: $scope.application_gid
                }
                var url = 'api/MstCADApplication/Getlogstatusdetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.lms_statuslog = resp.data.lms_statuslog;
                    unlockUI();
                });

            }
        }
        $scope.customergst_change = function (customer_gst) {
            for (var i = 0; i < $scope.gst_list.length; i++) {
                if (customer_gst == $scope.gst_list[i].application_gid)
                    $scope.selectinterest = $scope.gst_list[i].Gst.replace(" ", "").toLowerCase();
            }
        }
        $scope.customerbankacccont_change = function (customer_bankacc) {
            for (var i = 0; i < $scope.bankaccnocontact_list.length; i++) {
                if (customer_bankacc == $scope.bankaccnocontact_list[i].application_gid)
                    $scope.selectinterest = $scope.bankaccnocontact_list[i].bankaccount_number.replace(" ", "").toLowerCase();
                var params = {
                    bankaccount_number: $scope.selectinterest
                }
                var url = 'api/MstCADApplication/GetCustomerCreatebankdetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.ifsc_code = resp.data.ifsc_code;
                    $scope.bank_name = resp.data.bank_name;
                    $scope.branch_name = resp.data.branch_name;
                    $scope.accountholder_name = resp.data.accountholder_name;
                });
            }
        }
        $scope.Back = function () {
            $location.url('app/MstCadAcceptedCustomers');
        }
        $scope.urn_update = function () {
            $location.url('app/MstCustomerUpdatedRequestSummary');
        }
        $scope.rejected = function () {
            $location.url('app/MstCustomerRejectedRequestSummary');
        }
        

        $scope.Initiate = function () {
            lockUI();
            //if ($scope.customer_bankacc == undefined || $scope.customer_bankacc == null) {
            //    Notify.alert('Kindly Add Bank Account Deatils', 'warning');
            //}
            //else if ($scope.customer_gst == undefined || $scope.customer_gst == null) {
            //    Notify.alert('Kindly Add Gst Deatils', 'warning');
            //}

                var gst_name = $('#initiate_gst :selected').text();
                var bankacc_no = $('#initiate_bankacc :selected').text();
          

                var params = {
                    application_gid: $scope.application_gid,
                    Gst: gst_name,
                    bankaccount_number: bankacc_no


                }
                var url = 'api/MstCADApplication/PostCustomerCreationLMS';
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        activate();
                        $state.go('app.MstCadAcceptedCustomers');

                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                });

           
            unlockUI();
        }





    }
})();
