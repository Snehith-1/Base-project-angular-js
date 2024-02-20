(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstProductSummaryController', MstProductSummaryController);

    MstProductSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService'];

    function MstProductSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstProductSummaryController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();
        function activate() {
            var url = 'api/MstApplication360/GetProducts';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.products_list = resp.data.application_list;
                unlockUI();
            });
        }

        $scope.addproduct = function () {
            $location.url('app/MstProductAdd');
        }

        $scope.editproduct = function (product_gid) {
            $location.url('app/MstProductEdit?lsproduct_gid=' + product_gid);
        }
    
        $scope.viewproduct = function (product_gid) {
            $location.url('app/MstProductView?lsproduct_gid=' + product_gid);
        }
        //Product Report 
        $scope.productreport = function () {
            lockUI();
            var url = 'api/MstApplicationReport/ExportExcelAddProduct';
            SocketService.get(url).then(function (resp) {
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

        $scope.Status_update = function (product_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusproduct.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    product_gid: product_gid
                }
                lockUI();
                var url = 'api/MstApplication360/EditProduct';            
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtproduct_code = resp.data.product_code;
                    $scope.txtproduct_name = resp.data.product_name;
                    $scope.rbo_status = resp.data.Status;
                    unlockUI();
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        product_gid: product_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    lockUI();
                    var url = 'api/MstApplication360/InactiveProduct';                 
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
                        } activate();
                    });

                    $modalInstance.close('closed');

                }
                var params = {
                    product_gid: product_gid
                }

                var url = 'api/MstApplication360/InactiveProductHistory';

                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.productinactivelog_data = resp.data.inactivehistory_list;
                    unlockUI();
                });
            }
        }

        $scope.delete = function (product_gid) {
            var params = {
                product_gid: product_gid
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
                    var url = 'api/MstApplication360/DeleteProduct';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
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
        }      
    }
})();
