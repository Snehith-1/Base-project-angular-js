(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCadUrnAcceptedCustomersNPATagController', MstCadUrnAcceptedCustomersNPATagController);

        MstCadUrnAcceptedCustomersNPATagController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCadUrnAcceptedCustomersNPATagController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCadUrnAcceptedCustomersNPATagController';

        activate();
        
        function activate() {
            var url = 'api/MstCAD/GetCADUrnGroupingNPATagSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.npatagged_list = resp.data.cadapplicationlist;
            });
            
        }

        $scope.back = function () {
            $location.url('app/MstCadUrnAcceptedCustomers');
        }
        $scope.npa_untag = function (customer2tag_gid,customer_urn,customer_name) {
            var modalInstance = $modal.open({
                templateUrl: '/npauntag.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.customer_urn=customer_urn;
                $scope.customer_name=customer_name;
                var params = {
                    customer2tag_gid : customer2tag_gid
                }
                var url = 'api/MstCAD/URNNPAtagHistory';
                lockUI();
                SocketService.getparams(url,params).then(function (resp) {
                    unlockUI();
                    $scope.npatag_list = resp.data.customerurntag_list;
                });
                $scope.untag_urn = function (){
                    var params = {
                        customer2tag_gid : customer2tag_gid,
                        tag_remarks : $scope.untagremarks,
                        tag_type : 'NPA'
                    }
                    var url = 'api/MstCAD/URNUntag';
                    lockUI();
                    SocketService.post(url,params).then(function (resp) {
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
                        $modalInstance.close('closed');
                        activate();
                    });
                   
                }
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.description= function (description){
                    var modalInstance = $modal.open({
                        templateUrl: '/description.html',
                        controller: ModalInstanceCtrl,
                        backdrop: 'static',
                        keyboard: false,
                        size: 'md'
                    });
                    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                    function ModalInstanceCtrl($scope, $modalInstance) {
                        $scope.description=description;
                        $scope.back = function () {
                            $modalInstance.close('closed');
                        }; 
                    }
                }

            }

        }
        $scope.description= function (description){
            var modalInstance = $modal.open({
                templateUrl: '/description.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.description=description;
                $scope.back = function () {
                    $modalInstance.close('closed');
                }; 
            }
        }
    }
})();
