(function () {
    'use strict';

    angular
        .module('angle')
        .controller('taggedCustomerListController', taggedCustomerListController);

    taggedCustomerListController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function taggedCustomerListController($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        var relpath1;
        vm.title = 'taggedCustomerListController';

        activate();


        function activate() {

            var url = window.location.href;
            var relPath = url.split("lspage=");
            $scope.relpath1 = relPath[1];
            //console.log(relpath1);

            $scope.totalDisplayed = 100;
            var url = 'api/Customer/TaggedCustomerList';
            SocketService.get(url).then(function (resp) {
                $scope.customertag_list = resp.data.customertag_list;
                unlockUI();
                // new code start   
                if ($scope.customertag_list == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.customertag_list.length;
                    if ($scope.customertag_list.length < 100) {
                        $scope.totalDisplayed = $scope.customertag_list.length;
                    }
                }
                // new code endd
                //$scope.total=$scope.covenant_data.length;

            });
        }


        $scope.btnback = function () {
            //console.log($scope.relpath1);
            if ($scope.relpath1 == "regcustomer") {
                //console.log('sub');
                $state.go('app.registerCustomersummary');
              
            }
            else {
                //console.log('main');
                $state.go('app.customerMaster');
            }      
        }

        $scope.loadMore = function (pagecount) {
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            // new code start
            if ($scope.covenant_data != null) {

                if (pagecount < $scope.covenant_data.length) {
                    $scope.totalDisplayed += Number;
                    if ($scope.covenant_data.length < $scope.totalDisplayed) {
                        $scope.totalDisplayed = $scope.covenant_data.length;
                        Notify.alert(" Total Summary " + $scope.covenant_data.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.covenant_data.length + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            // $scope.totalDisplayed += Number;
            // console.log(pagecount);
            unlockUI();
        };
   

        $scope.untag = function (customer_gid, customer_name, customer_urn) {
            var modalInstance = $modal.open({
                templateUrl: '/customeruntag.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            //var doc = document.getElementById('edit');
            //doc.style.display = 'block';
            $scope.customer_gid = customer_gid;
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.customer_name = customer_name;

                if (customer_urn != "") {
                    $scope.customer_urn = customer_urn;
                }
                else {
                    $scope.customer_urn = "-";
                }
                var params = {
                    customer_gid: customer_gid
                }
                var url = 'api/Customer/TaggedHistoryList';
                SocketService.getparams(url, params).then(function (resp) {
                    //console.log(resp.data.customertag_list, url);
                    $scope.customertag_list = resp.data.customertag_list;
                    unlockUI();                  
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.cancel = function () {
                    $modalInstance.dismiss('cancel');
                };
                $scope.btnuntagcustomer = function () {

                    var params = {
                        customer_gid: customer_gid,
                        customer_name: customer_name,
                        currentcustomer_urn: customer_urn,
                        untag_remarks: $scope.untagremarks
                    }
                    var url = 'api/Customer/UnTagtoLegal';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            activate();
                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }
                    });
                }

            }

        }

       



    }
})();