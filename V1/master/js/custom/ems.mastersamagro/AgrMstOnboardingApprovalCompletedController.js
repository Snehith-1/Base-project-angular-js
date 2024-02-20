(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstOnboardingApprovalCompletedController', AgrMstOnboardingApprovalCompletedController);

    AgrMstOnboardingApprovalCompletedController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function AgrMstOnboardingApprovalCompletedController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstOnboardingApprovalCompletedController';

        const lsdynamiclimitmanagementback = 'AgrMstOnboardingApprovalCompleted';

        activate();

        function activate() { }


        $scope.GetBuyeronboardingApprovedlist = function () {
            getApprovalCount();
            lockUI();
            var url = 'api/AgrMstBuyerOnboard/GetBuyerOnboardApprovedSummary';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.buyerapplicationlist = resp.data.onboardapplicationdtl;
                }
                else unlockUI();
            });
        }

        $scope.GetsupplieronboardingApprovedlist = function () {
            getApprovalCount();
            lockUI();
            var url = 'api/AgrMstBuyerOnboard/GetSupplierOnboardApprovedSummary';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.supplierapplicationlist = resp.data.onboardapplicationdtl;
                }
                else unlockUI();
            });
        }

        function getApprovalCount() {
            lockUI();
            var url = 'api/AgrMstBuyerOnboard/GetApproverApprovedCountDetail';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.approvalcount = resp.data;
                }
                else unlockUI();
            });
        }

        $scope.applcreation_view = function (application_gid) {
            $location.url('app/AgrMstCustomerOnboardingApproval?hash=' + cmnfunctionService.encryptURL('application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsApp=Y&FromRM=N'));
        }

        $scope.suprapplcreation_view = function (application_gid) {
            $location.url('app/AgrMstSupplierOnboardingApproval?hash=' + cmnfunctionService.encryptURL('application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsApp=Y&FromRM=N'));
        }

        $scope.onboardappdetailinfo = function (onboard_gid) {
            $location.url('app/AgrMstOnboardingApplicationInfo?hash=' + cmnfunctionService.encryptURL('onboard_gid=' + onboard_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback +'&lsApp=Y&FromRM=N'));
        }

        $scope.transferRM = function (application_gid, application_no, customer_name) {
          
            var modalInstance = $modal.open({
                templateUrl: '/RMTransfer.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) { 
                var url = 'api/SystemMaster/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employeelist;
                    unlockUI();
                }); 
                var params = {
                    onboard_gid: application_gid,
                }
                var url = 'api/AgrMstBuyerOnboard/GetOnboardTranferRMLog';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.Rmtransferlist = resp.data.MdlRmtransferdtl; 
                        unlockUI();
                    } 
                });
                $scope.txtapplication_no = application_no;
                $scope.txtcustomer_name = customer_name;

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.submittransfer = function () {
                    lockUI();
                    var transferTo = {
                        onboard_gid: application_gid,
                        transferto_employeegid: $scope.cbotransferto.employee_gid,
                        transferto_employeename: $scope.cbotransferto.employee_name,
                        transfer_remarks: $scope.txttransfer_remarks
                    }
                    var url = "api/AgrMstBuyerOnboard/PostOnboardTranferRM";
                    SocketService.post(url, transferTo).then(function (resp) {
                        if (resp.data.status == true) {

                            var params = {
                                onboard_gid: application_gid, 
                            }
                            var url = 'api/AgrMstBuyerOnboard/GetOnboardTranferRMLog';
                            lockUI();
                            SocketService.getparams(url, params).then(function (resp) {
                                if (resp.data.status == true) {
                                    $scope.Rmtransferlist = resp.data.MdlRmtransferdtl; 
                                    unlockUI();
                                }
                            });
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                          
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
            }
        }

        //$scope.applcreation_edit = function (application_gid) {
        //    $location.url('app/AgrMstByrOnboardApprovalEdit?application_gid=' + application_gid + '&lsedit=OnCompleted');
        //}

        //$scope.suprapplcreation_edit = function (application_gid) {
        //    $location.url('app/AgrMstSuprOnboardApprovalEdit?application_gid=' + application_gid + '&lsedit=OnCompleted');
        //}

        $scope.applcreation_edit = function (application_gid) {
            $location.url('app/AgrMstByrOnboardApprovedEdit?hash=' + cmnfunctionService.encryptURL('application_gid=' + application_gid + '&lsedit=OnCompleted'));
        }

        $scope.suprapplcreation_edit = function (application_gid) {
            $location.url('app/AgrMstSuprOnboardApprovedEdit?hash=' + cmnfunctionService.encryptURL('application_gid=' + application_gid + '&lsedit=OnCompleted'));
        }


        $scope.BuyerOnboardExport = function () {
            lockUI();
            var url = 'api/AgrMstOnboardApprovalReport/ExportBuyerOnboardApproved';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                    //var phyPath = resp.data.lspath;
                    //var relPath = phyPath.split("EMS");
                    //var relpath1 = relPath[1].replace("\\", "/");
                    //var hosts = window.location.host;
                    //var prefix = location.protocol + "//";
                    //var str = prefix.concat(hosts, relpath1);
                    //var link = document.createElement("a");
                    //var name = resp.data.lsname.split('.');
                    //link.download = name[0];
                    //var uri = str;
                    //link.href = uri;
                    //link.click();
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'warning')
                    
                }

            });
        }

        $scope.SupplierOnboardExport = function () {
            lockUI();
            var url = 'api/AgrMstOnboardApprovalReport/ExportSupplierOnboardApproved';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                    //var phyPath = resp.data.lspath;
                    //var relPath = phyPath.split("EMS");
                    //var relpath1 = relPath[1].replace("\\", "/");
                    //var hosts = window.location.host;
                    //var prefix = location.protocol + "//";
                    //var str = prefix.concat(hosts, relpath1);
                    //var link = document.createElement("a");
                    //var name = resp.data.lsname.split('.');
                    //link.download = name[0];
                    //var uri = str;
                    //link.href = uri;
                    //link.click();
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'warning')
                    
                }

            });
        }

        $scope.lgl_tag = function (application_gid, application_no, customer_name) {

            var modalInstance = $modal.open({
                templateUrl: '/tagto.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    onboard_gid: application_gid,
                }
                var url = 'api/AgrMstBuyerOnboard/GetOnboardLgltagstatusLog';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.MdlOboardlglstatuslist = resp.data.MdlOboardlglstatuslist;
                        unlockUI();
                    }
                });
                $scope.txtapplication_no = application_no;
                $scope.txtcustomer_name = customer_name;

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.submittag = function () {
                    lockUI();
                    var params = {
                        onboard_gid: application_gid,
                        lgltag_status: $scope.txtlgltag,
                    }
                    var url = "api/AgrMstBuyerOnboard/PostOnboardlglstatus";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {

                            var params = {
                                onboard_gid: application_gid,
                            }
                            var url = 'api/AgrMstBuyerOnboard/GetOnboardLgltagstatusLog';
                            lockUI();
                            SocketService.getparams(url, params).then(function (resp) {
                                if (resp.data.status == true) {
                                    $scope.MdlOboardlglstatuslist = resp.data.MdlOboardlglstatuslist;
                                    unlockUI();
                                }
                            });
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            lgltaginsummary();

                            unlockUI();
                            $modalInstance.close('closed');
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                        }
                    });
                }
            }
        }


        function lgltaginsummary() {

            var url = 'api/AgrMstBuyerOnboard/GetBuyerOnboardApprovedSummary';
            SocketService.get(url).then(function (resp) {
                $scope.buyerapplicationlist = resp.data.onboardapplicationdtl;
            });
        }

        $scope.posttoerp_buyer = function (application_gid) {
            var params = {
                application_gid: application_gid
            }
            var url = 'api/SamAgroHBAPIConn/PostBuyerToERP';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {                   
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    GetBuyeronboardingApprovedlistERP();
                }
                else {
                    if(resp.data.error_response != null) {
                        var error_message = resp.data.message; 
                        error_message += " - NetSuite Response: " + resp.data.error_response;
                        Notify.alert(error_message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 10000
                        });
                    }
                    else{ 
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }                   
                }
                

            });
        }

        $scope.posttoerp_supplier = function (application_gid) {
            var params = {
                application_gid: application_gid
            }
            var url = 'api/SamAgroHBAPIConn/PostSupplierToERP';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {                   
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    GetSupplieronboardingApprovedlistERP();
                }
                else {
                    if(resp.data.error_response != null) {
                        var error_message = resp.data.message; 
                        error_message += " - NetSuite Response: " + resp.data.error_response;
                        Notify.alert(error_message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 10000
                        });
                    }
                    else{ 
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                }
                

            });
        }

        function GetBuyeronboardingApprovedlistERP() {
            getApprovalCount();
            lockUI();
            var url = 'api/AgrMstBuyerOnboard/GetBuyerOnboardApprovedSummary';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.buyerapplicationlist = resp.data.onboardapplicationdtl;
                }
                else unlockUI();
            });
        }

        function GetSupplieronboardingApprovedlistERP() {
            getApprovalCount();
            lockUI();
            var url = 'api/AgrMstBuyerOnboard/GetSupplierOnboardApprovedSummary';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.supplierapplicationlist = resp.data.onboardapplicationdtl;
                }
                else unlockUI();
            });
        }


        $scope.suprlgl_tag = function (application_gid, application_no, customer_name) {

            var modalInstance = $modal.open({
                templateUrl: '/suprtagto.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    onboard_gid: application_gid,
                }
                var url = 'api/AgrMstSupplierOnboard/GetOnboardLgltagstatusLog';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.MdlOboardlglstatuslist = resp.data.MdlOboardlglstatuslist;
                        unlockUI();
                    }
                });
                $scope.txtapplication_no = application_no;
                $scope.txtcustomer_name = customer_name;

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.submittag = function () {
                    lockUI();
                    var params = {
                        onboard_gid: application_gid,
                        lgltag_status: $scope.txtlgltag,
                    }
                    var url = "api/AgrMstSupplierOnboard/PostOnboardlglstatus";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {

                            var params = {
                                onboard_gid: application_gid,
                            }
                            var url = 'api/AgrMstSupplierOnboard/GetOnboardLgltagstatusLog';
                            lockUI();
                            SocketService.getparams(url, params).then(function (resp) {
                                if (resp.data.status == true) {
                                    $scope.MdlOboardlglstatuslist = resp.data.MdlOboardlglstatuslist;
                                    unlockUI();
                                }
                            });
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            suprlgltaginsummary();

                            unlockUI();
                            $modalInstance.close('closed');
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                        }
                    });
                }
            }
        }


        function suprlgltaginsummary() {

            var url = 'api/AgrMstBuyerOnboard/GetSupplierOnboardApprovedSummary';
            SocketService.get(url).then(function (resp) {
                $scope.supplierapplicationlist = resp.data.onboardapplicationdtl;
            });
        }

        $scope.suprtransferRM = function (application_gid, application_no, customer_name) {

            var modalInstance = $modal.open({
                templateUrl: '/SuprRMTransfer.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var url = 'api/SystemMaster/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employeelist;
                    unlockUI();
                });
                var params = {
                    onboard_gid: application_gid,
                }
                var url = 'api/AgrMstSupplierOnboard/GetOnboardTranferRMLog';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.Rmtransferlist = resp.data.MdlRmtransferdtl;
                        unlockUI();
                    }
                });
                $scope.txtapplication_no = application_no;
                $scope.txtcustomer_name = customer_name;

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.submittransfer = function () {
                    lockUI();
                    var transferTo = {
                        onboard_gid: application_gid,
                        transferto_employeegid: $scope.cbotransferto.employee_gid,
                        transferto_employeename: $scope.cbotransferto.employee_name,
                        transfer_remarks: $scope.txttransfer_remarks
                    }
                    var url = "api/AgrMstSupplierOnboard/PostOnboardTranferRM";
                    SocketService.post(url, transferTo).then(function (resp) {
                        if (resp.data.status == true) {

                            var params = {
                                onboard_gid: application_gid,
                            }
                            var url = 'api/AgrMstSupplierOnboard/GetOnboardTranferRMLog';
                            lockUI();
                            SocketService.getparams(url, params).then(function (resp) {
                                if (resp.data.status == true) {
                                    $scope.Rmtransferlist = resp.data.MdlRmtransferdtl;
                                    unlockUI();
                                }
                            });
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');

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
            }
        }

        $scope.create_virtualaccount = function (application_gid,application_no,virtualaccount_number) {
            var params = {
                application_gid: application_gid,
                application_no: application_no,
                virtualaccount_number: virtualaccount_number
            }
            var url = "api/AgrVirtualAccount/CreateVirtualAccount";
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    GetBuyeronboardingApprovedlistERP();
                    unlockUI();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    if (resp.data.message.toLowerCase().includes('duplicate'.toLowerCase())) { 
                        GetBuyeronboardingApprovedlistERP();
                    }                    
                    unlockUI();
                }
            });
        }


    }
})();
