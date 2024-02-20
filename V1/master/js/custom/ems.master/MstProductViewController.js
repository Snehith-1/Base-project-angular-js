(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstProductViewController', MstProductViewController);

    MstProductViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstProductViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstProductViewController';
        $scope.product_gid = $location.search().lsproduct_gid;
        var product_gid = $scope.product_gid;

        lockUI();
        activate();
        function activate() {
            var param = {
                product_gid: product_gid
            };
            var url = 'api/MstApplication360/GetVarietyEditList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.varietyedit_list = resp.data.variety_list;
                unlockUI();
            });

            var params = {
                product_gid: product_gid
            }
            var url = 'api/MstApplication360/EditProduct';
            SocketService.getparams(url, params).then(function (resp) {

                $scope.lblproduct_code = resp.data.product_code;
                $scope.lblproduct_name = resp.data.product_name;
                $scope.cboSector = resp.data.businessunit_name;
                $scope.cboCategory = resp.data.valuechain_name;
                $scope.cboCSACategory = resp.data.category_name;
                unlockUI();
            });       
        }
  
        $scope.back = function () {
            $state.go('app.MstProductSummary');
        }

        $scope.varity_addgststatus = function (variety_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/ViewGSTStatus.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                lockUI();
                var params = {
                    variety_gid: variety_gid
                }
                var url = 'api/AgrMstSamAgroMaster/GetCommodityGstList';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.commoditygststatuslist = resp.data.commoditygststatus;
                });
                unlockUI();
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.varity_addtradeproduct = function (variety_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/ViewTradeProductDetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                lockUI();
                var params = {
                    variety_gid: variety_gid
                }
                var url = 'api/AgrMstSamAgroMaster/GetCommodityTradeProdctList';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.commodityTradeProdctlist = resp.data.commodityTradeProdct;
                    unlockUI();
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.varity_adddocumentupload = function (variety_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/ViewDocumentUpload.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance', 'DownloaddocumentService'];
            function ModalInstanceCtrl($scope, $modalInstance, DownloaddocumentService) {
                lockUI();
                var params = {
                    variety_gid: variety_gid
                }
                var url = 'api/AgrMstSamAgroMaster/GetCommodityDocumentUploadList';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.commodityDocumentUpload = resp.data.commodityDocumentUpload;
                    unlockUI();
                });

                $scope.doc_downloads = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }
                $scope.documentviewer = function (val1, val2) {
                    lockUI();
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                    if (IsValidExtension == false) {
                        Notify.alert("View is not supported for this format..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        return false;
                    }
                    DownloaddocumentService.DocumentViewer(val1, val2);
                }

                $scope.commoditydownloadall = function () {
                    for (var i = 0; i < $scope.commodityDocumentUpload.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.commodityDocumentUpload[i].commodityreport_filepath, $scope.commodityDocumentUpload[i].commodityreport_filename);
                    }
                }

                $scope.riskdownloadall = function () {
                    for (var i = 0; i < $scope.commodityDocumentUpload.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.commodityDocumentUpload[i].riskanalysisreport_filepath, $scope.commodityDocumentUpload[i].riskanalysisreport_filename);
                    }
                }
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
    }
})();