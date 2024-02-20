(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstProductDeskMappingController', AgrMstProductDeskMappingController);

        AgrMstProductDeskMappingController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrMstProductDeskMappingController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstProductDeskMappingController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;


        activate();
        
        function activate() {
            var url = 'api/AgrMstSamAgroMaster/GetProductDeskSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.ProductDesk = resp.data.ProductDesk;
                unlockUI();
            });
        }


        $scope.addproductdesk = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addproductdesk.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var url = 'api/SystemMaster/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.cboproductdeskmember_list = resp.data.employeelist;
                    $scope.cboproductdeskmanager_list = resp.data.employeelist;

                    unlockUI();
                });
                var url = 'api/AgrMstSamAgroMaster/GetProductsNameSummary';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.cboproducts_name = resp.data.Products_Name;
                    unlockUI();
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
               
              /*  var url = 'api/SystemMaster/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.cboproductdeskmanager_list = resp.data.employeelist;
                    unlockUI();
                }); */
                $scope.submit = function () {
                    var params = {
                        productdesk_name: $scope.txtproductdesk_name,
                        products_gid: $scope.txtcboproducts_name.products_gid,
                        products_name: $scope.txtcboproducts_name.products_name,
                        ProductDeskMember: $scope.cboproductdesk_member,
                        ProductDeskManager: $scope.cboproductdesk_manager,
                        productdesk_lms: $scope.txtproductdesk_lms,
                        productdesk_bureau: $scope.txtproductdesk_bureau

                    }
                    var url = 'api/AgrMstSamAgroMaster/PostProductDeskAdd';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });
                    $modalInstance.close('closed');
                }
            }
        }

        $scope.editproductdesk = function (productdesk_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editproductdesk.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                    

                var url = 'api/AgrMstSamAgroMaster/GetProductsNameSummary';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.cboproducts_name = resp.data.Products_Name;
                    unlockUI();
                });
                
                var params = {
                    productdesk_gid: productdesk_gid
                }
                var url = 'api/AgrMstSamAgroMaster/GetProductDeskEdit';
                    lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditproductdesk_id = resp.data.productdesk_id;
                    $scope.cboeditproducts_gid = resp.data.products_gid;
                    $scope.txteditproductdesk_name = resp.data.productdesk_name;
                    $scope.txteditproductdesk_lms = resp.data.productdesk_lms;
                    $scope.txteditproductdesk_bureau = resp.data.productdesk_bureau;
                    $scope.cboproductdeskmember_editlist = resp.data.ProductDeskMemberem_list;
                    $scope.ProductDeskMember = resp.data.ProductDeskMember;
                    $scope.cboeditproductdesk_member = [];
                    if (resp.data.ProductDeskMember != null) {
                        var count = resp.data.ProductDeskMember.length;
                        for (var i = 0; i < count; i++) {
                            var indexs = $scope.cboproductdeskmember_editlist.map(function (x) { return x.employee_gid; }).indexOf(resp.data.ProductDeskMember[i].employee_gid);
                            $scope.cboeditproductdesk_member.push($scope.cboproductdeskmember_editlist[indexs]);
                        }
                    }
                    
                    $scope.cboproductdeskmanager_editlist = resp.data.ProductDeskManagerem_list;
                    $scope.ProductDeskManager = resp.data.ProductDeskManager;
                    $scope.cboeditproductdesk_manager = [];
                    if (resp.data.ProductDeskManager != null) {
                        var count = resp.data.ProductDeskManager.length;
                        for (var i = 0; i < count; i++) {
                            var indexs = $scope.cboproductdeskmanager_editlist.map(function (x) { return x.employee_gid; }).indexOf(resp.data.ProductDeskManager[i].employee_gid);
                            $scope.cboeditproductdesk_manager.push($scope.cboproductdeskmanager_editlist[indexs]);
                        }
                    }
                    unlockUI();
                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {
                    lockUI();
                    var cboproducts_name = $('#cboproducts_name :selected').text();
                    var url = 'api/AgrMstSamAgroMaster/PostProductDeskUpdate';
                    var params = {
                        productdesk_gid: productdesk_gid,
                        products_gid: $scope.cboeditproducts_gid,
                        products_name: cboproducts_name,
                        productdesk_name: $scope.txteditproductdesk_name,
                        ProductDeskMember: $scope.cboeditproductdesk_member,
                        ProductDeskManager: $scope.cboeditproductdesk_manager,
                        productdesk_lms: $scope.txteditproductdesk_lms,
                        productdesk_bureau: $scope.txteditproductdesk_bureau
                    }
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();

                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                    });
                    $modalInstance.close('closed');
                   
                }
            }
        }

        $scope.showPopover = function (productdesk_gid, productdesk_name) {
            var modalInstance = $modal.open({
                templateUrl: '/showproductdesk.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    productdesk_gid: productdesk_gid
                }
                lockUI();
                var url = 'api/AgrMstSamAgroMaster/GetProductDeskDetails';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.productdesk_name = productdesk_name;
                    $scope.ProductDeskMember = resp.data.productdesk_member;
                    $scope.ProductDeskManager = resp.data.productdesk_manager;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        
        $scope.Status_update = function (productdesk_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusproductdesk.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    productdesk_gid: productdesk_gid
                }
                var url = 'api/AgrMstSamAgroMaster/GetProductDeskEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.productdesk_gid = resp.data.productdesk_gid
                    $scope.productdesk_name = resp.data.productdesk_name;
                    $scope.rbo_status = resp.data.productdesk_status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        productdesk_gid: productdesk_gid,
                        productdesk_name: $scope.productdesk_name,
                        remarks: $scope.txtremarks,
                        productdesk_status: $scope.rbo_status

                    }
                    var url = 'api/AgrMstSamAgroMaster/PostProductDeskInactive';
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
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        activate();
                    });

                    $modalInstance.close('closed');

                }

                var param = {
                    productdesk_gid: productdesk_gid
                }

                var url = 'api/AgrMstSamAgroMaster/GetProductDeskInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.productdesklog = resp.data.ProductDesklog;
                    unlockUI();
                });

            }
        }
    }
})();
