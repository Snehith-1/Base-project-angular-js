(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstUDCMakerSummaryController', MstUDCMakerSummaryController);

        MstUDCMakerSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstUDCMakerSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstUDCMakerSummaryController';
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            var params = {
                application_gid: application_gid
            }
            var url = 'api/UdcManagement/GetUdcSummary';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.udcmanagement_list = resp.data.udcmanagement_list;
                unlockUI();
            });

            var url = 'api/UdcManagement/GetApprovalDetails';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.maker_name = resp.data.maker_name;
                $scope.checker_name = resp.data.checker_name;
                $scope.approver_name = resp.data.approver_name;
                $scope.maker_approveddate = resp.data.maker_approveddate;
                $scope.checker_approveddate = resp.data.checker_approveddate;
                $scope.approver_approveddate = resp.data.approver_approveddate;
            });            

            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstCadFlow/GetApplicationBasicView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtapplication_no = resp.data.application_no;
                $scope.txtbasiccustomer_name = resp.data.customer_name;
            });
        }

        $scope.addMaker = function(){
            $location.url('app/MstUDCMakerAdd?application_gid=' + application_gid + '&lspage=makerpending');
        }

        $scope.editUdc = function (udcmanagement_gid, udcmanagement2cheque_gid) {
            $location.url('app/MstUDCMakerEditCheque?lsudcmanagement2cheque_gid=' + udcmanagement2cheque_gid + '&lsudcmanagement_gid=' + udcmanagement_gid + '&application_gid=' + application_gid + '&lspage=makerpending');
        }

        $scope.viewCheque = function (udcmanagement2cheque_gid, udcmanagement_gid) {
            $location.url('app/MstUDCMakerView?lsudcmanagement2cheque_gid=' + udcmanagement2cheque_gid + '&lsudcmanagement_gid=' + udcmanagement_gid + '&application_gid=' + application_gid + '&lspage=makerpending');
        }

        $scope.deleteUdc = function (udcmanagement_gid) {
            var params = {
                udcmanagement_gid: udcmanagement_gid
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
                   var url = 'api/UdcManagement/DeleteUdc';
                   SocketService.getparams(url, params).then(function (resp) {
                       if (resp.data.status == true) {
                           activate();
                       }
                       else {
                           Notify.alert('Error Occurred While Deleting Udc!', {
                               status: 'warning',
                               pos: 'top-center',
                               timeout: 3000
                           });
                           activate();
                       }
                   }); 
                   SweetAlert.swal('Deleted Successfully!');
               }

           });
        }

        //$scope.Back = function () {
        //    $location.url('app/MstCadChequeManagementSummary?application_gid=' + application_gid);
        //}

        $scope.proceedtochecker = function () {
            if ($scope.udcmanagement_list == undefined || $scope.udcmanagement_list == "" || $scope.udcmanagement_list == "null") {
                Notify.alert('Kindly add atleast one cheque details!', 'warning')
            }
            else {
                var application_gid = $scope.application_gid;
                var params = {
                    application_gid: application_gid,
                    application_no: $scope.txtapplication_no,
                    customer_name: $scope.txtbasiccustomer_name,
                }

                var url = "api/UdcManagement/PostChequeMakerSubmit";
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status = true) {
                        Notify.alert(resp.data.message, 'success');
                        $location.url('app/MstCadChequeManagementSummary');
                    }
                    else {
                        Notify.alert(resp.data.message, 'warning');
                        activate();
                    }
                });
            }
            
        }


    }
})();
