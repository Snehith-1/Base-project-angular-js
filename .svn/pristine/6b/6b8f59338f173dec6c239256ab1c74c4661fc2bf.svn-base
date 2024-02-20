(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstCreditorMasterSummaryController', AgrMstCreditorMasterSummaryController);

    AgrMstCreditorMasterSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'cmnfunctionService'];

    function AgrMstCreditorMasterSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstCreditorMasterSummaryController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        //lockUI();
        activate();
        function activate() {
            lockUI();

            $scope.showallbtn = true;
            $scope.open = true;
            $scope.submit = false;
            $scope.Approved = false;
            $scope.reject = false;
            $('#opentab').addClass('tabactivecolorstyle');
            var url = 'api/AgrMstCreditorMaster/GetRMCreditorCountDetail';
            SocketService.get(url).then(function (resp) {
                $scope.CreditorCount = resp.data;
            });
            ////var url = 'api/AgrMstWarehouseAdd/GetNewWarehouseSummary';
            //SocketService.get(url).then(function (resp) {
            //    unlockUI();
            //    $scope.warehouseadd_list = resp.data.MdlAgrMstWarehouseCreation;
            //});  

            var url = 'api/AgrMstCreditorMaster/GetRMCreditorOpenSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.creditoradd_list = resp.data.MdlcreditorCreation;
            });


        }

        $scope.pending = function () {
            $scope.showallbtn = false;
            $scope.open = false;
            $scope.submit = true;
            $scope.Approved = false;
            $scope.reject = false;
            $('#approvalpendingtab').addClass('tabactivecolorstyle');
            $('#opentab').removeClass('tabactivecolorstyle');
            $('#approvedtab').removeClass('tabactivecolorstyle');
            $('#rejectedtab').removeClass('tabactivecolorstyle');
            lockUI();
            var url = 'api/AgrMstCreditorMaster/GetRMCreditorCountDetail';
            SocketService.get(url).then(function (resp) {
                $scope.CreditorCount = resp.data;
            });
            var url = 'api/AgrMstCreditorMaster/GetRMCreditorApprovalPendingSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.creditoradd_list = resp.data.MdlcreditorCreation;
            });

        }

        $scope.approved = function () {
            $scope.showallbtn = false;
            $scope.open = false;
            $scope.submit = false;
            $scope.Approved = true;
            $scope.reject = false;
            $('#approvedtab').addClass('tabactivecolorstyle');
            $('#opentab').removeClass('tabactivecolorstyle');
            $('#approvalpendingtab').removeClass('tabactivecolorstyle');
            $('#rejectedtab').removeClass('tabactivecolorstyle');
            lockUI();
            var url = 'api/AgrMstCreditorMaster/GetRMCreditorCountDetail';
            SocketService.get(url).then(function (resp) {
                $scope.CreditorCount = resp.data;
            });
            var url = 'api/AgrMstCreditorMaster/GetRMCreditorApprovedSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.creditoradd_list = resp.data.MdlcreditorCreation;
            });

        }

        $scope.rejected = function () {
            $scope.showallbtn = false;
            $scope.open = false;
            $scope.submit = false;
            $scope.Approved = false;
            $scope.reject = true;
            $('#rejectedtab').addClass('tabactivecolorstyle');
            $('#opentab').removeClass('tabactivecolorstyle');
            $('#approvalpendingtab').removeClass('tabactivecolorstyle');
            $('#approvedtab').removeClass('tabactivecolorstyle');
            lockUI();
            var url = 'api/AgrMstCreditorMaster/GetRMCreditorCountDetail';
            SocketService.get(url).then(function (resp) {
                $scope.CreditorCount = resp.data;
            });
            var url = 'api/AgrMstCreditorMaster/GetRMRejectedCreditorSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.creditoradd_list = resp.data.MdlcreditorCreation;
            });

        }


        $scope.openfn = function () {
            $scope.showallbtn = true;
            $scope.open = true;
            $scope.submit = false;
            $scope.Approved = false;
            $scope.reject = false;
            $('#opentab').addClass('tabactivecolorstyle');
            $('#approvedtab').removeClass('tabactivecolorstyle');
            $('#approvalpendingtab').removeClass('tabactivecolorstyle');
            $('#rejectedtab').removeClass('tabactivecolorstyle');
            lockUI();
            var url = 'api/AgrMstCreditorMaster/GetRMCreditorCountDetail';
            SocketService.get(url).then(function (resp) {
                $scope.CreditorCount = resp.data;
            });
            var url = 'api/AgrMstCreditorMaster/GetRMCreditorOpenSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.creditoradd_list = resp.data.MdlcreditorCreation;
            });

        }


        $scope.creditor_Add = function () {
            $state.go('app.AgrMstCreditorMasterAdd');
        }

        $scope.creditor_view = function (creditor_gid) {
            $location.url('app/AgrMstCreditorMasterView?hash=' + cmnfunctionService.encryptURL('creditor_gid=' + creditor_gid ));
        }

        $scope.creditor_edit = function (creditor_gid) {
            $location.url('app/AgrMstCreditorMasterEdit?hash=' + cmnfunctionService.encryptURL('creditor_gid=' + creditor_gid ));
        }

        $scope.RM_view = function (creditor_gid) {
            $location.url('app/AgrMstCreditorMasterView?hash=' + cmnfunctionService.encryptURL('creditor_gid=' + creditor_gid + '&lspage=AppRM'));
        }

        //$scope.SubmitApproval = function (creditor_gid, creditorref_no, Applicant_name) {
          
        //    var modalInstance = $modal.open({
        //        templateUrl: '/SubmitApproval.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'lg'
        //    });

        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {
        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };

                
        //        $scope.txtcustomer_name = Applicant_name;
        //        $scope.txtapplication_no = creditorref_no;


        //        $scope.submit = function () {
        //            var params = {
        //                creditor_gid: creditor_gid,
        //                initiated_remarks: $scope.txtremarks
        //            }

        //            var url = 'api/AgrMstCreditorMaster/PostCreditorSubmitApproval';
        //            SocketService.post(url, params).then(function (resp) {
                   
        //                if (resp.data.status == true) {
        //                    Notify.alert(resp.data.message, {
        //                        status: 'success',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
                            
        //                }
        //                else {
        //                    Notify.alert(resp.data.message, {
        //                        status: 'warning',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                }
        //            });
        //            activate();
        //            $modalInstance.close('closed');

        //        }
        //    }
        //}
    

        $scope.SubmitApproval = function (creditor_gid) {

            var params = {
                creditor_gid: creditor_gid,
                initiated_remarks: $scope.txtremarks
            }

            var url = 'api/AgrMstCreditorMaster/PostCreditorSubmitApproval';
            SocketService.post(url, params).then(function (resp) {

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
            });
            activate();

        }

}
})();
