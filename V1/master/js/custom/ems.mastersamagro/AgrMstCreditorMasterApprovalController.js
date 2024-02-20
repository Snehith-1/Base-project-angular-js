(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstCreditorMasterApprovalController', AgrMstCreditorMasterApprovalController);

    AgrMstCreditorMasterApprovalController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'cmnfunctionService'];

    function AgrMstCreditorMasterApprovalController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstCreditorMasterApprovalController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        //lockUI();
        activate();
        function activate() {
            lockUI();
            $scope.showallbtn = true;
            $scope.showview = false;
            $scope.submit = true;
            $scope.Approved = false;
            $scope.reject = false;
            $('#approvalpendingtab').addClass('tabactivecolorstyle');
            var url = 'api/AgrMstCreditorMaster/GetApprovalCreditorCountDetail';
            SocketService.get(url).then(function (resp) {
                $scope.CreditorCount = resp.data;
            });
            ////var url = 'api/AgrMstWarehouseAdd/GetNewWarehouseSummary';
            //SocketService.get(url).then(function (resp) {
            //    unlockUI();
            //    $scope.warehouseadd_list = resp.data.MdlAgrMstWarehouseCreation;
            //});  

            var url = 'api/AgrMstCreditorMaster/GetCreditorApprovalPendingSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.creditoradd_list = resp.data.MdlcreditorCreation;
            });


        }

        $scope.pending = function () {
            $scope.showallbtn = true;
            $scope.showview = false;
            $scope.submit = true;
            $scope.Approved = false;
            $scope.reject = false;
            $('#approvalpendingtab').addClass('tabactivecolorstyle');
            $('#opentab').removeClass('tabactivecolorstyle');
            $('#approvedtab').removeClass('tabactivecolorstyle');
            $('#rejectedtab').removeClass('tabactivecolorstyle');
            lockUI();
            var url = 'api/AgrMstCreditorMaster/GetApprovalCreditorCountDetail';
            SocketService.get(url).then(function (resp) {
                $scope.CreditorCount = resp.data;
            });
            var url = 'api/AgrMstCreditorMaster/GetCreditorApprovalPendingSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.creditoradd_list = resp.data.MdlcreditorCreation;
            });

        }

        $scope.approved = function () {
            $scope.showallbtn = false;
            $scope.showview = true;

            $scope.submit = false;
            $scope.Approved = true;
            $scope.reject = false;
            $('#approvedtab').addClass('tabactivecolorstyle');
            $('#opentab').removeClass('tabactivecolorstyle');
            $('#approvalpendingtab').removeClass('tabactivecolorstyle');
            $('#rejectedtab').removeClass('tabactivecolorstyle');
            lockUI();
            var url = 'api/AgrMstCreditorMaster/GetApprovalCreditorCountDetail';
            SocketService.get(url).then(function (resp) {
                $scope.CreditorCount = resp.data;
            });
            var url = 'api/AgrMstCreditorMaster/GetCreditorApprovedSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.creditoradd_list = resp.data.MdlcreditorCreation;
            });

        }

        $scope.rejected = function () {
            $scope.showallbtn = false;
            $scope.showview = true;

            $scope.submit = false;
            $scope.Approved = false;
            $scope.reject = true;
            $('#rejectedtab').addClass('tabactivecolorstyle');
            $('#opentab').removeClass('tabactivecolorstyle');
            $('#approvalpendingtab').removeClass('tabactivecolorstyle');
            $('#approvedtab').removeClass('tabactivecolorstyle');
            lockUI();
            var url = 'api/AgrMstCreditorMaster/GetApprovalCreditorCountDetail';
            SocketService.get(url).then(function (resp) {
                $scope.CreditorCount = resp.data;
            });
            var url = 'api/AgrMstCreditorMaster/GetRejectedCreditorSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.creditoradd_list = resp.data.MdlcreditorCreation;
            });

        }



        $scope.creditor_Add = function () {
            $state.go('app.AgrMstCreditorMasterAdd');
        }

        $scope.creditor_view = function (creditor_gid) {
            $location.url('app/AgrMstCreditorMasterView?hash=' + cmnfunctionService.encryptURL('creditor_gid=' + creditor_gid + '&lspage=AppV'));
        }

        $scope.creditor_edit = function (creditor_gid) {
            $location.url('app/AgrMstCreditorMasterEdit?hash=' + cmnfunctionService.encryptURL('creditor_gid=' + creditor_gid + '&lspage=Approved'));
        }

        $scope.approval_view = function (creditor_gid) {
            $location.url('app/AgrMstCreditorMasterView?hash=' + cmnfunctionService.encryptURL('creditor_gid=' + creditor_gid + '&lspage=AppRej'));
        }

        $scope.posttoerp_othercreditor = function (creditor_gid) {
            var params = {
                creditor_gid: creditor_gid
            }
            var url = 'api/SamAgroHAPIOtherCreditor/PostOtherCreditorAddHAPI';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {                   
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    GetOtherCreditorApprovedlistERP();
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

        function GetOtherCreditorApprovedlistERP() {
            var url = 'api/AgrMstCreditorMaster/GetCreditorApprovedSummary';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.creditoradd_list = resp.data.MdlcreditorCreation;
                }
                else unlockUI();
            });
        }
        //check
    }
})();
