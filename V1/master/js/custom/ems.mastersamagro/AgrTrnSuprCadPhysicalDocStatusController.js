(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnSuprCadPhysicalDocStatusController', AgrTrnSuprCadPhysicalDocStatusController);

        AgrTrnSuprCadPhysicalDocStatusController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AgrTrnSuprCadPhysicalDocStatusController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnSuprCadPhysicalDocStatusController';
        var application_gid = $location.search().application_gid;
        var lspage = $location.search().lspage;
        var credit_gid = $location.search().credit_gid;
        var lstype = $location.search().lstype;
        var lsdocumentcheckdtl_gid = $location.search().lsdocumentcheckdtl_gid;
        var processtypeassign_gid = $location.search().processtypeassign_gid;
        var lspath = $location.search().lspath;
        var lsdeferraltag = $location.search().lsdeferraltag;

        activate();
        function activate() {
            $scope.errormsg = "";
            $scope.showerrordiv = false;
            vm.calender6 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open6 = true;
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            var param = {
                documentcheckdtl_gid: lsdocumentcheckdtl_gid
            };
            var url = 'api/AgrTrnSuprPhysicalDocument/GetInitiatedExtensionorwaiver'; 
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.intiatextension_list = resp.data.mdlinitiateextendwaiver;
                unlockUI();
            });

            var url = 'api/SystemMaster/GetEmployeelist';
            SocketService.get(url).then(function (resp) {
                $scope.cboemployee_list = resp.data.employeelist;
                unlockUI();
            });

            $scope.hideeditevent = true;
            if (lspage == "CadPhysicalCompleted") {
                $scope.hideeditevent = false;
            } 
        }

        $scope.change_activity = function (cboactivitytype) {
            $scope.errormsg = "";
            $scope.showerrordiv = false;
            $scope.requireddatepicker = false;
            if ($scope.cboactivitytype == 'Extension' && lsdeferraltag == "") {
                $scope.errormsg = "Deferral is not tagged";
                $scope.txttitle = '';
                $scope.txtextendeddue_date = '';
                $scope.txtreason = '';
                $scope.cboapprovalperson = '';
                $scope.showerrordiv = true;
            }
            else if ($scope.cboactivitytype == 'Extension') {
                $scope.extensionshow = true;
                $scope.waivershow = true;
                $scope.closeshow = false;
                $scope.requireddatepicker = true;
            }
            else if ($scope.cboactivitytype == 'Waiver') {
                $scope.extensionshow = true;
                $scope.waivershow = false;
                $scope.closeshow = false;
            }
            else if ($scope.cboactivitytype == 'Close') {
                $scope.closeshow = true;
                $scope.waivershow = false;
                $scope.extensionshow = false;
            }
            else {
                $scope.extensionshow = false;
                $scope.waivershow = false;
                $scope.closeshow = false;
            }
        }

        $scope.intiatextension_submit = function () {
            if ($scope.cboactivitytype === "Extension" && $scope.txtextendeddue_date == "") {
                Notify.alert('Kindly Choose Extended Due Date !', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
            }
            else if ($scope.cboactivitytype === "Waiver" && $scope.cboapprovalperson.length == undefined) {
                Notify.alert('Kindly select Approval person !', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
            }
            else {

                var approval_status = $scope.cboapprovalperson == undefined ? 'No Approval' : 'Pending';
                var params = {
                    activity_type: $scope.cboactivitytype,
                    activity_title: $scope.txttitle,
                    extendeddue_date: $scope.txtextendeddue_date,
                    reason: $scope.txtreason,
                    application_gid: application_gid,
                    documentcheckdtl_gid: lsdocumentcheckdtl_gid,
                    mdlapproval: $scope.cboapprovalperson,
                    approval_status: approval_status
                }
                var url = 'api/AgrTrnSuprPhysicalDocument/PostPhysicalInitiateExtensionorwaiver';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        activate();
                        $scope.Cancel();
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                });
            }
        }

        $scope.Cancel = function () {
            $scope.cboactivitytype = '';
            $scope.txttitle = '';
            $scope.txtextendeddue_date = '';
            $scope.txtreason = '';
            $scope.cboapprovalperson = '';
        }

        $scope.reasonapproval_view = function (reason, approval_status, initiateextendorwaiver_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/remarksandreasondtl.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.lblreason = reason;
                $scope.lblapproval_status = approval_status;
                if (approval_status != 'No Approval') {
                    $scope.lblapproval_status = '';
                    var params = {
                        initiateextendorwaiver_gid: initiateextendorwaiver_gid
                    }
                    var url = 'api/AgrMstSuprScannedDocument/GetApprovalExtensionwaiver';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.approvallist = resp.data.mdlapprovaldtl;

                    });
                }


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }

        }

        $scope.Back = function () {
            if (lspage == "RMDocChecklist") {
                $location.url('app/MstRMDeferralDtlsView?application_gid=' + application_gid + '&credit_gid=' + credit_gid + '&lspage=' + lspage + '&lstype=' + lstype);
            }
            else {
                $location.url('app/AgrTrnSuprCadPhysicalDochecklist?application_gid=' + application_gid + '&credit_gid=' + credit_gid + '&lspage=' + lspage + '&lstype=' + lstype + '&processtypeassign_gid=' + processtypeassign_gid + '&lspath=' + lspath);
            }
        } 
    }
})();
