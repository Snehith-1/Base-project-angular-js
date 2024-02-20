(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCustomercreationlmsurnupdationController', MstCustomercreationlmsurnupdationController);

    MstCustomercreationlmsurnupdationController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService'];

    function MstCustomercreationlmsurnupdationController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCustomercreationlmsurnupdationController';
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.cuclms_gid = $location.search().cuclms_gid;
        var cuclms_gid = $scope.cuclms_gid;
        $scope.showlmsdiv = false;

        //$scope.selectallmenu_show = true;

        lockUI();
        activate();

        function activate() {

            if ((lspage == 'CADCustomerURNupdateview') || (lspage == 'CADCustomerURNrejectview')){
                $scope.showlmsdiv = true;
            } 
            else {$scope.showlmsdiv = false;}

            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/MstCADApplication/GetCustomerCreateLMSView';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.customername = resp.data.customername;
                $scope.address1 = resp.data.address1;
                $scope.address2 = resp.data.address2;
                $scope.state = resp.data.state;
                $scope.city = resp.data.city;
                $scope.country = resp.data.country;
                $scope.sector = resp.data.sector;
                // $scope.RMName = resp.data.RMName;
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
                $scope.RMName = resp.data.rm_name
            });
            if (lspage == 'CADCustomerURNupdateLMS') {
                var params = {
                    application_gid: $scope.application_gid
                }
                var url = 'api/MstCADApplication/Getgstbankaccnodetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.Gst = resp.data.Gst;
                    $scope.bankaccno = resp.data.bankaccount_number;
                    $scope.reject_flag = resp.data.reject_flag;
                    $scope.update_flag = resp.data.update_flag;
                    $scope.lms_status = resp.data.lms_status;
                    $scope.encorefindcust_status = resp.data.encorefindcust_status;

                    var param = {
                        bankaccount_number: $scope.bankaccno
                    }
                    var url = 'api/MstCADApplication/GetCustomerCreatebankdetails';
                    lockUI();
                    SocketService.getparams(url, param).then(function (resp) {
                        unlockUI();
                        $scope.ifsc_code = resp.data.ifsc_code;
                        $scope.bank_name = resp.data.bank_name;
                        $scope.branch_name = resp.data.branch_name;
                        $scope.accountholder_name = resp.data.accountholder_name;
                    });
                    unlockUI();
                });
            }
            else if (lspage == 'CADCustomerURNrejectview') {
                var params = {
                    application_gid: $scope.application_gid
                }
                var url = 'api/MstCADApplication/Getgstbankaccnodetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.Gst = resp.data.Gst;
                    $scope.bankaccno = resp.data.bankaccount_number;
                    $scope.reject_flag = resp.data.reject_flag;
                    $scope.update_flag = resp.data.update_flag;
                    var param = {
                        bankaccount_number: $scope.bankaccno
                    }
                    var url = 'api/MstCADApplication/GetCustomerCreatebankdetails';
                    lockUI();
                    SocketService.getparams(url, param).then(function (resp) {
                        unlockUI();
                        $scope.ifsc_code = resp.data.ifsc_code;
                        $scope.bank_name = resp.data.bank_name;
                        $scope.branch_name = resp.data.branch_name;
                        $scope.accountholder_name = resp.data.accountholder_name;
                    });
                    unlockUI();
                });
            }
            else if (lspage == 'CADCustomerURNupdateview') {
                var params = {
                    application_gid: $scope.application_gid
                }
                var url = 'api/MstCADApplication/Getgstbankaccnodetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.Gst = resp.data.Gst;
                    $scope.bankaccno = resp.data.bankaccount_number;
                    $scope.reject_flag = resp.data.reject_flag;
                    $scope.update_flag = resp.data.update_flag;
                    var param = {
                        bankaccount_number: $scope.bankaccno
                    }
                    var url = 'api/MstCADApplication/GetCustomerCreatebankdetails';
                    lockUI();
                    SocketService.getparams(url, param).then(function (resp) {
                        unlockUI();
                        $scope.ifsc_code = resp.data.ifsc_code;
                        $scope.bank_name = resp.data.bank_name;
                        $scope.branch_name = resp.data.branch_name;
                        $scope.accountholder_name = resp.data.accountholder_name;
                    });
                    unlockUI();
                });
            }
         
            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/MstCADApplication/GetCustomerUpdatingSummary';
            lockUI();
            SocketService.getparams(url,params).then(function (resp) {
                $scope.updated_list = resp.data.updated_list;
                unlockUI();
            });
            var url = 'api/MstCADApplication/GetCustomerRejectingSummary';
            lockUI();
            SocketService.getparams(url,params).then(function (resp) {
                $scope.rejected_list = resp.data.rejected_list;
                unlockUI();
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
       
        function updatereject() {
            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/MstCADApplication/Getgstbankaccnodetails';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {               
                $scope.reject_flag = resp.data.reject_flag;
                $scope.update_flag = resp.data.update_flag;
            });
        }

        $scope.postcreatecustomertoencore = function () {

            var params = {

                application_gid: $location.search().application_gid,
   
            }
            var url = 'api/SamFinEncoreCustomer/PostCreateCustomerEncore';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000

                    });
                   
                    var params = {

                        application_gid: $location.search().application_gid,
                    }
                    var url = 'api/MstCADApplication/GetCustomerUpdatingSummary';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.updated_list = resp.data.updated_list;
                        unlockUI();
                    });
                    activate();
                    $location.url('app/MstCustomercreationlmsurnupdation?cuclms_gid=' + $location.search().cuclms_gid + '&application_gid=' + $location.search().application_gid + '&lspage=' + $location.search().lspage);
                    updatereject();

                }
                else {
                   
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                unlockUI();
                $modalInstance.close('closed');
                

            });

        }

        $scope.postfindcustomer_encore = function () {

            var params = {

                application_gid: $location.search().application_gid,

            }
            var url = 'api/SamFinEncoreCustomer/PostFindCustomerEncoreApplicant';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000

                    });

                    var params = {

                        application_gid: $location.search().application_gid,
                    }
                    var url = 'api/MstCADApplication/GetCustomerUpdatingSummary';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.updated_list = resp.data.updated_list;
                        unlockUI();
                    });
                    activate();
                    $location.url('app/MstCustomercreationlmsurnupdation?cuclms_gid=' + $location.search().cuclms_gid + '&application_gid=' + $location.search().application_gid + '&lspage=' + $location.search().lspage);
                    updatereject();

                }
                else {

                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });

                    var params = {

                        application_gid: $location.search().application_gid,
                    }
                    activate();
                    $location.url('app/MstCustomercreationlmsurnupdation?cuclms_gid=' + $location.search().cuclms_gid + '&application_gid=' + $location.search().application_gid + '&lspage=' + $location.search().lspage);
                    updatereject();
                }
                unlockUI();
                $modalInstance.close('closed');


            });

        }
       
        $scope.back = function () {

            if ($scope.lspage == 'CADCustomerURNupdateLMS') {
                $location.url('app/MstCustomerCreationRequestSummary');
            }
            else if ($scope.lspage == 'CADCustomerURNupdateview') {
                $location.url('app/MstCustomerUpdatedRequestSummary');
            }
            else {
                $location.url('app/MstCustomerRejectedRequestSummary');
            }
           
        } 

        $scope.update_urn = function () {
            var modalInstance = $modal.open({
                templateUrl: '/Urnupdation.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {                

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_urnlms = function () {

                    var params ={
                        
                        application_gid: $location.search().application_gid,
                        customer_urn: $scope.txt_urn
                    }
                    var url = 'api/MstCADApplication/PostCustomerURN';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000

                            });

                           
                            $modalInstance.close('closed');
                            var params = {

                                application_gid: $location.search().application_gid,
                            }
                            var url = 'api/MstCADApplication/GetCustomerUpdatingSummary';
                            lockUI();
                            SocketService.getparams(url,params).then(function (resp) {
                                $scope.updated_list = resp.data.updated_list;
                                unlockUI();
                            });
                            activate();
                            $location.url('app/MstCustomercreationlmsurnupdation?cuclms_gid=' + $location.search().cuclms_gid + '&application_gid=' + $location.search().application_gid + '&lspage=' +$location.search().lspage);
                            updatereject();

                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        unlockUI();

                    });

                }
              
            }
        }

        $scope.reject = function () {
            var modalInstance = $modal.open({
                templateUrl: '/rejectedlms.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.rejected_lms = function () {

                    var params = {

                        application_gid: $location.search().application_gid,
                        rejected_remarks: $scope.txtremarks
                    }
                    var url = 'api/MstCADApplication/PostCustomerlmsreject';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000

                            });
                            
                            $modalInstance.close('closed');
                            var params = {

                                application_gid: $location.search().application_gid,
                                
                            }
                            var url = 'api/MstCADApplication/GetCustomerRejectingSummary';
                            lockUI();
                            SocketService.getparams(url,params).then(function (resp) {
                                $scope.rejected_list = resp.data.rejected_list;
                                unlockUI();
                            });
                            activate();
                            $location.url('app/MstCustomercreationlmsurnupdation?cuclms_gid=' + $location.search().cuclms_gid + '&application_gid=' + $location.search().application_gid + '&lspage=' +$location.search().lspage);
                            updatereject();

                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        unlockUI();

                    });

                }


            }
        }

        $scope.lmsreport1 = function () {
            lockUI();
            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/MstCADApplication/GetExportLmsReport1';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);

                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'success')

                }

            });
        }
        $scope.lmsreport2 = function () {
            lockUI();
            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/MstCADApplication/GetExportLmsReport2';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);

                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'success')

                }

            });
        }
    }
})();
