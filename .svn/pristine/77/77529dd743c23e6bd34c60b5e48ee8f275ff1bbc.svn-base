(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstUDCMakerEditController', MstUDCMakerEditController);

        MstUDCMakerEditController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstUDCMakerEditController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstUDCMakerEditController';

        

        activate();

        function activate() {      

            $scope.udcmanagement_gid = $location.search().lsudcmanagement_gid;

            var params = {
                udcmanagement_gid: $scope.udcmanagement_gid
            }

            var url = 'api/UdcManagement/ChequeList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.cheque_list = resp.data.cheque_list;
                unlockUI();
            });

            var url = 'api/UdcManagement/UdcDetailsEdit';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customer_gid = resp.data.customer_gid;
                $scope.txtcustomer_name = resp.data.customer_name;
            });
        }
        $scope.back = function(){
            $location.url('app/MstUDCMakerSummary');
        }
        $scope.add_cheque = function () {
            $location.url('app/MstUDCMakerAddCheque?lscustomer_gid=' + $scope.customer_gid + '&lsudcmanagement_gid=' + $scope.udcmanagement_gid + '&lstab=edit');

        }
        $scope.editCheque = function (udcmanagement2cheque_gid) {
            $location.url('app/MstUDCMakerEditCheque?lsudcmanagement2cheque_gid=' + udcmanagement2cheque_gid + '&lsudcmanagement_gid=' + $scope.udcmanagement_gid + '&lstab=edit');
        }

    

        $scope.viewCheque = function (udcmanagement2cheque_gid) {         
            $location.url('app/MstUDCMakerView?lsudcmanagement2cheque_gid=' + udcmanagement2cheque_gid + '&lsudcmanagement_gid=' + $scope.udcmanagement_gid + '&lstab=edit');

        }

        $scope.deleteCheque = function (udcmanagement2cheque_gid) {
            lockUI();
            var params = {
                udcmanagement2cheque_gid: udcmanagement2cheque_gid
            }
            var url = 'api/UdcManagement/DeleteChequeDetail';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

                var params = {
                    udcmanagement_gid: $scope.udcmanagement_gid
                }
                var url = 'api/UdcManagement/ChequeList';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.cheque_list = resp.data.cheque_list;
                    unlockUI();
                });

                unlockUI();
            });
        }

        $scope.udc_update = function () {
       
                var params = {
                    udcmanagement_gid: $scope.udcmanagement_gid
                }
                var url = 'api/UdcManagement/UpdateUdcDetail';
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
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    $state.go('app.MstUDCMakerSummary');
                });
            
        }



    }
})();