(function () {
    'use strict';
    angular
           .module('angle')
           .controller('idasTrnDocumentTaggingController', idasTrnDocumentTaggingController);

           idasTrnDocumentTaggingController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function idasTrnDocumentTaggingController($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'idasTrnDocumentTaggingController';

        activate();
        lockUI();
        function activate() {
            $scope.totalDisplayed = 50;
            var url = 'api/IdasTrnDocumentUpload/GetDocumentTaggedCustomer';
          
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.customer_data = resp.data.customer_list;
                // new code start   
                if ($scope.customer_data == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.customer_data.length;
                    if ($scope.customer_data.length < 100) {
                        $scope.totalDisplayed = $scope.customer_data.length;
                    }
                }
                // new code end
                // $scope.total=$scope.customer_data.length;
            });
            var url = 'api/IdasTrnDocumentUpload/GetDocumentCustomerCount';

            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.tagged_count = resp.data.tagged_count;
                $scope.untagged_count = resp.data.untagged_count;
            });
        }

        $scope.viewcustomerdocument = function (customer_gid) {
            $location.url('app/idasTrnDocumentTaggingView?customer_gid='+customer_gid);
        }

        // UnTagged Customer Tab

        $scope.UnTagged_customer = function () {
            lockUI();
            $scope.totalDisplayed = 50;
            var url = 'api/IdasTrnDocumentUpload/GetDocumentUnTaggedCustomer';

            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.customer_data = resp.data.customer_list;
                // new code start   
                if ($scope.customer_data == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.customer_data.length;
                    if ($scope.customer_data.length < 100) {
                        $scope.totalDisplayed = $scope.customer_data.length;
                    }
                }
                
            });
        }
       
        // Tagged Customer Tab

        $scope.Tagged_customer = function () {
            lockUI();
            $scope.totalDisplayed = 50;
            var url = 'api/IdasTrnDocumentUpload/GetDocumentTaggedCustomer';

            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.customer_data = resp.data.customer_list;
                // new code start   
                if ($scope.customer_data == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.customer_data.length;
                    if ($scope.customer_data.length < 100) {
                        $scope.totalDisplayed = $scope.customer_data.length;
                    }
                }
                // new code end
                // $scope.total=$scope.customer_data.length;
            });
        }
        $scope.loadMore = function (pagecount) {
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            // new code start
            if ($scope.customer_data != null) {

                if (pagecount < $scope.customer_data.length) {
                    $scope.totalDisplayed += Number;
                    if ($scope.customer_data.length < $scope.totalDisplayed) {
                        $scope.totalDisplayed = $scope.customer_data.length;
                        Notify.alert(" Total Summary " + $scope.customer_data.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.customer_data.length + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            // $scope.totalDisplayed += Number;
            // console.log(pagecount);
           
        };
    }
})();
