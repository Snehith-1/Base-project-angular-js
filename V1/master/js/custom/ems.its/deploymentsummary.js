(function () {
    'use strict';

    angular
        .module('angle')
        .controller('deploymentsummaryController', deploymentsummaryController);

    deploymentsummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];
    function deploymentsummaryController($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        var vm = this;
        vm.title = 'deploymentsummaryController';

        activate();

        function activate() {
          
            var url = 'api/deployment/getSummaryLive';
            // Live Deployment Data
            SocketService.get(url).then(function (resp) {
                $scope.liveData = resp.data.SummariesLive;
                
                $scope.liveTable = new ngTableParams({
                    page: 1,
                    count: 5
                    
                }, {
                    total: $scope.liveData.length,
                        getData: function ($defer, params) {
                        $scope.datasLive = $scope.liveData.slice((params.page() - 1) * params.count(), params.page() * params.count());
                        $defer.resolve($scope.datasLive);
                    }
                });
            });
            var url = 'api/deployment/getSummaryUAT';
            // UAT Deployment Data
            SocketService.get(url).then(function (resp) {
                $scope.UATData = resp.data.SummariesUAT;
                
                $scope.UATtable = new ngTableParams({
                    page: 1,
                    count: 5
                 
                }, {
                    total: $scope.UATData.length,
                    getData: function ($defer, params) {
                        
                        $scope.datasUAT = $scope.UATData.slice((params.page() - 1) * params.count(), params.page() * params.count());
                        $defer.resolve($scope.datasUAT);
                    }
                });
            });
            var url = 'api/deployment/getSummaryTest';
            // Test Deployment Data 
            SocketService.get(url).then(function (resp) {
                $scope.TestData = resp.data.SummariesTest;
                
                $scope.Testtable = new ngTableParams({
                    page: 1,
                    count: 5
                   
                }, {
                    total: $scope.TestData.length,
                    getData: function ($defer, params) {
                        
                        $scope.datasTest = $scope.TestData.slice((params.page() - 1) * params.count(), params.page() * params.count());
                        $defer.resolve($scope.datasTest);
                    }
                });
            });

        };
        $scope.add = function () {
            $state.go('app.deploymentadd');
        };


        $scope.viewModal = function (val) {
           
            var modalInstance = $modal.open({
                templateUrl: '/myModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var url ='api/deployment/viewDepDtl';
                SocketService.get(url + '?dep_gid=' + val).then(function (resp) {
                    $scope.viewDepData = resp.data;
                    if ($scope.viewDepData.new_reports == '') {
                        $scope.viewDepData.new_reports = 'No New Reports Added';
                    }
                    
                    if ($scope.viewDepData.new_pages == '') {
                        $scope.viewDepData.new_pages = 'No New Pages Added';
                    }
                    
                    if ($scope.viewDepData.dep_by == '---') {
                        $scope.viewDepData.dep_by = 'Not Yet Deployed';
                    }
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.cancel = function () {
                    $modalInstance.dismiss('cancel');
                };
            }
        };

      
       
    }
})();

