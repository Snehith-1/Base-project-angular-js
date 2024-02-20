(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstEncoreProductController', MstEncoreProductController);

    MstEncoreProductController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService'];

    function MstEncoreProductController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstEncoreProductController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() { 

            $scope.rbo_txtmoratoriamstatus = false;
          
            var url = 'api/MstApplication360/GetEncoreProduct';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.EncoreProduct_List = resp.data.EncoreProduct_List;
                unlockUI();
            });

        }
              

        $scope.addEncoreProduct = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addencoreproduct.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.loantennure_list=[{
                    "id":"0",
                    "loantennure_name":"Day"
                },{
                    "id":"1",
                    "loantennure_name":"Month"
                },{
                    "id":"2",
                    "loantennure_name":"Annual"
                    
                }];

                //dropdown lists - API
                var url = 'api/MstApplication360/GetLoanProductActive';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.encoreproduct_list = resp.data.application_list;
                    unlockUI();
                });
                // var url = 'api/MstApplication360/GetLoanSubProductActive';
                // lockUI();
                // SocketService.get(url).then(function (resp) {
                //     $scope.loansubproduct_list = resp.data.application_list;
                //     unlockUI();
                // });
                var url = 'api/MstApplication360/GetPrincipalFrequencyActive';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.principalfrequency_data = resp.data.application_list;
                    unlockUI();
                });
                var url = 'api/MstApplication360/GetInterestFrequencyActive';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.interestfrequency_data = resp.data.application_list;
                    unlockUI();
                });

                $scope.producttype = function () {
                    var params = {
                        loanproduct_gid: $scope.cboProducts.loanproduct_gid,
                    }
                    var url = 'api/MstApplication360/GetLoanSubProductActive';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.loansubproduct_list = resp.data.application_list;
                        unlockUI();
                    });
                }

                $scope.moratoriamstatus_yes = function () { 
                    debugger
                    if ($scope.rbo_moratoriamstatus == 'Y') {
        
                        $scope.moratoriumtype_list=[{
                            "id":"0",
                            "moratoriumtype_name":"Principal"
                        },{
                                "id": "2",
                            "moratoriumtype_name": "Interest"
                            },{
                            "id":"1",
                            "moratoriumtype_name":"Principal and interest"
                            
                        }];
                        $scope.rbo_txtmoratoriamstatus = true;
        
                    }
                    else {
                        $scope.rbo_txtmoratoriamstatus = false;
                        $scope.cboMoratoriumType ="";
                        // scope.moratoriumtype_list=[];
                    }
                }
                                
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {
                    var lsloanproduct_gid = '';
                    var lsloanproduct_name = '';
                    if ($scope.cboProducts != undefined || $scope.cboProducts != null) {
                        lsloanproduct_gid = $scope.cboProducts.loanproduct_gid;
                        lsloanproduct_name = $scope.cboProducts.loanproduct_name;
                    }
                    var lsloansubproduct_gid = '';
                    var lsloansubproduct_name = '';
                    if ($scope.cboSubProduct != undefined || $scope.cboSubProduct != null) {
                        lsloansubproduct_gid = $scope.cboSubProduct.loansubproduct_gid;
                        lsloansubproduct_name = $scope.cboSubProduct.loansubproduct_name;
                    }
                    var lsprincipalfrequency_gid = '';
                    var lsprincipalfrequency_name = '';
                    if ($scope.cboPrincipalTennure != undefined || $scope.cboPrincipalTennure != null) {
                        lsprincipalfrequency_gid = $scope.cboPrincipalTennure.principalfrequency_gid;
                        lsprincipalfrequency_name = $scope.cboPrincipalTennure.principalfrequency_name;
                    }
                    var lsinterestfrequency_gid = '';
                    var lsinterestfrequency_name = '';
                    if ($scope.cboInterestTennure != undefined || $scope.cboInterestTennure != null) {
                        lsinterestfrequency_gid = $scope.cboInterestTennure.interestfrequency_gid;
                        lsinterestfrequency_name = $scope.cboInterestTennure.interestfrequency_name;
                    }
                    var lsloantennure_gid = '';
                    var lsloantennure_name = '';
                    if ($scope.cboLoanTennure != undefined || $scope.cboLoanTennure != null) {
                        lsloantennure_gid = $scope.cboLoanTennure.id;
                        lsloantennure_name = $scope.cboLoanTennure.loantennure_name;
                    }
                    var lsmoratoriumtype_gid = '';
                    var lsmoratoriumtype_name = '';
                    if ($scope.rbo_moratoriamstatus == 'Y') {
                        if ($scope.cboMoratoriumType != undefined || $scope.cboMoratoriumType != null) {
                            lsmoratoriumtype_gid = $scope.cboMoratoriumType.id;
                            lsmoratoriumtype_name = $scope.cboMoratoriumType.moratoriumtype_name;
                    }
                }
                    
                    var params = {
                        Products: lsloanproduct_name,
                        Products_gid: lsloanproduct_gid,
                        SubProduct_gid: lsloansubproduct_gid,
                        SubProduct: lsloansubproduct_name,
                        PrincipalTennure_gid: lsprincipalfrequency_gid,
                        PrincipalTennure: lsprincipalfrequency_name,
                        InterestTennure_gid: lsinterestfrequency_gid,
                        InterestTennure: lsinterestfrequency_name,
                        Intdeductstatus: $scope.rbo_intdeductstatus,
                        LoanTennure_gid: lsloantennure_gid,
                        LoanTennure: lsloantennure_name,
                        Moratoriamstatus: $scope.rbo_moratoriamstatus,
                        MoratoriumType_gid: lsmoratoriumtype_gid,
                        MoratoriumType: lsmoratoriumtype_name,
                        lms_code: $scope.txtlms_code
                    }
                    var url = 'api/MstApplication360/CreateEncoreProduct';
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
                
            }
        }

        $scope.editencoreproduct = function (encoreproduct_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editencoreproduct.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                 var params = {
                    encoreproduct_gid: encoreproduct_gid
                }
                 var url = 'api/MstApplication360/EditEncoreProduct';
                SocketService.getparams(url, params).then(function (resp) {

                        $scope.edittxtProducts = resp.data.Products;
                        $scope.edittxtSubProduct = resp.data.SubProduct;
                        $scope.edittxtPrincipalTennure = resp.data.PrincipalTennure;
                        $scope.edittxtInterestTennure = resp.data.InterestTennure;
                        $scope.rbo_editintdeductstatus = resp.data.Intdeductstatus;
                        $scope.edittxtLoanTennure = resp.data.LoanTennure;
                        $scope.rbo_editmoratoriamstatus = resp.data.Moratoriamstatus;
                        $scope.edittxtMoratoriumType = resp.data.MoratoriumType;
                        $scope.edittxtlms_code = resp.data.lms_code;
                        $scope.EncoreProductLMSlog_List = resp.data.EncoreProductLMSlog_List;

                }); 
                
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                
               
                $scope.update = function () {

                    var url = 'api/MstApplication360/UpdateEncoreProduct';
                    var params = {
                        Products: $scope.edittxtProducts,
                        SubProduct: $scope.edittxtSubProduct,
                        PrincipalTennure: $scope.edittxtPrincipalTennure,
                        InterestTennure: $scope.edittxtInterestTennure,
                        Intdeductstatus: $scope.rbo_editintdeductstatus,
                        LoanTennure: $scope.edittxtLoanTennure,
                        Moratoriamstatus: $scope.rbo_editmoratoriamstatus,
                        MoratoriumType: $scope.edittxtMoratoriumType,
                        lms_code: $scope.edittxtlms_code,
                        encoreproduct_gid: encoreproduct_gid
                    }
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        activate();

                    }); 

                }
                
            }
        }

        $scope.Status_update = function (encoreproduct_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusencoreproduct.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    encoreproduct_gid: encoreproduct_gid
                }
                var url = 'api/MstApplication360/EditEncoreProduct';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.encoreproduct_gid = resp.data.encoreproduct_gid;
                    $scope.cboProducts = resp.data.Products;
                    $scope.cboSubProduct = resp.data.SubProduct;
                    $scope.rbo_status = resp.data.status_log;
                });    
                var url = 'api/MstApplication360/GetEncoreProductInActiveLog';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.Encoreproductinactive_list = resp.data.EncoreProduct_List;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        encoreproduct_gid:encoreproduct_gid,
                        Products: $scope.cboProducts,
                        SubProduct: $scope.cboSubProduct,
                        remarks: $scope.txtremarks,
                        status_log:$scope.rbo_status
                        
                    }
                    var url = 'api/MstApplication360/EncoreProductStatusUpdate';
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
                
            }
        }

        $scope.delete = function (encoreproduct_gid) {
             var params = {
                encoreproduct_gid: encoreproduct_gid
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
                            lockUI();
                            var url = 'api/MstApplication360/DeleteEncoreProduct';
                            SocketService.getparams(url, params).then(function (resp) {
                                unlockUI();
                                if (resp.data.status == true) {
                                    SweetAlert.swal('Deleted Successfully!');
                                }
                                else {
                                    alert(resp.data.message, {
                                        status: 'warning',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    
                                }
                                activate();
                                unlockUI;
                            });
                            }
                    });
        }

        $scope.excelreport = function () {
            lockUI();
            var url = 'api/MstApplicationReport/ExportMstEncoreReport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);

                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'warning')

                }

            });
        }
    }
})();

